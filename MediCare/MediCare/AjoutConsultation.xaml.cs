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
    public partial class AjoutConsultation : UserControl
    {
        Traite TraitementEnreg = new Traite();
        List<Traite> traitementList = new List<Traite>();
        Medic med = new Medic();
        List<Medicaments> listMedicTmp;
        public int i = 0;

        public AjoutConsultation()
        {
            InitializeComponent();
            foreach (Medicaments medic in Globals.ListMedicaments)
            {
                medicamentT.Items.Add(medic.nom);
                i++;
                if(i>100)
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
            if (medicamentT.Text != "" && doseT.Text != "" && indicationT.Text != "")
            {
                TraitementEnreg.Dose = doseT.Text;
                TraitementEnreg.NomMed = medicamentT.Text;
                TraitementEnreg.Indication = indicationT.Text;
                traitementList.Add(TraitementEnreg);
            }

            if (ageT.Text == "" || diagnosticT.Text == "" || medicamentT.Text == "" || doseT.Text == "")
            {
                Add_Consultation.Background = Brushes.Red;
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (diagnosticT.Text == "") Diagnostic.Foreground = Brushes.Red; else Diagnostic.Foreground = Brushes.Black;
                if (medicamentT.Text == "") Medicament.Foreground = Brushes.Red; else Medicament.Foreground = Brushes.Black;
                if (doseT.Text == "") Dose.Foreground = Brushes.Red; else Dose.Foreground = Brushes.Black;
            }
            else
            {
                Globals.Age = int.Parse(ageT.Text);
                var parent = (Grid)this.Parent;
                UserControl ordo = new GenererOrdonnance(labelT.Text, diagnosticT.Text, descriptionT.Text, traitementList);
                parent.Children.Clear();
                parent.Children.Add(ordo);
            }
        }

        private void AjouterFichier_Click(object sender, RoutedEventArgs e)
        {
            Globals.Age = int.Parse(ageT.Text);
            var parent = (Grid)this.Parent;
            UserControl fich = new MenuFichier(labelT.Text, diagnosticT.Text,descriptionT.Text,traitementList);
            parent.Children.Clear();
            parent.Children.Add(fich);
        }

        private void medicamentT_TextChanged(object sender, TextChangedEventArgs e)
        {
            listMedicTmp = med.RechercheMedicament(medicamentT.Text);
            medicamentT.Items.Clear();
            foreach (Medicaments patient in listMedicTmp)
            {
                medicamentT.Items.Add(medicamentT.Items.Add(patient.nom));
                if (medicamentT.Items.Count != 0) medicamentT.Items.RemoveAt(medicamentT.Items.Count - 1);
            }
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


