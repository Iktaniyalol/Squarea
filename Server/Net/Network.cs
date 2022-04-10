﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Server.Net.Packets;
using System.Threading;

namespace Server.Net
{
    public class Network
    {
        public static bool networkDestroyed = false;
        ushort maxConnectionsCount;
        ushort countOfConnections = 0;
        static Type[] packets = new Type[20];
        TcpListener listener;
        Server server;
        static List<PlayerSession> sessions = new List<PlayerSession>();
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

        void RegisterPackets()
        {
            packets[DataPacket.REGISTER_PACKET] = typeof(RegisterPacket);
            packets[DataPacket.REGISTER_RESULT_PACKET] = typeof(RegisterResultPacket);
            packets[DataPacket.LOGIN_PACKET] = typeof(LoginPacket);
            packets[DataPacket.LOGIN_RESULT_PACKET] = typeof(LoginResultPacket);
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

        static void ProcNewConFunc(object newConObject)
        {
            TcpClient newCon = (TcpClient)newConObject;
            Console.WriteLine("Новое подключение — " + newCon.Client.RemoteEndPoint.Serialize().ToString());
            sessions.Add(new PlayerSession(newCon));
        }

        public Server Server
        {
            get
            {
                return server;
            }
        }
    }


    public class PlayerSession
    {
        bool stopSession = false;
        TcpClient tcpClient;
        Queue<DataPacket> inbound = new Queue<DataPacket>();
        Queue<DataPacket> outbound = new Queue<DataPacket>();
        Player player;

        public void StopSession()
        {
            stopSession = true;
        }

        public PlayerSession(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            Thread listeningThread = new Thread(listeningFunc);
            listeningThread.Start();
            Thread sendingThread = new Thread(OutboundQueueProcess);
            sendingThread.Start();
            Thread inboundProcessingThread = new Thread(InboundProcessing);
            inboundProcessingThread.Start();
        }
        private void listeningFunc()
        {
            byte[] packetSizeData = new byte[4];
            while (true)
            {

                if (Network.networkDestroyed || stopSession)
                {
                    CloseSession();
                    return;
                }
                if (tcpClient == null || !tcpClient.Connected)
                {
                    StopSession();
                    CloseSession();
                    return;
                }
                try
                {
                    while (tcpClient.Client.Available == 0)
                    {
                        if (Network.networkDestroyed || stopSession)
                        {
                            CloseSession();
                            return;
                        }
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
        }
        private void RecievePacket(byte[] packetData)
        {
            byte packetId = packetData[0];
            DataPacket packet = Network.GetPacket(packetId);
            packet.Data = new byte[packetData.Length - 1];
            Array.Copy(packetData, 1, packet.Data, 0, packet.Data.Length);
            //packet.Decode();
            inbound.Enqueue(packet);
        }
        private void CloseSession()
        {
            //TODO Метод завершения сессии
        }
        public void InboundProcessing()
        {
            while (true)
            {
                if (Network.networkDestroyed || stopSession)
                {
                    CloseSession();
                    return;
                }
                while (inbound.Count < 1)
                {
                    if (Network.networkDestroyed || stopSession)
                    {
                        CloseSession();
                        return;
                    }
                }
                DataPacket dp = inbound.Dequeue();
                dp.Decode();
                dp.Handle(this);
            }
        }
        public void OutboundQueueProcess()
        {
            while (true)
            {
                if (Network.networkDestroyed || stopSession)
                {
                    CloseSession();
                    return;
                }
                while (outbound.Count < 1)
                {
                    if (Network.networkDestroyed || stopSession)
                    {
                        CloseSession();
                        return;
                    }
                }
                if (tcpClient == null || !tcpClient.Connected)
                {
                    StopSession();
                    CloseSession();
                    return;
                }
                DataPacket packet = (DataPacket)outbound.Dequeue();
                packet.Encode();
                byte[] dataToSend = new byte[packet.Data.Length + 4];
                Array.Copy(BitConverter.GetBytes(packet.Data.Length), 0, dataToSend, 0, 4);
                Array.Copy(packet.Data, 0, dataToSend, 4, packet.Data.Length);
                tcpClient.Client.Send(dataToSend);
            }
        }
        public void SendPacket(DataPacket dp)
        {
            outbound.Enqueue(dp);
        }

        public void Ping()
        {
            //TODO Отдельный поток
        }
    }
}
