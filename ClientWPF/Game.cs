using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWPF
{
    public class Game
    {
        Client client;
        Player player;
        GameWindow gameWindow;

        public Game(GameWindow gameWindow, Player player, Client client)
        {
            this.gameWindow = gameWindow;
            this.player = player;
            this.client = client;
        }

        public Player Player
        {
            get
            {
                return player;
            }
        }

        public Client Client
        {
            get
            {
                return client;
            }
        }

        public GameWindow GameWindow
        {
            get
            {
                return gameWindow;
            }
        }
    }
}
