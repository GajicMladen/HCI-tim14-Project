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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Contorls
{
    /// <summary>
    /// Interaction logic for TrainLineControl.xaml
    /// </summary>
    public partial class TrainLineControl : UserControl
    { 

        public TrainLineControl(TrainLine trainLine)
        {
            InitializeComponent();
            lbl_StartStation.Content = StationDAO.GetStationByID(trainLine.StartStationID).Name;
            lbl_EndStation.Content = StationDAO.GetStationByID(trainLine.EndStationID).Name;

            lbl_OnWayStations.Content = "";

            List<OnWayStation> owsList = OnWayStationDAO.GetAllOnWayStationsByTrainLineID(trainLine.TrainLineID);

            if (owsList.Count > 0)
            {
                int cnt = 1;
                foreach (OnWayStation onWayStation in owsList)
                {
                    if (cnt < owsList.Count)
                        lbl_OnWayStations.Content += StationDAO.GetStationByID(StationDAO.GetOnWayStationByID(onWayStation.OnWayStationID).StationID).Name + " , ";
                    cnt++;
                }
            }
            else
            {
                lbl_OnWayStations.Content = " / ";

            }
            if (lbl_OnWayStations.Content.ToString().Length > 3)
                lbl_OnWayStations.Content = lbl_OnWayStations.Content.ToString().Substring(0, lbl_OnWayStations.Content.ToString().Length - 3);
            else
                lbl_OnWayStations.Content = " / ";
            lbl_price.Content = trainLine.getTotalPrice().ToString();
            lbl_time.Content = trainLine.getTotalTime().ToString();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
