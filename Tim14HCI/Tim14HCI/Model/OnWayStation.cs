using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim14HCI.Model
{
    public class OnWayStation
    {
        public int OnWayStationID { get; set; }
        public int StationOrder { get; set; }
        public float Price { get; set; }

        public float Time { get; set; }

        public int StationID { get; set; }
        public Station Station { get; set; }


        public int TrainLineID { get; set; }
        public TrainLine TrainLine { get; set; }

    }
}
