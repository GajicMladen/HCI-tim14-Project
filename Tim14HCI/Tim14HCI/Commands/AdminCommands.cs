using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tim14HCI.Windows;

namespace Tim14HCI.Commands
{
    public class AdminCommands
    {
        static AdminWindow adminWindow;

        public static RoutedCommand openTrains = new RoutedCommand();
        public static RoutedCommand openStations = new RoutedCommand();
        public static RoutedCommand openTrainLines = new RoutedCommand();
        public static RoutedCommand openSchedule = new RoutedCommand();
        public static RoutedCommand openAddNew = new RoutedCommand();

        static AdminCommands() {

            openTrains.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            openStations.InputGestures.Add(new KeyGesture(Key.W, ModifierKeys.Control));
            openTrainLines.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            openSchedule.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
            openAddNew.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
        }

        public static RoutedCommand OpenTrain
        {
            get { return openTrains; }
        }

        public static RoutedCommand OpenStations
        {
            get { return openStations; }
        }

        public static RoutedCommand OpenTrainLines
        {
            get { return openTrainLines; }
        }

        public static RoutedCommand OpenSchedule
        {
            get { return openSchedule; }
        }
        public static RoutedCommand OpenAddNew
        {
            get { return openAddNew; }
        }


        public static void OpenTrains_Executed(object sender,
                   ExecutedRoutedEventArgs e)
        {
            adminWindow.showTrains();
        }

        public static void OpenTrainLines_Executed(object sender,
                   ExecutedRoutedEventArgs e)
        {
            adminWindow.showTrainLines();
        }
        public static void OpenStations_Executed(object sender,
                   ExecutedRoutedEventArgs e)
        {
            adminWindow.showStations();
        }
        public static void OpenSchedule_Executed(object sender,
                   ExecutedRoutedEventArgs e)
        {
            adminWindow.showDepartures();
        }
        public static void OpenAddNew_Executed(object sender,
                   ExecutedRoutedEventArgs e)
        {
            adminWindow.showAddNew();
        }
        public static void OpenTabs_CanExecute(object sender,
                           CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        public static void BindCommandsToWindow(AdminWindow window)
        {
            adminWindow = window;
            window.CommandBindings.Add(new CommandBinding(OpenTrain, OpenTrains_Executed, OpenTabs_CanExecute));
            window.CommandBindings.Add(new CommandBinding(OpenStations, OpenStations_Executed, OpenTabs_CanExecute));
            window.CommandBindings.Add(new CommandBinding(OpenTrainLines, OpenTrainLines_Executed, OpenTabs_CanExecute));
            window.CommandBindings.Add(new CommandBinding(OpenSchedule, OpenSchedule_Executed, OpenTabs_CanExecute));
            window.CommandBindings.Add(new CommandBinding(OpenAddNew, OpenAddNew_Executed, OpenTabs_CanExecute));
        }
    }
}
