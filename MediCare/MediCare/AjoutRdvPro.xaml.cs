using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour AjoutRdvPro.xaml
    /// </summary>
    public partial class AjoutRdvPro : UserControl
    {

        List<Personne> listPatientsTmp;

        public AjoutRdvPro()
        {
            InitializeComponent();
            int nbElemMax = 0;
            foreach (Personne patient in Globals.ListPatients)
            {
                nomPatientT.Items.Add(patient.nom);
                prenomPatientT.Items.Add(patient.prenom);
                nbElemMax++;
                if (nbElemMax > 100) break;
            }
        }

        //List<string> caract = new List<string> { ",", ".", ":", ";", "!", "*", "$", "/", "?", "+", "_", "=", "§", "<", ">", "{", "}", "[", "]", "(", ")", "'", "\"", "&", "²", "@", "|", "#", "£", "µ", "%", "€", "¤" };
        Agenda rdv = new Agenda();
        Regex charControl = new Regex(@"[A-Za-z]+");

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate = false;
            if (dateT.Text == "" || horaire.Text == "" || prenomPatientT.Text == "" || nomPatientT.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (dateT.Text == "") dateT.BorderBrush = Brushes.Red; else dateT.BorderBrush = Brushes.Black;
                if (horaire.Text == null) horaire.BorderBrush = Brushes.Red; else horaire.BorderBrush = Brushes.Black;
                if (prenomPatientT.Text == "") prenomPatientT.BorderBrush = Brushes.Red; else prenomPatientT.BorderBrush = Brushes.Black;
                if (nomPatientT.Text == "") nomPatientT.BorderBrush = Brushes.Red; else nomPatientT.BorderBrush = Brushes.Black;
            }
            else
            {
                checkDate = DateCheck();
                if (checkDate)
                {
                    try
                    {

                        if (rdv.AddRdv(DateTime.Parse(dateT.Text + " " + horaire.Text), Globals.IdMedecin, nomPatientT.Text, prenomPatientT.Text, (bool)isImportant.IsChecked, notesT.Text))
                        {
                            MessageBox.Show("Rendez-vous ajouté avec succés !");
                            var parent = (Grid)this.Parent;
                            parent.Children.Clear();
                            var parent2 = (Grid)parent.Parent;
                            parent2.Children.Add(new AgendaMenu());
                        }
                        else MessageBox.Show("Vous avez déjà un rendez-vous à cette date");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Database error");
                    }
                }   
                else
                {
                    MessageBox.Show("Veuillez rentrer une date valide");
                    dateT.BorderBrush = Brushes.Red;
                    horaire.BorderBrush = Brushes.Red;
                } 
            }
        }

        private void nomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //  if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void prenomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;            
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }
        
        //private bool NumberCheck(string verifText)
        //{
        //    return int.TryParse(verifText, out int result);
        //}

        private bool DateCheck()
        {
            DateTime dateRdv = DateTime.Parse(dateT.Text + " " + horaire.Text);
            return (dateRdv < DateTime.Now) ? false : true;
        }

        private void dateT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void nomPatientT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (prenomPatientT.Text == "") listPatientsTmp = rdv.RechercherPatientNom(nomPatientT.Text);
            else listPatientsTmp = rdv.RechercherPatient(nomPatientT.Text + " " + prenomPatientT.Text);
            prenomPatientT.Items.Clear();
            nomPatientT.Items.Clear();
            foreach (Personne patient in listPatientsTmp)
            {
                prenomPatientT.Items.Add(prenomPatientT.Items.Add(patient.prenom));
                nomPatientT.Items.Add(nomPatientT.Items.Add(patient.nom));
                if (prenomPatientT.Items.Count != 0) prenomPatientT.Items.RemoveAt(prenomPatientT.Items.Count - 1);
                if (nomPatientT.Items.Count != 0) nomPatientT.Items.RemoveAt(nomPatientT.Items.Count - 1);
            }
        }

        private void prenomPatientT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(nomPatientT.Text == "") listPatientsTmp = rdv.RechercherPatientPrenom(prenomPatientT.Text);
            else listPatientsTmp = rdv.RechercherPatient(nomPatientT.Text+" "+prenomPatientT.Text);
            prenomPatientT.Items.Clear();
            nomPatientT.Items.Clear();
            foreach (Personne patient in listPatientsTmp)
            {
                prenomPatientT.Items.Add(prenomPatientT.Items.Add(patient.prenom));
                nomPatientT.Items.Add(nomPatientT.Items.Add(patient.nom));
                if (prenomPatientT.Items.Count != 0) prenomPatientT.Items.RemoveAt(prenomPatientT.Items.Count - 1);
                if (nomPatientT.Items.Count != 0) nomPatientT.Items.RemoveAt(nomPatientT.Items.Count - 1);
            }
        }

        private void horaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void prenomPatientT_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && prenomPatientT.IsDropDownOpen && prenomPatientT.HasItems) prenomPatientT.Text = prenomPatientT.Items.GetItemAt(0).ToString();
        }

        private void prenomPatientT_GotFocus(object sender, RoutedEventArgs e)
        {
            prenomPatientT.IsDropDownOpen = true;
        }

        private void nomPatientT_GotFocus(object sender, RoutedEventArgs e)
        {
            nomPatientT.IsDropDownOpen = true;
        }

        private void nomPatientT_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && nomPatientT.IsDropDownOpen && nomPatientT.HasItems) nomPatientT.Text = nomPatientT.Items.GetItemAt(0).ToString();
        }
    }
}
