using System.Net;
using System.Net.Sockets;
using System.Threading;
using ClientWPF.Net.Packets;
using System;
using System.Collections.Generic;

namespace ClientWPF.Net
{
    public class Network
    {
        Client client;
        TcpClient tcpClient;
        Queue<DataPacket> inbound = new Queue<DataPacket>();
        Queue<DataPacket> outbound = new Queue<DataPacket>();
        IPEndPoint iPEndPointOfServer;
        static Type[] packets = new Type[20];
        public bool connected = false;
        public bool pingAccepted = false;
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
            Thread inboundProcessingThread = new Thread(InboundProcessing);
            inboundProcessingThread.Start();
            threads.Add(inboundProcessingThread);
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
                    Thread.Sleep(20);
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
                            Thread.Sleep(20);
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
                        Thread.Sleep(20);
                        if (networkDestroyed) return;
                    }
                    DataPacket packet;
                    if (!outbound.TryDequeue(out packet)) continue;
                    packet.Encode();
                    byte[] dataToSend = new byte[packet.Data.Length + 4];
                    Array.Copy(BitConverter.GetBytes(packet.Data.Length), 0, dataToSend, 0, 4);
                    Array.Copy(packet.Data, 0, dataToSend, 4, packet.Data.Length);
                    if (!tcpClient.Connected)
                    {
                        outbound.Clear();
                        continue;
                    }
                    tcpClient.Client.Send(dataToSend);
                }
            });
            sendingTcpPacketsThread.Start();
            threads.Add(sendingTcpPacketsThread);
        }
        private void CreateConnectedTcpClient()
        {
            this.tcpClient = new TcpClient();
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

        public void TcpSendPacket(DataPacket packet, bool instantly = false)
        {
            if (instantly)
            {
                packet.Encode();
                byte[] dataToSend = new byte[packet.Data.Length + 4];
                Array.Copy(BitConverter.GetBytes(packet.Data.Length), 0, dataToSend, 0, 4);
                Array.Copy(packet.Data, 0, dataToSend, 4, packet.Data.Length);
                if (!tcpClient.Connected)
                {
                    return;
                }
                tcpClient.Client.Send(dataToSend);
            } else
            {
                packet.Encode();
                outbound.Enqueue(packet);
            }
        }

        public void DestroyNetworkThreads()
        {
            networkDestroyed = true;
            tcpClient.Close();
        }

        private void RegisterPackets()
        {
            packets[DataPacket.REGISTER_PACKET] = typeof(RegisterPacket);
            packets[DataPacket.REGISTER_RESULT_PACKET] = typeof(RegisterResultPacket);
            packets[DataPacket.LOGIN_PACKET] = typeof(LoginPacket);
            packets[DataPacket.LOGIN_RESULT_PACKET] = typeof(LoginResultPacket);
            packets[DataPacket.PING_PACKET] = typeof(PingPacket);
            packets[DataPacket.PLAYER_CONNECT_PACKET] = typeof(PlayerConnectPacket);
            packets[DataPacket.DISCONNECT_PACKET] = typeof(DisconnectPacket);
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
            packets[DataPacket.DISCONNECT_PACKET] = typeof(DisconnectPacket);
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

        public void SetPingReceived()
        {
            pingAccepted = true;
        }

        private void InboundProcessing()
        {
            while (true)
            {
                if (networkDestroyed)
                {
                    return;
                }
                while (inbound.Count < 1)
                {
                    Thread.Sleep(20);
                    if (networkDestroyed)
                    {
                        return;
                    }
                }
                DataPacket dp = inbound.Dequeue();
                dp.Decode();
                dp.Handle(client);
            }
        }
    }
}
