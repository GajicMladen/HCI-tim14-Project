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
    /// Interaction logic for DepartureReportItem.xaml
    /// </summary>
    public partial class DepartureReportItem : UserControl
    {
        DepartureTicketInfo Dti { get; set; }

        public DepartureReportItem()
        {
            InitializeComponent();
        }

        public DepartureReportItem(DepartureTicketInfo dti)
        {
            InitializeComponent();
            Dti = dti;
            TrainLine line = TrainLinesDAO.getTrainLineByID(Dti.Departure.TrainLineID);

            lbl_Line.Content = StationDAO.GetStationByID(line.StartStationID).Name + " - " + StationDAO.GetStationByID(line.EndStationID).Name;
            lbl_StartTime.Content = Dti.Departure.StartTime.ToString("dd.MM.yyyy. HH:mm");
            lbl_TotalMoney.Content = Math.Round(Dti.NumOfMoney, 2).ToString();
            lbl_TotalTickets.Content = Dti.NumOfTickets.ToString();
        }
    }
}
