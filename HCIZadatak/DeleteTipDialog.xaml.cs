using HCIZadatak.Entiteti;
using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace HCIZadatak
{
    /// <summary>
    /// Interaction logic for DeleteTipDialog.xaml
    /// </summary>
    public partial class DeleteTipDialog : Window
    {
        private IList selectedItems = new List<Tip>();

        public DeleteTipDialog()
        {

        }

        public DeleteTipDialog(IList selectedItems)
        {
            foreach (Tip tip in selectedItems)
            {
                this.selectedItems.Add(tip);
            }
            InitializeComponent();
            tipoviDG.ItemsSource = ((App)Application.Current).Tipovi;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)brisi.IsChecked)
            {
                foreach (Lokal lokal in ((App)Application.Current).Lokali.ToArray())
                {
                    if (selectedItems.Contains(lokal.Tip))
                    {
                        ((App)Application.Current).Lokali.Remove(lokal);
                    }
                }
                foreach (Tip tip in selectedItems)
                {
                    ((App)Application.Current).Tipovi.Remove(tip);
                }
            }
            else if ((bool)prevezi.IsChecked)
            {
                Tip prevez = (Tip)tipoviDG.SelectedItem;
                foreach (Lokal lokal in ((App)Application.Current).Lokali.ToArray())
                {
                    if (selectedItems.Contains(lokal.Tip))
                    {
                        lokal.Tip = prevez;
                    }
                }
                foreach (Tip tip in selectedItems)
                {
                    ((App)Application.Current).Tipovi.Remove(tip);
                }
            }
            Close();
        }
    }
}
