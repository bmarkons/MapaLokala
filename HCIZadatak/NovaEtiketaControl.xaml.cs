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
    /// Interaction logic for NovaEtiketaControl.xaml
    /// </summary>
    public partial class NovaEtiketaControl : UserControl
    {
        private Lokal lokal;
        private Etiketa etiketa = new Etiketa();
        public NovaEtiketaControl(Lokal lokal)
        {
            InitializeComponent(); 
            DataContext = etiketa;
            this.lokal = lokal;
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
            ((App)App.Current).Etikete.Add(etiketa);
            lokal.Etikete.Add(etiketa);
            NavigationService.GetNavigationService(this).Navigate(new EtiketeCheckList(lokal));
            e.Handled = true;
        }

        private void odustaniBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new EtiketeCheckList(lokal));
        }
    }
}
