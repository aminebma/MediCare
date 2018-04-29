using Microsoft.Win32;
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
	/// Logique d'interaction pour Menu_Fichier.xaml
	/// </summary>
	public partial class MenuFichier : UserControl
	{
        string label;
        string diagnostic;
        string description;
        List<Traite> traitement = new List<Traite>();
        List<string> radio = new List<string>();
        string CheminScanner;
        string CheminBilan;
        string CheminCertificat;
        string CheminLettre;

        public MenuFichier(string label, string diagnostic, string description, List<Traite> traitement)
		{
            this.label = label;
            this.diagnostic = diagnostic;
            this.description = description;
            this.traitement = traitement;
            InitializeComponent();
		}
        public MenuFichier()
        {
            InitializeComponent();
        }

        private void AjouterRadio_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                MessageBox.Show(ofd.FileName);
                this.radio.Add(ofd.FileName);
            }
        }

        private void AjouterScanner_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                MessageBox.Show(ofd.FileName);
                this.CheminCertificat = ofd.FileName;
                this.CheminLettre = ofd.FileName;
                this.CheminScanner = ofd.FileName;

            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            UserControl ordo = new GenererOrdonnance(label,diagnostic,description,traitement,this.radio,this.CheminBilan,this.CheminScanner,this.CheminCertificat,this.CheminLettre);
            parent.Children.Clear();
            parent.Children.Add(ordo);
        }

        private void AjouterBilan_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                this.CheminBilan = ofd.FileName;
            }
        }

        private void CertificatMedical_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new Certificat(label, diagnostic, description, traitement));

        }

        private void LettreOrientation_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new LettreOr(label, diagnostic, description, traitement));
        }
    }
}
