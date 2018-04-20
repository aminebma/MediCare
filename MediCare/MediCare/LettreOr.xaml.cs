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
    /// <summary>
    /// Logique d'interaction pour LettreOr.xaml
    /// </summary>
    public partial class LettreOr : UserControl
    {
        public LettreOr()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new Document();
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Lettre_orientation.pdf", FileMode.Create));
            doc.Open();
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times2 = new Font(bfTimes, 20, Font.BOLD);
            Font times = new Font(bfTimes, 12, Font.BOLD);
            Font times1 = new Font(bfTimes, 12, Font.NORMAL);

            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph("République Algérienne Démocratique et Populaire \n الجمهورية الجزائرية الديمقراطية الشعبية\n " +
            "Ministère de la santé et de la population \n\n\n\n  وزارة التعليم العالي و البحث العلمي", times);
            doc.Add(para);
            iTextSharp.text.Paragraph titre = new iTextSharp.text.Paragraph("                                  Lettre d'orientation \n\n\n\n", times2);
            doc.Add(titre);
            iTextSharp.text.Paragraph para2 = new iTextSharp.text.Paragraph("                                                                                                                   Docteur\n\n " +Globals.NomMedecin+ " " + Globals.PrenomMedecin, times1);
            doc.Add(para2);
            iTextSharp.text.Paragraph para3 = new iTextSharp.text.Paragraph("                                                                                                                   Adresse \n\n ", times1);
            doc.Add(para3);
            iTextSharp.text.Paragraph para4 = new iTextSharp.text.Paragraph(adr.Text + ", " + date.Text, times1);
            doc.Add(para4);
            if ((bool) ___op_Copy1.IsChecked)
                    {
                iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph("Objet :       \n" + "         Avis de traitement \n " + "         Examens complémentaires\n\n\n" + "Mon cher confrere\n\n", times1);
                doc.Add(para5);
                iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph("Je vous adresse Monsieur " + Globals.NomPatient + " " + Globals.PrenomPatient + " agé de " + Globals.Age + "  ans. Demeurant à " + adresse.Text + " qui présente  " + patientsT2.Text, times1);
                doc.Add(para6);
                iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("Je souhaiterais que vous le preniez en main en lui faisant des examens complémentaires pour ensuite me donner votre avis.\n\n " + "Je vous remercie et vous prie d'agréer, mon cher confrere, mes salutations distinguées.\n\n\n\n   ", times1);
                doc.Add(para7);
            }
            if ((bool)___op_Copy.IsChecked)
            {
                iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph("Objet :         \n" + "               Avis de traitement \n " + "          Examens complémentaires\n\n\n" + "Mon cher confrere\n\n", times1);
                doc.Add(para5);
                iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph("Je vous adresse Madame " + Globals.NomPatient + " " + Globals.PrenomPatient + " agée de " + Globals.Age + "  ans. Demeurant à " + adresse.Text + " qui présente  " + patientsT2.Text, times1);
                doc.Add(para6);
                iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("Je souhaiterais que vous le preniez en main en lui faisant des examens complémentaires pour ensuite me donner votre avis.\n\n " + "Je vous remercie et vous prie d'agréer, mon cher confrere, mes salutations distinguées.\n\n\n\n   ", times1);
                doc.Add(para7);
            }

            if ((bool)___op.IsChecked)
            {
                iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph("Objet :          \n" + "     Avis de traitement \n " + "     Examens complémentaires\n\n\n" + "Mon cher confrere\n\n", times1);
                doc.Add(para5);
                iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph("Je vous adresse Mademoiselle " + Globals.NomPatient + " " + Globals.PrenomPatient + " agée de " + Globals.Age + "  ans. Demeurant à " + adresse.Text + " qui présente  " + patientsT2.Text, times1);
                doc.Add(para6);
                iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("Je souhaiterais que vous le preniez en main en lui faisant des examens complémentaires pour ensuite me donner votre avis.\n\n " + "Je vous remercie et vous prie d'agréer, mon cher confrere, mes salutations distinguées. \n\n\n\n  ", times1);
                doc.Add(para7);
            }

            iTextSharp.text.Paragraph para10 = new iTextSharp.text.Paragraph(" Signature du medecin                                                                              Cachet du medecin ", times);
            doc.Add(para10);
            doc.Close();
            MessageBox.Show("Le fichier a été généré ! ");

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Suivant_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)this.Parent;
            UserControl fich = new MenuFichier();
            parent.Children.Clear();
            parent.Children.Add(fich);
        }
    }
}
