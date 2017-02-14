using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.China;
using System.IO;
using System.Configuration;
namespace Geone.Test.ParseConsole
{
    public class ChineseParse
    {
        public static Dictionary<string, int> dicNum = new Dictionary<string, int>();
        public static List<string> listObj = new List<string>();
        public ChineseParse()
        {
            dicNum = NumParse.InitNumDic();
            listObj = ObjDic.InitObjList();
        }
        public string Parse(string input)
        {
            StringBuilder sb = new StringBuilder();
            sb.Remove(0, sb.Length);
            string t1 = "";
            int i = 0;
            Analyzer analyzer = new Lucene.China.ChineseAnalyzer();
            StringReader sr = new StringReader(input);
            TokenStream stream = analyzer.TokenStream(null, sr);

            long begin = System.DateTime.Now.Ticks;
            Token t = stream.Next();
            while (t != null)
            {
                t1 = t.ToString();   //显示格式： (关键词,0,2) ，需要处理
                t1 = t1.Replace("(", "");
                char[] separator = { ',' };
                t1 = t1.Split(separator)[0];

                sb.Append(t1 + "###");
                t = stream.Next();
                i++;
            }

            return sb.ToString();

        }

        public string[] SplitStr(string input)
        {
            string pStr = "。|！|，|；|,";
            string[] pStrArray = pStr.Split('|');

            Dictionary<int, string> dic = new Dictionary<int, string>();
            int cnt = 0;

            //foreach (string str in pStrArray)
            //{
            //    string[] str1 = input.Split(st
            //}
            string[] sd = input.Split(pStrArray, StringSplitOptions.None);

            return sd;
        }


        public List<NumAndObject> ParseManager(string input)
        {
            string[] firstArr = SplitStr(input);
            List<NumAndObject> list = new List<NumAndObject>();

            foreach (string str in firstArr)
            {
                #region
                string rs = Parse(str);
                //分词后 
                string[] rsArr = rs.Split(new string[] { "###" }, StringSplitOptions.None);
                NumAndObject obj = new NumAndObject();

                //循环每组，判断是否有 数字和对象
                foreach (string _str in rsArr)
                {
                    int rs_int;
                    if (int.TryParse(_str, out rs_int))
                    {
                        obj.Num = rs_int;
                    }
                    if (dicNum.Keys.Contains(_str)  )
                    {
                        obj.Num = dicNum[_str];
                    }
                    else
                    {
                        var _list = listObj.Where(p => p == _str);
                        if (_list != null && _list.Count() > 0)
                        {
                            obj.ObjName = _list.First();
                        }

                    }
                }
                if (obj.Num == 0 || string.IsNullOrEmpty(obj.ObjName))
                {
                    continue;
                }
                else
                {
                    list.Add(obj);
                }
                #endregion
            }
            return list;
        }
    }
}
