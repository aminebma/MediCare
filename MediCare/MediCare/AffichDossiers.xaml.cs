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
    /// Logique d'interaction pour AffichDossiers.xaml
    /// </summary>
    public partial class AffichDossiers : UserControl
    {
        PersonneClasse personne = new PersonneClasse();
        Medecin2 patients = new Medecin2();
        List<Patients> list;
        public AffichDossiers()
        {
            InitializeComponent();
            list = patients.DossiersMedical();
            if (list.Count() != 0)
            {
                foreach (Patients p in list)
                {
                    if (Globals.NomPatient == p.Nom )
                    {
                        TextBlock expSuivi = new TextBlock();
                        expSuivi.Text = "Nom : " + p.Nom + "    Prénom : " + p.Prenom + "\n    Date de naissance : " + p.Datenaiss + "\n    Taille : " + p.Taille + " cm " + "\n    Poids : " + p.Poids + " Kg " + "\n    Sexe : " + p.Sexe + "\n     Numéro de téléphone : 0" + p.Numtel + "\n     Maladie : " + p.Maladie + "\n    Groupage sanguin : " + p.Groupage + "\n    Etat de santé actuel : " + p.EtatSante + "\n    Adresse : " + p.Adresse;
                        StackSuivi.Children.Add(expSuivi);
                    }
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new Menu());
        }
    }
}
