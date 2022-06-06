using System;
using System.Windows;
using Tim14HCI.DAO;
using Tim14HCI.Model;

namespace Tim14HCI.Windows
{
    /// <summary>
    /// Interaction logic for NewTrain.xaml
    /// </summary>
    public partial class NewTrain : Window
    {
        private Window parent;
        private Train train;

        public NewTrain()
        {
            InitializeComponent();
        }

        public NewTrain(Window x)
        {
            parent = x;
            InitializeComponent();
            train = new Train();
            btn_Back.IsEnabled = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private bool changeDisplay()
        {
            if (progresBar.SelectedIndex == 0)
            {
                btn_Back.IsEnabled = false;

                if (TrainDAO.TrainNameExists(trainNameTextbox.Text))
                {
                    errorLabel.Content = "Voz sa zadatim imenom već postoji!";
                    return false;
                }

                else
                {
                    int capacity;
                    if (!int.TryParse(trainCapacityTextbox.Text, out capacity))
                    {
                        errorLabel.Content = "Kapacitet mora biti brojčana vrednost!";
                        return false;
                    }
                    else if (capacity <= -1)
                    {
                        errorLabel.Content = "Kapacitet mora biti veći ili jednak nuli!";
                        return false;
                    }
                    else train.Capacity = capacity;

                    int maxSpeed;
                    if (!int.TryParse(trainMaxSpeedTextbox.Text, out maxSpeed))
                    {
                        errorLabel.Content = "Maksimalna brzina mora biti brojčana vrednost!";
                        return false;
                    }
                    else if (maxSpeed <= -1)
                    {
                        errorLabel.Content = "Maksimalna brzina mora biti veća ili jednaka nuli!";
                        return false;
                    }
                    else train.MaxSpeed = maxSpeed;
                }
            }
            return true;

            if (progresBar.SelectedIndex == 1)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (changeDisplay())
                progresBar.SelectedIndex++;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            progresBar.SelectedIndex -= 2;
            if (progresBar.SelectedIndex < -1) progresBar.SelectedIndex = -1;
            grid_display.Children.Clear();
            changeDisplay();
            progresBar.SelectedIndex++;
        }
    }
}
