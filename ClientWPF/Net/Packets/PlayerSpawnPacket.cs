using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWPF.Net.Packets
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
