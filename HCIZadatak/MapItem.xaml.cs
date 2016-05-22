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
    /// <summary>
    /// Interaction logic for MapItem.xaml
    /// </summary>
    public partial class MapItem : UserControl
    {
        Point startPoint;
        Lokal lokal;

        public Lokal Lokal
        {
            get
            {
                return lokal;
            }

            set
            {
                lokal = value;
            }
        }

        public MapItem(Lokal lokal)
        {
            InitializeComponent();
            DataContext = lokal;
            this.lokal = lokal;

            //imgPanel.DataContext = this;
            //if (lokal.Hendikep)
            //{
            //    Image invalidImg = new Image();
            //    invalidImg.Width = 20;
            //    //invalidImg.Source = new BitmapImage(new Uri("pack://application:,,,/HCIZadatak;component/Images/disabled.png"));
            //    imgPanel.Children.Add(invalidImg);
            //}
        }

        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);

        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                DataObject dragData = new DataObject("moveFormat", this);

                //mapa ispred expandera
                Mapa.Current.SetMapFront();
                //kako bi moglo da se dropuje i na njega samog
                this.IsEnabled = false;
                //mora
                int zIndex = Canvas.GetZIndex(this);
                Canvas.SetZIndex(this, -100);
                Mapa.Current.canvas.AllowDrop = true;
                DragDropEffects ret = DragDrop.DoDragDrop(this, dragData, DragDropEffects.Move);
                Mapa.Current.setMapBack();
                this.IsEnabled = true;
                Canvas.SetZIndex(this, zIndex);
            }
        }

        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Mapa.Current.canvas.Children.Remove(this);
            lokal.MapPoint = new Point(Double.NaN, Double.NaN);
        }
    }
}

