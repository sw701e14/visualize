﻿using DeadDog;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeVisualizer
{
    public class MapPainter : IPainter
    {
        private Control owner;
        private string apiKey;

        private BackgroundWorker loader;
        private Queue<GPSLocation> newLocations;

        private object accesObj = new object();
        private GPSLocation center;
        private Image image;

        public MapPainter(Control owner, string apiKey)
        {
            this.owner = owner;
            this.apiKey = apiKey;

            this.loader = new BackgroundWorker();
            this.newLocations = new Queue<GPSLocation>();

            this.loader.WorkerReportsProgress = true;
            this.loader.DoWork += loader_DoWork;
            this.loader.ProgressChanged += (s, e) => owner.Invalidate();
            this.loader.RunWorkerAsync();
        }

        void loader_DoWork(object sender, DoWorkEventArgs e)
        {
            int count;
            lock (newLocations) { count = newLocations.Count; }

            while (count == 0)
            {
                System.Threading.Thread.Sleep(10);
                lock (newLocations) { count = newLocations.Count; }
            }

            GPSLocation next;
            lock (newLocations)
            {
                while (newLocations.Count > 1) newLocations.Dequeue();
                next = newLocations.Dequeue();
            }
        }

        public void Paint(Graphics graphics)
        {
            lock (accesObj)
                if (image != null)
                    graphics.DrawImage(image, new Rectangle(0, 0, 640, 640));
        }

        private Image loadImage(GPSLocation location)
        {
            string urlStr = string.Format("https://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom=13&size=640x640&key={2}&markers=color:blue|label:S|57.0325,9.93",
                location.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                location.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                apiKey);
            URL url = new URL(urlStr);

            return url.GetImage();
        }
    }
}
