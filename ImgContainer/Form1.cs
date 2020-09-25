using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImgContainer
{
    public partial class Form1 : Form
    {
        Image srcImage;
        string defaultName;

        public Form1()
        {
            InitializeComponent();

            labImage.Click += new EventHandler(delegate (object sender1, EventArgs e1)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "All Support Images|*.jpg;*.jpeg;*.png;*.bmp;*.tiff|JPG Files|*.jpg;*.jpeg|PNG Files|*.png|BMP Files|*.bmp|TIFF Files|*.tiff|All Files|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    defaultName = Path.GetFileName(fileDialog.FileName);
                    srcImage = Image.FromFile(Path.GetFullPath(fileDialog.FileName));
                    if ((double)srcImage.Width / (double)srcImage.Height > 16.0 / 9.0)
                    {
                        labImage.Image = new Bitmap(srcImage, new Size(labImage.Width, (int)(srcImage.Height / ((double)srcImage.Width / labImage.Width))));
                    }
                    else
                    {
                        labImage.Image = new Bitmap(srcImage, new Size((int)(srcImage.Width / ((double)srcImage.Height / labImage.Height)), labImage.Height));
                    }
                    labImage.Text = "";

                    labLog.Text = String.Format("Resolution: {0} x {1}    Max volume: {2}", srcImage.Width, srcImage.Height, Core.GetVolume(srcImage));
                }
            });

            btnChoose.Click += new EventHandler(delegate (object sender1, EventArgs e1)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInput.Text = Path.GetFullPath(fileDialog.FileName);
                    labInfo2.Text = String.Format("Size: {0}", Core.GetSize(new FileInfo(txtInput.Text).Length));
                }
            });

            btnCombine.Click += new EventHandler(delegate (object sender1, EventArgs e1)
            {
                if(!File.Exists(txtInput.Text))
                {
                    MessageBox.Show("Illegal file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileInfo srcFile = new FileInfo(txtInput.Text);
                if (Core.Check(srcImage, srcFile))
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.FileName = Path.GetFileNameWithoutExtension(defaultName);
                    saveDialog.DefaultExt = Path.GetExtension(defaultName);
                    saveDialog.Filter = "PNG File|*.png|BMP File|*.bmp";

                    if(saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        Image result = Core.Combine(srcImage, new FileInfo(txtInput.Text), Path.GetFileName(txtInput.Text));
                        switch (saveDialog.FilterIndex)
                        {
                            default:
                            case 0:
                                result.Save(saveDialog.FileName, ImageFormat.Png);
                                break;
                            case 1:
                                result.Save(saveDialog.FileName, ImageFormat.Bmp);
                                break;
                        }
                        MessageBox.Show("Complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("This file is too large!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            btnSeparate.Click += new EventHandler(delegate (object sender1, EventArgs e1)
            {
                if(srcImage == null)
                {
                    MessageBox.Show("Empty Image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string filename = Core.GetFilename(srcImage);
                if(filename == null)
                {
                    MessageBox.Show("This image may not be a combined image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.FileName = filename;
                saveDialog.Filter = "All Files|*.*";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] data = Core.GetData(srcImage);
                    if(data == null)
                    {
                        MessageBox.Show("This image may not be a combined image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    File.WriteAllBytes(saveDialog.FileName, data);
                    MessageBox.Show("Complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
        }
    }
}
