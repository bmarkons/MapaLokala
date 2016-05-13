using HCIZadatak.Entiteti;
using HCIZadatak.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Mapa.xaml
    /// </summary>
    public partial class Mapa : UserControl
    {
        private static Mapa current;
        public static Mapa Current
        {
            get
            {
                if (current == null)
                {
                    current = new Mapa();
                }
                return current;
            }
        }

        private Mapa()
        {
            InitializeComponent();

            //DataGrids
            lokaliDG.ItemsSource = ((App)App.Current).Lokali;
            tipoviDG.ItemsSource = ((App)App.Current).Tipovi;
            etiketeDG.ItemsSource = ((App)App.Current).Etikete;
        }

        private void novilokalbtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).Navigate(new NoviLokal());
        }

        private void novitipbtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).Navigate(new NoviTip());
        }

        private void novaetiketabtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).Navigate(new NovaEtiketa());
        }

        private void ObrisiLokal_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void ObrisiLokal_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ObservableCollection<Lokal> lokali = ((App)App.Current).Lokali;
            string oznaka = e.Parameter as string;
            foreach (Lokal lokal in ((App)App.Current).Lokali)
            {
                if (lokal.OznakaTxt.Equals(oznaka))
                {
                    lokali.Remove(lokal);
                    break;
                }
            }
            e.Handled = true;
        }

        private void ObrisiTip_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void ObrisiTip_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ObservableCollection<Tip> tipovi = ((App)App.Current).Tipovi;
            string oznaka = e.Parameter as string;
            foreach (Tip tip in tipovi)
            {
                if (tip.Oznaka.Equals(oznaka))
                {
                    tipovi.Remove(tip);
                    break;
                }
            }
            e.Handled = true;
        }

        private void ObrisiEtiketu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void ObrisiEtiketu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ObservableCollection<Etiketa> etikete = ((App)App.Current).Etikete;
            string oznaka = e.Parameter as string;
            foreach (Etiketa etiketa in ((App)App.Current).Etikete)
            {
                if (etiketa.Oznaka.Equals(oznaka))
                {
                    etikete.Remove(etiketa);
                    break;
                }
            }
            e.Handled = true;
        }

        private void lokaliDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid lokaliDG = sender as DataGrid;
            Lokal selected = (Lokal)lokaliDG.SelectedItem;
            if(selected != null)
            {
                ExpanderPanel.Children.Add(new LokalPodaci());
            }
        }
    }
}
