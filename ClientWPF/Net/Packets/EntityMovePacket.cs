namespace ClientWPF.Net.Packets
{
    class EntityMovePacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.ENTITY_MOVE_PACKET;
        }
        //TODO
    }
}
