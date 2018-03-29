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
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;

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
            UserControl usc = new Menu(ListRDV);
            SelectionGrid.Children.Add(usc);
            usc.VerticalAlignment = VerticalAlignment.Stretch;
            usc.HorizontalAlignment = HorizontalAlignment.Stretch;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Globals.TempRappelRDV = 15;
            Task.Factory.StartNew(GenererNotif, TaskCreationOptions.LongRunning);
        }

        private async void GenererNotif()
        {
            while (true)
            {
                try
                {
                    string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
                    MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
                    IQueryable<RendezVous> NotifRdv = (from rendezVous in dataClass.RendezVous
                                                       orderby rendezVous.Date
                                                       select rendezVous);
                    if (NotifRdv.Count() != 0)
                    {
                        foreach (RendezVous rdv in NotifRdv)
                        {
                            TimeSpan t = (DateTime)rdv.Date - DateTime.Now;
                            string dateRdv, dateAJRD;
                            dateRdv = rdv.Date.ToString().Substring(0, 10);
                            dateAJRD = DateTime.Today.ToString().Substring(0, 10);

                            if (string.Compare(dateRdv, dateAJRD) == 0 && t.TotalMinutes > 0 && t.TotalMinutes < Globals.TempRappelRDV && rdv.IdPatient != 0 && rdv.Important == true && rdv.Notified == false)
                            {

                                IQueryable<Patient> patientRDV = (from patient in dataClass.Patient
                                                                  where rdv.IdPatient == patient.Id
                                                                  select patient);
                                IQueryable<Personne> personneRDV = (from personne in dataClass.Personne
                                                                    where patientRDV.First().IdPersonne == personne.Id
                                                                    select personne);
                                if (personneRDV.Count() != 0 && patientRDV.Count() != 0)
                                {
                                    string NomPatient = personneRDV.First().nom;
                                    string PrenomPatient = personneRDV.First().prenom;
                                    string heureRDV = rdv.Date.ToString().Substring(10, 9);
                                    await Dispatcher.BeginInvoke(new Action(delegate ()
                                     {
                                         TextBlock TextRDV = new TextBlock();
                                         TextRDV.Text = " RENDEZ VOUS DANS " + Globals.TempRappelRDV.ToString() + " min \n Patient: \n Nom: " + NomPatient + "\n Prenom: " + PrenomPatient + "\n Heure: " + heureRDV + "\n\t(" + DateTime.Now.ToString() + ")";
                                         ListeNotif.Items.Add(TextRDV);
                                         if (NotificationBadge.Badge.Equals("0")) NotificationBadge.BadgeBackground = Brushes.Red;
                                         NotificationBadge.Badge = int.Parse(NotificationBadge.Badge.ToString()) + 1;
                                         rdv.Notified = true;
                                         dataClass.SubmitChanges();
                                     }));
                                    System.Windows.Forms.NotifyIcon notif = new System.Windows.Forms.NotifyIcon();
                                    notif.Visible = true;
                                    notif.Icon = new System.Drawing.Icon(@"../../ressources/Icones/icones ico/logo_white.ico");
                                    notif.ShowBalloonTip(5000, "Medicare", "Vous avez un rendez vous important dans moins de " + Globals.TempRappelRDV.ToString() + " minutes ", System.Windows.Forms.ToolTipIcon.Info);
                                }
                            }
                        }
                    }
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
                catch (Exception) { }
            }
        }

        public ListView ListRDV { get; }


        private void EffacerNotif_Click(object sender, RoutedEventArgs e)
        {
            ListeNotif.Items.Clear();
            NotificationBadge.BadgeBackground = Brushes.Green;
            NotificationBadge.Badge = "0";
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            SelectionGrid.Children.Clear();
            UserControl usc = new Menu(ListeNotif);
            SelectionGrid.Children.Add(usc);
        }

        private void LogoutBTN_Click(object sender, RoutedEventArgs e)
        {
            LogOutPop wndw = new LogOutPop();
            wndw.Show();
        }
    }
}
