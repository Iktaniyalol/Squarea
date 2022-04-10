using System.IO;

namespace Server.Net.Packets
{
    class TextPacket : DataPacket
    {
        public Type type;
        public string message = "default";
        public string playerName = "default";
        public enum Type
        {
            Chat = 0,
            Announcement = 1,
            System = 2
        }

        override public byte GetId()
        {
            return DataPacket.TEXT_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write((int) type); //Тип текста
            switch(type)
            {
                case Type.Chat:
                    {
                        writer.Write(playerName); //Ник игрока который написал сообщение
                    }
                    break;
            }

            writer.Write(message); //Сообщение
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            type = (Type) reader.ReadInt32();
            switch (type)
            {
                case Type.Chat:
                    {
                        playerName = reader.ReadString(); //Ник игрока который написал сообщение
                    }
                    break;
            }
            message = reader.ReadString(); //Сообщение
        }

        public override void Handle(PlayerSession session)
        {
            //TODO
        }
    }
}
