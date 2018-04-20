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
    /// Logique d'interaction pour MonCompte.xaml
    /// </summary>
    public partial class MonCompte : UserControl
    {
        Window fenetrePrincipale;

        public MonCompte()
        {
            InitializeComponent();
            IQueryable<Personne> infoMed= (from personne in Globals.DataClass.Personne
                                            where personne.nom == Globals.NomMedecin && personne.prenom == Globals.PrenomMedecin
                                            select personne);
           
            if (infoMed.Count()!=0)
            {
                Personne mdc = infoMed.First<Personne>();
                NomMed.Content ="Nom : \n" + mdc.nom;
                PrenomMed.Content ="Prenom : \n"+ mdc.prenom;
                Adresse.Content ="Adresse : \n"+ mdc.adresse;
                IQueryable<Medecin> mail = (from medecin in Globals.DataClass.Medecin
                                            where medecin.IdPersonne == mdc.Id orderby medecin.Id descending
                                            select medecin);
                Medecin med = mail.First();
                email.Content ="Email : \n"+ med.email;
                NomMed.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                PrenomMed.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                Adresse.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                email.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            }
           
        }

        public Window SetFenetrePrincipale
        {
            get { return fenetrePrincipale; }
            set { fenetrePrincipale = value; }
        }

        private void ModifMed_Click(object sender, RoutedEventArgs e)
        {
            ModifMed wndw = new ModifMed();
            wndw.Show();
            wndw.SetGridAppelant = this.GridDroit;
            wndw.SetFenetrePrincipale = fenetrePrincipale;
            wndw.SetSelectionGrid = (Grid)this.Parent;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LogOutPop wndw = new LogOutPop();
            wndw.setCreatingForm = fenetrePrincipale;
            wndw.Show();
        }

        private void Historique_Click(object sender, RoutedEventArgs e)
        {
            GridDroit.Children.Clear();
            GridDroit.Children.Add(new Historique());
            
        }

        private void MonHistorique_Click(object sender, RoutedEventArgs e)
        {
            GridDroit.Children.Clear();
            GridDroit.Children.Add(new HistoriqueMedecin());
        }
    }
}
