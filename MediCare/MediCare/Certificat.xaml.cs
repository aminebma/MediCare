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
    public partial class Certificat : UserControl
    {
        public string chemin ;
        Regex charControl = new Regex(@"[A-Za-z]+");
        Regex numControl = new Regex(@"[0-9]+");
        string label;
        string diagnostic;
        string description;
        List<Traite> traitement = new List<Traite>();

        public Certificat(string label, string diagnostic, string description, List<Traite> traitement)
        {
            InitializeComponent();
            this.label = label;
            this.diagnostic = diagnostic;
            this.description = description;
            this.traitement = traitement;
        }

        private void nom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!charControl.IsMatch(e.Text)) e.Handled = true;
        }

        private void date_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void Suivant_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new Document();
            System.IO.FileStream file = new System.IO.FileStream("Certificat" + " " + Globals.PrenomPatient + " " + Globals.NomPatient + Globals.IdConsult + ".pdf", FileMode.Create);
            PdfWriter wri = PdfWriter.GetInstance(doc, file);
            doc.Open();
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times2 = new Font(bfTimes, 20, Font.BOLD);
            Font times = new Font(bfTimes, 12, Font.BOLD);
            Font times1 = new Font(bfTimes, 12, Font.NORMAL);
            iTextSharp.text.Font Gtitre = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE);
            iTextSharp.text.Font Gras = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font Surligne = new iTextSharp.text.Font(bfTimes, 13, iTextSharp.text.Font.UNDERLINE);
            Font normal = new Font(bfTimes, 10, Font.NORMAL);
            Font normalsurligner = new Font(bfTimes, 10, Font.NORMAL | Font.UNDERLINE);


            PdfPTable tableau = new PdfPTable(3);
            float[] widths = new float[] { 20, 60, 20 };
            tableau.SetWidths(widths);

            tableau.TotalWidth = 500;
            tableau.LockedWidth = true;

            tableau.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            iTextSharp.text.Image imgTrace = iTextSharp.text.Image.GetInstance(@"./ressources/Images/LOGO.png");
            imgTrace.Alignment = iTextSharp.text.Image.ALIGN_TOP;
            imgTrace.ScaleAbsoluteHeight(70f);
            imgTrace.ScaleAbsoluteWidth(100f);
            PdfPCell cellule = new PdfPCell(imgTrace);
            cellule.Rowspan = 50;
            cellule.HorizontalAlignment = 0;
            cellule.Border = 0;
            tableau.AddCell(cellule);
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph("CABINET MEDICAL DU DOCTEUR " + Globals.NomMedecin, Gtitre);
            para.Alignment = Element.ALIGN_CENTER;
            PdfPCell cellule1 = new PdfPCell(para);
            cellule1.Rowspan = 50;
            cellule1.HorizontalAlignment = 0;
            cellule1.Border = 0;
            tableau.AddCell(cellule1);



            iTextSharp.text.Image imgTrace2 = iTextSharp.text.Image.GetInstance("./ressources/images/qr_code.png");
            imgTrace2.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
            imgTrace2.ScaleAbsoluteHeight(50f);
            imgTrace2.ScaleAbsoluteWidth(50f);

            PdfPCell cellule2 = new PdfPCell(imgTrace2);
            cellule2.Rowspan = 50;
            cellule2.HorizontalAlignment = 0;
            cellule2.Border = 0;

            tableau.AddCell(cellule2);
            tableau.SpacingAfter = 25f;

            doc.Add(tableau);

            Personne med = (from personne in Globals.DataClass.Personne
                            where Globals.NomMedecin == personne.nom && Globals.PrenomMedecin == personne.prenom
                            select personne).First();

            Personne pat = (from personne in Globals.DataClass.Personne
                            where Globals.NomPatient == personne.nom && Globals.PrenomPatient == personne.prenom
                            select personne).First();




            iTextSharp.text.Paragraph text1 = new iTextSharp.text.Paragraph("Dr " + Globals.PrenomMedecin + " " + Globals.NomMedecin, Gras);
            doc.Add(text1);
            iTextSharp.text.Paragraph text2 = new iTextSharp.text.Paragraph(Globals.specialite + " ( N° " + Globals.codeMedecin + ")", normal);
            text2.SpacingAfter = 50;
            doc.Add(text2);
            iTextSharp.text.Paragraph text3 = new iTextSharp.text.Paragraph(med.adresse, normal);
            doc.Add(text3);
            iTextSharp.text.Paragraph text4 = new iTextSharp.text.Paragraph("Tel : " + Globals.num + ", Mobile : " + Globals.numMobile, normal);
            doc.Add(text4);
            iTextSharp.text.Paragraph text5 = new iTextSharp.text.Paragraph("Fax : " + Globals.fax + ", Email : " + Globals.MailMedecin, normal);
            text5.SpacingAfter = 50f;

            doc.Add(text5);

            //iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph("République Algérienne Démocratique et Populaire \n الجمهورية الجزائرية الديمقراطية الشعبية\n " +
            //"Ministère de la santé et de la population     \n\n\n\n \n\n\n\n  وزارة التعليم العالي و البحث العلمي", times);
            //doc.Add(para);
            iTextSharp.text.Paragraph titre = new iTextSharp.text.Paragraph("                                  Certificat médical \n\n\n\n", times2);
            doc.Add(titre);
            iTextSharp.text.Paragraph para2 = new iTextSharp.text.Paragraph("Je sousigné Docteur  " + Globals.NomMedecin + " " + Globals.PrenomMedecin, times1);
            doc.Add(para2);

            if (title.Text == "Mademoiselle")
            {
                iTextSharp.text.Paragraph para3 = new iTextSharp.text.Paragraph("Mademoiselle  " + Globals.NomPatient + " " + Globals.PrenomPatient + "  Agée le  " + Globals.Age + " ans,  Demeurant à  " + Globals.AdressePatient +".", times1);
                doc.Add(para3);

                if ((bool)option.IsChecked)
                {
                    iTextSharp.text.Paragraph para4 = new iTextSharp.text.Paragraph(" Elle doit etre dispensée d'activité physique et sportive pendant " + jour.Text + " à compter du " + date_naiss2.Text + "\n\n", times1);
                    doc.Add(para4);
                }


                if ((bool)option_Copy.IsChecked)
                {
                    iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph(" Elle ne présente aucune contre-indication apparente à la pratique du sport suivant  " + nom_sport.Text + "\n\n", times1);
                    doc.Add(para5);
                }

                if ((bool)option_Copy1.IsChecked)
                {
                    iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph(" Elle ne présente actuellement aucun signe cliniquement décelable d'affectation contagieuse \n\n", times1);
                    doc.Add(para6);
                }

                if ((bool)option_Copy2.IsChecked)
                {
                    iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph(" Elle est inapte à la vie en collectivité et ne pourra fréquenter l'école pendant  " + jour2.Text + "  à compter du  " + jour22.Text + "\n\n", times1);
                    doc.Add(para7);
                }
                if ((bool)option_Copy3.IsChecked)
                {
                    iTextSharp.text.Paragraph para8 = new iTextSharp.text.Paragraph("Elle  présente un état de santé nécessitant un arrêt de travail  pendant  " + jour3.Text + " à compter du  " + jour33.Text + "\n\n", times1);
                    doc.Add(para8);
                }
            }

            if (title.Text == "Madame")
            {
                iTextSharp.text.Paragraph para3 = new iTextSharp.text.Paragraph("Madame  " + Globals.NomPatient + " " + Globals.PrenomPatient + "  Agée le  " + Globals.Age + " ans,  Demeurant à  " + Globals.AdressePatient + "\n\n\n", times1);
                doc.Add(para3);

                if ((bool)option.IsChecked)
                {
                    iTextSharp.text.Paragraph para4 = new iTextSharp.text.Paragraph(" Elle doit etre dispensée d'activité physique et sportive pendant " + jour.Text + " à compter du " + date_naiss2.Text + "\n\n", times1);
                    doc.Add(para4);
                }


                if ((bool)option_Copy.IsChecked)
                {
                    iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph(" Elle ne présente aucune contre-indication apparente à la pratique du sport suivant  " + nom_sport.Text + "\n\n", times1);
                    doc.Add(para5);
                }

                if ((bool)option_Copy1.IsChecked)
                {
                    iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph("Elle  ne présente actuellement aucun signe cliniquement décelable d'affectation contagieuse \n\n", times1);
                    doc.Add(para6);
                }

                if ((bool)option_Copy2.IsChecked)
                {
                    iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("Elle est inapte à la vie en collectivité et ne pourra fréquenter l'école pendant  " + jour2.Text + " à compter du  " + jour22.Text + "\n\n", times1);
                    doc.Add(para7);
                }
                if ((bool)option_Copy3.IsChecked)
                {
                    iTextSharp.text.Paragraph para8 = new iTextSharp.text.Paragraph("Elle présente un état de santé nécessitant un arrêt de travail  pendant  " + jour3.Text + "  à compter du  " + jour33.Text + "\n\n", times1);
                    doc.Add(para8);
                }
            }

            if (title.Text == "Monsieur")
            {
                iTextSharp.text.Paragraph para3 = new iTextSharp.text.Paragraph("Monsieur " + Globals.NomPatient + " " + Globals.PrenomPatient + "  Agé le  " + Globals.Age + " ans, Demeurant à  " + Globals.AdressePatient , times1);
                doc.Add(para3);

                if ((bool)option.IsChecked)
                {
                    iTextSharp.text.Paragraph para4 = new iTextSharp.text.Paragraph("Il  doit etre dispensé d'activité physique et sportive pendant " + jour.Text + " à compter du " + date_naiss2.Text + "\n\n", times1);
                    doc.Add(para4);
                }


                if ((bool)option_Copy.IsChecked)
                {
                    iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph("Il ne présente aucune contre-indication apparente à la pratique du sport suivant  " + nom_sport.Text + "\n\n", times1);
                    doc.Add(para5);
                }

                if ((bool)option_Copy1.IsChecked)
                {
                    iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph("Il  ne présente actuellement aucun signe cliniquement décelable d'affectation contagieuse \n\n", times1);
                    doc.Add(para6);
                }

                if ((bool)option_Copy2.IsChecked)
                {
                    iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("Il est inapte à la vie en collectivité et ne pourra fréquenter l'école pendant  " + jour2.Text + " à compter du  " + jour22.Text + "\n\n", times1);
                    doc.Add(para7);
                }
                if ((bool)option_Copy3.IsChecked)
                {
                    iTextSharp.text.Paragraph para8 = new iTextSharp.text.Paragraph("Il présente un état de santé nécessitant un arrêt de travail  pendant  " + jour3.Text + " à compter du  " + jour33.Text + "\n\n", times1);
                    doc.Add(para8);
                }

            }
            iTextSharp.text.Paragraph para9 = new iTextSharp.text.Paragraph("      \n\n                                                                                          Fait à  " + adr.Text + " Le   " + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + "\n\n\n\n\n\n\n ", times1);
            doc.Add(para9);
            iTextSharp.text.Paragraph para10 = new iTextSharp.text.Paragraph(" Signature du medecin                                                                              Cachet du medecin ", times);
            doc.Add(para10);
            chemin = file.Name;
            doc.Close();
            System.Diagnostics.Process.Start(chemin);
            var parent = (Grid)this.Parent;
            parent.Children.Clear();
            parent.Children.Add(new MenuFichier(label, diagnostic, description, traitement));
        }

        private void jour_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!numControl.IsMatch(e.Text)) e.Handled = true;
        }
    }
}

