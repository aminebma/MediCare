using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare
{

	class Mois
	{
		public static string ConvertMois(DateTime date)
		{
			string tmp = date.Month.ToString();
			switch (tmp)
			{
				case "1":
					return "Janvier";

				case "2":
					return "Février";

				case "3":
					return "Mars";
				case "4":
					return "Avril";
				case "5":
					return "Mai";
				case "6":
					return "Juin";
				case "7":
					return "Juillet";
				case "8":
					return "Août";
				case "9":
					return "Septembre";
				case "10":
					return "Octobre";
				case "11":
					return "Novembre";
				case "12":
					return "Décembre";
			}
			return "";
		}
		public static string ConvertDay(DateTime date)
		{
			string tmp = date.DayOfWeek.ToString();
			switch (tmp)
			{
				case "Sunday":
					return "Dimanche";

				case "Monday":
					return "Lundi";
                case "Thursday":
					return "Mardi";
				case "Wednesday":
					return "Mercredi";
				case "Tuesday":
					return "Jeudi";
				case "Friday":
					return "Vendredi";
				case "Saturday":
					return "Samedi";
				
			}
			return "";
		}
	}
}