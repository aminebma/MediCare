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
    /// Logique d'interaction pour ModifMed.xaml
    /// </summary>
    public partial class ModifMed : Window
    {
        Grid GridAppelant;
        Grid SelectionGrid;

        public ModifMed()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        Medecin2 med = new Medecin2();

        public Grid SetGridAppelant
        {
            get { return GridAppelant; }
            set { GridAppelant = value; }
        }

        public Grid SetSelectionGrid
        {
            get { return SelectionGrid; }
            set { SelectionGrid = value; }
        }

        Window fenetrePrincipale;
        public Window SetFenetrePrincipale
        {
            get { return fenetrePrincipale; }
            set { fenetrePrincipale = value; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool verif= med.VerifMed(username2.Text, Password.Password);
            if ( verif == false)
            {
                MessageBox.Show("Vos données sont erronées ! Veuillez les resaisir  ");
            }
            else
            {
                GridAppelant.Children.Clear();
                ModifMed_2 usc = new ModifMed_2(username2.Text);
                usc.SetGridAppelant = SelectionGrid;
                usc.SetFenetrePrincipale = fenetrePrincipale;
                GridAppelant.Children.Add(usc);
                this.Close();
            }
        }
    }
}
