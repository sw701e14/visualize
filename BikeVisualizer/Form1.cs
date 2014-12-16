using Shared.DAL;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeVisualizer
{
    public partial class Form1 : Form
    {
        private const float zoom = 1.2f;

        private System.Drawing.Drawing2D.Matrix transform = new System.Drawing.Drawing2D.Matrix();

        private Image mapImage;

        private PointF[] locations;
        private PointF[] hotspotLocations;
        private PointF[] hotspotLocations2;
        private PointF[][] hotspots;

        public Form1()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);



            foreach (Control c in this.Controls)
                c.MouseWheel += (s, e) => OnMouseWheel(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //            mapImage = Image.FromFile("map.png");

            //          transform.Translate((this.Width - 1400) / 2, (this.Height - 700) / 2);

            //BackgroundWorker w = new BackgroundWorker();
            //w.DoWork += (s, ee) => updateClusters();
            //w.RunWorkerAsync();
            //updateClusters();
            timer1.Start();
        }

        private void updateClusters()
        {
            GPSLocation[] loc;
            GPSLocation[] hot;

            using (Database db = new Database())
            {
                loc = db.RunSession(s => GPSData.GetAllHasMoved(s).Select(x => x.Location).ToArray());
                hot = db.RunSession(s => GPSData.GetAllHasNotMoved(s).Select(x => x.Location).ToArray());
                hotspots = db.RunSession(s => Hotspot.LoadAllHotspots(s).Select(x => x.getDataPoints().ToPixels()).ToArray());

                //var m = db.RunSession(s => MarkovChain.LoadMarkovChain(s));
            }

            locations = loc.ToPixels();
            hotspotLocations = hot.ToPixels();

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            saveMap(e.Graphics);

            PointF[] p = new PointF[] { mouse };
            var m = e.Graphics.Transform;
            m.Invert();
            m.TransformPoints(p);

            GPSLocation gps = p[0].ToGPS();

            //e.Graphics.FillPoint(Brushes.Pink, p[0], 4);
            using (Font f = new Font(this.Font.FontFamily, this.Font.Size * 0.05f))
                e.Graphics.DrawString(gps.ToString(), f, Brushes.Black, p[0]);

            //GPSLocation loc = new GPSLocation(57.06m, 9.84m);
            //e.Graphics.FillPoint(Brushes.Red, loc);

        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            e.Graphics.Transform = transform;

            if (mapImage != null)
                e.Graphics.DrawImage(mapImage, new Rectangle(0, 0, 1400, 700));
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
            {
                transform.Translate(e.X - mouse.X, e.Y - mouse.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
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
            this.Invalidate();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.Text = "Neighbours: " + trackBar1.Value + " | Distance: " + trackBar2.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.Text = "Neighbours: " + trackBar1.Value + " | Distance: " + trackBar2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateClusters();
        }

        private void saveMap(Graphics Graphics)
        {
            Pen p = new Pen(Brushes.Gray, 0.1f);

            Graphics.Transform = transform;

            Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if (locations != null)
                Graphics.FillRectangles(Brushes.Gray, locations, 0.15f);

            if (hotspotLocations != null)
                Graphics.FillPoints(Brushes.Black, hotspotLocations, 0.15f);

            if (hotspots != null)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(120, Color.Red)))
                using (Pen pen = new Pen(Color.Black, 0.05f) { LineJoin = System.Drawing.Drawing2D.LineJoin.Round })
                    foreach (var h in hotspots)
                    {
                        // e.Graphics.FillPolygon(brush, h);
                        Graphics.DrawPolygon(pen, h);
                    }


            Graphics.DrawCross(p, new GPSLocation(57.0482921M, 9.9229831M), 0.2f);
            Graphics.DrawCross(p, new GPSLocation(57.0487731m, 9.9220231m), 0.2f); // nytorv 1
            

        }
    }
}
