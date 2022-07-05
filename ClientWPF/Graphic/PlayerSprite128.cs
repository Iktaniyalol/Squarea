using System.Windows.Media.Imaging;

namespace ClientWPF.Graphic
{
    public class PlayerSprite128 : PlayerSprite
    {
        public PlayerSprite128(WriteableBitmap bitmap, WriteableBitmap jumpBitMap, WriteableBitmap deathBitMap) : base(bitmap, jumpBitMap, deathBitMap)
        {
            spriteHeight = 116;
            spriteWidth = 128;
        }
    }
}
