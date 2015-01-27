using Shared.DAL;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeVisualizer
{
    public class BikePainter : ColoredPainter
    {
        private PointF[] points = null;
        private Bike bike;

        public BikePainter(uint bikeID, Color color)
            : base(color)
        {
            this.bike = new Bike(bikeID);
            this.points = null;
        }

        public override void Load(DatabaseSession session)
        {
            this.points = GPSData.GetBikeData(session, bike, true).ToPixels();
        }

        public override void Paint(Graphics graphics, float widthScale)
        {
            if (points == null)
                return;

            if (points.Length >= 5)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color)))
                    graphics.FillPoint(brush, points[4], PointWidth * widthScale);

            if (points.Length >= 4)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, Color)))
                    graphics.FillPoint(brush, points[3], PointWidth * widthScale);

            if (points.Length >= 3)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, Color)))
                    graphics.FillPoint(brush, points[2], PointWidth * widthScale);

            if (points.Length >= 2)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(200, Color)))
                    graphics.FillPoint(brush, points[1], PointWidth * widthScale);

            if (points.Length >= 1)
            {
                using (SolidBrush brush = new SolidBrush(Color))
                    graphics.FillPoint(brush, points[0], (PointWidth + 3) * widthScale);
                using (Pen pen = new Pen(Color.Black, widthScale))
                    graphics.DrawPoint(pen, points[0], (PointWidth + 1.5f) * widthScale);
            }
        }
    }
}
