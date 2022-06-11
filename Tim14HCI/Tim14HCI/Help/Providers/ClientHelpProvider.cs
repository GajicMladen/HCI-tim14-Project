using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tim14HCI.Help.Viewer;

namespace Tim14HCI.Help.Providers
{
    class ClientHelpProvider
    {
        public static readonly DependencyProperty HelpKeyProperty =
            DependencyProperty.RegisterAttached("HelpKey", typeof(string), typeof(ClientHelpProvider), new PropertyMetadata("index", HelpKey));

        public static string GetHelpKey(DependencyObject obj)
        {
            return obj.GetValue(HelpKeyProperty) as string;
        }

        public static void SetHelpKey(DependencyObject obj, string value)
        {
            obj.SetValue(HelpKeyProperty, value);
        }

        private static void HelpKey(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
        }

        public static void ShowHelp(string key, Window originator)
        {
            HelpViewer hh = new HelpViewer(key, originator);
            hh.Show();
        }
    }
}
