using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MediCare
{
    class PersonneClasse
    {
       public void AddPatientPersonne(string nom, string prenom, string date, string adresse, string num_tel, string sexe, string taille, string poids, string groupage, string maladie, string etat_sante)
        {
            nom = nom.ToUpper();
            prenom = prenom.ToUpper();
            Personne tabpersonne = new Personne
            {
                nom = nom,
                prenom = prenom,
                dateNaissance = DateTime.Parse(date),
                adresse = adresse,
                telephone = int.Parse(num_tel),
                sexe = sexe,
            };

            Globals.DataClass.Personne.InsertOnSubmit(tabpersonne);
            Globals.DataClass.SubmitChanges();

            Patient tabpatient = new Patient
            {
                taille = double.Parse(taille),
                poids = double.Parse(poids),
                groupage = groupage,
                maladie = maladie,
                etatSante = etat_sante,
                IdPersonne = tabpersonne.Id
            };
             
            Globals.DataClass.Patient.InsertOnSubmit(tabpatient);
            Globals.DataClass.SubmitChanges();
            //System.IO.File.Copy($@"{Globals.CurrentDirectoryPath}\\MCDatabase.mdf", $@"{Globals.CurrentDirectoryPath}\\restauration\\MCDatabase.mdf", true);
        }
    } 

}
