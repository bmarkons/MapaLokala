using HCIZadatak.Validation;
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

namespace HCIZadatak.Validation
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        private NoviLokal noviLokal;

        public Page3()
        {
            InitializeComponent();
        }

        public Page3(NoviLokal noviLokal)
        {
            InitializeComponent();
            this.DataContext = noviLokal.Novi;
            this.noviLokal = noviLokal;
        }

        private void ucitajbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ikona.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        //private void daljebtn_Click(object sender, RoutedEventArgs e)
        //{
        //    noviLokal.NavigateToPage(4);
        //}

        //private void nazadbtn_Click(object sender, RoutedEventArgs e)
        //{
        //    noviLokal.NavigateToPage(2);
        //}

        //private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}

        //private void Nazad_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    noviLokal.NavigateToPage(2);
        //    e.Handled = true;
        //}

        //private void Dalje_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    noviLokal.NavigateToPage(4);
        //    e.Handled = true;

        //}
    }
}
