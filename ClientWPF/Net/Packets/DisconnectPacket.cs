namespace ClientWPF.Net.Packets
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

        public override void Handle(Client client)
        {
            client.Close();
        }
    }
}
