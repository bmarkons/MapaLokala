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

namespace HCIZadatak.Validation
{
    /// <summary>
    /// Interaction logic for NoviTip.xaml
    /// </summary>
    public partial class NoviTip : UserControl
    {

        private bool edit = false;
        private int index;
        private Tip stari;

        private Tip novi = new Tip();

        public NoviTip()
        {
            InitializeComponent();
            this.DataContext = novi;
        }

        public NoviTip(Tip tip)
        {
            index = ((App)App.Current).Tipovi.IndexOf(tip);
            ((App)App.Current).Tipovi.Remove(tip);
            stari = tip;
            edit = true;
            novi = stari.Clone();
            ((App)App.Current).EditingTip = new Tuple<Tip, int>(stari, index);

            InitializeComponent();
            this.DataContext = novi;

            title.Text = "Izmena podataka tipa";
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

        private void odustanibtn_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                ((App)App.Current).Tipovi.Insert(index, stari);
                Mapa.Current.tipoviDG.SelectedItem = stari;
                ((App)App.Current).EditingTip = null;
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
                ((App)App.Current).Tipovi.Insert(index, novi);
                ((App)App.Current).EditingTip = null;
            }
            else
            {
                ((App)App.Current).Tipovi.Add(novi);
            }
            Mapa.Current.tipoviDG.SelectedItem = novi;
            ((MainWindow)App.Current.MainWindow).Navigate(Mapa.Current);
            e.Handled = true;
        }
    }
}
