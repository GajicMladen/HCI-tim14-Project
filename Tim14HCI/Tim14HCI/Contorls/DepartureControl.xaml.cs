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
    public partial class DepartureControl : UserControl
    {
        Departure departure;

        public DepartureControl()
        {
            InitializeComponent();
        }

        public DepartureControl(Departure x)
        {
            departure = x;
            InitializeComponent();

            lbl_Line.Content = departure.TrainLine.StartStation.Name + " - " + departure.TrainLine.EndStation.Station.Name;
        }
    }
}
