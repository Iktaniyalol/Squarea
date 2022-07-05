using System.Net;
using ClientWPF.Net;
using ClientWPF.Net.Packets;
using ClientWPF.Graphic;
using System.Windows.Media;
using System.Windows;
using System.Diagnostics;

namespace ClientWPF
{
    public class Client
    {
        static Client instance;
        IPAddress IP = IPAddress.Parse("127.0.0.1"); //Айпи сервера
        int port = 27767; // порт сервера
        

        Network network;
        GameEngine game; //Создается только тогда, когда игрок откроет GameWindow
        GameWindow gameWindow;
        MainWindow mainWindow;
        PreGameWindow preGameWindow;

        public Client()
        {
            Client.instance = this;
            //Подключаем клиент к серверу
            network = new Network(this, new IPEndPoint(IP, port));
        }

        public Network Network
        {
            get
            {
                return network;
            }
        }

        public GameEngine Game
        {
            get
            {
                return game;
            }
        }

        public static Client Instance
        {
            get
            {
                return instance;
            }
        }

        public GameWindow GameWindow
        {
            get
            {
                return gameWindow;
            }
            set
            {
                gameWindow = value;
            }
        }

        public MainWindow MainWindow
        {
            get
            {
                return mainWindow;
            }
            set
            {
                mainWindow  = value;
            }
        }

        public PreGameWindow PreGameWindow
        {
            get
            {
                return preGameWindow;
            }
            set
            {
                preGameWindow = value;
            }
        }

        public void SendPacketToServer(DataPacket packet, bool instantly = false)
        {
            Network.TcpSendPacket(packet, instantly);
        }

        public void CreateGame(GameWindow gameWindow, Player player)
        {
            game = new GameEngine(gameWindow, player, this);
        }

        public void DisconnectFromServer()
        {
            SendPacketToServer(new DisconnectPacket(), true);
        }

        public void Close()
        {
            Close(false);
        }

        public void Close(bool wasServerInitiated)
        {
            if (wasServerInitiated)
            {
                MessageBox.Show("Разорвано соединение с сервером.");
            }

            Client.Instance.Network.DestroyNetworkThreads();
            Application.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }
    }
}
