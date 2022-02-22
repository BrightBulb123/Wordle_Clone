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

            if (currentRow == 0) {
                if (currentColumn == 0) { TR0C0.Text = character.ToString(); }
                else if (currentColumn == 1) { TR0C1.Text = character.ToString(); }
                else if (currentColumn == 2) { TR0C2.Text = character.ToString(); }
                else if (currentColumn == 3) { TR0C3.Text = character.ToString(); }
                else if (currentColumn == 4) { TR0C4.Text = character.ToString(); }
                currentColumn++;
            }
            else if (currentRow == 1) {
                if (currentColumn == 0) { TR1C0.Text = character.ToString(); }
                else if (currentColumn == 1) { TR1C1.Text = character.ToString(); }
                else if (currentColumn == 2) { TR1C2.Text = character.ToString(); }
                else if (currentColumn == 3) { TR1C3.Text = character.ToString(); }
                else if (currentColumn == 4) { TR1C4.Text = character.ToString(); }
                currentColumn++;
            }
            else if (currentRow == 2) {
                if (currentColumn == 0) { TR2C0.Text = character.ToString(); }
                else if (currentColumn == 1) { TR2C1.Text = character.ToString(); }
                else if (currentColumn == 2) { TR2C2.Text = character.ToString(); }
                else if (currentColumn == 3) { TR2C3.Text = character.ToString(); }
                else if (currentColumn == 4) { TR2C4.Text = character.ToString(); }
                currentColumn++;
            }
            else if (currentRow == 3) {
                if (currentColumn == 0) { TR3C0.Text = character.ToString(); }
                else if (currentColumn == 1) { TR3C1.Text = character.ToString(); }
                else if (currentColumn == 2) { TR3C2.Text = character.ToString(); }
                else if (currentColumn == 3) { TR3C3.Text = character.ToString(); }
                else if (currentColumn == 4) { TR3C4.Text = character.ToString(); }
                currentColumn++;
            }
            else if (currentRow == 4) {
                if (currentColumn == 0) { TR4C0.Text = character.ToString(); }
                else if (currentColumn == 1) { TR4C1.Text = character.ToString(); }
                else if (currentColumn == 2) { TR4C2.Text = character.ToString(); }
                else if (currentColumn == 3) { TR4C3.Text = character.ToString(); }
                else if (currentColumn == 4) { TR4C4.Text = character.ToString(); }
                currentColumn++;
            }
            else if (currentRow == 5) {
                if (currentColumn == 0) { TR5C0.Text = character.ToString(); }
                else if (currentColumn == 1) { TR5C1.Text = character.ToString(); }
                else if (currentColumn == 2) { TR5C2.Text = character.ToString(); }
                else if (currentColumn == 3) { TR5C3.Text = character.ToString(); }
                else if (currentColumn == 4) { TR5C4.Text = character.ToString(); }
                currentColumn++;
            }

            if (currentColumn >= 5) {
                currentRow++;
                currentColumn = 0;
            }
        }
    }
}
