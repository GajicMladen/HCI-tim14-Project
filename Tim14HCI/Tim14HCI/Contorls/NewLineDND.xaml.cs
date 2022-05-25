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
        public NewLineDND()
        {
            InitializeComponent();
            List<Station> stations = StationDAO.getAllStations();
            foreach (Station station in stations) {
                StationForDragAndDrop stationForDragAndDrop = new StationForDragAndDrop(station);
                satck_AllStations.Children.Add(stationForDragAndDrop);
            }
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

        private void StackPanel_Drop(object sender, DragEventArgs e)
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
                            _parent.Children.Remove(_element);
                            _panel.Children.Add(_element);
                            OnWayStations.Add(station);
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
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
                            _parent.Children.Remove(_element);
                            _panel.Children.Add(_element);
                            e.Effects = DragDropEffects.Move;
                            removeStationFromLists(station);
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

        private void StackPanel_Drop_1(object sender, DragEventArgs e)
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
                            _parent.Children.Remove(_element);
                            if (_panel.Children.Count == 2)
                            {
                                StationForDragAndDrop toBeBack = (StationForDragAndDrop)_panel.Children[1];
                                _panel.Children.RemoveAt(1);
                                satck_AllStations.Children.Add(toBeBack);
                                removeStationFromLists(toBeBack.station);
                            }

                            _panel.Children.Add(_element);
                            StartStation = station;
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
        }

        private void StackPanel_Drop_2(object sender, DragEventArgs e)
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
                            _parent.Children.Remove(_element);

                            if (_panel.Children.Count == 2)
                            {
                                StationForDragAndDrop toBeBack = (StationForDragAndDrop)_panel.Children[1];
                                _panel.Children.RemoveAt(1);
                                satck_AllStations.Children.Add(toBeBack);
                                removeStationFromLists(toBeBack.station);
                            }
                            _panel.Children.Add(_element);
                            EndStation = station;
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
        }

        private void removeStationFromLists(Station x) { 
            
            if(StartStation == x)
            {
                StartStation = null;
                return;
            }
            if (EndStation == x) {
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

    }
}
