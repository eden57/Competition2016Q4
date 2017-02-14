using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Demo
{
    public partial class Form1 : Form
    {
        List<string> arr = new List<string>()
        {
           "二十","十九","十八","十七","十六","十五","十四","十三","十二", "十一","十","九","八","七","六","五","四","三","二","一"
        };

        private Dictionary<string, string> dir = new Dictionary<string, string>();
        List<string> OutDir = new List<string>()
        {
            "数","时", "年纪","家","口","三角","眼觑","刺","声","段","惨祸","话","身材","杯","位",
            "道","肉卖","日","生","饮干","兄弟","廷","话","林","官","怀中","物","意兴","招","农",
            "路","难","身","波","新绿","郭犯","事","弟","月","龙廷","相","诗","时分","时辰","实",
           "步","黑物飞","身","高","首","金","袍","事","都","村","瓦","官儿","腕","声长","法","岳","名","武艺",
            "呼哨","惶","呻吟","座","都","招","祸"
        };
        List<string> lc = new List<string>();
        List<ListClass> result = new List<ListClass>();
        public List<string> Arr
        {
            get { return arr; }
            set { arr = value; }
        }

        private string Content = "";
        StringBuilder sb = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            result.Clear();
            string regx = @"一.*?[，：、。\”！]";
            Regex regx1 = new Regex(regx);
            List<string> ListContent = new List<string>();//有对象的数据源
            List<string> ListJuZi = new List<string>();//句子
            string regx2 = @".*?[，：、。\”]";
            string content = Content;
            //找所有的句子
            Regex regx22 = new Regex(regx2);
            Match juzi = regx22.Match(content);
            while (juzi.Success)
            {
                ListJuZi.Add(juzi.Groups[0].ToString());
                juzi = juzi.NextMatch();
            }
            foreach (var jz in ListJuZi)
            {
                foreach (var number in arr)
                {
                    if (jz.Contains(number))
                    {
                        ListContent.Add(jz);
                        break;
                    }
                }
            }

            //词库
            object obj = PostFunction(Content);
            List<MyClass> myClass = JsonConvert.DeserializeObject<List<MyClass>>(obj.ToString());
            for (int i = 0; i < myClass[0].tag.Count; i++)
            {
                if (myClass[0].tag[i] == "n")//名词
                {
                    if (!dir.ContainsKey(myClass[0].word[i]))
                    {
                        dir.Add(myClass[0].word[i], myClass[0].word[i] + ".png");
                    }

                }
                if (myClass[0].tag[i] == "q")//介词
                {
                    if (!lc.Contains(myClass[0].word[i]))
                    {
                        lc.Add(myClass[0].word[i]);
                    }
                }
            }
            foreach (var outdir in OutDir)
            {
                if (dir.ContainsKey(outdir))
                {
                    dir.Remove(outdir);
                }
            }

            foreach (var listContent in ListContent)
            {
                foreach (var lcDemo in lc)
                {
                    if (listContent.Contains(lcDemo))
                    {
                        int startindex = 0;
                        if (listContent.IndexOf(lcDemo) > 3)
                        {
                            startindex = listContent.IndexOf(lcDemo) - 3;
                        }
                        string s = listContent.Substring(startindex, listContent.IndexOf(lcDemo) - startindex);
                        int index = 0;
                        if (listContent.Length - listContent.IndexOf(lcDemo) > 6)
                        {
                            index = 6;
                        }
                        else
                        {
                            index = listContent.Length - listContent.IndexOf(lcDemo);
                        }
                        string last = listContent.Substring(listContent.IndexOf(lcDemo), index);
                        for (int i = 0; i < arr.Count; i++)
                        {
                            bool flag = false;
                            if (s.Contains(arr[i]))
                            {
                                if (i < 10)
                                {
                                    foreach (var mc in dir)
                                    {
                                        if (last.Contains(mc.Key))
                                        {
                                            result.Add(new ListClass(mc.Key, arr[i]));
                                            flag = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {

                                    foreach (var mc in dir)
                                    {
                                        if (last.Contains(mc.Key))
                                        {
                                            result.Add(new ListClass(mc.Key, arr[i]));
                                            flag = true;
                                            break;
                                        }
                                    }

                                }
                                if (flag) break;

                            }
                        }

                    }
                }

            }

            listView1.GridLines = true;
            listView1.Scrollable = true;
            //listView1.MultiSelect = true;
            listView1.Items.Clear();
            imageList1.ImageSize = new Size(1, 50);//宽度和高度值必须大于等于1且不超过256
            this.listView1.SmallImageList = imageList1;
            listView1.Columns.Add("MC", "名称");
            listView1.Columns.Add("Number", "数量");
            listView1.Columns.Add("Image", "图片",600);
            this.listView1.BeginUpdate();
            foreach (var resultItem in result)
            {
                ListViewItem li = new ListViewItem();
                li.Text = resultItem.MC;
                li.SubItems.Add(resultItem.Number.ToString());
                li.ImageIndex = 0;
                this.listView1.Items.Add(li);
            }
            this.listView1.EndUpdate();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = result;
        }

        ////post方法调用接口获取json文件内容
        public object PostFunction(string da)
        {

            string serviceAddress = "http://api.bosonnlp.com/tag/analysis?space_mode=0&oov_level=3&t2s=0&&special_char_conv=0";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("X-Token", "sL7JbPmr.11387.iRBlsTdf6eQA");
            request.AllowAutoRedirect = false;
            string strContent = JsonConvert.SerializeObject(da, new DataTableConverter()); ;
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();

            ////解析josn
            return retString;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "E:\\Compition";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog1.Filter = "文本文件|*.*|txt文件|*.txt|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label3.Text = openFileDialog1.FileName;
                StreamReader st = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default);
                Content = st.ReadToEnd();//读取文件内容
                st.Close();
            }
        }

        public class MyClass
        {
            public List<string> word { get; set; }
            public List<string> tag { get; set; }

        }

        public class ListClass
        {
            public string MC { get; set; }
            public int Number { get; set; }
            Dictionary<string, string> dir = new Dictionary<string, string>() { };
            List<string> arr = new List<string>()
        {
           "二十","十九","十八","十七","十六","十五","十四","十三","十二", "十一","十","九","八","七","六","五","四","三","二","一"
        };
            public ListClass(string mc, string number)
            {
                MC = mc;
                for (int i = 0; i < arr.Count; i++)
                {
                    dir.Add(arr[i], (arr.Count - i).ToString());
                }
                Number = SwithNumber(number);

            }

            public int SwithNumber(string number)
            {
                if (dir.ContainsKey(number))
                {
                    return Convert.ToInt32(dir[number]);
                }
                return 0;
            }
        }


    }
}
