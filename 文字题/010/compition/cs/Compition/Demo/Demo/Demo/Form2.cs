using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form2 : Form
    {
        public  List<Form1.ListClass>  List=new List<Form1.ListClass>();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Image image;// 具体这张图是从文件读取还是从picturebox什么的获取你来指定
            //using (Graphics g = Graphics.FromImage(image))
            //{
            //    g.DrawString("xxxxx", new Font("宋体", 13),
            //        Brushes.Red, new PointF(100, 100));
            //    g.Flush();
            //}
            //image.Save("D:\newimage.jpg");
        }
    }
}
