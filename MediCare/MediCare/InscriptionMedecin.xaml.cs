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
    /// Logique d'interaction pour InscriptionMedecin.xaml
    /// </summary>
    public partial class InscriptionMedecin : Window
    {
        public InscriptionMedecin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
        Medecin2 med = new Medecin2();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // med.AddMed(nom.Text, prenom.Text, date_naiss.Text, adresse.Text, num_tel.Text, sexe.Text,  username.Text, password.Text, clef.Text);
          //  MessageBox.Show("Le medecin a été inséré ! ");
        }

        private void heureBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    }

     

