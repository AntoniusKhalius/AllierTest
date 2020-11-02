using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using Microsoft.VisualBasic;

namespace qcm
{
    public partial class validerQuestionnaire : Form
    {
        private Form sender;

        public validerQuestionnaire(Form sender)
        {
            InitializeComponent();
            this.sender = sender;
            foreach (Form questionnaire in sender.MdiChildren)
            {
                this.listBox1.Items.Add(questionnaire.Text);
            }
        }

        private void validate_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.sender.MdiChildren)
            {
                if (form.Name.Equals(listBox1.SelectedItem.ToString()))
                {
                    foreach (Control unControl in form.Controls)
                    {
                        questionnaire questionnaire = (questionnaire)form;
                        OdbcConnection cnx = new OdbcConnection("DSN=MySqlQCM");
                        OdbcCommand cmd;
                        string req;

                        string classeControl = unControl.GetType().Name.ToLower();
                        switch (classeControl)
                        {
                            case "combobox":
                                ComboBox laComboBox = (ComboBox)unControl;
                                try
                                {
                                    cnx.Open();
                                    req = "INSERT INTO reponses (cle_questionnaire, utilisateur, rang, date_creation, reponse) VALUES (@cle, @user, @rang, @date, @rep)";
                                    cmd = new OdbcCommand(req, cnx);
                                    cmd.Parameters.Add("@cle", OdbcType.VarChar).Value = "";
                                    cmd.Parameters.Add("@user", OdbcType.VarChar).Value = this.textBox1.Text;
                                    cmd.Parameters.Add("@rang", OdbcType.VarChar).Value = "";
                                    cmd.Parameters.Add("@date", OdbcType.VarChar).Value = new DateTime();
                                    cmd.Parameters.Add("@rep", OdbcType.VarChar).Value = laComboBox.Text;

                                    cmd.ExecuteNonQuery();

                                    cnx.Close();
                                } catch (Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
                                break;

                            case "radiobutton":
                                try
                                {
                                    RadioButton leRadioButton = (RadioButton)unControl;
                                    cnx.Open();
                                    req = "INSERT INTO reponses (cle_questionnaire, utilisateur, rang, date_creation, reponse) VALUES (@cle, @user, @rang, @date, @rep)";
                                    cmd = new OdbcCommand(req, cnx);
                                    cmd.Parameters.Add("@cle", OdbcType.VarChar).Value = "";
                                    cmd.Parameters.Add("@user", OdbcType.VarChar).Value = this.textBox1.Text;
                                    cmd.Parameters.Add("@rang", OdbcType.VarChar).Value = "";
                                    cmd.Parameters.Add("@date", OdbcType.VarChar).Value = new DateTime();
                                    if (leRadioButton.Checked)
                                    {
                                        cmd.Parameters.Add("@rep", OdbcType.VarChar).Value = leRadioButton.Text;
                                    }

                                    cmd.ExecuteNonQuery();

                                    cnx.Close();
                                } catch (Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }

                                break;

                            case "textbox":
                                try
                                {
                                    TextBox laTextBox = (TextBox)unControl;
                                    cnx.Open();
                                    req = "INSERT INTO reponses (cle_questionnaire, utilisateur, rang, date_creation, reponse) VALUES (@cle, @user, @rang, @date, @rep)";
                                    cmd = new OdbcCommand(req, cnx);
                                    cmd.Parameters.Add("@cle", OdbcType.VarChar).Value = "";
                                    cmd.Parameters.Add("@user", OdbcType.VarChar).Value = this.textBox1.Text;
                                    cmd.Parameters.Add("@rang", OdbcType.VarChar).Value = "";
                                    cmd.Parameters.Add("@date", OdbcType.VarChar).Value = new DateTime();
                                    cmd.Parameters.Add("@rep", OdbcType.VarChar).Value = laTextBox.Text;

                                    cmd.ExecuteNonQuery();

                                    cnx.Close();
                                } catch (Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
                                break;

                            case "listbox":
                                try
                                {
                                    ListBox laListBox = (ListBox)unControl;
                                    cnx.Open();
                                    req = "INSERT INTO reponses (cle_questionnaire, utilisateur, rang, date_creation, reponse) VALUES (@cle, @user, @rang, @date, @rep)";
                                    cmd = new OdbcCommand(req, cnx);
                                    cmd.Parameters.Add("@cle", OdbcType.VarChar).Value = "";
                                    cmd.Parameters.Add("@user", OdbcType.VarChar).Value = this.textBox1.Text;
                                    cmd.Parameters.Add("@rang", OdbcType.VarChar).Value = "";
                                    cmd.Parameters.Add("@date", OdbcType.VarChar).Value = new DateTime();
                                    string rep = "";
                                    foreach (string value in laListBox.SelectedItems)
                                    {
                                        rep += " - " + value;
                                    }
                                    cmd.Parameters.Add("@rep", OdbcType.VarChar).Value = rep;

                                    cmd.ExecuteNonQuery();

                                    cnx.Close();
                                } catch (Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
