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
    /// Logique d'interaction pour LogOutPop.xaml
    /// </summary>
    public partial class LogOutPop : Window
    {
        Window creatingForm;

        public LogOutPop()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        public Window setCreatingForm
        {
            get { return creatingForm; }
            set { creatingForm = value; }
        }

        private void Non_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Oui_Click(object sender, RoutedEventArgs e)
        {
            SignUp sgnup = new SignUp();
            sgnup.Show();
            creatingForm.Close();
            this.Close();
        }
    }
}
