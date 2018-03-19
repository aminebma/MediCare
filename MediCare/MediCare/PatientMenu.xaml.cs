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
    /// Logique d'interaction pour PatientMenu.xaml
    /// </summary>
    public partial class PatientMenu : UserControl
    {
        public PatientMenu()
        {
            InitializeComponent();
        }

        private void Operation_Patient_Click(object sender, RoutedEventArgs e)
        {
            FichePatient Test = new FichePatient();
            Test.Show();
        }
    }
}
