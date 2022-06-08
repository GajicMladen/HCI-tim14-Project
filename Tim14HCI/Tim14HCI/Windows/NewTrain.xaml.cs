using System;
using System.Windows;
using System.Windows.Forms;
using Tim14HCI.Contorls;
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

        private string mode;

        private String oldTrainName = null;
        public NewTrain()
        {
            mode = "none";
            InitializeComponent();
        }

        public NewTrain(Window x)
        {
            parent = x;
            mode = "new";
            InitializeComponent();
            train = new Train();
            //btn_Back.IsEnabled = false;

            //trainDataForm = new TrainDataForm();
            //trainLinePicker = new TrainLinePicker();
            //grid_display.Children.Add(trainDataForm);
        }

        public NewTrain(Window x, Train existingTrain)
        {
            parent = x;
            mode = "modify";
            InitializeComponent();
            train = existingTrain;
            addButton.Content = "Izmeni";
            oldTrainName = existingTrain.Name;
            trainNameTextbox.Text = train.Name;
            trainCapacityTextbox.Text = train.Capacity.ToString();
            trainMaxSpeedTextbox.Text = train.MaxSpeed.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(mode == "new")
            {
                if (TrainDAO.TrainNameExists(trainNameTextbox.Text))
                {
                    errorLabel.Content = "Voz sa zadatim imenom već postoji!";
                    return;
                }
            }

            else if (mode == "modify")
            {
                if (TrainDAO.TrainNameExists(trainNameTextbox.Text) && trainNameTextbox.Text != oldTrainName)
                {
                    errorLabel.Content = "Voz sa zadatim imenom već postoji!";
                    return;
                }
            }

            if (trainNameTextbox.Text.Trim().Length == 0)
            {
                errorLabel.Content = "Neispravno ime voza!";
            }

            else
            {
                int capacity;
                if (!int.TryParse(trainCapacityTextbox.Text, out capacity))
                {
                    errorLabel.Content = "Kapacitet mora biti brojčana vrednost!";
                    return;
                }
                else if (capacity <= -1)
                {
                    errorLabel.Content = "Kapacitet mora biti veći ili jednak nuli!";
                    return;
                }
                else train.Capacity = capacity;

                int maxSpeed;
                if (!int.TryParse(trainMaxSpeedTextbox.Text, out maxSpeed))
                {
                    errorLabel.Content = "Maksimalna brzina mora biti brojčana vrednost!";
                    return;
                }
                else if (maxSpeed <= -1)
                {
                    errorLabel.Content = "Maksimalna brzina mora biti veća ili jednaka nuli!";
                    return;
                }
                else train.MaxSpeed = maxSpeed;

                train.Name = trainNameTextbox.Text;
                train.Capacity = capacity;
                train.MaxSpeed = maxSpeed;

                if (mode == "new")
                {
                    TrainDAO.AddTrain(train);
                    errorLabel.Content = "";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    string message = "Voz " + train.Name + " je uspešno dodat!";
                    string title = "Dodavanje voza";
                    System.Windows.Forms.MessageBox.Show(message, title, buttons);
                    AdminWindow parentCasted = parent as AdminWindow;
                    parentCasted.fillStackDataWithTrains();
                    Hide();
                    parent.Show();
                    return;
                }
                else if (mode == "modify")
                {
                    TrainDAO.ModifyTrain(train);
                    errorLabel.Content = "";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    string message = "Voz " + train.Name + " je uspešno izmenjen!";
                    string title = "Izmena voza";
                    System.Windows.Forms.MessageBox.Show(message, title, buttons);
                    AdminWindow parentCasted = parent as AdminWindow;
                    parentCasted.fillStackDataWithTrains();
                    Hide();
                    parent.Show();
                    return;
                }
            }
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

        //private bool changeDisplayForward()
        //{
        //    if (progresBar.SelectedIndex == 0)
        //    { 
        //        if (TrainDAO.TrainNameExists(trainDataForm.trainNameTextbox.Text))
        //        {
        //            trainDataForm.errorLabel.Content = "Voz sa zadatim imenom već postoji!";
        //            return false;
        //        }

        //        else
        //        {
        //            int capacity;
        //            if (!int.TryParse(trainDataForm.trainCapacityTextbox.Text, out capacity))
        //            {
        //                trainDataForm.errorLabel.Content = "Kapacitet mora biti brojčana vrednost!";
        //                return false;
        //            }
        //            else if (capacity <= -1)
        //            {
        //                trainDataForm.errorLabel.Content = "Kapacitet mora biti veći ili jednak nuli!";
        //                return false;
        //            }
        //            else train.Capacity = capacity;

        //            int maxSpeed;
        //            if (!int.TryParse(trainDataForm.trainMaxSpeedTextbox.Text, out maxSpeed))
        //            {
        //                trainDataForm.errorLabel.Content = "Maksimalna brzina mora biti brojčana vrednost!";
        //                return false;
        //            }
        //            else if (maxSpeed <= -1)
        //            {
        //                trainDataForm.errorLabel.Content = "Maksimalna brzina mora biti veća ili jednaka nuli!";
        //                return false;
        //            }
        //            else train.MaxSpeed = maxSpeed;

        //            train.Name = trainDataForm.TrainName;
        //            train.Capacity = capacity;
        //            train.MaxSpeed = maxSpeed;

        //            grid_display.Children.Clear();
        //            trainDataForm.errorLabel.Content = "";
        //            btn_Back.IsEnabled = true;

        //            grid_display.Children.Add(trainLinePicker);
        //            populateTrainLinePicker();
        //            return true;
        //        }
        //    }

        //    else if (progresBar.SelectedIndex == 1)
        //    {
        //        return true;   
        //    }

        //    else
        //    {
        //        return true;
        //    }
        //}

        //private void populateTrainLinePicker()
        //{
        //    foreach(var line in TrainLinesDAO.getAllTrainLines())
        //    {
        //        TrainLineControl lineCtrl = new TrainLineControl(line);

        //        trainLinePicker.lsbx.Items.Add(lineCtrl);
        //        //trainLinePicker.stack_Data.Children.Add(lineCtrl);
        //    }

        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (changeDisplayForward())
        //        progresBar.SelectedIndex++;

        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    progresBar.SelectedIndex--;
        //}
    }
}
