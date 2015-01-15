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
        }

        public override void Paint(Graphics graphics, float widthScale)
        {
            if (hotspots == null)
                return;

            float scale = graphics.Transform.Elements[0];

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(120, Color)))
            using (Pen pen = new Pen(Color, PointWidth / scale) { LineJoin = System.Drawing.Drawing2D.LineJoin.Round })
                foreach (var h in hotspots)
                {
                    graphics.FillPolygon(brush, h);
                    graphics.DrawPolygon(pen, h);
                }
        }

        public override void Load(Shared.DAL.DatabaseSession session)
        {
            hotspots = Hotspot.LoadAllHotspots(session).Select(x => x.getDataPoints().ToPixels()).ToArray();
        }
    }
}
