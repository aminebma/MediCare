﻿using System;
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
        List<Personne> listPatientsTmp;
        List<Medicaments> listMedicTmp;
        Medic med = new Medic();
        Agenda pat = new Agenda();

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
                                         TextBlock TextRDV = new TextBlock();
                                         TextRDV.Text = " RENDEZ VOUS DANS " + Globals.TempRappelRDV.ToString() + " min \n Patient: \n Nom: " + NomPatient + "\n Prenom: " + PrenomPatient + "\n Heure: " + heureRDV + "\n\t(" + DateTime.Now.ToString() + ")";
                                         ListeNotif.Items.Add(TextRDV);
                                         if (NotificationBadge.Badge.Equals("0")) NotificationBadge.BadgeBackground = Brushes.Red;
                                         NotificationBadge.Badge = int.Parse(NotificationBadge.Badge.ToString()) + 1;
                                         rdv.Notified = true;
                                         Globals.DataClass.SubmitChanges();
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
            wndw.setCreatingForm = this;
            wndw.Show();
        }

        private void Compte_Click(object sender, RoutedEventArgs e)
        {
            SelectionGrid.Children.Clear();
            MonCompte usc = new MonCompte();
            usc.SetFenetrePrincipale = this;
            SelectionGrid.Children.Add(usc);
            
        }

        private void Consultation_Click(object sender, RoutedEventArgs e)
        {
            SelectionGrid.Children.Clear();
            SelectionGrid.Children.Add(new Consultation_Patient());
        }

        private void Agenda_Click(object sender, RoutedEventArgs e)
        {
            SelectionGrid.Children.Clear();
            SelectionGrid.Children.Add(new AgendaMenu());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void element_GotFocus(object sender, RoutedEventArgs e)
        {
            element.IsDropDownOpen = true;
        }

        private void element_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && element.IsDropDownOpen && element.HasItems) element.Text = element.Items.GetItemAt(0).ToString();
        }

        private void element_TextChanged(object sender, TextChangedEventArgs e)
        {
            listPatientsTmp = pat.RechercherPatientNom(element.Text);
            
            if (listPatientsTmp.Count() != 0)
            {
                element.Items.Clear();
                foreach (Personne patient in listPatientsTmp)
                {
                    element.Items.Add(element.Items.Add(patient.nom + " " + patient.prenom));
                    if (element.Items.Count != 0) element.Items.RemoveAt(element.Items.Count - 1);
                }
            }
            
            listMedicTmp = med.RechercheMedicament(element.Text);
            if (listMedicTmp.Count() != 0)
            {
                element.Items.Clear();
                foreach (Medicaments medicament in listMedicTmp)
                {
                    element.Items.Add(element.Items.Add(medicament.nom));
                    if (element.Items.Count != 0) element.Items.RemoveAt(element.Items.Count - 1);
                }
            }
            
        }

        private void AjtMedic_Click(object sender, RoutedEventArgs e)
        {
            AjouterMédic wndw = new AjouterMédic();
            wndw.Show();
        }
    }
}
