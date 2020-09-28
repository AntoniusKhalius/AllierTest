using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace qcm
{
    public partial class questionnaire : Form
    {
        #region ATTRIBUTS

        // Constantes
        private const int LARGEUR_CONTROLES = 300;
        private const int CARACTERES_PAR_LIGNE = 30;
        private const int HAUTEUR_PAR_LIGNE = 19;

        // Va permettre de définir l'emplacement :
        // 		a) Des contrôles crées dans la feuille
        // 		b) D'une nouvelle feuille en fonction du nombre et 
        // 		de la taille des contrôles qui seront crées dynamiquement
        //
        // Remarque : la structure "Point" représente une paire 
        // ordonnée de coordonnées x et y entières qui définit 
        // un point dans un plan à deux dimensions.
        private Point Emplacement = new Point(10, 10);

        // Document XML associé
        private XmlDocument xr;

        // Titre de la feuille
        private string Titre;

        // Réponse au questionnaire
        private string Réponse;

        #endregion

        #region CONSTRUCTEUR

        public questionnaire()
        {
            InitializeComponent();

            // Remplir le questionnaire à partir du document XML
            // REMARQUE : ici on transmet "en dur" un nom de fichier.
            // Il s'agira de transmettre le fichier choisi par l'utilisateur.
            this.CreerAPartirXML("Gouts.xml");
        }

        #endregion

        #region ACCESSEURS

        // Retourne ou modifie la propriété "Height" de la feuille
        private int LaHauteur
        {
            get { return this.Height; }
            set { this.Height = value; }
        }

        // Retourne ou modifie la propriété "Width" de la feuille
        private int Largeur
        {
            get { return this.Width; }
            set { this.Width = value; }
        }

        // Retourne une COLLECTION des CONTROLES (objets) graphiques figurant sur la feuille
        private Control.ControlCollection TousLesControles
        {
            get { return this.Controls; }
        }

        // Retourne ou modifie la propriété privée "Titre", et dans ce dernier cas, 
        // la propriété "Text" de la feuille est renseignée.
        private string LeTitre
        {
            get { return Titre; }

            set
            {
                Titre = value;
                this.Text = Titre;
            }
        }

        #endregion

        #region METHODES

        //---------------------------------------------------------
        // Création dynamique des contrôles sur la feuille à partir
        //  du contenu d'un document XML représentant un QCM
        //---------------------------------------------------------
        private void CreerAPartirXML(string doc)
        {
            // Accesseur
            this.LeTitre = "Questionnaire";

            // Appel de l'accesseur 'TousLesControles' pour récupérer la COLLECTION de contrôles sur la feuille
            Control.ControlCollection LesControles = this.TousLesControles;

            // Initialisation de l'emplacement
            Emplacement = new Point(10, 10);

            // Creation d'un DOCUMENT XML qui servira à remplir la nouvelle feuille
            xr = new XmlDocument();
            xr.Load(doc);

            // Sélectionne le PREMIER NOEUD ou BALISE (ici : <questionnaire>) et récupère 
            // la valeur de son attribut "name" (ici : name="AppliQuestions")
            // string PremierNoeud = ... ;

            // Initialise la propriété "Titre" de la nouvelle feuille à partir de la valeur
            // de l'attribut "displayName" (ici : displayName="Questionnaire" )
            // this.LeTitre = ... ;

            // Création d'une COLLECTION ordonnée de NOEUDS <question>
            // XmlNodeList LesNoeuds = ;

            // PARCOURS de l'ensemble des noeuds <question> présents dans la collection
            foreach (XmlNode UnNoeud in LesNoeuds)
            {
                if (UnNoeud.Attributes != null)
                {
                    // Détermine le TYPE DU CONTRÔLE (objet graphique) à créer.
                    // Le type est spécifié dans l'attribut "type" : <question type= ... >
                    // Suivant le type de contrôle, une méthode "AddXXX" est appelée.
                    // Les paramètres sont les suivants :
                    // 		a) L'objet noeud <question> en cours
                    // 		b) La collection de contrôles de la feuille
                    // 		c) L'emplacement (coordonnées X et Y)
                    //		d) L'objet premier noeud du document XML (<questionnaire>)
                    switch (UnNoeud.Attributes["type"].Value)
                    {
                        case "combo":
                            Emplacement = AddComboBox(UnNoeud, LesControles, Emplacement, PremierNoeud);
                            break;
                        case "liste":
                            Emplacement = AddListBox(UnNoeud, LesControles, Emplacement, PremierNoeud, true);
                            break;
                        case "text":
                            Emplacement = AddTextBox(UnNoeud, LesControles, Emplacement, PremierNoeud);
                            break;
                        case "radio":
                            Emplacement = AddRadioButtons(UnNoeud, LesControles, Emplacement, PremierNoeud);
                            break;
                    }
                }
            }

            // On spécifie la LARGEUR et la HAUTEUR de la feuille créée dynamiquement.
            // En effet, sa dimension dépend du NOMBRE de contrôles à placer, et par
            // conséquent du contenu du document XML.
            // Un ajustement (de 40) s'avère cependant nécessaire...
            this.Largeur = Emplacement.X + LARGEUR_CONTROLES + 40;
            this.LaHauteur = Emplacement.Y + 40;

            // Affichage du questionnaire
            this.Show();
        }

        //---------------------------------------------------------------------------------------------
        // Ensemble des méthodes qui, suivant le cas vont AJOUTER une ComboBox, une ListBox, 
        // une TextBox ou des RadioButtons à la collection passée en paramètre. 
        //
        // Retournent des coordonnées (X,Y) permettant de définir la dimension de la feuille 
        // qui va contenir ces contrôles...
        //
        // Ces méthodes sont appelées par la méthode "CreerAPartirXML" qui crée d'abord
        // dynamiquement une feuille, puis l'ensemble de ses contrôles, et ceci à partir des 
        // données d'un document XML (un contrôle par noeud <question>)
        //
        // Les paramètres sont les suivants :
        // 	    a) L'objet noeud <question> en cours
        // 	    b) La collection de contrôles de la feuille
        //	    c) L'emplacement (coordonnées X et Y) en cours (permet de placer les nouveaux contrôles)
        //	    d) L'objet premier noeud du document XML (<questionnaire>)
        //----------------------------------------------------------------------------------------------
        
        // A COMPLETER
        private Point AddComboBox(XmlNode unNoeud, Control.ControlCollection desControles, Point unEmplacement, string tag)
        {
            // ...
            // ...

            // Retour de l'emplacement pour placer le nouveau contrôle
            // (ou bien spécifier la dimension de la feuille)
            return unEmplacement;
        }

        // A COMPLETER
        private Point AddListBox(XmlNode unNoeud, Control.ControlCollection desControles, Point unEmplacement, string tag, bool MultiSelect)
        {
            // ...
            // ...

            // Retour de l'emplacement pour placer le nouveau contrôle
            // (ou bien spécifier la dimension de la feuille)
            return unEmplacement;
        }

        // A COMPLETER
        private Point AddRadioButtons(XmlNode unNoeud, Control.ControlCollection desControles, Point unEmplacement, string tag)
        {
            // ...

            // Retour de l'emplacement pour placer le nouveau contrôle
            // (ou bien spécifier la dimension de la feuille)
            return unEmplacement;
        }

        // FOURNI
        private Point AddTextBox(XmlNode unNoeud, Control.ControlCollection desControles, Point unEmplacement, string tag)
        {
            // Création d'un contrôle TextBox.
            TextBox maTextBox = new TextBox();

            // Il y a-t-il une réponse par défaut ? (cf. noeud <defaultreponse>
            if (unNoeud.SelectSingleNode("defaultreponse") != null)
                maTextBox.Text = unNoeud.SelectSingleNode("defaultreponse").InnerText;

            // Valeur de l'attribut "name" de la balise <question> en cours
            if (unNoeud.Attributes["name"] != null)
                maTextBox.Name = unNoeud.Attributes["name"].Value;

            maTextBox.Tag = tag;
            maTextBox.Width = LARGEUR_CONTROLES;

            // Il y a-t-il un nombre maximal de caractères ? (cf. noeud <maxCharacters>)
            if (unNoeud.SelectSingleNode("maxCharacters") != null)
                maTextBox.MaxLength = int.Parse(unNoeud.SelectSingleNode("maxCharacters").InnerText);

            // Calculer le nombre de lignes qui devront être affichées
            if (maTextBox.MaxLength > 0)
            {
                int numLines = (maTextBox.MaxLength / CARACTERES_PAR_LIGNE) + 1;

                // Calculer la largeur de la TextBox, et par conséquent s'il y a lieu d'avoir des barres de défilement
                if (numLines == 1)
                    maTextBox.Multiline = false;
                else
                {
                    if (numLines >= 4)
                    {
                        maTextBox.Multiline = true;
                        maTextBox.Height = 4 * HAUTEUR_PAR_LIGNE;
                        maTextBox.ScrollBars = ScrollBars.Vertical;
                    }
                    else
                    {
                        maTextBox.Multiline = true;
                        maTextBox.Height = numLines * HAUTEUR_PAR_LIGNE;
                        maTextBox.ScrollBars = ScrollBars.None;
                    }
                }
            }

            // Création d'un Label
            Label monLabel = new Label();
            monLabel.Name = maTextBox.Name + "Label";
            if (unNoeud.SelectSingleNode("text") != null)
                monLabel.Text = unNoeud.SelectSingleNode("text").InnerText;

            monLabel.Width = LARGEUR_CONTROLES;

            // Ajout à la collection
            monLabel.Location = unEmplacement;
            desControles.Add(monLabel);
            unEmplacement.Y += monLabel.Height;

            maTextBox.Location = unEmplacement;
            desControles.Add(maTextBox);
            unEmplacement.Y += maTextBox.Height + 10;

            return unEmplacement;
        }

        //----------------------------------------------------------------------
        // A COMPLETER
        // Affiche le résultat du questionnaire sous la forme d'un objet string.
        // Cette méthode sera exécutée à la fermeture du formulaire.
        //----------------------------------------------------------------------
        public void Afficher()
        {
            this.Réponse = "";

            // ...
            // ...

            MessageBox.Show(Réponse, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

    }
}
