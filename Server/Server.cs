using System;
using System.Net;
using Server.Net;

namespace Server
{
    public class Server
    {
        IPAddress IP = IPAddress.Parse("127.0.0.1");
        int port = 27767; // порт для приема входящих запросов
        ushort maxPlayers = 30; //максимальное количество игроков, может быть в будущем настроено через файл настройки сервера.
        Player[] players;
        DataBase dataBase;
        Network network;

        public Server()
        {
            this.players = new Player[maxPlayers]; //Создаем массив игроков
            this.dataBase = new DataBase(@"URI=file://players.db"); //Инициализируем базу данных
            this.dataBase.CreateRegistrationTable();
            Console.WriteLine(dataBase.GetPlayerRegistration("iktaniyalol").password);
            //TODO различные загрузки
            // Подключаем сервер к сети
            this.network = new Network(this, new IPEndPoint(IP, port), maxPlayers);
            Console.WriteLine("Сервер запущен.");
        }

        public Network GetNetwork
        {
            get
            {
                return network;
            }
        }

        public DataBase GetDataBase
        {
            get
            {
                return dataBase;
            }
        }
    }
}
