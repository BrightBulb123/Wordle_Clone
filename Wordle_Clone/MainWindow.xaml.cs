﻿using System;
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
        private static readonly string wordsFileName = @"wordsList.txt";
        static readonly int lineCount = File.ReadLines(wordsFileName).Count();
        int currentRow = 0;
        int currentColumn = 0;
        char[] word = new char[5];
        Dictionary<char, int> wordCharRepetitions = new();
        readonly GuessChar[] guess = new GuessChar[5];
        private static bool gameOver = false;
        private static List<char> wrongLetters = new();
        private static string results = "";
        private static int guessedIn = 0;

        public MainWindow() {
            InitializeComponent();
        }

        private void btnRerollClicked(object sender, RoutedEventArgs e) {
            WordPicker();
        }

        private void Main_Loaded(object sender, RoutedEventArgs e) {
            WordPicker();
        }

        private void WordPicker() {
            word = File.ReadLines(wordsFileName).Skip(rnd.Next(0, lineCount)).Take(1).First().ToCharArray();  // Pick the word that is randomly chosen by a value between 0 and file length.

            wordCharRepetitions = OccurenceCounter(word);

            BorlblMsg.Visibility = Visibility.Hidden;
            lblMsg.Content = new string(word);

            GridClearer();

            results = "";
            guessedIn = 0;
        }

        private void GridClearer() {
            for (int row = 0; row < 6; row++) {
                for (int col = 0; col < 5; col++) {
                    Border bor = (Border)Guesses.FindName($"BR{row}C{col}");
                    TextBlock txtBlock = (TextBlock)bor.FindName($"TR{row}C{col}");

                    txtBlock.Text = string.Empty;
                    bor.Background = Brushes.Black;
                    bor.BorderThickness = new Thickness(3);
                }
            }
            currentColumn = 0;
            currentRow = 0;
            gameOver = false;
            wrongLetters = new();
            lblWrongLetters.Content = "";
        }

        private static bool WordChecker(char[] w) {
            string w2 = new(w);
            return File.ReadLines(wordsFileName).Contains(w2);
        }

        private static Dictionary<char, int> OccurenceCounter(char[] arr) {
            Dictionary<char, int> res = new();

            for (int i = 0; i < arr.Length; i++) {
                if (res.ContainsKey(arr[i])) {
                    res[arr[i]]++;
                }
                else {
                    res[arr[i]] = 1;
                }
            }
            return res;
        }

        private void Main_KeyDown(object sender, KeyEventArgs e) {
            // Enter - Move onto the next row/line
            if (e.Key == Key.Enter) {
                if (gameOver) { WordPicker(); return; }

                if (currentColumn >= 5) {
                    GuessBuilder(currentRow);

                    if (!WordChecker(guess.Select(x => x.value).ToArray())) {
                        return;
                    }

                    wrongLetters.AddRange(guess.Where(l => !l.inWord).ToArray().Select(l => l.value).ToArray());
                    wrongLetters.Sort();
                    wrongLetters = wrongLetters.Distinct().ToList();
                    lblWrongLetters.Content = "Wrong: " + string.Join(", ", wrongLetters.ToArray());

                    GuessChecker();
                    currentRow++;
                    currentColumn = 0;

                    if (currentRow > 5) {
                        BorlblMsg.Visibility = Visibility.Visible;
                        gameOver = true;
                    }
                }
            }

            else if (gameOver) { return; }

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

        private struct GuessChar {
            public char value;
            public int column;  // 0-indexed
            public bool inWord;
            public bool inCorrectColumn;
            public Border bor;
            public bool allChecked;
        }

        /// <summary>
        /// Changes the background of the TextBlock(s) of the specified row
        /// </summary>
        /// <param name="row">The row whose TextBlock(s) will have their background(s) changed</param>

        private static void ChangeBackground(GuessChar letter) {
            Border bor = letter.bor;
            if (letter.inCorrectColumn && !letter.allChecked) {
                bor.Background = Colours.colours["orange"];
                results += "🟧";
            }
            else if (letter.inWord && !letter.inCorrectColumn && !letter.allChecked) {
                bor.Background = Colours.colours["blue"];
                results += "🟦";
            }
            else {
                bor.Background = Colours.colours["gray"];
                results += "⬛";
            }
            bor.BorderThickness = new Thickness(0);
        }

        private void GuessBuilder(int row) {
            for (int i = 0; i < 5; i++) {
                Border bor = (Border)Guesses.FindName($"BR{row}C{i}");
                TextBlock txtBlock = (TextBlock)bor.FindName($"TR{row}C{i}");

                GuessChar letter = new() {
                    value = Convert.ToChar(txtBlock.Text),
                    column = i,
                    bor = bor,
                    allChecked = false
                };

                if (letter.value == word[i]) {
                    letter.inWord = true;
                    letter.inCorrectColumn = true;
                }
                else if (word.Contains(letter.value)) {
                    letter.inWord = true;
                    letter.inCorrectColumn = false;
                }
                else {
                    letter.inWord = false;
                    letter.inCorrectColumn = false;
                }

                guess[i] = letter;
            }
        }

        private void GuessChecker() {
            // Copy for manipulating duplicate occurences in guess to see how many have been checked already
            Dictionary<char, int> wordCharRepetitionsCopy = wordCharRepetitions.ToDictionary(
                    x => x.Key,
                    x => x.Value
                );
            
            // Copy for manipulating, sorted by correctness (inCorrectColumn -> inWord/Neither)
            List<GuessChar> guess2 = guess.Where(l => l.inCorrectColumn).ToList();
            guess2.AddRange(guess.Where(l => !l.inCorrectColumn).ToList());

            for (int i = 0; i < guess2.Count; i++) {
                GuessChar letter = guess2[i];
                try {
                    if (wordCharRepetitionsCopy[letter.value] > 0) { wordCharRepetitionsCopy[letter.value]--; }
                    else { letter.allChecked = true; }
                    ChangeBackground(letter);
                }
                catch (KeyNotFoundException) {
                    ChangeBackground(letter);
                }
            }
            results += "\n";

            if (guess.Select(x => x.value).ToArray().SequenceEqual(word)) { gameOver = true; BorlblMsg.Visibility = Visibility.Visible; guessedIn = currentRow; }
        }

        private void BorlblMsg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (guessedIn != 0) { Clipboard.SetText($"Wordle_Clone {currentRow}/6\n\n" + results + "\nThink you can do better?\nTry Wordle_Clone by downloading it from my latest message (check Le)"); }
            else { Clipboard.SetText("Wordle_Clone X/6\n\n" + results + "\nThink you can do better?\nTry Wordle_Clone by downloading it from my latest message (check Le)"); }
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
