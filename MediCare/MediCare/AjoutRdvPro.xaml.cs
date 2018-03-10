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
    /// Logique d'interaction pour AjoutRdvPro.xaml
    /// </summary>
    public partial class AjoutRdvPro : Window
    {
        public AjoutRdvPro()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        List<string> caract = new List<string> { ",", ".", ":", ";", "!", "*", "$", "/", "?", "+", "_", "=", "§", "<", ">", "{", "}", "[", "]", "(", ")", "'", "\"", "&", "²", "@", "|", "#", "£", "µ", "%", "€", "¤" };
        Agenda rdv = new Agenda();

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate = false;
            // checkTime = false;
            int realHour = 0;
            if (dateT.Text == "" || heureBox.Text == "" || minutesBox.Text == "" || ampmBox.Text == "" || prenomPatientT.Text == "" || nomPatientT.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (dateT.Text == "") dateL.Foreground = Brushes.Red; else dateL.Foreground = Brushes.Black;
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
                //checkTime = TimeCheck(realHour);
                if (checkDate /*&& checkTime*/)
                {
                    minutesL.Foreground = Brushes.Black;
                    try
                    {
                        rdv.AddRdv(DateTime.Parse(dateT.Text + " " + realHour + ":" + minutesBox.Text + ":00"), 1, nomPatientT.Text.ToUpper(), prenomPatientT.Text.ToUpper(), (bool)isImportant.IsChecked, notesT.Text);
                        MessageBox.Show("Rendez-vous ajouté avec succés !");
                    }
                    catch (Exception)
                    {
                    MessageBox.Show("Database error");
                    }
            }
                else if (!checkDate)
                {
                    MessageBox.Show("Veuillez rentrer une date/heure valide");
                    dateL.Foreground = Brushes.Red;
                    minutesL.Foreground = Brushes.Red;
                    heuresL.Foreground = Brushes.Red;
                }
                //else
                //{
                //
                //    if(Int16.Parse(minutesBox.Text)>59)
                //    {
                //        MessageBox.Show("Minutes invalide");
                //        minutesL.Foreground = Brushes.Red;
                //    }
                //    else
                //    {
                //        MessageBox.Show("Heure invalide");
                //        heuresL.Foreground = Brushes.Red;
                //    }
                //}
            }
        }

        private void nomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
        }

        private void prenomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
        }

        private bool NumberCheck(string verifText)
        {
            return int.TryParse(verifText, out int result);
        }

        private bool DateCheck(int realhour)
        {
            DateTime dateRdv = DateTime.Parse(dateT.Text + " " + realhour + ":" + minutesBox.Text + ":00");
            return (dateRdv < DateTime.Now) ? false : true;
        }

        //private bool TimeCheck(int realHour)
        //{
        //    return (DateTime.Now.Hour > realHour || (DateTime.Now.Hour == realHour && DateTime.Now.Minute > Int16.Parse(minutesBox.Text))) ? false : true;
        //}

        private void dateT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }
    }
}
