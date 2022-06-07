using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.Model;

namespace Tim14HCI.DAO
{
    public class TicketDAO
    {
        public static List<Ticket> tickets = new List<Ticket>(); 

        public static void AddNewTicket(Ticket t)
        {
            tickets.Add(t);
        }

        public static List<Ticket> GetReservationTicketsByUser(int userId)
        {
            List<Ticket> reservationTickets = new List<Ticket>();

            foreach (Ticket t in tickets)
            {
                if (t.ForReservation && t.User.UserID == userId)
                {
                    reservationTickets.Add(t);
                }
            }

            return reservationTickets;
        }

        public static List<Ticket> GetBoughtTicketsByUser(int userId)
        {
            List<Ticket> boughtTickets = new List<Ticket>();

            foreach (Ticket t in tickets)
            {
                if (!t.ForReservation && t.User.UserID == userId)
                {
                    boughtTickets.Add(t);
                }
            }

            return boughtTickets;
        }
    }
}
