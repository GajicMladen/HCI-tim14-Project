using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tim14HCI.Model;

namespace Tim14HCI.Contorls
{
    public class ReportDictionary : Dictionary<string, ReportItem>
    {
        public ReportDictionary(Dictionary<string, ReportItem> d)
        {
            foreach (KeyValuePair<string, ReportItem> entry in d)
            {
                Add(entry.Key, entry.Value);
            }
        }
    }

    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
        public ReportDictionary SoldTicketsInfo { get; private set; }

        public Report()
        {
            InitializeComponent();
        }

        public Report(Dictionary<string, ReportItem> soldTicketsInfo)
        {
            SoldTicketsInfo = new ReportDictionary(soldTicketsInfo);
            InitializeComponent();
            ((ColumnSeries)ticketReport.Series[0]).ItemsSource = SoldTicketsInfo;
        }
    }
}
