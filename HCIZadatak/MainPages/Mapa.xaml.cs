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
        #region SliderEnabling
        public bool IsUpperSliderEnabled
        {
            get; set;
        }
        public bool IsLowerSliderEnabled
        {
            get; set;
        }
        #endregion

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

        private Point startPoint;

        private Mapa()
        {
            InitializeComponent();

            DataContext = this;

            //DataGrids
            lokaliDG.ItemsSource = ((App)App.Current).Lokali;
            tipoviDG.ItemsSource = ((App)App.Current).Tipovi;
            etiketeDG.ItemsSource = ((App)App.Current).Etikete;

            alkoholFilter.ItemsSource = Lokal.Sluzenje_alkohola;
            ceneFilter.ItemsSource = Lokal.Rang_cena;

            tipoviListFilter.ItemsSource = ((App)App.Current).Tipovi;
            etiketeListFilter.ItemsSource = ((App)App.Current).Etikete;

            dateSliderFilter.Maximum = DateTime.Now;


            foreach (Lokal lokal in ((App)App.Current).Lokali)
            {
                if (!Double.IsNaN(lokal.MapPoint.X))
                {
                    MapItem mapItem = new MapItem(lokal);
                    canvas.Children.Add(mapItem);
                    Canvas.SetLeft(mapItem, lokal.MapPoint.X - 43);
                    Canvas.SetTop(mapItem, lokal.MapPoint.Y - 43);
                }
            }

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

        private bool SearchInFilter(object item)
        {
            if (item is Tip)
            {
                Tip tip = item as Tip;
                if (String.IsNullOrEmpty(pretragaTipoviFilter.Text))
                {
                    return true;
                }
                else
                {
                    bool oznakaOk = (tip.Oznaka.IndexOf(pretragaTipoviFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                    bool imeOk = false;
                    if (tip.Ime != null)
                    {
                        imeOk = (tip.Ime.IndexOf(pretragaTipoviFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    return oznakaOk || imeOk;
                }
            }
            else if (item is Etiketa)
            {
                Etiketa etiketa = item as Etiketa;
                if (String.IsNullOrEmpty(pretragaEtiketeFilter.Text))
                {
                    return true;
                }
                else
                {
                    bool oznakaOk = (etiketa.Oznaka.IndexOf(pretragaEtiketeFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

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
                expanderFilteri.IsExpanded = false;
            }

        }

        public void pretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender.Equals(pretragaLokali))
            {
                ((CollectionView)CollectionViewSource.GetDefaultView(lokaliDG.ItemsSource)).Filter = Search;
                CollectionViewSource.GetDefaultView(lokaliDG.ItemsSource).Refresh();
            }
            else if (sender.Equals(pretragaTipovi))
            {
                ((CollectionView)CollectionViewSource.GetDefaultView(tipoviDG.ItemsSource)).Filter = Search;
                CollectionViewSource.GetDefaultView(tipoviDG.ItemsSource).Refresh();
            }
            else if (sender.Equals(pretragaEtikete))
            {
                ((CollectionView)CollectionViewSource.GetDefaultView(etiketeDG.ItemsSource)).Filter = Search;
                CollectionViewSource.GetDefaultView(etiketeDG.ItemsSource).Refresh();
            }
            else if (sender.Equals(pretragaTipoviFilter))
            {
                ((CollectionView)CollectionViewSource.GetDefaultView(tipoviListFilter.ItemsSource)).Filter = SearchInFilter;
                CollectionViewSource.GetDefaultView(tipoviListFilter.ItemsSource).Refresh();
            }
            else if (sender.Equals(pretragaEtiketeFilter))
            {
                ((CollectionView)CollectionViewSource.GetDefaultView(etiketeListFilter.ItemsSource)).Filter = SearchInFilter;
                CollectionViewSource.GetDefaultView(etiketeListFilter.ItemsSource).Refresh();
            }

        }

        private void ExpanderFilteri_Expanded(object sender, RoutedEventArgs e)
        {
            //zbog pretrage
            pretraga_TextChanged(pretragaTipoviFilter, null);
            pretraga_TextChanged(pretragaEtiketeFilter, null);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            //zbog pretrage
            pretraga_TextChanged(pretragaTipovi, null);
            pretraga_TextChanged(pretragaEtikete, null);
        }

        private void filterBtn_Click(object sender, RoutedEventArgs e)
        {
            ((CollectionView)CollectionViewSource.GetDefaultView(lokaliDG.ItemsSource)).Filter = Filtriraj;
            CollectionViewSource.GetDefaultView(lokaliDG.ItemsSource).Refresh();
        }

        private bool Filtriraj(object item)
        {
            bool? pusenje = pusenjeFilter.IsChecked;
            bool? rezervacije = rezervacijeFilter.IsChecked;
            bool? invalidi = invalidiFiler.IsChecked;

            string cene = ceneFilter.SelectedItem as string;
            string alkohol = alkoholFilter.SelectedItem as string;

            DateTime since = dateSliderFilter.LowerValue;
            DateTime until = dateSliderFilter.UpperValue;

            int? donjiKapacitet = minKapacitetFilter.Value;
            int? gornjiKapacitet = maxKapacitetFilter.Value;

            ObservableCollection<object> tipovi = (ObservableCollection<object>)tipoviListFilter.SelectedItems;
            ObservableCollection<object> etikete = (ObservableCollection<object>)etiketeListFilter.SelectedItems;

            Lokal lokal = item as Lokal;

            //PROVERA

            bool pusenjeOK = lokal.Pusenje == pusenje;
            bool rezervacijeOK = lokal.Rezervacije == rezervacije;
            bool invalidiOK = lokal.Hendikep == invalidi;

            bool ceneOK = true;
            if (cene != null)
            {
                if (lokal.Cena == null)
                {
                    ceneOK = false;
                }
                else
                {
                    ceneOK = cene.Equals(lokal.Cena);
                }
            }
            bool alkoholOK = true;
            if (alkohol != null)
            {
                if (lokal.Alkohol == null)
                {
                    alkoholOK = false;
                }
                else
                {
                    alkoholOK = alkohol.Equals(lokal.Alkohol);
                }
            }

            bool datumOK = true;
            if (lokal.Datum == null || lokal.Datum.Equals(""))
            {
                if (dateSliderFilter.LowerValue != dateSliderFilter.Minimum || dateSliderFilter.UpperValue != dateSliderFilter.Maximum)
                {
                    datumOK = false;
                }
            }
            else if (DateTime.Compare(DateTime.Parse(lokal.Datum), since) <= 0 || DateTime.Compare(DateTime.Parse(lokal.Datum), until) >= 0)
            {
                datumOK = false;
            }

            bool kapacitetOK = false;
            if (donjiKapacitet == null && gornjiKapacitet == null)
            {
                kapacitetOK = true;
            }
            else if (donjiKapacitet == null)
            {
                donjiKapacitet = 0;
                if (lokal.Kapacitet != null)
                {
                    if (lokal.Kapacitet >= donjiKapacitet && lokal.Kapacitet <= gornjiKapacitet)
                    {
                        kapacitetOK = true;
                    }
                }
            }
            else if (gornjiKapacitet == null)
            {
                gornjiKapacitet = int.MaxValue;
                if (lokal.Kapacitet != null)
                {
                    if (lokal.Kapacitet >= donjiKapacitet && lokal.Kapacitet <= gornjiKapacitet)
                    {
                        kapacitetOK = true;
                    }
                }
            }

            bool tipoviOK = false;
            try
            {
                if (tipovi.Count == 0)
                {
                    tipoviOK = true;
                }
                else if (tipovi.Contains(lokal.Tip))
                {
                    tipoviOK = true;
                }
            }
            catch (NullReferenceException ex)
            {
                tipoviOK = true;
            }

            bool etiketeOK = false;
            try
            {
                if (etikete.Count == 0)
                {
                    etiketeOK = true;
                }
                else {
                    foreach (Etiketa e in etikete)
                    {
                        if (lokal.Etikete.Contains(e))
                        {
                            etiketeOK = true;
                            break;
                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                etiketeOK = true;
            }

            return pusenjeOK && rezervacijeOK && invalidiOK && ceneOK && alkoholOK && datumOK && kapacitetOK && tipoviOK && etiketeOK;
        }

        private void ponistiFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            ((CollectionView)CollectionViewSource.GetDefaultView(lokaliDG.ItemsSource)).Filter = null;
            CollectionViewSource.GetDefaultView(lokaliDG.ItemsSource).Refresh();
        }

        private void lokaliDG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void lokaliDG_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                DataGrid dataGrid = sender as DataGrid;
                Lokal lokal = (Lokal)dataGrid.SelectedItem;
                if (lokal != null)
                {
                    DataObject dragData = new DataObject("myFormat", lokal);

                    //postavi mapu i canvas ispred expander-a
                    SetMapFront();

                    canvas.AllowDrop = true;
                    DragDrop.DoDragDrop(dataGrid, dragData, DragDropEffects.Move);
                    setMapBack();
                }
            }
        }

        private void canvas_Drop(object sender, DragEventArgs e)
        {
            //obrisi detalje lokala ako postoje
            object details = ExpanderPanel.FindName("Details");
            if (details != null)
            {
                ExpanderPanel.Children.Remove(details as UIElement);
                ExpanderPanel.UnregisterName("Details");
            }

            if (e.Data.GetDataPresent("myFormat"))
            {

                //prikazi lokal na mapi
                Lokal lokal = e.Data.GetData("myFormat") as Lokal;
                Point dropPoint = e.GetPosition(mapaGrid);
                lokal.MapPoint = dropPoint;

                MapItem mapItem = new MapItem(lokal);
                canvas.Children.Add(mapItem);

                Canvas.SetLeft(mapItem, dropPoint.X - 43);
                Canvas.SetTop(mapItem, dropPoint.Y - 43);

            }
            else if (e.Data.GetDataPresent("moveFormat"))
            {

                MapItem previousItem = (MapItem)e.Data.GetData("moveFormat");
                //obrisi sa prethodnog mesta
                canvas.Children.Remove(previousItem);

                //prikazi lokal na mapi
                Lokal lokal = previousItem.Lokal;
                Point dropPoint = e.GetPosition(mapaGrid);
                lokal.MapPoint = dropPoint;

                MapItem mapItem = new MapItem(lokal);
                canvas.Children.Add(mapItem);

                Canvas.SetLeft(mapItem, dropPoint.X - 43);
                Canvas.SetTop(mapItem, dropPoint.Y - 43);
            }
        }

        public void SetMapFront()
        {
            mapaGrid.Children.Remove(mapaView);
            mapaGrid.Children.Remove(canvas);
            mapaGrid.Children.Insert(mapaGrid.Children.Count, mapaView);
            mapaGrid.Children.Insert(mapaGrid.Children.Count, canvas);
        }

        public void setMapBack()
        {
            mapaGrid.Children.Remove(mapaView);
            mapaGrid.Children.Remove(canvas);
            mapaGrid.Children.Insert(0, canvas);
            mapaGrid.Children.Insert(0, mapaView);
        }

        private void canvas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Lokal lokal = null;
                lokal = (Lokal)e.Data.GetData("myFormat");

                if (!Double.IsNaN(lokal.MapPoint.X))
                {
                    canvas.AllowDrop = false;
                }
            }
        }
    }
}

