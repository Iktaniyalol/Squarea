using System.IO;

namespace Server.Net.Packets
{
    class LoginResultPacket : DataPacket
    {
        public Result status;
        public enum Result
        {
            InvalidLoginOrPassword = 0,
            Success = 1,
            Error = 2
        }

        override public byte GetId()
        {
            return LOGIN_RESULT_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write((int) status); //Статус
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            reader.ReadByte(); //ID пакета
            this.status = (Result) reader.ReadInt32(); //Статус
        }
    }
}
