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
    /// Interaction logic for NovaEtiketa.xaml
    /// </summary>
    public partial class NovaEtiketa : UserControl
    {
        private Etiketa novi = new Etiketa();

        public NovaEtiketa()
        {
            InitializeComponent();
            DataContext = novi;
        }

        private void odustanibtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).Navigate(Mapa.Current);
        }

        //private void zavrsibtn_Click(object sender, RoutedEventArgs e)
        //{
        //    ((App)App.Current).Etikete.Add(novi);
        //    ((MainWindow)App.Current.MainWindow).Navigate(Mapa.Current);
        //}

        private void Zavrsi_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (System.Windows.Controls.Validation.GetHasError(oznakatext))
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
            ((App)App.Current).Etikete.Add(novi);
            ((MainWindow)App.Current.MainWindow).Navigate(Mapa.Current);
            e.Handled = true;
        }
    }
}
