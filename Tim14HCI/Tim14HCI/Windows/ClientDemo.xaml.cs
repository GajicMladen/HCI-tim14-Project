using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ClientDemo.xaml
    /// </summary>
    public partial class ClientDemo : Window
    {

        Window parent;

        public ClientDemo()
        {
            InitializeComponent();
        }

        public ClientDemo(Window p)
        {
            parent = p;
            InitializeComponent();     

            mediaElement.LoadedBehavior = MediaState.Manual;            

            string curDir = Directory.GetCurrentDirectory();
            string path = String.Format("{0}/Images/clientDemo.mkv", curDir);
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                Console.WriteLine("Error");
            }
            Uri u = new Uri(String.Format("file:///{0}/Images/clientDemo.mkv", curDir));
            mediaElement.Source = u;
            mediaElement.Play();
            mediaElement.MediaEnded += new RoutedEventHandler(mediaElement_MediaEnded);
        }

        void mediaElement_MediaEnded(object sender, EventArgs e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(0);
            mediaElement.Play();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }
    }
}
