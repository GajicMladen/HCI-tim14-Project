using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim14HCI.Model
{
    public class TrainLine
    {
        public int TrainLineID { get; set; }

        public int StartStationID { get; set; }
        public Station StartStation { get; set; }

        public int EndStationID { get; set; }
        public OnWayStation EndStation { get; set; }

        public int TrainID { get; set; }
        public Train Train { get; set; }

        public virtual List<OnWayStation> OnWayStations { get; set; }

        public virtual List<Departure> Departures { get; set; }
        
        /*
        public float getTotalPrice() {
            float ret = 0;

            foreach (OnWayStation onWayStation in OnWayStations) {
                ret += onWayStation.Price;
            }

            ret += EndStation.Price;

            return ret;
        }
        public float getTotalTime()
        {
            float ret = 0;

            foreach (OnWayStation onWayStation in OnWayStations)
            {
                ret += onWayStation.Time;
            }

            ret += EndStation.Time;

            return ret;
        }*/


        public TrainLine()
        {

        }

        public TrainLine(int startStationId, int endStationId)
        {
            this.StartStationID = startStationId;
            this.StartStation = DAO.StationDAO.GetStationByID(startStationId);
            this.EndStationID = endStationId;
            
        }

    }
}
