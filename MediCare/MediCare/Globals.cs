﻿using System;
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
        private static string _medicament;
        public static string Medicament
        {
            get
            {
                return _medicament;
            }
            set
            {
                _medicament = value;
            }
        }
       
        private static string _adressePatient;
        public static string AdressePatient
        {
            get
            {
                return _adressePatient;
            }
            set
            {
                _adressePatient = value;
            }
        }
        private static string _specialite;
        public static string specialite
        {
            get
            {
                return _specialite;
            }
            set
            {
                _specialite = value;
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

        private static string _adresseMedecin;
        public static string AdresseMedecin
        {
            get
            {
                return _adresseMedecin;
            }
            set
            {
                _adresseMedecin = value;
            }
        }

        private static string _mailMedecin;
        public static string MailMedecin
        {
            get
            {
                return _mailMedecin;
            }
            set
            {
                _mailMedecin = value;
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

        private static int _num;
        public static int num
        {
            get
            {
                return _num;
            }
            set
            {
                _num = value;
            }
        }

        private static int _numMobile;
        public static int numMobile
        {
            get
            {
                return _numMobile;
            }
            set
            {
                _numMobile = value;
            }
        }

        private static int _fax;
        public static int fax
        {
            get
            {
                return _fax;
            }
            set
            {
                _fax = value;
            }
        }
        private static string _codeMedecin;
        public static string codeMedecin
        {
            get
            {
                return _codeMedecin;
            }
            set
            {
                _codeMedecin = value;
            }
        }

        private static int _idConsult;
        public static int IdConsult
        {
            get
            {
                return _idConsult;
            }
            set
            {
                _idConsult = value;
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
