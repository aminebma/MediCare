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
    /// Logique d'interaction pour AgendaMediCare.xaml
    /// </summary>
    public partial class AgendaMediCare : Window
    {
        public AgendaMediCare()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void AjtRDVpro_Click(object sender, RoutedEventArgs e)
        {
            AjoutRdvPro RDVPRO = new AjoutRdvPro();
            RDVPRO.Show();
        }

        private void AjtRDVPersBtn_click(object sender, RoutedEventArgs e)
        {
            AjoutRdvPerso RDVPERSO = new AjoutRdvPerso();
            RDVPERSO.Show();
        }
    }
}
