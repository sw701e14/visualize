using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeVisualizer
{
    public class HotspotsPainter : IPainter, IGPSConsumer
    {
        public void Paint(Graphics graphics)
        {
            throw new NotImplementedException();
        }

        public void Load(Shared.DAL.DatabaseSession session)
        {
            throw new NotImplementedException();
        }
    }
}
