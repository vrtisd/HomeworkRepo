using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

namespace Homework5.Models
{
    class GameState : INotifyPropertyChanged
    {
        //I'm hoping to track the state of the game using this class
        private int turnNumber;
        private string playerTurnText;
        private List<string> winningCombinations;
        private List<Player> players;
        private Player currentPlayer;

        public GameState()
        {
            turnNumber = 0;
            players = new List<Player> { new Player("X"), new Player("O") };
            currentPlayer = players[turnNumber % 2];
            playerTurnText = currentPlayer.TurnText;

            winningCombinations = new List<string>
            {
                "0,0|0,1|0,2",
                "1,0|1,1|1,2",
                "2,0|2,1|2,2",
                "0,0|1,0|2,0",
                "0,1|1,1|2,1",
                "0,2|1,2|2,2",
                "0,0|1,1|2,2",
                "0,2|1,1|2,0"
            };
        }

        public Player CurrentPlayer
        {
            get => currentPlayer;
            set => currentPlayer = players[turnNumber % 2];
        }

        public int TurnNumber
        {
            get => turnNumber;
            set
            {
                if (!PlayerIsWinner())
                {
                    turnNumber = value;
                    currentPlayer = players[turnNumber % 2];
                    playerTurnText = currentPlayer.TurnText;
                }
            }
        }

        public string PlayerTurnText
        {
            get => playerTurnText;
            set
            {
                if (playerTurnText != value)
                {
                    playerTurnText = value;
                    OnPropertyChanged("PlayerTurnText");
                }
            }
        }

        public Player this[string token]
        {
            get => players.FirstOrDefault(p => p.PlayerToken.ToString() == token);
        }

        public Player this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                    case 1:
                        return players[index];
                    default:
                        return null;
                }
            }
        }

        public Player this[Models.PlayerToken token]
        {
            get => players.FirstOrDefault(p => p.PlayerToken == token);
        }

        public bool PlayerIsWinner()
        {                        
            foreach (string winningCombination in winningCombinations)
            {
                string[] coordinates = winningCombination.Split('|');
                
                if (currentPlayer.SelectedButtons.IndexOf(coordinates[0]) != -1 &&
                    currentPlayer.SelectedButtons.IndexOf(coordinates[1]) != -1 &&
                    currentPlayer.SelectedButtons.IndexOf(coordinates[2]) != -1)
                {
                    return true;
                }

            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
