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

        public DateTime StartTime { get; set; }

        public int TrainLineID { get; set; }
        public TrainLine TrainLine { get; set; }

        public virtual List<Ticket> Tickets { get; set; }


        public Departure()
        {

        }

        public Departure(int id, DateTime startDate, DateTime endDate, int trainLineID,  TrainLine trainLine)
        {
            this.DepartureID = id;
            this.StartTime = startDate;
            //this.endDate = endDate;
            this.TrainLineID = trainLineID;
            this.TrainLine = trainLine;
        }


        /*
        public DateTime getEndTime() {

            DateTime ret = StartTime;
            
            foreach(OnWayStation onWayStation in TrainLine.OnWayStations)
            {
                ret.AddMinutes(onWayStation.Time);
            }

            ret.AddMinutes(TrainLine.EndStation.Time);

            return ret;
        }
        */

    }
}
