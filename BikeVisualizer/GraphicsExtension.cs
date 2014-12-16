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
        private static double getLatPixels(double lat)
        {
            double x = 57.005 - 57.06;
            double y = 657 - 69;
            double a = y / x;

            return a * (lat - 57.005) + 657;
        }
        private static double getLngPixels(double lng)
        {
            double x = 10.02 - 9.84;
            double y = 1237 - 188;
            double a = y / x;

            return a * (lng - 10.02) + 1237;
        }
        private static double getLatGPS(double lat)
        {
            double x = 657 - 69;
            double y = 57.005 - 57.06;
            double a = y / x;

            return a * (lat - 657) + 57.005;
        }
        private static double getLngGPS(double lng)
        {
            double x = 1237 - 188;
            double y = 10.02 - 9.84;
            double a = y / x;

            return a * (lng - 1237) + 10.02;
        }
        public static PointF ToPixels(this GPSLocation location)
        {
            return new PointF((float)getLngPixels((double)location.Longitude), (float)getLatPixels((double)location.Latitude));
        }
        public static PointF ToPixels(this GPSData data)
        {
            return new PointF((float)getLngPixels((double)data.Location.Longitude), (float)getLatPixels((double)data.Location.Latitude));
        }
        public static PointF[] ToPixels(this GPSLocation[] locations)
        {
            PointF[] points = new PointF[locations.Length];
            for (int i = 0; i < points.Length; i++)
                points[i] = ToPixels(locations[i]);
            return points;
        }
        public static PointF[] ToPixels(this GPSData[] data)
        {
            PointF[] points = new PointF[data.Length];
            for (int i = 0; i < points.Length; i++)
                points[i] = ToPixels(data[i].Location);
            return points;
        }

        public static GPSLocation ToGPS(this PointF point)
        {
            return new GPSLocation((decimal)getLatGPS(point.Y), (decimal)getLngGPS(point.X));
        }

        public static void FillPoint(this Graphics graphics, Brush brush, GPSLocation location, float radius = 1.5f)
        {
            var p = ToPixels(location);
            graphics.FillEllipse(brush, p.X - radius, p.Y - radius, radius * 2, radius * 2);
        }

        public static void FillPoint(this Graphics graphics, Brush brush, PointF location, float radius = 1.5f)
        {
            graphics.FillEllipse(brush, location.X - radius, location.Y - radius, radius * 2, radius * 2);
        }

        public static void FillPoints(this Graphics graphics, Brush brush, GPSLocation[] locations, float radius = 1.5f)
        {
            foreach (var p in locations)
                FillPoint(graphics, brush, p, radius);
        }

        
        public static void FillPoints(this Graphics graphics, Brush brush, PointF[] locations, float radius = 1.5f)
        {
            foreach (var p in locations)
                FillPoint(graphics, brush, p, radius);
        }

        public static void DrawCross(this Graphics graphics, Pen pen, GPSLocation loc, float radius = 1.5f)
        {
            var p = ToPixels(loc);

            graphics.DrawLine(pen, new PointF(p.X - radius, p.Y), new PointF(p.X + radius, p.Y));
            graphics.DrawLine(pen, new PointF(p.X, p.Y - radius), new PointF(p.X, p.Y + radius));
        }

        public static void DrawCross(this Graphics graphics, Pen pen, PointF p, float radius = 1.5f)
        {
            graphics.DrawLine(pen, new PointF(p.X - radius, p.Y), new PointF(p.X + radius, p.Y));
            graphics.DrawLine(pen, new PointF(p.X, p.Y - radius), new PointF(p.X, p.Y + radius));
        }

        public static void DrawCrosses(this Graphics graphics, Pen pen, GPSLocation[] locations, float radius = 1.5f)
        {
            foreach (var p in locations)
            {
                DrawCross(graphics, pen, p, radius);
            }
        }

        public static void DrawCrosses(this Graphics graphics, Pen pen, PointF[] locations, float radius = 1.5f)
        {
            foreach (var p in locations)
            {
                DrawCross(graphics, pen, p, radius);
            }
        }


        public static void DrawPoint(this Graphics graphics, Pen brush, GPSLocation location, float radius = 1.5f)
        {
            var p = ToPixels(location);
            graphics.DrawEllipse(brush, p.X - radius, p.Y - radius, radius * 2, radius * 2);
        }
        public static void DrawPoints(this Graphics graphics, Pen brush, GPSLocation[] locations, float radius = 1.5f)
        {
            foreach (var p in locations)
                DrawPoint(graphics, brush, p, radius);
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

        public static void FillPolygon(this Graphics graphics, Brush brush, GPSLocation[] locations)
        {
            graphics.FillPolygon(brush, ToPixels(locations));
        }
        public static void DrawPolygon(this Graphics graphics, Pen pen, GPSLocation[] locations)
        {
            graphics.DrawPolygon(pen, ToPixels(locations));
        }

        public static void DrawLines(this Graphics graphics, Pen pen, GPSLocation[] locations)
        {
            graphics.DrawLines(pen, ToPixels(locations));
        }
    }
}
