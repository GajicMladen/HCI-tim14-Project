using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for NewStation.xaml
    /// </summary>
    public partial class NewStation : Window
    {
        private Window parent;
        private Station station;
        private string mode;
        private Point p;

        private List<Station> stationList;

        private List<string> selectedStationNames;
        private String oldStationName = null;
        public NewStation()
        {
            InitializeComponent();
        }

        public NewStation(Window x)
        {
            parent = x;
            mode = "new";
            InitializeComponent();
            station = new Station();

            stationList = StationDAO.getAllStations();
            foreach (var station in stationList)
            {
                var listItem = new ListBoxItem();
                listItem.Content = station.Name;
                stationsListBox.Items.Add(listItem);
            }
            addStationButton.IsEnabled = false;
            removeStationsButton.IsEnabled = false;

            selectedStationNames = new List<string>();
            p = new Point(1000, 1000);
            //btn_Back.IsEnabled = false;

            //trainDataForm = new TrainDataForm();
            //trainLinePicker = new TrainLinePicker();
            //grid_display.Children.Add(trainDataForm);
        }

        public NewStation(Window x, Station oldStation)
        {
            parent = x;
            mode = "modify";
            InitializeComponent();
            station = oldStation;

            stationList = StationDAO.getAllStations();
            List<Station> linkedStations = StationDAO.getLinkedStations(station);
            stationList.RemoveAll(s => linkedStations.Any(st => st.Name == s.Name));
            stationList.RemoveAll(s => s.Name == station.Name);
            foreach(var station in stationList)
            {
                var listItem = new ListBoxItem();
                listItem.Content = station.Name;
                stationsListBox.Items.Add(listItem);
            }

            stationNameTextbox.Text = station.Name;

            addStationButton.IsEnabled = false;

            if (mode == "modify" && linkedStations.Count() != 0)
                removeStationsButton.IsEnabled = true;
            else
                removeStationsButton.IsEnabled = false;

            selectedStationNames = new List<string>();
            foreach(Station s in linkedStations)
            {
                selectedStationNames.Add(s.Name);
                drawSquare(s.position_x, s.position_y, s.Name, Brushes.BlueViolet);
                drawLine(s.position_x, s.position_y, station.position_x, station.position_y, Brushes.BlueViolet);
            }
            drawSquare(station.position_x, station.position_y, station.Name, Brushes.Blue);
            p = new Point(station.position_x, station.position_y);
            addButton.Content = "Izmeni";
            oldStationName = station.Name;

        }

        private void canvas_map_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UIElement toDelete = new UIElement();
            UIElement toDeleteBox = new UIElement();
            foreach(UIElement item in canvas_map.Children)
            {
                if (item.GetType() == typeof(Rectangle))
                {
                    Rectangle x = item as Rectangle;
                    if (x.Fill == Brushes.Blue)
                        toDelete = item;
                }

                if (item.GetType() == typeof(TextBlock))
                {
                    TextBlock x = item as TextBlock;
                    if (x.Foreground == Brushes.Blue)
                        toDeleteBox = item;
                }
            }
            canvas_map.Children.Remove(toDelete);
            canvas_map.Children.Remove(toDeleteBox);

            p = Mouse.GetPosition(canvas_map);
            drawSquare(p.X, p.Y, stationNameTextbox.Text, Brushes.Blue);

            for (int i = canvas_map.Children.Count - 1; i >= 0; i--)
            {
                UIElement Child = canvas_map.Children[i];
                if (Child is Line)
                    canvas_map.Children.Remove(Child);
            }

            foreach (string s in selectedStationNames)
            {
                Station station = StationDAO.GetStationByName(s);
                drawLine(p.X, p.Y, station.position_x, station.position_y, Brushes.BlueViolet);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void drawSquare(double x, double y, string text, SolidColorBrush color)
        {
            Rectangle rectangle = new Rectangle() { Width = 5, Height = 5 };
            TextBlock textBlock = new TextBlock() { Text = text };

            rectangle.Fill = color;
            textBlock.Foreground = color;

            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);
            Canvas.SetLeft(textBlock, x + 10);
            Canvas.SetTop(textBlock, y);

            canvas_map.Children.Add(rectangle);
            canvas_map.Children.Add(textBlock);
        }

        private void drawLine(double x1, double y1, double x2, double y2, SolidColorBrush color)
        {
            Line line = new Line { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2 };
            line.Stroke = color;
            line.Visibility = Visibility.Visible;
            line.StrokeThickness = 0.8;

            canvas_map.Children.Add(line);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (sender as System.Windows.Controls.ListBox).SelectedItem as ListBoxItem;
            if (item != null)
            {
                addStationButton.IsEnabled = true;
            }
        }

        private void addStationButton_Click(object sender, RoutedEventArgs e)
        {
            string stationName = (stationsListBox.SelectedItem as ListBoxItem).Content.ToString();
            selectedStationNames.Add(stationName);
            removeListBoxItem(stationName);
            addStationButton.IsEnabled = false;
            removeStationsButton.IsEnabled = true;

            Station station = StationDAO.GetStationByName(stationName);

            drawSquare(station.position_x, station.position_y, station.Name, Brushes.BlueViolet);
            if (p.X != 1000)
            {
                drawLine(p.X, p.Y, station.position_x, station.position_y, Brushes.BlueViolet);
            }
        }   

        private void removeListBoxItem(string itemContent)
        {
            ListBoxItem toDelete = new ListBoxItem();
            foreach (var item in stationsListBox.Items)
            {
                if (itemContent == (item as ListBoxItem).Content.ToString())
                {
                    toDelete = item as ListBoxItem;
                }
            }
            stationsListBox.Items.Remove(toDelete);
        }

        private void removeStationsButton_Click(object sender, RoutedEventArgs e)
        {
            stationsListBox.Items.Clear();
            List<Station> stations = StationDAO.getAllStations();
            stations.RemoveAll(s => s.Name == station.Name);
            foreach (var station in stations)
            {
                var listItem = new ListBoxItem();
                listItem.Content = station.Name;
                stationsListBox.Items.Add(listItem);
            }
            addStationButton.IsEnabled = false;
            removeStationsButton.IsEnabled = false;

            selectedStationNames = new List<string>();
            removeAllAdjacent();
        }

        private void removeAllAdjacent()
        {
            for (int i = canvas_map.Children.Count - 1; i >= 0; i--)
            {
                UIElement Child = canvas_map.Children[i];
                if (Child is Line)
                    canvas_map.Children.Remove(Child);
                if (Child is Rectangle)
                {
                    Rectangle casted = Child as Rectangle;
                    if (casted.Fill == Brushes.BlueViolet)
                        canvas_map.Children.Remove(Child);
                }
                if (Child is TextBlock)
                {
                    TextBlock casted = Child as TextBlock;
                    if (casted.Foreground == Brushes.BlueViolet)
                        canvas_map.Children.Remove(Child);
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (stationNameTextbox.Text.Trim().Length == 0)
            {
                MessageBoxButtons errButtons = MessageBoxButtons.OK;
                string errMessage = "Morate uneti ime stanice!";
                string errTitle = "Dodavanje stanice";
                System.Windows.Forms.MessageBox.Show(errMessage, errTitle, errButtons);
                return;
            }

            if (mode == "new")
            {
                if (StationDAO.StationNameExists(stationNameTextbox.Text))
                {
                    MessageBoxButtons errButtons = MessageBoxButtons.OK;
                    string errMessage = "Stanica " + stationNameTextbox.Text + " već postoji!";
                    string errTitle = "Dodavanje stanice";
                    System.Windows.Forms.MessageBox.Show(errMessage, errTitle, errButtons);
                    return;
                }

                Station newStation = new Station() { Name = stationNameTextbox.Text, position_x = Convert.ToInt32(p.X), position_y = Convert.ToInt32(p.Y) };
                StationDAO.AddStation(newStation);
                foreach (string s in selectedStationNames)
                {
                    LinkedStationDAO.addLinkedStations(newStation, StationDAO.GetStationByName(s));
                }
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string message = "Stanica " + stationNameTextbox.Text + " je uspešno dodata!";
                string title = "Dodavanje stanice";
                System.Windows.Forms.MessageBox.Show(message, title, buttons);
            }

            else if (mode == "modify")
            {
                if (StationDAO.StationNameExists(stationNameTextbox.Text) && stationNameTextbox.Text != oldStationName)
                {
                    MessageBoxButtons errButtons = MessageBoxButtons.OK;
                    string errMessage = "Stanica " + stationNameTextbox.Text + " već postoji!";
                    string errTitle = "Dodavanje stanice";
                    System.Windows.Forms.MessageBox.Show(errMessage, errTitle, errButtons);
                    return;
                }
                List<Station> links = new List<Station>();
                foreach (string s in selectedStationNames)
                {
                    links.Add(StationDAO.GetStationByName(s));
                }
                station.Name = stationNameTextbox.Text;
                station.position_x = Convert.ToInt32(p.X);
                station.position_y = Convert.ToInt32(p.Y);
                StationDAO.ModifyStation(station, links);
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string message = "Stanica " + oldStationName + " je uspešno izmenjena!";
                string title = "Izmena stanice";
                System.Windows.Forms.MessageBox.Show(message, title, buttons);

            }


            AdminWindow parentWindow = parent as AdminWindow;
            parentWindow.fillStackDataWithStations();
            Hide();
            parent.Show();
            return;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            parent.Show();
        }
    }
}