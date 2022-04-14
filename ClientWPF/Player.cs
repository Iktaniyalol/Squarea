using ClientWPF.Graphic;
using System.Windows.Media;

namespace ClientWPF
{
    public class Player
    {
        double x, y;
        string name;
        PlayerSprite sprite;
        Color color;

        public Player(string name, PlayerSprite sprite, Color color)
        {
            this.name = name;
            this.sprite = sprite;
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

        public PlayerSprite Sprite
        {
            get
            {
                return sprite;
            }
        }

        public void Move(double x, double y)
        {
            if (y > 0)
            {
                sprite.SetState(PlayerSprite.SpriteState.DOWN);
            } else if (y < 0)
            {
                sprite.SetState(PlayerSprite.SpriteState.UP);
            } else if (x > 0)
            {
                sprite.SetState(PlayerSprite.SpriteState.RIGHT);
            } else if (x < 0)
            {
                sprite.SetState(PlayerSprite.SpriteState.LEFT);
            }
        }
    }
}
