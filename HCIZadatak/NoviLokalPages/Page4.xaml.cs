using HCIZadatak.Entiteti;
using HCIZadatak.Validation;
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
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        private NoviLokal noviLokal;

        public Page4()
        {
            InitializeComponent();
        }

        public Page4(NoviLokal noviLokal)
        {
            this.noviLokal = noviLokal;
            this.DataContext = noviLokal.Novi;

            InitializeComponent();

            odabraneEtiketeCB.ItemsSource = noviLokal.Novi.Etikete;
            tipFrame.NavigationService.Navigate(new TipoviDataGrid(noviLokal.Novi));
            etiketeFrame.NavigationService.Navigate(new EtiketeCheckList(noviLokal.Novi));
        }

        private void nazadbtn_Click(object sender, RoutedEventArgs e)
        {
            ((NoviLokal)App.Current.MainWindow.Content).NavigateToPage(3);
        }

        //private bool TipFilter(object item)
        //{
        //    Tip tip = item as Tip;
        //    if (String.IsNullOrEmpty(txtFilter.Text))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        bool oznakaOk = (tip.Oznaka.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //        bool imeOk = false;
        //        if (tip.Ime != null)
        //        {
        //            imeOk = (tip.Ime.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //        }
        //        return oznakaOk || imeOk;
        //    }

        //}

        //private void txtFlter_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    CollectionViewSource.GetDefaultView(tipovilist.ItemsSource).Refresh();
        //}

        //private void zavrsibtn_Click(object sender, RoutedEventArgs e)
        //{
        //    ((App)App.Current).Lokali.Add(noviLokal.Novi);
        //    ((MainWindow)App.Current.MainWindow).Navigate(new Mapa());
        //}

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            noviLokal.Novi.Etikete.Remove(e.Parameter as Etiketa);

            e.Handled = true;
        }
    }
}
