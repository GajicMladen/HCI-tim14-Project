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
using Tim14HCI.Model;

namespace Tim14HCI.Contorls
{
    /// <summary>
    /// Interaction logic for TicketControl.xaml
    /// </summary>
    public partial class TicketControl : UserControl
    {
        Ticket t;

        public TicketControl()
        {
            InitializeComponent();
        }

        public TicketControl(Ticket ticket)
        {
            InitializeComponent();
            this.t = ticket;
            lbl_start_date_time.Content = this.t.Departure.startDate.ToString("dd.mm.yyyy. HH:mm");
            lbl_end_date_time.Content = this.t.Departure.endDate.ToString("dd.mm.yyyy. HH:mm");
            lbl_start_location.Content = this.t.Departure.TrainLine.StartStation.Name;
            lbl_end_location.Content = "Novi Sad";
            lbl_price.Content = "1599";
        }
    }
}
