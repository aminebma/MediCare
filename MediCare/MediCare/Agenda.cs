using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
    class Agenda
    {
        //public Agenda(/*byte idMedecin*/)
        //{
        //    string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
        //    DataClassesDataContext dataClass = new DataClassesDataContext(con);
        //    IQueryable<RendezVous> list = from p in dataClass.RendezVous
        //                                  /*where idMedecin==p.idMedecin*/
        //                                  select p;
        //}

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
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<RendezVous> checkRdv = (from rdvCheck in dataClass.RendezVous
                                               where date == rdvCheck.Date
                                               select rdvCheck);
            if (checkRdv.Count() == 0)
            {
                IQueryable<Patient> patientRdv = (from personne in dataClass.Personne
                                                  where nomPatient == personne.nom && prenomPatient == personne.prenom
                                                  join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
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
                        Note = notes
                    };

                    dataClass.RendezVous.InsertOnSubmit(rdv);
                    dataClass.SubmitChanges();


                    MPRendezVous mPRdv = new MPRendezVous
                    {
                        IdMedecin = idMedecin,
                        IdPatient = pers.Id,
                        IdRendezVous = rdv.Id
                    };

                    dataClass.MPRendezVous.InsertOnSubmit(mPRdv);
                    dataClass.SubmitChanges();
                }
                else
                {

                    PersonneClasse newPatient = new PersonneClasse();
                    newPatient.AddPatientPersonne(nomPatient, prenomPatient, "01/01/1998", "Indéfini", "0123456789", "Indéfini", "170", "60", "/", "Indéfini", "Indéfini");
                    Patient addedPatient = (from personne in dataClass.Personne
                                            where nomPatient == personne.nom && prenomPatient == personne.prenom
                                            join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                            select patient).First<Patient>();

                    RendezVous rdv = new RendezVous
                    {
                        Date = date,
                        IdPatient = addedPatient.Id,
                        IdMedecin = idMedecin,
                        Important = important,
                        Fait = false,
                        Note = notes
                    };

                    dataClass.RendezVous.InsertOnSubmit(rdv);
                    dataClass.SubmitChanges();

                    MPRendezVous mPRdv = new MPRendezVous
                    {
                        IdMedecin = idMedecin,
                        IdPatient = addedPatient.Id,
                        IdRendezVous = rdv.Id
                    };

                    dataClass.MPRendezVous.InsertOnSubmit(mPRdv);
                    dataClass.SubmitChanges();
                }
                return true;
            }
            else return false;
        }

        public bool AddRdv(DateTime date, byte idMedecin, string notes)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<RendezVous> checkRdv = (from rdvCheck in dataClass.RendezVous
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
                    Note = notes
                };
                dataClass.RendezVous.InsertOnSubmit(rdvPerso);
                dataClass.SubmitChanges();
                return true;
            }
            else return false;
        }

        public void SuppRdv(string nomPatient, string prenomPatient)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            RendezVous rdvToDelete = (from personne in dataClass.Personne
                                      where nomPatient == personne.nom && prenomPatient == personne.prenom
                                      join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                      join rdv in dataClass.RendezVous on patient.Id equals rdv.IdPatient
                                      orderby rdv.Id descending
                                      select rdv).First<RendezVous>();

            MPRendezVous mPRendezVous = (from rdv in dataClass.MPRendezVous
                                         where rdvToDelete.Id == rdv.IdRendezVous && rdvToDelete.IdPatient == rdv.IdPatient && rdvToDelete.IdMedecin == rdv.IdMedecin
                                         orderby rdv.Id descending
                                         select rdv).First<MPRendezVous>();

            dataClass.MPRendezVous.DeleteOnSubmit(mPRendezVous);
            dataClass.SubmitChanges();
            dataClass.RendezVous.DeleteOnSubmit(rdvToDelete);
            dataClass.SubmitChanges();      
        }

        public void SuppRdv(DateTime date)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            RendezVous rdvToDelete = (from rdv in dataClass.RendezVous
                                      where date == rdv.Date
                                      select rdv).First<RendezVous>();

            MPRendezVous mPRendezVous = (from rdv in dataClass.MPRendezVous
                                         where rdvToDelete.Id == rdv.IdRendezVous && rdvToDelete.IdPatient == rdv.IdPatient && rdvToDelete.IdMedecin == rdv.IdMedecin
                                         orderby rdv.Id descending
                                         select rdv).First<MPRendezVous>();

            dataClass.MPRendezVous.DeleteOnSubmit(mPRendezVous);
            dataClass.SubmitChanges();
            dataClass.RendezVous.DeleteOnSubmit(rdvToDelete);
            dataClass.SubmitChanges();
        }

        public void ModifRdv(string nomPatient, string prenomPatient, DateTime newDate)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            RendezVous rdvToModify = (from personne in dataClass.Personne
                                      where nomPatient == personne.nom && prenomPatient == personne.prenom
                                      join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                      join rdv in dataClass.RendezVous on patient.Id equals rdv.IdPatient
                                      orderby rdv.Id descending
                                      select rdv).First<RendezVous>();

            rdvToModify.Date = newDate;
            dataClass.SubmitChanges();
        }

        public void ModifRdv(DateTime date, DateTime newDate)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            RendezVous rdvToModify = (from rdv in dataClass.RendezVous
                                      where date == rdv.Date
                                      select rdv).First<RendezVous>();
            rdvToModify.Date = newDate;
            dataClass.SubmitChanges();
        }

        //public void ModifRdv(DateTime date, DateTime newDate, byte idMedecin)
        //{
        //    string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
        //    MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
        //    RendezVous rdvToModify = (from rdv in dataClass.RendezVous
        //                              where date == rdv.Date && idMedecin == rdv.IdMedecin
        //                              select rdv).First();
        //    rdvToModify.Date = newDate;
        //    dataClass.RendezVous.InsertOnSubmit(rdvToModify);
        //    dataClass.SubmitChanges();
        //}

        public List<Personne> RechercherPatientNom(string nom)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<Personne> patients = (from personne in dataClass.Personne
                                             where personne.nom.Contains(nom)
                                             join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                             select personne);
            return patients.ToList<Personne>();
        }

        public List<Personne> RechercherPatientPrenom(string nom)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<Personne> patients = (from personne in dataClass.Personne
                                             where personne.prenom.Contains(nom)
                                             join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                             select personne);
            return patients.ToList<Personne>();
        }

        public List<Personne> RechercherPatient(string nom, string prenom)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<Personne> patients = (from personne in dataClass.Personne
                                             where personne.nom.Contains(nom) && personne.prenom.Contains(prenom)
                                             join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                             select personne);
            return patients.ToList<Personne>();
        }

    }
}
