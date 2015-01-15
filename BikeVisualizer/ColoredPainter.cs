using Shared.DAL;
using System.Drawing;

namespace BikeVisualizer
{
    public abstract class ColoredPainter : IPainter, IGPSConsumer
    {
        private Color color;

        public ColoredPainter(Color color)
        {
            this.color = color;
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public abstract void Load(DatabaseSession session);

        public void Paint(Graphics graphics)
        {
            Paint(graphics, 1f / graphics.Transform.Elements[0]);
        }
        public abstract void Paint(Graphics graphics, float widthScale);
    }
}
