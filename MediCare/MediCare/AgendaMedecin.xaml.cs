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
    /// Logique d'interaction pour AgendaMedecin.xaml
    /// </summary>
    public partial class AgendaMedecin : UserControl
    {
        public AgendaMedecin()
        {
            InitializeComponent();
            Calendrier.SelectedDate = DateTime.Today;
        }

        private void Calendrier_SelectedDatesChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var BackBrush = new SolidColorBrush(Color.FromRgb(153, 180, 209));
            var SelectBrush = new SolidColorBrush(Color.FromRgb(33, 150, 243));
            _7.Background = BackBrush;
            _8.Background = BackBrush;
            _9.Background = BackBrush;
            _10.Background = BackBrush;
            _11.Background = BackBrush;
            _12.Background = BackBrush;
            _13.Background = BackBrush;
            _14.Background = BackBrush;
            _15.Background = BackBrush;
            _16.Background = BackBrush;
            _17.Background = BackBrush;
            ListeRDV7.Items.Clear();
            ListeRDV8.Items.Clear();
            ListeRDV9.Items.Clear();
            ListeRDV10.Items.Clear();
            ListeRDV11.Items.Clear();
            ListeRDV12.Items.Clear();
            ListeRDV13.Items.Clear();
            ListeRDV14.Items.Clear();
            ListeRDV15.Items.Clear();
            ListeRDV16.Items.Clear();
            ListeRDV17.Items.Clear();
            agenda.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            Agenda agd = new Agenda();
            List<RendezVous> ListRDV = agd.RendezVousAujrd(Calendrier.SelectedDate.ToString());
            foreach(RendezVous rdv in ListRDV)
            {
                IQueryable<Patient> patientRDV = (from patient in Globals.DataClass.Patient
                                                  where rdv.IdPatient == patient.Id
                                                  select patient);
                IQueryable<Personne> personneRDV = (from personne in Globals.DataClass.Personne
                                                    where patientRDV.First().IdPersonne == personne.Id
                                                    select personne);
                if (patientRDV.Count() != 0)
                {
                    if (patientRDV.First().Id != 0)
                    {
                        TextBox test = new TextBox();
                        agenda.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                        test.AcceptsReturn = true;
                        test.Width = 450;
                        test.Foreground = Brushes.White;
                        test.IsReadOnly = true;
                        test.HorizontalContentAlignment = HorizontalAlignment.Center;
                        test.VerticalContentAlignment = VerticalAlignment.Center;
                        test.Text = "Rendez Vous: Patient " + personneRDV.First().nom + " " + personneRDV.First().prenom + "\n Heure" + rdv.Date.ToString().Substring(11, 8) + "\n Note :" + rdv.Note;
                        if (rdv.Important == true)
                        {
                            test.Foreground = Brushes.Red;
                            test.ToolTip = "Rendez Vous Important";
                        }
                        switch (rdv.Date.ToString().Substring(11, 2))
                        {
                            case "07":
                                ListeRDV7.Items.Add(test);
                                _7.Background = SelectBrush;
                            break;
                            case "08":
                                ListeRDV8.Items.Add(test);
                                _8.Background = SelectBrush;
                                break;
                            case "09":
                                ListeRDV9.Items.Add(test);
                                _9.Background = SelectBrush;
                                break;
                            case "10":
                                ListeRDV10.Items.Add(test);
                                _10.Background = SelectBrush;
                                break;
                            case "11":
                                ListeRDV11.Items.Add(test);
                                _11.Background = SelectBrush;
                                break;
                            case "12":
                                ListeRDV12.Items.Add(test);
                                _12.Background = SelectBrush;
                                break;
                            case "13":
                                ListeRDV13.Items.Add(test);
                                _13.Background = SelectBrush;
                                break;
                            case "14":
                                ListeRDV14.Items.Add(test);
                                _14.Background = SelectBrush;
                                break;
                            case "15":
                                ListeRDV15.Items.Add(test);
                                _15.Background = SelectBrush;
                                break;
                            case "16":
                                ListeRDV16.Items.Add(test);
                                _16.Background = SelectBrush;
                                break;
                            case "17":
                                ListeRDV17.Items.Add(test);
                                _17.Background = SelectBrush;
                                break;
                            default: break;
                        }
                    }
                }
            }
        }
    }
}
