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
                if (medic.nom.Length > 25) medicamentT.Items.Add(medic.nom.Substring(0, 25));
                else medicamentT.Items.Add(medic.nom);
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

            if (diagnosticT.Text == "" || (medicamentT.Text == "" && traitementList.Count==0) || (doseT.Text == "" && traitementList.Count==0))
            {
                if (diagnosticT.Text == "") diagnosticT.BorderBrush = Brushes.Red; else diagnosticT.BorderBrush = Brushes.Black;
                if (medicamentT.Text == "" && traitementList.Count == 0) medicamentT.BorderBrush = Brushes.Red; else medicamentT.BorderBrush = Brushes.Black;
                if (doseT.Text == "" && traitementList.Count == 0) doseT.BorderBrush = Brushes.Red; else doseT.BorderBrush = Brushes.Black;
                MessageBox.Show("Veuillez remplir toutes les informations!");
            }
            else
            {
                var parent = (Grid)this.Parent;
                UserControl ordo = new GenererOrdonnance(labelT.Text, diagnosticT.Text, descriptionT.Text, traitementList);
                parent.Children.Clear();
                parent.Children.Add(ordo);
            }
        }

        private void AjouterFichier_Click(object sender, RoutedEventArgs e)
        {
            if (medicamentT.Text != "" && doseT.Text != "" && indicationT.Text != "")
            {
                TraitementEnreg.Dose = doseT.Text;
                TraitementEnreg.NomMed = medicamentT.Text;
                TraitementEnreg.Indication = indicationT.Text;
                traitementList.Add(TraitementEnreg);
            }

            var parent = (Grid)this.Parent;
            UserControl fich = new MenuFichier(labelT.Text, diagnosticT.Text,descriptionT.Text,traitementList);
            parent.Children.Clear();
            parent.Children.Add(fich);
        }

        private void medicamentT_TextChanged(object sender, TextChangedEventArgs e)
        {
            listMedicTmp = med.RechercheMedicament(medicamentT.Text);
            medicamentT.Items.Clear();
            foreach (Medicaments medic in listMedicTmp)
            {
                if (medic.nom.Length > 25) medicamentT.Items.Add(medic.nom.Substring(0, 25));
                else medicamentT.Items.Add(medic.nom);
                //if (medicamentT.Items.Count != 0) medicamentT.Items.RemoveAt(medicamentT.Items.Count - 1);
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


