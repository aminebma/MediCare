using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour SupprimerPatient.xaml
    /// </summary>
    public partial class SupprimerPatient : UserControl
    {

        List<specCheckBox> listChecked = new List<specCheckBox>();
        List<specCheckBox> listComplete = new List<specCheckBox>();

        PersonneClasse personne = new PersonneClasse();
        Medecin2 patients = new Medecin2();
        List<Patients> list;
        public SupprimerPatient()
        {
            InitializeComponent();
            list = patients.DossiersMedical();
            if (list.Count() != 0)
            {
                foreach (Patients p in list)
                {
                    specCheckBox expSuivi = new specCheckBox();
                    expSuivi.Content = p.Nom + " " + p.Prenom;
                    expSuivi.nom = p.Nom;
                    expSuivi.prenom = p.Prenom;
                    listComplete.Add(expSuivi);
                    StackSuivi.Items.Add(expSuivi);
                }
                
            }
            
            
           

        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (specCheckBox p in listComplete)
            {
                if (p.IsChecked.Value == true)
                {
                    listChecked.Add(p);
                }
            }
            if (listChecked.Count != 0)
            {
                foreach (specCheckBox p in listChecked)
                {
                    personne.SuppPatient(p.nom, p.prenom);
                    MessageBox.Show("Patient supprimé ");

                }
            }
            else
            {
                MessageBox.Show("Non !");
                Supprimer.IsEnabled = false;
            }
            
        }
    }
   
    public class specCheckBox : CheckBox
    {
        public string nom { get; set; }
        public string prenom { get; set; }
    }
}
