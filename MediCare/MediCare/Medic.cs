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
            IQueryable<Medicaments> ListMed = from nomMed in Globals.DataClass.Medicaments
                                              select nomMed;
            foreach ( Medicaments p in ListMed )
            {
                this.medoc.Add(p.nom);
            }
        
        }
    }
}
