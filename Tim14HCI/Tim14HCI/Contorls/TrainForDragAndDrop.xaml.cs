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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tim14HCI.Model;

namespace Tim14HCI.Contorls
{
    /// <summary>
    /// Interaction logic for TrainForDragAndDrop.xaml
    /// </summary>
    public partial class TrainForDragAndDrop : UserControl
    {
        public Train train;
        public TrainPicker parent;
        public TrainForDragAndDrop()
        {
            InitializeComponent();
        }
        public TrainForDragAndDrop(TrainPicker y,Train x)
        {
            InitializeComponent();
            train = x;
            parent = y;
            lbl_Name.Content = x.Name;
            lbl_Capacity.Content = x.Capacity;
            lbl_MaxSpeed.Content = x.MaxSpeed;
        }

        public TrainForDragAndDrop( Train x)
        {
            InitializeComponent();
            train = x;
            lbl_Name.Content = x.Name;
            lbl_Capacity.Content = x.Capacity;
            lbl_MaxSpeed.Content = x.MaxSpeed;
        }
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent.selectedTrain = train;

            ScaleTransform scaleTransform = new ScaleTransform(0.5, 0.5);
            parent.newTrainLineWindow.stackPanel_selectedTrain.Children.Clear();
            parent.newTrainLineWindow.stackPanel_selectedTrain.Children.Add(new TrainForDragAndDrop(train) { RenderTransform = scaleTransform });
        }

        private void UserControl_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            parent.selectedTrain = train;
            ScaleTransform scaleTransform = new ScaleTransform(0.7, 0.7);
            parent.newTrainLineWindow.stackPanel_selectedTrain.Children.Clear();
            parent.newTrainLineWindow.stackPanel_selectedTrain.Children.Add(new TrainForDragAndDrop(train) { RenderTransform = scaleTransform});
        }
    }
}
