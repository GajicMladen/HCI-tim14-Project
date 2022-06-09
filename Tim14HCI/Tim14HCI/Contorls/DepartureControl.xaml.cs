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
    /// 

    public enum DepartureEnum {
        Departure,
        OnWayDeparture
    }

    public class DepartureChosenEventArgs : RoutedEventArgs
    {
        private readonly int departureId;
        private readonly int endStationId;  //OnWayStationId
        private readonly int startStationId;  //OnWayStationId
        private readonly String price;
        private readonly String arrivalTime;
        private readonly String startTime;
        private readonly String endTime;

        public int DepartureID
        {
            get { return departureId; }
        }

        public int EndStationID
        {
            get { return endStationId; }
        }

        public int StartStationID
        {
            get { return startStationId; }
        }

        public String Price
        {
            get { return price; }
        }

        public String ArrivalTime
        {
            get { return arrivalTime; }
        }

        public String StartTime
        {
            get { return startTime; }
        }

        public String EndTime
        {
            get { return endTime; }
        }

        public DepartureChosenEventArgs(RoutedEvent routedEvent, int departureId, int startStationId, int endStationId, String price, String arrivalTime, String startTime, String endTime) : base(routedEvent)
        {
            this.departureId = departureId;
            this.startStationId = startStationId;
            this.endStationId = endStationId;
            this.price = price;
            this.arrivalTime = arrivalTime;
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }

    public partial class DepartureControl : UserControl
    {
        Departure departure;
        OnWayStation startLocation;
        OnWayStation endLocation;

        DepartureEnum departureEnum;

        public static readonly RoutedEvent DepartureChosenEvent =
        EventManager.RegisterRoutedEvent("DepartureChosen", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(DepartureControl));

        public event RoutedEventHandler DepartureChosen
        {
            add { AddHandler(DepartureChosenEvent, value); }
            remove { RemoveHandler(DepartureChosenEvent, value); }
        }

        public DepartureControl()
        {
            InitializeComponent();
        }

        public DepartureControl(Departure d, OnWayStation ows)
        {
            InitializeComponent();

            departure = d;
            endLocation = ows;
            departureEnum = DepartureEnum.Departure;
            lbl_start_date_time.Content = departure.StartTime.ToString("dd.MM.yyyy. HH:mm");
            lbl_end_date_time.Content = CountTimeDuration();           
            lbl_start_location.Content = TrainLinesDAO.GetStartStationByTrainLineID(departure.TrainLineID).Name;
            lbl_end_location.Content = endLocation.Station.Name;
            lbl_price.Content = CountPrice();
        }

        public DepartureControl(Departure d, OnWayStation startLocationParam, OnWayStation endLocationParam)
        {
            InitializeComponent();

            departureEnum = DepartureEnum.OnWayDeparture;
            departure = d;
            startLocation = startLocationParam;
            endLocation = endLocationParam;
            String[] times = CountTimeDurationOnWayStations();
            lbl_start_date_time.Content = times[0];
            lbl_end_date_time.Content = times[1];
            lbl_start_location.Content = startLocation.Station.Name;
            lbl_end_location.Content = endLocation.Station.Name;
            lbl_price.Content = CountPriceOnWayStations();
        }

        private String CountPrice()
        {
            double price = 0;
            
            foreach (OnWayStation ows in OnWayStationDAO.GetAllOnWayStationsByTrainLineID(departure.TrainLineID))
            {
                if (ows.OnWayStationID == endLocation.OnWayStationID)
                {
                    price += ows.Price;
                    break;
                }
                else
                {
                    price += ows.Price;
                }
            }
            
            return price.ToString();
        }

        private String CountTimeDuration()
        {
            double minutesPassed = 0;
            
            foreach (OnWayStation ows in OnWayStationDAO.GetAllOnWayStationsByTrainLineID(departure.TrainLineID))
            {
                if (ows.OnWayStationID == endLocation.OnWayStationID)
                {
                    minutesPassed += ows.Time;
                    break;
                }
                else
                {
                    minutesPassed += ows.Time;
                }
            }       
            return departure.StartTime.AddMinutes(minutesPassed).ToString("dd.MM.yyyy. HH:mm");
        }

        private String[] CountTimeDurationOnWayStations()
        {
            int ii = 1;
            DateTime startTime = departure.StartTime;
            DateTime endTime = departure.StartTime;   
            while (true)
            {
                OnWayStation ows = OnWayStationDAO.GetOnWayStationByOrderNumber(departure.TrainLineID, ii);
                
                if (ows == null)
                {
                    break;
                }

                if (ows.StationOrder <= startLocation.StationOrder)
                {
                    startTime = startTime.AddMinutes(ows.Time);
                    endTime = endTime.AddMinutes(ows.Time);
                }
                else if (ows.StationOrder <= endLocation.StationOrder)
                {
                    endTime = endTime.AddMinutes(ows.Time);
                }
                else
                {
                    break;
                }

                ii++;
            }
            String[] times = { startTime.ToString("dd.MM.yyyy. HH:mm"), endTime.ToString("dd.MM.yyyy. HH:mm") };
            
            return times;
        }

        private String CountPriceOnWayStations()
        {
            int ii = 1;
            double price = 0;
            while (true)
            {
                OnWayStation ows = OnWayStationDAO.GetOnWayStationByOrderNumber(departure.TrainLineID, ii);
                if (ows == null)
                {
                    break;
                }
                if (ows.StationOrder > startLocation.StationOrder && ows.StationOrder <= endLocation.StationOrder)
                {
                    price += ows.Price;
                }
                ii++;
            }
            return price.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Raise the custom routed event, this fires the event from the UserControl
            if (departureEnum == DepartureEnum.Departure)
            {
                DepartureChosenEventArgs args = new DepartureChosenEventArgs(DepartureControl.DepartureChosenEvent, departure.DepartureID, 0, endLocation.OnWayStationID, CountPrice(), CountTimeDuration(), "", "");
                RaiseEvent(args);
            }
            else
            {
                String[] times = CountTimeDurationOnWayStations();
                DepartureChosenEventArgs args = new DepartureChosenEventArgs(DepartureControl.DepartureChosenEvent, departure.DepartureID, startLocation.OnWayStationID, endLocation.OnWayStationID, CountPriceOnWayStations(), "", times[0], times[1]);
                RaiseEvent(args);
            }
        }
    }
}
