using System.IO;
using System.Windows.Controls;
using System.Windows;

namespace ClientWPF.Net.Packets
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
            if (nickname == client.Game.Player.Name)
            {
                client.Game.Player.Teleport(x, y);
            } else
            {
                foreach(Player player in client.Game.Player.Viewers)
                {
                    if (player.Name == nickname)
                    {
                        player.Move(this.x - player.X, this.y - player.Y, true);
                        Application.Current.Dispatcher.Invoke(new System.Action(() =>
                        {
                        double x = 2 * (this.x - client.Game.Player.X) - player.playerInGameImage.Width / 2;
                        double y = 2 * (this.y - client.Game.Player.Y) - player.playerInGameImage.Height / 2;
                        Canvas.SetLeft(player.playerInGameImage, x);
                        Canvas.SetTop(player.playerInGameImage, y);
                        Canvas.SetTop(player.playerNicknameLabel, y - 50 + player.playerInGameImage.Height / 2);
                        Client.Instance.GameWindow.UpdateLayout();
                        Canvas.SetLeft(player.playerNicknameLabel, x - player.playerNicknameLabel.ActualWidth / 2 + player.playerInGameImage.Width / 2);
                        }));
                        return;
                    }
                }
            }
        }
    }
}
