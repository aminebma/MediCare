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


namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour GenererOrdonnance.xaml
    /// </summary>
    public partial class GenererOrdonnance : Window
    {
        public GenererOrdonnance()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Document doc = new Document();
            //PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Test.pdf", FileMode.Create));
            //doc.Open();

            //BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            //Font times = new Font(bfTimes, 10, Font.BOLD);
            //Font times1 = new Font(bfTimes, 12, Font.NORMAL);

            //iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph("République Algérienne Démocratique et Populaire \n الجمهورية الجزائرية الديمقراطية الشعبية\n " +
            //    "Ministère de l'Enseignement Supérieur et de la Recherche Scientifique \n وزارة التعليم العالي و البحث العلمي", times);
           
            //doc.Add(para);
            //iTextSharp.text.Paragraph para1 = new iTextSharp.text.Paragraph("DR HAOUCHINE \n ", times);



        }
    }
}
