using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using ClientWPF.Graphic;
using System.Windows;

namespace ClientWPF.Net.Packets
{
    class PlayerSpawnPacket : DataPacket
    {
        public String username;
        public Color color;
        public double x, y;

        override public byte GetId()
        {
            return DataPacket.PLAYER_SPAWN_PACKET;
        }

        public override void Encode()
        {
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            writer.Write(GetId()); //ID пакета
            writer.Write(username); //Ник подключающегося пользователя
            //Далее мы кодируем цвет кубика игрока
            writer.Write(color.R);
            writer.Write(color.G);
            writer.Write(color.B);
            writer.Write(x);
            writer.Write(y);
            data = memory.ToArray();
        }

        public override void Decode()
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(memory);
            //Считываем ник
            this.username = reader.ReadString();
            //Собираем цвет
            byte r = reader.ReadByte();
            byte g = reader.ReadByte();
            byte b = reader.ReadByte();
            this.color = Color.FromRgb(r, g, b);
            this.x = reader.ReadDouble();
            this.y = reader.ReadDouble();
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
            Image playerImage = null;
            Label label = null;
            Application.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                
                playerImage = new Image();
                playerImage.Width = 64;
                playerImage.Height = 58;
                double x = 2 * (this.x - client.Game.Player.X) - playerImage.Width / 2;
                double y = 2 * (this.y - client.Game.Player.Y) - playerImage.Height / 2;
                Canvas.SetLeft(playerImage, x);
                Canvas.SetTop(playerImage, y);
                label = new Label();
                label.Content = username;
                label.FontFamily = new FontFamily("Comic Sans MS");
                label.FontSize = 11;
                Canvas.SetTop(label, y-50 + playerImage.Height / 2);
                Client.Instance.GameWindow.canvas.Children.Add(playerImage);
                Client.Instance.GameWindow.canvas.Children.Add(label);
                Client.Instance.GameWindow.UpdateLayout();
                Canvas.SetLeft(label, x - label.ActualWidth / 2 + playerImage.Width / 2);
                PlayerSprite64 playerSprite64 = (PlayerSprite64)client.Game.Player.Sprite64.Clone();
                PlayerSprite128 playerSprite128 = (PlayerSprite128)client.Game.Player.Sprite128.Clone();
                playerSprite64.ChangeColor(color);
                playerSprite128.ChangeColor(color);
                Player player = new Player(username, label, playerImage, playerSprite64, playerSprite128, color);
                player.X = this.x;
                player.Y = this.y;
                client.Game.Player.Viewers.Add(player);
            }));
        }
    }
}
