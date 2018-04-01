using System.Drawing;
using System.Drawing.Imaging;

namespace JPEG.Images
{
    public class YCbCrImage
    {
        public int Width { get; }
        public int Height { get; }
        public double[,] Y { get; }
        public double[,] Cb { get; }
        public double[,] Cr { get; }

        public YCbCrImage(int height, int width)
        {
            Height = height;
            Width = width;
            Y = new double[Height, Width];
            Cb = new double[Height, Width];
            Cr = new double[Height, Width];
        }

        public YCbCrImage(Bitmap bmp) : this(bmp.Height, bmp.Width)
        {
            unsafe
            {
                var bitmapData = bmp.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                var heightInPixels = bitmapData.Height;
                var ptrFirstPixel = (byte*)bitmapData.Scan0;

                for (var y = 0; y < heightInPixels; y++)
                {
                    var currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (var x = 0; x < Width; x++)
                    {
                        var xPor3 = x * 3;
                        float blue = currentLine[xPor3++];
                        float green = currentLine[xPor3++];
                        float red = currentLine[xPor3];

                        Y[y, x] = 16.0 + (65.738 * red + 129.057 * green + 24.064 * blue) / 256.0;
                        Cb[y, x] = 128.0 + (-37.945 * red - 74.494 * green + 112.439 * blue) / 256.0;
                        Cr[y, x] = 128.0 + (112.439 * red - 94.154 * green - 18.285 * blue) / 256.0;
                    }
                }
                bmp.UnlockBits(bitmapData);
            }
        }
        public static explicit operator Bitmap(YCbCrImage image)
        {
            var bmp = new Bitmap(image.Width, image.Height);

            for (var j = 0; j < bmp.Height; j++)
                for (var i = 0; i < bmp.Width; i++)
                {
                    var y = image.Y[j, i];
                    var cb = image.Cb[j, i];
                    var cr = image.Cr[j, i];
                    var r = (298.082 * y + 408.583 * cr) / 256.0 - 222.921;
                    var g = (298.082 * y - 100.291 * cb - 208.120 * cr) / 256.0 + 135.576;
                    var b = (298.082 * y + 516.412 * cb) / 256.0 - 276.836;
                    bmp.SetPixel(i, j, Color.FromArgb(ToByte(r), ToByte(g), ToByte(b)));
                }

            return bmp;
        }

        private static int ToByte(double d)
        {
            var val = (int)d;
            if (val > byte.MaxValue)
                return byte.MaxValue;
            return val < byte.MinValue ? byte.MinValue : val;
        }
    }
}