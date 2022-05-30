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
using Tim14HCI.Windows;

namespace Tim14HCI.Contorls
{
    /// <summary>
    /// Interaction logic for RoutePicker.xaml
    /// </summary>
    public partial class RoutePicker : UserControl
    {

        List<List<Station>> routes;
        public List<Station> selectedRoute;
        NewTrainLine parent;
        public RoutePicker(List<List<Station>> posibleRoute, NewTrainLine window )
        {
            InitializeComponent();
            parent = window;
            routes = posibleRoute;
            posibleRoute.ForEach(
                pr => addMap(pr)
                );
        }

        private void addMap(List<Station> route)
        {
            ListBoxItem lb_Item = new ListBoxItem();

            Canvas newMap = new Canvas();
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "/Images/SerbiaMap.jpg"));
            newMap.Background = b;
            newMap.Width = 230;
            newMap.Height = 300;

            foreach (Station station in route) {
                drawStationOnMap(newMap,station, false);     
            }
            for (int i = 1; i < route.Count; i++) {
                drawLinesOnMap(newMap, route[i - 1], route[i]);
            }

            lb_Item.Content = newMap;


            lsbx_items.Items.Add(lb_Item);

        }


        private void drawStationOnMap(Canvas canvas_map,Station x, bool onWay)
        {

            Rectangle rectangle = new Rectangle() { Width = 10, Height = 10};
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
            Canvas.SetTop(textBlock, x.position_y - 10 );

            canvas_map.Children.Add(rectangle);
            canvas_map.Children.Add(textBlock);
        }

        private void drawLinesOnMap(Canvas canvas_map, Station station1,Station station2) {

            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 5;

            line.X1 = station1.position_x+5;
            line.Y1 = station1.position_y+5;

            line.X2 = station2.position_x+5;
            line.Y2 = station2.position_y+5;

            canvas_map.Children.Add(line);
        
        }


        private void lsbx_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRoute = routes[lsbx_items.SelectedIndex];
            parent.stack_Route.Children.Clear();
            /*for (int i = 0; i < selectedRoute.Count; i++)
            {
                if (i == 0 || i == selectedRoute.Count - 1)
                    parent.stack_Route.Children.Add(new Label() { Content = selectedRoute[i].Name, Foreground = Brushes.Blue });
                else
                    parent.stack_Route.Children.Add(new Label() { Content = "\t" + selectedRoute[i].Name, Foreground = Brushes.Black });


            }*/
            MapControl mapControl = new MapControl(selectedRoute);
            mapControl.RenderTransform = new ScaleTransform(0.5, 0.5);
            parent.stack_Route.Children.Add(mapControl);
            //parent.stack_Route.RenderTransform = new ScaleTransform(0.5, 0.5);
            parent.stack_Route.Height = 150;
        }
    }
}
