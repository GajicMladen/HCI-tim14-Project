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

        public void fillStackDataWithTrains() {

            stack_Data.Children.Clear();

            List<Train> trains = TrainDAO.getAllTrains();
            //List<Train> trains = new List<Train>();

            foreach (Train train in trains) {

                TrainControl trainControl = new TrainControl(train);
                stack_Data.Children.Add(trainControl);
            }
        }
        public void fillStackDataWithStations()
        {
            stack_Data.Children.Clear();

            List<Station> stations = StationDAO.getAllStations();
            //List<Station> stations = new List<Station>();

            foreach (Station station in stations)
            {
                StationControl stationControl = new StationControl(station);
                stack_Data.Children.Add(stationControl);
            }
        }
        private void fillStackDataWithTrainLines()
        {
            stack_Data.Children.Clear();

            List<TrainLine> trainLines = TrainLinesDAO.getAllTrainLines();
            //List<Station> stations = new List<Station>();

            foreach (TrainLine trainLine in trainLines)
            {
                TrainLineControl stationControl = new TrainLineControl(trainLine);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            lbl_ShownData.Content ="Vozne linije";
            stack_Data.Children.Clear();
            fillStackDataWithTrainLines();


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if ((string)lbl_ShownData.Content == "Vozne linije")
            {
                NewTrainLine newTrainLine = new NewTrainLine(this);
                Visibility = Visibility.Hidden;
                newTrainLine.Show();
            }
            else if ((string)lbl_ShownData.Content == "Vozovi")
            {
                NewTrain newTrain = new NewTrain(this);
                Visibility = Visibility.Hidden;
                newTrain.Show();
            }
            else if ((string)lbl_ShownData.Content == "Stanice")
            {
                NewStation newStation = new NewStation(this);
                Visibility = Visibility.Hidden;
                newStation.Show();
            }
        }
    }
}
