using ClientWPF.Graphic;
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
    /// Логика взаимодействия для PreGameWindow.xaml
    /// </summary>
    public partial class PreGameWindow : Window
    {
        WriteableBitmap playerSpriteBitMap;
        WriteableBitmap playerWindowSpriteBitMap;
        String playerName;
        Color color;
        bool isClosedForTransition = false;

        public PreGameWindow(String nickname)
        {
            InitializeComponent();
            Client.Instance.PreGameWindow = this;
            playerName = nickname;
            NicknameLabel.Content = nickname;
            playerWindowSpriteBitMap = ImageTransform.GetWriteableBitmapFromResources("Images/square4.png");
            playerSpriteBitMap = ImageTransform.GetWriteableBitmapFromResources("Images/square2.png");
        }

        private void ColorRedButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(227, 47, 58);
            ChangePlayerSpriteColor(color);
        }

        private void ColorYellowButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(255, 249, 153);
            ChangePlayerSpriteColor(color);
        }

        private void ColorOrangeButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(241, 151, 65);
            ChangePlayerSpriteColor(color);
        }

        private void ColorPinkButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(255, 192, 213);
            ChangePlayerSpriteColor(color);
        }

        private void ColorMagentaButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(193, 115, 177);
            ChangePlayerSpriteColor(color);
        }

        private void ColorPurpleButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(131, 97, 157);
            ChangePlayerSpriteColor(color);
        }

        private void ColorVioletButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(114, 118, 163);
            ChangePlayerSpriteColor(color);
        }

        private void ColorBlueButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(69, 102, 137);
            ChangePlayerSpriteColor(color);
        }

        private void ColorLightBlueButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(140, 187, 229);
            ChangePlayerSpriteColor(color);
        }

        private void ColorCyanButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(1, 173, 185);
            ChangePlayerSpriteColor(color);
        }

        private void ColorGreenButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(50, 127, 57);
            ChangePlayerSpriteColor(color);
        }

        private void ColorLimeButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(132, 199, 122);
            ChangePlayerSpriteColor(color);
        }

        private void ColorGrayButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(201, 196, 192);
            ChangePlayerSpriteColor(color);
        }

        private void ColorWhiteButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(255, 255, 255);
            ChangePlayerSpriteColor(color);
        }

        private void ColorBrownButton_Click(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(93, 75, 61);
            ChangePlayerSpriteColor(color);
        }

        private void ChangePlayerSpriteColor(Color color)
        {
            playerWindowSpriteBitMap = ImageTransform.GetWriteableBitmapFromResources("Images/square4.png");
            playerSpriteBitMap = ImageTransform.GetWriteableBitmapFromResources("Images/square2.png");
            ImageTransform.SetColorToPlayerSprite(color, playerSpriteBitMap);
            PlayerSpriteImage.Source = ImageTransform.SetColorToPlayerSprite(color, playerWindowSpriteBitMap);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            isClosedForTransition = true;
            GameWindow window = new GameWindow();
            Player player = new Player(playerName, new PlayerSprite(window.ImagePlayer, playerSpriteBitMap), color);
            Client.Instance.CreateGame(window, player);
            //Отправлять то что игрок присоединился на сервер
            window.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosedForTransition) Client.Instance.Network.DestroyNetworkThreads();
        }

        public void CreateGameWindow()
        {

        }
    }
}
