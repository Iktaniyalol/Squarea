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
    }
}
