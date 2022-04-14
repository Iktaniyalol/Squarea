using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows;

namespace ClientWPF.Graphic
{
    public class PlayerSprite
    {
        const int spriteWidth = 64;
        const int spriteHeight = 52;
        CroppedBitmap spriteLeftLeg;
        CroppedBitmap spriteLeft;
        CroppedBitmap spriteRightLeg;
        CroppedBitmap spriteRight;
        CroppedBitmap spriteUpRightLeg;
        CroppedBitmap spriteUpLeftLeg;
        CroppedBitmap spriteUp;
        CroppedBitmap spriteDownRightLeg;
        CroppedBitmap spriteDownLeftLeg;
        CroppedBitmap spriteDown;
        Image imageOnWindow;
        public PlayerSprite(Image image, WriteableBitmap bitmap)
        {
            this.imageOnWindow = image;
            spriteLeftLeg = new CroppedBitmap(bitmap, new Int32Rect(spriteWidth * 9, 0, spriteWidth, spriteHeight));
            spriteLeft = new CroppedBitmap(bitmap, new Int32Rect(spriteWidth * 8 + 1, 0, spriteWidth, spriteHeight));
            spriteRightLeg = new CroppedBitmap(bitmap, new Int32Rect(spriteWidth * 4 + 1, 0, spriteWidth, spriteHeight));
            spriteRight = new CroppedBitmap(bitmap, new Int32Rect(spriteWidth * 3 + 1, 0, spriteWidth, spriteHeight));
            spriteUpRightLeg = new CroppedBitmap(bitmap, new Int32Rect(spriteWidth * 7 + 1, 0, spriteWidth, spriteHeight));
            spriteUpLeftLeg = new CroppedBitmap(bitmap, new Int32Rect(spriteWidth * 6 + 1, 0, spriteWidth, spriteHeight));
            spriteUp = new CroppedBitmap(bitmap, new Int32Rect(spriteWidth * 5 + 1, 0, spriteWidth, spriteHeight));
            spriteDownRightLeg = new CroppedBitmap(bitmap, new Int32Rect(spriteWidth * 2 + 1, 0, spriteWidth, spriteHeight));
            spriteDownLeftLeg = new CroppedBitmap(bitmap, new Int32Rect(spriteWidth * 1 + 1, 0, spriteWidth, spriteHeight));
            spriteDown = new CroppedBitmap(bitmap, new Int32Rect(0, 0, spriteWidth, spriteHeight));
            image.Source = spriteDown;
        }

        public void SetState(SpriteState state)
        {
            switch (state)
            {
                case SpriteState.LEFT:
                    {
                        imageOnWindow.Source = spriteLeft;
                        break;
                    }
                case SpriteState.LEFT_LEG_UP:
                    {
                        imageOnWindow.Source = spriteLeftLeg;
                        break;
                    }
                case SpriteState.RIGHT:
                    {
                        imageOnWindow.Source = spriteRight;
                        break;
                    }
                case SpriteState.RIGHT_LEG_UP:
                    {
                        imageOnWindow.Source = spriteRightLeg;
                        break;
                    }
                case SpriteState.UP:
                    {
                        imageOnWindow.Source = spriteUp;
                        break;
                    }
                case SpriteState.UP_LEFT_LEG_UP:
                    {
                        imageOnWindow.Source = spriteUpLeftLeg;
                        break;
                    }
                case SpriteState.UP_RIGHT_LEG_UP:
                    {
                        imageOnWindow.Source = spriteUpRightLeg;
                        break;
                    }
                case SpriteState.DOWN:
                    {
                        imageOnWindow.Source = spriteDown;
                        break;
                    }
                case SpriteState.DOWN_LEFT_LEG_UP:
                    {
                        imageOnWindow.Source = spriteDownLeftLeg;
                        break;
                    }
                case SpriteState.DOWN_RIGHT_LEG_UP:
                    {
                        imageOnWindow.Source = spriteDownRightLeg;
                        break;
                    }
            }
        }

        public enum SpriteState
        {
            LEFT,
            LEFT_LEG_UP,
            RIGHT,
            RIGHT_LEG_UP,
            UP,
            UP_LEFT_LEG_UP,
            UP_RIGHT_LEG_UP,
            DOWN,
            DOWN_RIGHT_LEG_UP,
            DOWN_LEFT_LEG_UP
        }
    }
}
