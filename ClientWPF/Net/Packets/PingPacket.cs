using System.IO;

namespace ClientWPF.Net.Packets
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

        public override void Handle(Client client)
        {
            if (!isCallback)
            {
                PingPacket ping = new PingPacket();
                ping.isCallback = true;
                client.SendPacketToServer(ping);
            } else
            {
                client.Network.SetPingReceived();
            }
        }
    }
}
