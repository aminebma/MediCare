﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MediCare
{
    class Medecin2
    {
        public void AddMed(string nom, string prenom, DateTime date, string adresse, string num_tel, string sexe, string key, string username, string password,string email)
        {
            nom=nom.ToUpper();
            prenom=prenom.ToUpper();
            Personne t = new Personne
            {
                nom = nom,
                prenom = prenom,
                dateNaissance = date,
                adresse = adresse,
                telephone = int.Parse(num_tel),
                sexe = sexe
            };
            Globals.DataClass.Personne.InsertOnSubmit(t);
            Globals.DataClass.SubmitChanges();

            Medecin tabmedecin = new Medecin
            {
                active = true,
                key = key,
                username = username,
                password = password,
                email = email,
                IdPersonne = t.Id
            };
            Globals.DataClass.Medecin.InsertOnSubmit(tabmedecin);
            Globals.DataClass.SubmitChanges();
        }

        //il me faudra une fonction de vérification du nom d'utilisateur et du mot de passe

        public bool VerifMed(string nom, string mot_pass)
        {
            string cryptedPass = EncryptPassword(mot_pass);
            IQueryable<Medecin> medverif = (from Medecin in Globals.DataClass.Medecin
                                            where Medecin.username == nom && Medecin.password == cryptedPass
                                            select Medecin);
            int nbr = medverif.Count();
            if (nbr == 0) return false;
            else
            {
                Globals.IdMedecin = (byte)medverif.First<Medecin>().Id;
                return true;
            }
        }

        public bool ModifMed(string username, string mot_pass, string new_password)
        {
            string cryptedPass = EncryptPassword(mot_pass);
            IQueryable<Medecin> medModif = (from medecin in Globals.DataClass.Medecin
                                            where medecin.username == username && medecin.password == cryptedPass
                                            select medecin);
            if (medModif.Count() != 0)
            {
                medModif.First().password = EncryptPassword(new_password);
                //Globals.DataClass.Medecin
                Globals.DataClass.SubmitChanges();
                return true;
            }
            else return false;


            /* medModif.ToList();
             List <Medecin> list = medModif.ToList<Medecin>();
           foreach (Medecin med in list)
           {
               med.username = new_username;
               med.password = new_password;
               Globals.DataClass.SubmitChanges();*/
        }

        public List<Personne> RechercherMedecin(string user)
        {
            IQueryable<Personne> medecins = (from med in Globals.DataClass.Medecin
                                             where med.username.Contains(user)
                                             join personne in Globals.DataClass.Personne on med.IdPersonne equals personne.Id
                                             select personne);
            return medecins.ToList<Personne>();
        }
		public List<Medecins> RechercheToutMedecin ()
		{
			List<Medecins> list = new List<Medecins>();


			IQueryable<Medecin> medcin = (from med in Globals.DataClass.Medecin
											  select med);
			

			foreach ( Medecin p in medcin)
			{
				Personne personne = (from per in Globals.DataClass.Personne
									 where per.Id == p.IdPersonne
									 select per).First<Personne>();

				Medecins q = new Medecins(personne.nom, personne.prenom, (DateTime)personne.dateNaissance, personne.adresse, (int)personne.telephone, personne.sexe, (Boolean)p.active, p.key, p.username, p.password);
				list.Add(q);

			}
			return list;
		}
		public List<Patients> DossiersMedical()
		{
			List<Patients> list = new List<Patients>();


			IQueryable<Patient> pat = (from med in Globals.DataClass.Patient
										  select med);


			foreach (Patient p in pat)
			{
				Personne personne = (from per in Globals.DataClass.Personne
									 where per.Id == p.IdPersonne
									 select per).First<Personne>();

			       Patients q = new Patients(personne.nom, personne.prenom, (DateTime)personne.dateNaissance, personne.adresse, (int)personne.telephone, personne.sexe, (int)p.taille, (int)p.poids, p.groupage, p.maladie, p.etatSante);
				list.Add(q);

			}
			return list;
		}
        public string EncryptPassword(string password)
        {
            byte[] passBytes = Encoding.Unicode.GetBytes(password);
            string encryptedPass = Convert.ToBase64String(passBytes);
            return encryptedPass;
        }

    }
}












