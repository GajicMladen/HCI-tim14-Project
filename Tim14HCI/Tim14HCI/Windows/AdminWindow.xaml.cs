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
using System.Windows.Shapes;
using Tim14HCI.Commands;
using Tim14HCI.Contorls;
using Tim14HCI.DAO;
using Tim14HCI.Help.Providers;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        Window parent;
        User user;

        public AdminWindow(Window x, User user)
        {
            parent = x;
            this.user = user;
            InitializeComponent();
            lbl_logedUser.Content = user.FirstName + " " + user.LastName;

            if (user.UserRole == UserRole.MANAGER)
            {
                lbl_userRole.Content = "Menadžer";
            }
            else {
                lbl_userRole.Content = "Klijent";
            }
            grid_test.Visibility = Visibility.Collapsed;

            AdminCommands.BindCommandsToWindow(this);
            showTrains();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void btn_logOut_Click(object sender, RoutedEventArgs e)
        {
            parent.Show();
            Close();
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp("Admin", this);
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void fillStackDataWithTrains() {

            stack_Data.Children.Clear();

            OpComboBox.IsEnabled = true;
            OpComboBox.Items.Clear();
            OpComboBox.Items.Add(new ComboBoxItem { Content = "Filtriraj veće" });
            OpComboBox.Items.Add(new ComboBoxItem { Content = "Filtriraj manje" });
            OpComboBox.SelectedIndex = 0;
            attributesComboBox.IsEnabled = true;
            attributesComboBox.Items.Clear();
            attributesComboBox.Items.Add(new ComboBoxItem { Content = "Kapacitet" });
            attributesComboBox.Items.Add(new ComboBoxItem { Content = "Brzina" });
            attributesComboBox.SelectedIndex = 0;

            List<Train> trains = TrainDAO.getAllTrains();
            
            foreach (Train train in trains) {

                TrainControl trainControl = new TrainControl(train);
                stack_Data.Children.Add(trainControl);
            }
            if (stack_Data.Children.Count == 0)
            {
                stack_Data.Children.Add(new Label() { Content = "Trenutno ne postoji nijedan voz! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
            }
        }

        public void fillStackDataWithTrainsSearch(string query)
        {
            stack_Data.Children.Clear();

            List<Train> trains = TrainDAO.getAllTrainsSearch(query);

            foreach (Train train in trains)
            {

                TrainControl trainControl = new TrainControl(train);
                stack_Data.Children.Add(trainControl);
            }
            if (stack_Data.Children.Count == 0)
            {
                stack_Data.Children.Add(new Label() { Content = "Nijedan voz ne odgovara pretrazi! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
            }
        }

        public void fillStackDataWithStations()
        {
            stack_Data.Children.Clear();

            attributesComboBox.IsEnabled = false;
            OpComboBox.IsEnabled = false;

            List<Station> stations = StationDAO.getAllStations();
            
            foreach (Station station in stations)
            {
                StationControl stationControl = new StationControl(station);
                stack_Data.Children.Add(stationControl);
            }
            if (stack_Data.Children.Count == 0)
            {
                stack_Data.Children.Add(new Label() { Content = "Trenutno ne postoji nijedna stanica! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
            }
        }

        public void fillStackDataWithStationsSearch(string query)
        {
            stack_Data.Children.Clear();

            List<Station> stations = StationDAO.getAllStationsSearch(query);

            foreach (Station station in stations)
            {
                StationControl stationControl = new StationControl(station);
                stack_Data.Children.Add(stationControl);
            }
            if (stack_Data.Children.Count == 0)
            {
                stack_Data.Children.Add(new Label() { Content = "Nijedna stanica ne odgovara pretrazi! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
            }
        }

        public void fillStackDataWithTrainLines()
        {
            stack_Data.Children.Clear();

            List<TrainLine> trainLines = TrainLinesDAO.getAllTrainLinesNotDeleted();

            OpComboBox.IsEnabled = true;
            OpComboBox.Items.Clear();
            OpComboBox.Items.Add(new ComboBoxItem { Content = "Filtriraj veće" });
            OpComboBox.Items.Add(new ComboBoxItem { Content = "Filtriraj manje" });
            OpComboBox.SelectedIndex = 0;
            attributesComboBox.IsEnabled = true;
            attributesComboBox.Items.Clear();
            attributesComboBox.Items.Add(new ComboBoxItem { Content = "Ukupno vreme" });
            attributesComboBox.Items.Add(new ComboBoxItem { Content = "Ukupna cena" });
            attributesComboBox.SelectedIndex = 0;

            foreach (TrainLine trainLine in trainLines)
            {
                TrainLineControl stationControl = new TrainLineControl(trainLine,this);
                stack_Data.Children.Add(stationControl);
            }
            if (stack_Data.Children.Count == 0) {
                stack_Data.Children.Add(new Label() { Content = "Trenutno ne postoji nijedna vozna linija! " ,FontSize=32 , HorizontalAlignment = HorizontalAlignment.Center});
            }
        }

        private void fillStackDataWithTrainLinesSearch(string query)
        {

            List<TrainLine> trainLines = TrainLinesDAO.getAllTrainLinesSearch(query);

            foreach (TrainLine trainLine in trainLines)
            {

                TrainLineControl trainControl = new TrainLineControl(trainLine, this);
                stack_Data.Children.Add(trainControl);
            }
            if (stack_Data.Children.Count == 0)
            {
                stack_Data.Children.Add(new Label() { Content = "Nijedna vozna linija ne odgovara pretrazi! ", FontSize = 32,HorizontalAlignment = HorizontalAlignment.Center });
            }
        }

        public void fillStackDataWithDepartures()
        {
            stack_Data.Children.Clear();
            List<Departure> departures = DepartureDAO.GetAllDepartures();

            stack_Data.Children.Clear();

            OpComboBox.IsEnabled = true;
            OpComboBox.Items.Clear();
            OpComboBox.Items.Add(new ComboBoxItem { Content = "Filtriraj buduće" });
            OpComboBox.Items.Add(new ComboBoxItem { Content = "Filtriraj prošlo" });
            OpComboBox.SelectedIndex = 0;
            attributesComboBox.IsEnabled = true;
            attributesComboBox.Items.Clear();
            attributesComboBox.Items.Add(new ComboBoxItem { Content = "Vreme polaska" });
            attributesComboBox.Items.Add(new ComboBoxItem { Content = "Vreme dolaska" });
            attributesComboBox.SelectedIndex = 0;

            foreach (Departure d in departures)
            {
                DepartureManagerControl departureControl = new DepartureManagerControl(d);
                stack_Data.Children.Add(departureControl);
            }
            if (stack_Data.Children.Count == 0)
            {
                stack_Data.Children.Add(new Label() { Content = "Trenutno ne postoji nijedan polazak! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
            }
        }

        public void fillStackDataWithDeparturesSearch(string query)
        {
            stack_Data.Children.Clear();
            List<Departure> departures = DepartureDAO.getAllDeparturesSearch(query);

            attributesComboBox.Items.Clear();
            attributesComboBox.Items.Add(new ComboBoxItem { Content = "Datum polaska" });
            attributesComboBox.Items.Add(new ComboBoxItem { Content = "Datum dolaska" });

            foreach (Departure d in departures)
            {
                DepartureManagerControl departureControl = new DepartureManagerControl(d);
                stack_Data.Children.Add(departureControl);
            }
            if (stack_Data.Children.Count == 0)
            {
                stack_Data.Children.Add(new Label() { Content = "Nijedna polazak ne odgovara pretrazi! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FilterButton.IsEnabled = true;
            showTrains();
        }

        public void showTrains() {
            fillStackDataWithTrains();
            lbl_ShownData.Content = "Vozovi";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FilterButton.IsEnabled = false;
            showStations();
        }

        public void showStations() {
            fillStackDataWithStations();
            lbl_ShownData.Content = "Stanice";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FilterButton.IsEnabled = true;
            showTrainLines();
        }

        public void showTrainLines() {
            lbl_ShownData.Content = "Vozne linije";
            stack_Data.Children.Clear();
            fillStackDataWithTrainLines();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            showAddNew();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            FilterButton.IsEnabled = true;
            showDepartures();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            showReports();
        }

        public void showReports() {

            ReportsWindow rw = new ReportsWindow(this);
            Visibility = Visibility.Hidden;
            rw.Show();
        }
        public void showDepartures() {
            lbl_ShownData.Content = "Red vožnje";
            stack_Data.Children.Clear();
            fillStackDataWithDepartures();
        }

        public void showAddNew() {

            if ((string)lbl_ShownData.Content == "Vozne linije")
            {
                NewTrainLine newTrainLine = new NewTrainLine(this);
                Visibility = Visibility.Hidden;
                newTrainLine.Show();
            }
            else if ((string)lbl_ShownData.Content == "Vozovi")
            {
                NewTrain newTrain = new NewTrain(this);
                Visibility = Visibility.Hidden;
                newTrain.Show();
            }
            else if ((string)lbl_ShownData.Content == "Stanice")
            {
                NewStation newStation = new NewStation(this);
                Visibility = Visibility.Hidden;
                newStation.Show();
            }
            else if ((string)lbl_ShownData.Content == "Red vožnje")
            {
                NewDeparture newDeparture = new NewDeparture(this);
                Visibility = Visibility.Hidden;
                newDeparture.Show();
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string query = searchTextBox.Text;
            if ((string)lbl_ShownData.Content == "Vozovi")
            {
                stack_Data.Children.Clear();
                fillStackDataWithTrainsSearch(query);
            }
            else if ((string)lbl_ShownData.Content == "Vozne linije")
            {
                stack_Data.Children.Clear();
                fillStackDataWithTrainLinesSearch(query);
            }
            else if ((string)lbl_ShownData.Content == "Stanice")
            {
                stack_Data.Children.Clear();
                fillStackDataWithStationsSearch(query);
            }
            else if ((string)lbl_ShownData.Content == "Red vožnje")
            {
                stack_Data.Children.Clear();
                fillStackDataWithDeparturesSearch(query);
            }
        }

        private void Demo_Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)lbl_ShownData.Content == "Vozovi")
            {
                DemoWindow dw = new DemoWindow(this, "trains");
                Visibility = Visibility.Hidden;
                dw.Show();
            }
            else if ((string)lbl_ShownData.Content == "Vozne linije")
            {
                DemoWindow dw = new DemoWindow(this, "trainlines");
                Visibility = Visibility.Hidden;
                dw.Show();
            }
            else if ((string)lbl_ShownData.Content == "Stanice")
            {
                DemoWindow dw = new DemoWindow(this, "stations");
                Visibility = Visibility.Hidden;
                dw.Show();
            }
            else if ((string)lbl_ShownData.Content == "Red vožnje")
            {
                DemoWindow dw = new DemoWindow(this, "departures");
                Visibility = Visibility.Hidden;
                dw.Show();
            }
        }

        public void FocusSearch() {

            searchTextBox.Focus();
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_Click_6(sender, e);
            }
        }

        private void Filter_Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)lbl_ShownData.Content == "Vozovi")
            {
                filterTrains();
            }
            else if ((string)lbl_ShownData.Content == "Vozne linije")
            {
                filterTrainLines();
            }
            else if ((string)lbl_ShownData.Content == "Red vožnje")
            {
                filterDepartures();
            }
        }

        private void filterTrains()
        {
            if (searchTextBox.Text == "")
            {
                fillStackDataWithTrains();
                return;
            }
            try
            {
                int.Parse(searchTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Neispravan unos parametra za filtriranje!", "Greška");
                return;
            }
            if ((string)(OpComboBox.SelectedValue as ComboBoxItem).Content == "Filtriraj veće")
            {
                if ((string)(attributesComboBox.SelectedValue as ComboBoxItem).Content == "Brzina")
                {
                    List<Train> trains = TrainDAO.getAllTrains().Where(t => t.MaxSpeed > int.Parse(searchTextBox.Text)).ToList();

                    stack_Data.Children.Clear();
                    foreach (Train train in trains)
                    {
                        TrainControl trainControl = new TrainControl(train);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedan voz ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
                else // Kapacitet
                {
                    List<Train> trains = TrainDAO.getAllTrains().Where(t => t.Capacity > int.Parse(searchTextBox.Text)).ToList();

                    stack_Data.Children.Clear();
                    foreach (Train train in trains)
                    {
                        TrainControl trainControl = new TrainControl(train);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedan voz ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
            }
            else
            {
                if ((string)(attributesComboBox.SelectedValue as ComboBoxItem).Content == "Brzina")
                {
                    List<Train> trains = TrainDAO.getAllTrains().Where(t => t.MaxSpeed < int.Parse(searchTextBox.Text)).ToList();

                    stack_Data.Children.Clear();
                    foreach (Train train in trains)
                    {
                        TrainControl trainControl = new TrainControl(train);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedan voz ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
                else // Kapacitet
                {
                    List<Train> trains = TrainDAO.getAllTrains().Where(t => t.Capacity < int.Parse(searchTextBox.Text)).ToList();

                    stack_Data.Children.Clear();
                    foreach (Train train in trains)
                    {
                        TrainControl trainControl = new TrainControl(train);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedan voz ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
            }
        }

        private void filterTrainLines()
        {
            if (searchTextBox.Text == "")
            {
                fillStackDataWithTrainLines();
                return;
            }
            try
            {
                int.Parse(searchTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Neispravan unos parametra za filtriranje!", "Greška");
                return;
            }
            if ((string)(OpComboBox.SelectedValue as ComboBoxItem).Content == "Filtriraj veće")
            {
                if ((string)(attributesComboBox.SelectedValue as ComboBoxItem).Content == "Ukupno vreme")
                {
                    List<TrainLine> trainLines = TrainLinesDAO.getAllTrainLines().Where(t => t.getTotalTime() > int.Parse(searchTextBox.Text)).ToList();

                    stack_Data.Children.Clear();
                    foreach (TrainLine tl in trainLines)
                    {
                        TrainLineControl trainControl = new TrainLineControl(tl, this);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedna linija ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
                else // Cena
                {
                    List<TrainLine> trainLines = TrainLinesDAO.getAllTrainLines().Where(t => t.getTotalPrice() > int.Parse(searchTextBox.Text)).ToList();

                    stack_Data.Children.Clear();
                    foreach (TrainLine tl in trainLines)
                    {
                        TrainLineControl trainControl = new TrainLineControl(tl, this);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedna linija ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
            }
            else
            {
                if ((string)(attributesComboBox.SelectedValue as ComboBoxItem).Content == "Ukupno vreme")
                {
                    List<TrainLine> trainLines = TrainLinesDAO.getAllTrainLines().Where(t => t.getTotalTime() < int.Parse(searchTextBox.Text)).ToList();

                    stack_Data.Children.Clear();
                    foreach (TrainLine tl in trainLines)
                    {
                        TrainLineControl trainControl = new TrainLineControl(tl, this);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedna linija ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
                else // Cena
                {
                    List<TrainLine> trainLines = TrainLinesDAO.getAllTrainLines().Where(t => t.getTotalPrice() < int.Parse(searchTextBox.Text)).ToList();

                    stack_Data.Children.Clear();
                    foreach (TrainLine tl in trainLines)
                    {
                        TrainLineControl trainControl = new TrainLineControl(tl, this);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedna linija ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
            }
        }

        private void filterDepartures()
        {
            if (searchTextBox.Text == "")
            {
                fillStackDataWithDepartures();
                return;
            }
            try
            {
                DateTime.ParseExact(searchTextBox.Text, "dd.MM.yyyy. HH:mm", null);
            }
            catch
            {
                MessageBox.Show("Neispravan unos parametra za filtriranje!\nPotrebno je uneti datum u formatu 'dd.mm.yyyy. hh:mm'", "Greška");
                return;
            }
            if ((string)(OpComboBox.SelectedValue as ComboBoxItem).Content == "Filtriraj buduće")
            {
                if ((string)(attributesComboBox.SelectedValue as ComboBoxItem).Content == "Vreme polaska")
                {
                    List<Departure> deps = DepartureDAO.GetAllDepartures().Where(t => t.StartTime.CompareTo(DateTime.ParseExact(searchTextBox.Text, "dd.MM.yyyy. HH:mm", null)) > 0).ToList();

                    stack_Data.Children.Clear();
                    foreach (Departure d in deps)
                    {
                        DepartureManagerControl trainControl = new DepartureManagerControl(d);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedan polazak ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
                else // Vreme dolaska
                {
                    List<Departure> deps = DepartureDAO.GetAllDepartures().Where(t => t.GetEndTime().CompareTo(DateTime.ParseExact(searchTextBox.Text, "dd.MM.yyyy. HH:mm", null)) > 0).ToList();

                    stack_Data.Children.Clear();
                    foreach (Departure d in deps)
                    {
                        DepartureManagerControl trainControl = new DepartureManagerControl(d);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedan polazak ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
            }
            else
            {
                if ((string)(attributesComboBox.SelectedValue as ComboBoxItem).Content == "Vreme polaska")
                {
                    List<Departure> deps = DepartureDAO.GetAllDepartures().Where(t => t.StartTime.CompareTo(DateTime.ParseExact(searchTextBox.Text, "dd.MM.yyyy. HH:mm", null)) < 0).ToList();

                    stack_Data.Children.Clear();
                    foreach (Departure d in deps)
                    {
                        DepartureManagerControl trainControl = new DepartureManagerControl(d);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedan polazak ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
                else // Vreme dolaska
                {
                    List<Departure> deps = DepartureDAO.GetAllDepartures().Where(t => t.GetEndTime().CompareTo(DateTime.ParseExact(searchTextBox.Text, "dd.MM.yyyy. HH:mm", null)) < 0).ToList();

                    stack_Data.Children.Clear();
                    foreach (Departure d in deps)
                    {
                        DepartureManagerControl trainControl = new DepartureManagerControl(d);
                        stack_Data.Children.Add(trainControl);
                    }
                    if (stack_Data.Children.Count == 0)
                    {
                        stack_Data.Children.Add(new Label() { Content = "Nijedan polazak ne odgovara filteru! ", FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center });
                    }
                }
            }
        }
    }
}
