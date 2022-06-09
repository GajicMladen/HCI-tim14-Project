using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim14HCI.Model
{
    public class Ticket
    {
        public int TicketID { get; set; }

        public bool ForReservation { get; set; }

        public int DepartureID { get; set; }
        public Departure Departure { get; set; }

        public int StartStationID { get; set; }
        public Station StartStation { get; set; }
        public int EndStationID { get; set; }
        public Station EndStation { get; set; }
        public double Price { get; set; }
        public int Seat { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

        public Ticket()
        {

        }
        
        public Ticket(bool forReservation, int departureId, int startStationId, int endStationId, double price, int userId, int seatNumber)
        {
            this.ForReservation = forReservation;
            this.DepartureID = departureId;
            this.StartStationID = startStationId;
            this.EndStationID = endStationId;
            this.Price = price;
            this.UserID = userId;
            this.Seat = seatNumber;
        }

        public Ticket(bool forReservation, Departure departure, Station startStation, Station endStation, double price, User user)
        {
            this.ForReservation = forReservation;
            this.DepartureID = departure.DepartureID;
            this.Departure = departure;
            this.StartStationID = startStation.StationID;
            this.StartStation = startStation;
            this.EndStationID = endStation.StationID;
            this.EndStation = endStation;
            this.Price = price;
            this.UserID = user.UserID;
            this.User = user;
        }

    }
}
