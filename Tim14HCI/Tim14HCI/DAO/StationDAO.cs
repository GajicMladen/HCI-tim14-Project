using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tim14HCI.Model;

namespace Tim14HCI.DAO
{
    public static class StationDAO
    {
        public static List<int> deleted = new List<int>();

        public static List<Station> getAllStations() {

            using (var context = new SerbiaRailwayContext()) {

                return context.stations.Where( s => ! deleted.Contains(s.StationID)).ToList();
            }
        
        }

        public static List<Station> getAllStationsSearch(string query)
        {
            List<Station> retVal = new List<Station>();
            using (var context = new SerbiaRailwayContext())
            {
                foreach (Station s in context.stations.ToList())
                {
                    if (s.GetSearchString().ToLower().Contains(query.ToLower()) && !deleted.Contains(s.StationID)) retVal.Add(s);
                }
                return retVal;
            }
        }

        public static Station GetStationByID(int id)
        {

            using (var context = new SerbiaRailwayContext())
            {
                return context.stations.Where(s => s.StationID == id).FirstOrDefault();
            }

        }

        public static OnWayStation GetOnWayStationByID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.onWayStations.Where(s => s.OnWayStationID == id).FirstOrDefault();
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
        {/*
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
            }*/

            deleted.Add(station.StationID);
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

        public static List<OnWayStation> GetOnWayStations(int trainLineId)
        {
            using (var context = new SerbiaRailwayContext())
            {
                List <OnWayStation> owStations = context.onWayStations.Where(t => t.TrainLineID == trainLineId).ToList();
                return owStations;
            }
        }

        public static float GetTrainLineDuration(int trainLineId)
        {
            float time = 0;
            List<OnWayStation> owStations = GetOnWayStations(trainLineId);
            foreach(OnWayStation station in owStations)
            {
                time += station.Time;
            }
            return time;
        }

        public static List<Station> GetConnectedStations(string s1, string s2) {

            List<Station> ret = new List<Station>();
            try
            {
                Station startStation = GetStationByName(s1);
                Station endStation = GetStationByName(s2);

                List<Station> n1 = getLinkedStations(startStation);
                List<Station> n2 = getLinkedStations(endStation);

                foreach (Station ss1 in n1)
                {
                    if (!ret.Contains(ss1))
                    {
                        foreach (Station ss2 in n2)
                        {
                            if (ss1.Name.Equals(ss2.Name))
                            {
                                ret.Add(ss1);
                                break;
                            }
                        }
                    }
                }
            }
            catch { 
            
            }
            return ret;

        }
    }
}
