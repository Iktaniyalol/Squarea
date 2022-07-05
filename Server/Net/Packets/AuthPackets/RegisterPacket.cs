using System.IO;
using Server.Data;
using System;

namespace Server.Net.Packets
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
            username = reader.ReadString(); //Логин регистрирующегося пользователя
            password = reader.ReadString(); //Пароль, зашифрованный SHA256
        }

        public override void Handle(PlayerSession session)
        {
            if (Server.Instance.DataBase == null)
            {
                RegisterResultPacket registerResultPacket = new RegisterResultPacket();
                registerResultPacket.status = RegisterResultPacket.Result.Error;
                session.SendPacket(registerResultPacket);
            }
            else
            {
                PlayerRegistration reg = Server.Instance.DataBase.GetPlayerRegistration(username);
                if (reg == null)
                {
                    reg = new PlayerRegistration(username, password, "");
                    Server.Instance.DataBase.InsertPlayerRegistration(reg);
                    RegisterResultPacket registerResultPacket = new RegisterResultPacket();
                    registerResultPacket.status = RegisterResultPacket.Result.Success;
                    session.SendPacket(registerResultPacket);
                }
                else
                {
                    RegisterResultPacket registerResultPacket = new RegisterResultPacket();
                    registerResultPacket.status = RegisterResultPacket.Result.UsernameAlreadyUsed;
                    session.SendPacket(registerResultPacket);
                    session.isAuthorized = true;
                }
            }
        }
    }
}
