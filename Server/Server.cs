using System;
using System.Net;
using Server.Net;
using Server.Data;
using System.Collections.Generic;
using Server.Net.Packets;

namespace Server
{
    public class Server
    {
        IPAddress IP = IPAddress.Parse("127.0.0.1");
        static Server instance;
        int port = 27767; // порт для приема входящих запросов
        ushort maxPlayers = 30; //максимальное количество игроков, может быть в будущем настроено через файл настройки сервера.
        List<Player> players;
        DataBase dataBase;
        Network network;

        public Server()
        {
            Server.instance = this;
            this.players = new List<Player>(maxPlayers); //Создаем массив игроков
            this.dataBase = new DataBase(@"URI=file://players.db"); //Инициализируем базу данных
            this.dataBase.CreateRegistrationTable();
            //TODO различные загрузки
            // Подключаем сервер к сети
            this.network = new Network(this, new IPEndPoint(IP, port), 1000);
            Console.WriteLine("Сервер запущен.");
        }

        public Network Network
        {
            get
            {
                return network;
            }
        }

        public DataBase DataBase
        {
            get
            {
                return dataBase;
            }
        }

        public static Server Instance
        {
            get
            {
                return instance;
            }
        }

        public void sendPacketTo(DataPacket packet, List<Player> players, bool instantly = false)
        {
            foreach (Player player in players)
            {
                player.Session.SendPacket(packet, instantly);
            }
        }

        public void sendPacketTo(DataPacket packet, Player[] players, bool instantly = false)
        {
            foreach(Player player in players)
            {
                player.Session.SendPacket(packet, instantly);
            }
        }

        public void sendPacketTo(DataPacket packet, Player player, bool instantly = false)
        {
            player.Session.SendPacket(packet, instantly);
        }

        public Player GetPlayer(String name)
        {
            foreach (Player player in players)
            {
                if (player.Name == name) return player;
            }
            return null;
        }

        public void RemovePlayer(Player player)
        {
            players.Remove(player);
        }

        public void RemovePlayer(String name)
        {
            Player remove = null;
            foreach (Player player in players)
            {
                if (player.Name == name) remove = player;
            }
            players.Remove(remove);
        }


        public void DisconnectPlayer(Player player)
        {
            DisconnectSession(player.Session);
        }

        public void DisconnectSession(PlayerSession session)
        {
            session.SendPacket(new DisconnectPacket(), true);
            session.StopSession();
            if (session.player != null)
            {
                foreach (Player viewer in session.player.Viewers)
                {
                    viewer.Session.SendPacket(new PlayerRemovePacket()); //TODO Данные
                }
                RemovePlayer(session.player);
            }
        }
    }
}
