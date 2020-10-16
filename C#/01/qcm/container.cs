using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace qcm
{
    public partial class container : Form
    {
        public container()
        {
            InitializeComponent();
        }

        xmlSelection xs;
        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xs == null)
            {
                xs = new xmlSelection(this);
                xs.MdiParent = this;
                xs.FormClosed += new FormClosedEventHandler(xs_FormClosed);
                xs.Show();
            }
            else
                xs.Activate();
        }

        private void xs_FormClosed(object sender, FormClosedEventArgs e)
        {
            xs = null;
        }
    }
}
