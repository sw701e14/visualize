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
    public partial class SettingsForm : Form
    {
        private DisplayForm display;

        public SettingsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var standstill = new StandstillPainter() { Enabled = false };
            standCheck.CheckedChanged += (s, ee) => { standstill.Enabled = standCheck.Checked; display.Invalidate(); };

            var moving = new MovingDataPainter() { Enabled = false };
            movingCheck.CheckedChanged += (s, ee) => { moving.Enabled = movingCheck.Checked; display.Invalidate(); };

            var hotspots = new HotspotsPainter() { Enabled = false };
            hotspotCheck.CheckedChanged += (s, ee) => { hotspots.Enabled = groupBox1.Enabled = hotspotCheck.Checked; display.Invalidate(); };
            hsPointCheck.CheckedChanged += (s, ee) => { hotspots.DrawPoints = hsPointCheck.Checked; display.Invalidate(); };
            hsFillCheck.CheckedChanged += (s, ee) => { hotspots.DrawFill = hsFillCheck.Checked; display.Invalidate(); };

            display = new DisplayForm(this, moving, standstill, hotspots);
            display.Show();

            this.Location = new Point(
                display.Location.X + display.Width + 30,
                display.Location.Y + (display.Height - this.Height) / 2);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!Program.IsClosing)
            {
                Program.IsClosing = true;
                display.Close();
            }
        }
    }
}
