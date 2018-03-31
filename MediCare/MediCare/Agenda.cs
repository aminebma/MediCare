using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
    class Agenda
    {
        public List<RendezVous> listRdv;

        public Agenda(byte idMedecin)
        {
            listRdv = (from p in Globals.DataClass.RendezVous
                       where idMedecin == p.IdMedecin
                       select p).ToList<RendezVous>();
        }

        public Agenda() { }

        List<string> mois = new List<string> { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Décembre" };

        public int ConvertMoisToNum(string month)
        {
            return mois.IndexOf(month) + 1;
        }

        public string ConvertNumToMois(DateTime date)
        {
            return mois[date.Month - 1];
        }

        public bool AddRdv(DateTime date, byte idMedecin, string nomPatient, string prenomPatient, bool important, string notes)
        {
            nomPatient=nomPatient.ToUpper();
            prenomPatient=prenomPatient.ToUpper();
            IQueryable<RendezVous> checkRdv = (from rdvCheck in Globals.DataClass.RendezVous
                                               where date == rdvCheck.Date
                                               select rdvCheck);
            if (checkRdv.Count() == 0)
            {
                IQueryable<Patient> patientRdv = (from personne in Globals.DataClass.Personne
                                                  where nomPatient == personne.nom && prenomPatient == personne.prenom
                                                  join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                                  select patient);
                if (patientRdv.Count() != 0)
                {
                    Patient pers = patientRdv.First<Patient>();
                    RendezVous rdv = new RendezVous
                    {
                        Date = date,
                        IdPatient = pers.Id,
                        IdMedecin = idMedecin,
                        Important = important,
                        Fait = false,
                        Note = notes,
                        Notified = false
                    };

                    Globals.DataClass.RendezVous.InsertOnSubmit(rdv);
                    Globals.DataClass.SubmitChanges();


                    MPRendezVous mPRdv = new MPRendezVous
                    {
                        IdMedecin = idMedecin,
                        IdPatient = pers.Id,
                        IdRendezVous = rdv.Id
                    };

                    Globals.DataClass.MPRendezVous.InsertOnSubmit(mPRdv);
                    Globals.DataClass.SubmitChanges();
                }
                else
                {

                    PersonneClasse newPatient = new PersonneClasse();
                    newPatient.AddPatientPersonne(nomPatient, prenomPatient, "01/01/1998", "Indéfini", "0123456789", true, "170", "60", "/", "Indéfini", "Indéfini");
                    Patient addedPatient = (from personne in Globals.DataClass.Personne
                                            where nomPatient == personne.nom && prenomPatient == personne.prenom
                                            join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                            select patient).First<Patient>();

                    RendezVous rdv = new RendezVous
                    {
                        Date = date,
                        IdPatient = addedPatient.Id,
                        IdMedecin = idMedecin,
                        Important = important,
                        Fait = false,
                        Note = notes,
                        Notified = false
                    };

                    Globals.DataClass.RendezVous.InsertOnSubmit(rdv);
                    Globals.DataClass.SubmitChanges();

                    MPRendezVous mPRdv = new MPRendezVous
                    {
                        IdMedecin = idMedecin,
                        IdPatient = addedPatient.Id,
                        IdRendezVous = rdv.Id
                    };

                    Globals.DataClass.MPRendezVous.InsertOnSubmit(mPRdv);
                    Globals.DataClass.SubmitChanges();
                }
                //System.IO.File.Copy($@"{Globals.CurrentDirectoryPath}\\MCDatabase.mdf", $@"{Globals.CurrentDirectoryPath}\\restauration\\MCDatabase.mdf", true);
                return true;
            }
            else return false;
        }

        public bool AddRdv(DateTime date, byte idMedecin, string notes)
        {
            IQueryable<RendezVous> checkRdv = (from rdvCheck in Globals.DataClass.RendezVous
                                               where date == rdvCheck.Date
                                               select rdvCheck);
            if (checkRdv.Count() == 0)
            {
                RendezVous rdvPerso = new RendezVous
                {
                    Date = date,
                    IdPatient = 0,
                    IdMedecin = idMedecin,
                    Important = false,
                    Fait = false,
                    Note = notes,
                    Notified = false
                };
                Globals.DataClass.RendezVous.InsertOnSubmit(rdvPerso);
                Globals.DataClass.SubmitChanges();
                //System.IO.File.Copy($@"{Globals.CurrentDirectoryPath}\\MCDatabase.mdf", $@"{Globals.CurrentDirectoryPath}\\restauration\\MCDatabase.mdf", true);
                return true;
            }
            else return false;
        }

        public void SuppRdv(string nomPatient, string prenomPatient)
        {
            nomPatient=nomPatient.ToUpper();
            prenomPatient=prenomPatient.ToUpper();
            RendezVous rdvToDelete = (from personne in Globals.DataClass.Personne
                                      where nomPatient == personne.nom && prenomPatient == personne.prenom
                                      join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                      join rdv in Globals.DataClass.RendezVous on patient.Id equals rdv.IdPatient
                                      orderby rdv.Id descending
                                      select rdv).First<RendezVous>();

            MPRendezVous mPRendezVous = (from rdv in Globals.DataClass.MPRendezVous
                                         where rdvToDelete.Id == rdv.IdRendezVous && rdvToDelete.IdPatient == rdv.IdPatient && rdvToDelete.IdMedecin == rdv.IdMedecin
                                         orderby rdv.Id descending
                                         select rdv).First<MPRendezVous>();

            Globals.DataClass.MPRendezVous.DeleteOnSubmit(mPRendezVous);
            Globals.DataClass.SubmitChanges();
            Globals.DataClass.RendezVous.DeleteOnSubmit(rdvToDelete);
            Globals.DataClass.SubmitChanges();
            //System.IO.File.Copy($@"{Globals.CurrentDirectoryPath}\\MCDatabase.mdf", $@"{Globals.CurrentDirectoryPath}\\restauration\\MCDatabase.mdf", true);
        }

        public void SuppRdv(DateTime date)
        {
            RendezVous rdvToDelete = (from rdv in Globals.DataClass.RendezVous
                                      where date == rdv.Date
                                      select rdv).First<RendezVous>();

            MPRendezVous mPRendezVous = (from rdv in Globals.DataClass.MPRendezVous
                                         where rdvToDelete.Id == rdv.IdRendezVous && rdvToDelete.IdPatient == rdv.IdPatient && rdvToDelete.IdMedecin == rdv.IdMedecin
                                         orderby rdv.Id descending
                                         select rdv).First<MPRendezVous>();

            Globals.DataClass.MPRendezVous.DeleteOnSubmit(mPRendezVous);
            Globals.DataClass.SubmitChanges();
            Globals.DataClass.RendezVous.DeleteOnSubmit(rdvToDelete);
            Globals.DataClass.SubmitChanges();
            //System.IO.File.Copy($@"{Globals.CurrentDirectoryPath}\\MCDatabase.mdf", $@"{Globals.CurrentDirectoryPath}\\restauration\\MCDatabase.mdf", true);
        }

        public void ModifRdv(string nomPatient, string prenomPatient, DateTime newDate)
        {
            nomPatient=nomPatient.ToUpper();
            prenomPatient=prenomPatient.ToUpper();
            RendezVous rdvToModify = (from personne in Globals.DataClass.Personne
                                      where nomPatient == personne.nom && prenomPatient == personne.prenom
                                      join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                      join rdv in Globals.DataClass.RendezVous on patient.Id equals rdv.IdPatient
                                      orderby rdv.Id descending
                                      select rdv).First<RendezVous>();

            rdvToModify.Date = newDate;
            Globals.DataClass.SubmitChanges();
            //System.IO.File.Copy($@"{Globals.CurrentDirectoryPath}\\MCDatabase.mdf", $@"{Globals.CurrentDirectoryPath}\\restauration\\MCDatabase.mdf", true);
        }

        public void ModifRdv(DateTime date, DateTime newDate)
        {
            RendezVous rdvToModify = (from rdv in Globals.DataClass.RendezVous
                                      where date == rdv.Date
                                      select rdv).First<RendezVous>();
            rdvToModify.Date = newDate;
            Globals.DataClass.SubmitChanges();
            //System.IO.File.Copy($@"{Globals.CurrentDirectoryPath}\\MCDatabase.mdf", $@"{Globals.CurrentDirectoryPath}\\restauration\\MCDatabase.mdf", true);
        }

        //public void ModifRdv(DateTime date, DateTime newDate, byte idMedecin)
        //{
        //    string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
        //    RendezVous rdvToModify = (from rdv in Globals.DataClass.RendezVous
        //                              where date == rdv.Date && idMedecin == rdv.IdMedecin
        //                              select rdv).First();
        //    rdvToModify.Date = newDate;
        //    Globals.DataClass.RendezVous.InsertOnSubmit(rdvToModify);
        //    Globals.DataClass.SubmitChanges();
        //}

        public List<Personne> RechercherPatientNomBDD(string nom)
        {
            nom=nom.ToUpper();
            IQueryable<Personne> patients = (from personne in Globals.DataClass.Personne
                                             where personne.nom.Contains(nom)
                                             join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                             select personne);
            return patients.ToList<Personne>();
        }

        public List<Personne> RechercherPatientNom(string nom)
        {
            nom=nom.ToUpper();
            List<Personne> filtrePatient = new List<Personne>();
            foreach (Personne patient in Globals.ListPatients)
                if ((patient.nom).Contains(nom))
                    filtrePatient.Add(patient);
            return filtrePatient;
        }

        public List<Personne> RechercherPatientPrenomBDD(string prenom)
        {
            prenom=prenom.ToUpper();
            IQueryable<Personne> patients = (from personne in Globals.DataClass.Personne
                                             where personne.prenom.Contains(prenom)
                                             join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                             select personne);
            return patients.ToList<Personne>();
        }

        public List<Personne> RechercherPatientPrenom(string prenom)
        {
            prenom=prenom.ToUpper();
            List<Personne> filtrePatient = new List<Personne>();
            foreach (Personne patient in Globals.ListPatients)
                if ((patient.prenom).Contains(prenom))
                    filtrePatient.Add(patient);
            return filtrePatient;
        }

        public List<Personne> RechercherPatientBDD(string nom, string prenom)
        {
            nom=nom.ToUpper();
            prenom=prenom.ToUpper();
            IQueryable<Personne> patients = (from personne in Globals.DataClass.Personne
                                             where personne.nom.Contains(nom) && personne.prenom.Contains(prenom)
                                             join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                             select personne);
            return patients.ToList<Personne>();
        }

        public List<Personne> RechercherPatient(string nom)
        {
            nom=nom.ToUpper();
            List<Personne> filtrePatient = new List<Personne>();
            foreach (Personne patient in Globals.ListPatients)
                if ((patient.nom+" "+patient.prenom).Contains(nom))
                    filtrePatient.Add(patient);
            return filtrePatient;
        }
        
    }
}
