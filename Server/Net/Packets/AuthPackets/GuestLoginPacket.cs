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
            //TODO
        }
    }
}
