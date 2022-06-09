using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.Model;

namespace Tim14HCI.DAO
{
    public static class TrainLinesDAO
    {

        public static List<TrainLine> getAllTrainLines() {

            using (var context = new SerbiaRailwayContext())
            {
                return context.trainLines.ToList();
            }
        }

        public static TrainLine getTrainLineByID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.trainLines.Where(t => t.TrainLineID == id).FirstOrDefault();
            }
        }

        private static int getNewTrainLineID() {
            int maxId = 1;
            foreach(TrainLine trainLine in getAllTrainLines())
            {
                if (maxId < trainLine.TrainLineID)
                    maxId = trainLine.TrainLineID;

            }
            return maxId + 1;
            

        }
        //public static void addNewTrainLine(List<Station> route,
        //    Train train,List<int> prices,List<int> times) {

        //    TrainLine newTrainLine = new TrainLine();
        //    List<OnWayStation> onWayStations = new List<OnWayStation>();
            
        //    newTrainLine.Train = train;
        //    newTrainLine.TrainID = train.TrainID;

        //    newTrainLine.StartStation = route[0];
        //    newTrainLine.StartStationID = route[0].StationID;

        //    newTrainLine.TrainLineID = getNewTrainLineID();

        //    //newTrainLine.OnWayStations = onWayStations;

        //    for (int i = 0; i < prices.Count; i++) {

                
        //        OnWayStation onWayStation = new OnWayStation();
        //        onWayStation.Price = prices[i];
        //        onWayStation.StationOrder = i;
        //        onWayStation.Time = times[i];

        //        onWayStation.Station = route[i+1];
        //        onWayStation.StationID = route[i+1].StationID;

        //        onWayStation.TrainLineID = newTrainLine.TrainLineID;

        //        if (i == prices.Count - 1)
        //        {
        //            newTrainLine.EndStation = onWayStation;
        //            newTrainLine.EndStationID = onWayStation.StationID;
        //        }
        //        else {

        //            //newTrainLine.OnWayStations.Add(onWayStation);
        //        }
        //        //onWayStations.Add(onWayStation);
                
        //    }

        //    newTrainLine.Departures = new List<Departure>();

        //    //saveTrainLineInDB(newTrainLine, onWayStations);

        //    TrainLines.Add(newTrainLine);
        //}

        
        public static void saveTrainLineInDB(TrainLine newTrainLine,List<OnWayStation> onWayStations) {

            using (var context = new SerbiaRailwayContext())
            {

                context.trainLines.Add(newTrainLine);

                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.trainLines OFF;");
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.trains ON;");
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.stations ON;");
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.trainLines ON;");

                foreach (OnWayStation onWayStation in onWayStations)
                {
                    context.onWayStations.Add(onWayStation);
                }


                context.SaveChanges();
            }
        }

        public static List<List<Station>> checkTrainLine(Station startStation, Station endStation, List<Station> onWayStations) {

            List<List<Station>> allRoutes = getAllRoutes(startStation,endStation);
            List<List<Station>> posibleRoute = new List<List<Station>>();

            if (onWayStations.Count > 0)
            {
                foreach (List<Station> route in allRoutes)
                {
                    if (route.Any(r => onWayStations.Any(o => o.StationID == r.StationID)))
                        posibleRoute.Add(route);
                }
            }
            else {
                posibleRoute = allRoutes;
            }

            return posibleRoute;
        
        }

        public static List<List<Station>> getAllRoutes(Station startStation, Station endStation) {

            Queue<List<Station>> data_structure = new Queue<List<Station>>();
            List<int> visited = new List<int>();
            List<Station> startList = new List<Station>();
            startList.Add(startStation);
            data_structure.Enqueue(startList);

            List<List<Station>> findRoutes = new List<List<Station>>();

            while (data_structure.Count > 0) {

                List<Station> stations = data_structure.Dequeue();
                Station station = stations.Last();

                if (station.StationID == endStation.StationID)
                {
                    findRoutes.Add(stations);
                    continue;
                }

                if (! visited.Contains(station.StationID)) {

                    visited.Add(station.StationID);
                    foreach (Station succ in getLinkedStations(station)) {
                        List<Station> stations1 = new List<Station>(stations);
                        stations1.Add(succ);
                        data_structure.Enqueue(stations1);
                    }
                }
            }

            return findRoutes;
            
        }

        public static List<Station> getLinkedStations(Station station) {

            using (var context = new SerbiaRailwayContext()) {

                List<LinkedStation> linkedStations = context.linkedStations
                    .Where(l => l.Station1ID == station.StationID || l.Station2ID == station.StationID)
                    .ToList();

                List<Station> retVal = new List<Station>();

                linkedStations.ForEach(ls => {
                    if (ls.Station1ID != station.StationID)
                    {
                        retVal.Add(context.stations.Where(s => s.StationID == ls.Station1ID).FirstOrDefault());
                    }
                    else {
                        retVal.Add(context.stations.Where(s => s.StationID == ls.Station2ID).FirstOrDefault());
                    }
                });

                return retVal;
            }
        
        }
    }
}
