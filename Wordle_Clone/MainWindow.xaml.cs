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
            
        }

        private void Main_KeyDown(object sender, KeyEventArgs e) {
            if ((lblDebug.Content.ToString() == "1") || (lblDebug.Content.ToString() == "0")) {
                lblDebug.Content = "2";
            }
            else if (lblDebug.Content.ToString() == "2") {
                lblDebug.Content = "2";
            }

            Fill_Box(e.Key.ToString()[0]);
        }

        private void Fill_Box(char character) {
            lblDebug.Content = character;

            object txt = Guesses.FindName($"TR{currentRow}C{currentColumn}");
            if (txt is TextBlock txtBlock) {
                txtBlock.Text = character.ToString();
            }
            currentColumn++;

            if (currentColumn >= 5) {
                Change_Background(currentRow);
                currentRow++;
                currentColumn = 0;
            }
        }

        private void Change_Background(int row) {
            for (int i = 0; i < 5; i++) {
                Border bor = (Border)Guesses.FindName($"BR{row}C{i}");
                if (row % 2 == 0) {
                    //bor.Background = Brushes.Green;
                    bor.Background = Colours.ColoursDict()["orange"];
                }
                else if (row % 3 == 0) {
                    //bor.Background = Brushes.Blue;
                    bor.Background = Colours.ColoursDict()["blue"];
                }
                else {
                    //bor.Background = Brushes.DarkSlateGray;
                    bor.Background = Colours.ColoursDict()["gray"];
                }
                bor.BorderThickness = new Thickness(0);
            }
        }
    }

    public static class Colours {

        /// <summary>
        /// All the custom brushes to be used in the app
        /// </summary>

        // Setting up the brushes
        private static readonly BrushConverter bc = new();

        public static Dictionary<string, Brush> ColoursDict() {
            Dictionary<string, Brush> colours = new();
            
            var gray = bc.ConvertFrom("#3A3A3C");

            var blue = bc.ConvertFrom("#85C0F9");
            var orange = bc.ConvertFrom("#F5793A");

            var yellow = bc.ConvertFrom("#B59F3B");
            var green = bc.ConvertFrom("#538D4E");

            if (gray != null) { 
                colours["gray"] = (Brush)gray;
            }
            if (blue != null) {
                colours["blue"] = (Brush)blue;
            }
            if (orange != null) {
                colours["orange"] = (Brush)orange;
            }
            if (yellow != null) {
                colours["yellow"] = (Brush)yellow;
            }
            if (green != null) { 
                colours["green"] = (Brush)green;
            }

            return colours;
        }
    }
}
