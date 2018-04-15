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
using System.Text.RegularExpressions;
using System.Windows.Navigation;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour MenuHistorique.xaml
    /// </summary>
    public partial class MenuHistorique : UserControl
    {
        public MenuHistorique()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "hist":
                    this.GridDroit.Children.Clear();
                    this.GridDroit.Children.Add(new Historique());
                    break;
                case "histmed":
                    this.GridDroit.Children.Clear();
                    this.GridDroit.Children.Add(new HistoriqueMedecin());
                    break;
                default:
                    break;
            }
        }
    }
}
