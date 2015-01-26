using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeVisualizer
{
    public class HotspotsPainter : ColoredPainter
    {
        private PointF[][] hotspots;

        public HotspotsPainter()
            : this(Color.Orange)
        { }
        public HotspotsPainter(Color color)
            : base(color)
        {
            this.drawPoints = true;
            this.drawFill = true;

            base.PointWidth *= 2;
        }

        private bool drawPoints;
        private bool drawFill;

        public bool DrawPoints
        {
            get { return drawPoints; }
            set { drawPoints = value; }
        }
        public bool DrawFill
        {
            get { return drawFill; }
            set { drawFill = value; }
        }

        public override void Paint(Graphics graphics, float widthScale)
        {
            if (hotspots == null)
                return;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(120, Color)))
            using (Pen pen = new Pen(Color, PointWidth * widthScale) { LineJoin = System.Drawing.Drawing2D.LineJoin.Round })
                foreach (var h in hotspots)
                {
                    if (drawFill)
                        graphics.FillPolygon(brush, h);
                    if (drawPoints)
                        graphics.DrawPolygon(pen, h);
                }
        }

        public override void Load(Shared.DAL.DatabaseSession session)
        {
            hotspots = Hotspot.LoadAllHotspots(session).Select(x => x.getDataPoints().ToPixels()).ToArray();
        }
    }
}
