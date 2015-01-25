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
        private Color[] bikeColors = new Color[] { Color.DodgerBlue, Color.OrangeRed, Color.GreenYellow, Color.Purple, Color.HotPink, Color.MidnightBlue };
        private DisplayForm display;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void hookPainter(ColoredPainter painter, ColoredCheckbox check)
        {
            check.CheckedChanged += (s, ee) => { painter.Enabled = check.Checked; display.Invalidate(); };
            check.ColorChanged += (s, ee) => { painter.Color = check.Color; display.Invalidate(); };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            List<ColoredPainter> painters = new List<ColoredPainter>();

            var moving = new MovingDataPainter() { Enabled = false };
            hookPainter(moving, movingCheck);
            painters.Add(moving);

            var standstill = new StandstillPainter() { Enabled = false };
            hookPainter(standstill, standCheck);
            painters.Add(standstill);

            var hotspots = new HotspotsPainter() { Enabled = false };
            hotspotCheck.CheckedChanged += (s, ee) => { hotspots.Enabled = groupBox1.Enabled = hotspotCheck.Checked; display.Invalidate(); };
            hotspotCheck.ColorChanged += (s, ee) => { hotspots.Color = hotspotCheck.Color; display.Invalidate(); };
            hsPointCheck.CheckedChanged += (s, ee) => { hotspots.DrawPoints = hsPointCheck.Checked; display.Invalidate(); };
            hsFillCheck.CheckedChanged += (s, ee) => { hotspots.DrawFill = hsFillCheck.Checked; display.Invalidate(); };
            painters.Add(hotspots);

            uint[] bikes;
            using (Shared.DAL.Database db = new Shared.DAL.Database())
                bikes = db.RunSession(s => Shared.DTO.Bike.GetAll(s)).Select(x => x.Id).ToArray();

            int y = groupBox1.Location.Y + groupBox1.Height;
            for (int i = 0; i < bikes.Length; i++)
            {
                Color color = bikeColors[i % bikeColors.Length];

                ColoredCheckbox box = new ColoredCheckbox()
                {
                    Color = color,
                    Label = "Bike " + bikes[i]
                };
                this.Controls.Add(box);

                box.Location = new Point(groupBox1.Location.X, y + box.Margin.Top);
                y += box.Margin.Top + box.Height + 3;

                BikePainter painter = new BikePainter(bikes[i], color) { Enabled = false };
                hookPainter(painter, box);
                painters.Add(painter);
            }

            display = new DisplayForm(this, painters.ToArray());
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
