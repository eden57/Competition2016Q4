using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string ciku = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            string source = Read(path);
            string getData = ReadXml("reg");
            string result = string.Empty;
            List<String> list = new List<string>();
            //Regex reg = new Regex("NAME=(.+);");
            MatchCollection mc = Regex.Matches(source, getData);
            ciku = Read("../../Config/cikuheiheihei.txt");
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (Match match in mc)
            {
                string v = match.Value;
                if (v.IndexOf("的") > -1)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        try
                        {
                            string occ = v.Substring(v.IndexOf("的") + 1, i);
                            if (ciku.IndexOf(" " + occ + " ") > -1)
                            {
                                int num = convertNumber(v.Substring(0, 1));
                                if (!dic.ContainsKey(occ))
                                    dic.Add(occ, 1);
                                else
                                    dic[occ] = dic[occ] + num;
                            }
                        }
                        catch { }
                    }
                }
                else
                {
                    for (int i = 1; i < 5; i++)
                    {
                        try
                        {
                            string occ = v.Substring(2, i);
                            if (ciku.IndexOf(" " + occ + " ") > -1)
                            {
                                int num = convertNumber(v.Substring(0, 1));
                                if (!dic.ContainsKey(occ))
                                    dic.Add(occ, 1);
                                else
                                    dic[occ] = dic[occ] + num;
                            }
                        }
                        catch { }
                    }
                }
            }


            foreach (KeyValuePair<string, int> a in dic)//遍历集合
            {
                result += "对象 " + a.Key + " 出现次数为： " + a.Value + "\r\n";
            }
            textBox2.Text = result;

        }

        public string Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            string s = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                s += line.ToString();
            }
            return s;
        }

        private string ReadXml(string xmlName)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("../../Config/" + xmlName + ".xml");
            XmlNode root = xml.SelectSingleNode("/root");
            return root.InnerText;
        }

        private int convertNumber(string s)
        {
            switch (s)
            {
                case "一":
                    return 1;
                case "二":
                    return 2;
                case "三":
                    return 3;
                case "四":
                    return 4;
                case "五":
                    return 5;
                case "六":
                    return 6;
                case "七":
                    return 7;
                case "八":
                    return 8;
                case "九":
                    return 9;
                case "十":
                    return 10;
                case "十一":
                    return 11;
                case "十二":
                    return 12;
                case "十三":
                    return 13;
                case "十四":
                    return 14;
                case "十五":
                    return 15;
                case "十六":
                    return 16;
                case "十七":
                    return 17;
                case "十八":
                    return 18;
                case "十九":
                    return 19;
                case "二十":
                    return 20;
                default:
                    return 0;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd=new OpenFileDialog();
            ofd.ShowDialog();
            textBox1.Text = ofd.FileName;
        }
    }
}
