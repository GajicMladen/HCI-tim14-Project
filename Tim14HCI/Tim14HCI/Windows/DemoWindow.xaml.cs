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

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for DemoWindow.xaml
    /// </summary>
    public partial class DemoWindow : Window
    {
        Window parent;
        public DemoWindow()
        {
            InitializeComponent();
        }

        public DemoWindow(Window x, string mode)
        {
            parent = x;
            InitializeComponent();
            if (mode == "trains")
            {
                
                mtl.Source = new Uri(@"C://Users/djord/Documents/Fakultet/6_semestar/HCI/Project/HCI-tim14-Project/Tim14HCI/Tim14HCI/Demo/trains.mp4", UriKind.Absolute);
            }
            else if (mode == "stations")
            {

                mtl.Source = new Uri(@"C://Users/djord/Documents/Fakultet/6_semestar/HCI/Project/HCI-tim14-Project/Tim14HCI/Tim14HCI/Demo/stations.mp4", UriKind.Absolute);
            }
            else if (mode == "trainlines")
            {

                mtl.Source = new Uri(@"C://Users/djord/Documents/Fakultet/6_semestar/HCI/Project/HCI-tim14-Project/Tim14HCI/Tim14HCI/Demo/trainlines.mp4", UriKind.Absolute);
            }
            else if (mode == "departures")
            {

                mtl.Source = new Uri(@"C://Users/djord/Documents/Fakultet/6_semestar/HCI/Project/HCI-tim14-Project/Tim14HCI/Tim14HCI/Demo/departures.mp4", UriKind.Absolute);
            }
            else if (mode == "client")
            {
                mtl.Source = new Uri(@"C://Users/djord/Documents/Fakultet/6_semestar/HCI/Project/HCI-tim14-Project/Tim14HCI/Tim14HCI/Demo/client.mkv", UriKind.Absolute);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        void DemoWindow_KeyDown(object sender, KeyEventArgs e)
        {
            parent.Show();
            Close();
        }

    }
}
