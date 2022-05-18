using System;
using System.Threading;

namespace ClientWPF
{
    public class GameEngine
    {
        Client client;
        Player player;
        GameWindow gameWindow;
        public int tickPerSecond = 100;
        bool doTick = false;
        TimeSpan tickPeriod = new TimeSpan(100000);
        TimeSpan waitForTickPeriod = new TimeSpan(20000);

        public GameEngine(GameWindow gameWindow, Player player, Client client)
        {
            this.gameWindow = gameWindow;
            this.player = player;
            this.client = client;
            Thread tickProcessThread = new Thread(TickProcess);
            Thread tickingControl = new Thread(TickingControl);
            tickingControl.Start();
            tickProcessThread.Start();
        }

        private void TickProcess()
        {
            while(true)
            {
                while (!doTick) 
                {
                    Thread.Sleep(waitForTickPeriod);
                }
                doTick = false;
                player.Tick();
                foreach(Player player1 in player.Viewers.ToArray())
                {
                    player1.Tick();
                }
            }
        }

        private void TickingControl()
        {
            while (true)
            {
                Thread.Sleep(tickPeriod);
                doTick = true;
            }
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
