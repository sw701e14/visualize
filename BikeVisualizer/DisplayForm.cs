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
    public partial class DisplayForm : Form
    {
        private static readonly Size GoogleMapsSize = new Size(640, 640);

        public DisplayForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Size diff = this.Size - this.ClientSize;
            this.Size = GoogleMapsSize + diff;
        }
    }
}
