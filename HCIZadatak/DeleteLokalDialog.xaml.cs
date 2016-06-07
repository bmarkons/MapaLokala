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
    /// Interaction logic for DeleteLokalDialog.xaml
    /// </summary>
    public partial class DeleteLokalDialog : Window
    {
        private IList selectedItems = new List<Lokal>();

        public DeleteLokalDialog()
        {
            InitializeComponent();
        }

        public DeleteLokalDialog(IList selectedItems)
        {
            foreach(Lokal lokal in selectedItems)
            {
                this.selectedItems.Add(lokal);
            }

            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void zavrsi_Click(object sender, RoutedEventArgs e)
        {
            foreach(Lokal lokal in selectedItems)
            {
                ((App)Application.Current).Lokali.Remove(lokal);
            }
            Close();
        }
    }
}
