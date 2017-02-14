using System;
using System.Windows.Forms;
using System.IO;

namespace TextReconize
{
    public partial class FrmAddImage : Form
    {
        System.Drawing.Image imgOutput2;
        public static string picDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory , "Pictures");

        public FrmAddImage()
        {
            InitializeComponent();
        }

        private void btn_View_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialgo = new OpenFileDialog();
            dialgo.Filter = "图片文件(*.png)|*.png | 图片文件(*.jpg)|*.jpg";
            if (dialgo.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Image imgOutput = System.Drawing.Bitmap.FromFile(dialgo.FileName);
                imgOutput2 = imgOutput.GetThumbnailImage(80, 80, null, IntPtr.Zero);
                this.panel1.BackgroundImage = imgOutput2;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                errorProvider1.SetError(this.textBox1, "请输入描述文字。");
                return;
            }
            if (this.panel1.BackgroundImage == null)
            {
                errorProvider1.SetError(this.panel1, "请添加图片。");
                return;
            }
            if (!Directory.Exists(picDir))
            {
                Directory.CreateDirectory(picDir);
            }
            string path = Path.Combine(picDir, this.textBox1.Text + ".jpg");
            imgOutput2.Save(path);
            MessageBox.Show("保存成功。");
        }
    }
}
