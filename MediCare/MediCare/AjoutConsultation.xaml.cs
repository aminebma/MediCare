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
using System.Windows.Shapes;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour Consultation.xaml
    /// </summary>
    public partial class AjoutConsultation : Window
    {
        Traite TraitementEnreg = new Traite();
        List<string> radioList = new List<string>();
        List<Traite> traitementList = new List<Traite>();
        Consultation_Patient CP = new Consultation_Patient();

        public AjoutConsultation()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            /*Medic medicament = new Medic();
            foreach (string p in medicament.medoc)
            {
                medicamentCB.Items.Add(p);
            }*/

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            radioList.Add(radioT.Text);
            radioT.Text = "";
        }

        private void AjouterTraitement_Click(object sender, RoutedEventArgs e)
        {
            TraitementEnreg.Dose = doseT.Text;
            TraitementEnreg.NomMed = medicamentT.Text;
            TraitementEnreg.Indication = indicationT.Text;
            traitementList.Add(TraitementEnreg);
            doseT.Text = "";
            indicationT.Text = "";
            medicamentT.Text = "";

        }




        private void Add_Consultation_Click(object sender, RoutedEventArgs e)
        {
            if (nomMedecinT.Text == "" || prenomMedecinT.Text == "" || nomPatientT.Text == "" || prenomPatientT.Text == "" || diagnosticT.Text == "" || descriptionT.Text == "" || medicamentT.Text == "" || doseT.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (nomMedecinT.Text == "") NomMedecin.Foreground = Brushes.Red; else NomMedecin.Foreground = Brushes.Black;
                if (nomPatientT.Text == "") NomPatient.Foreground = Brushes.Red; else NomPatient.Foreground = Brushes.Black;
                if (prenomMedecinT.Text == "") PrenomMedecin.Foreground = Brushes.Red; else PrenomMedecin.Foreground = Brushes.Black;
                if (prenomPatientT.Text == "") PrenomPatient.Foreground = Brushes.Red; else PrenomPatient.Foreground = Brushes.Black;
                if (diagnosticT.Text == "") Diagnostic.Foreground = Brushes.Red; else Diagnostic.Foreground = Brushes.Black;
                if (descriptionT.Text == "") Description.Foreground = Brushes.Red; else Description.Foreground = Brushes.Black;
                if (medicamentT.Text == "") Medicament.Foreground = Brushes.Red; else Medicament.Foreground = Brushes.Black;
                if (doseT.Text == "") Dose.Foreground = Brushes.Red; else Dose.Foreground = Brushes.Black;
            }
            else
            {

                Consult consultation = new Consult();
                try
                {
                    radioList.Add(radioT.Text);
                    TraitementEnreg.Dose = doseT.Text;
                    TraitementEnreg.NomMed = medicamentT.Text;
                    TraitementEnreg.Indication = indicationT.Text;
                    traitementList.Add(TraitementEnreg);
                    consultation.AddConsult(nomPatientT.Text, prenomPatientT.Text, nomMedecinT.Text, prenomMedecinT.Text, diagnosticT.Text, descriptionT.Text, certificatT.Text, lettreT.Text, scannerT.Text, bilanT.Text, ordoT.Text, radioList, traitementList,labelT.Text);
                    MessageBox.Show("Rendez-vous ajouté avec succés !");
                }
                catch (Exception)
                {
                    MessageBox.Show("Une erreur s'est produite !");
                }



            }
        }


        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            Consult consultation = new Consult();
            try
            {
                consultation.SuppConsultation(nomPatientT.Text, prenomPatientT.Text, DateTime.Parse(dateT.Text));
                MessageBox.Show("la consultation a été supprimée");
            }
            catch
            {
                MessageBox.Show("Une erreur s'est produite !");
            }
        }

        //private void medicamentCB_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}