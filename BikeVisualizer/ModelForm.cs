using ModelUpdater;
using Shared.DAL;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeVisualizer
{
    public partial class ModelForm : Form
    {
        private const int MINIMUMPOINTSINCLUSTER = 4;
        private const double RADIUSINCLUSTER = 15;
        private bool canClose = false;

        public ModelForm()
        {
            InitializeComponent();

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            updateModel();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            canClose = true;
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!canClose)
                e.Cancel = true;
        }


        private static void updateModel()
        {
            Database database = new Database();

            database.RunSession(session =>
            {
                GPSData[] allTheData = GPSData.GetAll(session);

                GPSData[] allStandstillGPSData = GPSData.GetAllHasNotMoved(session);
                if (allStandstillGPSData == null || allStandstillGPSData.Length == 0)
                    return;

                GPSLocation[] allStandstillGPSLocations = getGPSLocationsFromGPSData(allStandstillGPSData);
                if (allStandstillGPSLocations == null || allStandstillGPSLocations.Length == 0)
                    return;

                GPSLocation[][] allClusters = ClusteringTechniques<GPSLocation>.DBSCAN(allStandstillGPSLocations, (a, b) => a.DistanceTo(b) < RADIUSINCLUSTER, MINIMUMPOINTSINCLUSTER);
                if (allClusters == null || allClusters.Length == 0)
                    return;

                GPSData[] latestGPSData = Bike.GetLatestData(session);

                //truncateOldData(session);
                //foreach (var point in latestGPSData)
                //    GPSData.InsertInDatabase(session, point);

                List<Hotspot> hotspots = new List<Hotspot>();
                foreach (var cluster in allClusters)
                {
                    var hs = Hotspot.CreateHotspot(session, cluster);
                    if (hs != null)
                        hotspots.Add(hs);
                }

                MarkovChain.CreateMarkovChain(session, hotspots.ToArray(), allTheData);
            });

            database.Dispose();
        }

        private static GPSLocation[] getGPSLocationsFromGPSData(GPSData[] data)
        {
            GPSLocation[] allGPSLocations = new GPSLocation[data.Count()];

            int i = 0;
            foreach (GPSData gpsdata in data)
            {
                allGPSLocations[i] = gpsdata.Location;
                i++;
            }

            return allGPSLocations;
        }

        private static void truncateOldData(DatabaseSession session)
        {
            DeleteQueries.TruncateGPS_data(session);
            DeleteQueries.TruncateHotspots(session);
            DeleteQueries.TruncateMarkov_chains(session);
        }
    }
}
