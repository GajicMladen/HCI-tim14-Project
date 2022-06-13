using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim14HCI.DAO;

namespace Tim14HCI.Model
{
    public class Station
    {
        public int StationID { get; set; }

        public string Name { get; set; }

        public int position_x { get; set; }

        public int position_y { get; set; }

        public virtual List<Ticket> TicketsStartStation { get; set; }

        public virtual List<Ticket> TicketsEndStation { get; set; }
      
        //for Linked Stations
        public virtual ICollection<LinkedStation> stations1 { get; set; }
        public virtual ICollection<LinkedStation> stations2 { get; set; }

        public Station() { 
        }
        public Station(Station x) {
            this.Name = x.Name;
            this.StationID = x.StationID;
            this.position_x = x.position_x;
            this.position_y = x.position_y;
        }

        public string GetSearchString()
        {
            return Name;
        }
    }
}
