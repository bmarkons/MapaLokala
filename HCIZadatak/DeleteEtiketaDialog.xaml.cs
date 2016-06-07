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
    /// Interaction logic for DeleteEtiketaDialog.xaml
    /// </summary>
    public partial class DeleteEtiketaDialog : Window
    {
        private IList selectedItems = new List<Etiketa>();

        public DeleteEtiketaDialog()
        {
            InitializeComponent();
        }

        public DeleteEtiketaDialog(IList selectedItems)
        {
            InitializeComponent();
            foreach (Etiketa e in selectedItems)
            {
                this.selectedItems.Add(e);
            }
        }

        private void zavrsi_Click(object sender, RoutedEventArgs e)
        {
            foreach (Etiketa et in selectedItems)
            {
                if (((App)Application.Current).Etikete.Contains(et))
                {
                    ((App)Application.Current).Etikete.Remove(et);
                }
                foreach (Lokal lokal in ((App)Application.Current).Lokali)
                {
                    if (lokal.Etikete.Contains(et))
                    {
                        lokal.Etikete.Remove(et);
                    }
                }
            }
            Close();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
