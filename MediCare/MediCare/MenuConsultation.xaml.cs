

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
    /// Logique d'interaction pour MenuConsultation.xaml
    /// </summary>
    public partial class MenuConsultation : Window
    {
        public MenuConsultation()
        {
            InitializeComponent();
        }

        private void AjoutNouvPatient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AjoutConsultPatient_Click(object sender, RoutedEventArgs e)
        {
            AjoutConsultation CSLT = new AjoutConsultation();
            CSLT.Show();
        }

        private void consutlationItem_Selected(object sender, RoutedEventArgs e)
        {
            AgendaMediCare Agd = new AgendaMediCare();
            Agd.Show();
        }
    }
}
