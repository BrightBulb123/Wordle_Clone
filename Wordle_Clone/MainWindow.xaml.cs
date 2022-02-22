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

namespace Wordle_Clone {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {

        int currentRow = 0;
        int currentColumn = 0;

        public MainWindow() {
            InitializeComponent();
        }

        private void Main_Loaded(object sender, RoutedEventArgs e) {
            lblDebug.Content = currentRow;
            //Border[] borders = {
            //                BR0C0, BR1C0, BR2C0,
            //                BR3C0, BR4C0, BR5C0,
            //                BR0C1, BR1C1, BR2C1,
            //                BR3C1, BR4C1, BR5C1,
            //                BR0C2, BR1C2, BR2C2,
            //                BR3C2, BR4C2, BR5C2,
            //                BR0C3, BR1C3, BR2C3,
            //                BR3C3, BR4C3, BR5C3,
            //                BR0C4, BR1C4, BR2C4,
            //                BR3C4, BR4C4, BR5C4
            //                };
        }

        private void Main_KeyDown(object sender, KeyEventArgs e) {
            if ((lblDebug.Content.ToString() == "1") || (lblDebug.Content.ToString() == "0")) {
                lblDebug.Content = "2";
            }
            else if (lblDebug.Content.ToString() == "2") {
                lblDebug.Content = BR0C0.Child.ToString());
            }

            Fill_Box(e.Key.ToString()[0]);
        }

        private void Fill_Box(char character) {

        }
    }
}
