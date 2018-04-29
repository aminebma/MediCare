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

namespace MediCare
{
    public partial class LettreOr : UserControl
    {
        public string chemin;
        private string label;
        string diagnostic;
        string description;
        List<Traite> traitement = new List<Traite>();

        public LettreOr(string label, string diagnostic, string description, List<Traite> traitement)
        {
            InitializeComponent();
            this.label = label;
            this.diagnostic = diagnostic;
            this.description = description;
            this.traitement = traitement;
        }


        private void Suivant_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new Document();
            System.IO.FileStream file = new System.IO.FileStream("Lettre_orientation_"+" " + Globals.PrenomPatient + " " + Globals.NomPatient + Globals.IdConsult + ".pdf", FileMode.Create);
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
            iTextSharp.text.Paragraph titre = new iTextSharp.text.Paragraph("                                  Lettre d'orientation \n\n\n\n", times2);
            doc.Add(titre);
            iTextSharp.text.Paragraph para2 = new iTextSharp.text.Paragraph("                                                                                                                   Docteur\n\n " + Globals.NomMedecin + " " + Globals.PrenomMedecin, times1);
            doc.Add(para2);
            iTextSharp.text.Paragraph para3 = new iTextSharp.text.Paragraph("                                                                                                                   Adresse \n\n ", times1);
            doc.Add(para3);
            iTextSharp.text.Paragraph para4 = new iTextSharp.text.Paragraph(adr.Text + ", " + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year, times1);
            doc.Add(para4);
            if (Sexe.Text == "Monsieur")
            {
                iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph("Objet :       \n" + "         Avis de traitement \n " + "         Examens complémentaires\n\n\n" + "Mon cher confrere\n\n", times1);
                doc.Add(para5);
                iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph("Je vous adresse Monsieur " + Globals.NomPatient + " " + Globals.PrenomPatient + " agé de " + Globals.Age + "  ans. Demeurant à " + Globals.AdressePatient + " qui présente  " + patientsT2.Text, times1);
                doc.Add(para6);
                iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("Je souhaiterais que vous le preniez en main en lui faisant des examens complémentaires pour ensuite me donner votre avis.\n\n " + "Je vous remercie et vous prie d'agréer, mon cher confrere, mes salutations distinguées.\n\n\n\n   ", times1);
                doc.Add(para7);
            }
            if (Sexe.Text == "Madame")
            {
                iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph("Objet :         \n" + "               Avis de traitement \n " + "          Examens complémentaires\n\n\n" + "Mon cher confrere\n\n", times1);
                doc.Add(para5);
                iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph("Je vous adresse Madame " + Globals.NomPatient + " " + Globals.PrenomPatient + " agée de " + Globals.Age + "  ans. Demeurant à " + Globals.AdressePatient + " qui présente  " + patientsT2.Text, times1);
                doc.Add(para6);
                iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("Je souhaiterais que vous le preniez en main en lui faisant des examens complémentaires pour ensuite me donner votre avis.\n\n " + "Je vous remercie et vous prie d'agréer, mon cher confrere, mes salutations distinguées.\n\n\n\n   ", times1);
                doc.Add(para7);
            }

            if (Sexe.Text == "Mademoiselle")
            {
                iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph("Objet :          \n" + "     Avis de traitement \n " + "     Examens complémentaires\n\n\n" + "Mon cher confrere\n\n", times1);
                doc.Add(para5);
                iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph("Je vous adresse Mademoiselle " + Globals.NomPatient + " " + Globals.PrenomPatient + " agée de " + Globals.Age + "  ans. Demeurant à " + Globals.AdressePatient + " qui présente  " + patientsT2.Text, times1);
                doc.Add(para6);
                iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("Je souhaiterais que vous le preniez en main en lui faisant des examens complémentaires pour ensuite me donner votre avis.\n\n " + "Je vous remercie et vous prie d'agréer, mon cher confrere, mes salutations distinguées. \n\n\n\n  ", times1);
                doc.Add(para7);
            }

            iTextSharp.text.Paragraph para10 = new iTextSharp.text.Paragraph(" Signature du medecin                                                                              Cachet du medecin ", times);
            doc.Add(para10);
            chemin = file.Name;
            doc.Close();
            MessageBox.Show("Le fichier a été généré ! ");
            System.Diagnostics.Process.Start(chemin);
            var parent = (Grid)this.Parent;
            UserControl fich = new MenuFichier(label, diagnostic, description, traitement);
            parent.Children.Clear();
            parent.Children.Add(fich);
        }

       
    }
}
