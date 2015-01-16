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
    public class MovingDataPainter : PointsPainter
    {
        public MovingDataPainter()
            : this(Color.Purple)
        { }
        public MovingDataPainter(Color color)
            : base(color)
        {
            PointWidth = 1.5f;
        }

        public override PointF[] LoadPoints(DatabaseSession session)
        {
            return GPSData.GetAllHasMoved(session).Select(x => x.Location.ToPixel()).ToArray();
        }
    }
}
