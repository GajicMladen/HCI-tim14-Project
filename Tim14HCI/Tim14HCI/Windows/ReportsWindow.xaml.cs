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
using Tim14HCI.Contorls;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow : Window
    {
        public Window parent;
        public string mode;

        public Report monthlyReport;
        public ReportPerLine perLineReport;

        public ReportsWindow()
        {
            InitializeComponent();
        }

        public ReportsWindow(Window x)
        {
            parent = x;
            InitializeComponent();
            monthlyReport = new Report(TicketDAO.GetReportByMonths());
            perLineReport = new ReportPerLine();
            chartVisual.Children.Add(monthlyReport);
            mode = "monthly";
        
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

        private void detailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (mode == "monthly")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string title = "Detaljni mesečni izveštaj";
                string message = "";
                foreach (KeyValuePair<string, ReportItem> entry in monthlyReport.SoldTicketsInfo)
                {
                    message += $"Mesec: {entry.Key}, Ukupno prodatih karata: {entry.Value.TotalTicketsSold}, Ukupan promet: {entry.Value.TotalIncome}\n";
                    message += "=============================================\n";
                }
                _ = System.Windows.Forms.MessageBox.Show(message, title, buttons);
            }
        }

        private void departureReportButton_Click(object sender, RoutedEventArgs e)
        {
            if (mode == "monthly")
            {
                chartVisual.Children.Clear();
                chartVisual.Children.Add(perLineReport);
                detailsButton.IsEnabled = false;
                mode = "per_line";
                lineReportButton.Content = "Mesečni izveštaj";
                titleLabel.Content = "Izveštaj po linijama";
            }
            else
            {
                chartVisual.Children.Clear();
                chartVisual.Children.Add(monthlyReport);
                detailsButton.IsEnabled = true;
                mode = "monthly";
                lineReportButton.Content = "Izveštaj po polascima";
                titleLabel.Content = "Mesečni izveštaj";
            }

        }
    }
}
