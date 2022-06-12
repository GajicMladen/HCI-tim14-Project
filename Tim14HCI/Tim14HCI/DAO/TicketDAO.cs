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

        public static Dictionary<string, ReportItem> GetReportByMonths()
        {
            var report = new Dictionary<string, ReportItem>();
            report.Add("Jul", new ReportItem());
            report.Add("Avgust", new ReportItem());
            report.Add("Septembar", new ReportItem());
            report.Add("Oktobar", new ReportItem());
            report.Add("Novembar", new ReportItem());
            report.Add("Decembar", new ReportItem());
            report.Add("Januar", new ReportItem());
            report.Add("Februar", new ReportItem());
            report.Add("Mart", new ReportItem());
            report.Add("April", new ReportItem());
            report.Add("Maj", new ReportItem());
            report.Add("Jun", new ReportItem());


            using (var context = new SerbiaRailwayContext())
            {
                foreach (Ticket t in context.tickets.ToList())
                {
                    Departure d = DepartureDAO.GetDepartureByID(t.DepartureID);

                    if (d.StartTime.CompareTo(new DateTime(2021, 8, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Jul", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Jul"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2021, 9, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Avgust", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Avgust"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2021, 10, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Septembar", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Septembar"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2021, 11, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Oktobar", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Oktobar"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2021, 12, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Novembar", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Novembar"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2022, 1, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Decembar", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Decembar"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2022, 2, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Januar", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Januar"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2022, 3, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Februar", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Februar"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2022, 4, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Mart", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Mart"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2022, 5, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("April", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["April"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2022, 6, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Maj", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Maj"] = newRi;
                    }

                    else if (d.StartTime.CompareTo(new DateTime(2022, 7, 1)) <= 0)
                    {
                        ReportItem ri;
                        report.TryGetValue("Jun", out ri);
                        ReportItem newRi = new ReportItem();
                        newRi.TotalTicketsSold = ri.TotalTicketsSold + 1;
                        newRi.TotalIncome = ri.TotalIncome + t.Price;
                        report["Jun"] = newRi;
                    }

                }

                return report;
            }

        }

        public static int GetNumOfTicketsForDeparture(int departureId)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.tickets.Where(t => t.DepartureID == departureId).Count();
            }
        }

        public static double GetMoneyForDeparture(int departureId)
        {
            using (var context = new SerbiaRailwayContext())
            {
                double ret = 0;
                foreach (Ticket t in context.tickets.ToList())
                {
                    if (t.DepartureID == departureId)
                    {
                        ret += t.Price;
                    }
                }
                return ret;
            }
        }
    }
}
