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
    /// Interaction logic for PriceList.xaml
    /// </summary>
    public partial class PriceList : UserControl
    {
        public PriceList(List<Station> route,List<int> prices, List<int> times )
        {
            InitializeComponent();

            for(int i = 0; i< route.Count; i++)
            { 
                if(i == 0) { }

                if (i == route.Count) { }

                if (i > 0) { 
                
                }
            }
        }
    }
}
