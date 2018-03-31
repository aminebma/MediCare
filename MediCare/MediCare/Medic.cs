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

        public List<Medicaments> RechercheMedicamentBDD(string nom)
        {
            nom = nom.ToUpper();
            IQueryable<Medicaments> medicaments = (from medic in Globals.DataClass.Medicaments
                                             where medic.nom.Contains(nom)
                                             select medic);
            return medicaments.ToList<Medicaments>();
        }


        public List<Medicaments> RechercheMedicament(string nom)
        {
            nom = nom.ToUpper();
            List<Medicaments> filtremedic = new List<Medicaments>();
            foreach (Medicaments medic in Globals.ListMedicaments)
                if ((medic.nom).Contains(nom))
                    filtremedic.Add(medic);
            return filtremedic;
        }
    }
}
