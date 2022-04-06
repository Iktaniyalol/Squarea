using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using Client.Net.Packets;
using System;
using System.Collections.Generic;

namespace Client.Net
{
    public class Network
    {
        //TODO
        Socket serverSocket;
        Client client;
        static Type[] packets = new Type[20];

        public Network(Client client, IPEndPoint ipPort)
        {
            this.client = client;
            RegisterPackets();
        }
        
        public Socket GetServerSocket
        {
            get
            {
                return serverSocket;
            }
        }
        void RegisterPackets()
        {
            packets[DataPacket.REGISTER_PACKET] = typeof(RegisterPacket);
            packets[DataPacket.REGISTER_RESULT_PACKET] = typeof(RegisterResultPacket);
            packets[DataPacket.LOGIN_PACKET] = typeof(LoginPacket);
            packets[DataPacket.LOGIN_RESULT_PACKET] = typeof(LoginResultPacket);
            packets[DataPacket.PING_PACKET] = typeof(PingPacket);
            //TODO
        }

        public static DataPacket GetPacket(byte id)
        {
            return (DataPacket)packets[id].GetConstructor(new Type[] { }).Invoke(null); //Создаем чистый экземпляр пакета по айди
        }

        public Client GetClient
        {
            get
            {
                return client;
            }
        }
    }
}
