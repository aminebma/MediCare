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
                    // Reads are usually simple
                    return _id;
                }
                set
                {
                    // You can add logic here for race conditions,
                    // or other measurements
                    _id = value;
                }
            }
        private static string _nomPatient;
        public static string NomPatient
        {
            get
            {
                // Reads are usually simple
                return _nomPatient;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _nomPatient = value;
            }
        }
        private static string _prenomPatient;
        public static string PrenomPatient
        {
            get
            {
                // Reads are usually simple
                return _prenomPatient;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _prenomPatient = value;
            }
        }
        // Perhaps extend this to have Read-Modify-Write static methods
        // for data integrity during concurrency? Situational.

    }
}
