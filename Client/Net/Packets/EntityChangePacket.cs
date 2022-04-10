namespace Client.Net.Packets
{
    class EntityChangePacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.ENTITY_CHANGE_PACKET;
        }
        //TODO
    }
}
