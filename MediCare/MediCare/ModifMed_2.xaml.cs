﻿using System;
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
using System.Windows.Shapes;

namespace MediCare
{
    /// <summary>
    /// Logique d'interaction pour ModifMed_2.xaml
    /// </summary>
    public partial class ModifMed_2 : Window
    {
       

        Medecin2 medd = new Medecin2();
         
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            medd.ModifMed(user.Text, npass.Text, pass.Text);
             MessageBox.Show("Tout  a été modifié ! ");
            
        }
    }
}
