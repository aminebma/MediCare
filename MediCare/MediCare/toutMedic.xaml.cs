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

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour toutMedic.xaml
    /// </summary>
    public partial class toutMedic : UserControl
    {
        int i = 0;
        Agenda personne = new Agenda();
        List<Medicaments> listMedicTmp;
        List<Medicaments> list;
        Medic med = new Medic();

        public toutMedic()
        { 
                InitializeComponent();
                foreach (Medicaments medic in Globals.ListMedicaments)
                {
                    if (medic.nom.Length > 25) medicament.Items.Add(medic.nom.Substring(0, 25));
                    else medicament.Items.Add(medic.nom);
                    i++;
                    if (i > 100)
                    {
                        i = 0;
                        break;
                    }
                }

        }

        private void medicamentT_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (list.Count() != 0)
            //{
            //    foreach (Medicaments p in list)
            //    {
            //        Expander expSuivi = new Expander();
            //        expSuivi.Header = "Nom du Médicament :" + p.nom;
            //        expSuivi.Content = " Dose : " + p.Dosage + "\n  :" + p.Type;
            //        StackSuivi.Children.Add(expSuivi);
            //    }
            //}
            listMedicTmp = med.RechercheMedicament(medicament.Text);
            medicament.Items.Clear();
            foreach (Medicaments medic in listMedicTmp)
            {
                if (medic.nom.Length > 25) medicament.Items.Add(medic.nom.Substring(0, 25));
                else medicament.Items.Add(medic.nom);
                //if (medicamentT.Items.Count != 0) medicamentT.Items.RemoveAt(medicamentT.Items.Count - 1);
            }
        }

        private void medicament_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //listMedicTmp = med.RechercheMedicament(medicament.Text);
            //medicament.Items.Clear();
            //foreach (Medicaments medic in listMedicTmp)
            //{
            //    if (medic.nom.Length > 25) medicament.Items.Add(medic.nom.Substring(0, 25));
            //    else medicament.Items.Add(medic.nom);
            //    //if (medicamentT.Items.Count != 0) medicamentT.Items.RemoveAt(medicamentT.Items.Count - 1);
            //}
            //if (list.Count() != 0)
            //{
            //    foreach (Medicaments p in list)
            //    {
            //        Expander expSuivi = new Expander();
            //        expSuivi.Header = "Nom du Médicament :" + p.nom;
            //        expSuivi.Content = " Dose : " + p.Dosage + "\n  :" + p.Type;
            //        StackSuivi.Children.Add(expSuivi);
            //    }
            //}
            StackSuivi.Children.Clear();
            foreach(Medicaments medic in listMedicTmp)
            {
                Expander expSuivi = new Expander();
                expSuivi.Header = "Nom du Médicament :" + medic.nom;
                expSuivi.Content = " Dose : " + medic.Dosage + "\n  :" + medic.Type;
                StackSuivi.Children.Add(expSuivi);
            }
        }

        private void medicament_PreviewKeyDown(object sender, RoutedEventArgs e)
        {
                medicament.IsDropDownOpen = true;
        }

        private void medicament_GotFocus(object sender, KeyEventArgs e)
        {
                if (e.Key == Key.Tab && medicament.IsDropDownOpen && medicament.HasItems) medicament.Text = medicament.Items.GetItemAt(0).ToString();
            
        }
    }
}
