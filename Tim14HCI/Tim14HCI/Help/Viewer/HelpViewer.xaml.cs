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

namespace Tim14HCI.Help.Viewer
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {
        public HelpViewer()
        {
            InitializeComponent();
        }

        public HelpViewer(string key, Window originator)
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();
            string path = String.Format("{0}/Help/Pages/{1}.html", curDir, key);
            Console.WriteLine(path);
            if (!File.Exists(path))
            {                
                key = "error";
            }            
            Uri u = new Uri(String.Format("file:///{0}/Help/Pages/{1}.html", curDir, key));            
            wbHelp.Navigate(u);
        }

        private void wbHelp_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {

        }
    }
}
