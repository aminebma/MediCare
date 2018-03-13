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
        public void AddMed(string nom, string prenom, DateTime date, string adresse, string num_tel, string sexe, string key, string username, string password)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataclass = new MCDataClassDataContext(con);
            Personne t = new Personne
            {
                nom = nom,
                prenom = prenom,
                dateNaissance = date,
                adresse = adresse,
                telephone = int.Parse(num_tel),
                sexe = sexe
            };
            dataclass.Personne.InsertOnSubmit(t);
            dataclass.SubmitChanges();

            Medecin tabmedecin = new Medecin
            {
                active = true,
                key = key,
                username = username,
                password = password,
                IdPersonne = t.Id
            };
            dataclass.Medecin.InsertOnSubmit(tabmedecin);
            dataclass.SubmitChanges();
        }

        //il me faudra une fonction de vérification du nom d'utilisateur et du mot de passe

        public bool VerifMed(string mot_pass, string nom)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataclass = new MCDataClassDataContext(con);

            IQueryable<Medecin> medverif = (from medecin in dataclass.Medecin
                                            where medecin.username == nom && medecin.password == mot_pass
                                            select medecin);
            medverif.ToList();
            if (medverif == null)
            {
                return false;
            }
            else
            {
                return true;
            }



        }



        public void ModifMed(string nom, string mot_pass, string new_username, string new_password)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<Medecin> medToModify = (from medecin in dataClass.Medecin
                                               where nom == medecin.username && mot_pass == medecin.password
                                               select medecin);
            List<Medecin> list = medToModify.ToList<Medecin>();
            //medToModify.username = new_username;
            foreach(Medecin med in list)
            {
                med.password = new_password;
                dataClass.SubmitChanges();
            }
        }


    } 
    
}

    



   
 
      

