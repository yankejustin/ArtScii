using System.Windows;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ArtScii.Extensions;
using System.Windows.Documents;
using System.Drawing.Imaging;

namespace ArtScii
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bitmap originalBitmap = null;
        private double numZoom = 0.5;
        private int numPixelsToChar = 5;
        private int numColors = 16;
        private float numFontSize = 3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            txtAscii.Paste();
        }

        private void btnOpenOriginal_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "Select an image file.",
                Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg|Bitmap Images(*.bmp)|*.bmp"
            })
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (StreamReader streamReader = new StreamReader(ofd.FileName))
                    {
                        if (originalBitmap != null)
                        {
                            originalBitmap.Dispose();
                        }

                        originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                    }

                    ApplyFilter();
                }
        }

        private void ApplyFilter()
        {
            if (originalBitmap != null)
            {
                txtAscii.Document.Blocks.Clear();

                //originalBitmap.ScaleBitmap((float)(numZoom.Value / 100)).ASCIIFilter((int)numPixelsToChar.Value, (int)numColors.Value);
                txtAscii.Document.Blocks.Add(new Block(originalBitmap.ScaleBitmap((float)(numZoom / 100)).ASCIIFilter(numPixelsToChar, numColors)));
            }
        }

        private void btnSaveNewImage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(new TextRange(txtAscii.Document.ContentStart, txtAscii.Document.ContentEnd).Text))
            {
                return;
            }

            Font textFont = new Font(txtAscii.Document.FontFamily.ToString(), numFontSize, (GraphicsUnit)txtAscii.Document.FontSize);
            using (Bitmap textBitmap = originalBitmap.ASCIIFilter((int)numPixelsToChar).TextToImage(textFont, (float)(numZoom / 100)))
            {
                if (textBitmap != null)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog()
                        {
                            Title = "Specify a file name and file path",
                            Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg|Bitmap Images(*.bmp)|*.bmp"
                        })
                    {
                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                            ImageFormat imgFormat = ImageFormat.Png;

                            switch (fileExtension)
                            {
                                case "BMP":
                                    imgFormat = ImageFormat.Bmp;
                                    break;
                                case "JPG":
                                    imgFormat = ImageFormat.Jpeg;
                                    break;
                            }

                            using (StreamWriter streamWriter = new StreamWriter(sfd.FileName, false))
                            {
                                textBitmap.Save(streamWriter.BaseStream, imgFormat);
                            }
                        }
                    }
                }
            }
        }
    }
}