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
using Tim14HCI.Contorls;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for NewTrainLine.xaml
    /// </summary>
    public partial class NewTrainLine : Window
    {
        public NewTrainLine()
        {
            InitializeComponent();
        }
        
        Station StartStation;
        OnWayStation EndStation;
        List<OnWayStation> onWayStations;

        Station EndStation_I;
        List<Station> onWayStations_I;

        List<Station> selectedRoute;
            
        NewLineDND control_newLineDND;
        RoutePicker routePicker;
        TrainPicker trainPicker;
        SetPriceForNewTrainLine setPricesForNewTrainLine;
        PriceList priceList;

        Train selectedTrain;
        List<int> prices;
        List<int> times;
       

        Window parent;
        public NewTrainLine(Window x)
        {
            parent = x;
            InitializeComponent();
            control_newLineDND = new NewLineDND();
            trainPicker = new TrainPicker(this);
            
            prices = new List<int>();
            times = new List<int>();

            btn_Back.IsEnabled = false;
            grid_display.Children.Add(control_newLineDND);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(changeDisplay())
                progresBar.SelectedIndex++;
            
        }

        private bool changeDisplay() {

            if (progresBar.SelectedIndex == -1)
            {
                grid_display.Children.Add(control_newLineDND);
                btn_Back.IsEnabled = false;
            }
            if (progresBar.SelectedIndex == 0)
            {

                if (control_newLineDND.StartStation == null)
                {
                    MessageBox.Show("Molimo vas unesite polaznu stanicu");
                    return false;

                }
                else if (control_newLineDND.EndStation == null)
                {
                    MessageBox.Show("Molimo vas unesite krajnju stanicu");
                    return false;
                }
                StartStation = control_newLineDND.StartStation;
                EndStation_I = control_newLineDND.EndStation;
                onWayStations_I = control_newLineDND.OnWayStations;

                if (selectedRoute != null)
                    selectedRoute.Clear();
                else
                    selectedRoute = new List<Station>();

                selectedRoute.Add(StartStation);
                foreach (Station s in onWayStations_I) {
                    selectedRoute.Add(s);
                }
                selectedRoute.Add(EndStation_I);
                
                //routePicker = new RoutePicker(posibleRoutes, this);
                grid_display.Children.Clear();
                btn_Back.IsEnabled = true;
                //grid_display.Children.Add(routePicker);


                MapControl mapControl = new MapControl(selectedRoute);
                mapControl.RenderTransform = new ScaleTransform(0.5, 0.5);
                stack_Route.Children.Clear();
                stack_Route.Children.Add(mapControl);
                stack_Route.Height = 150;
                
                grid_display.Children.Add(trainPicker);
            }/*
            if (progresBar.SelectedIndex == 1)
            {

                if (routePicker.selectedRoute == null)
                {
                    MessageBox.Show("Molimo vas odaberite rutu!");
                    return false;
                }
                selectedRoute = routePicker.selectedRoute;
                grid_display.Children.Clear();
                grid_display.Children.Add(trainPicker);

            }*/
            if (progresBar.SelectedIndex == 1)
            {
                if (trainPicker.selectedTrain == null)
                {
                    MessageBox.Show("Molimo vas odaberite voz");
                    return false;
                }
                selectedTrain = trainPicker.selectedTrain;

                grid_display.Children.Clear();
                setPricesForNewTrainLine = new SetPriceForNewTrainLine(selectedRoute);
                grid_display.Children.Add(setPricesForNewTrainLine);

            }
            if (progresBar.SelectedIndex == 2)
            {
                if (!setPricesForNewTrainLine.allDataIsEnterd())
                {
                    MessageBox.Show("Molimo vas popunite sva polja!");
                    return false;
                }
                prices = setPricesForNewTrainLine.getAllPrices();
                times = setPricesForNewTrainLine.getAllTimes();

                btn_next_label.Content = "Sacuvaj";
                btn_next_img.Source = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "/Images/confirm.png"));
                grid_display.Children.Clear();

                grid_display.Children.Add(new PriceList(selectedRoute,prices,times));

            }
            else
            {

                btn_next_label.Content = "Sledeće";
                btn_next_img.Source = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "/Images/next.png"));
            }
            if (progresBar.SelectedIndex == 3)
            {

                TrainLinesDAO.addNewTrainLine(selectedRoute, selectedTrain, prices, times);
                Close();
            }

            return true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            progresBar.SelectedIndex -= 2;
            if (progresBar.SelectedIndex < -1) progresBar.SelectedIndex = -1;
            grid_display.Children.Clear();
            changeDisplay();
            progresBar.SelectedIndex ++;
        }
    }
}
