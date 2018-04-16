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
        string username;

        public ModifMed_2(string user)
        {
            InitializeComponent();
            username = user;
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
         
        private void validateBtn_Click(object sender, RoutedEventArgs e)
        {
            if(pass.Password != npass.Password)
            {
                MessageBox.Show("Les mots de passes doivent être identiques");
                pass.Password = "";
                pass.BorderBrush = Brushes.Red;
                npass.Password = "";
                npass.BorderBrush = Brushes.Red;
            }
            else
            {
                if (medd.ModifMed(username, pass.Password, npass.Password)) MessageBox.Show("MotDePasse modifié avec succès !"); ;
                GridAppelant.Children.Clear();
                MonCompte usc = new MonCompte
                {
                    SetFenetrePrincipale = fenetrePrincipale
                };
                GridAppelant.Children.Add(usc);
            }           
        }
    }
}
