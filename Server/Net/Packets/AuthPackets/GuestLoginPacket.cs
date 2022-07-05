using System;
namespace Server.Net.Packets
{
    class GuestLoginPacket : DataPacket
    {
        override public byte GetId()
        {
            return GUEST_LOGIN_PACKET;
        }

        public override void Encode()
        {
            data = new byte[]{GetId()}; //Мы записываем только айди
        }

        public override void Decode()
        {
            //Пусто
        }

        public override void Handle(PlayerSession session)
        {
            Random random = new Random();
            int numbers = random.Next(0, 1000000);
            while (Server.Instance.GetPlayer("Guest" + numbers) != null)
            {
                numbers = random.Next(0, 1000000);
            }
            GuestLoginResultPacket guestLoginResultPacket = new GuestLoginResultPacket();
            guestLoginResultPacket.playerName = "Guest" + numbers;
            session.SendPacket(guestLoginResultPacket);
            session.isAuthorized = true;
        }
    }
}
