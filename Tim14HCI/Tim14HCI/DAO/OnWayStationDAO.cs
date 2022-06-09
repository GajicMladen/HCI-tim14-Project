using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.Model;

namespace Tim14HCI.DAO
{
    class OnWayStationDAO
    {
        public static OnWayStation GetOnWayStationByID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.onWayStations.Include(ows => ows.Station).Where(ows => ows.OnWayStationID == id).FirstOrDefault();
            }
        }

        public static List<OnWayStation> GetAllOnWayStationsByTrainLineID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.onWayStations.Include(ows => ows.Station).Where(ows => ows.TrainLineID == id).ToList();
            }
        }

        public static OnWayStation GetEndStationByTrainLineID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.onWayStations.Include(ows => ows.Station).Where(ows => ows.TrainLineID == id && ows.isEndStation == true).FirstOrDefault();  
            }                    
        }

        public static OnWayStation GetOnWayStationByOrderNumber(int id, int order)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.onWayStations.Include(ows => ows.Station).Where(ows => ows.TrainLineID == id && ows.StationOrder == order).FirstOrDefault();
            }
        }
    }
}
