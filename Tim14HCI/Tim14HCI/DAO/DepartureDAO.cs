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
        public static List<Departure> getDapertures()
        {
            List<Departure> departures = new List<Departure>();

            departures.Add(new Departure(1, new DateTime(2022, 6, 10, 17, 0, 0), new DateTime(2022, 6, 10, 17, 40, 00), 1, TrainLinesDAO.getTrainLineByID(1)));
            departures.Add(new Departure(2, new DateTime(2022, 6, 10, 18, 0, 0), new DateTime(2022, 6, 10, 18, 40, 00), 1, TrainLinesDAO.getTrainLineByID(1)));
            departures.Add(new Departure(3, new DateTime(2022, 6, 10, 19, 0, 0), new DateTime(2022, 6, 10, 19, 40, 00), 1, TrainLinesDAO.getTrainLineByID(1)));
            departures.Add(new Departure(4, new DateTime(2022, 6, 10, 16, 0, 0), new DateTime(2022, 6, 10, 16, 40, 00), 2, TrainLinesDAO.getTrainLineByID(2)));
            departures.Add(new Departure(5, new DateTime(2022, 6, 10, 15, 0, 0), new DateTime(2022, 6, 10, 15, 40, 00), 2, TrainLinesDAO.getTrainLineByID(2)));
            departures.Add(new Departure(6, new DateTime(2022, 6, 10, 14, 0, 0), new DateTime(2022, 6, 10, 14, 40, 00), 2, TrainLinesDAO.getTrainLineByID(2)));
            departures.Add(new Departure(7, new DateTime(2022, 6, 10, 13, 0, 0), new DateTime(2022, 6, 10, 13, 40, 00), 2, TrainLinesDAO.getTrainLineByID(2)));
            departures.Add(new Departure(8, new DateTime(2022, 6, 10, 12, 0, 0), new DateTime(2022, 6, 10, 12, 40, 00), 2, TrainLinesDAO.getTrainLineByID(2)));
            departures.Add(new Departure(9, new DateTime(2022, 6, 10, 11, 0, 0), new DateTime(2022, 6, 10, 11, 40, 00), 2, TrainLinesDAO.getTrainLineByID(2)));
            departures.Add(new Departure(10, new DateTime(2022, 6, 10, 10, 0, 0), new DateTime(2022, 6, 10, 10, 40, 00), 2, TrainLinesDAO.getTrainLineByID(2)));

            return departures;
        }

        public static List<Departure> GetAllDepartures()
        {
            using (var context = new SerbiaRailwayContext())
            {

                return context.departures.ToList();
            }
        }

        public static Departure GetDepartureByID(int id)
        {
            foreach (Departure d in getDapertures())
            {
                if (d.DepartureID == id)
                {
                    return d;
                }
            }
            return null;
        }
    }
}
