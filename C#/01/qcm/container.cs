using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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

        ouvrirQuestionnaire xs;
        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xs == null)
            {
                xs = new ouvrirQuestionnaire(this);
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

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toutFermerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogRes = MessageBox.Show("Etes-vous sûr de vouloir fermer tous les questionnaires ?", "Tout fermer", MessageBoxButtons.YesNo);
            if (dialogRes == DialogResult.Yes) ;
            {
                foreach (Form form in this.MdiChildren)
                {
                    form.Close();
                }
            }
        }

        fermerQuestionnaire cq;
        private void fermerUnQuestionnaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cq == null)
            {
                cq = new fermerQuestionnaire(this);
                cq.MdiParent = this;
                cq.FormClosed += new FormClosedEventHandler(cq_FormClosed);
                cq.Show();
            }
            else
                cq.Activate();
        }

        private void cq_FormClosed(object sender, FormClosedEventArgs e)
        {
            cq = null;
        }

        validerQuestionnaire vForm;
        private void validerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vForm == null)
            {
                vForm = new validerQuestionnaire(this);
                vForm.MdiParent = this;
                vForm.FormClosed += new FormClosedEventHandler(vForm_FormClosed);
                vForm.Show();
            }
            else
                vForm.Activate();
        }

        private void vForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            vForm = null;
        }
    }
}
