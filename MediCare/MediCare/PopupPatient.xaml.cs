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
    /// Logique d'interaction pour PopupPatient.xaml
    /// </summary>
    public partial class PopupPatient : Window
    {
        public PopupPatient()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        PersonneClasse pers = new PersonneClasse();

        private void Oui_Click(object sender, RoutedEventArgs e)
        {

            FichePatient TestPatient = new FichePatient();
            TestPatient.Show();
           
        }

        private void Non_Click(object sender, RoutedEventArgs e)
        {
            pers.AddPatientPersonne(Globals.NomPatient,Globals.PrenomPatient,null,null,null,null,null,null,null,null,null);
            AjoutConsultation CSLT = new AjoutConsultation();
            CSLT.Show();
        }
    }
}
