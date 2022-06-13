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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tim14HCI.DAO;
using Tim14HCI.Model;
using Tim14HCI.Windows;

namespace Tim14HCI.Contorls
{
    /// <summary>
    /// Interaction logic for DepartureControl.xaml
    /// </summary>
    public partial class DepartureManagerControl : System.Windows.Controls.UserControl
    {
        Departure departure;

        public DepartureManagerControl()
        {
            InitializeComponent();
        }

        public DepartureManagerControl(Departure x)
        {
            departure = x;
            InitializeComponent();

            TrainLine line = TrainLinesDAO.getTrainLineByID(departure.TrainLineID);

            lbl_Line.Content = StationDAO.GetStationByID(line.StartStationID).Name + " - " + StationDAO.GetStationByID(line.EndStationID).Name;
            lbl_StartTime.Content = departure.StartTime.ToString("dd.MM.yyyy. HH:mm");
            lbl_EndTime.Content = departure.StartTime.AddMinutes(StationDAO.GetTrainLineDuration(departure.TrainLineID)).ToString("dd.MM.yyyy. HH:mm");
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            string message = "Da li ste sigurni da želite da obrišete odabrani polazak?";
            string title = "Brisanje polaska";
            DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                AdminWindow parent = System.Windows.Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
                DepartureDAO.RemoveDeparture(departure);
                parent.fillStackDataWithDepartures();
                message = "Polazak je uspešno obrisan!";
                buttons = MessageBoxButtons.OK;
                System.Windows.Forms.MessageBox.Show(message, title, buttons);

            }
            else
            {
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            NewDeparture newDep= new NewDeparture(parentWindow, departure);
            parentWindow.Hide();
            newDep.Show();
        }
    }
}