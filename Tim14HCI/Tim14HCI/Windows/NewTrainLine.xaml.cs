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
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for NewTrainLine.xaml
    /// </summary>
    public partial class NewTrainLine : Window
    {
        public NewTrainLine()
        {
            InitializeComponent();
        }

        Window parent;
        public NewTrainLine(Window x)
        {
            parent = x;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stack_Route.Children.Clear();
            if (control_newLineDND.StartStation == null)
            {
                stack_Route.Children.Add(new Label() { Content = "Molimo vas unesite polaznu tacku", Foreground = Brushes.Red });
                return;

            }
            else if (control_newLineDND.EndStation == null) {
                stack_Route.Children.Add(new Label() { Content = "Molimo vas unesite polaznu tacku", Foreground = Brushes.Red });
                return;
            }
            stack_Route.Children.Add(new Label() { Content = control_newLineDND.StartStation.Name, Foreground = Brushes.Black });

            foreach(Station station in control_newLineDND.OnWayStations)
            {
                stack_Route.Children.Add(new Label() { Content = station.Name, Foreground = Brushes.Blue });

            }

            stack_Route.Children.Add(new Label() { Content = control_newLineDND.EndStation.Name, Foreground = Brushes.Black });

            progresBar.SelectedIndex++;
            grid_display.Children.Clear();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }
    }
}
