using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MediCare
{
    class Consult
    {
        
        public void AddConsult(string nomPatient, string prenomPatient, string nomMedecin, string prenomMedecin,DateTime date, string diagnostic, string description, string cheminCertificat,string cheminLettreOrientation, string cheminScanner, string cheminBilan, List<string> cheminRadio, List<Traite> traitement)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            Consultation table = new Consultation
            {
                date=date,
                diagnostic=diagnostic,
                description=description,
                cheminCertificat=cheminCertificat,
                cheminLettreOrientation=cheminLettreOrientation,
                cheminScanner=cheminScanner,
                cheminBilan=cheminBilan
            };
            dataClass.Consultation.InsertOnSubmit(table);
            dataClass.SubmitChanges();
            Radio tableRadio = new Radio();
            Consultation actualConsultation = (from consult in dataClass.Consultation
                                               orderby consult.Id descending
                                               select consult).First();
            foreach (var chemin in cheminRadio)
            {
                tableRadio.chemin = chemin;
                tableRadio.IdConsultation = actualConsultation.Id;
                dataClass.Radio.InsertOnSubmit(tableRadio);
                dataClass.SubmitChanges();
            };
            Traitement tableTraitement = new Traitement();
            foreach (var trait in traitement)
            {
                tableTraitement.Dose = trait.Dose;
                tableTraitement.Indication = trait.Indication;
                tableTraitement.IdConsultation = actualConsultation.Id;
                dataClass.Traitement.InsertOnSubmit(tableTraitement);
                dataClass.SubmitChanges();


                Traitement actualTraitement = (from trt in dataClass.Traitement
                                               orderby trt.Id descending
                                               select trt).First();

                Medicaments medTraitement = (from medicament in dataClass.Medicaments
                                             where trait.NomMed == medicament.nom
                                             select medicament).First();
                MedicamenTraitement tableMT = new MedicamenTraitement();
                {
                    tableMT.IdTraitement = actualTraitement.Id;
                    tableMT.IdMedicaments = medTraitement.Id;
                    dataClass.MedicamenTraitement.InsertOnSubmit(tableMT);
                    dataClass.SubmitChanges();
                };
            };
           
            Personne patientConsultation = (from personne in dataClass.Personne
                                   where nomPatient == personne.nom && prenomPatient == personne.prenom
                                   join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                   select personne).First();
            Personne medecinConsultation = (from personne in dataClass.Personne
                                   where nomMedecin == personne.nom && prenomMedecin == personne.prenom
                                   join medecin in dataClass.Medecin on personne.Id equals medecin.IdPersonne
                                   select personne).First();
            MPConsultation tableMPC = new MPConsultation();
            {
                tableMPC.IdConsultation = actualConsultation.Id;
                tableMPC.IdPatient = patientConsultation.Id;
                tableMPC.IdMedecin = medecinConsultation.Id;
                dataClass.MPConsultation.InsertOnSubmit(tableMPC);
                dataClass.SubmitChanges();
            };

            
        }
    }
}

