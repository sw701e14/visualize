using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Draw = System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeVisualizer
{
    public partial class DisplayForm : Form
    {
        private static readonly Size GoogleMapsSize = new Size(640, 640);
        private static readonly GPSLocation InitialCenter = new GPSLocation(57.0325m, 9.93m);

        private Draw.Matrix transform = new Draw.Matrix();

        public DisplayForm()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Size diff = this.Size - this.ClientSize;
            this.Size = GoogleMapsSize + diff;
        }

        private Point mouse = new Point();
        private bool moving = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                moving = true;
                mouse = e.Location;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                moving = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (moving)
                transform.Translate(e.X - mouse.X, e.Y - mouse.Y, Draw.MatrixOrder.Append);
            mouse = e.Location;
            this.Invalidate();
        }
    }
}
