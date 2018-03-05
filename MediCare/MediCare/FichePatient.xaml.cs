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
using System.Windows.Shapes;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pourFichePatient.xaml
    /// </summary>
    public partial class FichePatient : Window
    {
        public FichePatient()
        {
            InitializeComponent();
        }
        PersonneClasse pers = new PersonneClasse();

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {

            pers.AddPatientPersonne(Nom.Text, Prenom.Text, Date_naiss.Text, Adresse.Text, Num_tel.Text, Sexe.Text, Taille.Text, Poids.Text, Groupage.Text, Maladie.Text, Etat_santé.Text);
            MessageBox.Show("Le patient a été inséré ! ");
        }
    }
}
