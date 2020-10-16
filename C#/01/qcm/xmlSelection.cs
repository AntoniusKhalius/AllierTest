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
    public partial class xmlSelection : Form
    {
        private Form sender;

        public xmlSelection(object sender)
        {
            this.sender = (Form)sender;

            string path = "..\\..\\xml";
            InitializeComponent();
            foreach (string sFileName in System.IO.Directory.GetFiles(path))
            {
                if (System.IO.Path.GetExtension(sFileName) == ".xml")
                {
                    this.xmlListBox.Items.Add(sFileName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            questionnaire questionnaire = new questionnaire(this.xmlListBox.SelectedItem.ToString());
            questionnaire.MdiParent = this.sender;
            questionnaire.Show();
            this.Close();
        }
    }
}
