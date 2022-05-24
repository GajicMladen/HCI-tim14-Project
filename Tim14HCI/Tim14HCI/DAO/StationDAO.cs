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
    }
}
