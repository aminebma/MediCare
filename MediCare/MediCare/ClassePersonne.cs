using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MediCare
{
    class PersonneClasse
    {
        public void AddPatientPersonne(String nom, String prenom, string date, String adresse, string num_tel, string homme, string taille, string poids, string groupage, string maladie, string etat_sante)
        {

            Personne tabpersonne = new Personne
            {
                nom = nom,
                prenom = prenom,
                dateNaissance = DateTime.Parse(date),
                adresse = adresse,
                telephone = int.Parse(num_tel),
                sexe =homme
            };
            

            Globals.DataClass.Personne.InsertOnSubmit(tabpersonne);
            Globals.DataClass.SubmitChanges();

            Patient tabpatient = new Patient
            {
                taille = float.Parse(taille),
                poids = float.Parse(poids),
                groupage = groupage,
                maladie = maladie,
                etatSante = etat_sante,
                IdPersonne = tabpersonne.Id
            };

            Globals.DataClass.Patient.InsertOnSubmit(tabpatient);
            Globals.DataClass.SubmitChanges();
        }
        public void AddPatientPersonne(String nom, String prenom)
        {
           
            Personne tabpersonne = new Personne
            {
                nom = nom,
                prenom = prenom
            };


            Globals.DataClass.Personne.InsertOnSubmit(tabpersonne);
            Globals.DataClass.SubmitChanges();

            Patient tabpatient = new Patient
            {
                IdPersonne = tabpersonne.Id
            };

            Globals.DataClass.Patient.InsertOnSubmit(tabpatient);
            Globals.DataClass.SubmitChanges();
        }

        public void SuppRdv(string nomPatient, string prenomPatient)
        {
            nomPatient = nomPatient.ToUpper();
            prenomPatient = prenomPatient.ToUpper();
            IQueryable<RendezVous> r = (from personne in Globals.DataClass.Personne
                                        where personne.nom.Contains(nomPatient) && personne.prenom.Contains(prenomPatient)
                                        join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                        join rdv in Globals.DataClass.RendezVous on patient.Id equals rdv.IdPatient
                                        orderby rdv.Id descending
                                        select rdv);
            
            foreach (RendezVous p in r )
            {
                IQueryable<MPRendezVous> mprdv = (from rdv in Globals.DataClass.MPRendezVous
                                                         where  p.Id == rdv.IdRendezVous && p.IdPatient == rdv.IdPatient &&  p.IdMedecin == rdv.IdMedecin
                                                         orderby rdv.Id descending
                                                         select rdv);
                foreach (MPRendezVous l in mprdv)
                {
                    Globals.DataClass.MPRendezVous.DeleteOnSubmit(l);
                    Globals.DataClass.SubmitChanges();
                }
                Globals.DataClass.RendezVous.DeleteOnSubmit(p);
                Globals.DataClass.SubmitChanges();
            }

            //List<Personne> filtrePatient = new List<Personne>();
            
            //foreach (Personne patient in Globals.ListPatients)
            //{
            //    if ((patient.nom).Contains(nom))
            //    {
            //        filtrePatient.Add(patient);
            //        nbElemMax++;
            //        if (nbElemMax > 100) break;
            //    }
            //}

            //MPRendezVous mPRendezVous = (from rdv in Globals.DataClass.MPRendezVous
            //                             where rdvToDelete.Id == rdv.IdRendezVous && rdvToDelete.IdPatient == rdv.IdPatient && rdvToDelete.IdMedecin == rdv.IdMedecin
            //                             orderby rdv.Id descending
            //                             select rdv).First<MPRendezVous>();

            //Globals.DataClass.MPRendezVous.DeleteOnSubmit(mPRendezVous);
            //Globals.DataClass.SubmitChanges();
            //Globals.DataClass.RendezVous.DeleteOnSubmit(rdvToDelete);
            //Globals.DataClass.SubmitChanges();
        }


        public void SuppConsultation(string nomPatient, string prenomPatient)
        {
            Personne Pers = (from personne in Globals.DataClass.Personne
                             where nomPatient == personne.nom && prenomPatient == personne.prenom
                             select personne).First<Personne>();

            Patient Pat = (from patient in Globals.DataClass.Patient
                           where patient.IdPersonne == Pers.Id
                           select patient).First<Patient>();

            MPConsultation mpcnsltToDelete = (from cns in Globals.DataClass.Consultation
                                              
                                              join mpc in Globals.DataClass.MPConsultation on cns.Id equals mpc.IdConsultation
                                              where mpc.IdPatient == Pat.Id
                                              select mpc).First<MPConsultation>();

            Consultation cnsltToDelete = (from cns in Globals.DataClass.Consultation
                                          where cns.Id == mpcnsltToDelete.IdConsultation
                                          select cns).First<Consultation>();
            IQueryable<Radio> rdi = (from radio in Globals.DataClass.Radio
                                     where radio.IdConsultation == cnsltToDelete.Id
                                     select radio);
            foreach (Radio p in rdi)
            {
                Globals.DataClass.Radio.DeleteOnSubmit(p);
                Globals.DataClass.SubmitChanges();
            }

            IQueryable<Traitement> trait = (from traitement in Globals.DataClass.Traitement
                                            where traitement.IdConsultation == cnsltToDelete.Id

                                            select traitement);

            foreach (Traitement p in trait)
            {
                Globals.DataClass.Traitement.DeleteOnSubmit(p);
                Globals.DataClass.SubmitChanges();
            }



            Globals.DataClass.MPConsultation.DeleteOnSubmit(mpcnsltToDelete);
            Globals.DataClass.SubmitChanges();
            Globals.DataClass.Consultation.DeleteOnSubmit(cnsltToDelete);
            Globals.DataClass.SubmitChanges();
            

        }
        public void SuppPatient(string nom, string prenom)
        {
            nom = nom.ToUpper();
            prenom = prenom.ToUpper();
            Personne personasupp = (from pers in Globals.DataClass.Personne
                                    where nom == pers.nom && prenom == pers.prenom
                                    orderby pers.Id descending
                                    select pers).First<Personne>();
            Patient patienasup = (from pat in Globals.DataClass.Patient
                                  where personasupp.Id == pat.IdPersonne
                                  select pat).First<Patient>();
            SuppRdv(nom, prenom);
            IQueryable<Consultation> consult = (from mpc in Globals.DataClass.MPConsultation
                                                where mpc.IdPatient == patienasup.Id
                                                join cns in Globals.DataClass.Consultation on mpc.IdConsultation equals cns.Id
                                                select cns);
            foreach (Consultation p in consult)
            {
                SuppConsultation(nom, prenom);
            }

            Globals.DataClass.Patient.DeleteOnSubmit(patienasup);
            Globals.DataClass.SubmitChanges();
            Globals.DataClass.Personne.DeleteOnSubmit(personasupp);
            Globals.DataClass.SubmitChanges();
        }
        public List<Personne> RechercherPatientBDD(string nom, string prenom)
        {
            nom = nom.ToUpper();
            prenom = prenom.ToUpper();
            IQueryable<Personne> patients = (from personne in Globals.DataClass.Personne
                                             where personne.nom.Contains(nom) && personne.prenom.Contains(prenom)
                                             join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                             select personne);
            return patients.ToList<Personne>();
        }
    }
}



