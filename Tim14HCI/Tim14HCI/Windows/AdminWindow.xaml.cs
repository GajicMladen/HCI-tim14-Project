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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        Window parent;
        User user;
        public AdminWindow(Window x,User user)
        {
            parent = x;
            this.user = user;
            InitializeComponent();
            lbl_logedUser.Content = user.FirstName + " " + user.LastName;
            lbl_userRole.Content = user.UserRole.ToString().ToLower();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void btn_logOut_Click(object sender, RoutedEventArgs e)
        {
            parent.Show();
            Close();
        }
    }
}
