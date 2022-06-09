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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for NewDeparture.xaml
    /// </summary>
    public partial class NewDeparture : Window
    {
        private Window parent;
        private string mode;

        private Departure departure;

        private List<TrainLine> trainLines;
        
        public NewDeparture()
        {
            mode = "none";
            InitializeComponent();
        }

        public NewDeparture(Window x)
        {
            parent = x;
            mode = "new";
            InitializeComponent();
            departure = new Departure();

            trainLines = TrainLinesDAO.getAllTrainLines();
            foreach (var line in trainLines)
            {
                var listItem = new ListBoxItem();
                listItem.Content = "ID: " + line.TrainLineID + ", Putanja: " + StationDAO.GetStationByID(line.StartStationID).Name;

                List<OnWayStation> ows = StationDAO.GetOnWayStations(line.TrainLineID);
                foreach (OnWayStation o in ows)
                {
                    listItem.Content += " - " + StationDAO.GetStationByID(o.StationID).Name;
                }
                linesListBox.Items.Add(listItem);
            }

            startDTPicker.Language = XmlLanguage.GetLanguage("sr-Latn-RS");
            startDTPicker.Minimum = DateTime.Now.AddDays(1).Subtract(DateTime.Now.TimeOfDay);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            parent.Show();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {

        }
    }
}
