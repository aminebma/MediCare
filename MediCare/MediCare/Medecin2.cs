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

        public bool VerifMed(string nom, string mot_pass)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataclass = new MCDataClassDataContext(con);

            IQueryable<Medecin> medverif = (from Medecin in dataclass.Medecin
                                            where Medecin.username == nom && Medecin.password == mot_pass
                                            select Medecin);
            int nbr = medverif.Count();
            if (nbr == 0) return false;
            else return true;


            /*  bool rep = medverif.Any();
              if (rep == false) return false;
              else return true;*/
            // List<int> liste = medverif.ToList < int > ();
            //  int rep = medverif.AsQueryable().DefaultIfEmpty(1);

            //  if ( medverif.AsQueryable().FirstOrDefault()== null) return false;
            //   else return true;
            //   FirstOrDefault<Medecin>();
            //List<Medecin> s = medverif.ToList();
            /*    if (s.Count()== 0) {
                    return true ;
                }
                else { return false ; }
            }*/

            //  Array < Medecin >  array =  medverif.ToArray<Medecin>();
            //List<Medecin> list = medverif.ToList<Medecin>();

            /*   //  String.IsNullOrEmpty(s)
             if (String.IsNullOrEmpty(s))
             {
                 return true;
             }
             else
             {*/
        }




        public void ModifMed(string username2, string mot_pass, string new_password)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            Medecin medModif = (from medecin in dataClass.Medecin
                                where medecin.username == username2 && medecin.password == mot_pass
                                select medecin).First<Medecin>();
            medModif.password = new_password;
            dataClass.SubmitChanges();

            /* medModif.ToList();
             List <Medecin> list = medModif.ToList<Medecin>();
           foreach (Medecin med in list)
           {
               med.username = new_username;
               med.password = new_password;
               dataClass.SubmitChanges();*/
        }





    }


}












