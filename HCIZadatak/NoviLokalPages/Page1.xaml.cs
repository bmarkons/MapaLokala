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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private NoviLokal noviLokal;

        public Page1()
        {
            InitializeComponent();
        }

        public Page1(NoviLokal noviLokal)
        {
            InitializeComponent();
            this.DataContext = noviLokal.Novi;
            this.noviLokal = noviLokal;
        }

        public bool HasErrors
        {
            get
            {
                return System.Windows.Controls.Validation.GetHasError(oznakatext);
            }
        }

        //private void daljebtn_Click(object sender, RoutedEventArgs e)
        //{
        //    noviLokal.NavigateToPage(2);
        //}

        private void Dalje_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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

        private void Dalje_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            noviLokal.NavigateToPage(2);
            e.Handled = true;
        }
    }
}
