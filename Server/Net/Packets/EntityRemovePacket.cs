namespace Server.Net.Packets
{
    class EntityRemovePacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.ENTITY_REMOVE_PACKET;
        }

        //TODO
    }
}
