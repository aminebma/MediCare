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
    /// Logique d'interaction pour SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal main = new MenuPrincipal();
            Medecin2 MED = new Medecin2();
            //MessageBox.Show(password.Password);
            InscriptionMedecin t = new InscriptionMedecin();
            this.Close();
            t.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Medecin2 med = new Medecin2();
            if(med.VerifMed(nom.Text,password.Password))
            {
                MenuPrincipal main = new MenuPrincipal();
                main.Show();
                this.Close();
            }
            else MessageBox.Show("Les informations saisies sont incorrectes");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MenuPrincipal Menu = new MenuPrincipal();
            Menu.Show();
            this.Close();
        }
    }
}