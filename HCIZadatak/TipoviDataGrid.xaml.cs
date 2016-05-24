using HCIZadatak.Entiteti;
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
    /// Interaction logic for TipoviDataGrid.xaml
    /// </summary>
    public partial class TipoviDataGrid : UserControl
    {
        private Lokal lokal;

        public TipoviDataGrid(Lokal lokal)
        {
            InitializeComponent();
            DataContext = lokal;
            this.lokal = lokal;

            tipovilist.ItemsSource = ((App)App.Current).Tipovi;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(tipovilist.ItemsSource);
            view.Filter = TipFilter;
        }

        private bool TipFilter(object item)
        {
            Tip tip = item as Tip;
            if (String.IsNullOrEmpty(txtFilter.Text))
            {
                return true;
            }
            else
            {
                bool oznakaOk = (tip.Oznaka.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                bool imeOk = false;
                if (tip.Ime != null)
                {
                    imeOk = (tip.Ime.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                return oznakaOk || imeOk;
            }

        }

        private void txtFlter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(tipovilist.ItemsSource).Refresh();
        }

        private void noviTipBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new NoviTipControl(lokal));
        }
    }
}
