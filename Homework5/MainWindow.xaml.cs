using System;
using System.Collections;
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
using System.Reflection;

namespace Homework5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.GameState gameState;

        public MainWindow()
        {
            InitializeComponent();

            EnableButtons(true);
            ClearButtonContent();
            gameState = new Models.GameState();
            uxTurn.Text = gameState.CurrentPlayer.TurnText;
        }

        private void EnableButtons(bool enabled)
        {
            var buttons = uxGrid.Children.OfType<Button>();

            foreach (var button in buttons)
            {
                button.IsEnabled = enabled;
            }
        }

        private void uxNewGame_Click(object sender, RoutedEventArgs e)
        {
            EnableButtons(true);
            ClearButtonContent();
            gameState = new Models.GameState();
            uxTurn.Text = gameState.CurrentPlayer.TurnText;
        }

        private void ClearButtonContent()
        {
            var buttons = uxGrid.Children.OfType<Button>();

            foreach (var button in buttons)
            {
                button.Content = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button b)
            {
                b.IsEnabled = false;
                b.Content = gameState.CurrentPlayer.PlayerToken.ToString();
                gameState.CurrentPlayer.SelectedButtons.Add(b.Tag.ToString());

                if(!gameState.PlayerIsWinner())
                {
                    gameState.TurnNumber++;
                    uxTurn.Text = gameState.CurrentPlayer.TurnText;
                }
                else
                {
                    uxTurn.Text = $"{gameState.CurrentPlayer.PlayerToken.ToString()} wins!";                    
                    EnableButtons(false);
                }
            }
        }

        private void uxExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
