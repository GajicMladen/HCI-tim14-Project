using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.DAO;

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
        
        public string GetSearchString(bool forDeparture = false)
        {
            string ows = "";
            foreach (OnWayStation onWayStation in OnWayStationDAO.GetAllOnWayStationsByTrainLineID(TrainLineID))
            {
                ows += StationDAO.GetStationByID(StationDAO.GetOnWayStationByID(onWayStation.OnWayStationID).StationID).Name + " ";

            }
            if (forDeparture)
                return StationDAO.GetStationByID(StartStationID).Name + " " + StationDAO.GetStationByID(EndStationID).Name;
            else
                return TrainLineID.ToString() + " " + StationDAO.GetStationByID(StartStationID).Name + ows + " " + StationDAO.GetStationByID(EndStationID).Name + " " + TrainDAO.GetTrainByID(TrainID).Name + " " + getTotalPrice().ToString() + " " + getTotalTime().ToString();
        }

        public float getTotalPrice() {
            float ret = 0;

            foreach (OnWayStation onWayStation in OnWayStationDAO.GetAllOnWayStationsByTrainLineID(this.TrainLineID)) {
                ret += onWayStation.Price;
            }

            

            return ret;
        }
        public float getTotalTime()
        {
            float ret = 0;

            foreach (OnWayStation onWayStation in OnWayStationDAO.GetAllOnWayStationsByTrainLineID(this.TrainLineID))
            {
                ret += onWayStation.Time;
            }

            

            return ret;
        }


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
