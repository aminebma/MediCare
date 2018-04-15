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
    /// Logique d'interaction pour AffichPatient.xaml
    /// </summary>
    public partial class AffichPatient : UserControl
    {
        public AffichPatient()
        {
            InitializeComponent();
        }
        PersonneClasse pers = new PersonneClasse();
        private void search_Click(object sender, RoutedEventArgs e)
        {
            IList<Personne> l;
            l = pers.RechercherPatientBDD(Nom.Text, Prenom.Text);
            
        }
    }
}
