using System.IO;

namespace Client.Net.Packets
{
    class LoginPacket : DataPacket
    {
        public string username;
        public string password;

        override public byte GetId()
        {
            return LOGIN_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(username); //Логин авторизующегося пользователя
            writer.Write(password); //Пароль, зашифрованный SHA256, сервер получает его уже зашифрованным
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            this.username = reader.ReadString(); //Логин авторизующегося пользоваться
            this.password = reader.ReadString(); //Пароль, зашифрованный SHA256
        }

        public override void Handle(Client client)
        {
            //TODO
        }
    }
}
