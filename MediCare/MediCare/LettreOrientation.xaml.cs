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
    /// Logique d'interaction pour LettreOrientation.xaml
    /// </summary>
    public partial class LettreOrientation : UserControl
    {
        public LettreOrientation()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Document doc = new Document();
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Certificat.pdf", FileMode.Create));
            doc.Open();
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times2 = new Font(bfTimes, 20, Font.BOLD);
            Font times = new Font(bfTimes, 12, Font.BOLD);
            Font times1 = new Font(bfTimes, 12, Font.NORMAL);

            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph("République Algérienne Démocratique et Populaire \n الجمهورية الجزائرية الديمقراطية الشعبية\n " +
            "Ministère de la santé et de la population \n\n\n\n \n\n\n\n  وزارة التعليم العالي و البحث العلمي", times);
            doc.Add(para);
            iTextSharp.text.Paragraph titre = new iTextSharp.text.Paragraph("                                  Lettre d'orientation \n\n\n\n", times2);
            doc.Add(titre);
            iTextSharp.text.Paragraph para2 = new iTextSharp.text.Paragraph("                                                            Docteur\n\n ", times1);
            doc.Add(para2);
            iTextSharp.text.Paragraph para3 = new iTextSharp.text.Paragraph("                                                           Adresse \n\n ", times1);
            doc.Add(para3);
            iTextSharp.text.Paragraph para4 = new iTextSharp.text.Paragraph(                                                            adr.Text+","+date.Text               , times1);
            doc.Add(para4);
            iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph( "Objet : \n"+" Avis de traitement \n "+"Examens complémentaires\n\n\n"+"Mon cher confrere\n\n", times1);
            doc.Add(para5);
            iTextSharp.text.Paragraph para6 = new iTextSharp.text.Paragraph("Je vous adresse Monsieur "+nom.Text+" agé de " +age.Text+" ans qui présente "+sit.Text, times1);
            doc.Add(para6);
            iTextSharp.text.Paragraph para7 = new iTextSharp.text.Paragraph("Je souhaiterais que vous le preniez en main en lui faisant des examens complémentaires pour ensuite me donner votre avis.\n\n "+"Je vous remercie et vous prie d'agréer, mon cher confrère, mes salutations distinguées.  ", times1);
            doc.Add(para7);

            if ((bool)femme.IsChecked)
            {
                iTextSharp.text.Paragraph para55 = new iTextSharp.text.Paragraph("                                                           Docteur :"+ Globals.NomMedecin+" "+Globals.PrenomMedecin   , times1);
                doc.Add(para55);
            }
            }
        }
}
