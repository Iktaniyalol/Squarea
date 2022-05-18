using System;
using System.IO;

namespace Server.Net.Packets
{
    public class PlayerStartGamePacket : DataPacket
    {
        public long levelSeed;
        public double x, y;

        override public byte GetId()
        {
            return PLAYER_START_GAME_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(levelSeed); //TODO
            writer.Write(x);
            writer.Write(y);
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            levelSeed = reader.ReadInt64();
            x = reader.ReadDouble();
            y = reader.ReadDouble();
        }

        public override void Handle(PlayerSession session)
        {
            //Ничего
        }
    }
}
