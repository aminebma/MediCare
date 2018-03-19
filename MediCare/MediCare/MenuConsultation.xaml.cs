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
    /// Logique d'interaction pour MenuConsultation.xaml
    /// </summary>
    public partial class MenuConsultation : UserControl
    {
        public MenuConsultation()
        {
            InitializeComponent();
        }

        private void ConsultBTN1_Click(object sender, RoutedEventArgs e)
        {
            Consultation_Patient usc = new Consultation_Patient();
            usc.Show();
        }

        private void ConsultBTN2_Click(object sender, RoutedEventArgs e)
        {
            SUIVIS suiv = new SUIVIS();
            suiv.Show();
        }
    }
}
