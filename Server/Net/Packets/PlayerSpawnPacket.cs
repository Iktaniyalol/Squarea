using System;
using System.IO;
using System.Drawing;

namespace Server.Net.Packets
{
    class PlayerSpawnPacket : DataPacket
    {
        public String username;
        public Color color;
        public double x, y;

        override public byte GetId()
        {
            return DataPacket.PLAYER_SPAWN_PACKET;
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
            writer.Write(x);
            writer.Write(y);
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
            this.color = Color.FromArgb(r, g, b);
            this.x = reader.ReadDouble();
            this.y = reader.ReadDouble();
        }
    }
}
