using ClientWPF.Graphic;
using System.Windows.Media;
using System.Windows.Controls;
using ClientWPF.Data;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Windows;
using System;

namespace ClientWPF
{
    public class Player : ITickable
    {
        double x, y;
        string name;
        PlayerSprite64 sprite64;
        PlayerSprite128 sprite128;
        Color color;
        Image playerInGameImage;
        List<Player> viewers = new List<Player>();
        ClientWPF.Graphic.Frame spriteFrame;
        PlayerState playerState = PlayerState.STANDING_DOWN;
        int localTicks = 0;

        public Player(string name, Image playerInGameImage, PlayerSprite64 sprite64, PlayerSprite128 sprite128, Color color)
        {
            this.name = name;
            this.playerInGameImage = playerInGameImage;
            this.sprite64 = sprite64;
            this.sprite128 = sprite128;
            this.spriteFrame = sprite64.downFrame;
            this.color = color;
            playerInGameImage.Source = spriteFrame.FrameBitMap;
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public PlayerSprite64 Sprite64
        {
            get
            {
                return sprite64;
            }
        }

        public PlayerSprite128 Sprite128
        {
            get
            {
                return sprite128;
            }
        }

        public void Move(double x, double y)
        {
            if (y > 0)
            {
                if (playerState != PlayerState.MOVING_DOWN)
                {
                    localTicks = 0;
                    playerState = PlayerState.MOVING_DOWN;
                }
            }
            else if (y < 0)
            {
                if (playerState != PlayerState.MOVING_UP)
                {
                    localTicks = 0;
                    playerState = PlayerState.MOVING_UP;
                }
            }
            else if (x > 0)
            {
                if (playerState != PlayerState.MOVING_RIGHT)
                {
                    localTicks = 0;
                    playerState = PlayerState.MOVING_RIGHT;
                }
            }
            else if (x < 0)
            {
                if (playerState != PlayerState.MOVING_LEFT)
                {
                    localTicks = 0;
                    playerState = PlayerState.MOVING_LEFT;
                }
            }
        }

        public void StopMove()
        {
            switch(playerState)
            {
                case PlayerState.MOVING_LEFT:
                    {
                        playerState = PlayerState.STANDING_LEFT;
                        break;
                    }
                case PlayerState.MOVING_RIGHT:
                    {
                        playerState = PlayerState.STANDING_RIGHT;
                        break;
                    }
                case PlayerState.MOVING_UP:
                    {
                        playerState = PlayerState.STANDING_UP;
                        break;
                    }
                case PlayerState.MOVING_DOWN:
                    {
                        playerState = PlayerState.STANDING_DOWN;
                        break;
                    }
            }
        }

        public void Tick()
        {
            switch (playerState)
            {
                case PlayerState.STANDING_LEFT:
                    {
                        spriteFrame = sprite64.leftFrame;
                        break;
                    }
                case PlayerState.STANDING_RIGHT:
                    {
                        spriteFrame = sprite64.rightFrame;
                        break;
                    }

                case PlayerState.STANDING_UP:
                    {
                        spriteFrame = sprite64.upFrame;
                        break;
                    }
                case PlayerState.STANDING_DOWN:
                    {
                        spriteFrame = sprite64.downFrame;
                        break;
                    }
                case PlayerState.MOVING_LEFT:
                    {
                        spriteFrame = sprite64.leftMoveFrames[(localTicks / (Client.Instance.Game.tickPerSecond / 6)) % 2];
                        break;
                    }
                case PlayerState.MOVING_RIGHT:
                    {
                        spriteFrame = sprite64.rightMoveFrames[(localTicks / (Client.Instance.Game.tickPerSecond / 6)) % 2];
                        break;
                    }
                case PlayerState.MOVING_UP:
                    {
                        spriteFrame = sprite64.upMoveFrames[(localTicks / (Client.Instance.Game.tickPerSecond / 6)) % 4];
                        break;
                    }
                case PlayerState.MOVING_DOWN:
                    {
                        spriteFrame = sprite64.downMoveFrames[(localTicks / (Client.Instance.Game.tickPerSecond / 6)) % 4];
                        break;
                    }
                case PlayerState.DEATH:
                    {
                        //TODO
                        break;
                    }
                case PlayerState.JUMP_LEFT:
                    {
                        //TODO
                        break;
                    }
                case PlayerState.JUMP_RIGHT:
                    {
                        //TODO
                        break;
                    }
                case PlayerState.JUMP_UP:
                    {
                        //TODO
                        break;
                    }
                case PlayerState.JUMP_DOWN:
                    {
                        //TODO
                        break;
                    }
            }
            localTicks++;
            try
            {
                Application.Current.Dispatcher.Invoke(new System.Action(() => { playerInGameImage.Source = spriteFrame.FrameBitMap; }));
            } catch
            {

            }
        }

        public enum PlayerState
        {
            STANDING_LEFT,
            STANDING_RIGHT,
            STANDING_UP,
            STANDING_DOWN,
            MOVING_LEFT,
            MOVING_RIGHT,
            MOVING_DOWN,
            MOVING_UP,
            DEATH,
            JUMP_LEFT,
            JUMP_RIGHT,
            JUMP_UP,
            JUMP_DOWN
        }
    }
}
