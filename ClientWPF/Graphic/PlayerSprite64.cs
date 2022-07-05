using System.Windows.Media.Imaging;

namespace ClientWPF.Graphic
{
    public class PlayerSprite64 : PlayerSprite
    {

        public PlayerSprite64(WriteableBitmap bitmap, WriteableBitmap jumpBitMap, WriteableBitmap deathBitMap) : base(bitmap, jumpBitMap, deathBitMap)
        {
            spriteHeight = 58;
            spriteWidth = 64;
        }
    }
}
