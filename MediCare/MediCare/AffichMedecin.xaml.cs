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
    /// Logique d'interaction pour AffichMedecin.xaml
    /// </summary>
    public partial class AffichMedecin : UserControl
    {

        Medecin2 personne = new Medecin2();
        List<Medecins> list;

        public AffichMedecin()
        {
            InitializeComponent();
            list = personne.RechercheToutMedecin();
            if (list.Count() != 0)
            {
                
                foreach (Medecins p in list)
                {
                    if (Globals.NomMedecin == p.Nom)
                    {
                        Expander expSuivi = new Expander();
                        expSuivi.Header = "Nom du médecin :" + p.Nom + "\n Prénom du médecin :" + p.Prenom;
                        expSuivi.Content = " Date de naissance : " + p.Datenaiss + "\n Adresse :" + p.Adresse + "\n Numéro de télèphone : " + p.Numtel + "\n Sexe : " + p.Sexe;
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
