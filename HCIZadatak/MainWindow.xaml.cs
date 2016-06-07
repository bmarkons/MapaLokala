using HCIZadatak.Help;
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

namespace HCIZadatak
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Content = Mapa.Current;
        }

        public void Navigate(UserControl nextPage)
        {
            Content = nextPage;
        }

        private void CommandExpander_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Content is Mapa)
            {
                e.CanExecute = true;
            }
        }

        private void CommandExpander_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Mapa.Current.mainExpander.IsExpanded)
            {
                Mapa.Current.mainExpander.IsExpanded = false;
            }
            else
            {
                Mapa.Current.mainExpander.IsExpanded = true;
            }
        }

        private void CommandNoviLokal_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Content is Mapa)
                e.CanExecute = true;
        }

        private void CommandNoviLokal_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Navigate(new NoviLokal());
        }

        private void CommandNoviTip_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Content is Mapa)
                e.CanExecute = true;
        }

        private void CommandNoviTip_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Navigate(new NoviTip());
        }

        private void CommandNovaEtiketa_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Content is Mapa)
                e.CanExecute = true;
        }

        private void CommandNovaEtiketa_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Navigate(new NovaEtiketa());
        }

        //HELP
        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //DependencyObject focusedControl = (DependencyObject)Content;
            //string str = HelpProvider.GetHelpKey(focusedControl);
            //HelpProvider.ShowHelp(str, this);

            if (((App)App.Current).HelpVisibility)
            {
                ((App)App.Current).HelpVisibility = false;
            }
            else
            {
                ((App)App.Current).HelpVisibility = true;
            }
        }

        private void Demo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Content is Mapa)
                e.CanExecute = true;
        }

        private void Demo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MediaElement mediaElement = new MediaElement();
            mediaElement.Source = new Uri("demo.mp4", UriKind.RelativeOrAbsolute);
            mediaElement.Stretch = Stretch.UniformToFill;
            mediaElement.IsMuted = true;
            Content = mediaElement;
        }

        private void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Content is MediaElement)
            {
                e.CanExecute = true;
                return;
            }
            e.CanExecute = true;
        }

        private void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Content is MediaElement)
            {
                Navigate(Mapa.Current);
                return;
            }
            else if (Content is Mapa)
            {
                IInputElement focusedControl = Keyboard.FocusedElement;
                if (focusedControl.Equals(Mapa.Current.pretragaLokali) || focusedControl.Equals(Mapa.Current.pretragaTipovi) || focusedControl.Equals(Mapa.Current.pretragaEtikete))
                {
                    TextBox pretraga = focusedControl as TextBox;
                    pretraga.Text = "";

                    return;
                }
            }
        }

        private void Search_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!(Content is Mapa && Mapa.Current.mainExpander.IsExpanded))
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = true;
        }

        private void Search_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (Mapa.Current.tabControl.SelectedIndex)
            {
                case 0:
                    Mapa.Current.pretragaLokali.Focus();
                    break;
                case 1:
                    Mapa.Current.pretragaTipovi.Focus();
                    break;
                case 2:
                    Mapa.Current.pretragaEtikete.Focus();
                    break;
            }
        }

        private void Nazad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Content is Mapa)
            {
                if (Mapa.Current.mainExpander.IsExpanded)
                {
                    TabControl tabControl = Mapa.Current.tabControl;
                    if (tabControl.SelectedIndex == 0)
                    {
                        tabControl.SelectedIndex = 2;
                    }
                    else
                    {
                        tabControl.SelectedIndex = tabControl.SelectedIndex - 1;
                    }
                }
            }
        }

        private void Dalje_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Content is Mapa)
            {
                if (Mapa.Current.mainExpander.IsExpanded)
                {
                    TabControl tabControl = Mapa.Current.tabControl;
                    if (tabControl.SelectedIndex == 2)
                    {
                        tabControl.SelectedIndex = 0;
                    }
                    else
                    {
                        tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
                    }
                }
            }
        }

        private void Filter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((Content is Mapa) && Mapa.Current.mainExpander.IsExpanded && (Mapa.Current.tabControl.SelectedIndex == 0))
            {
                if (Mapa.Current.expanderFilteri.IsExpanded)
                {
                    Mapa.Current.expanderFilteri.IsExpanded = false;
                }
                else
                {
                    Mapa.Current.expanderFilteri.IsExpanded = true;
                    Mapa.Current.expanderFilteri.Focus();
                }
            }
        }
    }
}
