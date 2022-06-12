using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Contorls
{
    /// <summary>
    /// Interaction logic for ReportPerLine.xaml
    /// </summary>
    public partial class ReportPerLine : UserControl
    {
        public ReportPerLine()
        {
            InitializeComponent();
            foreach(Departure d in DepartureDAO.GetAllDepartures())
            {
                DepartureReportItem dri = new DepartureReportItem(new DepartureTicketInfo(d));
                stack_Data.Children.Add(dri);
            }
        }
    }
}
