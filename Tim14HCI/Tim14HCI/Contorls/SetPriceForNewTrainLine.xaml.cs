using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SetPriceForNewTrainLine.xaml
    /// </summary>
    public partial class SetPriceForNewTrainLine : UserControl
    {

        public List<TextBox> prices;
        public List<TextBox> times;
        public SetPriceForNewTrainLine(List<Station> route)
        {
            InitializeComponent();
            
            prices = new List<TextBox>();
            times = new List<TextBox>();

            int i = 0;
            route.ForEach(station =>
            {
                addSetPriceItem(station,i ==0);
                i++;
            });
        }
        private void addSetPriceItem(Station station,bool startStation) {

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;

            StationForDragAndDrop stationForDragAndDrop = new StationForDragAndDrop(station);

            stackPanel.Children.Add(stationForDragAndDrop);

            if (!startStation) {
                StackPanel stackPanelPrice = new StackPanel();
                Label labelPrice = new Label() { Content= "Cena ( din ) : "};
                TextBox textBoxPrice = new TextBox();
                textBoxPrice.Width = 75;
                textBoxPrice.PreviewTextInput += NumberValidationTextBox;
                stackPanelPrice.Orientation = Orientation.Horizontal;
                stackPanelPrice.HorizontalAlignment = HorizontalAlignment.Right;
                stackPanelPrice.Children.Add(labelPrice);
                stackPanelPrice.Children.Add(textBoxPrice);

                StackPanel stackPanelTime = new StackPanel();
                Label labelTime = new Label() { Content = "Vreme ( min ) : " };
                TextBox textBoxTime = new TextBox();
                textBoxTime.Width = 75;
                textBoxTime.PreviewTextInput += NumberValidationTextBox;
                stackPanelTime.Orientation = Orientation.Horizontal;
                stackPanelTime.HorizontalAlignment = HorizontalAlignment.Right;
                stackPanelTime.Children.Add(labelTime);
                stackPanelTime.Children.Add(textBoxTime);

                stackPanel.Children.Add(stackPanelPrice);
                stackPanel.Children.Add(stackPanelTime);

                prices.Add(textBoxPrice);
                times.Add(textBoxTime);
            }

            lsbx_items.Items.Add(stackPanel);

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public bool allDataIsEnterd() {
            foreach (TextBox textBox in prices) {
                if (textBox.Text == "")
                    return false;
            }
            foreach (TextBox textBox in times)
            {
                if (textBox.Text == "")
                    return false;
            }

            return true;
        }

        public List<int> getAllPrices() {
            List<int> ret = new List<int>();
            foreach (TextBox textBox in prices) {
                ret.Add(Int32.Parse(textBox.Text));
            }
            return ret;
        }
        public List<int> getAllTimes()
        {
            List<int> ret = new List<int>();
            foreach (TextBox textBox in times)
            {
                ret.Add(Int32.Parse(textBox.Text));
            }
            return ret;
        }
    }
}
