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
    /// Logique d'interaction pour AjouterMédic.xaml
    /// </summary>
    public partial class AjouterMédic : Window
    {
        public AjouterMédic()
        {
            InitializeComponent();
        }

        private void AjtMedic_Click(object sender, RoutedEventArgs e)
        {
            Medic medc = new Medic();
            medc.AjouterMedic(NomMedic.Text, Type.Text, Dosage.Text);
            MessageBox.Show("Médicament ajouter avec succès !");
            this.Close();
        }
    }
}
