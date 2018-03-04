using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
    class Medecin2
    {
        public void AddMed( string  key_ , Boolean active, string username, string password,  int IdPersonne)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext db = new MCDataClassDataContext(con);
            Medecin t = new Medecin
            {
                 key_ = key_ ,
                active = active,
                username = username,
                password = password,
                IdPersonne = IdPersonne,
                
            };
            db.Medecin.InsertOnSubmit(t);
            db.SubmitChanges();
        }



    }
}
