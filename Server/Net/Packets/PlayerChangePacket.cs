namespace Server.Net.Packets
{
    class PlayerChangePacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.PLAYER_CHANGE_PACKET;
        }

        //TODO
    }
}
