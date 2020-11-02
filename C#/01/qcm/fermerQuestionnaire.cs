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
    public partial class fermerQuestionnaire : Form
    {
        private Form sender;

        public fermerQuestionnaire(Form sender)
        {
            this.sender = sender;

            InitializeComponent();

            if (this.sender.MdiChildren.Count() > 0)
            {
                this.button1.Enabled = true;
                foreach (Form questionnaire in this.sender.MdiChildren)
                {
                    this.listBox1.Items.Add(questionnaire.Text);
                }
            }
            else
            {
                this.button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.sender.MdiChildren)
            {
                if (form.Text == this.listBox1.SelectedItem.ToString())
                {
                    form.Close();
                }
            }
            this.Close();
        }
    }
}
