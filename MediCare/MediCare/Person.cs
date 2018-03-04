using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MediCare
{
    class Person
    {
        public void AddPersonne(String nom, String prenom, DateTime date_naissance, String adresse, int num_tel, String sexe)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataclass = new MCDataClassDataContext(con);
            Personne table = new Personne();
            {
               
            }
            
        }
    }

}
