using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MediCare
{
    class Agenda
    {
        //public Agenda(/*byte idMedecin*/)
        //{
        //    string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
        //    DataClassesDataContext dataClass = new DataClassesDataContext(con);
        //    IQueryable<RendezVous> list = from p in dataClass.RendezVous
        //                                  /*where idMedecin==p.idMedecin*/
        //                                  select p;
        //}

        List<string> mois = new List<string>{"Janvier","Février","Mars","Avril","Mai","Juin","Juillet","Aout","Septembre","Octobre","Novembre","Décembre"};

        public int ConvertMoisToNum(string month)
        {
            return mois.IndexOf(month)+1;
        }

        public string ConvertNumToMois(DateTime date)
        {
            return mois[date.Month - 1];
        }

        public void AddRdv(DateTime date, byte idMedecin, string nomPatient, string prenomPatient, bool important, string notes)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            Personne patientRdv = (from personne in dataClass.Personne
                                   where nomPatient.Equals(personne.nom) && prenomPatient.Equals(personne.prenom)
                                   join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                   select personne).First();
            RendezVous rdv = new RendezVous
            {
                Date = date,
                IdPatient = patientRdv.Id,
                IdMedecin = idMedecin,
                Important = important,
                Fait = false,
                Note = notes
            };
            dataClass.RendezVous.InsertOnSubmit(rdv);
            dataClass.SubmitChanges();
        }

        public void SuppRdv(string nomPatient, string prenomPatient)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            RendezVous rdvToDelete = (from personne in dataClass.Personne
                                      where nomPatient == personne.nom && prenomPatient == personne.prenom
                                      join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                      join rdv in dataClass.RendezVous on personne.Id equals rdv.Id
                                      orderby rdv.Id descending
                                      select rdv).First();
            dataClass.RendezVous.DeleteOnSubmit(rdvToDelete);
            dataClass.SubmitChanges();

        }

        public void SuppRdv(DateTime date)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            RendezVous rdvToDelete = (from rdv in dataClass.RendezVous
                                      where date == rdv.Date
                                      select rdv).First();
            dataClass.RendezVous.DeleteOnSubmit(rdvToDelete);
            dataClass.SubmitChanges();
        }

        public void ModifRdv(string nomPatient, string prenomPatient, DateTime newDate)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            RendezVous rdvToModify = (from personne in dataClass.Personne
                                      where nomPatient == personne.nom && prenomPatient == personne.prenom
                                      join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                      join rdv in dataClass.RendezVous on personne.Id equals rdv.Id
                                      orderby rdv.Id descending
                                      select rdv).First();
            rdvToModify.Date = newDate;
            dataClass.RendezVous.InsertOnSubmit(rdvToModify);
            dataClass.SubmitChanges();
        }

        public void ModifRdv(DateTime date, DateTime newDate)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            RendezVous rdvToModify = (from rdv in dataClass.RendezVous
                                      where date == rdv.Date
                                      select rdv).First();
            rdvToModify.Date = newDate;
            dataClass.RendezVous.InsertOnSubmit(rdvToModify);
            dataClass.SubmitChanges();
        }

        //public void ModifRdv(DateTime date, DateTime newDate, byte idMedecin)
        //{
        //    string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
        //    MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
        //    RendezVous rdvToModify = (from rdv in dataClass.RendezVous
        //                              where date == rdv.Date && idMedecin == rdv.IdMedecin
        //                              select rdv).First();
        //    rdvToModify.Date = newDate;
        //    dataClass.RendezVous.InsertOnSubmit(rdvToModify);
        //    dataClass.SubmitChanges();
        //}
    }
}
