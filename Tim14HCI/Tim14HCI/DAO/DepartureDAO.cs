using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.Model;

namespace Tim14HCI.DAO
{
    class DepartureDAO
    {        
        public static Departure GetDepartureByID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.departures.Include(d => d.TrainLine).ThenInclude(tr => tr.StartStation).Where(d => d.DepartureID == id).FirstOrDefault();
            }
        }

        public static List<Departure> GetAllDepartures()
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.departures.Include(d => d.TrainLine).ToList();
            }
        }
    }
}
