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
    public partial class MonCompte : UserControl
    {
        Window fenetrePrincipale;

        public MonCompte()
        {
            InitializeComponent();
            NomMed.Text ="Nom : \n" + Globals.NomMedecin;
            PrenomMed.Text ="Prenom : \n"+ Globals.PrenomMedecin;
            email.Text ="Email : \n"+ Globals.MailMedecin;
            specialite.Text = "Spécialité : \n" + Globals.specialite;
            NomMed.HorizontalAlignment = HorizontalAlignment.Stretch;
            PrenomMed.HorizontalAlignment = HorizontalAlignment.Stretch;
            email.HorizontalAlignment = HorizontalAlignment.Stretch;
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
