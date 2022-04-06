namespace Client.Net.Packets
{
    class PlayerDisconnectPacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.PLAYER_DISCONNECT_PACKET;
        }

        //TODO
    }
}
