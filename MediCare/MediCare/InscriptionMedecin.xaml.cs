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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (num_tel.Text.Length == 10)
            {
                Globals.NomMedecin = nom.Text;
                Globals.PrenomMedecin = prenom.Text;
                med.AddMed(nom.Text, prenom.Text, DateTime.Parse(date_naiss.Text), adresse.Text, num_tel.Text, sex.Text, clef.Text, username.Text, password.Text);
                MessageBox.Show("Le medecin a été inséré ! ");
                MenuPrincipal t = new MenuPrincipal();
                Globals.ListPatients = (from patient in Globals.DataClass.Patient
                                        join personne in Globals.DataClass.Personne on patient.IdPersonne equals personne.Id
                                        select personne).ToList<Personne>();
                t.Show();
                this.Close();
            }
            else MessageBox.Show("Votre numero est incorrect ");
        }

    }
    }

     

