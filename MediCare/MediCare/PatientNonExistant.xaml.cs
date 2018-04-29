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
    /// Logique d'interaction pour PatientNonExistant.xaml
    /// </summary>
    public partial class PatientNonExistant : UserControl
    {
        public PatientNonExistant()
        {
            InitializeComponent();
        }

        PersonneClasse pers = new PersonneClasse();

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            pers.AddPatientPersonne(Globals.NomPatient, Globals.PrenomPatient);
            Globals.AdressePatient = Adresse.Text;
            DateTime date = Date_naiss.SelectedDate.Value;
            Globals.Age = DateTime.Today.Year - date.Year;
            MessageBox.Show("Le patient a été inséré ! ");
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
        }

       

        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }


        private void Sexe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
