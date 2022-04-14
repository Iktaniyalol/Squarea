namespace ClientWPF.Net.Packets
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
