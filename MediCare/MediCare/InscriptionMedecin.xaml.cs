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
    /// Logique d'interaction pour InscriptionMedecin.xaml
    /// </summary>
    public partial class InscriptionMedecin : Window
    {
        public InscriptionMedecin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        
        Medecin2 med = new Medecin2();
        Regex charControl = new Regex(@"[A-Za-z]+");
        Regex intControl = new Regex(@"[0-9]+");

        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nom.Text == "" || prenom.Text == "" || adresse.Text == "" || num_tel.Text == "" || sex.Text == "" || clef.Text=="" || username.Text == "" || password.Password == "" || email.Text == "" || date_naiss.Text == "")
            {
                MessageBox.Show("Veuillez remplir toutes les informations!");
                if (nom.Text == "") nom.BorderBrush = Brushes.Red; else nom.BorderBrush = Brushes.Black;
                if (prenom.Text == "") prenom.BorderBrush = Brushes.Red; else prenom.BorderBrush = Brushes.Black;
                if (adresse.Text == "") adresse.BorderBrush = Brushes.Red; else adresse.BorderBrush = Brushes.Black;
                if (num_tel.Text == "") num_tel.BorderBrush = Brushes.Red; else num_tel.BorderBrush = Brushes.Black;
                if (date_naiss.Text == "") date_naiss.BorderBrush = Brushes.Red; else date_naiss.BorderBrush = Brushes.Black;
                if (sex.Text == "") sex.BorderBrush = Brushes.Red; else sex.BorderBrush = Brushes.Black;
                if (email.Text == "") email.BorderBrush = Brushes.Red; else email.BorderBrush = Brushes.Black;
                if (clef.Text == "") clef.BorderBrush = Brushes.Red; else clef.BorderBrush = Brushes.Black;
                if (username.Text == "") username.BorderBrush = Brushes.Red; else username.BorderBrush = Brushes.Black;
                if (password.Password == "") password.BorderBrush = Brushes.Red; else password.BorderBrush = Brushes.Black;
            }
            else if(checkDate())
            {
                if(clef.Text==Encrypt(nom.Text.ToUpper()+prenom.Text.ToUpper()+date_naiss.Text))
                {
                    if (num_tel.Text.Length == 10)
                    {
                        if (!med.RechercherMedecinAdd(username.Text))
                        {
                            Globals.NomMedecin = nom.Text;
                            Globals.PrenomMedecin = prenom.Text;
                            med.AddMed(nom.Text, prenom.Text, DateTime.Parse(date_naiss.Text), adresse.Text, num_tel.Text, sex.Text, clef.Text, username.Text, password.Password, email.Text);
                            MessageBox.Show("Le medecin a été inséré ! ");
                            MenuPrincipal t = new MenuPrincipal();
                            Globals.ListPatients = (from patient in Globals.DataClass.Patient
                                                    join personne in Globals.DataClass.Personne on patient.IdPersonne equals personne.Id
                                                    select personne).ToList<Personne>();
                            t.Show();
                            this.Close();
                        }
                        else
                        {
                            bool exist = true;
                            int i = 1;
                            string userSuggest = username.Text;
                            while (exist)
                            {
                                userSuggest = username.Text + i;
                                exist = med.RechercherMedecinAdd(userSuggest);
                                i++;
                            }
                            MessageBox.Show("Nom d'utilisateur existant, essayez: \n" + userSuggest);
                        }
                    }
                    else MessageBox.Show("Votre numero est incorrect ");
                }
            }
            else
            {
                MessageBox.Show("Veuillez rentrer une date valide");
                date_naiss.BorderBrush = Brushes.Red;
            }
        }

        private bool checkDate()
        {
            return (DateTime.Parse(date_naiss.Text) < DateTime.Now) ? true : false;
        }

        private void nom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void prenom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void num_tel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!intControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password.Password.Length > 0) validationMdp.Visibility = Visibility.Visible; else validationMdp.Visibility = Visibility.Hidden;
            if (password.Password.Length > 5) leng.Foreground = Brushes.Green; else leng.Foreground = Brushes.Black;
            if (password.Password.Any(c => char.IsUpper(c))) maj.Foreground = Brushes.Green; else maj.Foreground = Brushes.Black;
            if (password.Password.Any(c => char.IsLower(c))) min.Foreground = Brushes.Green; else min.Foreground = Brushes.Black;
            if (password.Password.Any(c => char.IsDigit(c))) chiff.Foreground = Brushes.Green; else chiff.Foreground = Brushes.Black;
            if (password.Password == passwordConfirm.Password) passValidation.Foreground = Brushes.Green; else passValidation.Foreground = Brushes.Black;
            if (leng.Foreground == Brushes.Green && maj.Foreground == Brushes.Green && min.Foreground == Brushes.Green && chiff.Foreground == Brushes.Green && passValidation.Foreground == Brushes.Green) signUpBtn.IsEnabled = true; else signUpBtn.IsEnabled = false;
        }

        private void passwordConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password.Password == passwordConfirm.Password) passValidation.Foreground = Brushes.Green; else passValidation.Foreground = Brushes.Black;
            if (leng.Foreground == Brushes.Green && maj.Foreground == Brushes.Green && min.Foreground == Brushes.Green && chiff.Foreground == Brushes.Green && passValidation.Foreground == Brushes.Green) signUpBtn.IsEnabled = true; else signUpBtn.IsEnabled = false;
        }

        private void date_naiss_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        public string Encrypt(string word)
        {
            byte[] passBytes = Encoding.Unicode.GetBytes(word);
            string encryptedPass = Convert.ToBase64String(passBytes);
            return encryptedPass;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter && signUpBtn.IsEnabled==true)
            {
                if (nom.Text == "" || prenom.Text == "" || adresse.Text == "" || num_tel.Text == "" || sex.Text == "" || clef.Text == "" || username.Text == "" || password.Password == "" || email.Text == "" || date_naiss.Text == "")
                {
                    MessageBox.Show("Veuillez remplir toutes les informations!");
                    if (nom.Text == "") nom.BorderBrush = Brushes.Red; else nom.BorderBrush = Brushes.Black;
                    if (prenom.Text == "") prenom.BorderBrush = Brushes.Red; else prenom.BorderBrush = Brushes.Black;
                    if (adresse.Text == "") adresse.BorderBrush = Brushes.Red; else adresse.BorderBrush = Brushes.Black;
                    if (num_tel.Text == "") num_tel.BorderBrush = Brushes.Red; else num_tel.BorderBrush = Brushes.Black;
                    if (date_naiss.Text == "") date_naiss.BorderBrush = Brushes.Red; else date_naiss.BorderBrush = Brushes.Black;
                    if (sex.Text == "") sex.BorderBrush = Brushes.Red; else sex.BorderBrush = Brushes.Black;
                    if (email.Text == "") email.BorderBrush = Brushes.Red; else email.BorderBrush = Brushes.Black;
                    if (clef.Text == "") clef.BorderBrush = Brushes.Red; else clef.BorderBrush = Brushes.Black;
                    if (username.Text == "") username.BorderBrush = Brushes.Red; else username.BorderBrush = Brushes.Black;
                    if (password.Password == "") password.BorderBrush = Brushes.Red; else password.BorderBrush = Brushes.Black;
                }
                else if (checkDate())
                {
                    if (clef.Text == Encrypt(nom.Text.ToUpper() + prenom.Text.ToUpper() + date_naiss.Text))
                    {
                        if (num_tel.Text.Length == 10)
                        {
                            if (!med.RechercherMedecinAdd(username.Text))
                            {
                                Globals.NomMedecin = nom.Text;
                                Globals.PrenomMedecin = prenom.Text;
                                med.AddMed(nom.Text, prenom.Text, DateTime.Parse(date_naiss.Text), adresse.Text, num_tel.Text, sex.Text, clef.Text, username.Text, password.Password, email.Text);
                                MessageBox.Show("Le medecin a été inséré ! ");
                                MenuPrincipal t = new MenuPrincipal();
                                Globals.ListPatients = (from patient in Globals.DataClass.Patient
                                                        join personne in Globals.DataClass.Personne on patient.IdPersonne equals personne.Id
                                                        select personne).ToList<Personne>();
                                t.Show();
                                this.Close();
                            }
                            else
                            {
                                bool exist = true;
                                int i = 1;
                                string userSuggest = username.Text;
                                while (exist)
                                {
                                    userSuggest = username.Text + i;
                                    exist = med.RechercherMedecinAdd(userSuggest);
                                    i++;
                                }
                                MessageBox.Show("Nom d'utilisateur existant, essayez: \n" + userSuggest);
                            }
                        }
                        else MessageBox.Show("Votre numero est incorrect ");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez rentrer une date valide");
                    date_naiss.BorderBrush = Brushes.Red;
                }
            }
        }
    }
}

     

