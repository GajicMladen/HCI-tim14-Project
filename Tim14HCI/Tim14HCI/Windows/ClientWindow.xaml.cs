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

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        Window parent;
        User user;
        int chosenDeparture;

        String startLocationSearch;
        String endLocationSearch;
        String startDatetimeSearch;
        String endDatetimeSearch;

        String[] acceptableDateTimeFormats = { "dd.MM.yyyy.", "dd.MM.yyyy. HH:mm", "dd.MM.yyyy. HH:mm:ss" };

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
            //grid_test.Visibility = Visibility.Collapsed;
            AddHandler(DepartureControl.DepartureChosenEvent,
                new RoutedEventHandler(DepartureChosenEventHandler));

        }

        private void DepartureChosenEventHandler(object sender, RoutedEventArgs e)
        {
            DepartureChosenEventArgs args = (DepartureChosenEventArgs)e;
            chosenDeparture = args.DepartureID;
            Departure departure = DepartureDAO.GetDepartureByID(chosenDeparture);
            lbl_departure.Content = departure.startDate.ToString("dd.MM.yyyy. HH:mm") + "  " + departure.endDate.ToString("dd.MM.yyyy. HH:mm") + "  " + departure.TrainLine.StartStation.Name + "  Novi Sad";

        }



        private void fillStackDataWithDepartures()
        {
            stack_Data.Children.Clear();
            List<Departure> departures = DAO.DepartureDAO.getDapertures();

            foreach (Departure departure in departures)
            {
                DepartureControl departureControl = new DepartureControl(departure);
                stack_Data.Children.Add(departureControl);
            }
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

        private void ReservationClick(object sender, RoutedEventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.User = user;
            ticket.ForReservation = true;
            ticket.Departure = DepartureDAO.GetDepartureByID(chosenDeparture);
            TicketDAO.AddNewTicket(ticket);
            MessageBox.Show("Karta uspešno rezervisana!");
        }

        private void BuyClick(object sender, RoutedEventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.User = user;
            ticket.ForReservation = false;
            ticket.Departure = DepartureDAO.GetDepartureByID(chosenDeparture);
            TicketDAO.AddNewTicket(ticket);
            MessageBox.Show("Karta uspešno kupljena!");
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

        private void Search(object sender, RoutedEventArgs e)
        {
            List<Departure> departures = DAO.DepartureDAO.getDapertures();

            this.startLocationSearch = txbx_start_location.Text;
            this.endLocationSearch = txbx_end_location.Text;
            this.startDatetimeSearch = txbx_start_time.Text;
            this.endDatetimeSearch = txbx_end_time.Text;

            if (!CheckDatetimeInput())
            {
                MessageBox.Show("Pogrešan format datuma!\nPrimeri prihvatljivih formata datuma: 21.04.2022. ili 21.04.2022. 14:00");
                return;
            }

            List<Departure> startingStationFiltered = FilterByStartingStation(departures);
            List<Departure> destinationFiltered = FilterByDestination(startingStationFiltered);
            List<Departure> startDateFiltered = FilterByStartDatetime(destinationFiltered);
            List<Departure> endDateFiltered = FilterByEndDatetime(startDateFiltered);

            stack_Data.Children.Clear();
            foreach (Departure departure in endDateFiltered)
            {
                DepartureControl departureControl = new DepartureControl(departure);
                stack_Data.Children.Add(departureControl);
            }

        }

        private List<Departure> FilterByEndDatetime(List<Departure> departures)
        {
            List<Departure> filtered = new List<Departure>();
            if (this.endDatetimeSearch != "")
            {
                DateTime dt = DateTime.ParseExact(this.endDatetimeSearch, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                foreach (Departure departure in departures)
                {
                    if (DateTime.Compare(departure.endDate, dt) <= 0)
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

        private List<Departure> FilterByStartDatetime(List<Departure> departures)
        {
            List<Departure> filtered = new List<Departure>();
            if (this.startDatetimeSearch != "")
            {
                DateTime dt = DateTime.ParseExact(this.startDatetimeSearch, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                foreach (Departure departure in departures)
                {
                    if (DateTime.Compare(departure.startDate, dt) >= 0)
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

        private List<Departure> FilterByDestination(List<Departure> departures)
        {
            return departures;
        }

        private List<Departure> FilterByStartingStation(List<Departure> departures)
        {
            List<Departure> filtered = new List<Departure>();
            if (this.startLocationSearch != "")
            {
                foreach(Departure departure in departures)
                {
                    if (departure.TrainLine.StartStation.Name.ToLower() == this.startLocationSearch.ToLower())
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

        private bool CheckDatetimeInput()
        {
            DateTime dt;
            
            if (this.startDatetimeSearch != "")
            {
                if (!DateTime.TryParseExact(this.startDatetimeSearch, this.acceptableDateTimeFormats, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
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
}
