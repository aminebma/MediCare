 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
    public class Perso
    {

        private string nom;
		private string prenom;
		private DateTime datenaiss;
		private string adresse;
	    private int numtel;
		private string sexe;
        protected Perso (string nom,string prenom , DateTime datenaiss,string adresse, int numtel,string sexe)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.datenaiss = datenaiss;
            this.adresse = adresse;
            this.numtel = numtel;
            this.sexe = sexe;
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
    }
}
