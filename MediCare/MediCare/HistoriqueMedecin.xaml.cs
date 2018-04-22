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
    public partial class HistoriqueMedecin : UserControl
    {
        List<ConsultLabel> list;
        Consult cons = new Consult();
        Consulta consulta = new Consulta();

        public HistoriqueMedecin()
        {
            InitializeComponent();
            list = cons.HistoriqueMedecin(Globals.NomMedecin, Globals.PrenomMedecin);
            foreach (ConsultLabel p in list)
            {
                Expander expSuivi = new Expander
                {
                    Header = " Patient : " + p.Nom + " " + p.Prenom + "\n Label : " + p.Label + " " + "\n Date de la consultation : " + p.Date.Day + "/" + p.Date.Month + "/" + p.Date.Year
                };
                StackSuivi.Children.Add(expSuivi);
                consulta = cons.AcceeConsultationId(p.Id);
                expSuivi.Content = " " + consulta.Diagnostic + "\n " + consulta.Description;
                if (consulta.traitement.Count() !=0 )
                {
                    expSuivi.Content = expSuivi.Content + "\n Traitement :";
                    foreach (Traite d in consulta.traitement)
                    {
                        expSuivi.Content = expSuivi.Content + "\n -" + d.NomMed + " " + d.Dose + " " + d.Indication + "\n";
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            var parent2 = (Grid)parent.Parent;
            parent2.Children.Add(new MonCompte());
        }
    }
}
