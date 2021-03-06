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
    /// Interaction logic for StationControl.xaml
    /// </summary>
    public partial class StationControl : System.Windows.Controls.UserControl
    {


        Station station;

        public StationControl()
        {
            InitializeComponent();
        }
        public StationControl(Station x)
        {
            station = x;
            InitializeComponent();

            lbl_Name.Content = station.Name;
            string linkContent = "";

            List<Station> links = StationDAO.getLinkedStations(station);

            if (links.Count > 0)
            {
                foreach (var link in links)
                {
                    linkContent += link.Name + ", ";
                }

                linkContent = linkContent.Substring(0, linkContent.Length - 2);
            }
            else
            {
                linkContent = "Nema povezanih stanica!";
            }

            lbl_Links.Content = linkContent;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            string message = "Da li ste sigurni da želite da obrišete stanicu " + station.Name + "?";
            string title = "Brisanje stanice";
            DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                AdminWindow parent = System.Windows.Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
                StationDAO.RemoveStation(station);
                parent.fillStackDataWithStations();
                message = "Stanica " + station.Name + " je uspešno obrisana!";
                buttons = MessageBoxButtons.OK;
                System.Windows.Forms.MessageBox.Show(message, title, buttons);
            }
            else
            {
                return;
            }
        }

        private void Change_Button_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            NewStation newStation = new NewStation(parentWindow, station);
            parentWindow.Hide();
            newStation.Show();
        }
    }
}
