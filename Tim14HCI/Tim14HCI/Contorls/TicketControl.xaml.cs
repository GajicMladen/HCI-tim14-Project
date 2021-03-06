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
    /// Interaction logic for TicketControl.xaml
    /// </summary>
    public partial class TicketControl : UserControl
    {
        Ticket t;

        public TicketControl()
        {
            InitializeComponent();
        }

        public TicketControl(Ticket ticket)
        {
            InitializeComponent();
            this.t = ticket;
            //lbl_start_date_time.Content = this.t.Departure.StartTime.ToString("dd.MM.yyyy. HH:mm");
            lbl_start_date_time.Content = CountStartTimeDuration();
            lbl_end_date_time.Content = CountEndTimeDuration();
            lbl_start_location.Content = this.t.StartStation.Name;
            lbl_end_location.Content = this.t.EndStation.Name;
            lbl_price.Content = this.t.Price.ToString();
            lbl_seat.Content = this.t.Seat.ToString();
        }

        private object CountStartTimeDuration()
        {
            double minutesPassed = 0;
            foreach (OnWayStation ows in OnWayStationDAO.GetAllOnWayStationsByTrainLineID(this.t.Departure.TrainLineID))
            {
                if (ows.Station.Name == this.t.StartStation.Name)
                {
                    minutesPassed += ows.Time;
                    break;
                }
                else
                {
                    minutesPassed += ows.Time;
                }
            }

            return this.t.Departure.StartTime.AddMinutes(minutesPassed).ToString("dd.MM.yyyy. HH:mm");
        }

        private String CountEndTimeDuration()
        {
            double minutesPassed = 0;            
            foreach (OnWayStation ows in OnWayStationDAO.GetAllOnWayStationsByTrainLineID(this.t.Departure.TrainLineID))
            {
                if (ows.Station.Name == this.t.EndStation.Name)
                {
                    minutesPassed += ows.Time;
                    break;
                }
                else
                {
                    minutesPassed += ows.Time;
                }
            }           

            return this.t.Departure.StartTime.AddMinutes(minutesPassed).ToString("dd.MM.yyyy. HH:mm");
        }
    }
}
