﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{
	class ConsultLabel
	{
		private DateTime date;
		private string label;
		private int id;
		private string nom;
		private string prenom;
        private string nomed;
        private string prenomed;

		public ConsultLabel(DateTime date, string label, int id, string nom, string prenom)
		{
			this.date = date;
			this.label = label;
			this.id = id;
			this.nom = nom;
			this.prenom = prenom;
		}

        public ConsultLabel(DateTime date, string label, int id, string nom, string prenom, string nomed, string prenomed)
        {
            this.date = date;
            this.label = label;
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.nomed = nomed;
            this.prenomed = prenomed;
        }

        public ConsultLabel()
        {

        }

		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

		public string Label
		{
			get { return label; }
			set { label = value; }
		}

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Nom
		{
			get { return nom; }
			set { nom = value; }
		}

		public string Prenom
		{
			get { return prenom; }
			set { prenom = value; }
		}

        public string Nomed
        {
            get { return nomed; }
            set { nomed = value; }
        }

        public string Prenomed
        {
            get { return prenomed; }
            set { prenomed = value; }
        }
    }
}