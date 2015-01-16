using Shared.DAL;
using System.Drawing;

namespace BikeVisualizer
{
    public abstract class ColoredPainter : IPainter, IGPSConsumer
    {
        private Color color;
        private float pointWidth;

        private bool enabled;

        public ColoredPainter(Color color)
        {
            this.color = color;
            this.pointWidth = 2f;

            this.enabled = true;
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public float PointWidth
        {
            get { return pointWidth; }
            set
            {
                if (value <= 0) value = 0.1f;
                pointWidth = value;
            }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public abstract void Load(DatabaseSession session);

        public void Paint(Graphics graphics)
        {
            if (enabled)
                Paint(graphics, 1f / graphics.Transform.Elements[0]);
        }
        public abstract void Paint(Graphics graphics, float widthScale);
    }
}
