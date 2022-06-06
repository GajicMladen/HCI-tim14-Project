using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim14HCI.Model
{
    public class Departure
    {

        public int DepartureID { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public DateTime StartTimeEveryday { get; set; }

        public int TrainLineID { get; set; }
        public TrainLine TrainLine { get; set; }

        public virtual List<Ticket> Tickets { get; set; }

        public Departure()
        {

        }

        public Departure(int id, DateTime startDate, DateTime endDate, TrainLine trainLine)
        {
            this.DepartureID = id;
            this.startDate = startDate;
            this.endDate = endDate;
            this.TrainLine = trainLine;
        }

    }
}
