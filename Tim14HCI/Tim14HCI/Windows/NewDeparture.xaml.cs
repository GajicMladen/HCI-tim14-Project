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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for NewDeparture.xaml
    /// </summary>
    public partial class NewDeparture : Window
    {
        private Window parent;
        private string mode;

        private Departure departure;

        private List<TrainLine> trainLines;
        
        public NewDeparture()
        {
            mode = "none";
            InitializeComponent();
        }

        public NewDeparture(Window x)
        {
            parent = x;
            mode = "new";
            InitializeComponent();
            departure = new Departure();

            trainLines = TrainLinesDAO.getAllTrainLines();
            foreach (var line in trainLines)
            {
                var listItem = new ListBoxItem();
                listItem.Content = "ID: " + line.TrainLineID + " | Putanja: " + StationDAO.GetStationByID(line.StartStationID).Name;

                List<OnWayStation> ows = StationDAO.GetOnWayStations(line.TrainLineID);
                foreach (OnWayStation o in ows)
                {
                    listItem.Content += " - " + StationDAO.GetStationByID(o.StationID).Name;
                }
                linesListBox.Items.Add(listItem);
            }
        }

        public NewDeparture(Window x, Departure d)
        {
            parent = x;
            mode = "modify";
            InitializeComponent();
            departure = d;
            addButton.Content = "Izmeni";

            trainLines = TrainLinesDAO.getAllTrainLines();
            int selectedIndex = 0;
            bool stopCount = false;
            foreach (var line in trainLines)
            {
                var listItem = new ListBoxItem();
                listItem.Content = "ID: " + line.TrainLineID + " | Putanja: " + StationDAO.GetStationByID(line.StartStationID).Name;

                List<OnWayStation> ows = StationDAO.GetOnWayStations(line.TrainLineID);
                foreach (OnWayStation o in ows)
                {
                    listItem.Content += " - " + StationDAO.GetStationByID(o.StationID).Name;
                }
                linesListBox.Items.Add(listItem);

                if (line.TrainLineID == departure.TrainLineID) stopCount = true;
                if (!stopCount) selectedIndex++;
            }
            linesListBox.SelectedItem = linesListBox.Items.GetItemAt(selectedIndex);
            startDateTextBox.Text = departure.StartTime.ToString("dd.MM.yyyy. HH:mm");
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            parent.Show();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (linesListBox.SelectedIndex == -1)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string message = "Morate odabrati liniju za polazak!";
                string title;
                if (mode == "new") title = "Dodavanje polaska";
                else title = "Izmena polaska";
                _ = System.Windows.Forms.MessageBox.Show(message, title, buttons);
                return;
            }

            if (!TryConvertStringToDT(startDateTextBox.Text))
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string message = "Neispravan format polaznog datuma i vremena!";
                string title;
                if (mode == "new") title = "Dodavanje polaska";
                else title = "Izmena polaska";
                _ = System.Windows.Forms.MessageBox.Show(message, title, buttons);
                return;
            }
            else
            {
                DateTime dt = DateTime.ParseExact(startDateTextBox.Text, "dd.MM.yyyy. HH:mm", null);
                if (!IsValidDate(dt))
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    string message = "Datum mora biti u budućnosti!";
                    string title;
                    if (mode == "new") title = "Dodavanje polaska";
                    else title = "Izmena polaska";
                    _ = System.Windows.Forms.MessageBox.Show(message, title, buttons);
                    return;
                }
                departure.StartTime = dt;
                if (mode == "modify")
                {
                    DepartureDAO.ModifyDeparture(departure);
                    MessageBoxButtons b = MessageBoxButtons.OK;
                    string m = "Polazak je uspešno izmenjen!";
                    string t = "Izmena polaska";
                    System.Windows.Forms.MessageBox.Show(m, t, b);
                } else
                {
                    DepartureDAO.AddDeparture(departure);
                    MessageBoxButtons b = MessageBoxButtons.OK;
                    string m = "Polazak je uspešno dodat!";
                    string t = "Dodavanje polaska";
                    System.Windows.Forms.MessageBox.Show(m, t, b);
                }

                AdminWindow parentWindow = parent as AdminWindow;
                parentWindow.fillStackDataWithDepartures();
                Hide();
                parent.Show();
                return;
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            string selectedOption = (linesListBox.SelectedItem as ListBoxItem).Content.ToString();
            int trainLineId = int.Parse(selectedOption.Split('|')[0].Split(':')[1].Trim());
            departure.TrainLineID = trainLineId;
        }

        private bool IsValidDate(DateTime? dt)
        {
            try
            {
                return ((DateTime)dt).CompareTo(DateTime.Now) > 0;
            } catch
            {
                return false;
            }
            
        }

        private bool TryConvertStringToDT(string s)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(s, "dd.MM.yyyy. HH:mm", null);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
