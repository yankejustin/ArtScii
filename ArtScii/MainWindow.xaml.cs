using System;
using System.Windows;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ArtScii
{
    public partial class MainWindow : Window
    {
        private Bitmap originalBitmap = null;

        public MainWindow()
        {
            InitializeComponent();

            txtAscii.Clear();
        }

        private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(txtAscii.Text);
        }

        private void btnOpenOriginal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtAscii.Clear();

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

                        // Set the output textbox text to the text representation of the image.
                        txtAscii.Text = AsciiHelpers.ASCIIConverter.GrayscaleImageToASCII(originalBitmap);
                    }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Unable to open the image: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void btnSaveNewImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Title = "Specify a file name and file path",
                    Filter = "Text File(*.txt)|*.txt"
                })
                {
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, txtAscii.Text);

                        System.Windows.Forms.MessageBox.Show("Saved!", "Success", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Unable to save contents to a file: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}