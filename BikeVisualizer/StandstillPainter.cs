using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeVisualizer
{
    public class StandstillPainter : ColoredPainter
    {
        private PointF[] points;

        public StandstillPainter()
            : this(Color.Green)
        { }
        public StandstillPainter(Color color)
            : base(color)
        {
        }

        public override void Paint(Graphics graphics, float widthScale)
        {
            if (points == null)
                return;

            using (SolidBrush brush = new SolidBrush(Color))
                graphics.FillPoints(brush, points, 2f * widthScale);
        }

        public override void Load(Shared.DAL.DatabaseSession session)
        {
            points = GPSData.GetAllHasNotMoved(session).Select(x => x.Location.ToPixel()).ToArray();
        }
    }
}
