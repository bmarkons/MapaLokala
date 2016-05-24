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
    /// Interaction logic for EtiketeCheckList.xaml
    /// </summary>
    public partial class EtiketeCheckList : UserControl
    {
        private Lokal lokal;
        public EtiketeCheckList(Lokal lokal)
        {
            InitializeComponent();
            this.lokal = lokal;
            this.DataContext = lokal;

            etiketaCheckList.ItemsSource = ((App)App.Current).Etikete;
            foreach(Etiketa e in etiketaCheckList.Items)
            {
                if (lokal.Etikete.Contains(e))
                {
                    etiketaCheckList.SelectedItems.Add(e);
                }
            }
        }

        //private void etiketaCheckList_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        //{
        //    //if (e.IsSelected)
        //    //{
        //    //    lokal.Etikete.Add((Etiketa)e.Item);
        //    //}
        //    //else
        //    //{
        //    //    lokal.Etikete.Remove((Etiketa)e.Item);
        //    //}
        //}

        private void noviTagBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new NovaEtiketaControl(lokal));
        }

        private void etiketaCheckList_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("data context changed.");
        }
    }
}
