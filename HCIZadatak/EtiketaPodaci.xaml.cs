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
    /// Interaction logic for EtiketaPodaci.xaml
    /// </summary>
    public partial class EtiketaPodaci : UserControl
    {
        public EtiketaPodaci(Etiketa etiketa)
        {
            InitializeComponent();
            DataContext = etiketa;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parent = (StackPanel)Parent;
            parent.Children.Remove(this);
            parent.UnregisterName("Details");
        }
    }
}
