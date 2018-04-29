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
        public int i = 0;
        Medic med = new Medic();
        Agenda pat = new Agenda();
        List<Personne> listPatientsTmp;
        List<Medicaments> listMedicTmp;
        List<Personne> listMedecinsTmp;

        public MenuPrincipal()
        {
            InitializeComponent();
            Menu usc = new Menu();
            usc.SetFenetreprincipal = this;
            SelectionGrid.Children.Add(usc);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Globals.TempRappelRDV = 15;
            Task.Factory.StartNew(GenererNotif, TaskCreationOptions.LongRunning);
            
        }

        
        private async void GenererNotif()
        {
            IQueryable<RendezVous> NotifRdv0 = (from rendezVous in Globals.DataClass.RendezVous
                                               orderby rendezVous.Date
                                               select rendezVous);
            foreach( RendezVous rdvv in NotifRdv0)
            {
                rdvv.Notified = false;
                Globals.DataClass.SubmitChanges();
            }
            while (true)
            {
                try
                {
                    IQueryable<RendezVous> NotifRdv = (from rendezVous in Globals.DataClass.RendezVous
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

                                IQueryable<Patient> patientRDV = (from patient in Globals.DataClass.Patient
                                                                  where rdv.IdPatient == patient.Id
                                                                  select patient);
                                IQueryable<Personne> personneRDV = (from personne in Globals.DataClass.Personne
                                                                    where patientRDV.First().IdPersonne == personne.Id
                                                                    select personne);
                                if (personneRDV.Count() != 0 && patientRDV.Count() != 0)
                                {
                                    string NomPatient = personneRDV.First().nom;
                                    string PrenomPatient = personneRDV.First().prenom;
                                    string heureRDV = rdv.Date.ToString().Substring(10, 9);
                                    await Dispatcher.BeginInvoke(new Action(delegate ()
                                     {
                                         rdv.Notified = true;
                                         Globals.DataClass.SubmitChanges();
                                         Notification.IsActive = true;
                                         Notification.Message.Content = "Vous avez un rendez vous important dans " + Globals.TempRappelRDV.ToString() + " minutes";
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

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            SelectionGrid.Children.Clear();
            UserControl usc = new Menu();
            SelectionGrid.Children.Add(usc);
        }

        private void LogoutBTN_Click(object sender, RoutedEventArgs e)
        {
            SignUp wndw = new SignUp();
            wndw.Show();
            this.Close();
        }

        private void Compte_Click(object sender, RoutedEventArgs e)
        {
            SelectionGrid.Children.Clear();
            MonCompte usc = new MonCompte();
            usc.SetFenetrePrincipale = this;
            SelectionGrid.Children.Add(usc);
            
        }

        

        private void Agenda_Click(object sender, RoutedEventArgs e)
        {
            SelectionGrid.Children.Clear();
            SelectionGrid.Children.Add(new AgendaMenu());
        }

        private void Patient_Click(object sender, RoutedEventArgs e)
        {
            SelectionGrid.Children.Clear();
            SelectionGrid.Children.Add(new AffichPatient());
        }


        private void ActionClick(object sender, RoutedEventArgs e)
        {
            Notification.IsActive = false;
        }

        private void element_GotFocus(object sender, RoutedEventArgs e)
        {
            element.IsDropDownOpen = true;
        }

        private void element_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && element.IsDropDownOpen && element.HasItems) element.Text = element.Items.GetItemAt(0).ToString();
            if(e.Key==Key.Enter && element.Text != "")
            {
                listPatientsTmp = pat.RechercherPatient(element.Text);
                if (listPatientsTmp.Count() != 0)
                {
                    Globals.NomPatient = listPatientsTmp[0].nom;
                    SelectionGrid.Children.Clear();
                    SelectionGrid.Children.Add(new AffichDossiers());
                }
                else
                {
                    listMedicTmp = med.RechercheMedicament(element.Text);
                    if (listMedicTmp.Count() != 0)
                    {
                        Globals.Medicament = listMedicTmp[0].nom;
                        SelectionGrid.Children.Clear();
                        SelectionGrid.Children.Add(new affichMedicament());
                    }
                    else
                    {
                        listPatientsTmp = pat.RechercherMedecin(element.Text);
                        if (listPatientsTmp.Count() != 0)
                        {
                            Globals.NomMedecin = listPatientsTmp[0].nom;
                            SelectionGrid.Children.Clear();
                            SelectionGrid.Children.Add(new AffichMedecin());
                        }
                    }
                }
            }
        }

        private void element_TextChanged(object sender, TextChangedEventArgs e)
        {
            listPatientsTmp = pat.RechercherPatientNom(element.Text);

            if (listPatientsTmp.Count() != 0)
            {
                element.Items.Clear();
                foreach (Personne patient in listPatientsTmp)
                {
                    element.Items.Add(element.Items.Add(patient.nom));
                    if (element.Items.Count != 0) element.Items.RemoveAt(element.Items.Count - 1);
                }
            }

            listMedicTmp = med.RechercheMedicament(element.Text);
            if (listMedicTmp.Count() != 0)
            {
                element.Items.Clear();
                foreach (Medicaments medicament in listMedicTmp)
                {
                    if (medicament.nom.Length > 25) element.Items.Add(medicament.nom.Substring(0, 25));
                    else element.Items.Add(medicament.nom);
                }
            }
            listMedecinsTmp = pat.RechercherMedecin(element.Text);
            if (listMedecinsTmp.Count() != 0)
            {
                element.Items.Clear();
                foreach (Personne medecin in listMedecinsTmp)
                {
                    if (medecin.nom.Length > 25) element.Items.Add(medecin.nom.Substring(0, 25));
                    else element.Items.Add(medecin.nom);
                }
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(element.Text!="")
            {
                listPatientsTmp = pat.RechercherPatient(element.Text);
                if (listPatientsTmp.Count() != 0)
                {
                    Globals.NomPatient = listPatientsTmp[0].nom;
                    SelectionGrid.Children.Clear();
                    SelectionGrid.Children.Add(new AffichDossiers());
                }
                else
                {
                    listMedicTmp = med.RechercheMedicament(element.Text);
                    if (listMedicTmp.Count() != 0)
                    {
                        Globals.Medicament = listMedicTmp[0].nom;
                        SelectionGrid.Children.Clear();
                        SelectionGrid.Children.Add(new affichMedicament());
                    }
                    else
                    {
                        listPatientsTmp = pat.RechercherMedecin(element.Text);
                        if (listPatientsTmp.Count() != 0)
                        {
                            Globals.NomMedecin = listPatientsTmp[0].nom;
                            SelectionGrid.Children.Clear();
                            SelectionGrid.Children.Add(new AffichMedecin());
                        }
                    }
                }
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SelectionGrid.Children.Clear();
            SelectionGrid.Children.Add(new MenuRecherche());
        }

        private void LogoutBTN(object sender, RoutedEventArgs e)
        {
            Dialog.IsOpen=true;
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"medicare_help.chm");
        }
    }
}
