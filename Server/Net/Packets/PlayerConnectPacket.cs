using System;
using System.IO;
using System.Drawing;

namespace Server.Net.Packets
{
    class PlayerConnectPacket : DataPacket
    {
        public string username;
        public Color color;

        override public byte GetId()
        {
            return DataPacket.PLAYER_CONNECT_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(username); //Ник подключающегося пользователя
            //Далее мы кодируем цвет кубика игрока
            writer.Write(color.R);
            writer.Write(color.G);
            writer.Write(color.B);
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            //Считываем ник
            this.username = reader.ReadString();
            //Собираем цвет
            byte r = reader.ReadByte();
            byte g = reader.ReadByte();
            byte b = reader.ReadByte();
            this.color = Color.FromArgb(r,g,b);
        }

        public override void Handle(PlayerSession session)
        {
            if (!session.isAuthorized)
            {
                Server.Instance.DisconnectSession(session);
                Console.WriteLine("Попытка неавторизованного входа в игру.");
                return;
            }

            Player player = new Player(session, username, color);
            session.player = player;
            Server.Instance.AddPlayer(player);
            Random rand = new Random();
            PlayerStartGamePacket startGamePacket = new PlayerStartGamePacket();
            startGamePacket.x = rand.Next(-50, 50);
            startGamePacket.y = rand.Next(-50, 50);
            //TODO SEED
            session.SendPacket(startGamePacket);
        }
    }
}
