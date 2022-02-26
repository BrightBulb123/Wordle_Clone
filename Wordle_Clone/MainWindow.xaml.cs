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
                currentRow++;
                currentColumn = 0;
            }
        }
    }
}
