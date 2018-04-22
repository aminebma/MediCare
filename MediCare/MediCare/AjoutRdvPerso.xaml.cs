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
    public partial class AjoutRdvPerso : UserControl
    {
        public AjoutRdvPerso()
        {
            InitializeComponent();
           
        }
        
        Agenda rdv = new Agenda();

        private bool DateCheck()
        {
            DateTime dateRdv = DateTime.Parse(dateT.Text + " " + horaire.Text);
            return (dateRdv < DateTime.Now) ? false : true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate = false;
            if (dateT.Text == "" || horaire.Text == null)
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
                        rdv.AddRdv(DateTime.Parse(dateT.Text + " " + horaire.Text), Globals.IdMedecin, notesT.Text);
                        Dialog.IsOpen = true;
                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Une erreur s'est produite lors de l'accés à la base de données, le rendez-vous n'a pas été ajouté");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez saisir une date valide");
                    dateT.BorderBrush = Brushes.Red;
                    horaire.BorderBrush = Brushes.Red;
                }
            }
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
            parent2.Children.Add(new AgendaMenu());
        }
    }
}
