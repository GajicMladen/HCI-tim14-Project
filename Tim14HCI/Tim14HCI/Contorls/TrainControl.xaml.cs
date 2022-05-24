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
    /// Interaction logic for TrainControl.xaml
    /// </summary>
    public partial class TrainControl : UserControl
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
            lbl_Capacity.Content = train.Capacity;
            lbl_MaxSpeed.Content = train.MaxSpeed;
        }
    }
}
