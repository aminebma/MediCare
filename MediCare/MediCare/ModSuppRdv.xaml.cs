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
using System.Text.RegularExpressions;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour ModSuppRdv.xaml
    /// </summary>
    public partial class ModSuppRdv : UserControl
    {
        List<Personne> listPatientsTmp;
        public ModSuppRdv()
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

        Agenda rdv = new Agenda();
        Regex charControl = new Regex(@"[A-Za-z]+");

        private bool DateCheck(DateTime dateRdv)
        {
            return (dateRdv < DateTime.Now) ? false : true;
        }

        private void dateT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }


        private void SuppButton_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate1 = false, checkDate2 = false;

            if (NewDateT.Text == "" || OldDateT.Text == "" || horaire.Text == "" || nomPatientT.Text == "" || prenomPatientT.Text == "")
            {
                MessageBox.Show("Veuillez remplir le minimum d'informations!");
                if (NewDateT.Text == "") NewDateT.Foreground = Brushes.Red; else NewDateT.Foreground = Brushes.Black;
                if (OldDateT.Text == "") OldDateT.Foreground = Brushes.Red; else OldDateT.Foreground = Brushes.Black;
                if (horaire.Text == "") horaire.Foreground = Brushes.Red; else horaire.Foreground = Brushes.Black;
                if (prenomPatientT.Text == "") prenomPatientT.BorderBrush = Brushes.Red; else prenomPatientT.BorderBrush = Brushes.Black;
                if (nomPatientT.Text == "") nomPatientT.BorderBrush = Brushes.Red; else nomPatientT.BorderBrush = Brushes.Black;
            }
            else
            {
                checkDate1 = DateCheck(DateTime.Parse(OldDateT.Text + " " + horaire.Text));
                checkDate2 = DateCheck(DateTime.Parse(NewDateT.Text + " " + horaire.Text));

                if (checkDate1 && checkDate2)
                {
                    try
                    {
                        rdv.SuppRdv(DateTime.Parse(OldDateT.Text + " " + horaire.Text));
                        MessageBox.Show("Rendez-vous supprimé avec succés !");
                        var parent = (Grid)this.Parent;
                        parent.Children.Clear();
                        var parent2 = (Grid)parent.Parent;
                        parent2.Children.Add(new AgendaMenu());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Database error");
                    }
                }
                else
                { 
                    MessageBox.Show("Veuillez rentrer une date valide");
                    if (!checkDate1 && checkDate2) OldDateT.BorderBrush = Brushes.Red;
                    else if (!checkDate1 && !checkDate2) NewDateT.BorderBrush = Brushes.Red;
                    else
                    {
                        OldDateT.BorderBrush = Brushes.Red;
                        NewDateT.BorderBrush = Brushes.Red;
                    }
                }
            }
        }

        private void nomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void prenomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
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

        private void prenomPatientT_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && prenomPatientT.IsDropDownOpen && prenomPatientT.HasItems) prenomPatientT.Text = prenomPatientT.Items.GetItemAt(0).ToString();
        }

        private void horaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            if (nomPatientT.Text == "") listPatientsTmp = rdv.RechercherPatientPrenom(prenomPatientT.Text);
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

        private void ModifBtn_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate1 = false, checkDate2 = false;

            if (NewDateT.Text == "" || OldDateT.Text == "" || horaire.Text == "" || nomPatientT.Text == "" || prenomPatientT.Text == "")
            {
                MessageBox.Show("Veuillez remplir le minimum d'informations!");
                if (NewDateT.Text == "") NewDateT.Foreground = Brushes.Red; else NewDateT.Foreground = Brushes.Black;
                if (OldDateT.Text == "") OldDateT.Foreground = Brushes.Red; else OldDateT.Foreground = Brushes.Black;
                if (horaire.Text == "") horaire.Foreground = Brushes.Red; else horaire.Foreground = Brushes.Black;
                if (prenomPatientT.Text == "") prenomPatientT.BorderBrush = Brushes.Red; else prenomPatientT.BorderBrush = Brushes.Black;
                if (nomPatientT.Text == "") nomPatientT.BorderBrush = Brushes.Red; else nomPatientT.BorderBrush = Brushes.Black;
            }
            else
            {
                checkDate1 = DateCheck(DateTime.Parse(OldDateT.Text + " " + horaire.Text));
                checkDate2 = DateCheck(DateTime.Parse(NewDateT.Text + " " + horaire.Text));

                if (checkDate1 && checkDate2)
                {
                    try
                    {
                        rdv.ModifRdv(nomPatientT.Text, prenomPatientT.Text, DateTime.Parse(OldDateT.Text + " " + horaire.Text));
                        MessageBox.Show("Rendez-vous modifié avec succés !");
                        var parent = (Grid)this.Parent;
                        parent.Children.Clear();
                        var parent2 = (Grid)parent.Parent;
                        parent2.Children.Add(new AgendaMenu());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Database error");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez rentrer une date valide");
                    if (!checkDate1 && checkDate2) OldDateT.BorderBrush = Brushes.Red;
                    else if (!checkDate1 && !checkDate2) NewDateT.BorderBrush = Brushes.Red;
                    else
                    {
                        OldDateT.BorderBrush = Brushes.Red;
                        NewDateT.BorderBrush = Brushes.Red;
                    }
                }
            }
        }
    }
}
