using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace OutlookCalendar.Model
{
    public class Appointments : ObservableCollection<Appointment>
    {
        public Appointments(List<MediCare.RendezVous> rendezVous)
        {
            foreach(MediCare.RendezVous rdv in rendezVous)
            {
                Add(new Appointment() { Subject = rdv.Note, StartTime = (DateTime)rdv.Date, EndTime = (DateTime)rdv.Date });
            }
        }
    }
}
