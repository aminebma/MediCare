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

                        Button ButtonSuivi = new Button();
                        ButtonSuivi.Background = Brushes.Gray;
                        ButtonSuivi.Content = p.Label + " " + p.Date;
                        ListSuivi.Items.Add(ButtonSuivi);
                        ////ButtonSuivi.Click += ButtonSuivi_Click;
                        ButtonSuivi.Click += NewMethod(p);

                    }

                }

            }
        }

        private RoutedEventHandler NewMethod(ConsultLabel p)
        {
            return delegate
            {
                Globals.Id = p.Id;
                consulta = consultation.AcceeConsultationId(Globals.Id);
                consAccessT.Text = consulta.Date + " " + consulta.Diagnostic + " " + consulta.Description;
                foreach (string d in consulta.Radio)
                {
                    consAccessT.Text = "\n" + d;
                }
                foreach (Traite d in consulta.traitement)
                {
                    consAccessT.Text = "\n" + d.NomMed + " " + d.Dose + " " + d.Indication;

                }
            };
        }
        //protected void ButtonSuivi_Click(object sender, RoutedEventArgs e)
        //{

        //    consulta = consultation.AcceeConsultationId(Id);
        //    TextBox consBox = new TextBox();


        //}
        //Button button = new Button();
        //button.Click += new EventHandler(button_Click);

        //protected void button_Click(object sender, EventArgs e)
        //{
        //    Button button = sender as Button;
        //    // identify which button was clicked and perform necessary actions
        //}
    }
}
 
               