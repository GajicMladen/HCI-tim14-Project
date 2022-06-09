using Microsoft.EntityFrameworkCore;
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
        public static List<Ticket> GetReservationTicketsByUserID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.tickets.Include(t => t.StartStation).Include(t => t.EndStation).Include(t => t.Departure).ThenInclude(d => d.TrainLine).Where(t => t.ForReservation == true && t.UserID == id).ToList();
            }
        }

        public static List<Ticket> GetBoughtTicketsByUserID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.tickets.Include(t => t.StartStation).Include(t => t.EndStation).Include(t => t.Departure).ThenInclude(d => d.TrainLine).Where(t => t.ForReservation == false && t.UserID == id).ToList();
            }
        }

        public static void AddTicket(Ticket ticket)
        {
            using (var context = new SerbiaRailwayContext())
            {
                context.tickets.Add(ticket);
                context.SaveChanges();
            }
        }

        public static List<Ticket> GetAllTicketsByDepartureID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.tickets.Where(t => t.DepartureID == id).ToList();
            }
        }

        public static Ticket IsSeatTaken(int id, int seat)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.tickets.Where(t => t.DepartureID == id && t.Seat == seat).FirstOrDefault();
            }
        }

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
