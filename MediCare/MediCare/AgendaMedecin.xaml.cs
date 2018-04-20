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
            _0.Background = BackBrush;
            _1.Background = BackBrush;
            _2.Background = BackBrush;
            _3.Background = BackBrush;
            _4.Background = BackBrush;
            _5.Background = BackBrush;
            _6.Background = BackBrush;
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
            _18.Background = BackBrush;
            _19.Background = BackBrush;
            _20.Background = BackBrush;
            _21.Background = BackBrush;
            _22.Background = BackBrush;
            _23.Background = BackBrush;
            ListeRDV0.Items.Clear();
            ListeRDV1.Items.Clear();
            ListeRDV2.Items.Clear();
            ListeRDV3.Items.Clear();
            ListeRDV4.Items.Clear();
            ListeRDV5.Items.Clear();
            ListeRDV5.Items.Clear();
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
            ListeRDV18.Items.Clear();
            ListeRDV19.Items.Clear();
            ListeRDV20.Items.Clear();
            ListeRDV21.Items.Clear();
            ListeRDV22.Items.Clear();
            ListeRDV23.Items.Clear();
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
                if (rdv.IdPatient != 0)
                { 
                    if (patientRDV.Count() != 0)
                    {
                    TextBox test = new TextBox();
                        agenda.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                        test.AcceptsReturn = true;
                        test.Width = 435;
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
                            case "00":
                                ListeRDV0.Items.Add(test);
                                _0.Background = SelectBrush;
                                break;
                            case "01":
                                ListeRDV1.Items.Add(test);
                                _1.Background = SelectBrush;
                                break;
                            case "02":
                                ListeRDV2.Items.Add(test);
                                _2.Background = SelectBrush;
                                break;
                            case "03":
                                ListeRDV3.Items.Add(test);
                                _3.Background = SelectBrush;
                                break;
                            case "04":
                                ListeRDV4.Items.Add(test);
                                _4.Background = SelectBrush;
                                break;
                            case "05":
                                ListeRDV5.Items.Add(test);
                                _5.Background = SelectBrush;
                                break;
                            case "06":
                                ListeRDV6.Items.Add(test);
                                _6.Background = SelectBrush;
                                break;
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
                            case "18":
                                ListeRDV18.Items.Add(test);
                                _18.Background = SelectBrush;
                                break;
                            case "19":
                                ListeRDV19.Items.Add(test);
                                _19.Background = SelectBrush;
                                break;
                            case "20":
                                ListeRDV20.Items.Add(test);
                                _20.Background = SelectBrush;
                                break;
                            case "21":
                                ListeRDV21.Items.Add(test);
                                _21.Background = SelectBrush;
                                break;
                            case "22":
                                ListeRDV22.Items.Add(test);
                                _22.Background = SelectBrush;
                                break;
                            case "23":
                                ListeRDV23.Items.Add(test);
                                _23.Background = SelectBrush;
                                break;
                            default: break;
                        }
                    }
                }
                else
                {
                    TextBox test = new TextBox();
                    agenda.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    test.AcceptsReturn = true;
                    test.Width = 435;
                    test.Foreground = Brushes.White;
                    test.IsReadOnly = true;
                    test.HorizontalContentAlignment = HorizontalAlignment.Center;
                    test.VerticalContentAlignment = VerticalAlignment.Center;
                    test.Text = "Rendez Vous personnel : " + rdv.Note + "\n Heure" + rdv.Date.ToString().Substring(11, 8);
                    switch (rdv.Date.ToString().Substring(11, 2))
                    {
                        case "00":
                            ListeRDV0.Items.Add(test);
                            _0.Background = SelectBrush;
                            break;
                        case "01":
                            ListeRDV1.Items.Add(test);
                            _1.Background = SelectBrush;
                            break;
                        case "02":
                            ListeRDV2.Items.Add(test);
                            _2.Background = SelectBrush;
                            break;
                        case "03":
                            ListeRDV3.Items.Add(test);
                            _3.Background = SelectBrush;
                            break;
                        case "04":
                            ListeRDV4.Items.Add(test);
                            _4.Background = SelectBrush;
                            break;
                        case "05":
                            ListeRDV5.Items.Add(test);
                            _5.Background = SelectBrush;
                            break;
                        case "06":
                            ListeRDV6.Items.Add(test);
                            _6.Background = SelectBrush;
                            break;
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
                        case "18":
                            ListeRDV18.Items.Add(test);
                            _18.Background = SelectBrush;
                            break;
                        case "19":
                            ListeRDV19.Items.Add(test);
                            _19.Background = SelectBrush;
                            break;
                        case "20":
                            ListeRDV20.Items.Add(test);
                            _20.Background = SelectBrush;
                            break;
                        case "21":
                            ListeRDV21.Items.Add(test);
                            _21.Background = SelectBrush;
                            break;
                        case "22":
                            ListeRDV22.Items.Add(test);
                            _22.Background = SelectBrush;
                            break;
                        case "23":
                            ListeRDV23.Items.Add(test);
                            _23.Background = SelectBrush;
                            break;
                        default: break;
                    }
                }

            }
            }
        }
    }

