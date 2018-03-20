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

		public void AddConsult(string nomPatient, string prenomPatient, string nomMedecin, string prenomMedecin, string diagnostic, string description, string cheminCertificat, string cheminLettreOrientation, string cheminScanner, string cheminBilan, string cheminOrdo, List<string> cheminRadio, List<Traite> traitement, string label   )
		{
			string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
			MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
			Consultation table = new Consultation
			{
				date = DateTime.Today,
				diagnostic = diagnostic,
				description = description,
				cheminCertificat = cheminCertificat,
				cheminLettreOrientation = cheminLettreOrientation,
				cheminScanner = cheminScanner,
				cheminBilan = cheminBilan,
				cheminOrdo = cheminOrdo,
                label = label
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
			}

			Traitement tableTraitement = new Traitement();

			foreach (var trait in traitement)
			{
				tableTraitement.Dose = trait.Dose;
				tableTraitement.Indication = trait.Indication;
				tableTraitement.IdConsultation = table.Id;


				Medicaments medTraitement = (from medicament in dataClass.Medicaments
											 where trait.NomMed == medicament.nom
											 select medicament).First<Medicaments>();

				tableTraitement.IdMedicament = medTraitement.Id;

				dataClass.Traitement.InsertOnSubmit(tableTraitement);
				dataClass.SubmitChanges();

			}

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
				IdMedecin = medecinConsultation.Id
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
			IQueryable<Radio> rdi = (from radio in dataClass.Radio
									 where radio.IdConsultation == cnsltToDelete.Id
									 select radio);
			foreach (Radio p in rdi)
			{
				dataClass.Radio.DeleteOnSubmit(p);
				dataClass.SubmitChanges();
			}

			IQueryable<Traitement> trait = (from traitement in dataClass.Traitement
											where traitement.IdConsultation == cnsltToDelete.Id

											select traitement);

			foreach (Traitement p in trait)
			{
				dataClass.Traitement.DeleteOnSubmit(p);
				dataClass.SubmitChanges();
			}



			dataClass.MPConsultation.DeleteOnSubmit(mpcnsltToDelete);
			dataClass.SubmitChanges();
			dataClass.Consultation.DeleteOnSubmit(cnsltToDelete);
			dataClass.SubmitChanges();

		}
	

		public List<ConsultLabel> RechercheConsultationDate(DateTime date)
		{
			string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
			MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
			List<ConsultLabel> list = new List<ConsultLabel>();


			IQueryable<Consultation> cnslt = (from cns in dataClass.Consultation
											  where cns.date == date
											  select cns);


			foreach (Consultation p in cnslt)
			{
				MPConsultation mpcnslt = (from mpc in dataClass.MPConsultation
										  where mpc.IdConsultation == p.Id
										  select mpc).First();
				Patient pat = (from patient in dataClass.Patient
							   where patient.Id == mpcnslt.IdPatient
							   select patient).First();
				Personne pers = (from personne in dataClass.Personne
								 where personne.Id == pat.IdPersonne
								 select personne).First();

					                  
				ConsultLabel q = new ConsultLabel((DateTime)p.date, p.label , p.Id, pers.nom, pers.prenom);
				list.Add(q);
			}
			return list;

		}

		public List<ConsultLabel> Suivie(string nomPatient, string prenomPatient)
		{
			string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
			MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
            List <ConsultLabel> list = new List<ConsultLabel>();

			Personne Pers = (from personne in dataClass.Personne
							 where nomPatient == personne.nom && prenomPatient == personne.prenom
							 select personne).First<Personne>();

            Patient Pat = (from patient in dataClass.Patient
                           where patient.IdPersonne == Pers.Id
                           select patient).First<Patient>();
			IQueryable<Consultation> cnslt = (from cns in dataClass.MPConsultation
											  where cns.IdPatient == Pat.Id
											  join mpc in dataClass.Consultation on cns.IdConsultation equals mpc.Id
											  select mpc);

			foreach (Consultation p in cnslt)
			{

				ConsultLabel q = new ConsultLabel((DateTime)p.date, p.label, p.Id, nomPatient, prenomPatient);
                list.Add(q);

			}
			return list;

		}
		public Consulta AcceeConsultationId(int Id)
		{
			string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
			MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
			List<string> radio = new List<string>();
			List<Traite> traitement = new List<Traite>();

			Consultation cnslt = (from cns in dataClass.Consultation
								  where cns.Id == Id
								  select cns).First<Consultation>();
			IQueryable<Radio> rdi = (from radi in dataClass.Radio
									 where radi.IdConsultation == cnslt.Id
									 select radi);
			foreach (Radio p in rdi)
			{
				radio.Add(p.chemin);
			}
			IQueryable<Traitement> trait = (from traitemen in dataClass.Traitement
											where traitemen.IdConsultation == cnslt.Id
                                            select traitemen);

			foreach (Traitement p in trait)

			{
				Medicaments Med = (from med in dataClass.Medicaments
								   where med.Id == p.IdMedicament
								   select med).First<Medicaments>();
				Traite q = new Traite(p.Dose, p.Indication, Med.nom);
                traitement.Add(q);
			}

			Consulta consultation = new Consulta((DateTime)cnslt.date, cnslt.diagnostic, cnslt.description, cnslt.cheminCertificat, cnslt.cheminLettreOrientation, cnslt.cheminScanner, cnslt.cheminBilan, cnslt.cheminOrdo, traitement, radio);
			return consultation;

		}

		public List<ConsultLabel> Historique ( )
		{
			string con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\MCDatabase.mdf;Integrated Security=True";
			MCDataClassDataContext dataClass = new MCDataClassDataContext(con);
			List<ConsultLabel> list = new List<ConsultLabel>();


			IQueryable<Consultation> cnslt = (from cns in dataClass.Consultation
											  select cns);

			foreach (Consultation p in cnslt)
			{
				MPConsultation mpcnslt = (from mpc in dataClass.MPConsultation
										  where mpc.IdConsultation == p.Id
										  select mpc).First<MPConsultation>();
				Patient pat = (from patient in dataClass.Patient
							   where patient.Id == mpcnslt.IdPatient
							   select patient).First();
				Personne pers = (from personne in dataClass.Personne
								 where personne.Id == pat.IdPersonne
								 select personne).First();

				ConsultLabel q = new ConsultLabel((DateTime)p.date, p.label, p.Id,  pers.nom, pers.prenom);
				list.Add(q);

			}
			return list;

		}

	}
}

