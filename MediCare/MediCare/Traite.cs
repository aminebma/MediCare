using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
    class Traite
    {
            private string dose;
            private string indication;
            private string nomMed;


        public Traite()
        {
        }

        public Traite(string dose, string indication, string nomMed)
            {
                this.dose = dose;
                this.indication = indication;
                this.nomMed = nomMed;

            }

            public string Dose
            {
                get { return dose; }
                set { dose = value; }
            }

            public string Indication
            {
                get { return indication; }
                set { indication = value; }
            }
            public string NomMed
            {
                get { return nomMed; }
                set { nomMed = value; }
            }
    }
}
