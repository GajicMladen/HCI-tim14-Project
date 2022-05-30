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
    /// Interaction logic for PriceListItem.xaml
    /// </summary>
    public partial class PriceListItem : UserControl
    {
        public PriceListItem(Station station,int stationOrder,int price, int time,bool lastStation)
        {
            InitializeComponent();
            StationForDragAndDrop stationForDragAndDrop = new StationForDragAndDrop(station);
           
            grid_data.Children.Add(stationForDragAndDrop);
            Grid.SetColumn(stationForDragAndDrop, 1);
            lbl_price.Content = price.ToString();
            lbl_time.Content = time.ToString();
            if (stationOrder == 0)
            {
                lbl_stationOrder.Content = "Pocetna stanica: ";
                lbl_price.Visibility = Visibility.Hidden;
                lbl_pricee.Visibility = Visibility.Hidden;
                lbl_priceee.Visibility = Visibility.Hidden;
                lbl_time.Visibility = Visibility.Hidden;
                lbl_timee.Visibility = Visibility.Hidden;
                lbl_timeee.Visibility = Visibility.Hidden;

            }
            else {
                lbl_stationOrder.Content = "Stanica " + stationOrder.ToString()+" :";
            }

            if (lastStation)
            {
                lbl_stationOrder.Content = "Krajnja stanica: ";
            }
        }
    }
}
