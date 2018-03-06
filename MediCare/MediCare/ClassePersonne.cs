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
       public void AddPatientPersonne(String nom, String prenom, string date, String adresse, string num_tel, String sexe, string taille, string poids, string groupage, string maladie, string etat_sante)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataclass = new MCDataClassDataContext(con);

            Personne tabpersonne = new Personne
            {
                nom = nom,
                prenom = prenom,
                dateNaissance = DateTime.Parse(date),
                adresse = adresse,
                telephone = int.Parse(num_tel),
                sexe = sexe,
            };

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
    } 

}
