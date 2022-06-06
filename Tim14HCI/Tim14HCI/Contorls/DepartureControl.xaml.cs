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
    /// Interaction logic for DepartureControl.xaml
    /// </summary>
    /// 
    public class DepartureChosenEventArgs : RoutedEventArgs
    {
        private readonly int departureId;

        public int DepartureID
        {
            get { return departureId; }
        }

        public DepartureChosenEventArgs(RoutedEvent routedEvent, int departureId) : base(routedEvent)
        {
            this.departureId = departureId;
        }
    }

    public partial class DepartureControl : UserControl
    {
        Departure departure;

        public static readonly RoutedEvent DepartureChosenEvent =
        EventManager.RegisterRoutedEvent("DepartureChosen", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(DepartureControl));

        public event RoutedEventHandler DepartureChosen
        {
            add { AddHandler(DepartureChosenEvent, value); }
            remove { RemoveHandler(DepartureChosenEvent, value); }
        }

        public DepartureControl()
        {
            InitializeComponent();
        }

        public DepartureControl(Departure d)
        {
            InitializeComponent();

            departure = d;
            lbl_start_date_time.Content = departure.startDate.ToString("dd.MM.yyyy. HH:mm");
            lbl_end_date_time.Content = departure.endDate.ToString("dd.MM.yyyy. HH:mm");
            lbl_start_location.Content = departure.TrainLine.StartStation.Name;
            lbl_end_location.Content = "Novi Sad";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(departure.DepartureID + "*******************");
            // Raise the custom routed event, this fires the event from the UserControl
            DepartureChosenEventArgs args = new DepartureChosenEventArgs(DepartureControl.DepartureChosenEvent, departure.DepartureID);
            RaiseEvent(args);
        }
    }
}
