namespace Client.Net.Packets
{
    class PlayerRemovePacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.PLAYER_REMOVE_PACKET;
        }

        //TODO
    }
}
