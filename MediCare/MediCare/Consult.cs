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
        
        public void AddConsult(string nomPatient, string prenomPatient, string nomMedecin, string prenomMedecin, string diagnostic, string description, string cheminCertificat,string cheminLettreOrientation, string cheminScanner, string cheminBilan, List<string> cheminRadio, List<Traite> traitement)
        {
            string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
            MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            Consultation table = new Consultation
            {
                date=DateTime.Today,
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
            foreach (var chemin in cheminRadio)
            {
                tableRadio.chemin = chemin;
                tableRadio.IdConsultation = table.Id;
                dataClass.Radio.InsertOnSubmit(tableRadio);
                dataClass.SubmitChanges();
            };

            Traitement tableTraitement = new Traitement();

            foreach (var trait in traitement)
            {
                tableTraitement.Dose = trait.Dose;
                tableTraitement.Indication = trait.Indication;
                tableTraitement.IdConsultation = table.Id;
                dataClass.Traitement.InsertOnSubmit(tableTraitement);
                dataClass.SubmitChanges();

                Medicaments medTraitement = (from medicament in dataClass.Medicaments
                                             where trait.NomMed == medicament.nom
                                             select medicament).First<Medicaments>();

                MedicamenTraitement tableMT = new MedicamenTraitement
                {
                    IdTraitement = tableTraitement.Id,
                    IdMedicaments = medTraitement.Id, 
                };

                dataClass.MedicamenTraitement.InsertOnSubmit(tableMT);
                dataClass.SubmitChanges();
            };
           
            Patient patientConsultation = (from personne in dataClass.Personne
                                   where nomPatient == personne.nom && prenomPatient == personne.prenom
                                   join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
                                   select patient).First<Patient>();
            Medecin medecinConsultation = (from personne in dataClass.Personne
                                   where nomMedecin == personne.nom && prenomMedecin == personne.prenom
                                   join medecin in dataClass.Medecin on personne.Id equals medecin.IdPersonne
                                   select medecin).First<Medecin>();

            MPConsultation tableMPC = new MPConsultation
            {
                IdConsultation = table.Id,
                IdPatient = patientConsultation.Id,
                IdMedecin = medecinConsultation.Id,
            };

            dataClass.MPConsultation.InsertOnSubmit(tableMPC);
            dataClass.SubmitChanges();

        }
		public void SuppConsultation(string nomPatient, string prenomPatient, DateTime date)
		{
			string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
			MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
			Personne Pers = (from personne in dataClass.Personne
							 where nomPatient == personne.nom && prenomPatient == personne.prenom
							 select personne).First<Personne>();

			Patient Pat = (from patient in dataClass.Patient
						   where patient.IdPersonne == Pers.Id
						   select patient).First<Patient>();

			MPConsultation mpcnsltToDelete = (from cns in dataClass.Consultation
										  where cns.date == date
										  join mpc in dataClass.MPConsultation on cns.Id equals mpc.IdConsultation
									   	  where mpc.IdPatient == Pat.Id
										  select mpc).First<MPConsultation>();

			Consultation cnsltToDelete = (from cns in dataClass.Consultation
										  where cns.Id == mpcnsltToDelete.IdConsultation
										  select cns).First<Consultation>();


			
			dataClass.MPConsultation.DeleteOnSubmit(mpcnsltToDelete);
			dataClass.SubmitChanges();
			dataClass.Consultation.DeleteOnSubmit(cnsltToDelete);
			dataClass.SubmitChanges();

		}
		/* public void SuppRdv(string nomPatient, string prenomPatient, DateTime date)
		 {
			 string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
			 MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
			 RendezVous rdvToDelete = (from personne in dataClass.Personne
									   where nomPatient == personne.nom && prenomPatient == personne.prenom
									   join patient in dataClass.Patient on personne.Id equals patient.IdPersonne
									   join cslt in dataClass.Consultation on personne.Id equals consultation.Id
									   orderby rdv.Id descending
									   select rdv).First();
			 dataClass.RendezVous.DeleteOnSubmit(rdvToDelete);
			 dataClass.SubmitChanges();

		 }

		 public void SuppRdv(DateTime date)
		 {
			 string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
			 MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
			 RendezVous rdvToDelete = (from rdv in dataClass.RendezVous
									   where date == rdv.Date
									   select rdv).First();
			 dataClass.RendezVous.DeleteOnSubmit(rdvToDelete);
			 dataClass.SubmitChanges();
		 }*/
	}
}

