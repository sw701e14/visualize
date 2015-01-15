using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeVisualizer
{
    public class StandstillPainter : IPainter, IGPSConsumer
    {
        private PointF[] points;

        public void Paint(Graphics graphics)
        {
            if (points == null)
                return;

            float scale = graphics.Transform.Elements[0];
            graphics.FillPoints(Brushes.Blue, points, 2f / scale);
        }

        public void Load(Shared.DAL.DatabaseSession session)
        {
            points = GPSData.GetAllHasNotMoved(session).Select(x => x.Location.ToPixel()).ToArray();
        }
    }
}
