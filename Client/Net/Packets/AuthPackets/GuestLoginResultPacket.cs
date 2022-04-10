using System.IO;

namespace Client.Net.Packets
{
    class GuestLoginResultPacket : DataPacket
    {
        public string playerName;

        override public byte GetId()
        {
            return GUEST_LOGIN_RESULT_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(playerName); //Ник, который мы присвоим нашему гостю
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            this.playerName = reader.ReadString(); //Ник гостя
        }

        public override void Handle(Client client)
        {
            //TODO
        }
    }
}
