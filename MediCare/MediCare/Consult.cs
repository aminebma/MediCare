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

        public void AddConsult(string nomPatient, string prenomPatient, string nomMedecin, string prenomMedecin, string diagnostic, string description, string cheminCertificat, string cheminLettreOrientation, string cheminScanner, string cheminBilan, string cheminOrdo, List<string> cheminRadio, List<Traite> traitement, string label, int age)
        {
            nomPatient = nomPatient.ToUpper();
            prenomMedecin = prenomMedecin.ToUpper();
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
                age = age,
                label = label
            };
            Globals.DataClass.Consultation.InsertOnSubmit(table);
            Globals.DataClass.SubmitChanges();


            foreach (var chemin in cheminRadio)
            {
                Radio tableRadio = new Radio();
                tableRadio.chemin = chemin;
                tableRadio.IdConsultation = table.Id;
                Globals.DataClass.Radio.InsertOnSubmit(tableRadio);
                Globals.DataClass.SubmitChanges();
            }



            foreach (var trait in traitement)
            {
                Traitement tableTraitement = new Traitement();
                tableTraitement.Dose = trait.Dose;
                tableTraitement.Indication = trait.Indication;
                tableTraitement.IdConsultation = table.Id;


                Medicaments medTraitement = (from medicament in Globals.DataClass.Medicaments
                                             where trait.NomMed == medicament.nom
                                             select medicament).First<Medicaments>();

                tableTraitement.IdMedicament = medTraitement.Id;

                Globals.DataClass.Traitement.InsertOnSubmit(tableTraitement);
                Globals.DataClass.SubmitChanges();

            }

            Patient patientConsultation = (from personne in Globals.DataClass.Personne
                                           where nomPatient == personne.nom && prenomPatient == personne.prenom
                                           join patient in Globals.DataClass.Patient on personne.Id equals patient.IdPersonne
                                           select patient).First<Patient>();
            Medecin medecinConsultation = (from personne in Globals.DataClass.Personne
                                           where nomMedecin == personne.nom && prenomMedecin == personne.prenom
                                           join medecin in Globals.DataClass.Medecin on personne.Id equals medecin.IdPersonne
                                           select medecin).First<Medecin>();

            MPConsultation tableMPC = new MPConsultation
            {
                IdConsultation = table.Id,
                IdPatient = patientConsultation.Id,
                IdMedecin = medecinConsultation.Id
            };

            Globals.DataClass.MPConsultation.InsertOnSubmit(tableMPC);
            Globals.DataClass.SubmitChanges();
        }





        public void SuppConsultation(string nomPatient, string prenomPatient, DateTime date)
        {
            nomPatient = nomPatient.ToUpper();
            prenomPatient = prenomPatient.ToUpper();

            Personne Pers = (from personne in Globals.DataClass.Personne
                             where nomPatient == personne.nom && prenomPatient == personne.prenom
                             select personne).First<Personne>();

            Patient Pat = (from patient in Globals.DataClass.Patient
                           where patient.IdPersonne == Pers.Id
                           select patient).First<Patient>();

            MPConsultation mpcnsltToDelete = (from cns in Globals.DataClass.Consultation
                                              where cns.date == date
                                              join mpc in Globals.DataClass.MPConsultation on cns.Id equals mpc.IdConsultation
                                              where mpc.IdPatient == Pat.Id
                                              select mpc).First<MPConsultation>();

            Consultation cnsltToDelete = (from cns in Globals.DataClass.Consultation
                                          where cns.Id == mpcnsltToDelete.IdConsultation
                                          select cns).First<Consultation>();
            IQueryable<Radio> rdi = (from radio in Globals.DataClass.Radio
                                     where radio.IdConsultation == cnsltToDelete.Id
                                     select radio);
            foreach (Radio p in rdi)
            {
                Globals.DataClass.Radio.DeleteOnSubmit(p);
                Globals.DataClass.SubmitChanges();
            }

            IQueryable<Traitement> trait = (from traitement in Globals.DataClass.Traitement
                                            where traitement.IdConsultation == cnsltToDelete.Id

                                            select traitement);

            foreach (Traitement p in trait)
            {
                Globals.DataClass.Traitement.DeleteOnSubmit(p);
                Globals.DataClass.SubmitChanges();
            }



            Globals.DataClass.MPConsultation.DeleteOnSubmit(mpcnsltToDelete);
            Globals.DataClass.SubmitChanges();
            Globals.DataClass.Consultation.DeleteOnSubmit(cnsltToDelete);
            Globals.DataClass.SubmitChanges();
        }


        public List<ConsultLabel> RechercheConsultationDate(DateTime date)
		{
			List<ConsultLabel> list = new List<ConsultLabel>();


			IQueryable<Consultation> cnslt = (from cns in Globals.DataClass.Consultation
											  where cns.date == date
											  select cns);


			foreach (Consultation p in cnslt)
			{
				MPConsultation mpcnslt = (from mpc in Globals.DataClass.MPConsultation
										  where mpc.IdConsultation == p.Id
										  select mpc).First();
				Patient pat = (from patient in Globals.DataClass.Patient
							   where patient.Id == mpcnslt.IdPatient
							   select patient).First();
				Personne pers = (from personne in Globals.DataClass.Personne
								 where personne.Id == pat.IdPersonne
								 select personne).First();

					                  
				ConsultLabel q = new ConsultLabel((DateTime)p.date, p.label , p.Id, pers.nom, pers.prenom);
				list.Add(q);
			}
            return list;
		}

		public List<ConsultLabel> Suivie(string nomPatient, string prenomPatient)
		{
            nomPatient = nomPatient.ToUpper();
            prenomPatient = prenomPatient.ToUpper();
            List <ConsultLabel> list = new List<ConsultLabel>();

			Personne Pers = (from personne in Globals.DataClass.Personne
							 where nomPatient == personne.nom && prenomPatient == personne.prenom
							 select personne).First<Personne>();

            Patient Pat = (from patient in Globals.DataClass.Patient
                           where patient.IdPersonne == Pers.Id
                           select patient).First<Patient>();
			IQueryable<Consultation> cnslt = (from cns in Globals.DataClass.MPConsultation
											  where cns.IdPatient == Pat.Id
											  join mpc in Globals.DataClass.Consultation on cns.IdConsultation equals mpc.Id
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
			List<string> radio = new List<string>();
			List<Traite> traitement = new List<Traite>();

			Consultation cnslt = (from cns in Globals.DataClass.Consultation
								  where cns.Id == Id
								  select cns).First<Consultation>();
			IQueryable<Radio> rdi = (from radi in Globals.DataClass.Radio
									 where radi.IdConsultation == cnslt.Id
									 select radi);
			foreach (Radio p in rdi)
			{
				radio.Add(p.chemin);
			}
			IQueryable<Traitement> trait = (from traitemen in Globals.DataClass.Traitement
											where traitemen.IdConsultation == cnslt.Id
                                            select traitemen);

			foreach (Traitement p in trait)

			{
				Medicaments Med = (from med in Globals.DataClass.Medicaments
								   where med.Id == p.IdMedicament
								   select med).First<Medicaments>();
				Traite q = new Traite(p.Dose, p.Indication, Med.nom);
                traitement.Add(q);
			}

			Consulta consultation = new Consulta((DateTime)cnslt.date, cnslt.diagnostic, cnslt.description, cnslt.cheminCertificat, cnslt.cheminLettreOrientation, cnslt.cheminScanner, cnslt.cheminBilan, cnslt.cheminOrdo, traitement, radio);
			return consultation;

		}

		public List<ConsultLabel> Historique()
		{
			List<ConsultLabel> list = new List<ConsultLabel>();
			IQueryable<Consultation> cnslt = (from cns in Globals.DataClass.Consultation
											  select cns);

			foreach (Consultation p in cnslt)
			{
                MPConsultation mpcnslt = (from mpc in Globals.DataClass.MPConsultation
                                          where mpc.IdConsultation == p.Id
                                          select mpc).First<MPConsultation>();
                Patient pat = (from patient in Globals.DataClass.Patient
							   where patient.Id == mpcnslt.IdPatient
							   select patient).First<Patient>();
				Personne pers = (from personne in Globals.DataClass.Personne
								 where personne.Id == pat.IdPersonne
								 select personne).First<Personne>();
                Medecin patmed = (from medecin in Globals.DataClass.Medecin
                               where medecin.Id == mpcnslt.IdMedecin
                               select medecin).First<Medecin>();
                Personne persmed = (from personne in Globals.DataClass.Personne
                                 where personne.Id == patmed.IdPersonne
                                 select personne).First<Personne>();

                ConsultLabel q = new ConsultLabel((DateTime)p.date, p.label, p.Id,  pers.nom, pers.prenom,persmed.nom,persmed.prenom);
				list.Add(q);

			}
			return list;

		}


		 
		public List<ConsultLabel> HistoriqueMedecin(string nomMedecin, string prenomMedecin)
		{
			List<ConsultLabel> list = new List<ConsultLabel>();

			Personne perso = (from personne in Globals.DataClass.Personne
							  where nomMedecin == personne.nom && prenomMedecin == personne.prenom
							  select personne).First<Personne>();
			Medecin med = (from medecin in Globals.DataClass.Medecin
						   where perso.Id == medecin.IdPersonne
						   select medecin).First<Medecin>();

			IQueryable<Consultation> cnslt = (from mpc in Globals.DataClass.MPConsultation
											  where mpc.IdMedecin == med.Id
											  join cns in Globals.DataClass.Consultation on mpc.IdConsultation equals cns.Id
											  select cns);

			foreach (Consultation p in cnslt)
			{
				MPConsultation mpcnslt = (from mpc in Globals.DataClass.MPConsultation
										  where mpc.IdConsultation == p.Id
										  select mpc).First<MPConsultation>();
				Patient pat = (from patient in Globals.DataClass.Patient
							   where patient.Id == mpcnslt.IdPatient
							   select patient).First<Patient>();
				Personne pers = (from personne in Globals.DataClass.Personne
								 where personne.Id == pat.IdPersonne
								 select personne).First<Personne>();

				ConsultLabel q = new ConsultLabel((DateTime)p.date, p.label, p.Id, pers.nom, pers.prenom);
				list.Add(q);

			}
			return list;
		}

	}
}

