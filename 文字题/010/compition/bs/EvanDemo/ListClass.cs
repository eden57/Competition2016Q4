using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo
{
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