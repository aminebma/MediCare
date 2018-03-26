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
using System.Text.RegularExpressions;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour AjoutRdvPro.xaml
    /// </summary>
    public partial class AjoutRdvPro : Window
    {
        MCDataClassDataContext dataClass = new MCDataClassDataContext($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True");
        List<Personne> listPatients,listPatientsTmp;
        
        public AjoutRdvPro()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            listPatients = (from patient in dataClass.Patient
                           join personne in dataClass.Personne on patient.IdPersonne equals personne.Id
                           select personne).ToList<Personne>();
            foreach(Personne patient in listPatients)
                nomPatientT.Items.Add(patient.nom+" "+patient.prenom); 
        }

        //List<string> caract = new List<string> { ",", ".", ":", ";", "!", "*", "$", "/", "?", "+", "_", "=", "§", "<", ">", "{", "}", "[", "]", "(", ")", "'", "\"", "&", "²", "@", "|", "#", "£", "µ", "%", "€", "¤" };
        Agenda rdv = new Agenda();
        List<Personne> patients;
        Regex charControl = new Regex(@"[A-Za-z]+");

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            bool checkDate = false,
                 checkTime = false;
            int realHour = 0;
            if (dateT.Text == "" || heureBox.Text == "" || minutesBox.Text == "" || ampmBox.Text == "" || prenomPatientT.Text == "" || nomPatientT.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (dateT.Text == "") dateL.Foreground = Brushes.Red; else dateL.Foreground = Brushes.Black;
                if (heureBox.Text == "") heuresL.Foreground = Brushes.Red; else heuresL.Foreground = Brushes.Black;
                if (minutesBox.Text == "") minutesL.Foreground = Brushes.Red; else minutesL.Foreground = Brushes.Black;
                if (ampmBox.Text == "") ampmL.Foreground = Brushes.Red; else ampmL.Foreground = Brushes.Black;
                if (prenomPatientT.Text == "") prenomPatientL.Foreground = Brushes.Red; else prenomPatientL.Foreground = Brushes.Black;
                if (nomPatientT.Text == "") nomPatientL.Foreground = Brushes.Red; else nomPatientL.Foreground = Brushes.Black;
            }
            else
            {
                realHour = (ampmBox.Text == "AM") ? Int32.Parse(heureBox.Text) : (Int32.Parse(heureBox.Text) + 12);
                checkDate = DateCheck(realHour);
                checkTime = TimeCheck(realHour);
                if (checkDate && checkTime)
                {
                    minutesL.Foreground = Brushes.Black;
                    try
                    {
                        if (rdv.AddRdv(DateTime.Parse(dateT.Text + " " + realHour + ":" + minutesBox.Text + ":00"), Globals.IdMedecin, nomPatientT.Text.ToUpper(), prenomPatientT.Text.ToUpper(), (bool)isImportant.IsChecked, notesT.Text)) MessageBox.Show("Rendez-vous ajouté avec succés !");
                        else MessageBox.Show("Vous avez déjà un rendez-vous à cette date");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Database error");
                    }
                }   
                else if (!checkTime)
                {
                    MessageBox.Show("Horaire invalide");
                    heuresL.Foreground = Brushes.Red;
                    minutesL.Foreground = Brushes.Red;
                }
                else if (!checkDate)
                {
                    MessageBox.Show("Veuillez rentrer une date valide");
                    dateL.Foreground = Brushes.Red;
                    minutesL.Foreground = Brushes.Red;
                    heuresL.Foreground = Brushes.Red;
                } 
            }
        }

        private void nomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //  if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void prenomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;            
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }
        
        //private bool NumberCheck(string verifText)
        //{
        //    return int.TryParse(verifText, out int result);
        //}

        private bool DateCheck(int realhour)
        {
            DateTime dateRdv = DateTime.Parse(dateT.Text + " " + realhour + ":" + minutesBox.Text + ":00");
            return (dateRdv < DateTime.Now) ? false : true;
        }

        private bool TimeCheck(int realHour)
        {
            DateTime dateRdv = DateTime.Parse(dateT.Text + " " + realHour + ":" + minutesBox.Text + ":00");
            return ( (DateTime.Now>dateRdv && DateTime.Now.Hour > realHour) || (DateTime.Now>dateRdv && DateTime.Now.Hour == realHour && DateTime.Now.Minute > Int16.Parse(minutesBox.Text))) ? false : true;
        }

        private void dateT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        //private void nomPatientT_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //if (nomPatientT.Text == "") patientsT.Text = "";
        //else
        //{
        //    if(prenomPatientT.Text == "") patients = rdv.RechercherPatientNom(nomPatientT.Text);
        //    else patients = rdv.RechercherPatient(nomPatientT.Text,prenomPatientT.Text);
        //    patientsT.Text = "";
        //    foreach (Personne p in patients)
        //    {
        //        patientsT.Text = patientsT.Text + "\n" + p.nom + " " + p.prenom;
        //    }
        //}  
        //}

        private void prenomPatientT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (prenomPatientT.Text == "") patientsT.Text = "";
            else
            {
                if (nomPatientT.Text == "") patients = rdv.RechercherPatientPrenom(prenomPatientT.Text);
                else patients = rdv.RechercherPatient(nomPatientT.Text, prenomPatientT.Text);
                patientsT.Text = "";
                foreach (Personne p in patients)
                {
                    patientsT.Text = patientsT.Text + "\n" + p.nom + " " + p.prenom;
                }
            }
        }

        private void nomPatientT_TextChanged(object sender, TextChangedEventArgs e)
        {
            listPatientsTmp = rdv.RechercherPatient(listPatients, nomPatientT.Text.ToUpper());
            nomPatientT.Items.Clear();
            foreach (Personne patient in listPatientsTmp)
            {
                nomPatientT.Items.Add(nomPatientT.Items.Add(patient.nom + " " + patient.prenom));
                if (nomPatientT.Items.Count != 0) nomPatientT.Items.RemoveAt(nomPatientT.Items.Count - 1);
            }
        }
    }
}
