using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeVisualizer
{
    public static class GraphicsExtension
    {
        private static readonly PointF pixel1 = new PointF(28, 25), pixel2 = new PointF(611, 613);
        private static readonly GPSLocation gps1 = new GPSLocation(57.06m, 9.88m), gps2 = new GPSLocation(57.005m, 9.98m);

        private static double getLatPixels(double lat)
        {
            double x = (double)gps2.Latitude - (double)gps1.Latitude;
            double y = pixel2.Y - pixel1.Y;
            double a = y / x;

            return a * (lat - (double)gps2.Latitude) + pixel2.Y;
        }
        private static double getLngPixels(double lng)
        {
            double x = (double)gps2.Longitude - (double)gps1.Longitude;
            double y = pixel2.X - pixel1.X;
            double a = y / x;

            return a * (lng - (double)gps2.Longitude) + pixel2.X;
        }
        private static double getLatGPS(double lat)
        {
            double x = pixel2.Y - pixel1.Y;
            double y = (double)gps2.Latitude - (double)gps1.Latitude;
            double a = y / x;

            return a * (lat - pixel2.Y) + (double)gps2.Latitude;
        }
        private static double getLngGPS(double lng)
        {
            double x = pixel2.X - pixel1.X;
            double y = (double)gps2.Longitude - (double)gps1.Longitude;
            double a = y / x;

            return a * (lng - pixel2.X) + (double)gps2.Longitude;
        }

        public static PointF ToPixel(this GPSLocation location)
        {
            return new PointF((float)getLngPixels((double)location.Longitude), (float)getLatPixels((double)location.Latitude));
        }
        public static PointF ToPixel(this GPSData data)
        {
            return new PointF((float)getLngPixels((double)data.Location.Longitude), (float)getLatPixels((double)data.Location.Latitude));
        }
        public static PointF[] ToPixels(this GPSLocation[] locations)
        {
            PointF[] points = new PointF[locations.Length];
            for (int i = 0; i < points.Length; i++)
                points[i] = ToPixel(locations[i]);
            return points;
        }
        public static PointF[] ToPixels(this GPSData[] data)
        {
            PointF[] points = new PointF[data.Length];
            for (int i = 0; i < points.Length; i++)
                points[i] = ToPixel(data[i].Location);
            return points;
        }

        public static GPSLocation ToGPS(this PointF point)
        {
            return new GPSLocation((decimal)getLatGPS(point.Y), (decimal)getLngGPS(point.X));
        }


        public static void FillPoint(this Graphics graphics, Brush brush, PointF location, float radius = 1.5f)
        {
            graphics.FillEllipse(brush, location.X - radius, location.Y - radius, radius * 2, radius * 2);
        }
        public static void FillPoints(this Graphics graphics, Brush brush, PointF[] locations, float radius = 1.5f)
        {
            foreach (var p in locations)
                FillPoint(graphics, brush, p, radius);
        }

        public static void DrawCross(this Graphics graphics, Pen pen, PointF p, float radius = 1.5f)
        {
            graphics.DrawLine(pen, new PointF(p.X - radius, p.Y), new PointF(p.X + radius, p.Y));
            graphics.DrawLine(pen, new PointF(p.X, p.Y - radius), new PointF(p.X, p.Y + radius));
        }
        public static void DrawCrosses(this Graphics graphics, Pen pen, PointF[] locations, float radius = 1.5f)
        {
            foreach (var p in locations)
            {
                DrawCross(graphics, pen, p, radius);
            }
        }

        public static void DrawPoint(this Graphics graphics, Pen brush, PointF p, float radius = 1.5f)
        {
            graphics.DrawEllipse(brush, p.X - radius, p.Y - radius, radius * 2, radius * 2);
        }
        public static void DrawPoints(this Graphics graphics, Pen brush, PointF[] locations, float radius = 1.5f)
        {
            foreach (var p in locations)
                DrawPoint(graphics, brush, p, radius);
        }

        public static void FillRectangle(this Graphics graphics, Brush brush, PointF p, float radius = 1.5f)
        {
            graphics.FillRectangle(brush, p.X - radius, p.Y - radius, radius * 2, radius * 2);
        }
        public static void FillRectangles(this Graphics graphics, Brush brush, PointF[] locations, float radius = 1.5f)
        {
            foreach (var p in locations)
            {
                graphics.FillRectangle(brush, p, radius);
            }
        }
    }
}
