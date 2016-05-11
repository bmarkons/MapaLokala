using HCIZadatak.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for NoviLokal.xaml
    /// </summary>
    public partial class NoviLokal : UserControl
    {
        private Page[] pages = new Page[4];

        private Lokal novi = new Lokal();

        public Lokal Novi
        {
            get
            {
                return novi;
            }
        }

        public Page[] Pages
        {
            get
            {
                return pages;
            }
        }
        public NoviLokal()
        {
            InitializeComponent();
            pages[0] = new Page1(this);
            pages[1] = new Page2(this);
            pages[2] = new Page3(this);
            pages[3] = new Page4(this);

            NavigateToPage(1);
        }

        public void NavigateToPage(int pageNum)
        {
            frame1.NavigationService.Navigate(pages[pageNum - 1]);
            switch (pageNum)
            {
                case 1:
                    textBlock1.FontWeight = FontWeights.Bold;
                    textBlock2.FontWeight = FontWeights.Bold;
                    textBlock3.FontWeight = FontWeights.Bold;
                    textBlock4.FontWeight = FontWeights.Normal;
                    textBlock5.FontWeight = FontWeights.Normal;
                    textBlock6.FontWeight = FontWeights.Normal;
                    textBlock7.FontWeight = FontWeights.Normal;
                    textBlock8.FontWeight = FontWeights.Normal;
                    textBlock9.FontWeight = FontWeights.Normal;
                    textBlock10.FontWeight = FontWeights.Normal;
                    textBlock11.FontWeight = FontWeights.Normal;
                    textBlock12.FontWeight = FontWeights.Normal;
                    textBlock13.FontWeight = FontWeights.Normal;
                    break;
                case 2:
                    textBlock1.FontWeight = FontWeights.Normal;
                    textBlock2.FontWeight = FontWeights.Normal;
                    textBlock3.FontWeight = FontWeights.Normal;
                    textBlock4.FontWeight = FontWeights.Bold;
                    textBlock5.FontWeight = FontWeights.Bold;
                    textBlock6.FontWeight = FontWeights.Bold;
                    textBlock7.FontWeight = FontWeights.Bold;
                    textBlock8.FontWeight = FontWeights.Bold;
                    textBlock9.FontWeight = FontWeights.Normal;
                    textBlock10.FontWeight = FontWeights.Normal;
                    textBlock11.FontWeight = FontWeights.Normal;
                    textBlock12.FontWeight = FontWeights.Normal;
                    textBlock13.FontWeight = FontWeights.Normal;
                    break;
                case 3:
                    textBlock1.FontWeight = FontWeights.Normal;
                    textBlock2.FontWeight = FontWeights.Normal;
                    textBlock3.FontWeight = FontWeights.Normal;
                    textBlock4.FontWeight = FontWeights.Normal;
                    textBlock5.FontWeight = FontWeights.Normal;
                    textBlock6.FontWeight = FontWeights.Normal;
                    textBlock7.FontWeight = FontWeights.Normal;
                    textBlock8.FontWeight = FontWeights.Normal;
                    textBlock9.FontWeight = FontWeights.Bold;
                    textBlock10.FontWeight = FontWeights.Bold;
                    textBlock11.FontWeight = FontWeights.Bold;
                    textBlock12.FontWeight = FontWeights.Normal;
                    textBlock13.FontWeight = FontWeights.Normal;
                    break;
                case 4:
                    textBlock1.FontWeight = FontWeights.Normal;
                    textBlock2.FontWeight = FontWeights.Normal;
                    textBlock3.FontWeight = FontWeights.Normal;
                    textBlock4.FontWeight = FontWeights.Normal;
                    textBlock5.FontWeight = FontWeights.Normal;
                    textBlock6.FontWeight = FontWeights.Normal;
                    textBlock7.FontWeight = FontWeights.Normal;
                    textBlock8.FontWeight = FontWeights.Normal;
                    textBlock9.FontWeight = FontWeights.Normal;
                    textBlock10.FontWeight = FontWeights.Normal;
                    textBlock11.FontWeight = FontWeights.Normal;
                    textBlock12.FontWeight = FontWeights.Bold;
                    textBlock13.FontWeight = FontWeights.Bold;
                    break;

            }
        }

        private void textBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender.Equals(textBlock1) || sender.Equals(textBlock2) || sender.Equals(textBlock3))
            {
                NavigateToPage(1);
            }
            else if (sender.Equals(textBlock4) || sender.Equals(textBlock5) || sender.Equals(textBlock6) || sender.Equals(textBlock7) || sender.Equals(textBlock8))
            {
                NavigateToPage(2);
            }
            else if (sender.Equals(textBlock9) || sender.Equals(textBlock10) || sender.Equals(textBlock11))
            {
                NavigateToPage(3);
            }
            else if (sender.Equals(textBlock12) || sender.Equals(textBlock13))
            {
                NavigateToPage(4);
            }
        }

        private void odustanibtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).Navigate(Mapa.Current);
        }
    }
}
