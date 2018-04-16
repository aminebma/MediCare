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
    /// Logique d'interaction pour Consultation_Patient.xaml
    /// </summary>
    public partial class Consultation_Patient : UserControl
    {

        List<Personne> listPatientsTmp;
        public Consultation_Patient()
        {
            InitializeComponent();
            foreach (Personne patient in Globals.ListPatients)
            {
                nomPatientT.Items.Add(patient.nom);
                prenomPatientT.Items.Add(patient.prenom);
            }
        }

        Agenda pat = new Agenda();
        List<Personne> patients;
        Regex charControl = new Regex(@"[A-Za-z]+");

        

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nomPatientT.Text == "" || prenomPatientT.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (nomPatientT.Text == "") nomPatientT.BorderBrush = Brushes.Red; else nomPatientT.BorderBrush = Brushes.Black;
                if (prenomPatientT.Text == "") prenomPatientT.BorderBrush = Brushes.Red; else prenomPatientT.BorderBrush = Brushes.Black;
            }
            else
            {
                Globals.NomPatient = nomPatientT.Text;
                Globals.PrenomPatient = prenomPatientT.Text;
                patients = pat.RechercherPatient(Globals.NomPatient+" "+Globals.PrenomPatient);
                if (patients.Count() != 0)
                {
                    //(DateTime)patients[0].dateNaissance;
                    DateTime date = (DateTime) patients[0].dateNaissance;
                    Globals.Age = DateTime.Today.Year - date.Year;
                    nomPatientT.IsEnabled = false;
                    prenomPatientT.IsEnabled = false;
                    okBtn.IsEnabled = false;
                    this.GridDroit.Children.Add(new AjoutConsultation());
                    
                }
                else
                {
                    nomPatientT.IsEnabled = false;
                    prenomPatientT.IsEnabled = false;
                    okBtn.IsEnabled = false;
                    PopupPatient pop = new PopupPatient();
                    pop.SetGridAppelant =this.GridDroit;
                    pop.SetTotal = GridTotal;
                    pop.Show();
                }
            }

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

       

        private void Nompatient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }
        private void Prenompatient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
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

        private void nomPatientT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            nomPatientT.Focus();
            nomPatientT.IsDropDownOpen = true;
        }

        private void prenomPatientT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            prenomPatientT.Focus();
            prenomPatientT.IsDropDownOpen = true;
        }
    }
}

