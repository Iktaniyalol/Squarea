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

        public Server()
        {
            players = new Player[maxPlayers]; //Создаем массив игроков

            //TODO различные загрузки
            // Сеть
            Console.WriteLine("Сервер запущен.");
        }
    }
}
