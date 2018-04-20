using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour DossierMedical.xaml
    /// </summary>
    public partial class DossierMedical : UserControl
    {

        PersonneClasse personne = new PersonneClasse();
        Medecin2 patients = new Medecin2();
        List<Patients> list;
        Agenda pat = new Agenda();
        List<ConsultLabel> l;
        Consult consultation = new Consult();
        Consulta consulta = new Consulta();

        public DossierMedical()
        {
            InitializeComponent();
            list = patients.DossiersMedical();
            if (list.Count() != 0)
            {
                foreach (Patients p in list)
                {
                    if (Globals.NomPatient == p.Nom && Globals.PrenomPatient == p.Prenom)
                    {
                        TextBlock expSuivi = new TextBlock();
                        expSuivi.Text = "Nom : " + p.Nom + "    Prénom : " + p.Prenom + "\n    Date de naissance : " + p.Datenaiss + "\n    Taille : " + p.Taille +" cm "+ "\n    Poids : " + p.Poids +" Kg "+ "\n    Sexe : " + p.Sexe + "\n     Numéro de téléphone : 0" + p.Numtel + "\n     Maladie : " + p.Maladie + "\n    Groupage sanguin : " + p.Groupage + "\n    Etat de santé actuel : " + p.EtatSante + "\n    Adresse : " + p.Adresse;
                        StackSuivi.Children.Add(expSuivi);
                    }
                }

            }
            l = consultation.Suivie(Globals.NomPatient, Globals.PrenomPatient);
            if (l.Count() != 0)
            {
                foreach (ConsultLabel p in l)
                {
                    Expander expSuivi = new Expander();
                    expSuivi.Header = "Titre de Consultation :" + p.Label + "\n  Date de la consultation :" + p.Date.Day + "/" + p.Date.Month + "/" + p.Date.Year;
                    Stack.Children.Add(expSuivi);
                    consulta = consultation.AcceeConsultationId(p.Id);
                    expSuivi.Content = " Diagnostic : " + consulta.Diagnostic + "\n Description :" + consulta.Description;
                    if (consulta.traitement.Count() != 0 )
                    {
                        expSuivi.Content = expSuivi.Content + "\n Traitement :";
                        foreach (Traite d in consulta.traitement)
                        {
                            expSuivi.Content = expSuivi.Content + "\n Nom du médicament : " + d.NomMed + " ,Dose préscrite : " + d.Dose + " ,Indication : " + d.Indication;
                        }
                    }


                }
            }
        }
    }
}
