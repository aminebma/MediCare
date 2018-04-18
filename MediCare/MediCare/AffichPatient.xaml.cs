using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour AffichPatient.xaml
    /// </summary>
    public partial class AffichPatient : UserControl
    {
        List<Personne> listPatientsTmp;
        Regex charControl = new Regex(@"[A-Za-z]+");
        Agenda pat = new Agenda();
        specRadio Checked = new specRadio();
        List<specRadio> listComplete = new List<specRadio>();

        PersonneClasse personne = new PersonneClasse();
        Medecin2 patients = new Medecin2();
        List<Patients> list;

        public AffichPatient()
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

            list = patients.DossiersMedical();

            if (list.Count() != 0)
            {
                foreach (Patients p in list)
                {
                    specRadio expSuivi = new specRadio();
                    expSuivi.Content = p.Nom + " " + p.Prenom;
                    expSuivi.nom = p.Nom;
                    expSuivi.prenom = p.Prenom;
                    listComplete.Add(expSuivi);
                    StackSuivi.Items.Add(expSuivi);
                }


            }
        }
        PersonneClasse pers = new PersonneClasse();
        private void search_Click(object sender, RoutedEventArgs e)
        {
            foreach (specRadio p in listComplete)
            {
                if (p.IsChecked.Value == true)
                {
                    Checked = p;
                }
            }

            if ((nomPatientT.Text == "" || prenomPatientT.Text == "") && (Checked.IsChecked == false ))
            {
                MessageBox.Show("Veuillez saisir toutes les informations ou choisir un patient !");
                if (nomPatientT.Text == "") nomPatientT.BorderBrush = Brushes.Red; else nomPatientT.BorderBrush = Brushes.Black;
                if (prenomPatientT.Text == "") prenomPatientT.BorderBrush = Brushes.Red; else prenomPatientT.BorderBrush = Brushes.Black;
            }
            else
            {
                if (Checked.IsChecked != false)
                {

                    Globals.NomPatient = Checked.nom;
                    Globals.PrenomPatient = Checked.prenom;
                    nomPatientT.Text = Globals.NomPatient;
                    prenomPatientT.Text = Globals.PrenomPatient;
                    listPatientsTmp = pat.RechercherPatient(Globals.NomPatient + " " + Globals.PrenomPatient);
                    if (listPatientsTmp.Count() != 0)
                    {
                        DateTime date = (DateTime)listPatientsTmp[0].dateNaissance;
                        Globals.Age = DateTime.Today.Year - date.Year;
                        nomPatientT.IsEnabled = false;
                        prenomPatientT.IsEnabled = false;
                        search.IsEnabled = false;
                        var parent = (Grid)this.Parent;
                        parent.Children.Clear();
                        parent.Children.Add(new MenuPatient());

                    }
                    else
                    {
                        nomPatientT.IsEnabled = false;
                        prenomPatientT.IsEnabled = false;
                        search.IsEnabled = false;
                        PopupPatient pop = new PopupPatient();
                        pop.Show();
                    }
                }
                else
                {
                    if (Checked.IsChecked == false && (nomPatientT.Text != "" && prenomPatientT.Text != ""))
                    {
                        Globals.NomPatient = nomPatientT.Text;
                        Globals.PrenomPatient = prenomPatientT.Text;
                        listPatientsTmp = pat.RechercherPatient(Globals.NomPatient + " " + Globals.PrenomPatient);
                        if (listPatientsTmp.Count() != 0)
                        {
                            DateTime date = (DateTime)listPatientsTmp[0].dateNaissance;
                            Globals.Age = DateTime.Today.Year - date.Year;
                            nomPatientT.IsEnabled = false;
                            prenomPatientT.IsEnabled = false;
                            search.IsEnabled = false;
                            var parent = (Grid)this.Parent;
                            parent.Children.Clear();
                            parent.Children.Add(new MenuPatient());

                        }
                        else
                        {
                            nomPatientT.IsEnabled = false;
                            prenomPatientT.IsEnabled = false;
                            search.IsEnabled = false;
                            PopupPatient pop = new PopupPatient();
                            pop.Show();
                        }
                    }
                }
            }

        }
        




        public class specRadio : RadioButton
        {
            public string nom { get; set; }
            public string prenom { get; set; }
        }
        private void nomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void prenomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void nomPatientT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (prenomPatientT.Text == "") listPatientsTmp = pat.RechercherPatientNom(nomPatientT.Text);
            else listPatientsTmp = pat.RechercherPatient(nomPatientT.Text + " " + prenomPatientT.Text);
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
            if (nomPatientT.Text == "") listPatientsTmp = pat.RechercherPatientPrenom(prenomPatientT.Text);
            else listPatientsTmp = pat.RechercherPatient(nomPatientT.Text + " " + prenomPatientT.Text);
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


        private void nomPatientT_MouseEnter(object sender, MouseEventArgs e)
        {
            nomPatientT.Focus();
            nomPatientT.IsDropDownOpen = true;
        }

        private void prenomPatientT_MouseEnter(object sender, MouseEventArgs e)
        {
            prenomPatientT.Focus();
            prenomPatientT.IsDropDownOpen = true;
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
