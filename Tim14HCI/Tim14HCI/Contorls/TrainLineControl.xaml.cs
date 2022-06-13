using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Tim14HCI.Windows;

namespace Tim14HCI.Contorls
{
    /// <summary>
    /// Interaction logic for TrainLineControl.xaml
    /// </summary>
    public partial class TrainLineControl : UserControl
    {

        int trainLineId;
        AdminWindow parent;

        public TrainLineControl(TrainLine trainLine,AdminWindow window)
        {
            InitializeComponent();

            parent = window;
            trainLineId = trainLine.TrainLineID;

            lbl_StartStation.Content = trainLine.StartStation.Name;
            lbl_EndStation.Content = trainLine.EndStation.Station.Name;

            lbl_OnWayStations.Content = "";
            
            if (trainLine.OnWayStations.Count > 0)
            {

                foreach (OnWayStation onWayStation in trainLine.OnWayStations)
                {
                    lbl_OnWayStations.Content += onWayStation.Station.Name + " , ";

                }
            }
            else
            {
                lbl_OnWayStations.Content = " / ";

            }
            lbl_price.Content = trainLine.getTotalPrice().ToString();
            lbl_time.Content = trainLine.getTotalTime().ToString();


            List<Station> stations = new List<Station>();
            stations.Add(trainLine.StartStation);
            foreach (OnWayStation onw in trainLine.OnWayStations) {
                stations.Add(onw.Station);
            }
            stations.Add(trainLine.EndStation.Station);

            MapControl mapControl = new MapControl(stations);
            mapControl.Padding = new Thickness(10);
            //mapControl.RenderTransform = new ScaleTransform(0.7, 0.7);
            //mapControl.Width = 100;
            //mapControl.Height = 170;

            mainGrid.Children.Add(mapControl);

            Grid.SetColumn(mapControl, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var culture = new System.Globalization.CultureInfo("es-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da obrisete?", "Potvrda brisanja",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                TrainLinesDAO.deleteTrainLine(trainLineId);
                parent.showTrainLines();
            }
        }
    }
}
