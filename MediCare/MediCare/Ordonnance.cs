using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace MediCare
{
	class Ordonnance
	{
		public string GenererOrdonnance(string nomMedecin, string prenomMedecin, string nomPatient, string prenomPatient, List<Traite> trait, string label)
		{

			Document doc = new Document();
			System.IO.FileStream file = new System.IO.FileStream(Globals.NomMedecin+DateTime.Today, System.IO.FileMode.OpenOrCreate);
			PdfWriter writer = PdfWriter.GetInstance(doc, file);

			doc.Open();
			BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
			iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
			iTextSharp.text.Font times1 = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.NORMAL);

			PdfPTable tableau = new PdfPTable(2);
			float[] widths = new float[] { 50, 50 };
			tableau.SetWidths(widths);

			tableau.TotalWidth = 500;
			tableau.LockedWidth = true;

			tableau.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

			iTextSharp.text.Image imgTrace = iTextSharp.text.Image.GetInstance("LOGO.png");
			imgTrace.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
			imgTrace.ScaleAbsoluteHeight(50f);
			imgTrace.ScaleAbsoluteWidth(50f);
			PdfPCell cellule = new PdfPCell(imgTrace);
			cellule.Rowspan = 50;
			cellule.HorizontalAlignment = 0;
			cellule.Border = 0;
			tableau.AddCell(cellule);
			PdfPCell cellule1 = new PdfPCell(new iTextSharp.text.Paragraph("CABINET MEDICAL DU DOCTEUR HAOUCHINE", times));
			cellule1.Rowspan = 50;
			cellule1.HorizontalAlignment = 0;
			cellule1.Border = 0;
			tableau.AddCell(cellule1);

			doc.Add(tableau);
		
			
			Font times2 = new Font(bfTimes, 20, Font.BOLD);
		
			iTextSharp.text.Paragraph titre = new iTextSharp.text.Paragraph("                                               " + label + "\n\n\n\n", times2);
			doc.Add(titre);
			Personne med = (from personne in Globals.DataClass.Personne
							where nomMedecin == personne.nom && prenomMedecin == personne.prenom
							select personne).First();
			iTextSharp.text.Paragraph para1 = new iTextSharp.text.Paragraph("                                       " + med.adresse + " ,  le " + DateTime.Today.DayOfWeek + "  " + DateTime.Today.DayOfYear + " " + DateTime.Today.Month + " " + DateTime.Today.Year, times1);
			doc.Add(para1);
			Personne pat = (from personne in Globals.DataClass.Personne
							where nomMedecin == personne.nom && prenomMedecin == personne.prenom
							select personne).First();

			iTextSharp.text.Paragraph para2 = new iTextSharp.text.Paragraph("                                       " + nomPatient + " " + prenomPatient  , times1);
			doc.Add(para2);
			iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(); 
			foreach ( var p in trait )
			{
				iTextSharp.text.Phrase phrase = new iTextSharp.text.Phrase( p.NomMed + " , " + p.Dose + ", Indication :" +p.Indication, times1);
				para.Add(phrase);
				para.Alignment = Element.ALIGN_JUSTIFIED; 
			}
			doc.Add(para); 


			doc.Close();
			file.Close();
            return file.Name;

		}
        
	}
}
