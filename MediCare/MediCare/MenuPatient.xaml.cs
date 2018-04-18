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

        
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Consultation":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new AjoutConsultation());
                    break;
                case "Suivi":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new SUIVIS());
                    break;
                case "Dossier":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new DossierMedical());
                    break;
                case "Supprimer":
                    GridDroit.Children.Clear();
                    GridDroit.Children.Add(new SupprimerPatient());
                    break;
                default:
                    break;
            }
        }

        
    }
}
