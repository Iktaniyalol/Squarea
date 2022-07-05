using System.IO;

namespace ClientWPF.Net.Packets
{
    class RegisterResultPacket : DataPacket
    {
        public Result status;

        public enum Result
        {
            UsernameAlreadyUsed = 0,
            Success = 1,
            Error = 2
        }
        override public byte GetId()
        {
            return REGISTER_RESULT_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write((int)status); //Статус
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            this.status = (Result)reader.ReadInt32(); //Статус
        }

        public override void Handle(Client client)
        {
            if (Client.Instance.MainWindow == null) return; //Вдруг такое получится
            Client.Instance.MainWindow.ResultAuthPacket = this;
        }
    }
}
