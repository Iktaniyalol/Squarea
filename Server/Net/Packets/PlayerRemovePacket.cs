using System;
using System.IO;

namespace Server.Net.Packets
{
    class PlayerRemovePacket : DataPacket
    {
        public string username;
        override public byte GetId()
        {
            return DataPacket.PLAYER_REMOVE_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(username); //Ник подключающегося пользователя
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            //Считываем ник
            this.username = reader.ReadString();
        }

        public override void Handle(PlayerSession session)
        {
            //Ничего
        }
    }
}
