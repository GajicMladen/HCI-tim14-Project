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

                return context.departures.ToList();
            }
        }

        public static List<Departure> getAllDeparturesSearch(string query)
        {
            List<Departure> retVal = new List<Departure>();
            using (var context = new SerbiaRailwayContext())
            {
                foreach (Departure d in context.departures.ToList())
                {
                    if (d.GetSearchString().ToLower().Contains(query.ToLower())) retVal.Add(d);
                }
                return retVal;
            }
        }

        /*public static Departure GetDepartureByID(int id)
        {
            using (var context = new SerbiaRailwayContext())
            {
                return context.departures.Include(d => d.TrainLine).ToList();
            }
        }*/

        public static void AddDeparture(Departure d)
        {
            using (var context = new SerbiaRailwayContext())
            {
                context.departures.Add(d);
                context.SaveChanges();
            }
        }

        public static void ModifyDeparture(Departure d)
        {
            using (var context = new SerbiaRailwayContext())
            {
                var oldDep = context.departures.SingleOrDefault(x => x.DepartureID == d.DepartureID);
                if (oldDep != null)
                {
                    oldDep.StartTime = d.StartTime;
                    oldDep.TrainLineID = d.TrainLineID;
                    context.SaveChanges();
                }
            }
        }

        public static void RemoveDeparture(Departure departure)
        {
            using (var context = new SerbiaRailwayContext())
            {
                var depToRemove = context.departures.SingleOrDefault(t => t.DepartureID== departure.DepartureID);
                if (depToRemove != null)
                {
                    context.departures.Remove(depToRemove);
                    context.SaveChanges();
                }
            }
        }
    }
}
