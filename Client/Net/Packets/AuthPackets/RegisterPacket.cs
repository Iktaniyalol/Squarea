using System.IO;

namespace Client.Net.Packets
{
    class RegisterPacket : DataPacket
    {
        public string username;
        public string password;

        override public byte GetId()
        {
            return REGISTER_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(username); //Логин регистрирующегося пользователя
            writer.Write(password); //Пароль, зашифрованный SHA256, сервер получает его уже зашифрованным
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            this.username = reader.ReadString(); //Логин регистрирующегося пользователя
            this.password = reader.ReadString(); //Пароль, зашифрованный SHA256
        }
    }
}
