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
    public partial class AjouterMédic : UserControl
    {
        public AjouterMédic()
        {
            InitializeComponent();
        }

        private void AjtMedic_Click(object sender, RoutedEventArgs e)
        {
            Medic medc = new Medic();
            try
            {
                medc.AjouterMedic(NomMedic.Text.ToUpper(), Type.Text.ToUpper(), Dosage.Text.ToUpper());
                MessageBox.Show("Médicament ajouté à la base de données");
                ((Grid)this.Parent).Children.Clear();
            }
            catch(Exception)
            {
                MessageBox.Show("Une erreur s'est produite, le médicament n'a pas été ajouté");
            }
        }
    }
}
