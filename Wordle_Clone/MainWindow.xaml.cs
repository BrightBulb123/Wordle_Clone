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

            if (currentRow <= 5) {
                Fill_Box(e.Key.ToString()[0]);
            }
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
                    bor.Background = Colours.colours["orange"];
                }
                else if (row % 3 == 0) {
                    //bor.Background = Brushes.Blue;
                    bor.Background = Colours.colours["blue"];
                }
                else {
                    //bor.Background = Brushes.DarkSlateGray;
                    bor.Background = Colours.colours["gray"];
                }
                bor.BorderThickness = new Thickness(0);
            }
        }
    }

    /// <summary>
    /// All the custom brushes to be used in the app
    /// </summary>

    public static class Colours {
        // Setting up the brushes
        private static readonly BrushConverter bc = new();
        public static readonly Dictionary<string, Brush> colours = new() {
            ["gray"] = ObjToBrush(bc.ConvertFrom("#3A3A3C")),

            ["blue"] = ObjToBrush(bc.ConvertFrom("#85C0F9")),
            ["orange"] = ObjToBrush(bc.ConvertFrom("#F5793A")),

            ["yellow"] = ObjToBrush(bc.ConvertFrom("#B59F3B")),
            ["green"] = ObjToBrush(bc.ConvertFrom("#538D4E")),

        };

        private static Brush ObjToBrush(object? o) {
            return o switch {
                Brush b => b,
                _ => throw new InvalidCastException()
            };
        }
    }
}
