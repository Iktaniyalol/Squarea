using System.Data.SQLite;
using System.Threading;
using Server.Data;

namespace Server
{
    public class DataBase
    {
        SQLiteConnection sq;

        public DataBase(string path)
        {
            sq = new SQLiteConnection(path);
            sq.Open();

        }

        private void PingDataBase()
        {
            /*Thread pingThread = new Thread(() =>
            {
                while (true)
                {
                    sq.
                    Thread.Sleep(1000*60*5);
                }
            });
            receiveConnectionThread.Start();*/
            //TODO
        }

        public void CreateRegistrationTable()
        {
            SQLiteCommand cmd = new SQLiteCommand(sq);
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'Registrations' (PlayerName varchar(20) PRIMARY KEY, Password varchar(30), Email varchar(255));";
            cmd.ExecuteNonQuery();
        }

        public void InsertPlayerRegistration(PlayerRegistration registration)
        {
            SQLiteCommand cmd = new SQLiteCommand(sq);
            cmd.CommandText = "INSERT INTO 'Registrations'(PlayerName, Password, Email) VALUES(@name, @password, @email);";
            cmd.Parameters.AddWithValue("@name", registration.name);
            cmd.Parameters.AddWithValue("@password", registration.password);
            cmd.Parameters.AddWithValue("@email", registration.email);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public PlayerRegistration GetPlayerRegistration(string name)
        {
            SQLiteCommand cmd = new SQLiteCommand(sq);
            cmd.CommandText = "SELECT * FROM 'Registrations' WHERE PlayerName = @name;";
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Prepare();
            SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                return new PlayerRegistration(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2));
            }
            return null;
        }
    }

}
