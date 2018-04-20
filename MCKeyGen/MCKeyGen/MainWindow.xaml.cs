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

namespace MCKeyGen
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> location = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            location.Add(@"ressources\textbox_user_1.jpg");
            location.Add(@"ressources\textbox_user_2.jpg");
            location.Add(@"ressources\textbox_user_4.jpg");
            location.Add(@"ressources\textbox_user_5.jpg");
            location.Add(@"ressources\textbox_user_6.jpg");
            location.Add(@"ressources\textbox_user_7.jpg");
            location.Add(@"ressources\textbox_user_8.jpg");
            location.Add(@"ressources\textbox_user_9.jpg");
            location.Add(@"ressources\textbox_user_10.jpg");
            location.Add(@"ressources\textbox_user_11.jpg");
            location.Add(@"ressources\textbox_user_12.jpg");
            location.Add(@"ressources\textbox_user_13.jpg");
            location.Add(@"ressources\textbox_user_14.jpg");
            location.Add(@"ressources\textbox_user_15.jpg");
            location.Add(@"ressources\textbox_user_16.jpg");
            location.Add(@"ressources\textbox_user_17.jpg");
            location.Add(@"ressources\textbox_user_18.jpg");
            location.Add(@"ressources\textbox_user_19.jpg");
            location.Add(@"ressources\textbox_user_20.jpg");
            location.Add(@"ressources\textbox_user_21.jpg");
            location.Add(@"ressources\textbox_user_22.jpg");
            location.Add(@"ressources\textbox_user_23.jpg");
            location.Add(@"ressources\textbox_user_24.jpg");
            location.Add(@"ressources\debut.jpg");
            location.Add(@"ressources\textbox_password.png");
        }

        private void user_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (user.Text.Length > 0 && user.Text.Length <= 15)
            {

                im.Source = new BitmapImage(new Uri(location[user.Text.Length - 1], UriKind.Relative));
            }
            else if (user.Text.Length <= 0)
                im.Source = new BitmapImage(new Uri(location[23], UriKind.Relative));
            else
                im.Source = new BitmapImage(new Uri(location[22], UriKind.Relative));
        }

        private void pass_GotFocus(object sender, RoutedEventArgs e)
        {
            im.Source = new BitmapImage(new Uri(location[24], UriKind.Relative));
        }

        private void user_GotFocus(object sender, RoutedEventArgs e)
        {
            if (user.Text.Length > 0)

                im.Source = new BitmapImage(new Uri(location[user.Text.Length - 1], UriKind.Relative));
            else
                im.Source = new BitmapImage(new Uri(location[23], UriKind.Relative));
        }

        private void main_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                if (user.Text == "mcadmin" && pass.Password == "admin")
                {
                    KeyGen keygen = new KeyGen();
                    keygen.Show();
                    this.Close();
                }
                else MessageBox.Show("Accés refusé");
            }
        }
    }
}
