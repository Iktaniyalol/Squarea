using System.IO;

namespace Server.Net.Packets
{
    class PingPacket : DataPacket
    {
        public bool isCallback = false;
        override public byte GetId()
        {
            return DataPacket.PING_PACKET;
        }

        override public void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(isCallback); //От кого пакет
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            this.isCallback = reader.ReadBoolean(); //От кого пакет
        }

        public override void Handle(PlayerSession session)
        {
            if (!isCallback)
            {
                PingPacket ping = new PingPacket();
                ping.isCallback = true;
                session.SendPacket(ping);
            }
        }
    }
}
