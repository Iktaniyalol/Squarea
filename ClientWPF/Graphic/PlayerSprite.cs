using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace ClientWPF.Graphic
{
    public class PlayerSprite
    {
        protected int spriteWidth = 32;
        protected int spriteHeight = 29;
        WriteableBitmap origin;
        WriteableBitmap coloredOrigin;
        WriteableBitmap originJump;
        WriteableBitmap coloredOriginJump;
        WriteableBitmap originDeath;
        WriteableBitmap coloredOriginDeath;
        public List<Frame> leftMoveFrames;
        public List<Frame> rightMoveFrames;
        public List<Frame> upMoveFrames;
        public List<Frame> downMoveFrames;
        public List<Frame> downJumpFrames;
        public List<Frame> upJumpFrames;
        public List<Frame> leftJumpFrames;
        public List<Frame> rightJumpFrames;
        public Frame upFrame;
        public Frame downFrame;
        public Frame leftFrame;
        public Frame rightFrame;
        public List<Frame> deathFrames;

        public PlayerSprite(WriteableBitmap bitmap, WriteableBitmap jumpBitMap, WriteableBitmap deathBitMap)
        {
            this.origin = bitmap;
            this.originJump = jumpBitMap;
            this.originDeath = deathBitMap;
            this.coloredOrigin = bitmap;
            this.coloredOriginJump = jumpBitMap;
            this.coloredOriginDeath = deathBitMap;
            CreateFrames();
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
            DOWN_LEFT_LEG_UP,
            JUMP_UP,
            JUMP_DOWN,
            JUMP_LEFT,
            JUMP_RIGHT,
            DEATH
        }
        public void ChangeColor(Color color)
        {
            coloredOrigin = origin.Clone();
            ImageTransform.SetColorToPlayerSprite(color, coloredOrigin);
            UpdateFrames();
        }

        private void CreateFrames()
        {
            CroppedBitmap croppedBitmap = new CroppedBitmap(origin, new Int32Rect(spriteWidth * 8, 0, spriteWidth, spriteHeight));
            leftFrame = new Frame(SpriteState.LEFT, croppedBitmap);
            croppedBitmap = new CroppedBitmap(origin, new Int32Rect(spriteWidth * 3, 0, spriteWidth, spriteHeight));
            rightFrame = new Frame(SpriteState.RIGHT, croppedBitmap);
            croppedBitmap = new CroppedBitmap(origin, new Int32Rect(spriteWidth * 5, 0, spriteWidth, spriteHeight));
            upFrame = new Frame(SpriteState.UP, croppedBitmap);
            croppedBitmap = new CroppedBitmap(origin, new Int32Rect(0, 0, spriteWidth, spriteHeight));
            downFrame = new Frame(SpriteState.DOWN, croppedBitmap);
            leftMoveFrames = new List<Frame>(2);
            rightMoveFrames = new List<Frame>(2);
            upMoveFrames = new List<Frame>(4);
            downMoveFrames = new List<Frame>(4);
            leftMoveFrames.Add(leftFrame);
            leftMoveFrames.Add(new Frame(SpriteState.LEFT_LEG_UP, new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 9, 0, spriteWidth, spriteHeight))));
            rightMoveFrames.Add(rightFrame);
            rightMoveFrames.Add(new Frame(SpriteState.RIGHT_LEG_UP, new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 4, 0, spriteWidth, spriteHeight))));

            upMoveFrames.Add(upFrame);
            upMoveFrames.Add(new Frame(SpriteState.UP_LEFT_LEG_UP, new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 6, 0, spriteWidth, spriteHeight))));
            upMoveFrames.Add(upFrame);
            upMoveFrames.Add(new Frame(SpriteState.UP_RIGHT_LEG_UP, new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 7, 0, spriteWidth, spriteHeight))));

            downMoveFrames.Add(downFrame);
            downMoveFrames.Add(new Frame(SpriteState.DOWN_LEFT_LEG_UP, new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 1, 0, spriteWidth, spriteHeight))));
            downMoveFrames.Add(downFrame);
            downMoveFrames.Add(new Frame(SpriteState.DOWN_RIGHT_LEG_UP, new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 2, 0, spriteWidth, spriteHeight))));
        }

        private void UpdateFrames()
        {
            CroppedBitmap croppedBitmap = new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 8, 0, spriteWidth, spriteHeight));
            leftFrame.FrameBitMap = croppedBitmap;
            croppedBitmap = new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 3, 0, spriteWidth, spriteHeight));
            rightFrame.FrameBitMap = croppedBitmap;
            croppedBitmap = new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 5, 0, spriteWidth, spriteHeight));
            upFrame.FrameBitMap = croppedBitmap;
            croppedBitmap = new CroppedBitmap(coloredOrigin, new Int32Rect(0, 0, spriteWidth, spriteHeight));
            downFrame.FrameBitMap = croppedBitmap;

            leftMoveFrames[0].FrameBitMap = leftFrame.FrameBitMap;
            leftMoveFrames[1].FrameBitMap = new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 9, 0, spriteWidth, spriteHeight));

            rightMoveFrames[0].FrameBitMap = rightFrame.FrameBitMap;
            rightMoveFrames[1].FrameBitMap = new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 4, 0, spriteWidth, spriteHeight));

            upMoveFrames[0] = upFrame;
            upMoveFrames[1].FrameBitMap = new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 6, 0, spriteWidth, spriteHeight));
            upMoveFrames[2] = upFrame;
            upMoveFrames[3].FrameBitMap = new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 7, 0, spriteWidth, spriteHeight));

            downMoveFrames[0] = downFrame;
            downMoveFrames[1].FrameBitMap = new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 1, 0, spriteWidth, spriteHeight));
            downMoveFrames[2] = downFrame;
            downMoveFrames[3].FrameBitMap = new CroppedBitmap(coloredOrigin, new Int32Rect(spriteWidth * 2, 0, spriteWidth, spriteHeight));
        }

        
    }

    //public class JumpAnimation

    public class Frame
    {
        CroppedBitmap bitmap;
        PlayerSprite.SpriteState state;
        public Frame(PlayerSprite.SpriteState state, CroppedBitmap bitmap)
        {
            this.state = state;
            this.bitmap = bitmap;
        }

        public CroppedBitmap FrameBitMap
        {
            get
            {
                return bitmap;
            }
            set
            {
                bitmap = value;
            }
        }

        public PlayerSprite.SpriteState State
        {
            get
            {
                return state;
            }
        }
    }
}
