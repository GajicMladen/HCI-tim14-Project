using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.Model;

namespace Tim14HCI.DAO
{
    public static class StationDAO
    {

        public static List<Station> getAllStations() {

            using (var context = new SerbiaRailwayContext()) {

                return context.stations.ToList();
            }
        
        }


        public static Station GetStationByID(int id)
        {

            using (var context = new SerbiaRailwayContext())
            {
                return context.stations.Where(s => s.StationID == id).FirstOrDefault();
            }

        }

        public static Station GetStationByName(string name)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.stations.Where(s => s.Name == name).FirstOrDefault();
            }
        }

        public static bool StationNameExists(string name)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.stations.SingleOrDefault(train => train.Name.ToLower() == name.ToLower()) != null;
            }
        }

        public static void RemoveStation(Station station)
        {
            using (var context = new SerbiaRailwayContext())
            {
                var stationToRemove = context.stations.SingleOrDefault(t => t.Name == station.Name);
                if (stationToRemove != null)
                {
                    context.linkedStations.RemoveRange(context.linkedStations.Where(x => x.Station1ID == station.StationID || x.Station2ID == station.StationID));
                    context.SaveChanges();
                    context.stations.Remove(stationToRemove);
                    context.SaveChanges();
                }
            }
        }

        public static void ModifyStation(Station station, List<Station> links)
        {
            using (var context = new SerbiaRailwayContext())
            {
                var oldStation = context.stations.SingleOrDefault(x => x.StationID== station.StationID);
                if (oldStation != null)
                {
                    oldStation.Name = station.Name;
                    oldStation.position_x = station.position_x;
                    oldStation.position_y = station.position_y;
                    context.linkedStations.RemoveRange(context.linkedStations.Where(x => x.Station1ID == oldStation.StationID || x.Station2ID == oldStation.StationID));
                    context.SaveChanges();
                    foreach (Station s in links)
                    {
                        LinkedStationDAO.addLinkedStations(oldStation, s);
                    }
                    context.SaveChanges();
                }
            }
        }

        public static void AddStation(Station station)
        {
            using (var context = new SerbiaRailwayContext())
            {
                context.Add(station);
                context.SaveChanges();
            }
        }

        public static List<Station> getLinkedStations(Station station)
        {
            using (var context = new SerbiaRailwayContext())
            {
                List<LinkedStation> links = context.linkedStations.Where(x => x.Station1ID == station.StationID || x.Station2ID == station.StationID).ToList();
                List<Station> stations = new List<Station>();
                foreach(var link in links)
                {
                    if (link.Station1ID == station.StationID)
                        stations.Add(GetStationByID(link.Station2ID));
                    else
                        stations.Add(GetStationByID(link.Station1ID));
                }
                return stations;
            }
        }
    }
}
