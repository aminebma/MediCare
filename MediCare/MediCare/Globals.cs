using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
    public class Globals
    {
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
                _nomMedecin = value;
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

        private static int _TempRappelRDV;
        public static int TempRappelRDV
        { 
            get
            {
                return _TempRappelRDV;
            }
            set
            {
                _TempRappelRDV = value;
            }
        }
        private static string _label;
        public static string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
            }
        }
        private static int _age;
        public static int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
            }
        }

        private static string _currentDirectoryPath;
        public static string CurrentDirectoryPath
        {
            get
            {
                return _currentDirectoryPath;
            }
            set
            {
                _currentDirectoryPath = value;
            }
        }

        private static MCDataClassDataContext _dataClass;
        public static MCDataClassDataContext DataClass
        {
            get
            {
                return _dataClass;
            }
            set
            {
                _dataClass = value;
            }
        }

        private static List<Personne> _listPatients;
        public static List<Personne> ListPatients
        {
            get
            {
                return _listPatients;
            }
            set
            {
                _listPatients = value;
            }
        }
        private static List<Medicaments> _listMedicaments;
        public static List<Medicaments> ListMedicaments
        {
            get
            {
                return _listMedicaments;
            }
            set
            {
                _listMedicaments = value;
            }
        }

        private static List<Traite> _listTraitement;
        public static List<Traite> ListTraitement
        {
            get
            {
                return _listTraitement;
            }
            set
            {
                _listTraitement = value;
            }
        }

    }
}
