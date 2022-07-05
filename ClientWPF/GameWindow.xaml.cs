using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System;
using System.Windows.Controls;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        DispatcherTimer timer;
        public GameWindow()
        {
            InitializeComponent();
            Client.Instance.GameWindow = this;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (!Client.Instance.Network.connected)
            {
                MessageBox.Show("Сервер недоступен!");
                Client.Instance.Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.A:
                    {
                        Client.Instance.Game.Player.Move(-1, 0);
                        updatePlayersPosAround(Client.Instance.Game.Player);
                        break;
                    }
                case Key.Up:
                case Key.W:
                    {
                        Client.Instance.Game.Player.Move(0, -1);
                        updatePlayersPosAround(Client.Instance.Game.Player);
                        break;
                    }
                case Key.Down:
                case Key.S:
                    {
                        Client.Instance.Game.Player.Move(0, 1);
                        updatePlayersPosAround(Client.Instance.Game.Player);
                        break;
                    }
                case Key.Right:
                case Key.D:
                    {
                        Client.Instance.Game.Player.Move(1, 0);
                        updatePlayersPosAround(Client.Instance.Game.Player);
                        break;
                    }
            }

        }

        private void updatePlayersPosAround(Player player)
        {
            foreach(Player player1 in player.Viewers)
            {
                double x = 2 * (player1.X - player.X) - player1.playerInGameImage.Width / 2;
                double y = 2 * (player1.Y - player.Y) - player1.playerInGameImage.Height / 2;
                Canvas.SetLeft(player1.playerInGameImage, x);
                Canvas.SetTop(player1.playerInGameImage, y);
                Canvas.SetTop(player1.playerNicknameLabel, y - 50 + player1.playerInGameImage.Height / 2);
                Client.Instance.GameWindow.UpdateLayout();
                Canvas.SetLeft(player1.playerNicknameLabel, x - player1.playerNicknameLabel.ActualWidth / 2 + player1.playerInGameImage.Width / 2);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.A:
                    {
                        Client.Instance.Game.Player.StopMove();
                        break;
                    }
                case Key.Up:
                case Key.W:
                    {
                        Client.Instance.Game.Player.StopMove();
                        break;
                    }
                case Key.Down:
                case Key.S:
                    {
                        Client.Instance.Game.Player.StopMove();
                        break;
                    }
                case Key.Right:
                case Key.D:
                    {
                        Client.Instance.Game.Player.StopMove();
                        break;
                    }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Client.Instance.DisconnectFromServer();
            Client.Instance.Close();
        }
    }
}
