using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OutlookCalendar.Model;

namespace OutlookCalendar
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    { 

        private MediCare.Agenda rendezVous = new MediCare.Agenda(MediCare.Globals.IdMedecin);
        private Appointments appointments;

        public Window1()
        {
            InitializeComponent();
            appointments = new Appointments(rendezVous.listRdv);
            DataContext = appointments;
        }

        private void Calendar_AddAppointment(object sender, RoutedEventArgs e)
        {
            //Appointment appointment = new Appointment();
            //appointment.Subject = "Subject?";
            //appointment.StartTime = new DateTime(2008, 10, 22, 16, 00, 00);
            //appointment.EndTime = new DateTime(2008, 10, 22, 17, 00, 00);

            //AddAppointmentWindow aaw = new AddAppointmentWindow();
            //aaw.DataContext = appointment;
            //aaw.ShowDialog();

            //appointments.Add(appointment);
            //MediCare.AjoutRdvPro rdvPro = new MediCare.AjoutRdvPro();
            //rdvPro.Show();
        }
    }
}
