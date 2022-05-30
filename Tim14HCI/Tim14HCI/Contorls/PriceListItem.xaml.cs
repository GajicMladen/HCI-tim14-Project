﻿using System;
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
    /// Interaction logic for PriceListItem.xaml
    /// </summary>
    public partial class PriceListItem : UserControl
    {
        public PriceListItem(Station station,int price, int time)
        {
            InitializeComponent();
            StationForDragAndDrop stationForDragAndDrop = new StationForDragAndDrop(station);
            grid_data.Children.Add(stationForDragAndDrop);
        }
    }
}
