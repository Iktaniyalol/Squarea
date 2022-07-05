using System;
using System.IO;
using System.Windows;

namespace ClientWPF.Net.Packets
{
    class PlayerRemovePacket : DataPacket
    {
        public string username;
        override public byte GetId()
        {
            return DataPacket.PLAYER_REMOVE_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(username); //Ник подключающегося пользователя
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            //Считываем ник
            this.username = reader.ReadString();
        }

        public override void Handle(Client client)
        {
            if (client.Game == null)
            {
                return;
            }
            if (client.Game.Player == null)
            {
                return;
            }
            for (int i = client.Game.Player.Viewers.Count - 1; i >= 0; i--)
            {
                Player player = client.Game.Player.Viewers[i];
                if (player.Name == username)
                {
                    Application.Current.Dispatcher.Invoke(new System.Action(() =>
                    {
                        Client.Instance.GameWindow.canvas.Children.Remove(player.playerInGameImage);
                        Client.Instance.GameWindow.canvas.Children.Remove(player.playerNicknameLabel);
                    }));
                    client.Game.Player.Viewers.Remove(player);
                }
            }
        }
    }
}
