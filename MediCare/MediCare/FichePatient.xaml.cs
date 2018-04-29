using Microsoft.Win32;
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
    public partial class FichePatient : UserControl
    {
        public FichePatient()
        {
            InitializeComponent();
        }

        PersonneClasse pers = new PersonneClasse();
        Regex charControl = new Regex(@"[a-zA-Z]+");
        Regex numControl = new Regex(@"[0-9]+");

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            if (Date_naiss.Text == "" || Prenom.Text == "" || Nom.Text == "" || Adresse.Text=="" || Sexe.Text=="" || Poids.Text=="" || Taille.Text=="" || Etat_sante.Text == "" || Maladie.Text=="")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (Date_naiss.Text == "") Date_naiss.BorderBrush = Brushes.Red; else Date_naiss.BorderBrush = Brushes.Black;
                if (Prenom.Text == "") Prenom.BorderBrush = Brushes.Red; else Prenom.BorderBrush = Brushes.Black;
                if (Nom.Text == "") Nom.BorderBrush = Brushes.Red; else Nom.BorderBrush = Brushes.Black;
                if (Adresse.Text == "") Adresse.BorderBrush = Brushes.Red; else Adresse.BorderBrush = Brushes.Black;
                if (Sexe.Text == "") Sexe.BorderBrush = Brushes.Red; else Sexe.BorderBrush = Brushes.Black;
                if (Poids.Text == "") Poids.BorderBrush = Brushes.Red; else Poids.BorderBrush = Brushes.Black;
                if (Taille.Text == "") Taille.BorderBrush = Brushes.Red; else Taille.BorderBrush = Brushes.Black;
                if (Etat_sante.Text == "") Etat_sante.BorderBrush = Brushes.Red; else Etat_sante.BorderBrush = Brushes.Black;
                if (Maladie.Text == "") Maladie.BorderBrush = Brushes.Red; else Maladie.BorderBrush = Brushes.Black;
            }
            else
            {
                try
                {
                    pers.AddPatientPersonne(Nom.Text, Prenom.Text, Date_naiss.Text, Adresse.Text, Num_tel.Text, Sexe.Text, Taille.Text, Poids.Text, Groupage.Text, Maladie.Text, Etat_sante.Text);
                    Globals.NomPatient = Nom.Text;
                    Globals.PrenomPatient = Prenom.Text;
                    DateTime date = Date_naiss.SelectedDate.Value;
                    Globals.AdressePatient = Adresse.Text;
                    Globals.Age = DateTime.Today.Year - date.Year;
                    Dialog.IsOpen = true;
                }catch
                {
                    MessageBox.Show("une erreur s'est produite, le patient n'a pas été ajouté");
                }
  
            }

        }

        private void Nom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void Prenom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void Num_tel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!numControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void Groupage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (numControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new MenuPatient());
        }
    }
}
