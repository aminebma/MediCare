using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace MediCare
{
    class Medic
    {
        public List<String> medoc = new List<String>();
        public Medic()
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            IQueryable<Medicaments> ListMed = from nomMed in dataClass.Medicaments
                                              select nomMed;
            foreach ( Medicaments p in ListMed )
            {
                this.medoc.Add(p.nom);
            }
        
        }
    }
}
