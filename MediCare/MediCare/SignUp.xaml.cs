using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Globals.CurrentDirectoryPath = System.IO.Directory.GetCurrentDirectory();
            if (!System.IO.File.Exists($@"{Globals.CurrentDirectoryPath}\\MCDatabase.mdf"))
            {
                System.IO.File.Copy($@"{Globals.CurrentDirectoryPath}\\restauration\\MCDatabase.mdf", $@"{Globals.CurrentDirectoryPath}\\MCDatabase.mdf", true);
                MessageBox.Show("La base de données a rencontré un problème, la version la plus récente a été restaurée");
            }
            Globals.DataClass = new MCDataClassDataContext($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True");
        }

        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal main = new MenuPrincipal();
            Medecin2 MED = new Medecin2();
            InscriptionMedecin t = new InscriptionMedecin();
            this.Close();
            t.Show();
        }

        private void logBtn_Click(object sender, RoutedEventArgs e)
        {
            Medecin2 med = new Medecin2();
            if (med.VerifMed(nom.Text, password.Password))
            {
                Personne pers = new Personne();
                pers = med.RechercherMedecin(nom.Text);
                Globals.NomMedecin = pers.nom;
                Globals.PrenomMedecin = pers.prenom;
                Globals.AdresseMedecin = pers.adresse;
                Globals.num = (int)pers.telephone;
                Medecin medc = (from medecin in Globals.DataClass.Medecin
                                where medecin.IdPersonne == pers.Id
                                select medecin).First();
                Globals.MailMedecin = medc.email;
                Globals.codeMedecin = medc.code;
                Globals.fax = (int)medc.fax;
                Globals.numMobile = (int)medc.numCab;
                Globals.specialite = medc.specialite;
                MenuPrincipal main = new MenuPrincipal();
                main.Show();
                Globals.ListPatients = (from patient in Globals.DataClass.Patient
                                        join personne in Globals.DataClass.Personne on patient.IdPersonne equals personne.Id
                                        select personne).ToList<Personne>();
                Globals.ListMedicaments = (from medicament in Globals.DataClass.Medicaments
                                           select medicament).ToList<Medicaments>();

                this.Close();
            }
            else Dialog.IsOpen = true;
        }

        private void signUpGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Medecin2 med = new Medecin2();
                if (med.VerifMed(nom.Text, password.Password))
                {
                    Personne pers = new Personne();
                    pers = med.RechercherMedecin(nom.Text);
                    Globals.NomMedecin = pers.nom;
                    Globals.PrenomMedecin = pers.prenom;
                    Globals.AdresseMedecin = pers.adresse;
                    Globals.num = (int)pers.telephone;
                    Medecin medc = (from medecin in Globals.DataClass.Medecin
                                    where medecin.IdPersonne == pers.Id
                                    select medecin).First();
                    Globals.MailMedecin = medc.email;
                    Globals.codeMedecin = medc.code;
                    Globals.fax = (int)medc.fax;
                    Globals.numMobile = (int)medc.numCab;
                    Globals.specialite = medc.specialite;
                    MenuPrincipal main = new MenuPrincipal();
                    main.Show();
                    Globals.ListPatients = (from patient in Globals.DataClass.Patient
                                            join personne in Globals.DataClass.Personne on patient.IdPersonne equals personne.Id
                                            select personne).ToList<Personne>();
                    Globals.ListMedicaments = (from medicament in Globals.DataClass.Medicaments
                                               select medicament).ToList<Medicaments>();

                    this.Close();
                }
                else Dialog.IsOpen = true;
            }
        }
    }
}