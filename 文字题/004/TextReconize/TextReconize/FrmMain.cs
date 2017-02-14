using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextReconize
{
    public partial class FrmMain : Form
    {
        List<string> pairs = new List<string>();
        Dictionary<string, Image> images = new Dictionary<string, Image>();
        private string[] mWordsFlags = { "m", "mg", "mq" };
        private string[] nWordsFlags = { "n", "nr", "nr1", "nr2", "nrj", "nrf", "ns", "nsf", "nt", "nz", "nl", "ng" };

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btn_Transform_Click(object sender, EventArgs e)
        {
            pairs = new List<string>();
            if (string.IsNullOrEmpty(textBox_Resource.Text))
            {
                errorProvider1.SetError(this.textBox_Resource, "请输入分析文字。");
                return;
            }
            string toNum = ChineaseFiguresToNum.TransformToLong(textBox_Resource.Text);
            var posSeg = new PosSegmenter();
            var tokens = posSeg.Cut(toNum, true).ToList();
            this.textBox_SplitResult.Text = string.Join(",", tokens.Select(token => string.Format("{0}/{1}", token.Word, token.Flag)));
            for (int i = 0; i < tokens.Count; i++)
            {
                if (mWordsFlags.Contains(tokens[i].Flag))
                {
                    long num;
                    if (long.TryParse(tokens[i].Word, out num))
                    {
                        for (int j = 0; j < 5 && i + j < tokens.Count; j++)
                        {
                            if (nWordsFlags.Contains(tokens[i + j].Flag))
                            {
                                pairs.Add(num.ToString() +"*"+ tokens[i + j].Word);
                                break;
                            }
                        }
                    }
                }
            }
            this.textBox_mWords.Text = string.Join(",", pairs);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "示例（文字提取展示）.txt");
            if (System.IO.File.Exists(filePath))
            {
                this.textBox_Resource.Text = System.IO.File.ReadAllText(filePath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文件(*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_Resource.Text = System.IO.File.ReadAllText(dialog.FileName);
            }
        }

        private void btn_AddImage_Click(object sender, EventArgs e)
        {
            FrmAddImage frm = new FrmAddImage();
            frm.Show();
        }

        private void btn_ShowImage_Click(object sender, EventArgs e)
        {
            var dirInfo = new DirectoryInfo(FrmAddImage.picDir);
            if (dirInfo.GetFiles() == null || dirInfo.GetFiles().Count() == 0)
            {
                MessageBox.Show("无图片内容。");
                return;
            }
            foreach (FileInfo info in new DirectoryInfo(FrmAddImage.picDir).GetFiles())
            {
                if (images.ContainsKey(info.Name))
                    continue;
                images.Add(info.Name, Image.FromFile(info.FullName));
            }
            foreach (var item in pairs)
            {
                string[] str = item.Split('*');
                long a = long.Parse(str[0]);
                System.Drawing.Image image = images.Where(c => c.Key.Contains(str[1])).Select(c => c.Value).FirstOrDefault();
                for (int i = 0; i < a; i++)
                {
                    Clipboard.SetDataObject(image);//将数据置于系统剪贴板中
                    DataFormats.Format dataFormat = DataFormats.GetFormat(DataFormats.Bitmap);//格式
                    if (rtf_Images.CanPaste(dataFormat))
                        rtf_Images.Paste(dataFormat);
                }
            }
        }
    }
}
