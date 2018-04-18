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
    /// Logique d'interaction pour DossierPatient.xaml
    /// </summary>
    public partial class ToutPatients : UserControl
    {
         Medecin2 personne = new Medecin2();
        List<Patients> list;
        public ToutPatients()
        {
            InitializeComponent();
            list = personne.DossiersMedical();
            if (list.Count() != 0)
            {
                foreach (Patients p in list)
                {
                    Expander expSuivi = new Expander();
                    expSuivi.Header = "Nom du Patient :" + p.Nom + "\n Prénom du patient :" + p.Prenom;
                    expSuivi.Content = " Date de naissance : " + p.Datenaiss + "\n Taille :" + p.Taille + "\n Poids : " + p.Poids + "\n Sexe : " + p.Sexe + "\n Numéro de téléphone :" + p.Numtel + "\n Maladie :" + p.Maladie + "\n Groupage" + p.Groupage + "\n Etat de santé :" + p.EtatSante + "\n Adresse : " + p.Adresse;
                    StackSuivi.Children.Add(expSuivi);


                }
            }

        }
    }
}
