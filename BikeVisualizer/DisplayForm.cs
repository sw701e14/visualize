﻿using Shared.DTO;
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
        private const float zoom = 1.2f;
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
    }
}
