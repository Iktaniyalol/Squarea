using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Server.Net;
using Server.Net.Packets;

namespace Server
{
    public class Player
    {
        private PlayerSession session;
        private string name;
        private List<Player> viewers = new List<Player>();

        public Player(PlayerSession session)
        {
            this.session = session;    
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
    }
}
