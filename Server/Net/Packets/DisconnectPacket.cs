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
                for (int i = session.player.Viewers.Count - 1; i >= 0; i--)
                {
                    Player viewer = session.player.Viewers[i];
                    PlayerRemovePacket playerRemovePacket = new PlayerRemovePacket();
                    playerRemovePacket.username = session.player.Name;
                    viewer.Session.SendPacket(playerRemovePacket);
                    session.player.Viewers.Remove(viewer);
                }
                Server.Instance.RemovePlayer(session.player);
            }
            session.StopSession();
        }
    }
}
