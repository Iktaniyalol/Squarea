using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Server.Net.Packets;
using System.Threading;

namespace Server.Net
{
    public class Network
    {
        Socket socket;
        ushort maxConnectionsCount;
        ushort countOfConnections = 0;
        static Type[] packets = new Type[20];
        TcpListener listener;
        List<EndPoint> sessions;
        Server server;
        public Network(Server server, IPEndPoint ipPort, ushort maxConnectionsCount)
        {
            this.server = server;
            this.maxConnectionsCount = maxConnectionsCount;
            RegisterPackets();
            this.listener = new TcpListener(ipPort);
            ListenForNewConnections();
        }

        private void ListenForNewConnections() //Поток прослушивания подключений
        {
            listener.Start();
            Thread receiveConnectionThread = new Thread(() =>
            {
                while (true)
                {
                    TcpClient newConnection = listener.AcceptTcpClient();
                    Thread processNewConnection = new Thread(ProcNewConFunc);
                    processNewConnection.Start(newConnection);
                }
            });
            receiveConnectionThread.Start();

        }
        public ushort GetCountOfConnections
        {
            get
            {
                return countOfConnections;
            }
        }
        public ushort GetMaxCountOfConnections
        {
            get
            {
                return maxConnectionsCount;
            }
        }

        public Socket GetSocket
        {
            get
            {
                return socket;
            }
        }
        void RegisterPackets()
        {
            packets[DataPacket.REGISTER_PACKET] = typeof(RegisterPacket);
            packets[DataPacket.REGISTER_RESULT_PACKET] = typeof(RegisterResultPacket);
            packets[DataPacket.LOGIN_PACKET] = typeof(LoginPacket);
            packets[DataPacket.LOGIN_RESULT_PACKET] = typeof(LoginResultPacket);
            //TODO Остальные пакеты
        }

        public static DataPacket GetPacket(byte id)
        {
            return (DataPacket)packets[id].GetConstructor(new Type[] { }).Invoke(null); //Создаем чистый экземпляр пакета по айди
        }

        static void ProcNewConFunc(object newConObject)
        {
            TcpClient newCon = (TcpClient)newConObject;
            Console.WriteLine("Новое подключение — " + newCon.Client.RemoteEndPoint.Serialize().ToString());
            Socket conSocket = newCon.Client;
            byte[] packetSizeData = new byte[4];
            try
            {
                int timeoutCounter = 0;
                while(conSocket.Available==0 && timeoutCounter < 50)
                {
                    timeoutCounter += 1;
                    Thread.Sleep(100);
                }
                if (conSocket.Available < 5)
                {
                    Console.WriteLine(newCon.Client.RemoteEndPoint.Serialize().ToString() + ": размер пришедших данных меньше необходимого для пакета, сброс подключения.");
                    newCon.Close();
                    return;
                }
                int readCount = conSocket.Receive(packetSizeData);
                if (readCount < 4)
                {
                    Console.WriteLine(newCon.Client.RemoteEndPoint.Serialize().ToString() + ": размер пришедших данных меньше необходимого для переменной размера пакета, сброс подключения.");
                    newCon.Close();
                    return;
                }
                int packetSize = BitConverter.ToInt32(packetSizeData,0);
                if(packetSize > conSocket.Available)
                {
                    Console.WriteLine(newCon.Client.RemoteEndPoint.Serialize().ToString() + ": пришедшее количество данных меньше, чем указано в пакете, сброс подключения");
                    newCon.Close();
                    return;
                }
                byte[] packetData = new byte[packetSize];
                readCount = conSocket.Receive(packetData);
                if (readCount != packetSize)
                {
                    Console.WriteLine(newCon.Client.RemoteEndPoint.Serialize().ToString() + ": возникла непредвиденная ошибка: количество считанных данных меньше размера пакета; сброс подключения.");
                    newCon.Close();
                    return;
                }
                byte packetId = packetData[0];
                DataPacket packet = GetPacket(packetId);
                packet.Data = new byte[packetData.Length - 1];
                Array.Copy(packetData, 1, packet.Data, 0, packet.Data.Length);
                packet.Decode();
                //TODO анализ пришедшего пакета
                Console.WriteLine(newCon.Client.RemoteEndPoint.Serialize().ToString() + ": " + packet.GetType().Name + " успешно получен");
            }
            catch (Exception e)
            {
                Console.WriteLine(newCon.Client.RemoteEndPoint.Serialize().ToString() + "\n" + e.StackTrace);
                newCon.Close();
                return;
            }
        }

        public Server GetServer
        {
            get
            {
                return server;
            }
        }
    }


    class PlayerSession
    {
        Socket socket;
        Queue<DataPacket> outbound = new Queue<DataPacket>();
        Player player;
        byte[] buffer;

        public PlayerSession(Socket socket)
        {
            this.socket = socket;
        }

        public void OutboundQueueProcess()
        {
            //TODO Отдельный поток
        }

        public void Ping()
        {
            //TODO Отдельный поток
        }
    }
}
