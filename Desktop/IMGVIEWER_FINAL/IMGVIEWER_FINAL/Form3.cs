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
    public partial class frmSlide : Form
    {
        List<string> files;
        const int millisecond = 2500;
        int i = 0;
        public Image ImageBox
        {
            set
            {
                this.pictureBoxSlide.Image = value;
                this.pictureBoxSlide.Size = value.Size;
            }
        }
        public frmSlide(List<string> fileNames)
        {
            InitializeComponent();
            files = new List<string>();
            foreach (string file in fileNames)
            {
                files.Add(file);
            }
        }

      
        public async void StartSlideShow()
        {
            bool ok = false;
            do
            {
                for (i = 0; i < files.Count; i++)
                {
                    pictureBoxSlide.Image = Image.FromFile(files[i]);
                    await Task.Delay(millisecond);
                    pictureBoxSlide.Refresh();

                }
            } while (ok == false);
        }

       

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartSlideShow();
            btnStart.Enabled = false;
        }
    }
}
