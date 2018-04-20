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
        public void GenererRDVduJour(ListView ListeRDV)
        {
            IQueryable<RendezVous> NotifRdv = (from rendezVous in Globals.DataClass.RendezVous
                                               orderby rendezVous.Date
                                               select rendezVous);
            if (NotifRdv.Count() != 0)
            {
                foreach (RendezVous rdv in NotifRdv)
                {
                    if (string.Compare(rdv.Date.ToString().Substring(0, 10), DateTime.Today.ToString().Substring(0, 10)) == 0)
                    {
                        TextBox TextRDV = new TextBox();
                        TextRDV.Width = 270;
                        TextRDV.AcceptsReturn = true;
                        TextRDV.IsReadOnly = true;
                        TextRDV.HorizontalContentAlignment = HorizontalAlignment.Center;
                        TextRDV.VerticalContentAlignment = VerticalAlignment.Center;
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
                                string NomPatient = personneRDV.First().nom;
                                string PrenomPatient = personneRDV.First().prenom;
                                string heureRDV = rdv.Date.ToString().Substring(10, 9);
                                TextRDV.Text = " Heure : " + heureRDV + " \n Patient : " + NomPatient + " " + PrenomPatient + "\n Note :" + rdv.Note;
                                TextRDV.TextAlignment = TextAlignment.Center;
                                if (rdv.Important == true)
                                {
                                    TextRDV.Foreground = Brushes.Red;
                                    TextRDV.ToolTip = "Rendez Vous Important";
                                }

                                else
                                {
                                    Color color = (Color)ColorConverter.ConvertFromString("#FF42919E");
                                    TextRDV.Foreground = new SolidColorBrush(color);
                                }
                                ListeRDV.Items.Add(TextRDV);
                            }

                            else
                            {
                                string heureRDV = rdv.Date.ToString().Substring(10, 9);
                                TextRDV.Text = " Rendez vous personnel: \n " + rdv.Note + "\n Heure: " + heureRDV;
                                if (rdv.Important == true)
                                {
                                    TextRDV.Foreground = Brushes.Red;
                                    TextRDV.ToolTip = "Rendez Vous Important";
                                }
                                else
                                {
                                    Color color = (Color)ColorConverter.ConvertFromString("#FF42919E");
                                    TextRDV.Foreground = new SolidColorBrush(color);
                                }
                                ListeRDV.Items.Add(TextRDV);
                            }
                        }
                    }
                }
            }
        }
    }
}
