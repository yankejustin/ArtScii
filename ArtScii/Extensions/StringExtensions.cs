using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ArtScii.Extensions
{
    public static class StringExtensions
    {
        public static string RandomStringSort(this string stringValue)
        {
            char[] charArray = stringValue.ToCharArray();

            Random randomIndex = new Random((byte)charArray[0]);
            int iterator = charArray.Length;

            while (iterator > 1)
            {
                iterator -= 1;

                int nextIndex = randomIndex.Next(iterator + 1);

                char nextValue = charArray[nextIndex];
                charArray[nextIndex] = charArray[iterator];
                charArray[iterator] = nextValue;
            }

            return new string(charArray);
        }

        public static Bitmap TextToImage(this string text, Font font,
                                                        float factor)
        {
            Bitmap textBitmap = new Bitmap(1, 1);

            Graphics graphics = Graphics.FromImage(textBitmap);

            int width = (int)Math.Ceiling(
                        graphics.MeasureString(text, font).Width *
                        factor);

            int height = (int)Math.Ceiling(
                         graphics.MeasureString(text, font).Height *
                         factor);

            graphics.Dispose();

            textBitmap = new Bitmap(width, height,
                                    PixelFormat.Format32bppArgb);

            graphics = Graphics.FromImage(textBitmap);
            graphics.Clear(Color.Black);

            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            graphics.ScaleTransform(factor, factor);
            graphics.DrawString(text, font, Brushes.White, new PointF(0, 0));

            graphics.Flush();
            graphics.Dispose();

            return textBitmap;
        }
    }
}