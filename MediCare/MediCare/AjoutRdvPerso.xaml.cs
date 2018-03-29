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
    /// Logique d'interaction pour AjoutRdvPerso.xaml
    /// </summary>
    public partial class AjoutRdvPerso : UserControl
    {
        public AjoutRdvPerso()
        {
            InitializeComponent();
           
        }
        
        Agenda rdv = new Agenda();

        private bool DateCheck(int realhour)
        {
            DateTime dateRdv = DateTime.Parse(dateT.Text + " " + realhour + ":" + minutesBox.Text + ":00");
            return (dateRdv < DateTime.Now) ? false : true;
        }

        private bool TimeCheck(int realHour)
        {
            DateTime dateRdv = DateTime.Parse(dateT.Text + " " + realHour + ":" + minutesBox.Text + ":00");
            return ((DateTime.Now > dateRdv && DateTime.Now.Hour > realHour) || (DateTime.Now > dateRdv && DateTime.Now.Hour == realHour && DateTime.Now.Minute > Int16.Parse(minutesBox.Text))) ? false : true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate = false,
               checkTime = false;
            int realHour = 0;
            if (dateT.Text == "" || heureBox.Text == "" || minutesBox.Text == "" || ampmBox.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (dateT.Text == "") dateL.Foreground = Brushes.Red; else dateL.Foreground = Brushes.Black;
                if (heureBox.Text == "") heuresL.Foreground = Brushes.Red; else heuresL.Foreground = Brushes.Black;
                if (minutesBox.Text == "") minutesL.Foreground = Brushes.Red; else minutesL.Foreground = Brushes.Black;
                if (ampmBox.Text == "") ampmL.Foreground = Brushes.Red; else ampmL.Foreground = Brushes.Black;
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
                        rdv.AddRdv(DateTime.Parse(dateT.Text + " " + realHour + ":" + minutesBox.Text + ":00"), Globals.IdMedecin, notesT.Text);
                        MessageBox.Show("Rendez-vous ajouté avec succés !");
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
                    dateL.Foreground = Brushes.Red;
                    minutesL.Foreground = Brushes.Red;
                    heuresL.Foreground = Brushes.Red;
                }
            }
        }

        private void dateT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }
    }
}
