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
    /// Logique d'interaction pour AgendaMediCare.xaml
    /// </summary>
    public partial class AgendaMediCare : Window
    {
        public AgendaMediCare()
        {
            InitializeComponent();
            GenererNotification();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void AjtRDVpro_Click(object sender, RoutedEventArgs e)
        {
            AjoutRdvPro RDVPRO = new AjoutRdvPro();
            RDVPRO.Show();
        }

        private void AjtRDVPersBtn_click(object sender, RoutedEventArgs e)
        {
            AjoutRdvPerso RDVPERSO = new AjoutRdvPerso();
            RDVPERSO.Show();
        }

        private void GenererNotification()
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<RendezVous> NotifRdv = (from RendezVous in dataClass.RendezVous
                                               select RendezVous);

    
            if ( NotifRdv.Count() != 0)
            {
                RendezVous test = NotifRdv.First<RendezVous>();
                TextNotif.Text = " Rendez Vous \n Date :" + test.Date + " \n patient :" + test.IdPatient + "\n important :" + test.Important + "\n Note : " + test.Note;
            }

            if (TextNotif.Text != "Aucune notification...")
            {
                NotificationsExpender.Foreground = Brushes.Red;
            }
        }
    }
}
