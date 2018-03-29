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
    /// Logique d'interaction pour SppressionCons.xaml
    /// </summary>
    public partial class SppressionCons : Window
    {
        public SppressionCons()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        Agenda pat = new Agenda();
        List<Personne> patients;
        Regex charControl = new Regex(@"[A-Za-z]+");

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Nompatient.Text == "" || Prenompatient.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (Nompatient.Text == "") nomL.Foreground = Brushes.Red; else nomL.Foreground = Brushes.Black;
                if (Prenompatient.Text == "") prenomL.Foreground = Brushes.Red; else prenomL.Foreground = Brushes.Black;
            }
            else
            {

                Consult consultation = new Consult();
                try
                {
                    consultation.SuppConsultation(Nompatient.Text, Prenompatient.Text, DateTime.Parse(dateT.Text));
                    MessageBox.Show("la consultation a été supprimée");
                }
                catch (Exception)
                {
                    MessageBox.Show("Une erreur s'est produite !");
                }
            }
        }

        private void Nompatient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Nompatient.Text == "") patientsT.Text = "";
            else
            {
                if (Prenompatient.Text == "") patients = pat.RechercherPatientNom(Nompatient.Text);
                else patients = pat.RechercherPatient(Nompatient.Text+" "+Prenompatient.Text);
                patientsT.Text = "";
                foreach (Personne p in patients)
                {
                    patientsT.Text = patientsT.Text + "\n" + p.nom + " " + p.prenom;
                }
            }

        }

        private void Prenompatient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Prenompatient.Text == "") patientsT.Text = "";
            else
            {
                if (Nompatient.Text == "") patients = pat.RechercherPatientPrenom(Prenompatient.Text);
                else patients = pat.RechercherPatient(Nompatient.Text+" "+Prenompatient.Text);
                patientsT.Text = "";
                foreach (Personne p in patients)
                {
                    patientsT.Text = patientsT.Text + "\n" + p.nom + " " + p.prenom;
                }
            }
        }
        private void Prenompatient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void Nompatient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }
    }
}
