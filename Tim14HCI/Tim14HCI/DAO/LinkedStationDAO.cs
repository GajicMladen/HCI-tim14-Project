using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.Model;

namespace Tim14HCI.DAO
{
    public static class LinkedStationDAO
    {
        public static void addLinkedStations(Station s1, Station s2)
        {
            using (var context = new SerbiaRailwayContext())
            {
                LinkedStation linkedStation = new LinkedStation() {Station1ID = s1.StationID, Station2ID = s2.StationID };
                context.linkedStations.Add(linkedStation);
                context.SaveChanges();
            }
        }

        public static void deleteStationLinks(Station station)
        {
            using (var context = new SerbiaRailwayContext())
            {
                context.linkedStations.RemoveRange(context.linkedStations.Where(x => x.Station1ID == station.StationID || x.Station2ID == station.StationID));
                context.SaveChanges();
            }
        }

    }
}
