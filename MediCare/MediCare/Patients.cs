using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
	public class Patients 
	{
		private string nom;
		private string prenom;
		private DateTime datenaiss;
		private string adresse;
		private int numtel;
		private string sexe;
		private int taille;
		private int poids;
		private string groupage;
		private string maladie;
		private string etatSante;

		public Patients (string nom, string prenom, DateTime datenaiss, string adresse, int numtel, string sexe, int taille, int poids, string groupage, string maladie, string etatSante)
		{
			this.nom = nom;
			this.prenom = prenom;
			this.datenaiss = datenaiss;
			this.adresse = adresse;
			this.numtel = numtel;
			this.sexe = sexe;
			this.taille= taille;
			this.poids= poids;
			this.groupage = groupage;
			this.maladie = maladie;
			this.etatSante = etatSante;
		}
		public string Nom
		{
			get { return nom; }
			set { nom = value; }
		}
		public string Prenom
		{
			get { return prenom; }
			set { prenom = value; }
		}
		public DateTime Datenaiss
		{
			get { return datenaiss; }
			set { datenaiss = value; }
		}
		public string Adresse
		{
			get { return adresse; }
			set { adresse = value; }
		}
		public int Numtel
		{
			get { return numtel; }
			set { numtel = value; }
		}
		public string Sexe
		{
			get { return sexe; }
			set { sexe = value; }
		}
		public int Taille
		{
			get { return taille; }
			set { taille = value; }
		}
		public int Poids
		{
			get { return poids; }
			set { poids = value; }
		}
		public string Groupage
		{
			get { return groupage; }
			set { groupage = value; }
		}
		public string Maladie
		{
			get { return maladie; }
			set { maladie = value; }
		}
		public string EtatSante
		{
			get { return etatSante; }
			set { etatSante = value; }
		}
		
		}
}
