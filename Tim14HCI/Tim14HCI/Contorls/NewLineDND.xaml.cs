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
    /// Interaction logic for NewLineDND.xaml
    /// </summary>
    public partial class NewLineDND : UserControl
    {

        public Station StartStation = null;
        public Station EndStation = null;
        public List<Station> OnWayStations = new List<Station>();

        private List<Station> lastAdded = new List<Station>();

        public NewLineDND()
        {
            InitializeComponent();
            showAllStations();
        }

        private void StationForDragAndDrop_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void StackPanel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {

                e.Effects = DragDropEffects.Move;

            }
        }

        private void satck_AllStations_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");
                Station station = (Station)e.Data.GetData("Station");

                if (_panel != null && _element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {

                            if (lastAdded[lastAdded.Count - 1].StationID == station.StationID)
                            {

                                _parent.Children.Remove(_element);
                                _panel.Children.Add(_element);
                                removeStationFromLists(station);

                                lastAdded.Remove(station);
                                if (lastAdded.Count != 0)
                                    showOnlyLinkedStations(lastAdded[lastAdded.Count - 1]);
                                else
                                    showAllStations();

                                drawStationsOnMap();
                                e.Effects = DragDropEffects.Move;
                            }
                            else {
                                MessageBox.Show("Mozete samo vratiti poslednju dodatu stanicu! U ovom slucaju to je: " + lastAdded[lastAdded.Count - 1].Name
                                    +" \n *ili restartujte dodavanje nove linije klikom na dugme 'Restartuj'.");
                            }

                        }
                    }
                }
            }

        }

        private void satck_AllStations_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {

                e.Effects = DragDropEffects.Move;

            }

        }


        private bool allreadyAddedStation(Station x) {

            if (StartStation != null && StartStation.StationID == x.StationID)
                return true;
            foreach (Station station in OnWayStations) {
                if (station.StationID == x.StationID)
                    return true;
            }

            if (EndStation != null && EndStation.StationID == x.StationID)
                return true;

            return false;

        }

        private void removeStationFromLists(Station x) {

            if (StartStation != null && StartStation.StationID == x.StationID)
            {
                StartStation = null;
                return;
            }
            if (EndStation != null && EndStation.StationID == x.StationID) {
                EndStation = null;
                return;
            }
            foreach (Station station in OnWayStations) {
                if (station.StationID == x.StationID) {
                    OnWayStations.Remove(station);
                    return;
                }
            }

        }

        private void clearRoute() {

            StartStation = null;
            EndStation = null;
            OnWayStations.Clear();
            lastAdded.Clear();
        }

        private void drawStationsOnMap() {

            canvas_map.Children.Clear();
            if (StartStation != null) drawStationOnMap(StartStation,false);
            if (EndStation != null) drawStationOnMap(EndStation, false);
            if (OnWayStations.Count > 0) {
                foreach (Station onWayStation in OnWayStations) {
                    drawStationOnMap(onWayStation, true);
                }
            }
        }

        private void drawStationOnMap(Station x,bool onWay) {

            Rectangle rectangle = new Rectangle() { Width = 5, Height = 5 };
            TextBlock textBlock = new TextBlock() { Text = x.Name };

            if (onWay)
            {
                rectangle.Fill = Brushes.Blue;
                textBlock.Foreground = Brushes.Blue;
            }
            else
            {
                rectangle.Fill = Brushes.Black;
                textBlock.Foreground = Brushes.Black;
            }

            Canvas.SetLeft(rectangle, x.position_x);
            Canvas.SetLeft(textBlock, x.position_x + 10);
            Canvas.SetTop(rectangle, x.position_y);
            Canvas.SetTop(textBlock, x.position_y);

            canvas_map.Children.Add(rectangle);
            canvas_map.Children.Add(textBlock);
        }

        private void showAllStations() {

            succesStaionsDropPanel.Children.Clear();
            foreach (Station station in StationDAO.getAllStations())
            {
                StationForDragAndDrop stationForDragAndDrop = new StationForDragAndDrop(station);
                succesStaionsDropPanel.Children.Add(stationForDragAndDrop);
            }

        }

        private void showOnlyLinkedStations(Station station) {

            succesStaionsDropPanel.Children.Clear();

            List<Station> rez = new List<Station>();            

            foreach (Station s in TrainLinesDAO.getLinkedStations(station)) {

                if (!allreadyAddedStation(s))
                    rez.Add(s);
            }

            foreach (Station stat in rez)
            {
                StationForDragAndDrop stationForDragAndDrop = new StationForDragAndDrop(stat);
                succesStaionsDropPanel.Children.Add(stationForDragAndDrop);
            }


        }

        private void startStation_Drop(object sender, DragEventArgs e)
        {

            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");
                Station station = (Station)e.Data.GetData("Station");

                if (_panel != null && _element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {

                            if (_parent.Name == "onWayStationsDropPanel" || _parent.Name == "endStationDropPanel")
                            {
                                MessageBox.Show("dozvoljeno je dodavanje samo iz dostupnih stanica!");
                                return;
                            }
                            
                            if (_panel.Children.Count == 2)
                            {
                                StationForDragAndDrop toBeBack = (StationForDragAndDrop)_panel.Children[1];
                                _panel.Children.RemoveAt(1);
                                removeStationFromLists(toBeBack.station);
                            }

                            clearRoute();
                            showOnlyLinkedStations(station);
                            
                            onWayStationsDropPanel.Children.Clear();
                            if (endStationDropPanel.Children.Count == 2)
                            {
                                endStationDropPanel.Children.RemoveAt(1);
                            }

                            _panel.Children.Add(_element);
                            
                            
                            StartStation = station;
                            lastAdded.Add(station);

                            drawStationsOnMap();

                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
        }

        private void endStation_drop(object sender, DragEventArgs e)
        {

            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");
                Station station = (Station)e.Data.GetData("Station");

                if (_panel != null && _element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            if (_parent.Name == "onWayStationsDropPanel" || _parent.Name == "startStationDropPanel")
                            {
                                MessageBox.Show("dozvoljeno je dodavanje samo iz dostupnih stanica!");
                                return;
                            }
                            _parent.Children.Remove(_element);

                            if (_panel.Children.Count == 2)
                            {
                                StationForDragAndDrop toBeBack = (StationForDragAndDrop)_panel.Children[1];
                                _panel.Children.RemoveAt(1);
                                removeStationFromLists(toBeBack.station);

                            }
                            _panel.Children.Add(_element);
                            removeStationFromLists(station);
                            EndStation = station;
                            drawStationsOnMap();

                            succesStaionsDropPanel.Children.Clear();
                            lastAdded.Add(station);

                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
        }

        private void onWayStations_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");
                Station station = (Station)e.Data.GetData("Station");

                if (_panel != null && _element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {

                            if (_parent.Name == "startStationDropPanel" || _parent.Name == "endStationDropPanel")
                            {
                                MessageBox.Show("dozvoljeno je dodavanje samo iz dostupnih stanica!");
                                return;
                            }
                            _parent.Children.Remove(_element);
                            _panel.Children.Add(_element);
                            removeStationFromLists(station);
                            OnWayStations.Add(station);
                            drawStationsOnMap();

                            lastAdded.Add(station);
                            showOnlyLinkedStations(station);

                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }

        }
    }
}
