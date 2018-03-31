﻿using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace MediCare
{
	/// <summary>
	/// Logique d'interaction pour Menu_Fichier.xaml
	/// </summary>
	public partial class MenuFichier : Window
	{
		public MenuFichier()
		{
			InitializeComponent();
		}

        private void AjouterRadio_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                MessageBox.Show(ofd.FileName);

            }
        }

        private void AjouterScanner_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                MessageBox.Show(ofd.FileName);

            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)


        //	.

        //}
    }
}
