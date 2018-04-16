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
    /// Logique d'interaction pour MenuRecherche.xaml
    /// </summary>
    public partial class MenuRecherche : UserControl
    {
        public MenuRecherche()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "DossierPatient":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new ToutPatients());
                    break;
                case "Dossier":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new ToutPatients());
                    break;
                case "Medecins":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new ToutMedecins());
                    break;
                case "RechMedecin":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new ToutMedecins());
                    break;
                case "RechRDV":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new ToutPatients());
                    break;
                case "RechMedic":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new toutMedic());
                    break;
                default:
                    break;
            }
        }
    }
}
