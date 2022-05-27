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
using Tim14HCI.DAO;
using Tim14HCI.Model;
using Tim14HCI.Windows;

namespace Tim14HCI.Contorls
{
    /// <summary>
    /// Interaction logic for TrainPicker.xaml
    /// </summary>
    public partial class TrainPicker : UserControl
    {

        public Train selectedTrain;
        public NewTrainLine newTrainLineWindow;
        public TrainPicker(NewTrainLine x)
        {
            newTrainLineWindow = x;
            InitializeComponent();
            List<Train> trains = TrainDAO.getAllTrains();
            trains.ForEach(train =>
            {
                TrainForDragAndDrop trainForDragAndDrop = new TrainForDragAndDrop(this,train);
                lsbx_items.Items.Add(trainForDragAndDrop);
            });
        }

    }
}
