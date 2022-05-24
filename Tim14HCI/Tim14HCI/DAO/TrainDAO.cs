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
    }
}
