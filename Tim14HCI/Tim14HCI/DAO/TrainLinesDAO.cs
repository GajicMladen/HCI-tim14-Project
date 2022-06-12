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
        public static Station GetStartStationByTrainLineID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                TrainLine tr = context.trainLines.Where(t => t.TrainLineID == id).FirstOrDefault();
                return context.stations.Where(s => s.StationID == tr.StartStationID).FirstOrDefault();
            }
        }

        public static List<TrainLine> GetAllTrainLines()
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.trainLines.Include(tr => tr.StartStation).Include(tr => tr.EndStation).ToList();
            }
        }

        public static List<TrainLine> getAllTrainLines() {
            using (var context = new SerbiaRailwayContext()) {
                List<TrainLine> trainLines = context.trainLines.Include(tl => tl.OnWayStations).Include(tl => tl.EndStation).ThenInclude(es => es.Station).Include(tl => tl.StartStation).ToList();

                for (int i = 0; i < trainLines.Count; i++) {
                    trainLines[i].EndStation = context.onWayStations.Include(es => es.Station).Where(ow => ow.TrainLineID == trainLines[i].TrainLineID && ow.isEndStation).FirstOrDefault();
                    trainLines[i].OnWayStations = context.onWayStations.Include(es => es.Station).Where(ow => ow.TrainLineID == trainLines[i].TrainLineID && !ow.isEndStation).ToList();


                }
                return trainLines;
            } 
        }

        public static TrainLine getTrainLineByID(int id)
        {
            using (var context = new SerbiaRailwayContext()) {
                TrainLine trainLine = context.trainLines
                    .Include(tl => tl.OnWayStations)
                    .Include(tl => tl.EndStation)
                    .ThenInclude(es => es.Station)
                    .Include(tl => tl.StartStation)
                    .Where(tl => tl.TrainLineID == id).FirstOrDefault();

                trainLine.EndStation = context.onWayStations.Include(es => es.Station).Where(ow => ow.TrainLineID == trainLine.TrainLineID && ow.isEndStation).FirstOrDefault();
                trainLine.OnWayStations = context.onWayStations.Include(es => es.Station).Where(ow => ow.TrainLineID == trainLine.TrainLineID && ! ow.isEndStation).ToList();

                return trainLine;
            }
        }
        public static void addNewTrainLine(List<Station> route,
            Train train,List<int> prices,List<int> times) {


            TrainLine newTrainLine = new TrainLine();
            List<OnWayStation> onWayStations = new List<OnWayStation>();
            
            newTrainLine.TrainID = train.TrainID;
            
            newTrainLine.StartStationID = route[0].StationID;

            newTrainLine.EndStationID = route[route.Count - 1].StationID;

            using (var context = new SerbiaRailwayContext()) {

                context.trainLines.Add(newTrainLine);
                context.SaveChanges();



                for (int i = 0; i < prices.Count; i++)
                {


                    OnWayStation onWayStation = new OnWayStation();
                    onWayStation.Price = prices[i];
                    onWayStation.StationOrder = i;
                    onWayStation.Time = times[i];

                    onWayStation.StationID = route[i + 1].StationID;


                    onWayStation.TrainLineID = newTrainLine.TrainLineID;

                    if (i == prices.Count - 1)
                    {
                        onWayStation.isEndStation = true;
                    }

                    context.onWayStations.Add(onWayStation);
                }

                context.SaveChanges();
            }
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

        public static float GetTrainLineDuration(int trainLineId)
        {
            float retVal = 0;
            using (var context = new SerbiaRailwayContext())
            {
                TrainLine tl = context.trainLines.Where(l => l.TrainLineID == trainLineId).Include(t => t.OnWayStations).FirstOrDefault();
                foreach(OnWayStation ows in tl.OnWayStations)
                {
                    retVal += ows.Time;
                }
            }
            return retVal;
        }
    }
}
