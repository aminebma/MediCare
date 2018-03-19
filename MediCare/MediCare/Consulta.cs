using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
	class Consulta
	{
		private DateTime date;
		private string diagnostic;
		private string description;
		private string cheminCertificat;
		private string cheminLettreOrientation;
		private string cheminScanner;
		private string cheminBilan;
		private string cheminOrdo;
		public List<Traite> traitement;
		public List<string> Radio; 
	

		public Consulta(DateTime date, string diagnostic, string description, string cheminCertificat, string cheminLettreOrientation, string cheminScanner, string cheminBilan, string cheminOrdo, List<Traite> traitement, List<string> Radio )
		{
			this.date = date;
			this.diagnostic = diagnostic  ;
			this.description = description ;
			this.cheminCertificat = cheminCertificat;
			this.cheminLettreOrientation = cheminLettreOrientation;
			this.cheminScanner = cheminScanner;
			this.cheminOrdo = cheminOrdo;
			this.cheminBilan = cheminBilan;
			this.Radio = Radio;
			this.traitement = traitement; 


		}
        public Consulta()
        {

        }
		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

		public string Diagnostic
		{
			get { return diagnostic; }
			set { diagnostic = value; }
		}
		public string Description
		{
			get { return description ; }
			set { description = value; }
		}
		public string CheminCertificat
		{
			get { return cheminCertificat; }
			set { cheminCertificat = value; }
		}
		public string CheminLettreOrientation
		{
			get { return cheminLettreOrientation; }
			set { cheminLettreOrientation = value; }
		}
		public string CheminBilan
		{
			get { return cheminBilan; }
			set { cheminBilan = value; }
		}
		public string CheminScanner
		{
			get { return cheminScanner; }
			set { cheminScanner = value; }
		}
		public string CheminOrdo
		{
			get { return cheminOrdo; }
			set {  cheminOrdo = value; }
		}
	}
}

