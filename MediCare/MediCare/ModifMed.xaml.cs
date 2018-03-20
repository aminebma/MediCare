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
    /// Logique d'interaction pour ModifMed.xaml
    /// </summary>
    public partial class ModifMed : Window
    {
        public ModifMed()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        Medecin2 med = new Medecin2();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           bool verif= med.VerifMed(password2.Text, username2.Text);
            if ( verif == false)
            {
                MessageBox.Show("Vos données sont erronées ! Veuillez les resaisir  ");
            }
            else
            {
                ModifMed_2 tt2 = new ModifMed_2();
                tt2.Show();
                this.Close();
            }
        }
    }
}
