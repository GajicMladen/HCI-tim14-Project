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

        public static List<Train> getAllTrains() {

            using (var context = new SerbiaRailwayContext()) {
                return context.trains.ToList();
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
    }
}
