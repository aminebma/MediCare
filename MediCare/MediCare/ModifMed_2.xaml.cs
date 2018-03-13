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
    /// Logique d'interaction pour ModifMed_2.xaml
    /// </summary>
    public partial class ModifMed_2 : Window
    {
        public ModifMed_2(string username, string password)
        {
            InitializeComponent();
            user = username;
            pass = password;
        }

        Medecin2 medd = new Medecin2();
        private string user, pass;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            medd.ModifMed(user, pass, nTel.Text, nadresse.Text);
             MessageBox.Show("Tout  a été modifié ! ");
            
        }
    }
}
