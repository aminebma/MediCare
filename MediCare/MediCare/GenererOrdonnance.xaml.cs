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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Navigation;
using System.IO;
using Microsoft.Win32;

namespace MediCare
{
    public partial class GenererOrdonnance : UserControl
    {
        string label;
        string diagnostic;
        string description;
        List<Traite> traitment = new List<Traite>();
        List<string> radio = new List<string>();
        string ordonnance;
        string bilan;
        string scanner;
        string certificat;
        string Lettre;

        public GenererOrdonnance(string label ,string diagnostic, string description,List<Traite> traitment, List<string> radio, string bilan, string scanner, string certificat, string Lettre  )
        {
            InitializeComponent();
            this.label = label;
            this.diagnostic = diagnostic;
            this.description = description;
            this.traitment = traitment;
            this.radio = radio;
            this.bilan = bilan;
            this.scanner = scanner;
            this.certificat =certificat;
            this.Lettre  = Lettre;
        }

        public GenererOrdonnance(string label, string diagnostic, string description, List<Traite> traitment)
        {
            InitializeComponent();
            this.label = label;
            this.diagnostic = diagnostic;
            this.description = description;
            this.traitment = traitment;
        }

        public GenererOrdonnance()
        {
            InitializeComponent();
        }

        Ordonnance ordo = new Ordonnance();

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Consult consultation = new Consult();
            try
            {
                this.ordonnance = ordo.GenererOrdonnance(Globals.NomMedecin, Globals.PrenomMedecin, Globals.NomPatient, Globals.PrenomPatient, traitment, label, Globals.specialite, Globals.codeMedecin, Globals.Age, Globals.numMobile, Globals.num, Globals.fax, Globals.MailMedecin);
                consultation.AddConsult(Globals.NomPatient, Globals.PrenomPatient, Globals.NomMedecin, Globals.PrenomMedecin, diagnostic, description, certificat, Lettre, scanner, bilan, ordonnance, radio, traitment, label, Globals.Age);
                MessageBox.Show("Consultation ajoutée avec succés !");
                var parent = (Grid)this.Parent;
                parent.Children.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Une erreur s'est produite, la consultation n'a pas été ajoutée");
            }

        }


        private void VisuaiserOrdo_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                this.ordonnance = ordo.GenererOrdonnance(Globals.NomMedecin, Globals.PrenomMedecin, Globals.NomPatient, Globals.PrenomPatient, traitment, label,Globals.specialite,Globals.codeMedecin, Globals.Age,Globals.numMobile, Globals.num,Globals.fax,Globals.MailMedecin);
                System.Diagnostics.Process.Start(ordonnance);
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Une erreur s'est produite");
            //}
        }

        private void ImprimerOrdo_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog Fff = new PrintDialog();
            Fff.PageRangeSelection = PageRangeSelection.AllPages;
            Fff.UserPageRangeEnabled = true;
            bool? doPrint = Fff.ShowDialog();
            if (doPrint != true) return;
        }
    }
}
