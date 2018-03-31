using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediCare
{
    public partial class AjoutConsultation : Window
    {
        Traite TraitementEnreg = new Traite();
        List<string> radioList = new List<string>();
        List<Traite> traitementList = new List<Traite>();
        Medic med = new Medic();
        List<Medicaments> medicListTmp;
        public int i = 0;

        public AjoutConsultation()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            foreach (Medicaments medicament in Globals.ListMedicaments)
            {
                medicamentT.Items.Add(medicament.nom);
                i++;
                if (i > 100)
                {
                    i = 0;
                    break;
                }
            }
        }

        internal class Treatement
        {

            public int Traitement { get; set; }
            public string Dose { get; set; }
            public string Medicament { get; set; }
            public string Indication { get; set; }

        } 

        private void AjouterRadio_Click(object sender, RoutedEventArgs e)
        {
            radioList.Add(radioT.Text);
            radioT.Text = "";
        }



        private void AjouterTraitement_Click(object sender, RoutedEventArgs e)
        {
            i++;
            TraitementEnreg.Dose = doseT.Text;
            TraitementEnreg.NomMed = medicamentT.Text;
            TraitementEnreg.Indication = indicationT.Text;
            traitementList.Add(TraitementEnreg);
            var data = new Treatement { Traitement = i, Dose = doseT.Text, Indication = indicationT.Text,Medicament = medicamentT.Text };
            DataGridTrait.Items.Add(data);
            medicamentT.Items.Clear();
            doseT.Clear();
            indicationT.Clear();
        }

       



        private void Add_Consultation_Click(object sender, RoutedEventArgs e)
        {
            if (diagnosticT.Text == "" || descriptionT.Text == "" || medicamentT.Text == "" || doseT.Text == "")
            {
                Add_Consultation.Background = Brushes.Red;
                MessageBox.Show("Veuillez remplir toutes les informations!");
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
                    if (radioT.Text != "")
                    {
                        radioList.Add(radioT.Text);
                    }
                    if (doseT.Text != "" && medicamentT.Text != "" && indicationT.Text != "")
                    {
                        TraitementEnreg.Dose = doseT.Text;
                        TraitementEnreg.NomMed = medicamentT.Text;
                        TraitementEnreg.Indication = indicationT.Text;
                        traitementList.Add(TraitementEnreg);
                    }
                    consultation.AddConsult(Globals.NomPatient, Globals.PrenomPatient, Globals.NomMedecin, Globals.PrenomMedecin, diagnosticT.Text, descriptionT.Text, certificatT.Text, lettreT.Text, scannerT.Text, bilanT.Text, ordoT.Text, radioList, traitementList, labelT.Text);
                    MessageBox.Show("Consultation ajoutée avec succés !");
                }
                catch (Exception)
                {
                    MessageBox.Show("Une erreur s'est produite !");
                }



            }
        }

        
        private void medicamentT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            medicamentT.Focus();
            medicamentT.IsDropDownOpen = true;
        }

        private void medicamentT_TextChanged(object sender, TextChangedEventArgs e)
        {
            medicListTmp = med.RechercheMedicament(medicamentT.Text);
            medicamentT.Items.Clear();
            foreach (Medicaments medicament in medicListTmp)
            {
                medicamentT.Items.Add(medicamentT.Items.Add(medicament.nom));
                if (medicamentT.Items.Count != 0) medicamentT.Items.RemoveAt(medicamentT.Items.Count - 1);
            }
        }


        private void medicamentT_MouseEnter(object sender, MouseEventArgs e)
        {
            medicamentT.Focus();
            medicamentT.IsDropDownOpen = true;
        }


        private void medicamentT_GotFocus(object sender, RoutedEventArgs e)
        {
            medicamentT.IsDropDownOpen = true;
        }

        private void medicamentT_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && medicamentT.IsDropDownOpen && medicamentT.HasItems) medicamentT.Text = medicamentT.Items.GetItemAt(0).ToString();
        }
    }

}


