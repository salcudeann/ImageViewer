using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IMGVIEWER_FINAL
{
    public partial class Form1 : Form
    {
        List<string> fileNames = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }
        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = ".jpg(*.jpg)|*.jpg|.jpeg(*.jpeg)|*.jpeg|.png(*.png)|*.png|.gif(*.gif)|*.gif|All files(*.*)|*.*";

            openDialog.DefaultExt = ".jpg";
            openDialog.Multiselect = true;
            if (openDialog.ShowDialog(this) == DialogResult.OK)
            {
                fileNames.Clear();
                listViewFile.Items.Clear();

                foreach (string fileName in openDialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    fileNames.Add(fileInfo.FullName);
                    listViewFile.Items.Add(fileInfo.Name, 0);
                }
            }
            if (fileNames.Count > 0)
            {
                btnSlide.Enabled = true;
            }
        }
        private void listViewFile_ItemActivate(object sender, EventArgs e)
        {
            if (listViewFile.FocusedItem != null)
            {
                frmView frm = new frmView(fileNames);
                frm.ImageBox = Image.FromFile(fileNames[listViewFile.FocusedItem.Index]);
                frm.ShowDialog(this);
            }
        }

        private void btnSlide_Click(object sender, EventArgs e)
        {
            frmSlide slide = new frmSlide(fileNames);
            slide.ImageBox = Image.FromFile(fileNames[0]);
            slide.Show();
        }
    }
}
