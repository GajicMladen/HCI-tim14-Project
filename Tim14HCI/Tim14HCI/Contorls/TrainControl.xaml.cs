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
    /// Interaction logic for TrainControl.xaml
    /// </summary>
    public partial class TrainControl : System.Windows.Controls.UserControl
    {

        Train train;
        public TrainControl()
        {
            InitializeComponent();
        }
        public TrainControl(Train x)
        {
            train = x;
            InitializeComponent();

            lbl_Name.Content = train.Name;
            lbl_Capacity.Content = train.Capacity + " osoba";
            lbl_MaxSpeed.Content = train.MaxSpeed + " km/h";
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            string message = "Da li ste sigurni da želite da obrišete voz " + train.Name + "?";
            string title = "Brisanje voza";
            DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                AdminWindow parent = System.Windows.Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
                TrainDAO.RemoveTrain(train);
                parent.fillStackDataWithTrains();
                message = "Voz " + train.Name + " je uspešno obrisan!";
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
            NewTrain newTrain = new NewTrain(parentWindow, train);
            parentWindow.Hide();
            newTrain.Show();
        }
    }
}
