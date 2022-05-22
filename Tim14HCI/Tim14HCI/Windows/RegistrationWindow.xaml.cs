using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {


        public RegistrationWindow()
        {
            InitializeComponent();
        }

        LogInWindow parent;
        public RegistrationWindow(Window x)
        {
            parent = (LogInWindow)x;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            parent.Show();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string firsName = txbx_firstName.Text;
            string lastName = txbx_lastName.Text;
            string email = txbx_email.Text;
            string phone = txbx_phone.Text;
            string password = txbx_password.Password;
            string passwordCheck = txbx_passwordCheck.Password;

            if (firsName.Length == 0 ||
                lastName.Length == 0 ||
                email.Length == 0 ||
                phone.Length == 0 ||
                password.Length == 0 ||
                passwordCheck.Length == 0
                )
            {
                lbl_message.Content = "Morate popuniti sva polja.";
                return;
            }
            
            if (!IsValidEmailAddress(email))
            {
                lbl_message.Content = "E-mail nije validan.";
                return;
            }
            
            if (password != passwordCheck)
            {
                lbl_message.Content = "Lozinke se moraju poklapati";
                return;
            }

            int newUserID = UsersDAO.registerNewClient(firsName,lastName,email,phone,password);
            //int newUserID = 1 ;
            parent.setMessage("Uspesno ste se registrovali vas ID je : " + newUserID.ToString());
            parent.Show();
            Close();

        }
        public bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

    }
}
