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
using Shared.DAL;

namespace BikeVisualizer
{
    public partial class DisplayForm : Form
    {
        private const float zoom = 1.2f;
        private static readonly Size GoogleMapsSize = new Size(640, 640);
        private static readonly GPSLocation InitialCenter = new GPSLocation(57.0325m, 9.93m);

        private Draw.Matrix transform = new Draw.Matrix();
        private MapPainter mapPainter;

        private List<ColoredPainter> painters;

        private SettingsForm settings;

        public DisplayForm()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);

            this.painters = new List<ColoredPainter>();
        }

        public DisplayForm(SettingsForm settings, params ColoredPainter[] painters)
            : this()
        {
            this.settings = settings;
            this.painters.AddRange(painters);
        }

        private bool showBackground = true;
        public bool ShowBackground
        {
            get { return showBackground; }
            set { showBackground = value; }
        }

        public void AddPainter(ColoredPainter painter)
        {
            this.painters.Add(painter);
        }
        public void RemovePainter(ColoredPainter painter)
        {
            this.painters.Remove(painter);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!Program.IsClosing)
            {
                Program.IsClosing = true;
                settings.Close();
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            using (Database db = new Database())
                for (int i = 0; i < painters.Count; i++)
                    if (painters[i].Enabled)
                        db.RunSession(session => painters[i].Load(session));
            this.Invalidate();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Location = new Point(10, 10);

            Size diff = this.Size - this.ClientSize;
            this.Size = GoogleMapsSize + diff;

            mapPainter = new MapPainter(this, "AIzaSyDEokkPEUDateZ7nwVv1_BhLkWXskGqMI0");
            mapPainter.SetCenter(InitialCenter, 1);

            refreshTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Transform = transform;
            e.Graphics.SmoothingMode = Draw.SmoothingMode.HighQuality;

            foreach (var p in painters)
                p.Paint(e.Graphics);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (showBackground)
                mapPainter.Paint(e.Graphics);

            e.Graphics.Transform = new Draw.Matrix();

            float x = this.Width - Properties.Resources.location_bar.Width;
            e.Graphics.DrawImage(Properties.Resources.location_bar, x, 0);

            transform.Invert();
            var cursorGPS = transform.TransformPoint((PointF)mouse).ToGPS();
            transform.Invert();

            string text = string.Empty;
            text += cursorGPS.Latitude.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
            text += ", ";
            text += cursorGPS.Longitude.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
            var size = e.Graphics.MeasureString(text, this.Font);
            x += (Properties.Resources.location_bar.Width - size.Width) / 2;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.DrawString(text, this.Font, Brushes.White, x, -1);

#if DEBUG
            e.Graphics.Transform = transform;
            e.Graphics.SmoothingMode = Draw.SmoothingMode.HighQuality;

            e.Graphics.DrawCross(Pens.Red, InitialCenter.ToPixel(), 5);
            e.Graphics.DrawCross(Pens.Red, new GPSLocation(57.005m, 9.98m).ToPixel(), 5);
            e.Graphics.DrawCross(Pens.Red, new GPSLocation(57.06m, 9.88m).ToPixel(), 5);
#endif
        }

        #region DragDropZoom

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
            {
                transform.Translate(e.X - mouse.X, e.Y - mouse.Y, Draw.MatrixOrder.Append);

                transform.Invert();
                var newCenter = transform.TransformPoint(new PointF(319, 319)).ToGPS();
                transform.Invert();

                mapPainter.SetCenter(newCenter, transform.Elements[0]);
            }
            mouse = e.Location;
            this.Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
                doZoom(zoom);
            else
                doZoom(1f / zoom);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            if (Control.ModifierKeys == Keys.Control)
                doZoom(1f / zoom);
            else
                doZoom(zoom);
        }

        private void doZoom(float factor)
        {
            transform.Translate(-mouse.X, -mouse.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
            transform.Scale(factor, factor, System.Drawing.Drawing2D.MatrixOrder.Append);
            transform.Translate(mouse.X, mouse.Y, System.Drawing.Drawing2D.MatrixOrder.Append);

            transform.Invert();
            var newCenter = transform.TransformPoint(new PointF(319, 319)).ToGPS();
            transform.Invert();

            mapPainter.SetCenter(newCenter, transform.Elements[0]);

            this.Invalidate();
        }

        #endregion
    }
}
