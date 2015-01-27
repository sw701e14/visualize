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
        private DataLoader loader;

        public SettingsForm()
        {
            InitializeComponent();

            resetDataToolStripMenuItem.DropDownOpening += (s, e) => { resetToolStripMenuItem.Enabled = false; bikeCountBox.Text = ""; };
            bikeCountBox.TextChanged += (s, e) =>
            {
                int c;
                resetToolStripMenuItem.Enabled = int.TryParse(bikeCountBox.Text, out c) && c > 0;
            };
            resetToolStripMenuItem.Click += (s, e) =>
            {
                if (loader != null)
                    loader.Exit();

                loader = new DataLoader(int.Parse(bikeCountBox.Text));
                btnStart.Enabled = true;
                btnStep.Enabled = true;
            };
        }

        private void hookPainter(ColoredPainter painter, ColoredCheckbox check)
        {
            check.CheckedChanged += (s, ee) => { painter.Enabled = check.Checked; display.Invalidate(); };
            check.ColorChanged += (s, ee) => { painter.Color = check.Color; display.Invalidate(); };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            mapsCheck.CheckedChanged += (s, ee) => { display.ShowBackground = mapsCheck.Checked; display.Invalidate(); };

            var moving = new MovingDataPainter() { Enabled = false };
            hookPainter(moving, movingCheck);

            var standstill = new StandstillPainter() { Enabled = false };
            hookPainter(standstill, standCheck);

            var hotspots = new HotspotsPainter() { Enabled = false };
            hotspotCheck.CheckedChanged += (s, ee) => { hotspots.Enabled = groupBox1.Enabled = hotspotCheck.Checked; display.Invalidate(); };
            hotspotCheck.ColorChanged += (s, ee) => { hotspots.Color = hotspotCheck.Color; display.Invalidate(); };
            hsPointCheck.CheckedChanged += (s, ee) => { hotspots.DrawPoints = hsPointCheck.Checked; display.Invalidate(); };
            hsFillCheck.CheckedChanged += (s, ee) => { hotspots.DrawFill = hsFillCheck.Checked; display.Invalidate(); };

            display = new DisplayForm(this, new bikeUpdater(this), new dateUpdater(this), moving, standstill, hotspots);
            display.Show();

            this.Location = new Point(
                display.Location.X + display.Width + 30,
                display.Location.Y + (display.Height - this.Height) / 2);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (loader != null)
                loader.Exit();
            if (!Program.IsClosing)
            {
                Program.IsClosing = true;
                display.Close();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (loader.Paused)
            {
                loader.Paused = false;
                btnStep.Enabled = false;
                btnStart.Text = "Pause simulation";
            }
            else
            {
                loader.Paused = true;
                btnStep.Enabled = true;
                btnStart.Text = "Resume simulation";
            }
        }
        private void btnStep_Click(object sender, EventArgs e)
        {
            loader.LetOneThrough();
        }

        private class bikeUpdater : ColoredPainter
        {
            private Color[] bikeColors = new Color[] { Color.DodgerBlue, Color.OrangeRed, Color.GreenYellow, Color.Purple, Color.HotPink, Color.MidnightBlue };
            private SettingsForm owner;

            private Dictionary<uint, ColoredPainter> bikePainters;
            private Dictionary<uint, ColoredCheckbox> bikeCheckboxes;

            public bikeUpdater(SettingsForm owner)
                : base(Color.Black)
            {
                this.owner = owner;

                this.bikePainters = new Dictionary<uint, ColoredPainter>();
                this.bikeCheckboxes = new Dictionary<uint, ColoredCheckbox>();
            }

            public override void Load(Shared.DAL.DatabaseSession session)
            {
                uint[] bikes = Shared.DTO.Bike.GetAll(session).Select(x => x.Id).ToArray();
                uint[] old = bikePainters.Keys.ToArray();
                Array.Sort(bikes);
                Array.Sort(old);

                for (int o = 0, n = 0; o < old.Length || n < bikes.Length; )
                {
                    int d = (o >= old.Length) ? 1 : ((n >= bikes.Length) ? -1 : o.CompareTo(n));
                    if (d < 0)
                    {
                        var box = bikeCheckboxes[old[o]];
                        var painter = bikePainters[old[o]];
                        bikeCheckboxes.Remove(old[o]);
                        bikePainters.Remove(old[o]);

                        owner.bikePanel.Controls.Remove(box);
                        owner.display.RemovePainter(painter);
                        o++;
                    }
                    else if (d > 0)
                    {
                        Color color = bikeColors[n % bikeColors.Length];

                        ColoredCheckbox box = new ColoredCheckbox()
                        {
                            Color = color,
                            Label = "Bike " + bikes[n]
                        };
                        owner.bikePanel.Controls.Add(box);

                        BikePainter painter = new BikePainter(bikes[n], color) { Enabled = false };
                        owner.hookPainter(painter, box);
                        owner.display.AddPainter(painter);

                        bikePainters.Add(bikes[n], painter);
                        bikeCheckboxes.Add(bikes[n], box);
                        n++;
                    }
                    else if (d == 0)
                    {
                        n++;
                        o++;
                    }
                }
            }

            public override void Paint(Graphics graphics, float widthScale)
            {
            }
        }

        private class dateUpdater : ColoredPainter
        {
            private SettingsForm settingsForm;

            public dateUpdater(SettingsForm settingsForm)
                : base(Color.Black)
            {
                this.settingsForm = settingsForm;
            }

            public override void Load(Shared.DAL.DatabaseSession session)
            {
                var latestArr = Shared.DTO.Bike.GetLatestData(session);
                if (latestArr.Length == 0)
                    settingsForm.simLabel.Text = "";
                else
                    settingsForm.simLabel.Text = latestArr.Max(x => x.QueryTime).ToString();
            }

            public override void Paint(Graphics graphics, float widthScale)
            {
            }
        }

    }
}
