using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeVisualizer
{
    public partial class ColoredCheckbox : UserControl
    {
        public ColoredCheckbox()
        {
            InitializeComponent();
        }

        public string Label
        {
            get { return checkBox1.Text; }
            set { checkBox1.Text = value; }
        }

        public Color Color
        {
            get { return panel1.BackColor; }
            set { panel1.BackColor = value; }
        }

        public bool Checked
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        public event EventHandler ColorChanged
        {
            add { panel1.BackColorChanged += value; }
            remove { panel1.BackColorChanged -= value; }
        }
        public event EventHandler CheckedChanged
        {
            add { checkBox1.CheckedChanged += value; }
            remove { checkBox1.CheckedChanged -= value; }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                colorDialog1.Color = this.Color;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    this.Color = colorDialog1.Color;
            }
        }
    }
}
