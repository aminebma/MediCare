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
    public partial class AgendaMenu : UserControl
    {
        public AgendaMenu()
        {
            InitializeComponent();
            TextAJTRDVPRO.Text = "Ajouter un rendez \n vous professionel";
            TextAJTRDVPERD.Text = "Ajouter un rendez \n vous personnel";
            TextMODSUPRDV.Text = "Modifier/annuler \n un rendez vous";
            TextCSLTAGD.Text = "Consulter mon Agenda";
        }

        public UserControl SetGrid2
        {
            set { Grid2.Children.Add(value); }
            
        }
     
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "AjtRDVpro":
                    this.Grid2.Children.Clear();
                    this.Grid2.Children.Add(new AjoutRdvPro());
                    break;
                case "AjtRDVpers":
                    this.Grid2.Children.Clear();
                    this.Grid2.Children.Add((new AjoutRdvPerso()));
                    break;
                case "ModSupRDV":
                    this.Grid2.Children.Clear();
                    this.Grid2.Children.Add(new ModSuppRdv());
                    break;
                case "CsltAgd":
                    this.Grid2.Children.Clear();
                    this.Grid2.Children.Add(new AgendaMedecin());
                    break;
                default:
                    break;
            }
        }
    }
}
