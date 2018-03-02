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
    /// Logique d'interaction pour AjoutRdvPro.xaml
    /// </summary>
    public partial class AjoutRdvPro : Window
    {
        public AjoutRdvPro()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        List<string> caract = new List<string> {"," , "." , ":" , ";" , "!" , "*" , "$" , "/" , "?" , "+" , "_" , "=" , "§" , "<" , ">" , "{" , "}" , "[" , "]" , "(" , ")" , "'" , "\"" , "&" , "²" , "@" , "|" , "#"};
        List<string> longMois = new List<string> {"Janvier","Mars","Mai","Juillet","Aout","Octobre","Décembre"};
        List<string> shortMois = new List<string> {"Avril","Juin","Septembre","Novembre"};
        Agenda rdv = new Agenda();

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {            
            bool checkDate=false,
                 checkTime = false;
            int realHour=0;
            if (jourT.Text == "" || moisBox.Text == "" || anneeT.Text == "" || heureBox.Text=="" || minutesT.Text == "" || ampmBox.Text=="" || prenomPatientT.Text == "" || nomPatientT.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (jourT.Text == "") jourL.Foreground = Brushes.Red; else jourL.Foreground = Brushes.Black;
                if (moisBox.Text == "") moisL.Foreground = Brushes.Red; else moisL.Foreground = Brushes.Black;
                if (anneeT.Text == "") anneeL.Foreground = Brushes.Red; else anneeL.Foreground = Brushes.Black;
                if (heureBox.Text == "") heuresL.Foreground = Brushes.Red; else heuresL.Foreground = Brushes.Black;
                if (minutesT.Text == "") minutesL.Foreground = Brushes.Red; else minutesL.Foreground = Brushes.Black;
                if (ampmBox.Text == "") ampmL.Foreground = Brushes.Red; else ampmL.Foreground = Brushes.Black;
                if (prenomPatientT.Text == "") prenomPatientL.Foreground = Brushes.Red; else prenomPatientL.Foreground = Brushes.Black;
                if (nomPatientT.Text == "") nomPatientL.Foreground = Brushes.Red; else nomPatientL.Foreground = Brushes.Black;
            }
            else 
            {
                realHour = (ampmBox.Text == "AM") ? Int32.Parse(heureBox.Text) : (Int32.Parse(heureBox.Text)+12);
                checkDate = DateCheck(realHour);
                checkTime = TimeCheck(realHour);
                if (checkDate && checkTime)
                {
                    moisL.Foreground = Brushes.Black;
                    minutesL.Foreground = Brushes.Black;
                    anneeL.Foreground = Brushes.Black;
                    try
                    {
                        rdv.AddRdv(DateTime.Parse(jourT.Text + "/" + rdv.ConvertMoisToNum(moisBox.Text) + "/" + anneeT.Text + " " + realHour + ":" + minutesT.Text + ":00"), 1, nomPatientT.Text, prenomPatientT.Text, (bool)isImportant.IsChecked, notesT.Text);
                        MessageBox.Show("Rendez-vous ajouté avec succés !");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Database connexion error");
                    }
                }
                else if (!checkDate)
                {
                    if(YearCheck())
                    {
                        MessageBox.Show("Année invalide");
                        anneeL.Foreground = Brushes.Red;
                    }
                    else
                    {
                        MessageBox.Show("Jour invalide");
                        jourL.Foreground = Brushes.Red;
                    }
                }
                else
                {
                    if(Int16.Parse(minutesT.Text)>59)
                    {
                        MessageBox.Show("Minutes invalide");
                        minutesL.Foreground = Brushes.Red;
                    }
                    else
                    {
                        MessageBox.Show("Heure invalide");
                        heuresL.Foreground = Brushes.Red;
                    }
                }
            }
        }

        private void jourT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!NumberCheck(e.Text)) e.Handled = true;
        }

        private void anneeT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!NumberCheck(e.Text)) e.Handled = true;
        }

        private void minutesT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!NumberCheck(e.Text)) e.Handled = true;
        }

        private void nomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
        }

        private void prenomPatientT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (NumberCheck(e.Text) || caract.Contains(e.Text)) e.Handled = true;
        }

        private bool NumberCheck(string verifText)
        {
            return int.TryParse(verifText, out int result);
        }

        private bool DateCheck(int realhour)
        {
            short verifJour = Int16.Parse(jourT.Text),
                  verifAnnee = Int16.Parse(anneeT.Text);
            int verifMois= rdv.ConvertMoisToNum(moisBox.Text);
            DateTime today=DateTime.MinValue;
            try
            {
                today=DateTime.Parse(jourT.Text + "/" + verifMois + "/" + anneeT.Text + " " + realhour + ":" + minutesT.Text + ":00");
            }
            catch (Exception)
            {
                return false;
            }
            return (verifJour < 1 || (longMois.Contains(moisBox.Text) && verifJour > 31) || (shortMois.Contains(moisBox.Text) && verifJour > 30) || (today < DateTime.Now)) ? false : true;
        }

        private bool TimeCheck(int realHour)
        {
            short verifMinutes = Int16.Parse(minutesT.Text);
            return (DateTime.Now.Hour > realHour || (DateTime.Now.Hour == realHour && DateTime.Now.Minute> Int16.Parse(minutesT.Text)) || verifMinutes < 0 || verifMinutes > 59);
        }

        private bool YearCheck()
        {
            return (Int16.Parse(anneeT.Text) < DateTime.Now.Year);
        }
    }
}
