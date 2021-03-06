﻿using HCIZadatak.Validation;
using Microsoft.Win32;
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

namespace HCIZadatak
{
    /// <summary>
    /// Interaction logic for LokalPodaci.xaml
    /// </summary>
    public partial class LokalPodaci : UserControl
    {
        private Lokal lokal;

        public LokalPodaci(Lokal lokal)
        {
            InitializeComponent();
            DataContext = lokal;
            this.lokal = lokal;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parent = (StackPanel)Parent;
            parent.Children.Remove(this);
            parent.UnregisterName("Details");

            //deselektuj stavku u listi
            Mapa.Current.lokaliDG.SelectedItem = null;
        }

        //private void ucitajBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog op = new OpenFileDialog();
        //    op.Title = "Select a picture";
        //    op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
        //      "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
        //      "Portable Network Graphic (*.png)|*.png";
        //    if (op.ShowDialog() == true)
        //    {
        //        ikonaImg.Source = new BitmapImage(new Uri(op.FileName));
        //    }
        //}

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).Navigate(new NoviLokal(lokal));
        }
    }
}
