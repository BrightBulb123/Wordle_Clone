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

            (int, int) rowColumn = (currentRow, currentColumn);

            switch (rowColumn) {
                case (0, 0):
                    TR0C0.Text = character.ToString();
                    break;
                case (0, 1):
                    TR0C1.Text = character.ToString();
                    break;
                case (0, 2):
                    TR0C2.Text = character.ToString();
                    break;
                case (0, 3):
                    TR0C3.Text = character.ToString();
                    break;
                case (0, 4):
                    TR0C4.Text = character.ToString();
                    break;
                case (1, 0):
                    TR1C0.Text = character.ToString();
                    break;
                case (1, 1):
                    TR1C1.Text = character.ToString();
                    break;
                case (1, 2):
                    TR1C2.Text = character.ToString();
                    break;
                case (1, 3):
                    TR1C3.Text = character.ToString();
                    break;
                case (1, 4):
                    TR1C4.Text = character.ToString();
                    break;
                case (2, 0):
                    TR2C0.Text = character.ToString();
                    break;
                case (2, 1):
                    TR2C1.Text = character.ToString();
                    break;
                case (2, 2):
                    TR2C2.Text = character.ToString();
                    break;
                case (2, 3):
                    TR2C3.Text = character.ToString();
                    break;
                case (2, 4):
                    TR2C4.Text = character.ToString();
                    break;
                case (3, 0):
                    TR3C0.Text = character.ToString();
                    break;
                case (3, 1):
                    TR3C1.Text = character.ToString();
                    break;
                case (3, 2):
                    TR3C2.Text = character.ToString();
                    break;
                case (3, 3):
                    TR3C3.Text = character.ToString();
                    break;
                case (3, 4):
                    TR3C4.Text = character.ToString();
                    break;
                case (4, 0):
                    TR4C0.Text = character.ToString();
                    break;
                case (4, 1):
                    TR4C1.Text = character.ToString();
                    break;
                case (4, 2):
                    TR4C2.Text = character.ToString();
                    break;
                case (4, 3):
                    TR4C3.Text = character.ToString();
                    break;
                case (4, 4):
                    TR4C4.Text = character.ToString();
                    break;
                case (5, 0):
                    TR5C0.Text = character.ToString();
                    break;
                case (5, 1):
                    TR5C1.Text = character.ToString();
                    break;
                case (5, 2):
                    TR5C2.Text = character.ToString();
                    break;
                case (5, 3):
                    TR5C3.Text = character.ToString();
                    break;
                case (5, 4):
                    TR5C4.Text = character.ToString();
                    break;
            }
            currentColumn++;

            if (currentColumn >= 5) {
                currentRow++;
                currentColumn = 0;
            }
        }
    }
}
