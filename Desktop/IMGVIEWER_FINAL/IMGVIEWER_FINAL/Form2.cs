using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMGVIEWER_FINAL
{
    public partial class frmView : Form
    {
        List<string> files;
        int i = 0;
        Image imgOriginal;

        public frmView(List<string> fileNames)
        {
            InitializeComponent();

            files = new List<string>();
            foreach (string file in fileNames)
            {
                files.Add(file);
            }
            imgOriginal = Image.FromFile(files[0]);

        }
        public Image ImageBox
        {
            set
            {
                this.pictureBox.Image = value;
                this.pictureBox.Size = value.Size;
            }
        }

        

       
        Image Zoom(Image img, Size size)
        {
            Bitmap bmp = new Bitmap(img, img.Width + (img.Width * size.Width / 100), img.Height + (img.Height * size.Height / 100));
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            i++;
            if (i == files.Count)
            {
                i = 0;
            }
            pictureBox.Image = Image.FromFile(files[i]);
            pictureBox.Refresh();
            imgOriginal = pictureBox.Image;
            trackBar.Value = 0;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (i == 0)
            {
                i = files.Count - 1;
            }
            else
            {
                i--;
            }
            pictureBox.Image = Image.FromFile(files[i]);
            pictureBox.Refresh();
            imgOriginal = pictureBox.Image;
            trackBar.Value = 0;
        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            pictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox.Refresh();
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Refresh();
        }

        private void trackBar_Scroll_1(object sender, EventArgs e)
        {
            if (trackBar.Value > 0)
            {
                pictureBox.Image = Zoom(imgOriginal, new Size(trackBar.Value, trackBar.Value));
                pictureBox.Refresh();
            }
        }
    }
}
