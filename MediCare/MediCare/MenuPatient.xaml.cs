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
    /// Logique d'interaction pour MenuPatient.xaml
    /// </summary>
    public partial class MenuPatient : UserControl
    {
        public MenuPatient()
        {
            InitializeComponent();
        }

        private void Rechercher_pat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rech_pat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ajout_pat_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            UserControl usc = new FichePatient();
            parent.Children.Clear();
            parent.Children.Add(usc);
        }

        private void Supr_pat_Click(object sender, RoutedEventArgs e)
        {
            PatientASupp p = new PatientASupp();
            p.Show();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "SearchPatient":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new AffichPatient());
                    break;
                case "AjtPatient":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new FichePatient());
                    break;
                case "SupPatient":
                    break;
                default:
                    break;
            }
        }
    }
}
