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

            display = new DisplayForm(this, new ColoredPainter[0]);
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
