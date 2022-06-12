using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.DAO;

namespace Tim14HCI.Model
{
    public class DepartureTicketInfo
    {
        public Departure Departure { get; set; }
        public int NumOfTickets { get; set; }
        public double NumOfMoney { get; set; }

        public DepartureTicketInfo() { }

        public DepartureTicketInfo(Departure x)
        {
            Departure = x;
            NumOfMoney = TicketDAO.GetMoneyForDeparture(x.DepartureID);
            NumOfTickets = TicketDAO.GetNumOfTicketsForDeparture(x.DepartureID);
        }
    }
}
