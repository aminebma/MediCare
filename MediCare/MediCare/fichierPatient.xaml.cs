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
    /// Logique d'interaction pour fichierPatient.xaml
    /// </summary>
    public partial class fichierPatient : UserControl
    {
        Agenda pat = new Agenda();
        List<ConsultLabel> list;
        Consult consultation = new Consult();
        Consulta consulta = new Consulta();
        public string chemin;
        public fichierPatient()
        {
            InitializeComponent();
            list = consultation.Suivie(Globals.NomPatient, Globals.PrenomPatient);
            if (list.Count() != 0)
            {
                foreach (ConsultLabel p in list)
                {
                    consulta = consultation.AcceeConsultationId(p.Id);
                    if (consulta.CheminOrdo != null)
                    {
                        ButtonSpec fich = new ButtonSpec();
                        fich.Background = null;
                        fich.BorderBrush = null;
                        fich.chemin = consulta.CheminOrdo;
                        fich.Content = "Ordonnace : Label : " + p.Label + "Date : " + p.Date.Day + "-" + p.Date.Month + "-" + p.Date.Year;
                        fich.Click += new RoutedEventHandler(fich_Click);
                        Stack.Children.Add(fich);
                    }
                    if (consulta.CheminLettreOrientation != null)
                    {
                        ButtonSpec lettre = new ButtonSpec();
                        lettre.Background = null;
                        lettre.BorderBrush = null;
                        lettre.chemin = consulta.CheminLettreOrientation;
                        lettre.Content = "Lettre d'orientation : Label : " + p.Label + "\nDate : " + p.Date.Day + "-" + p.Date.Month + "-" + p.Date.Year;
                        lettre.Click += new RoutedEventHandler(fich_Click);
                        Stack.Children.Add(lettre);
                    }
                    if (consulta.CheminCertificat != null)
                    {
                        ButtonSpec certificat = new ButtonSpec();
                        certificat.Background = null;
                        certificat.BorderBrush = null;
                        certificat.chemin = consulta.CheminCertificat;
                        certificat.Content = "Certificat médical : Label : " + p.Label + "\nDate : " + p.Date.Day + "-" + p.Date.Month + "-" + p.Date.Year;
                        certificat.Click += new RoutedEventHandler(fich_Click);
                        Stack.Children.Add(certificat);
                    }
                    if (consulta.CheminBilan != null)
                    {
                        ButtonSpec bilan = new ButtonSpec();
                        bilan.Background = null;
                        bilan.BorderBrush = null;
                        bilan.chemin = consulta.CheminBilan;
                        bilan.Content = "Bilan médical : Label : " + p.Label + "\nDate : " + p.Date.Day + "-" + p.Date.Month + "-" + p.Date.Year;
                        bilan.Click += new RoutedEventHandler(fich_Click);
                        Stack.Children.Add(bilan);
                    }
                    if (consulta.CheminCertificat != null)
                    {
                        ButtonSpec scan = new ButtonSpec();
                        scan.Background = null;
                        scan.BorderBrush = null;
                        scan.chemin = consulta.CheminScanner;
                        scan.Content = "Scanner : Label : " + p.Label + "\nDate : " + p.Date.Day + "-" + p.Date.Month + "-" + p.Date.Year;
                        scan.Click += new RoutedEventHandler(fich_Click);
                        Stack.Children.Add(scan);
                    }
                    if (consulta.Radio.Count() != 0)
                    {
                        foreach (string rad in consulta.Radio)
                        {
                            if (rad != null)
                            {
                                ButtonSpec radio = new ButtonSpec();
                                radio.Background = null;
                                radio.BorderBrush = null;
                                radio.chemin = rad;
                                radio.Content = "Radio : Label : " + p.Label + "\nDate : " + p.Date.Day + "-" + p.Date.Month + "-" + p.Date.Year;
                                radio.Click += new RoutedEventHandler(fich_Click);
                                Stack.Children.Add(radio);
                            }
                        }
                    }

                }
            }
        }

        public void fich_Click(object s, EventArgs args)
        {
            ButtonSpec bout = new ButtonSpec();
            bout = (ButtonSpec)s;
            chemin = bout.chemin;
            try
            {
                System.Diagnostics.Process.Start(chemin);
            }
            catch (Exception)
            {
                MessageBox.Show("Une erreur s'est produite ! ");
            }

        }
        public class ButtonSpec : Button
        {
            public string chemin { get; set; }
        }
    }
}
