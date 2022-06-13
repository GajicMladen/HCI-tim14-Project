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
        public DemoWindow()
        {
            InitializeComponent();
        }

        public DemoWindow(string mode)
        {
            InitializeComponent();
            if (mode == "trains")
            {
                
                mtl.Source = new Uri(@"C://Fakultet/HCI/Projekat/HCI-tim14-Project/Tim14HCI/Tim14HCI/Demo/trains.mp4", UriKind.Absolute);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

        void DemoWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }

    }
}
