using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        
        static readonly Random rnd = new();
        private static readonly string solPath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
        static readonly int lineCount = File.ReadLines(solPath + @"\wordsList.txt").Count();
        int currentRow = 0;
        int currentColumn = 0;
        char[] word = new char[5];
        char[] guess = new char[5];

        public MainWindow() {
            InitializeComponent();
        }

        private void Main_Loaded(object sender, RoutedEventArgs e) {
            word = (File.ReadLines(solPath + @"\wordsList.txt").Skip(rnd.Next(0, lineCount)).Take(1).First()).ToCharArray();
            lblDebug.Content = new string(word);  // Debug Purposes
        }

        private void Main_KeyDown(object sender, KeyEventArgs e) {
            // Enter - Move onto the next row/line
            if (e.Key == Key.Enter) {
                if (currentColumn >= 5) {
                    GuessChecker(currentRow);
                    currentRow++;
                    currentColumn = 0;
                }
            }
            // Backspace - Deleting the character in the TextBox in the (previous) column; edge cases handled
            else if (e.Key == Key.Back) {
                if (currentColumn >= 5) { currentColumn = 4; }
                else if (currentColumn <= 0) { currentColumn = 0; }
                else { currentColumn--; }

                DeleteChar();
            }
            // If not past the last row and not Enter or Backspace pressed
            else if (currentRow <= 5) {
                if ((byte)e.Key <= 69 && (byte)e.Key >= 44) {
                    FillBox(e.Key.ToString()[0]);
                }
            }
        }

        private void FillBox(char character) {
            object txt = Guesses.FindName($"TR{currentRow}C{currentColumn}");
            if (txt is TextBlock txtBlock) {
                txtBlock.Text = character.ToString();
            }
            if (currentColumn <= 5) {
                currentColumn++;
            }
        }

        private void DeleteChar() {
            object txt = Guesses.FindName($"TR{currentRow}C{currentColumn}");
            if (txt is TextBlock txtBlock) {
                txtBlock.Text = "";
            }
        }

        /// <summary>
        /// Changes the background of the TextBlock(s) of the specified row
        /// </summary>
        /// <param name="row">The row whose TextBlock(s) will have their background(s) changed</param>

        private static void ChangeBackground(Border bor, string result) {
            if (result == "correct") {
                bor.Background = Colours.colours["orange"];
            }
            else if (result == "wrong spot") {
                bor.Background = Colours.colours["blue"];
            }
            else {
                bor.Background = Colours.colours["gray"];
            }
            bor.BorderThickness = new Thickness(0);
        }

        private void GuessChecker(int row) {
            for (int i = 0; i < 5; i++) {
                Border bor = (Border)Guesses.FindName($"BR{row}C{i}");
                TextBlock txtBlock = (TextBlock)bor.FindName($"TR{row}C{i}");
                char letter = Convert.ToChar(txtBlock.Text);
                string result;
                if (letter == word[i]) {
                    result = "correct";
                }
                else if (word.Contains(letter)) {
                    result = "wrong spot";
                }
                else {
                    result = "wrong";
                }
                guess[i] = letter;

                ChangeBackground(bor, result);
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
