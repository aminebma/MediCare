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
    public partial class PopupPatient : UserControl
    {
        public PopupPatient()
        {
            InitializeComponent();

        }
        PersonneClasse pers = new PersonneClasse();

        private void Oui_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            UserControl usc = new FichePatient();
            parent.Children.Clear();
            parent.Children.Add(usc);
        }

        private void Non_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            pers.AddPatientPersonne(Globals.NomPatient,Globals.PrenomPatient,null,null,null,true,null,null,null,null,null);
            //UserControl usc = new AjoutConsultation();
            //parent.Children.Add(usc);
        }
    }
}
