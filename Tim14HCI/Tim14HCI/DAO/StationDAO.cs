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
                    context.stations.Remove(stationToRemove);
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

        public static void ModifyTrain(Station station)
        {
            using (var context = new SerbiaRailwayContext())
            {
                var oldStation = context.stations.SingleOrDefault(x => x.StationID == station.StationID);
                if (oldStation != null)
                {
                    oldStation.Name = station.Name;
                    oldStation.position_x = station.position_x;
                    oldStation.position_y = station.position_y;
                    context.SaveChanges();
                }
            }
        }
    }
}
