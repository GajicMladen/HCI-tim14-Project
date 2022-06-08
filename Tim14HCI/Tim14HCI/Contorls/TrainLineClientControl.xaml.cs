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
using Tim14HCI.Model;

namespace Tim14HCI.Contorls
{
    /// <summary>
    /// Interaction logic for TrainLineClientControl.xaml
    /// </summary>
    public partial class TrainLineClientControl : UserControl
    {
        public TrainLineClientControl()
        {
            InitializeComponent();
        }
        public TrainLineClientControl(TrainLine trainLine)
        {
            InitializeComponent();
            lbl_StartStation.Content = trainLine.StartStation.Name;
            lbl_EndStation.Content = trainLine.EndStation.Station.Name;

            lbl_OnWayStations.Content = "";
            if (trainLine.OnWayStations.Count > 0)
            {

                foreach (OnWayStation onWayStation in trainLine.OnWayStations)
                {
                    lbl_OnWayStations.Content += onWayStation.Station.Name + " , ";

                }
            }
            else
            {
                lbl_OnWayStations.Content = " / ";

            }
            if (lbl_OnWayStations.Content.ToString() != " / ")
            {
                String content = lbl_OnWayStations.Content.ToString();
                lbl_OnWayStations.Content = content.Remove(content.Length - 2);
            }
            lbl_price.Content = trainLine.getTotalPrice().ToString();
            lbl_time.Content = trainLine.getTotalTime().ToString();

        }
    }
}
