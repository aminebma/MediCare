using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
    class Perso
    {

        private string nom;
        private string prenom;
        private DateTime datenaiss;
        private string adresse;
        private int numtel;
        private string sexe;
        public Perso (string nom,string prenom1 , DateTime datenaiss1,string adresse1, int numtel1,string sexe1)
        {
            this.nom = nom;
            this.prenom = prenom1;
            this.datenaiss = datenaiss1;
            this.adresse = adresse1;
            this.numtel = numtel1;
            this.sexe = sexe1;
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
