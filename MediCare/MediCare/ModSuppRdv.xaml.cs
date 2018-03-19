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
    public partial class ModSuppRdv : Window
    {
        public ModSuppRdv()
        {
            InitializeComponent();
        }

        Agenda rdv = new Agenda();
        Regex charControl = new Regex(@"[A-Za-z]+");

        private bool DateCheck(int realhour)
        {
            DateTime dateRdv = DateTime.Parse(NewDateT.Text + " " + realhour + ":" + minutesBox.Text + ":00");
            return (dateRdv < DateTime.Now) ? false : true;
        }

        private bool TimeCheck(int realHour)
        {
            DateTime dateRdv = DateTime.Parse(NewDateT.Text + " " + realHour + ":" + minutesBox.Text + ":00");
            return ((DateTime.Now > dateRdv && DateTime.Now.Hour > realHour) || (DateTime.Now > dateRdv && DateTime.Now.Hour == realHour && DateTime.Now.Minute > Int16.Parse(minutesBox.Text))) ? false : true;
        }

        private void dateT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }


        private void SuppButton_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate = false,
               checkTime = false;
            int realHour = 0;
            if (NewDateT.Text == "" || OldDateT.Text == "" || heureBox.Text == "" || minutesBox.Text == "" || ampmBox.Text == "" || nomPatientT.Text == "" || prenomPatientT.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (NewDateT.Text == "") NewDateT.Foreground = Brushes.Red; else NewDateT.Foreground = Brushes.Black;
                if (OldDateT.Text == "") OldDateT.Foreground = Brushes.Red; else OldDateT.Foreground = Brushes.Black;
                if (heureBox.Text == "") heuresL.Foreground = Brushes.Red; else heuresL.Foreground = Brushes.Black;
                if (minutesBox.Text == "") minutesL.Foreground = Brushes.Red; else minutesL.Foreground = Brushes.Black;
                if (ampmBox.Text == "") ampmL.Foreground = Brushes.Red; else ampmL.Foreground = Brushes.Black;
                if (prenomPatientT.Text == "") prenomPatientL.Foreground = Brushes.Red; else prenomPatientL.Foreground = Brushes.Black;
                if (nomPatientT.Text == "") nomPatientL.Foreground = Brushes.Red; else nomPatientL.Foreground = Brushes.Black;
            }
            else
            {
                realHour = (ampmBox.Text == "AM") ? Int32.Parse(heureBox.Text) : (Int32.Parse(heureBox.Text) + 12);
                checkDate = DateCheck(realHour);
                checkTime = TimeCheck(realHour);
                if (checkDate && checkTime)
                {
                    minutesL.Foreground = Brushes.Black;
                    try
                    {
                        rdv.SuppRdv(DateTime.Parse(OldDateT.Text + " " + realHour + ":" + minutesBox.Text + ":00"));
                        MessageBox.Show("Rendez-vous supprimé avec succés !");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Database error");
                    }
                }
                else if (!checkTime)
                {
                    MessageBox.Show("Horaire invalide");
                    heuresL.Foreground = Brushes.Red;
                    minutesL.Foreground = Brushes.Red;
                }
                else if (!checkDate)
                {
                    MessageBox.Show("Veuillez rentrer une date valide");
                    OldDateL.Foreground = Brushes.Red;
                    minutesL.Foreground = Brushes.Red;
                    heuresL.Foreground = Brushes.Red;
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

        private void ModifBtn_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate = false,
               checkTime = false;
            int realHour = 0;
            if (NewDateT.Text == "" || OldDateT.Text == "" || heureBox.Text == "" || minutesBox.Text == "" || ampmBox.Text == "" || nomPatientT.Text == "" || prenomPatientT.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (NewDateT.Text == "") NewDateT.Foreground = Brushes.Red; else NewDateT.Foreground = Brushes.Black;
                if (OldDateT.Text == "") OldDateT.Foreground = Brushes.Red; else OldDateT.Foreground = Brushes.Black;
                if (heureBox.Text == "") heuresL.Foreground = Brushes.Red; else heuresL.Foreground = Brushes.Black;
                if (minutesBox.Text == "") minutesL.Foreground = Brushes.Red; else minutesL.Foreground = Brushes.Black;
                if (ampmBox.Text == "") ampmL.Foreground = Brushes.Red; else ampmL.Foreground = Brushes.Black;
                if (prenomPatientT.Text == "") prenomPatientL.Foreground = Brushes.Red; else prenomPatientL.Foreground = Brushes.Black;
                if (nomPatientT.Text == "") nomPatientL.Foreground = Brushes.Red; else nomPatientL.Foreground = Brushes.Black;
            }
            else
            {
                realHour = (ampmBox.Text == "AM") ? Int32.Parse(heureBox.Text) : (Int32.Parse(heureBox.Text) + 12);
                checkDate = DateCheck(realHour);
                checkTime = TimeCheck(realHour);
                if (checkDate && checkTime)
                {
                    minutesL.Foreground = Brushes.Black;
                    try
                    {
                        rdv.ModifRdv(nomPatientT.Text,prenomPatientT.Text,DateTime.Parse(OldDateT.Text + " " + realHour + ":" + minutesBox.Text + ":00"));
                        MessageBox.Show("Rendez-vous modifié avec succés !");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Database error");
                    }
                }
                else if (!checkTime)
                {
                    MessageBox.Show("Horaire invalide");
                    heuresL.Foreground = Brushes.Red;
                    minutesL.Foreground = Brushes.Red;
                }
                else if (!checkDate)
                {
                    MessageBox.Show("Veuillez rentrer une date valide");
                    OldDateL.Foreground = Brushes.Red;
                    minutesL.Foreground = Brushes.Red;
                    heuresL.Foreground = Brushes.Red;
                }
            }
        }
    }
}
