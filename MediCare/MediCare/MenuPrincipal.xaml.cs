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
using System.Reflection;
using System.Threading;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        
        public MenuPrincipal()
        {
            Notifications ClassNotif = new Notifications();
            //Timer clock = new Timer(, null, 0, 1000);
            InitializeComponent();
            ClassNotif.GenererNotification(ListeNotif/*, NbNotifText, NbNotif*/);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            SelectionGrid.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    this.Close();
                    break;
                case "ItemAgenda":
                    usc = new AgendaMenu();
                    SelectionGrid.Children.Add(usc);
                    break;
                case "ItemConsultation":
                    usc = new MenuConsultation();
                    SelectionGrid.Children.Add(usc);
                    break;
                case "ItemPatient":
                    usc = new PatientMenu();
                    SelectionGrid.Children.Add(usc);
                    break;
                case "ItemRecherche":
                    usc = new RechercheMenu();
                    SelectionGrid.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LogOutPop logout = new LogOutPop();
            logout.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ModifMed tt = new ModifMed();
            tt.Show();
        }

        private void ListeNotif_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
