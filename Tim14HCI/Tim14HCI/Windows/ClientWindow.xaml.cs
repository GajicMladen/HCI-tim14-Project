using System;
using System.Collections.Generic;
using System.Globalization;
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
using Tim14HCI.Help.Providers;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    /// 

    public enum DepartureEnum
    {
        Departure,
        OnWayDeparture
    }

    public partial class ClientWindow : Window
    {
        Window parent;
        User user;
        int chosenDeparture;
        int chosenStartLocation;
        int chosenEndLocation;
        String price;

        String startLocationSearch;
        String endLocationSearch = "";
        String startDatetimeSearch;
        String endDatetimeSearch;        
        int capacity;
        List<SearchableDeparture> searchableDepartures = new List<SearchableDeparture>();

        String[] acceptableDateTimeFormats = { "dd.MM.yyyy.", "dd.MM.yyyy. HH:mm" };

        public ClientWindow()
        {
            InitializeComponent();
            fillStackDataWithDepartures();
        }
     
        public ClientWindow(Window x, User user)
        {
            parent = x;
            this.user = user;
            InitializeComponent();
            lbl_logedUser.Content = user.FirstName + " " + user.LastName;
            lbl_userRole.Content = "Korisnik";
            fillStackDataWithDepartures();            
            AddHandler(DepartureControl.DepartureChosenEvent,
                new RoutedEventHandler(DepartureChosenEventHandler));

        }

        private void DepartureChosenEventHandler(object sender, RoutedEventArgs e)
        {
            DepartureChosenEventArgs args = (DepartureChosenEventArgs)e;
            if (args.StartStationID == 0)
            {
                chosenDeparture = args.DepartureID;
                chosenEndLocation = args.EndStationID;
                Departure departure = DepartureDAO.GetDepartureByID(chosenDeparture);
                chosenStartLocation = departure.TrainLine.StartStationID;
                OnWayStation onWayStation = OnWayStationDAO.GetOnWayStationByID(chosenEndLocation);
                price = args.Price;
                lbl_departure.Content = departure.StartTime.ToString("dd.MM.yyyy. HH:mm") + "    " + args.ArrivalTime + "    " + departure.TrainLine.StartStation.Name + "    " + onWayStation.Station.Name + "    " + price;
                lbl_range.Content = "(1-" + TrainDAO.GetTrainByID(departure.TrainLine.TrainID).Capacity.ToString() + ")";
                capacity = TrainDAO.GetTrainByID(departure.TrainLine.TrainID).Capacity;
            }
            else
            {
                chosenDeparture = args.DepartureID;
                chosenEndLocation = args.EndStationID;
                chosenStartLocation = args.StartStationID;
                Departure departure = DepartureDAO.GetDepartureByID(chosenDeparture);                
                OnWayStation onWayStation = OnWayStationDAO.GetOnWayStationByID(chosenEndLocation);
                OnWayStation startStation = OnWayStationDAO.GetOnWayStationByID(chosenStartLocation);
                chosenStartLocation = startStation.StationID;
                price = args.Price;
                lbl_departure.Content = args.StartTime + "    " + args.EndTime + "    " + startStation.Station.Name + "    " + onWayStation.Station.Name + "    " + price;
                lbl_range.Content = "(1-" + TrainDAO.GetTrainByID(departure.TrainLine.TrainID).Capacity.ToString() + ")";
                capacity = TrainDAO.GetTrainByID(departure.TrainLine.TrainID).Capacity;
            }

        }

        private void fillStackDataWithDepartures()
        {
            stack_Data.Children.Clear();
            searchableDepartures.Clear();
            DepartureHeaderControl dhc = new DepartureHeaderControl();
            stack_Data.Children.Add(dhc);
            List<Departure> departures = DepartureDAO.GetAllDepartures();

            foreach (Departure departure in departures)
            {
                if (DateTime.Compare(departure.StartTime, DateTime.Now) >= 0)
                {
                    foreach (OnWayStation ows in OnWayStationDAO.GetAllOnWayStationsByTrainLineID(departure.TrainLineID))
                    {
                        DepartureControl departureControl = new DepartureControl(departure, ows);
                        SearchableDeparture sd = new SearchableDeparture(departure, ows, DepartureEnum.Departure);
                        searchableDepartures.Add(sd);
                        stack_Data.Children.Add(departureControl);
                    }
                    fillStacDataWithOnWayDepartures(departure);
                }                
            }           
        }

        private void fillStacDataWithOnWayDepartures(Departure departure)
        {                                
            foreach (OnWayStation ows in OnWayStationDAO.GetAllOnWayStationsByTrainLineID(departure.TrainLineID))
            {
                int ii = 1;
                while (true)
                {
                    OnWayStation endStation = OnWayStationDAO.GetOnWayStationByOrderNumber(departure.TrainLineID, ii);
                    if (endStation == null)
                    {
                        break;
                    }
                    if (endStation.StationOrder > ows.StationOrder)
                    {
                        DepartureControl departureControl = new DepartureControl(departure, ows, endStation);
                        SearchableDeparture sd = new SearchableDeparture(departure, ows, endStation, DepartureEnum.OnWayDeparture);
                        searchableDepartures.Add(sd);
                        stack_Data.Children.Add(departureControl);
                    }
                    ii++;
                }
                    
            }            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp("Client", this);
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void btn_logOut_Click(object sender, RoutedEventArgs e)
        {
            parent.Show();
            Close();
        }

        private void ReservationClick(object sender, RoutedEventArgs e)
        {
            Departure departure = DepartureDAO.GetDepartureByID(chosenDeparture);
            if (departure == null)
            {
                MessageBox.Show("Ruta nije izabrana!");
                return;
            }
            OnWayStation ows = OnWayStationDAO.GetOnWayStationByID(chosenEndLocation);
            int sn = GetSeatNumber();
            if (sn == 0)
            {
                return;
            }
            Ticket ticket = new Ticket(true, departure.DepartureID, chosenStartLocation, ows.StationID, Double.Parse(price), user.UserID, sn);            
            TicketDAO.AddTicket(ticket);
            MessageBox.Show("Karta uspešno rezervisana!");
            lbl_range.Content = "";
            txbx_seat.Text = "";
            lbl_departure.Content = "";
        }     

        private void BuyClick(object sender, RoutedEventArgs e)
        {
            Departure departure = DepartureDAO.GetDepartureByID(chosenDeparture);
            if (departure == null)
            {
                MessageBox.Show("Ruta nije izabrana!");
                return;
            }
            OnWayStation ows = OnWayStationDAO.GetOnWayStationByID(chosenEndLocation);
            int sn = GetSeatNumber();
            if (sn == 0)
            {
                return;
            }
            Ticket ticket = new Ticket(false, departure.DepartureID, chosenStartLocation, ows.StationID, Double.Parse(price), user.UserID, sn);            
            TicketDAO.AddTicket(ticket);
            MessageBox.Show("Karta uspešno kupljena!");
            lbl_range.Content = "";
            txbx_seat.Text = "";
            lbl_departure.Content = "";
        }

        private void DemoMode(object sender, RoutedEventArgs e)
        {
            /*
            ClientDemo clientDemo = new ClientDemo(this);
            Visibility = Visibility.Hidden;
            clientDemo.Show();
            */
            DemoWindow dw = new DemoWindow(this, "client");
            Visibility = Visibility.Hidden;
            dw.Show();
        }

        private void ShowBoughtTickets(object sender, RoutedEventArgs e)
        {
            TicketDisplayWindow ticketDisplayWindow = new TicketDisplayWindow(this, "Bought", user);
            Visibility = Visibility.Hidden;
            ticketDisplayWindow.Show();
        }

        private void ShowReservationTickets(object sender, RoutedEventArgs e)
        {
            TicketDisplayWindow ticketDisplayWindow = new TicketDisplayWindow(this, "Reservation", user);
            Visibility = Visibility.Hidden;
            ticketDisplayWindow.Show();
        }

        private void ShowTrainLines(object sender, RoutedEventArgs e)
        {
            TrainLinesDisplay trainLinesWindow = new TrainLinesDisplay(this);
            Visibility = Visibility.Hidden;
            trainLinesWindow.Show();
        }

        private void CancelSearch(object sender, RoutedEventArgs e)
        {
            txbx_start_time.Text = "";
            txbx_end_time.Text = "";
            txbx_start_location.Text = "";
            txbx_end_location.Text = "";
            fillStackDataWithDepartures();
        }

        private void Search(object sender, RoutedEventArgs e)
        {

            this.startLocationSearch = txbx_start_location.Text;
            this.endLocationSearch = txbx_end_location.Text;
            this.startDatetimeSearch = txbx_start_time.Text;
            this.endDatetimeSearch = txbx_end_time.Text;

            if (!CheckDatetimeInput())
            {
                MessageBox.Show("Pogrešan format datuma!\nPrimeri prihvatljivih formata datuma: 21.04.2022. ili 21.04.2022. 14:00");
                return;
            }

            List<SearchableDeparture> startingStationFiltered = FilterByStartingStation(searchableDepartures);
            List<SearchableDeparture> destinationFiltered = FilterByDestination(startingStationFiltered);
            List<SearchableDeparture> startDateFiltered = FilterByStartDatetime(destinationFiltered);
            List<SearchableDeparture> endDateFiltered = FilterByEndDatetime(startDateFiltered);

            stack_Data.Children.Clear();
            if (endDateFiltered.Count > 0)
            {
                DepartureHeaderControl dhc = new DepartureHeaderControl();
                stack_Data.Children.Add(dhc);
                foreach (SearchableDeparture departure in endDateFiltered)
                {
                    if (departure.DepartureKind == DepartureEnum.Departure)
                    {
                        DepartureControl departureControl = new DepartureControl(departure.Departure, departure.EndLocation);
                        stack_Data.Children.Add(departureControl);
                    }
                    else
                    {
                        DepartureControl departureControl = new DepartureControl(departure.Departure, departure.StartLocation, departure.EndLocation);
                        stack_Data.Children.Add(departureControl);
                    }
                }
            }
            else {
                stack_Data.Children.Add(new Label() { Content = "Trenutno nema takvih polazaka!", FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center });
                TrainLine existingTrainLine = TrainLinesDAO.checkExistLine(startLocationSearch, endLocationSearch);
                if (existingTrainLine != null)
                {
                    stack_Data.Children.Add(new Label() { Content = "U sistemu postoji takva direktna linija.\nSamo trenutno nema polazaka.", FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center });

                }
                else {
                    stack_Data.Children.Add(new Label() { Content = "U sistemu nema direktna linija!", FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center });

                    
                    List<Station> connectedStations = StationDAO.GetConnectedStations(startLocationSearch, endLocationSearch);
                    if (connectedStations.Count > 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Od vaše polazne stanice POKUŠAJTE da stignete do cilja sa presedanjem u stanicama:", FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center });
                        foreach (Station s in connectedStations) {
                            stack_Data.Children.Add(new Label() { Content = startLocationSearch.ToUpper() +" => "+s.Name.ToUpper()+" => "+endLocationSearch.ToUpper(), FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center });
                        }
                    }
                    else {
                        stack_Data.Children.Add(new Label() { Content = "Od vaše polazne stanice do odredišta je nemoguće stići sa jednim presedanjem.", FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center });
                        stack_Data.Children.Add(new Label() { Content = "Proverite da li ste lepo uneli podatke.", FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center });

                    }
                }
            }
        }
        
        private List<SearchableDeparture> FilterByEndDatetime(List<SearchableDeparture> departures)
        {
            List<SearchableDeparture> filtered = new List<SearchableDeparture>();
            if (this.endDatetimeSearch != "")
            {
                DateTime dt; // = DateTime.ParseExact(this.endDatetimeSearch, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                bool success = DateTime.TryParseExact(this.endDatetimeSearch, this.acceptableDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                foreach (SearchableDeparture departure in departures)
                {                  
                    if (DateTime.Compare(departure.GetEndTime(), dt) <= 0)
                    {
                        filtered.Add(departure);
                    }
                }
            }
            else
            {
                filtered = departures;
            }
            return filtered;
        }

        private List<SearchableDeparture> FilterByStartDatetime(List<SearchableDeparture> departures)
        {
            List<SearchableDeparture> filtered = new List<SearchableDeparture>();
            if (this.startDatetimeSearch != "")
            {
                DateTime dt; // = DateTime.ParseExact(this.startDatetimeSearch, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                bool success = DateTime.TryParseExact(this.startDatetimeSearch, this.acceptableDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                foreach (SearchableDeparture departure in departures)
                {
                    if (DateTime.Compare(departure.GetStartTime(), dt) >= 0)
                    {
                        filtered.Add(departure);
                    }
                }
            }
            else
            {
                filtered = departures;
            }
            return filtered;
        }

        private List<SearchableDeparture> FilterByDestination(List<SearchableDeparture> departures)
        {
            List<SearchableDeparture> filtered = new List<SearchableDeparture>();
            if (this.endLocationSearch != "")
            {                
                foreach (SearchableDeparture departure in departures)
                {
                    if (departure.GetEndStationName().ToLower() == this.endLocationSearch.ToLower())
                    {
                        filtered.Add(departure);
                    }                                               
                }
            }
            else
            {
                filtered = departures;
            }
            return filtered;
        }

        private List<SearchableDeparture> FilterByStartingStation(List<SearchableDeparture> departures)
        {
            List<SearchableDeparture> filtered = new List<SearchableDeparture>();
            if (this.startLocationSearch != "")
            {
                foreach(SearchableDeparture departure in departures)
                {                    
                    if (departure.GetStartStationName().ToLower() == this.startLocationSearch.ToLower())
                    {
                        filtered.Add(departure);
                    }
                }
            }
            else
            {
                filtered = departures;
            }
            return filtered;
        }
        private int GetSeatNumber()
        {
            int sn;
            if (Int32.TryParse(txbx_seat.Text.ToString(), out sn)) {
                if (sn < 1 || sn > capacity)
                {
                    MessageBox.Show("Uneti broj sedišta se ne nalazi u odgovarajućem opsegu!");
                    return 0;
                }
                else
                {
                    if (TicketDAO.IsSeatTaken(chosenDeparture, sn) == null)
                    {
                        return sn; 
                    }
                    else
                    {
                        MessageBox.Show("Odabrano sedište je zauzeto!");
                        return 0;
                    }
                }
            }
            else
            {
                MessageBox.Show("Broj sedišta mora biti numeričke vrednosti!");
                return 0;
            }
        }

        private bool CheckDatetimeInput()
        {
            DateTime dt;
            
            if (this.startDatetimeSearch != "")
            {
                if (!DateTime.TryParseExact(this.startDatetimeSearch, this.acceptableDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {   
                    return false;
                }
            }

            if (this.endDatetimeSearch != "")
            {
                if (!DateTime.TryParseExact(this.endDatetimeSearch, this.acceptableDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class SearchableDeparture
    {
        public Departure Departure { get; set; }
        public OnWayStation StartLocation { get; set; }
        public OnWayStation EndLocation { get; set; }
        public DepartureEnum DepartureKind { get; set; }

        public SearchableDeparture()
        {

        }

        public SearchableDeparture(Departure departure, OnWayStation endStation, DepartureEnum kind)
        {
            this.Departure = departure;
            this.EndLocation = endStation;
            this.DepartureKind = kind;
        }

        public SearchableDeparture(Departure departure, OnWayStation startStation, OnWayStation endStation, DepartureEnum kind)
        {
            this.Departure = departure;
            this.StartLocation = startStation;
            this.EndLocation = endStation;
            this.DepartureKind = kind;
        }

        public String GetStartStationName()
        {
            if (this.DepartureKind == DepartureEnum.Departure)
            {                
                return TrainLinesDAO.getTrainLineByID(this.Departure.TrainLineID).StartStation.Name;
            }
            else
            {
                return this.StartLocation.Station.Name;
            }
        }

        public String GetEndStationName()
        {
            return this.EndLocation.Station.Name;
        }

        public DateTime GetStartTime()
        {
            if (this.DepartureKind == DepartureEnum.Departure)
            {
                return this.Departure.StartTime;
            }
            else
            {
                return CountStartTimeOnWay();
            }
        }

        public DateTime GetEndTime()
        {
            int ii = 1;
            DateTime endTime = this.Departure.StartTime;
            while (true)
            {
                OnWayStation ows = OnWayStationDAO.GetOnWayStationByOrderNumber(this.Departure.TrainLineID, ii);

                if (ows == null)
                {
                    break;
                }
                if (ows.StationOrder <= this.EndLocation.StationOrder)
                {
                    endTime = endTime.AddMinutes(ows.Time);
                }
                else
                {
                    break;
                }

                ii++;
            }
            return endTime;
        }
        private DateTime CountStartTimeOnWay()
        {
            int ii = 1;
            DateTime startTime = this.Departure.StartTime;
            while (true)
            {
                OnWayStation ows = OnWayStationDAO.GetOnWayStationByOrderNumber(this.Departure.TrainLineID, ii);

                if (ows == null)
                {
                    break;
                }

                if (ows.StationOrder <= this.StartLocation.StationOrder)
                {
                    startTime = startTime.AddMinutes(ows.Time);                    
                }
                else
                {
                    break;
                }
                ii++;
            }
            return startTime;
        }
    }      
}