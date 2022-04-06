using System.Net;
using Client.Net;
using Client.Net.Packets;

namespace Client
{
    public class Client
    {
        static Client instance;
        IPAddress IP = IPAddress.Parse("127.0.0.1"); //Айпи сервера
        int port = 27767; // порт сервера

        Network network;

        public Client()
        {
            Client.instance = this;
            //Подключаем клиент к серверу
            network = new Network(this, new IPEndPoint(IP, port));
        }

        public Network GetNetwork
        {
            get
            {
                return network;
            }
        }

        public static Client GetInstance
        {
            get
            {
                return instance;
            }
        }

        public void SendPacketToServer(DataPacket packet)
        {
            GetNetwork.TcpSendPacket(packet);
        }
    }
}
