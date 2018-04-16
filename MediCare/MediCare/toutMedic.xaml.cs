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
    /// Logique d'interaction pour toutMedic.xaml
    /// </summary>
    public partial class toutMedic : UserControl
    {

        Agenda personne = new Agenda(); // /o\
        List<Medicaments> list;
        public toutMedic()
        { 
            //Rachid was here :)
            
                InitializeComponent();
                list = personne.RechercherToutMedicament();
                if (list.Count() != 0)
                {
                    foreach (Medicaments p in list)
                    {
                        Expander expSuivi = new Expander();
                        expSuivi.Header = "Nom du Médicament :" + p.nom;
                        expSuivi.Content = " Dose : " + p.Dosage + "\n  :" + p.Type ;
                        StackSuivi.Children.Add(expSuivi);


                    }
                }

            
        }
    }
}
