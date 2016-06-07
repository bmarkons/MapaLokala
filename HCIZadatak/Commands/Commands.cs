using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIZadatak.Commands
{
    public static class Commands
    {
        public static readonly RoutedUICommand Expander = new RoutedUICommand(
            "Expander",
            "Expander",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Q, ModifierKeys.Control),
            }
            );

        public static readonly RoutedUICommand NoviLokal = new RoutedUICommand(
            "NoviLokal",
            "NoviLokal",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.L, ModifierKeys.Control),
            }
            );

        public static readonly RoutedUICommand NoviTip = new RoutedUICommand(
            "NoviTip",
            "NoviTip",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.T, ModifierKeys.Control),
            }
            );

        public static readonly RoutedUICommand NovaEtiketa = new RoutedUICommand(
            "NovaEtiketa",
            "NovaEtiketa",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.E, ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand GoBack = new RoutedUICommand(
            "GoBack",
            "GoBack",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Escape)
            }
            );

        public static readonly RoutedUICommand Next = new RoutedUICommand(
            "Next",
            "Next",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Right,ModifierKeys.Shift)
            }
            );

        public static readonly RoutedUICommand Back = new RoutedUICommand(
            "Back",
            "Back",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Left,ModifierKeys.Shift)
            }
            );

        public static readonly RoutedUICommand Search = new RoutedUICommand(
            "Search",
            "Search",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S,ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand Demo = new RoutedUICommand(
            "Demo",
            "Demo",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D,ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand Filter = new RoutedUICommand(
            "Filter",
            "Filter",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F,ModifierKeys.Control)
            }
            );
    }
}
