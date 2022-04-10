namespace Server.Net.Packets
{
    class PlayerMovePacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.PLAYER_MOVE_PACKET;
        }

        //TODO
    }
}
