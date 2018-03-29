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
    /// Logique d'interaction pour TestCons.xaml
    /// </summary>
    public partial class TestCons : Window
    {
        public TestCons()
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

        private void ConsultBTN3_Click(object sender, RoutedEventArgs e)
        {
            Historique hist = new Historique();
            hist.Show();
        }
    }
}
