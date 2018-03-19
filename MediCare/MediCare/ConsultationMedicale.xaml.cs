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
    /// Logique d'interaction pour ConsultationMedicale.xaml
    /// </summary>
    public partial class ConsultationMedicale : Window
    {
        public ConsultationMedicale()
        {
            InitializeComponent();
        }

        private void AjoutTraitement_Click(object sender, RoutedEventArgs e)
        {
            TextBlock Medic = new TextBlock();
            ListeTraitement.Items.Add(Medic);
            TextBlock dose = new TextBlock();
            ListeTraitement.Items.Add(dose);
            TextBlock indication = new TextBlock();
            ListeTraitement.Items.Add(indication);
        }
    }
}
