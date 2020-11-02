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
    public partial class ouvrirQuestionnaire : Form
    {
        private Form sender;
        private string path;

        public ouvrirQuestionnaire(Form sender)
        {
            this.sender = sender;
            this.path = "..\\..\\xml\\";

            InitializeComponent();

            foreach (string sFileName in System.IO.Directory.GetFiles(path))
            {
                if (System.IO.Path.GetExtension(sFileName) == ".xml")
                {
                    this.xmlListBox.Items.Add(System.IO.Path.GetFileName(sFileName));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            questionnaire questionnaire = new questionnaire(this.path + this.xmlListBox.SelectedItem.ToString());

            // on comptabilise le nombre de questionnaire avec le même nom 
            // pour rajouter un nombre de differenciation au nom du questionnaire
            int nbQuestionnaireNom = 0;
            foreach (Form form in this.sender.MdiChildren)
            {
                nbQuestionnaireNom += 1;
            }
            if (nbQuestionnaireNom != 0)
            {
                questionnaire.Text += " (" + nbQuestionnaireNom + ")";
            }

            questionnaire.MdiParent = this.sender;
            questionnaire.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
