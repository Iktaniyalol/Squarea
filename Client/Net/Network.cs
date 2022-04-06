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
        Socket serverSocket;
        Client client;
        TcpClient tcpClient;
        Queue inbound = new Queue();
        Queue outbound = new Queue();
        IPEndPoint iPEndPointOfServer;
        static Type[] packets = new Type[20];
        public bool connected = false;
        bool pingAccepted = false;
        List<Thread> threads = new List<Thread>();
        bool networkDestroyed = false;

        public Network(Client client, IPEndPoint ipPort)
        {
            this.client = client;
            RegisterPackets();
            iPEndPointOfServer = ipPort;
            StartPinging();
            StartRecievingTcpPackets();
            StartTcpSendingPackets();
        }
        private void StartPinging()
        {
            Thread pingingThread = new Thread(() =>
            {
                while (tcpClient == null || !tcpClient.Connected) 
                {
                    if (networkDestroyed) return;
                }
                while (true)
                {
                    pingAccepted = false;
                    PingPacket p = new PingPacket();
                    outbound.Enqueue(p);
                    while (outbound.Contains(p))
                    {
                        if (networkDestroyed) return;
                    }
                    int i = 0;
                    while (!pingAccepted)
                    {
                        if (networkDestroyed) return;
                        if (i > 5)
                            break;
                        Thread.Sleep(1000);
                        i++;
                    }
                    if (i > 5) connected = false;
                    else connected = true;
                }
            });
            pingingThread.Start();
            threads.Add(pingingThread);
        }
        private void StartRecievingTcpPackets() //Выполняется каждые 5 секунд и проверяет подключение к серверу
        {
            Thread recievingTcpPacketsThread = new Thread(() =>
            {
                CreateConnectedTcpClient();
                byte[] packetSizeData = new byte[4];
                while (true)
                {

                    if (networkDestroyed) return;
                    if (tcpClient==null || !tcpClient.Connected)
                    {
                        if (networkDestroyed) return;
                        CreateConnectedTcpClient();
                    }
                    try
                    {
                        while (tcpClient.Client.Available == 0)
                        {
                            if (networkDestroyed) return;
                        }
                        int readCount = tcpClient.Client.Receive(packetSizeData);
                        if (readCount < 4)
                        {
                            //неправильный пакет
                            continue;
                        }
                        int packetSize = BitConverter.ToInt32(packetSizeData, 0);
                        if (packetSize > tcpClient.Client.Available)
                        {
                            //TODO неправильный размер пакета
                            byte[] trash = new byte[tcpClient.Available];
                            tcpClient.Client.Receive(trash);
                            trash = null;
                            continue;
                        }
                        byte[] packetData = new byte[packetSize];
                        readCount = tcpClient.Client.Receive(packetData);
                        if (readCount != packetSize)
                        {
                            //TODO неправильный пакет
                            packetData = null;
                            continue;
                        }
                        RecievePacket(packetData);
                        //Console.WriteLine(newCon.Client.RemoteEndPoint.Serialize().ToString() + ": " + packet.GetType().Name + " успешно получен");
                    }
                    catch { }
                }
            });
            recievingTcpPacketsThread.Start();
            threads.Add(recievingTcpPacketsThread);
        }
        private void StartTcpSendingPackets()
        {
            Thread sendingTcpPacketsThread = new Thread(() =>
            {
                while (true)
                {
                    while (outbound.Count <= 0 || tcpClient==null || !tcpClient.Connected)
                    {
                        if (networkDestroyed) return;
                    }
                    DataPacket packet = (DataPacket)outbound.Dequeue();
                    packet.Encode();
                    byte[] dataToSend = new byte[packet.Data.Length + 4];
                    Array.Copy(BitConverter.GetBytes(packet.Data.Length), 0, dataToSend, 0, 4);
                    Array.Copy(packet.Data, 0, dataToSend, 4, packet.Data.Length);
                    tcpClient.Client.Send(dataToSend);
                }
            });
            sendingTcpPacketsThread.Start();
            threads.Add(sendingTcpPacketsThread);
        }
        private void CreateConnectedTcpClient()
        {
            this.tcpClient = new TcpClient(new IPEndPoint(IPAddress.Any, 27767));
            while (!tcpClient.Connected)
            {
                if (networkDestroyed) return;
                try
                {
                    tcpClient.Connect(iPEndPointOfServer.Address, iPEndPointOfServer.Port);
                }
                catch { }
            }
        }
        private void RecievePacket(byte[] packetData)
        {
            byte packetId = packetData[0];
            DataPacket packet = GetPacket(packetId);
            packet.Data = new byte[packetData.Length - 1];
            Array.Copy(packetData, 1, packet.Data, 0, packet.Data.Length);
            //packet.Decode();
            inbound.Enqueue(packet);
        }

        public void TcpSendPacket(DataPacket packet)
        {
            packet.Encode();
            outbound.Enqueue(packet);
        }
        public void DestroyNetworkThreads()
        {
            networkDestroyed = true;
            tcpClient.Close();
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
            packets[DataPacket.PLAYER_CONNECT_PACKET] = typeof(PlayerConnectPacket);
            packets[DataPacket.PLAYER_DISCONNECT_PACKET] = typeof(PlayerDisconnectPacket);
            packets[DataPacket.PLAYER_SPAWN_PACKET] = typeof(PlayerSpawnPacket);
            packets[DataPacket.TEXT_PACKET] = typeof(TextPacket);
            packets[DataPacket.PLAYER_MOVE_PACKET] = typeof(PlayerMovePacket);
            packets[DataPacket.PLAYER_CHANGE_PACKET] = typeof(PlayerChangePacket);
            packets[DataPacket.PLAYER_REMOVE_PACKET] = typeof(PlayerRemovePacket);
            packets[DataPacket.ENTITY_SPAWN_PACKET] = typeof(EntitySpawnPacket);
            packets[DataPacket.ENTITY_MOVE_PACKET] = typeof(EntityMovePacket);
            packets[DataPacket.ENTITY_CHANGE_PACKET] = typeof(EntityChangePacket);
            packets[DataPacket.ENTITY_REMOVE_PACKET] = typeof(EntityRemovePacket);
            packets[DataPacket.GUEST_LOGIN_PACKET] = typeof(GuestLoginPacket);
            packets[DataPacket.GUEST_LOGIN_RESULT_PACKET] = typeof(GuestLoginResultPacket);
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
