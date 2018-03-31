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
        public void AddPatientPersonne(String nom, String prenom, string date, String adresse, string num_tel, bool homme, string taille, string poids, string groupage, string maladie, string etat_sante)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataclass = new MCDataClassDataContext(con);

            Personne tabpersonne = new Personne
            {
                nom = nom,
                prenom = prenom,
                dateNaissance = DateTime.Parse(date),
                adresse = adresse,
                telephone = int.Parse(num_tel)


            };
            if (homme)
            {
                tabpersonne.sexe = "Homme";
            }
            else
            {
                tabpersonne.sexe = "Femme";
            }

            dataclass.Personne.InsertOnSubmit(tabpersonne);
            dataclass.SubmitChanges();

            Patient tabpatient = new Patient
            {
                taille = float.Parse(taille),
                poids = float.Parse(poids),
                groupage = groupage,
                maladie = maladie,
                etatSante = etat_sante,
                IdPersonne = tabpersonne.Id
            };

            dataclass.Patient.InsertOnSubmit(tabpatient);
            dataclass.SubmitChanges();
        }


        public void SuppConsultation(string nomPatient, string prenomPatient)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            Personne Pers = (from personne in dataClass.Personne
                             where nomPatient == personne.nom && prenomPatient == personne.prenom
                             select personne).First<Personne>();

            Patient Pat = (from patient in dataClass.Patient
                           where patient.IdPersonne == Pers.Id
                           select patient).First<Patient>();

            MPConsultation mpcnsltToDelete = (from cns in dataClass.Consultation
                                              
                                              join mpc in dataClass.MPConsultation on cns.Id equals mpc.IdConsultation
                                              where mpc.IdPatient == Pat.Id
                                              select mpc).First<MPConsultation>();

            Consultation cnsltToDelete = (from cns in dataClass.Consultation
                                          where cns.Id == mpcnsltToDelete.IdConsultation
                                          select cns).First<Consultation>();
            IQueryable<Radio> rdi = (from radio in dataClass.Radio
                                     where radio.IdConsultation == cnsltToDelete.Id
                                     select radio);
            foreach (Radio p in rdi)
            {
                dataClass.Radio.DeleteOnSubmit(p);
                dataClass.SubmitChanges();
            }

            IQueryable<Traitement> trait = (from traitement in dataClass.Traitement
                                            where traitement.IdConsultation == cnsltToDelete.Id

                                            select traitement);

            foreach (Traitement p in trait)
            {
                dataClass.Traitement.DeleteOnSubmit(p);
                dataClass.SubmitChanges();
            }



            dataClass.MPConsultation.DeleteOnSubmit(mpcnsltToDelete);
            dataClass.SubmitChanges();
            dataClass.Consultation.DeleteOnSubmit(cnsltToDelete);
            dataClass.SubmitChanges();
            

        }
        public void SuppPatient(string nom, string prenom)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            Personne personasupp = (from pers in dataClass.Personne
                                    where nom == pers.nom && prenom == pers.prenom
                                    orderby pers.Id descending
                                    select pers).First<Personne>();
            Patient patienasup = (from pat in dataClass.Patient
                                  where personasupp.Id == pat.IdPersonne

                                  select pat).First<Patient>();

            

            IQueryable<Consultation> consult = (from mpc in dataClass.MPConsultation
                                                where mpc.IdPatient == patienasup.Id
                                                join cns in dataClass.Consultation on mpc.IdConsultation equals cns.Id
                                                select cns);
            foreach (Consultation p in consult)
            {


                SuppConsultation(nom, prenom);
            }

            dataClass.Patient.DeleteOnSubmit(patienasup);
            dataClass.SubmitChanges();
            dataClass.Personne.DeleteOnSubmit(personasupp);
            dataClass.SubmitChanges();
        }
    }
}



