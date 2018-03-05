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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void TestAgenda(object sender, RoutedEventArgs e)
        {
            AgendaMediCare TestAgenda = new AgendaMediCare();
            TestAgenda.Show();
            this.Close();
        }
        private void TestConsultation(object sender, RoutedEventArgs e)
        {
            MenuConsultation TestConsultation = new MenuConsultation();
            TestConsultation.Show();
            this.Close();
        }

        private void TestPatient(object sender, RoutedEventArgs e)
        {
            FichePatient TestPatient = new FichePatient();
            TestPatient.Show();
            this.Close();
        }

        private void medecin2_Click(object sender, RoutedEventArgs e)
        {
            InscriptionMedecin TestInsMedecin = new InscriptionMedecin();
            TestInsMedecin.Show();
            this.Close();
        }

        private void TestMain(object sender, RoutedEventArgs e)
        {
            SignUp TestSignUp = new SignUp();
            TestSignUp.Show();
            this.Close();
        }
    }
}
