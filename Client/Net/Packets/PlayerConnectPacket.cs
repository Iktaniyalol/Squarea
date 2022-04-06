using System;
using System.IO;
using System.Drawing;

namespace Client.Net.Packets
{
    class PlayerConnectPacket : DataPacket
    {
        public String username;
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

        public override void Handle(Client client)
        {
            //TODO
        }
    }
}
