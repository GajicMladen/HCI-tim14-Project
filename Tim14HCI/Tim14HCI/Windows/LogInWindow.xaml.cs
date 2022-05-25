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
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txbx_username.Text;
            string password = psbx_password.Password;

            //User user = UsersDAO.tryLogIn(username,password);
            User user = new User() { FirstName="Mladen", LastName="Gajic",UserRole=UserRole.MANAGER, Password="a",UserID=1,Email="a",Phone="00532",Tickets=null};
            if (user == null)
            {
                lbl_message.Content = "Neispravni kredencijali";
            }
            else {
                lbl_message.Content = "Uspesno ulogovani";
                if (user.UserRole == UserRole.CLIENT)
                {
                    ClientWindow clientWindow = new ClientWindow(this);
                    Visibility = Visibility.Hidden;
                    clientWindow.Show();
                }
                else {
                    AdminWindow clientWindow = new AdminWindow(this,user);
                    Visibility = Visibility.Hidden;
                    clientWindow.Show();
                }
                lbl_message.Content = "";
                txbx_username.Text = "";
                psbx_password.Password = "";
            }
        }

        private void txbx_username_KeyDown(object sender, KeyEventArgs e)
        {
            lbl_message.Content = "";
            if (e.Key == Key.Return) {
                Button_Click(sender,e);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            RegistrationWindow registrationWindow = new RegistrationWindow(this);
            Visibility = Visibility.Hidden;
            registrationWindow.Show();
        
        }

        public void setMessage(string msg) {
            lbl_message.Content = msg;
        }
    }
}
