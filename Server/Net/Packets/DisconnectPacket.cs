using System;
using System.IO;
using System.Drawing;

namespace Server.Net.Packets
{
    class DisconnectPacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.DISCONNECT_PACKET;
        }

        public override void Encode()
        {
            data = new byte[] { GetId() }; //Мы записываем только айди
        }

        public override void Decode()
        {
            //Пусто
        }

        public override void Handle(PlayerSession session)
        {
            //Если пришел пакет от клиента
            if (session.player != null)
            {
                foreach (Player viewer in session.player.Viewers)
                {
                    viewer.Session.SendPacket(new PlayerRemovePacket()); //TODO
                }
                Server.Instance.RemovePlayer(session.player);
            }
            session.StopSession();
        }
    }
}
