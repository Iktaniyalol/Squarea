using System;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;

namespace ClientWPF.Graphic
{
    public static class ImageTransform
    {
        public static BitmapImage GetBitMapImageFromUrl(string url)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@url, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            return bi;
        }

        public static BitmapImage GetBitMapImageFromResources(string path)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"pack://application:,,,/Resources/" + path, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            return bi;
        }
        public static uint[] GetPixelsFromBitMapImage(BitmapImage bi)
        {
            int width = (int)bi.Width;
            int height = (int)bi.Height;
            WriteableBitmap writeable = new WriteableBitmap(bi);
            uint[] pixels = new uint[width * height];
            writeable.CopyPixels(pixels, width * 4, 0);
            return pixels;
        }

        public static uint[] GetPixelsFromUrl(string url)
        {
            BitmapImage bi = GetBitMapImageFromUrl(url);
            bi.BeginInit();
            bi.UriSource = new Uri(@url, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            int width = (int)bi.Width;
            int height = (int)bi.Height;
            WriteableBitmap writeable = new WriteableBitmap(bi);
            uint[] pixels = new uint[width * height];
            writeable.CopyPixels(pixels, width * 4, 0);
            return pixels;
        }

        public static uint[] GetPixelsFromResources(string path)
        {
            BitmapImage bi = GetBitMapImageFromResources(path);
            int width = (int)bi.Width;
            int height = (int)bi.Height;
            WriteableBitmap writeable = new WriteableBitmap(bi);
            uint[] pixels = new uint[width * height];
            writeable.CopyPixels(pixels, width * 4, 0);
            return pixels;
        }

        public static uint[] GetPixelsFromWriteableBitMap(WriteableBitmap writeable)
        {
            int width = (int)writeable.Width;
            int height = (int)writeable.Height;
            uint[] pixels = new uint[width * height];
            writeable.CopyPixels(pixels, width * 4, 0);
            return pixels;
        }

        public static WriteableBitmap GetWriteableBitmapFromBitMapImage(BitmapImage bi)
        {
            WriteableBitmap writeable = new WriteableBitmap(bi);
            return writeable;
        }

        public static WriteableBitmap GetWriteableBitmapFromUrl(string url)
        {
            BitmapImage bi = GetBitMapImageFromUrl(url);
            WriteableBitmap writeable = new WriteableBitmap(bi);
            return writeable;
        }

        public static WriteableBitmap GetWriteableBitmapFromResources(string path)
        {
            BitmapImage bi = GetBitMapImageFromResources(path);
            WriteableBitmap writeable = new WriteableBitmap(bi);
            return writeable;
        }

        public static WriteableBitmap SetPixelsOnBitMapImageByUrl(string url, uint[] pixels)
        {
            BitmapImage bi = GetBitMapImageFromUrl(url);
            int width = (int)bi.Width;
            WriteableBitmap writeable = GetWriteableBitmapFromBitMapImage(bi);
            writeable.WritePixels(new Int32Rect(0, 0, writeable.PixelWidth, writeable.PixelHeight), pixels, width * 4, 0);
            return writeable;
        }

        public static WriteableBitmap SetPixelsOnBitMapImageByPath(string path, uint[] pixels)
        {
            BitmapImage bi = GetBitMapImageFromResources(path);
            int width = (int)bi.Width;
            WriteableBitmap writeable = GetWriteableBitmapFromBitMapImage(bi);
            writeable.WritePixels(new Int32Rect(0, 0, writeable.PixelWidth, writeable.PixelHeight), pixels, width * 4, 0);
            return writeable;
        }


        public static WriteableBitmap SetPixelsOnBitMapImage(BitmapImage bi, uint[] pixels)
        {
            int width = (int)bi.Width;
            WriteableBitmap writeable = GetWriteableBitmapFromBitMapImage(bi);
            writeable.WritePixels(new Int32Rect(0, 0, writeable.PixelWidth, writeable.PixelHeight), pixels, width * 4, 0);
            return writeable;
        }

        public static WriteableBitmap SetPixelsOnWriteableBitMap(WriteableBitmap writeable, uint[] pixels)
        {
            int width = (int)writeable.Width;
            writeable.WritePixels(new Int32Rect(0, 0, writeable.PixelWidth, writeable.PixelHeight), pixels, width * 4, 0);
            return writeable;
        }

        public static WriteableBitmap SetColorToPlayerSprite(Color color, WriteableBitmap writeable)
        {
            int width = (int)writeable.Width;
            int height = (int)writeable.Height;
            uint[] oldpixels = GetPixelsFromWriteableBitMap(writeable);

            uint[] newpixels = new uint[width * height];

            uint red;
            uint green;
            uint blue;
            uint alpha;

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {

                    int i = width * y + x;
                    uint pixelColor = oldpixels[i];
                    red = (uint)((pixelColor >> 16 & 255) * (color.R / 255.0));
                    green = (uint)((pixelColor >> 8 & 255) * (color.G / 255.0));
                    blue = (uint)((pixelColor & 255) * (color.B / 255.0));
                    alpha = pixelColor >> 24 & 255;

                    newpixels[i] = (alpha << 24) + (red << 16) + (green << 8) + blue;
                }
            }

            SetPixelsOnWriteableBitMap(writeable, newpixels);
            return writeable;
        }
    }
}
