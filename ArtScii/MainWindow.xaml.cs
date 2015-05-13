using System.Windows;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ArtScii
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bitmap originalBitmap = null;

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
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                // What is the WPF equivalent? Hmmm...
                //ApplyFilter();
            }
        }

        private void btnSaveNewImage_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(new TextRange(txtAscii.Document.ContentStart, txtAscii.Document.ContentEnd).Text))
            //{
            //    return;
            //}

            //Font textFont = new System.Drawing.Font(txtAscii.Document.FontFamily.ToString(), (float)numFontSize.Value, txtAscii.Font.Style);
            //Bitmap textBitmap = originalBitmap.ASCIIFilter((int)numPixelsToChar.Value).TextToImage(textFont, (float)(numZoom.Value / 100));

            //if (textBitmap != null)
            //{
            //    SaveFileDialog sfd = new SaveFileDialog();
            //    sfd.Title = "Specify a file name and file path";
            //    sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            //    sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            //    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
            //        ImageFormat imgFormat = ImageFormat.Png;

            //        if (fileExtension == "BMP")
            //        {
            //            imgFormat = ImageFormat.Bmp;
            //        }
            //        else if (fileExtension == "JPG")
            //        {
            //            imgFormat = ImageFormat.Jpeg;
            //        }

            //        StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
            //        textBitmap.Save(streamWriter.BaseStream, imgFormat);
            //        streamWriter.Flush();
            //        streamWriter.Close();

            //        textBitmap.Dispose();
            //    }
            //}
        }
    }
}
