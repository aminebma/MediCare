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
    /// Logique d'interaction pour SUIVIS.xaml
    /// </summary>
    public partial class SUIVIS : Window
    {
        public SUIVIS()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


        List<ConsultLabel> list;
        Consult consultation = new Consult();
        Consulta consulta = new Consulta();

        private void suivi_Click(object sender, RoutedEventArgs e)
        {
            if (nomT.Text != "")
            {
                list = consultation.Suivie(nomT.Text, prenomT.Text);
                if (list.Count() != 0)
                {
                    foreach (ConsultLabel p in list)
                    {
                        Expander expSuivi = new Expander();
                        expSuivi.Header = p.Label + " " + p.Date;
                        StackSuivi.Children.Add(expSuivi);
                        consulta = consultation.AcceeConsultationId(p.Id);
                        expSuivi.Content = consulta.Date + " " + consulta.Diagnostic + " " + consulta.Description;
                        foreach (string d in consulta.Radio)
                        {
                            expSuivi.Content = expSuivi.Content + "\n" + d;
                        }
                        foreach (Traite d in consulta.traitement)
                        {
                            expSuivi.Content =expSuivi.Content + "\n" + d.NomMed + " " + d.Dose + " " + d.Indication;

                        }


                    }

                }

            }
        }
        
    }
}
 
               