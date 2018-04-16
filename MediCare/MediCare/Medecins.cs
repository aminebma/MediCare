using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
	class Medecins
	{

		private string nom;
		private string prenom;
		private DateTime datenaiss;
		private string adresse;
		private int numtel;
		private string sexe;
		private Boolean active;
		private string key;
		private string username;
		private string password;

		public Medecins(string nom, string prenom, DateTime datenaiss, string adresse, int numtel, string sexe, Boolean active, string key, string username, string password)
		{
			this.nom = nom;
			this.prenom = prenom;
			this.datenaiss = datenaiss;
			this.adresse = adresse;
			this.numtel = numtel;
			this.sexe = sexe;
			this.active = active;
			this.key = key;
			this.username = username;
			this.password = password;
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
		public Boolean Active
		{
			get { return active; }
			set { active = value; }
		}
		public string Username
		{
			get { return username; }
			set { username = value; }
		}
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		public string Key
		{
			get { return key; }
			set { key = value; }
		}
		
	}
}

