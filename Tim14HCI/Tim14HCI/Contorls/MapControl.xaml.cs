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
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        public MapControl()
        {
            InitializeComponent();
        }

        public MapControl(List<Station> route)
        {
            InitializeComponent();
            foreach (Station station in route)
            {
                drawStationOnMap(canvas_map, station, false);
            }
            for (int i = 1; i < route.Count; i++)
            {
                drawLinesOnMap(canvas_map, route[i - 1], route[i]);
            }
        }


        private void drawStationOnMap(Canvas canvas_map, Station x, bool onWay)
        {

            Rectangle rectangle = new Rectangle() { Width = 10, Height = 10 };
            TextBlock textBlock = new TextBlock() { Text = x.Name };

            if (onWay)
            {
                rectangle.Fill = Brushes.Blue;
                textBlock.Foreground = Brushes.Blue;
            }
            else
            {
                rectangle.Fill = Brushes.Black;
                textBlock.Foreground = Brushes.Black;
            }

            Canvas.SetLeft(rectangle, x.position_x);
            Canvas.SetLeft(textBlock, x.position_x + 10);
            Canvas.SetTop(rectangle, x.position_y);
            Canvas.SetTop(textBlock, x.position_y - 10);

            canvas_map.Children.Add(rectangle);
            canvas_map.Children.Add(textBlock);
        }

        private void drawLinesOnMap(Canvas canvas_map, Station station1, Station station2)
        {

            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 5;

            line.X1 = station1.position_x + 5;
            line.Y1 = station1.position_y + 5;

            line.X2 = station2.position_x + 5;
            line.Y2 = station2.position_y + 5;

            canvas_map.Children.Add(line);

        }

    }
}
