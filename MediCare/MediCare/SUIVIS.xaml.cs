﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediCare
{
    public partial class SUIVIS : UserControl
    {
        Agenda pat = new Agenda();
        List<ConsultLabel> list;
        Consult consultation = new Consult();
        Consulta consulta = new Consulta();

        public SUIVIS()
        {
            InitializeComponent();
            list = consultation.Suivie(Globals.NomPatient, Globals.PrenomPatient);
            TextBlock expnom = new TextBlock();
            expnom.Text = "  Nom : " + Globals.NomPatient + "  Prénom : " + Globals.PrenomPatient;
            StackSuivi.Children.Add(expnom);

            if (list.Count() != 0)
            {
                foreach (ConsultLabel p in list)
                {
                    
                    Expander expSuivi = new Expander();
                    expSuivi.Header = "  Titre de Consultation :" + p.Label + "\n Date de la consultation :" + p.Date.Day + "/" + p.Date.Month + "/" + p.Date.Year;
                    Stack.Children.Add(expSuivi);
                    consulta = consultation.AcceeConsultationId(p.Id);
                    expSuivi.Content = " Diagnostic : " + consulta.Diagnostic + "\n Description :" + consulta.Description;
                    if (consulta.traitement.Count() != 0)
                    {
                        expSuivi.Content = expSuivi.Content + "\n Traitement :";
                        foreach (Traite d in consulta.traitement)
                        {
                            expSuivi.Content = expSuivi.Content + "\n Nom du médicament : " + d.NomMed + " ,Dose : " + d.Dose + " ,Indication : " + d.Indication;

                        }
                    }


                }
            }
        }
        

        
    }
}
 
               