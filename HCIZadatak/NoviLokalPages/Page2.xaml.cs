using HCIZadatak.Validation;
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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private NoviLokal noviLokal;

        public Page2()
        {
            InitializeComponent();
        }

        public Page2(NoviLokal noviLokal)
        {
            InitializeComponent();
            this.DataContext = noviLokal.Novi;
            this.noviLokal = noviLokal;
        }

        private void daljebtn_Click(object sender, RoutedEventArgs e)
        {
            noviLokal.NavigateToPage(3);
        }

        private void nazadbtn_Click(object sender, RoutedEventArgs e)
        {
            noviLokal.NavigateToPage(1);
        }
    }
}
