using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.Model;

namespace Tim14HCI.DAO
{
    public static class TrainDAO
    {

        public static List<int> deleted = new List<int>();

        public static List<Train> getAllTrains() {

            using (var context = new SerbiaRailwayContext()) {
                return context.trains.Where(t => ! deleted.Contains(t.TrainID)).ToList();
            }
        }

        public static List<Train> getAllTrainsSearch(string query)
        {
            List<Train> retVal = new List<Train>();
            using (var context = new SerbiaRailwayContext())
            {
                foreach(Train t in context.trains.ToList())
                {
                    if (t.GetSearchString().ToLower().Contains(query.ToLower()) && ! deleted.Contains(t.TrainID))
                        retVal.Add(t);
                }
                return retVal;
            }
        }

        public static Train GetTrainByID(int id) {

            using (var context = new SerbiaRailwayContext()) {


                return context.trains.Where(t => t.TrainID == id).FirstOrDefault();
            }
        
        }

        public static bool TrainNameExists(string name)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.trains.SingleOrDefault(train => train.Name.ToLower() == name.ToLower()) != null;
            }
        }

        public static void RemoveTrain(Train train)
        {
            deleted.Add(train.TrainID);
        }

        public static void AddTrain(Train train)
        {
            using (var context = new SerbiaRailwayContext())
            {
                context.Add(train);
                context.SaveChanges();
            }
        }

        public static void ModifyTrain(Train train)
        {
            using (var context = new SerbiaRailwayContext())
            {
                var oldTrain = context.trains.SingleOrDefault(x => x.TrainID == train.TrainID);
                if (oldTrain != null)
                {
                    oldTrain.Name = train.Name;
                    oldTrain.MaxSpeed = train.MaxSpeed;
                    oldTrain.Capacity = train.Capacity;
                    context.SaveChanges();
                }
            }
        }
    }
}
