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
            alkoholfilter.ItemsSource = Lokal.Sluzenje_alkohola;
            cenefilter.ItemsSource = Lokal.Rang_cena;

            ((CollectionView)CollectionViewSource.GetDefaultView(lokaliDG.ItemsSource)).Filter = Search;
            ((CollectionView)CollectionViewSource.GetDefaultView(tipoviDG.ItemsSource)).Filter = Search;
            ((CollectionView)CollectionViewSource.GetDefaultView(etiketeDG.ItemsSource)).Filter = Search;
        }

        private bool Search(object item)
        {
            if (item is Lokal)
            {
                Lokal lokal = item as Lokal;
                if (String.IsNullOrEmpty(pretragaLokali.Text))
                {
                    return true;
                }
                else
                {
                    bool oznakaOk = (lokal.OznakaTxt.IndexOf(pretragaLokali.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                    bool imeOk = false;
                    if (lokal.ImeTxt != null)
                    {
                        imeOk = (lokal.ImeTxt.IndexOf(pretragaLokali.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    return oznakaOk || imeOk;
                }

            }
            else if (item is Tip)
            {
                Tip tip = item as Tip;
                if (String.IsNullOrEmpty(pretragaTipovi.Text))
                {
                    return true;
                }
                else
                {
                    bool oznakaOk = (tip.Oznaka.IndexOf(pretragaTipovi.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                    bool imeOk = false;
                    if (tip.Ime != null)
                    {
                        imeOk = (tip.Ime.IndexOf(pretragaTipovi.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    return oznakaOk || imeOk;
                }
            }
            else if (item is Etiketa)
            {
                Etiketa etiketa = item as Etiketa;
                if (String.IsNullOrEmpty(pretragaEtikete.Text))
                {
                    return true;
                }
                else
                {
                    bool oznakaOk = (etiketa.Oznaka.IndexOf(pretragaEtikete.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                    return oznakaOk;
                }
            }
            return false;
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

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            object selected = dataGrid.SelectedItem;
            if (selected != null)
            {
                UserControl podaci = null;
                if (selected is Lokal)
                {
                    podaci = new LokalPodaci(selected as Lokal);
                }
                else if (selected is Tip)
                {
                    podaci = new TipPodaci(selected as Tip);
                }
                else if (selected is Etiketa)
                {
                    podaci = new EtiketaPodaci(selected as Etiketa);
                }
                setDetails(podaci);
            }
            else
            {
                object details = ExpanderPanel.FindName("Details");
                if (details != null)
                {
                    ExpanderPanel.Children.Remove(details as UIElement);
                    ExpanderPanel.UnregisterName("Details");
                }
            }
        }

        private void setDetails(UserControl detailsControl)
        {
            object details = ExpanderPanel.FindName("Details");
            if (details == null)
            {
                ExpanderPanel.Children.Add(detailsControl);
                ExpanderPanel.RegisterName("Details", detailsControl);
            }
            else
            {
                ExpanderPanel.Children.Remove(details as UIElement);
                ExpanderPanel.UnregisterName("Details");

                ExpanderPanel.Children.Add(detailsControl);
                ExpanderPanel.RegisterName("Details", detailsControl);
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                object details = ExpanderPanel.FindName("Details");
                if (details != null)
                {
                    ExpanderPanel.Children.Remove(details as UIElement);
                    ExpanderPanel.UnregisterName("Details");
                }
            }
        }

        private void pretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender.Equals(pretragaLokali))
            {
                CollectionViewSource.GetDefaultView(lokaliDG.ItemsSource).Refresh();
            }
            else if (sender.Equals(pretragaTipovi))
            {
                CollectionViewSource.GetDefaultView(tipoviDG.ItemsSource).Refresh();
            }
            else if (sender.Equals(pretragaEtikete))
            {
                CollectionViewSource.GetDefaultView(etiketeDG.ItemsSource).Refresh();
            }
        }
    }
}

