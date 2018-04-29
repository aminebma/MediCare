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
		public string GenererOrdonnance(string nomMedecin, string prenomMedecin, string nomPatient, string prenomPatient, List<Traite> trait, string label, string specialite, string codeMedecin,  int age, int numMobile, int numTel, int Fax, string Email)
		{
			Document doc = new Document();
			System.IO.FileStream file = new System.IO.FileStream("Ordonnance"+" "+prenomPatient+" "+nomPatient+Globals.IdConsult+".pdf", System.IO.FileMode.OpenOrCreate);
			PdfWriter writer = PdfWriter.GetInstance(doc, file);

			doc.Open();
			BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
			iTextSharp.text.Font Gtitre = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.BOLD|iTextSharp.text.Font.UNDERLINE);
			iTextSharp.text.Font Gras  = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font Surligne = new iTextSharp.text.Font(bfTimes, 13, iTextSharp.text.Font.UNDERLINE);
			Font normal = new Font(bfTimes, 10 , Font.NORMAL);
			Font normalsurligner = new Font(bfTimes, 10, Font.NORMAL|Font.UNDERLINE);


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
			iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph("CABINET MEDICAL DU DOCTEUR "+ nomMedecin , Gtitre);
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
							where nomMedecin == personne.nom && prenomMedecin == personne.prenom
							select personne).First();
			
			Personne pat = (from personne in Globals.DataClass.Personne
							where nomPatient == personne.nom && prenomPatient == personne.prenom
							select personne).First();


			
		
			iTextSharp.text.Paragraph text1 = new iTextSharp.text.Paragraph("Dr " +prenomMedecin + " " + nomMedecin , Gras);
			doc.Add(text1);
			iTextSharp.text.Paragraph text2 = new iTextSharp.text.Paragraph( specialite + " ( N° " + codeMedecin + ")", normal);
			text2.SpacingAfter = 50;
			doc.Add(text2);
			iTextSharp.text.Paragraph text3 = new iTextSharp.text.Paragraph(med.adresse, normal);
            doc.Add(text3);
			iTextSharp.text.Paragraph text4 = new iTextSharp.text.Paragraph("Tel : 0" +numTel+ ", Mobile : 0" +numMobile , normal);
			doc.Add(text4);
			iTextSharp.text.Paragraph text5 = new iTextSharp.text.Paragraph("Fax : 0" + Fax + ", Email : " +Email , normal);
			text5.SpacingAfter = 50f;

			doc.Add(text5);

			iTextSharp.text.Paragraph titre = new iTextSharp.text.Paragraph(label.ToUpper(),Surligne );
			titre.Alignment = Element.ALIGN_CENTER;
			//titre.SpacingBefore = 20;
			titre.SpacingAfter = 50f;

			//para.Alignment = 1 '0-Left, 1 middle,2 Right
			doc.Add(titre);
			
			string month = Mois.ConvertMois(DateTime.Today);
			string day = Mois.ConvertDay(DateTime.Today);



			iTextSharp.text.Paragraph para1 = new iTextSharp.text.Paragraph(" Alger , " + day + " le " + DateTime.Today.Day + " " + month + " " + DateTime.Today.Year, normal);
			para1.Alignment = 1;
			para1.IndentationLeft = 200;
			doc.Add(para1);
			if ( pat.sexe == "Homme")
			{
				iTextSharp.text.Paragraph para2 = new iTextSharp.text.Paragraph(" Monsieur " + prenomPatient + "  " + nomPatient + " " + age + " ans" , normal);
				para2.Alignment = 1;
				para2.IndentationLeft = 200;
				para2.SpacingAfter = 50f;
				doc.Add(para2);

			}
			else
			{
				iTextSharp.text.Paragraph para2 = new iTextSharp.text.Paragraph(" Madame " + prenomPatient + "  " + nomPatient + " " + age + " ans", normal);
				para2.Alignment = 1;
				para2.IndentationLeft = 200;
				para2.SpacingAfter = 50f;

				doc.Add(para2);
			}
			


			//para1.SpacingBefore = 20;

			//para1.SpacingAfter = 20;

			
		    //para1.IndentationRight= 100 ;  


			
			

			//iTextSharp.text.Paragraph para2 = new iTextSharp.text.Paragraph("                                       " + nomPatient + " " + prenomPatient  , times1);
			//doc.Add(para2);
			iTextSharp.text.Paragraph para5 = new iTextSharp.text.Paragraph(); 
			foreach ( Traite p in trait )
			{
				iTextSharp.text.Phrase phrase = new iTextSharp.text.Phrase( p.NomMed + " , avec la dose : " + p.Dose + ",   \n  Indication :         " +p.Indication+"\n", normal);
				para5.Add(phrase);
				para5.Alignment = Element.ALIGN_JUSTIFIED; 
			}
			doc.Add(para5);
			


			doc.Close();
			file.Close();
            return file.Name;

		}
        
	}
}
