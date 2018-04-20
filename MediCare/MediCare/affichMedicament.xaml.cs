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
    /// Logique d'interaction pour affichMedicament.xaml
    /// </summary>
    public partial class affichMedicament : UserControl
    {
        List<Medicaments> list;
        Agenda personne = new Agenda();
        public affichMedicament()
        {
            InitializeComponent();
            list = personne.RechercherMedic(Globals.Medicament);

            if (list.Count() != 0)
            {
                foreach (Medicaments p in list)
                {

                    Expander expSuivi = new Expander();
                    expSuivi.Header = "Nom du Médicament :" + p.nom;
                    expSuivi.Content = " Dose : " + p.Dosage + "\n Type :" + p.Type;
                    StackSuivi.Children.Add(expSuivi);


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
