using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        Window fenetreprincipal;

        public Window SetFenetreprincipal
        {
            get { return fenetreprincipal; }
            set { fenetreprincipal = value; }
        }

        Notifications ClassNotif = new Notifications();
        public Menu()
        {   
            InitializeComponent();
            ClassNotif.GenererRDVduJour(ListeRDV);       
        }

        

        private void AgendaBTN_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            AgendaMenu usc = new AgendaMenu();
            parent.Children.Clear();
            usc.SetGrid2 = new AgendaMedecin();
            parent.Children.Add(usc);
        }
        private void SuivieBTN_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new SUIVIS());
        }

        private void PAtientBTN_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new AffichPatient());
        }

        private void CompteBTN_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            MonCompte usc = new MonCompte();
            usc.SetFenetrePrincipale = fenetreprincipal;
            parent.Children.Clear();
            parent.Children.Add(usc);
        }

        private void RechercheBTN_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new MenuRecherche());
        }

        private void MedicamentBTN_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new MenuMédicament());
        }
    }

}
