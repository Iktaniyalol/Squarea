namespace ClientWPF.Net.Packets
{
    class EntitySpawnPacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.ENTITY_SPAWN_PACKET;
        }

        //TODO
    }
}
