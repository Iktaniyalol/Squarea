using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Server.Net;
using Server.Net.Packets;
using System.Drawing;

namespace Server
{
    public class Player
    {
        private PlayerSession session;
        private string name;
        private List<Player> viewers = new List<Player>();
        public double x, y;
        public Color color;

        public Player(PlayerSession session, string name, Color color)
        {
            this.session = session;
            this.name = name;
            this.color = color;
        }
        public PlayerSession Session
        {
            get
            {
                return this.session; // Уродливый код TODO кдал ить назу й   
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<Player> Viewers
        {
            get
            {
                return viewers;
            }
        }


        public void Move(double x, double y, bool isClient = false)
        {
            this.x += x;
            this.y += y;
            if (!isClient)
            {
                PlayerMovePacket playerMovePacket = new PlayerMovePacket();
                playerMovePacket.x = this.x;
                playerMovePacket.y = this.y;
                playerMovePacket.nickname = this.name;
                session.SendPacket(playerMovePacket);
            }
        }

        public void Teleport(double x, double y, bool isClient = false)
        {
            this.x = x;
            this.y = y;
            if (!isClient)
            {
                PlayerMovePacket playerMovePacket = new PlayerMovePacket();
                playerMovePacket.x = this.x;
                playerMovePacket.y = this.y;
                playerMovePacket.nickname = this.name;
                session.SendPacket(playerMovePacket);
            }
        }


        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
    }
}
