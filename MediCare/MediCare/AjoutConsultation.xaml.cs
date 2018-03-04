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
    /// Logique d'interaction pour Consultation.xaml
    /// </summary>
    public partial class AjoutConsultation : Window
    {
        public AjoutConsultation()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


        Traite TraitementEnreg = new Traite();


        private void Add_Consultation_Click(object sender, RoutedEventArgs e)
        {
            Consult consultation = new Consult();
            try
            {

                List<string> radioList = new List<string> { radioT.Text };
                Traite TraitementEnreg = new Traite(doseT.Text, indicationT.Text, medicamentT.Text);
                List<Traite> traitementList = new List<Traite> { TraitementEnreg };
                consultation.AddConsult(nomPatientT.Text, prenomPatientT.Text, nomMedecinT.Text, prenomMedecinT.Text, DateTime.Parse(dateT.Text), diagnosticT.Text, descriptionT.Text, certificatT.Text, lettreT.Text, scannerT.Text, bilanT.Text, radioList, traitementList);
                MessageBox.Show("Rendez-vous ajouté avec succés !");
            }
            catch (Exception)
            {
                MessageBox.Show("Une erreur s'est produite !");
            }
        }

        private void radioT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
