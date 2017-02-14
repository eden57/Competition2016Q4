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

namespace GeoneWordProcess
{
    public partial class Form1 : Form
    {
        //总文件内容
        string fileContent="";
    
       

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileName = new OpenFileDialog();
            fileName.InitialDirectory = Application.StartupPath;
            fileName.Filter = "txt文件(*.txt) | *.txt";
            fileName.FilterIndex = 2;
            fileName.RestoreDirectory = true;
            if(fileName.ShowDialog()==DialogResult.OK)
            {
                string Path = fileName.FileName.ToString();
                FileStream fs = new FileStream(Path, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                

                while (!sr.EndOfStream)
                {
                    fileContent+= sr.ReadLine();
                   
                }
                fs.Close();

                outputRead(fileContent);
            }

        }

        /// <summary>
        /// 展示文件
        /// </summary>
        /// <param name="fileContent"></param>
        public void outputRead(string fileContent)
        {
            //if (textBoxRead.GetLineFromCharIndex(textBoxRead.Text.Length) > 100)
            //    textBoxRead.Text = "";
           
                textBoxRead.AppendText(fileContent + "\r\n");
            
            
            
        }

        /// <summary>
        /// 展示结果
        /// </summary>
        /// <param name="Objects"></param>
        public void outputDisplay(List<TxtObject> Objects)
        {
            //if (textBoxDisplay.GetLineFromCharIndex(textBoxDisplay.Text.Length) > 100)
            //    textBoxDisplay.Text = "";
            for(int i=0; i< Objects.Count;i++)
            {
                textBoxDisplay.AppendText("数量： "+Objects[i].Num + "   " +"类别:   ");
                for (int j=0;j< Objects[i].Num;j++)
                {
                    textBoxDisplay.AppendText(Objects[i].NounWord+"  ");
                }

                textBoxDisplay.AppendText("\t\n");
                textBoxDisplay.AppendText("\t\n");


            }
            

        }


        /// <summary>
        /// 处理分句，展示结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string[] fileSentence = new string[] { };
            //对象组
            List<TxtObject> Objects = new List<TxtObject>();

            //按标点符号分句
            fileSentence = fileContent.Split(new char[] { '，', '。', '：', '？' });

            ObjectExact.DataExact(fileSentence, Objects);

            outputDisplay(Objects);

        }
    }
}
