using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geone.Test.ParseConsole
{
    public class NumParse
    {
        public NumParse() { }

        public static Dictionary<string, int> InitNumDic()
        {
            Dictionary<string, int> dicNum = new Dictionary<string, int>();
            dicNum.Add("一", 1);
            dicNum.Add("二", 2);
            dicNum.Add("三", 3);
            dicNum.Add("四", 4);
            dicNum.Add("五", 5);
            dicNum.Add("六", 6);
            dicNum.Add("七", 7);
            dicNum.Add("八", 8);
            dicNum.Add("九", 9);
            dicNum.Add("十", 10);
            dicNum.Add("两", 2);
            return dicNum;
        }

    }
}
