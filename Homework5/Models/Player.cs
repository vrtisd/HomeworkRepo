using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5.Models
{
    public enum PlayerToken { X = 0, O = 1}

    class Player : INotifyPropertyChanged
    {
        //I'm hoping to track player details using this class
        private PlayerToken playerToken;
        private List<string> selectedButtons;
        private string turnText;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Player(PlayerToken token)
        {
            Initialize(token);
        }

        public Player(string token)
        {
            if(PlayerToken.TryParse(token, out PlayerToken result))
            {
                Initialize(result);
            }
        }

        private void Initialize(PlayerToken token)
        {
            playerToken = token;
            selectedButtons = new List<string>();
            turnText = $"{token.ToString()}'s turn";
        }

        public PlayerToken PlayerToken
        {
            get => playerToken;
            set => playerToken = value;
        }

        public List<string> SelectedButtons
        {
            get => selectedButtons;
            set => selectedButtons = value;
        }

        public string TurnText
        {
            get => turnText;
            private set => turnText = value;
        }
    }
}
