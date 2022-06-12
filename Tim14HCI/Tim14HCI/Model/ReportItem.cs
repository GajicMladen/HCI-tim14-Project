using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim14HCI.Model
{
    public class ReportItem
    {
        public double TotalIncome { get; set; }
        public int TotalTicketsSold { get; set; }
        public string MostPopularLine { get; set; }

        public ReportItem()
        {
            TotalIncome = 0;
            TotalTicketsSold = 0;
        }
    }
}
