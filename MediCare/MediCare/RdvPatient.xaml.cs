using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour RdvPatient.xaml
    /// </summary>
    public partial class RdvPatient : UserControl
    {
        
        public RdvPatient()
        {
            InitializeComponent();
            
        }

        //List<string> caract = new List<string> { ",", ".", ":", ";", "!", "*", "$", "/", "?", "+", "_", "=", "§", "<", ">", "{", "}", "[", "]", "(", ")", "'", "\"", "&", "²", "@", "|", "#", "£", "µ", "%", "€", "¤" };
        Agenda rdv = new Agenda();
        Regex charControl = new Regex(@"[A-Za-z]+");

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate = false;
            if (dateT.Text == "" || horaire.Text == "" )
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (dateT.Text == "") dateT.BorderBrush = Brushes.Red; else dateT.BorderBrush = Brushes.Black;
                if (horaire.Text == null) horaire.BorderBrush = Brushes.Red; else horaire.BorderBrush = Brushes.Black;
                }
            else
            {
                checkDate = DateCheck();
                if (checkDate)
                {
                    try
                    {

                        if (rdv.AddRdv(DateTime.Parse(dateT.Text + " " + horaire.Text), Globals.IdMedecin, Globals.NomPatient, Globals.PrenomPatient, (bool)isImportant.IsChecked, notesT.Text))
                        {
                            Dialog.IsOpen = true; 
                        }
                        else MessageBox.Show("Vous avez déjà un rendez-vous à cette date");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur de base de données ! ");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez rentrer une date valide");
                    dateT.BorderBrush = Brushes.Red;
                    horaire.BorderBrush = Brushes.Red;
                }
            }
        }

        private void nomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //  if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void prenomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;            
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        //private bool NumberCheck(string verifText)
        //{
        //    return int.TryParse(verifText, out int result);
        //}

        private bool DateCheck()
        {
            DateTime dateRdv = DateTime.Parse(dateT.Text + " " + horaire.Text);
            return (dateRdv < DateTime.Now) ? false : true;
        }

        private void dateT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }


        private void horaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            var parent2 = (Grid)parent.Parent;
            parent2.Children.Add(new MenuPatient());
        }
    }
}
