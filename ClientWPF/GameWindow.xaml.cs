using System.Windows;
using System.Windows.Input;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            Client.Instance.GameWindow = this;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.A:
                    {
                        Client.Instance.Game.Player.Move(-1, 0);
                        break;
                    }
                case Key.Up:
                case Key.W:
                    {
                        Client.Instance.Game.Player.Move(0, -1);
                        break;
                    }
                case Key.Down:
                case Key.S:
                    {
                        Client.Instance.Game.Player.Move(0, 1);
                        break;
                    }
                case Key.Right:
                case Key.D:
                    {
                        Client.Instance.Game.Player.Move(1, 0);
                        break;
                    }
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
