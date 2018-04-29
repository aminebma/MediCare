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
    public partial class SupprimerPatient : UserControl
    {
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
                    if (Globals.NomPatient == p.Nom && Globals.PrenomPatient == p.Prenom)
                    {
                        TextBlock expSuivi = new TextBlock();
                        expSuivi.Text ="Nom : " + p.Nom + "    Prénom : " + p.Prenom + "\n Date de naissance : " + p.Datenaiss.Day + "/" + p.Datenaiss.Month +"/" +p.Datenaiss.Year+ "\n Taille : " + p.Taille +" cm "+ "\n Poids : " + p.Poids +" Kg "+ "\n Sexe : " + p.Sexe + "\n Numéro de téléphone : 0" + p.Numtel + "\n Maladie : " + p.Maladie + "\n Groupage : " + p.Groupage + "\n Etat de santé actuel : " + p.EtatSante + "\n Adresse : " + p.Adresse;
                        StackSuivi.Children.Add(expSuivi);
                    }
                }
                
            }
            
            
           

        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //foreach (specCheckBox p in listComplete)
            //{
            //    if (p.IsChecked.Value == true)
            //    {
            //        listChecked.Add(p);
            //    }
            //}
            //if (listChecked.Count != 0)
            //{
            //    foreach (specCheckBox p in listChecked)
            //    {
                 personne.SuppPatient(Globals.NomPatient, Globals.PrenomPatient);
            //        MessageBox.Show("Patient supprimé ");

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Non !");
            //    Supprimer.IsEnabled = false;
            //}
            
        }
    }
   
    
}
