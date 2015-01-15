using Shared.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeVisualizer
{
    public abstract class PointsPainter : ColoredPainter
    {
        private PointF[] points;

        public PointsPainter(Color color)
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

        public override void Load(DatabaseSession session)
        {
            points = LoadPoints(session);
        }

        public abstract PointF[] LoadPoints(DatabaseSession session);
    }
}
