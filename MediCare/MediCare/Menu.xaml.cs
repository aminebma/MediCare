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
        Notifications ClassNotif = new Notifications();
        public Menu(ListView ListRDV)
        {   
            InitializeComponent();
            ClassNotif.GenererRDVduJour(ListeRDV);            
        }

        private void refreshRDVListe_Click(object sender, RoutedEventArgs e)
        {
            ListeRDV.Items.Clear();
            ClassNotif.GenererRDVduJour(ListeRDV);
        }

        private void ConsultationBTN_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            UserControl usc = new Consultation_Patient();
            parent.Children.Clear();
            parent.Children.Add(usc);
        }

        private void AgendaBTN_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            UserControl usc = new AgendaMenu();
            parent.Children.Clear();
            parent.Children.Add(usc);
        }
        private void SuivieBTN_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            UserControl usc = new MenuSuivi();
            parent.Children.Clear();
            parent.Children.Add(usc);
        }
    }

}
