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
    /// Logique d'interaction pour MenuMédicament.xaml
    /// </summary>
    public partial class MenuMédicament : UserControl
    {
        public MenuMédicament()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "AjtMedic":
                    Grid2.Children.Clear();
                    Grid2.Children.Add(new AjouterMédic());
                    break;
                case "SearchMedic":
                    Grid2.Children.Clear();
                    Grid2.Children.Add(new toutMedic());
                    break;
                default: break;
            }
        }
    }
}
