using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            switch(e.Key)
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
                        switch(e.Key)
            {
                case Key.Left:
                case Key.A:
                    {
                        break;
                }
                case Key.Up:
                case Key.W:
                    {
                        break;
                    }
                case Key.Down:
                case Key.S:
                    {
                        break;
                    }
                case Key.Right:
                case Key.D:
                    {
                        break;
                    }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Client.Instance.Network.DestroyNetworkThreads();
            //TODO нормально дисконектить игрока с сервера, посылать пакет
        }
    }
}
