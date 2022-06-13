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
using Tim14HCI.Contorls;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for TicketDisplayWindow.xaml
    /// </summary>
    public partial class TicketDisplayWindow : Window
    {
        Window parent;
        User user;

        public TicketDisplayWindow()
        {
            InitializeComponent();
        }

        public TicketDisplayWindow(Window parent, String purpose, User user)
        {
            InitializeComponent();
            this.parent = parent;
            this.user = user;

            if (purpose == "Bought")
            {
                lbl_title.Content = "Kupljene karte";
                FillStackDataWithTickets("Bought");
            }
            else
            {
                lbl_title.Content = "Rezervisane karte";
                FillStackDataWithTickets("Reservation");
            }
        }

        private void FillStackDataWithTickets(String kind)
        {
            stack_Data.Children.Clear();
            List<Ticket> tickets = new List<Ticket>();
            if (kind == "Bought")
            {
                tickets = TicketDAO.GetBoughtTicketsByUserID(this.user.UserID);
            }
            else
            {
                tickets = TicketDAO.GetReservationTicketsByUserID(this.user.UserID);
            }

            foreach (Ticket ticket in tickets)
            {
                TicketControl ticketControl = new TicketControl(ticket);
                stack_Data.Children.Add(ticketControl);
                
            }

        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            parent.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(System.Windows.Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = Help.Providers.HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Help.Providers.HelpProvider.ShowHelp("Client", this);
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
