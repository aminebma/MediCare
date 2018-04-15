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
    /// Logique d'interaction pour SUIVIS.xaml
    /// </summary>
    public partial class SUIVIS : UserControl
    {
        public SUIVIS()
        {
            InitializeComponent();
        }

        List<Personne> listPatientsTmp;
        Agenda pat = new Agenda();
        List<ConsultLabel> list;
        Consult consultation = new Consult();
        Consulta consulta = new Consulta();

        private void suivi_Click(object sender, RoutedEventArgs e)
        {
            if (nomPatientT.Text != "")
            {
                list = consultation.Suivie(nomPatientT.Text, prenomPatientT.Text);
                if (list.Count() != 0)
                {
                    foreach (ConsultLabel p in list)
                    {
                        Expander expSuivi = new Expander();
                        expSuivi.Header ="Label :" + p.Label + "\n Date de la consultation :" + p.Date.Day + "/" + p.Date.Month + "/" + p.Date.Year;
                        StackSuivi.Children.Add(expSuivi);
                        consulta = consultation.AcceeConsultationId(p.Id);
                        expSuivi.Content = "Diagnostic : " + consulta.Diagnostic + "\n Description :" + consulta.Description;
                        if (consulta.traitement != null)
                        {
                            expSuivi.Content = expSuivi.Content + "\n Traitement :";
                            foreach (Traite d in consulta.traitement)
                            {
                                expSuivi.Content = expSuivi.Content + "\n" + d.NomMed + " " + d.Dose + " " + d.Indication;

                            }
                        }


                    }

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
 
               