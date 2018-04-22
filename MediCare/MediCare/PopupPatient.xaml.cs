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
    /// Logique d'interaction pour PopupPatient.xaml
    /// </summary>
    public partial class PopupPatient : Window
    {
        Grid GridAppelant;

        public PopupPatient()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }
        PersonneClasse pers = new PersonneClasse();

        public Grid SetGridAppelant
        {
            get { return GridAppelant; }
            set { GridAppelant = value; }
        }

        private void Oui_Click(object sender, RoutedEventArgs e)
        {
            GridAppelant.Children.Clear();
            GridAppelant.Children.Add(new FichePatient());
            this.Close();
        }

        private void Non_Click(object sender, RoutedEventArgs e)
        {        
            GridAppelant.Children.Clear();
            GridAppelant.Children.Add(new PatientNonExistant());
            this.Close();
        }
    }
}
