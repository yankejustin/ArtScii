using System;
using System.Drawing;
using System.Text;

namespace ArtScii.AsciiHelpers
{
    // Adapted from https://www.codeproject.com/Articles/20435/Using-C-To-Generate-ASCII-Art-From-An-Image
    // Thank you DreamInHex :)

    // This works for now. It is a basic way to convert from a grayscale image to a text representation. Will be improved soon but this is a good start.

    public class ASCIIConverter
    {
        /// <summary>
        /// These are the "pixels" for the output.
        /// </summary>
        private const string BLACK = "@";
        private const string CHARCOAL = "#";
        private const string DARKGRAY = "8";
        private const string MEDIUMGRAY = "&";
        private const string MEDIUM = "o";
        private const string GRAY = ":";
        private const string SLATEGRAY = "*";
        private const string LIGHTGRAY = ".";
        private const string WHITE = " ";

        /// <summary>
        /// Returns the appropriate character to represent the color the pixel should be.
        /// </summary>
        /// <param name="redValue"></param>
        /// <returns></returns>
        private static string getGrayShade(int redValue)
        {
            // Base the appropriate character using the intensity of the red value.

            string asciival = string.Empty;

            if (redValue >= 230)
            {
                asciival = WHITE;
            }
            else if (redValue >= 200)
            {
                asciival = LIGHTGRAY;
            }
            else if (redValue >= 180)
            {
                asciival = SLATEGRAY;
            }
            else if (redValue >= 160)
            {
                asciival = GRAY;
            }
            else if (redValue >= 130)
            {
                asciival = MEDIUM;
            }
            else if (redValue >= 100)
            {
                asciival = MEDIUMGRAY;
            }
            else if (redValue >= 70)
            {
                asciival = DARKGRAY;
            }
            else if (redValue >= 50)
            {
                asciival = CHARCOAL;
            }
            else
            {
                asciival = BLACK;
            }

            return asciival;
        }

        public static string GrayscaleImageToASCII(Bitmap bmp)
        {
            StringBuilder output = new StringBuilder();

            try
            {
                // Let's take a trip through each pixel in the image.
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color col = bmp.GetPixel(x, y);

                        // Convert to grayscale by adding the RGB colors and diving by three.
                        //TODO: Find a more accurate way of doing this. The end result is pretty good for how easy this is but it is pretty inaccurate.
                        col = Color.FromArgb((col.R + col.G + col.B) / 3, (col.R + col.G + col.B) / 3, (col.R + col.G + col.B) / 3);

                        // Get the red value (from 0 to 255).
                        int rValue = int.Parse(col.R.ToString());
                        
                        output.Append(getGrayShade(rValue));

                        // Append a newline once we get to the edge of the image.
                        //TODO: Change this so the image will scale dynamically. Should likely just add a check here so the output respects current settings.
                        if (x == bmp.Width - 1)
                            output.Append('\n');
                    }
                }
                return output.ToString();
            }
            catch (Exception exc)
            {
                return exc.ToString();
            }
            finally
            {
                bmp.Dispose();
            }
        }
    }
}
