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
        private bool edit = false;
        private int index;
        private Etiketa stari;

        private Etiketa novi = new Etiketa();

        public NovaEtiketa()
        {
            InitializeComponent();
            DataContext = novi;
        }

        public NovaEtiketa(Etiketa etiketa)
        {
            index = ((App)App.Current).Etikete.IndexOf(etiketa);
            ((App)App.Current).Etikete.Remove(etiketa);
            stari = etiketa;
            edit = true;
            novi = stari.Clone();
            ((App)App.Current).EditingEtiketa = new Tuple<Etiketa, int>(stari, index);

            InitializeComponent();
            DataContext = novi;

            title.Text = "Izmena podataka etikete";
        }

        private void odustanibtn_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                ((App)App.Current).Etikete.Insert(index, stari);
                Mapa.Current.etiketeDG.SelectedItem = stari;
                ((App)App.Current).EditingEtiketa = null;
            }
            ((MainWindow)App.Current.MainWindow).Navigate(Mapa.Current);
        }

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
            if (edit)
            {
                ((App)App.Current).Etikete.Insert(index, novi);
                ((App)App.Current).EditingEtiketa = null;
            }
            else
            {
                ((App)App.Current).Etikete.Add(novi);
            }
            Mapa.Current.etiketeDG.SelectedItem = novi;
            ((MainWindow)App.Current.MainWindow).Navigate(Mapa.Current);
            e.Handled = true;
        }
    }
}
