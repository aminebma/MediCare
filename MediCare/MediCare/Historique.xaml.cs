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
    /// Logique d'interaction pour Historique.xaml
    /// </summary>
    public partial class Historique : Window
    {
        public Historique()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


        List<ConsultLabel> list ;
        Consult cons = new Consult();
        Consulta consulta = new Consulta();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            list = cons.Historique();
            foreach (ConsultLabel p in list)
            {
                Expander expSuivi = new Expander();
                expSuivi.Header =p.Nom + " " + p.Prenom+ "\n"+ p.Label + " " + p.Date;
                StackSuivi.Children.Add(expSuivi);
                consulta = cons.AcceeConsultationId(p.Id);
                expSuivi.Content = consulta.Date + " " + consulta.Diagnostic + " " + consulta.Description ;
                foreach (string d in consulta.Radio)
                {
                    expSuivi.Content = expSuivi.Content + "\n" + d;
                }
                foreach (Traite d in consulta.traitement)
                {
                    expSuivi.Content = expSuivi.Content + "\n" + d.NomMed + " " + d.Dose + " " + d.Indication + "\n";

                }
            }
        }
    }
}
