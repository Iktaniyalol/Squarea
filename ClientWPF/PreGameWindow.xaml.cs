using ClientWPF.Graphic;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для PreGameWindow.xaml
    /// </summary>
    public partial class PreGameWindow : Window
    {
        PlayerSprite64 playerSprite64;
        PlayerSprite128 playerSprite128;
        String playerName;
        Color color;
        bool isClosedForTransition = false;
        SpriteState spriteState = SpriteState.STANDING_DOWN;

        public PreGameWindow(String nickname)
        {
            InitializeComponent();
            Client.Instance.PreGameWindow = this;
            playerName = nickname;
            NicknameLabel.Content = nickname;
            WriteableBitmap playerSprite64BitMap = ImageTransform.GetWriteableBitmapFromResources("Images/square2.png");
            WriteableBitmap playerSprite128BitMap = ImageTransform.GetWriteableBitmapFromResources("Images/square4.png");
            WriteableBitmap playerSpriteJump64BitMap = ImageTransform.GetWriteableBitmapFromResources("Images/square2.png");
            WriteableBitmap playerSpriteJump128BitMap = ImageTransform.GetWriteableBitmapFromResources("Images/square4.png");
            playerSprite64 = new PlayerSprite64(playerSprite64BitMap, playerSpriteJump64BitMap, null); ;
            playerSprite128 = new PlayerSprite128(playerSprite128BitMap, playerSpriteJump128BitMap, null);
        }

        private void ColorRedButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(227, 47, 58);
            ChangePlayerSpritesColor(color);
        }

        private void ColorYellowButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(255, 249, 153);
            ChangePlayerSpritesColor(color);
        }

        private void ColorOrangeButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(241, 151, 65);
            ChangePlayerSpritesColor(color);
        }

        private void ColorPinkButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(255, 192, 213);
            ChangePlayerSpritesColor(color);
        }

        private void ColorMagentaButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(193, 115, 177);
            ChangePlayerSpritesColor(color);
        }

        private void ColorPurpleButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(131, 97, 157);
            ChangePlayerSpritesColor(color);
        }

        private void ColorVioletButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(114, 118, 163);
            ChangePlayerSpritesColor(color);
        }

        private void ColorBlueButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(69, 102, 137);
            ChangePlayerSpritesColor(color);
        }

        private void ColorLightBlueButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(140, 187, 229);
            ChangePlayerSpritesColor(color);
        }

        private void ColorCyanButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(1, 173, 185);
            ChangePlayerSpritesColor(color);
        }

        private void ColorGreenButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(50, 127, 57);
            ChangePlayerSpritesColor(color);
        }

        private void ColorLimeButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(132, 199, 122);
            ChangePlayerSpritesColor(color);
        }

        private void ColorGrayButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(201, 196, 192);
            ChangePlayerSpritesColor(color);
        }

        private void ColorWhiteButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(255, 255, 255);
            ChangePlayerSpritesColor(color);
        }

        private void ColorBrownButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(93, 75, 61);
            ChangePlayerSpritesColor(color);
        }

        private void ChangePlayerSpritesColor(Color color)
        {
            playerSprite64.ChangeColor(color);
            playerSprite128.ChangeColor(color);
            PlayerSpriteImage.Source = playerSprite128.downFrame.FrameBitMap;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            isClosedForTransition = true;
            GameWindow window = new GameWindow();
            Player player = new Player(playerName, window.ImagePlayer, playerSprite64, playerSprite128, color);
            Client.Instance.CreateGame(window, player);
            //Отправлять то что игрок присоединился на сервер
            window.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosedForTransition)
            {
                Client.Instance.DisconnectFromServer();
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
                        spriteState = SpriteState.STANDING_LEFT;
                        PlayerSpriteImage.Source = playerSprite128.leftFrame.FrameBitMap;
                        break;
                    }
                case Key.Up:
                case Key.W:
                    {
                        spriteState = SpriteState.STANDING_UP;
                        PlayerSpriteImage.Source = playerSprite128.upFrame.FrameBitMap;
                        break;
                    }
                case Key.Down:
                case Key.S:
                    {
                        spriteState = SpriteState.STANDING_DOWN;
                        PlayerSpriteImage.Source = playerSprite128.downFrame.FrameBitMap;
                        break;
                    }
                case Key.Right:
                case Key.D:
                    {
                        spriteState = SpriteState.STANDING_RIGHT;
                        PlayerSpriteImage.Source = playerSprite128.rightFrame.FrameBitMap;
                        break;
                    }
            }
        }

        public enum SpriteState
        {
            STANDING_LEFT,
            STANDING_RIGHT,
            STANDING_UP,
            STANDING_DOWN,
            JUMP_DOWN
        }
    }
}
