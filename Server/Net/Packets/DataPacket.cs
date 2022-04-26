namespace Server.Net.Packets
{
    public abstract class DataPacket
    {
        public const int REGISTER_PACKET = 0;
        public const int REGISTER_RESULT_PACKET = 1;
        public const int LOGIN_PACKET = 2;
        public const int LOGIN_RESULT_PACKET = 3;
        public const int PING_PACKET = 4;
        public const int PLAYER_CONNECT_PACKET = 5;
        public const int DISCONNECT_PACKET = 6;
        public const int PLAYER_SPAWN_PACKET = 7;
        public const int TEXT_PACKET = 8;
        public const int PLAYER_MOVE_PACKET = 9;
        public const int PLAYER_CHANGE_PACKET = 10;
        public const int PLAYER_REMOVE_PACKET = 11;
        public const int ENTITY_SPAWN_PACKET = 12;
        public const int ENTITY_MOVE_PACKET = 13;
        public const int ENTITY_CHANGE_PACKET = 14;
        public const int ENTITY_REMOVE_PACKET = 15;
        public const int GUEST_LOGIN_PACKET = 16;
        public const int GUEST_LOGIN_RESULT_PACKET = 17;
        //TODO Новые пакеты

        protected byte[] data;

        public abstract byte GetId();

        virtual public void Encode()
        {
        }

        virtual public void Decode()
        {
        }

        virtual public void Handle(PlayerSession session)
        {
        }

        public byte[] Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public void Clean()
        {
            data = null;
        }
    }
}
