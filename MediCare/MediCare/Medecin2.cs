using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MediCare
{
    class Medecin2
    {
        public void AddMed(String nom, String prenom, string date, String adresse, string num_tel, String sexe, string key, string username, string password)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataclass = new MCDataClassDataContext(con);
            Personne t = new Personne
            {
                nom = nom,
                prenom = prenom,
                dateNaissance = DateTime.Parse(date),
                adresse = adresse,
                telephone = int.Parse(num_tel),
                sexe = sexe,




            };
            dataclass.Personne.InsertOnSubmit(t);
            dataclass.SubmitChanges();

            Medecin tabmedecin = new Medecin();
            tabmedecin.active = true;
            tabmedecin.key = key;

            tabmedecin.username = username;
            tabmedecin.password = password;
            tabmedecin.IdPersonne = t.Id;
            dataclass.Medecin.InsertOnSubmit(tabmedecin);
            dataclass.SubmitChanges();
        }
    }
    /*   public void ModifRdv(string username, string password, string newusername, string newpassword,string newtel)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            Medecin medToModify = (from personne in dataClass.Personne 
                                      where username ==  && password == personne.prenom
                                      join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                      join rdv in dataClass.RendezVous on personne.Id equals rdv.IdPatient
                                      orderby rdv.Id descending
                                      select rdv).First<RendezVous>();
            rdvToModify.Date = newDate;
            dataClass.RendezVous.InsertOnSubmit(rdvToModify);
            dataClass.SubmitChanges();
        }

        public void ModifRdv(string nomPatient, string prenomPatient, DateTime newDate)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            RendezVous rdvToModify = (from personne in dataClass.Personne -
                                      where nomPatient == personne.nom && prenomPatient == personne.prenom
                                      join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                      join rdv in dataClass.RendezVous on personne.Id equals rdv.IdPatient
                                      orderby rdv.Id descending
                                      select rdv).First<RendezVous>();
            rdvToModify.Date = newDate;
            dataClass.RendezVous.InsertOnSubmit(rdvToModify);
            dataClass.SubmitChanges();
        }
    }*/
}
