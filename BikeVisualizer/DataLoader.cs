using LocationService.Common;
using LocationService.LocationSource;
using Shared.DAL;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BikeVisualizer
{
    public class DataLoader
    {
        private List<PausableSource> sources;
        private IDataSource dataSource;

        private bool should_stop = false;
        private Thread loadThread;

        private const int SLEEP_MILLISECONDS = 100;

        public DataLoader(int bikeCount)
        {
            if (bikeCount <= 0)
                throw new ArgumentOutOfRangeException("bikeCount");
            resetAndCreateDataSource(bikeCount);

            loadThread = new Thread(runDataLoader);
            loadThread.Start();
        }
        private void resetAndCreateDataSource(int bikeCount)
        {
            List<uint> bikes = new List<uint>();
            for (uint i = 1; i <= bikeCount; i++)
                bikes.Add(i);

            using (Database db = new Database())
                db.RunSession(s =>
                    {
                        s.TruncateAll();

                        foreach (var id in bikes)
                            s.InsertBike(id);
                    });

            DateTime start = DateTime.Now.Date.AddDays(-1);
            sources = bikes.Select(id => new PausableSource(new NoFutureDataSource(GoogleDataSource.GetSource(id, start, int.MaxValue)))).ToList();
            dataSource = new MultiDataSource(sources);
        }

        public bool Paused
        {
            get { return !sources[0].Running; }
            set { foreach(var s in sources) s.Running = !value; }
        }
        public void LetOneThrough()
        {
            foreach (var s in sources) s.LetOneThrough();
        }

        public void Exit()
        {
            should_stop = true;
            while (loadThread.IsAlive) { }
        }

        private void runDataLoader()
        {
            while (!should_stop)
            {
                var data = dataSource.GetData();

                if (data != null)
                    InsertIntoDB(new GPSData(new Bike(data.BikeId), new GPSLocation(data.Latitude, data.Longitude), data.Accuracy, data.Timestamp));
                else
                    Thread.Sleep(SLEEP_MILLISECONDS);
            }
        }

        private void InsertIntoDB(GPSData data)
        {
            using (Database db = new Database())
            {
                db.RunSession(session =>
                {
                    var last = Bike.GetLatestData(session, data.Bike);
                    if (last.HasValue && GPSData.WithinAccuracy(last.Value, data))
                        data.Bike.SetHasNotMoved(session);
                    else
                        GPSData.InsertInDatabase(session, data);
                });
            }
        }

        private class PausableSource : IDataSource
        {
            private IDataSource source;
            private bool running;
            private bool letOneThrough;

            public PausableSource(IDataSource source)
            {
                this.source = source;
                this.running = false;
                this.letOneThrough = false;
            }

            public bool Running
            {
                get { return running; }
                set { running = value; }
            }

            public void LetOneThrough()
            {
                this.letOneThrough = true;
            }

            public GPSInput GetData()
            {
                if (running)
                    return source.GetData();
                else if (letOneThrough)
                {
                    letOneThrough = false;
                    return source.GetData();
                }
                else
                    return null;
            }
        }

    }
}
