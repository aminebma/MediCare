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
    /// Logique d'interaction pour AgendaMenu.xaml
    /// </summary>
    public partial class AgendaMenu : UserControl
    {
        public AgendaMenu()
        {
            InitializeComponent();
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

        private void ModifRdv_Btn_Click(object sender, RoutedEventArgs e)
        {
            ModSuppRdv MSRDV = new ModSuppRdv();
            MSRDV.Show();
        }
    }
}
