using System.IO;
using System;

namespace Server.Net.Packets
{
    class PlayerMovePacket : DataPacket
    {

        public double x, y;
        public string nickname;
        override public byte GetId()
        {
            return PLAYER_MOVE_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(nickname);
            writer.Write(x);
            writer.Write(y);
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            nickname = reader.ReadString();
            x = reader.ReadDouble();
            y = reader.ReadDouble();
        }

        public override void Handle(PlayerSession session)
        {
            if (session.player == null)
            {
                Server.Instance.DisconnectSession(session);
                Console.WriteLine("Непредвиденная попытка движения.");
                return;
            }
            session.player.Teleport(x, y, true);
            foreach (Player other in Server.Instance.GetPlayers())
            {
                if (other == session.player) continue;
                if (Math.Abs(other.X - session.player.X) < 320 + 64 || Math.Abs(other.Y - session.player.Y) < 256 + 58)
                {
                    if (!session.player.Viewers.Contains(other)) {
                        session.player.Viewers.Add(other);
                        PlayerSpawnPacket playerSpawnPacket = new PlayerSpawnPacket();
                        playerSpawnPacket.username = other.Name;
                        playerSpawnPacket.x = other.X;
                        playerSpawnPacket.y = other.Y;
                        playerSpawnPacket.color = other.color;
                        session.SendPacket(playerSpawnPacket);
                    } else
                    {
                        other.Session.SendPacket(this);
                    }
                    if (!other.Viewers.Contains(session.player))
                    {
                        other.Viewers.Add(session.player);
                        PlayerSpawnPacket playerSpawnPacket1 = new PlayerSpawnPacket();
                        playerSpawnPacket1.username = session.player.Name;
                        playerSpawnPacket1.x = session.player.X;
                        playerSpawnPacket1.y = session.player.Y;
                        playerSpawnPacket1.color = session.player.color;
                        other.Session.SendPacket(playerSpawnPacket1);
                    }

                } else
                {
                    if (session.player.Viewers.Contains(other))
                    {
                        PlayerRemovePacket playerRemovePacket = new PlayerRemovePacket();
                        playerRemovePacket.username = other.Name;
                        session.SendPacket(playerRemovePacket);
                        session.player.Viewers.Remove(other);
                        PlayerRemovePacket playerRemovePacket1 = new PlayerRemovePacket();
                        playerRemovePacket1.username = session.player.Name;
                        other.Session.SendPacket(playerRemovePacket1);
                        other.Viewers.Remove(other);
                    }
                }
            }
        }
    }
}
