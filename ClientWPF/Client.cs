using System.Net;
using ClientWPF.Net;
using ClientWPF.Net.Packets;
using ClientWPF.Graphic;
using System.Windows.Media;

namespace ClientWPF
{
    public class Client
    {
        static Client instance;
        IPAddress IP = IPAddress.Parse("127.0.0.1"); //Айпи сервера
        int port = 27767; // порт сервера
        

        Network network;
        Game game; //Создается только тогда, когда игрок откроет GameWindow
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

        public Game Game
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

        public void SendPacketToServer(DataPacket packet)
        {
            Network.TcpSendPacket(packet);
        }

        public void CreateGame(GameWindow gameWindow, Player player)
        {
            game = new Game(gameWindow, player, this);
        }
    }
}
