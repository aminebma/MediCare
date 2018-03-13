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

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            GenererNotification();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

    

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
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
                    usc = new Consultation_Patient();
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

            private void GenererNotification()
            {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<RendezVous> NotifRdv = (from rendezVous in dataClass.RendezVous
                                               select rendezVous);

    
            if ( NotifRdv.Count() != 0)
            {
                NotificationsExpender.Foreground = Brushes.Red;
                foreach ( RendezVous rdv in NotifRdv)
                {
                    TextBlock TextNotif = new TextBlock();
                    TextNotif.Text = " Rendez Vous \n Date :" + rdv.Date + " \n patient :" + rdv.IdPatient + "\n important :" + rdv.Important + "\n Note : " + rdv.Note;
                    ListeNotif.Items.Add(TextNotif);
                }
            }
        }
    }
}
