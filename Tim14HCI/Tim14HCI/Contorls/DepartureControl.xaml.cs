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
    /// Interaction logic for DepartureControl.xaml
    /// </summary>
    /// 
    public class DepartureChosenEventArgs : RoutedEventArgs
    {
        private readonly int departureId;
        private readonly int onWayStationId;
        private readonly String price;
        private readonly String arrivalTime;

        public int DepartureID
        {
            get { return departureId; }
        }

        public int OnWayStationID
        {
            get { return onWayStationId; }
        }

        public String Price
        {
            get { return price; }
        }

        public String ArrivalTime
        {
            get { return arrivalTime; }
        }

        public DepartureChosenEventArgs(RoutedEvent routedEvent, int departureId, int owsId, String price, String arrivalTime) : base(routedEvent)
        {
            this.departureId = departureId;
            this.onWayStationId = owsId;
            this.price = price;
            this.arrivalTime = arrivalTime;
        }
    }

    public partial class DepartureControl : UserControl
    {
        Departure departure;
        OnWayStation endLocation;
        bool isEndStation;

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

        public DepartureControl(Departure d, OnWayStation ows, bool isEndStationParam)
        {
            InitializeComponent();

            departure = d;
            endLocation = ows;
            isEndStation = isEndStationParam;

            lbl_start_date_time.Content = departure.startDate.ToString("dd.MM.yyyy. HH:mm");
            lbl_end_date_time.Content = CountTimeDuration(isEndStation);
            lbl_start_location.Content = departure.TrainLine.StartStation.Name;
            lbl_end_location.Content = endLocation.Station.Name;
            lbl_price.Content = CountPrice(isEndStation);
        }

        private String CountPrice(bool isEndStation)
        {
            double price = 0;

            foreach (OnWayStation ows in departure.TrainLine.OnWayStations)
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
            if (isEndStation)
            {
                price += departure.TrainLine.EndStation.Price;
            }
            return price.ToString();
        }

        private String CountTimeDuration(bool isEndStation)
        {
            double minutesPassed = 0;

            foreach (OnWayStation ows in departure.TrainLine.OnWayStations)
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
            if (isEndStation)
            {
                minutesPassed += departure.TrainLine.EndStation.Time;
            }
            return departure.startDate.AddMinutes(minutesPassed).ToString("dd.MM.yyyy. HH:mm");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Raise the custom routed event, this fires the event from the UserControl
            DepartureChosenEventArgs args = new DepartureChosenEventArgs(DepartureControl.DepartureChosenEvent, departure.DepartureID, endLocation.OnWayStationID, CountPrice(isEndStation), CountTimeDuration(isEndStation));
            RaiseEvent(args);
        }
    }
}
