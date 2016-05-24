using HCIZadatak.Entiteti;
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

namespace HCIZadatak
{
    /// <summary>
    /// Interaction logic for NoviTipControl.xaml
    /// </summary>
    public partial class NoviTipControl : UserControl
    {
        private Lokal lokal;
        private Tip noviTip = new Tip();

        public NoviTipControl(Lokal lokal)
        {
            InitializeComponent();
            DataContext = noviTip;
            this.lokal = lokal;
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

        private void Zavrsi_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (System.Windows.Controls.Validation.GetHasError(oznakaTxt))
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
            e.Handled = true;
        }

        private void Zavrsi_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            ((App)App.Current).Tipovi.Add(noviTip);
            lokal.Tip = noviTip;
            NavigationService.GetNavigationService(this).Navigate(new TipoviDataGrid(lokal));
            e.Handled = true;
        }

        private void odustaniBtn_Click(object sender, RoutedEventArgs e)
        {
            lokal.Tip = null;
            NavigationService.GetNavigationService(this).Navigate(new TipoviDataGrid(lokal));
        }
    }
}
