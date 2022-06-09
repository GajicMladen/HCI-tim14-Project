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
    /// Interaction logic for DepartureControl.xaml
    /// </summary>
    public partial class DepartureManagerControl : UserControl
    {
        Departure departure;

        public DepartureManagerControl()
        {
            InitializeComponent();
        }

        public DepartureManagerControl(Departure x)
        {
            departure = x;
            InitializeComponent();

            TrainLine line = TrainLinesDAO.getTrainLineByID(departure.TrainLineID);

            lbl_Line.Content = StationDAO.GetStationByID(line.StartStationID).Name + " - " + StationDAO.GetStationByID(StationDAO.GetOnWayStationByID(line.EndStationID).StationID).Name;
            lbl_StartTime.Content = departure.StartTime.ToString("dd.MM.yyyy. HH:mm");
            lbl_EndTime.Content = departure.StartTime.AddMinutes(StationDAO.GetTrainLineDuration(departure.TrainLineID)).ToString("dd.MM.yyyy. HH:mm");
        }
    }
}