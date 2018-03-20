using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
    public class Globals
    {
        
            private static int _id;
            public static int Id
            {
                get
                {
                    return _id;
                }
                set
                {
                    _id = value;
                }
            }

            private static byte _idMedecin;
            public static byte IdMedecin
            {
                get
                {
                    return _idMedecin;
                }
                set
                {
                    _idMedecin = value;
                }
            }

            private static string _nomPatient;
            public static string NomPatient
            {
                get
                {
                    return _nomPatient;
                }
                set
                {
                    _nomPatient = value;
                }
            }
            private static string _prenomPatient;
            public static string PrenomPatient
            {
                get
                {
                    return _prenomPatient;
                }
                set
                {
                    _prenomPatient = value;
                }
            }
            private static string _nomMedecin;
            public static string NomMedecin
            {
                get
                {
                    return _nomMedecin;
                }
                set
                {
                    _nomMedecin= value;
                }
            }
            private static string _prenomMedecin;
            public static string PrenomMedecin
            {
                get
                {
                    return _prenomMedecin;
                }
                set
                {
                    _prenomMedecin = value;
                }
            }
    }
}
