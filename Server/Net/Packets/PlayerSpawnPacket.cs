using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Net.Packets
{
    class PlayerSpawnPacket : DataPacket
    {
        override public byte GetId()
        {
            return DataPacket.PLAYER_SPAWN_PACKET;
        }

        //TODO
    }
}
