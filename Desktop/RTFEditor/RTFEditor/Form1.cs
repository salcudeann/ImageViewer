using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTFEditor
{
    public partial class Form1 : Form
    {
        const string DEFAULT_NAME = "Document1";
        private bool isDirty = false;
        public string FileName { get; set; } 

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(FileName))
            {
                FileName = DEFAULT_NAME;
                toolStripFileName.Text = FileName;
            }
            else
            {
                LoadFile(FileName);
            }
        }

        private void LoadFile(string fileName)
        {
            if(File.Exists(fileName))
            { 
                rbMain.LoadFile(fileName, GetFileType(fileName));
            }
        }

        private RichTextBoxStreamType GetFileType(string fileName)
        {
            var rbType = RichTextBoxStreamType.RichText;
            var fi = new FileInfo(fileName);
            switch(fi.Extension)
            {
                case ".txt":
                    rbType = RichTextBoxStreamType.PlainText;
                    break;
            }
            return rbType;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isDirty)   // in cazul in care am lucrat pe ceva si nu am salvat si incerc sa deschid un dn document nou
            {
                var res = MessageBox.Show(this,"Content is not saved.\nCreate new document anyway?","Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if(res == DialogResult.No)
                {
                    return;
                }
            }
            rbMain.Clear();
            FileName = DEFAULT_NAME;
            toolStripFileName.Text = DEFAULT_NAME;
            isDirty = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Rich Text Format Files(*.rtf)|*.rtf|All files(*.*)|*.*";
            if(openDialog.ShowDialog(this) == DialogResult.OK)
            {
                FileName = openDialog.FileName;
                LoadFile(FileName);
                isDirty = false;
                toolStripFileName.Text = FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(FileName == DEFAULT_NAME)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                rbMain.SaveFile(FileName);
                isDirty = false;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Rich Text Format Files(*.rtf)|*.rtf|All files(*.*)|*.*";
            saveDialog.FileName = FileName;
            saveDialog.OverwritePrompt = true; //daca vrem sa suprascriem, sa ne intrebe
            saveDialog.DefaultExt = ".rtf";
            if(saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                FileName = saveDialog.FileName;
                rbMain.SaveFile(FileName);
                isDirty = false;
                toolStripFileName.Text = FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rbMain.CanUndo)
                rbMain.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbMain.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbMain.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbMain.Paste();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog();
            fontDialog.Font = rbMain.SelectionFont;
            if (fontDialog.ShowDialog(this) == DialogResult.OK)
            {
                rbMain.SelectionFont = fontDialog.Font;
            }
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            colorDialog.Color = rbMain.SelectionColor;
            if(colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                rbMain.SelectionColor = colorDialog.Color;
            }
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            colorDialog.Color = rbMain.SelectionBackColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                rbMain.SelectionBackColor = colorDialog.Color;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new MyAboutBox();
            about.ShowDialog(this);
        }

        private void rbMain_TextChanged(object sender, EventArgs e)
        {
            isDirty = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(isDirty)
            {
                var result = MessageBox.Show(this, "Document is not saved. \nSave now ? ", "Warning?",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, EventArgs.Empty);
                }
                if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }   
        }
    }
}
