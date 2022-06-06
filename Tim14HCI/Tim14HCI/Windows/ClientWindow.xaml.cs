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
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        Window parent;
        User user;

        public ClientWindow()
        {
            InitializeComponent();
            fillStackDataWithDepartures();
        }
     
        public ClientWindow(Window x, User user)
        {
            parent = x;
            this.user = user;
            InitializeComponent();
            lbl_logedUser.Content = user.FirstName + " " + user.LastName;
            lbl_userRole.Content = "Korisnik";
            fillStackDataWithDepartures();
            //grid_test.Visibility = Visibility.Collapsed;
            AddHandler(DepartureControl.DepartureChosenEvent,
                new RoutedEventHandler(DepartureChosenEventHandler));

        }

        private void DepartureChosenEventHandler(object sender, RoutedEventArgs e)
        {
            DepartureChosenEventArgs args = (DepartureChosenEventArgs)e;
            Console.WriteLine(args.DepartureID);
            //btn_synchronize.IsEnabled = true;
        }



        private void fillStackDataWithDepartures()
        {
            stack_Data.Children.Clear();
            List<Departure> departures = DAO.DepartureDAO.getDapertures();

            foreach (Departure departure in departures)
            {

                DepartureControl departureControl = new DepartureControl(departure);
                stack_Data.Children.Add(departureControl);
            }
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
