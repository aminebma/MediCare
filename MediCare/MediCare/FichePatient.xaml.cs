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

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pourFichePatient.xaml
    /// </summary>
    public partial class FichePatient : UserControl
    {
        public FichePatient()
        {
            InitializeComponent();
        }

        PersonneClasse pers = new PersonneClasse();
        List<string> caract = new List<string> { ",", ".", ":", ";", "!", "*", "$", "/", "?", "+", "_", "=", "§", "<", ">", "{", "}", "[", "]", "(", ")", "'", "\"", "&", "²", "@", "|", "#" };

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
           
            pers.AddPatientPersonne(Nom.Text, Prenom.Text, Date_naiss.Text, Adresse.Text, Num_tel.Text, Sexe.Text, Taille.Text, Poids.Text, Groupage.Text, Maladie.Text, Etat_santé.Text);
            MessageBox.Show("Le patient a été inséré ! ");
        }

        private void Nom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
        }

        private bool NumberCheck(string verifText)
        {
            return int.TryParse(verifText, out int result);
        }

        private void Prenom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
        }

        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void Num_tel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
        }


        private void Groupage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            //PatientASupp patsup = new PatientASupp();
            //patsup.Show();
        }

        private void nom_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
