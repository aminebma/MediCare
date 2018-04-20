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

namespace MCKeyGen
{
    /// <summary>
    /// Logique d'interaction pour KeyGen.xaml
    /// </summary>
    public partial class KeyGen : Window
    {
        public KeyGen()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void generate_Click(object sender, RoutedEventArgs e)
        {
            code.Text = Encrypt(nom.Text + prenom.Text + date.Text);
        }

        private void decrypt_Click(object sender, RoutedEventArgs e)
        {
            decrypted.Text = Decrypt(crypted.Text);
        }

        public string Encrypt(string word)
        {
            byte[] passBytes = Encoding.Unicode.GetBytes(word);
            string encryptedPass = Convert.ToBase64String(passBytes);
            return encryptedPass;
        }

        public string Decrypt(string word)
        {
            byte[] passBytes = Convert.FromBase64String(word);
            string originalWord = System.Text.Encoding.Unicode.GetString(passBytes);
            return originalWord;
        }
    }
}
