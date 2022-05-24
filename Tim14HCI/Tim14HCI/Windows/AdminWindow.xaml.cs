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
using Tim14HCI.DAO;
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

            grid_test.Visibility = Visibility.Collapsed;
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

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void fillStackDataWithTrains() {

            stack_Data.Children.Clear();

            List<Train> trains = TrainDAO.getAllTrains();

            foreach (Train train in trains) {

                TrainControl trainControl = new TrainControl(train);
                stack_Data.Children.Add(trainControl);
            }

        }
        private void fillStackDataWithStations()
        {
            stack_Data.Children.Clear();

            List<Station> stations = StationDAO.getAllStations();

            foreach (Station station in stations)
            {
                StationControl stationControl = new StationControl(station);
                stack_Data.Children.Add(stationControl);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fillStackDataWithTrains();
            lbl_ShownData.Content = "Vozovi";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            fillStackDataWithStations();
            lbl_ShownData.Content = "Stanice";
        }
    }
}
