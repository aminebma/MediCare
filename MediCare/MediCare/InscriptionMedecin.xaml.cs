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
        
        Medecin2 med = new Medecin2();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           med.AddMed(nom.Text, prenom.Text, DateTime.Parse(date_naiss.Text), adresse.Text, num_tel.Text, sexe.Text,  clef.Text, username.Text, password.Text);
          MessageBox.Show("Le medecin a été inséré ! ");
        }

    }
    }

     

