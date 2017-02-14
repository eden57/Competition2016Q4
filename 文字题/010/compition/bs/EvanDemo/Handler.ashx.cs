using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Providers.Entities;

namespace Demo
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class Handler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string resultStr = string.Empty;
            resultStr = btnSub();
            context.Response.Write(resultStr);
        }

        List<string> arr = new List<string>()
        {
           "二十","十九","十八","十七","十六","十五","十四","十三","十二", "十一","十","九","八","七","六","五","四","三","二","一"
        };
        List<string> OutDir = new List<string>()
        {
            "数","时", "年纪","家","口","三角","眼觑","刺","声","段","惨祸","话","身材","杯","位",
            "道","肉卖","日","生","饮干","兄弟","廷","话","林","官","怀中","物","意兴","招","农",
            "路","难","身","波","新绿","郭犯","事","弟","月","龙廷","相","诗","时分","时辰","实",
           "步","黑物飞","身","高","首","金","袍","事","都","村","瓦","官儿","腕","声长","法","岳","名","武艺",
            "呼哨","惶","呻吟","座","都","招","祸"
        };
        private Dictionary<string, string> dir = new Dictionary<string, string>();
        private List<string> lc = new List<string>();

        /// <summary>
        /// 处理文本数据
        /// </summary>
        protected string btnSub()
        {
            List<ListClass> result = new List<ListClass>();
            result.Clear();
            string regx = @"一.*?[，：、。\”！]";
            Regex regx1 = new Regex(regx);
            List<string> ListContent = new List<string>();//有对象的数据源
            List<string> ListJuZi = new List<string>();//句子
            string regx2 = @".*?[，：、。\”]";

            //获取小说内容
            string filepath = HttpContext.Current.Server.MapPath(@"~\金庸小说第一回.txt");
            StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default);
            string content = sr.ReadToEnd();
            sr.Close();

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
            //调用API获取词库
            string serviceAddress = "http://api.bosonnlp.com/tag/analysis?space_mode=0&oov_level=3&t2s=0&&special_char_conv=0";
            object obj = HttpMethods.PostFunction(content);
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

            List<Dictionary<string, object>> listDc = new List<Dictionary<string, object>>();
            foreach (ListClass item in result)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < item.Number; i++)
                {
                    sb.Append("<img src=\"Images/" + item.MC + ".png\" style=\"height:50px\"  alt=\"" + item.MC + "\" />");
                }

                Dictionary<string, object> resultDic = new Dictionary<string, object>();
                resultDic.Add("Count", item.Number);
                resultDic.Add("Name", item.MC);
                resultDic.Add("Img", sb.ToString());

                listDc.Add(resultDic);
            }

            string dsdswq = JsonConvert.SerializeObject(listDc);
            return JsonConvert.SerializeObject(listDc);
        }

        /// <summary>
        /// 处理单字符，大写转小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string outStr(string str)
        {
            switch (str)   //switch语句,等于delphi中的case语句
            {
                case ("一"):
                    str = "1";
                    break;
                case ("二"):
                    str = "2";
                    break;
                case ("三"):
                    str = "3";
                    break;
                case ("四"):
                    str = "4";
                    break;
                case ("五"):
                    str = "5";
                    break;
                case ("六"):
                    str = "6";
                    break;
                case ("七"):
                    str = "7";
                    break;
                case ("八"):
                    str = "8";
                    break;
                case ("九"):
                    str = "9";
                    break;
                case ("十"):
                    str = "10";
                    break;
            }
            return str;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}