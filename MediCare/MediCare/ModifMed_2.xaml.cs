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
    /// Logique d'interaction pour ModifMed_2.xaml
    /// </summary>
    public partial class ModifMed_2 : UserControl
    {
        Grid GridAppelant;
        public ModifMed_2()
        {
            InitializeComponent();
        }

        public Grid SetGridAppelant
        {
            get { return GridAppelant; }
            set { GridAppelant = value; }
        }

        Window fenetrePrincipale;
        public Window SetFenetrePrincipale
        {
            get { return fenetrePrincipale; }
            set { fenetrePrincipale = value; }
        }

        Medecin2 medd = new Medecin2();
         
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (medd.ModifMed(user.Text, pass.Password, npass.Password)) MessageBox.Show("MotDePasse modifié avec succès !");;
            GridAppelant.Children.Clear();
            MonCompte usc = new MonCompte();
            usc.SetFenetrePrincipale = fenetrePrincipale;
            GridAppelant.Children.Add(usc);
        }
    }
}
