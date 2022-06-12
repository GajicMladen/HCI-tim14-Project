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
using System.Windows.Shapes;
using Tim14HCI.Contorls;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for TrainLinesDisplay.xaml
    /// </summary>
    public partial class TrainLinesDisplay : Window
    {
        Window parent;
        public TrainLinesDisplay()
        {
            InitializeComponent();
        }

        public TrainLinesDisplay(Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            FillStackDataWithTrainLines();
        }

        private void FillStackDataWithTrainLines()
        {
            stack_Data.Children.Clear();

            List<TrainLine> trainLines = TrainLinesDAO.GetAllTrainLines();

            foreach (TrainLine trainLine in trainLines)
            {
                TrainLineClientControl stationControl = new TrainLineClientControl(trainLine);
                stack_Data.Children.Add(stationControl);
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            parent.Show();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(System.Windows.Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = Help.Providers.HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Help.Providers.HelpProvider.ShowHelp("Client", this);
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
