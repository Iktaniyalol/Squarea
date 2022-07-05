using System.IO;
using Server.Data;

namespace Server.Net.Packets
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
            username = reader.ReadString(); //Логин авторизующегося пользоваться
            password = reader.ReadString(); //Пароль, зашифрованный SHA256
        }

        public override void Handle(PlayerSession session)
        {
            if (Server.Instance.DataBase == null)
            {
                LoginResultPacket loginResultPacket = new LoginResultPacket();
                loginResultPacket.status = LoginResultPacket.Result.Error;
                session.SendPacket(loginResultPacket);
            }
            else
            {
                PlayerRegistration reg = Server.Instance.DataBase.GetPlayerRegistration(username);
                if (reg == null)
                {
                    LoginResultPacket loginResultPacket = new LoginResultPacket();
                    loginResultPacket.status = LoginResultPacket.Result.InvalidLoginOrPassword;
                    session.SendPacket(loginResultPacket);
                }
                else
                {
                    if (reg.password != password)
                    {
                        LoginResultPacket loginResultPacket = new LoginResultPacket();
                        loginResultPacket.status = LoginResultPacket.Result.InvalidLoginOrPassword;
                        session.SendPacket(loginResultPacket);
                    }
                    else
                    {
                        LoginResultPacket loginResultPacket = new LoginResultPacket();
                        loginResultPacket.status = LoginResultPacket.Result.Success;
                        session.SendPacket(loginResultPacket);
                        session.isAuthorized = true;
                    }
                }
            }
        }
    }
}
