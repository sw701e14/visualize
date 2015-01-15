using Shared.DAL;
using Shared.DTO;
using System.Drawing;
using System.Linq;

namespace BikeVisualizer
{
    public class StandstillPainter : PointsPainter
    {
        public StandstillPainter()
            : this(Color.Green)
        { }
        public StandstillPainter(Color color)
            : base(color)
        {
        }

        public override PointF[] LoadPoints(DatabaseSession session)
        {
            return GPSData.GetAllHasNotMoved(session).Select(x => x.Location.ToPixel()).ToArray();
        }
    }
}
