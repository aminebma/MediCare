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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
/*using Syncfusion.Compression;
using Syncfusion.PdfViewer.Base;
using Syncfusion.Pdf;*/
using System.Text.RegularExpressions;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour Certificat.xaml
    /// </summary>
    public partial class Certificat : UserControl
    {
        Regex charControl = new Regex(@"[A-Za-z]+");

        public Certificat()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // PdfDocument document = new PdfDocument();

            Document doc = new Document();
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Certificat.pdf", FileMode.Create));
            doc.Open();
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times2 = new Font(bfTimes, 20, Font.BOLD);
            Font times = new Font(bfTimes, 12, Font.BOLD);
            Font times1 = new Font(bfTimes, 12, Font.NORMAL);

            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph("République Algérienne Démocratique et Populaire \n الجمهورية الجزائرية الديمقراطية الشعبية\n " +
            "Ministère de la santé et de la population     \n\n\n\n \n\n\n\n  وزارة التعليم العالي و البحث العلمي", times);
            doc.Add(para);
            iTextSharp.text.Paragraph titre = new iTextSharp.text.Paragraph("                                  Certificat médical \n\n\n\n", times2);
            doc.Add(titre);
            iTextSharp.text.Paragraph para2 = new iTextSharp.text.Paragraph("Je sousigné Docteur  " + Globals.NomMedecin + " " + Globals.PrenomMedecin, times1);
            doc.Add(para2);

            if (title.Text == "Mademoiselle")
            {
                iTextSharp.text.Paragraph para3 = new iTextSharp.text.Paragraph("Mademoiselle  " + Globals.NomPatient + " " + Globals.PrenomPatient + "  Née le  " + date_naiss.Text + "  Demeurant à  " + adresse.Text + "\n\n\n", times1);
                doc.Add(para3);

                if ((bool)option.IsChecked)
                {
                    iTextSharp.text.Paragraph para4 = new iTextSharp.text.Paragraph(" Doit etre dispensée d'activité physique et sportive pendant " + jour.Text + " à compter du " + date_naiss2.Text + "\n\n", times1);
                    doc.Add(para4);
                }


                if ((bool)option_Copy.IsChecked)
                {
                    iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph(" Ne présente aucune contre-indication apparente à la pratique du sport suivant  " + nom_sport.Text + "\n\n", times1);
                    doc.Add(para5);
                }

                if ((bool)option_Copy1.IsChecked)
                {
                    iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph(" Ne présente actuellement aucun signe cliniquement décelable d'affectation contagieuse \n\n", times1);
                    doc.Add(para6);
                }

                if ((bool)option_Copy2.IsChecked)
                {
                    iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("  Inapte à la vie en collectivité et ne pourra fréquenter l'école pendant  " + jour2.Text + "  à compter du  " + jour22.Text + "\n\n", times1);
                    doc.Add(para7);
                }
                if ((bool)option_Copy3.IsChecked)
                {
                    iTextSharp.text.Paragraph para8 = new iTextSharp.text.Paragraph("  Présente un état de santé nécessitant un arrêt de travail  pendant  " + jour3.Text + " à compter du  " + jour33.Text + "\n\n", times1);
                    doc.Add(para8);
                }
            }

            if (title.Text == "Madame")
            {
                iTextSharp.text.Paragraph para3 = new iTextSharp.text.Paragraph("Madame  " + Globals.NomPatient + " " + Globals.PrenomPatient + "  Née le  " + date_naiss.Text + "  Demeurant à  " + adresse.Text + "\n\n\n", times1);
                doc.Add(para3);

                if ((bool)option.IsChecked)
                {
                    iTextSharp.text.Paragraph para4 = new iTextSharp.text.Paragraph(" Doit etre dispensée d'activité physique et sportive pendant " + jour.Text + " à compter du " + date_naiss2.Text + "\n\n", times1);
                    doc.Add(para4);
                }


                if ((bool)option_Copy.IsChecked)
                {
                    iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph(" Ne présente aucune contre-indication apparente à la pratique du sport suivant  " + nom_sport.Text + "\n\n", times1);
                    doc.Add(para5);
                }

                if ((bool)option_Copy1.IsChecked)
                {
                    iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph(" Ne présente actuellement aucun signe cliniquement décelable d'affectation contagieuse \n\n", times1);
                    doc.Add(para6);
                }

                if ((bool)option_Copy2.IsChecked)
                {
                    iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("  Inapte à la vie en collectivité et ne pourra fréquenter l'école pendant  " + jour2.Text + " à compter du  " + jour22.Text + "\n\n", times1);
                    doc.Add(para7);
                }
                if ((bool)option_Copy3.IsChecked)
                {
                    iTextSharp.text.Paragraph para8 = new iTextSharp.text.Paragraph("  Présente un état de santé nécessitant un arrêt de travail  pendant  " + jour3.Text + "  à compter du  " + jour33.Text + "\n\n", times1);
                    doc.Add(para8);
                }
            }

            if (title.Text == "Monsieur")
            {
                iTextSharp.text.Paragraph para3 = new iTextSharp.text.Paragraph("Monsieur " + Globals.NomPatient + " " + Globals.PrenomPatient + "  Né le  " + date_naiss.Text + "  Demeurant à  " + adresse.Text + "\n\n\n", times1);
                doc.Add(para3);

                if ((bool)option.IsChecked)
                {
                    iTextSharp.text.Paragraph para4 = new iTextSharp.text.Paragraph(" Doit etre dispensé d'activité physique et sportive pendant " + jour.Text + " à compter du " + date_naiss2.Text + "\n\n", times1);
                    doc.Add(para4);
                }


                if ((bool)option_Copy.IsChecked)
                {
                    iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph(" Ne présente aucune contre-indication apparente à la pratique du sport suivant  " + nom_sport.Text + "\n\n", times1);
                    doc.Add(para5);
                }

                if ((bool)option_Copy1.IsChecked)
                {
                    iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph(" Ne présente actuellement aucun signe cliniquement décelable d'affectation contagieuse \n\n", times1);
                    doc.Add(para6);
                }

                if ((bool)option_Copy2.IsChecked)
                {
                    iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("  Inapte à la vie en collectivité et ne pourra fréquenter l'école pendant  " + jour2.Text + " à compter du  " + jour22.Text + "\n\n", times1);
                    doc.Add(para7);
                }
                if ((bool)option_Copy3.IsChecked)
                {
                    iTextSharp.text.Paragraph para8 = new iTextSharp.text.Paragraph("  Présente un état de santé nécessitant un arrêt de travail  pendant  " + jour3.Text + " à compter du  " + jour33.Text + "\n\n", times1);
                    doc.Add(para8);
                }

            }



            iTextSharp.text.Paragraph para9 = new iTextSharp.text.Paragraph("      \n\n                                                                                          Fait à  " + adr.Text + " Le   " + DateTime.Today + "\n\n\n\n\n\n\n ", times1);
            doc.Add(para9);
            iTextSharp.text.Paragraph para10 = new iTextSharp.text.Paragraph(" Signature du medecin                                                                              Cachet du medecin ", times);
            doc.Add(para10);
            doc.Close();
            MessageBox.Show("Le fichier a été généré ! ");

        }

        private void nom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void date_naiss_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void date_naiss2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void jour22_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void jour33_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void date_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void Suivant_Click(object sender, RoutedEventArgs e)
        {

            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new MenuFichier());
        }
    }
}

