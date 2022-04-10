namespace Server.Data
{
    public class PlayerRegistration
    {
        public readonly string name;
        public readonly string password;
        public readonly string email;

        public PlayerRegistration(string name, string password, string email)
        {
            this.name = name;
            this.password = password;
            this.email = email;
        }
    }
}
