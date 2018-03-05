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
            /*string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataclass = new MCDataClassDataContext(con);
            Personne tabpersonne = new Personne
            {
                nom = nom,
                prenom = prenom,
                date = DateTime.Parse(date),
                adresse = adresse,
                telephone = int.Parse(num_tel),
                sexe = sexe,
            };
            dataclass.Personne.InsertOnSubmit(tabpersonne);
            dataclass.SubmitChanges();

            Patient tabpatient = new Patient();
            Personne actualpersonne = (from pers in dataclass.Personne
                                       orderby pers.Id descending
                                       select pers).First();
            tabpatient.taille = float.Parse(taille);
            tabpatient.poids_ = float.Parse(poids);
            tabpatient.groupage = groupage;
            tabpatient.maladie = maladie;
            tabpatient.etatSante = etat_sante;
            tabpatient.IdPersonne = actualpersonne.Id;
            dataclass.Patient.InsertOnSubmit(tabpatient);
            dataclass.SubmitChanges(); */
        } 
    } 

}
