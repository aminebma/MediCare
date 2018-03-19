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
    class Notifications
    {
        public void GenererNotification(ListView ListeNotif/*, TextBlock NbNotifText , Border NbNotif*/)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<RendezVous> NotifRdv = (from rendezVous in dataClass.RendezVous
                                               orderby rendezVous.Date
                                               select rendezVous);
            int cpt = 0;
            if (NotifRdv.Count() != 0)
            {

                foreach (RendezVous rdv in NotifRdv)
                {
                    string dateRdv, dateAJRD;
                    dateRdv = rdv.Date.ToString().Substring(0, 10);
                    dateAJRD = DateTime.Today.ToString().Substring(0, 10);
                    if (string.Compare(dateRdv, dateAJRD) == 0)
                    {
                        TextBlock TextNotif = new TextBlock();
                        TextNotif.Text = " Rendez Vous \n Date :" + rdv.Date + " \n patient :" + rdv.IdPatient + "\n important :" + rdv.Important + "\n Note : " + rdv.Note;
                        if (string.Compare(rdv.Important.ToString(), "True") == 0) TextNotif.Foreground = Brushes.Red;
                        ListeNotif.Items.Add(TextNotif);
                        cpt++;
                    }
                }
                if (cpt != 0)
                {
                   // NbNotifText.Text = cpt.ToString();
                    System.Windows.Forms.NotifyIcon notif = new System.Windows.Forms.NotifyIcon();
                    notif.Visible = true;
                    notif.Icon = new System.Drawing.Icon(@"../../ressources/Icones/icones ico/logo_white.ico");
                    notif.ShowBalloonTip(5000, "Medicare", "Vous avez de nouvelles notifications", System.Windows.Forms.ToolTipIcon.Info);
                }
                else
                {
                    //NbNotifText.Text = "0";
                    //NbNotif.Background = Brushes.Green;
                }
            }
        }
    }
}
