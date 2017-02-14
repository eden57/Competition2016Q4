using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TextReconize
{
    public class ChineaseFiguresToNum
    {
        private static Dictionary<string, long> digit =
             new Dictionary<string, long>()
            { { "一", 1 },
            { "二", 2 },
            { "两", 2 },
            { "三", 3 },
            { "四", 4 },
            { "五", 5 },
            { "六", 6 },
            { "七", 7 },
            { "八", 8 },
            { "九", 9 } };

        private static Dictionary<string, long> word =
            new Dictionary<string, long>()
            { { "百", 100 },
            { "千", 1000 },
            { "万", 10000 },
            { "亿", 100000000 } };

        private static Dictionary<string, long> ten =
            new Dictionary<string, long>()
            { { "十", 10 } };

        public static string TransformToLong(string word)
        {
            string e = "([零一二两三四五六七八九十百千万亿])+";
            MatchCollection mc = Regex.Matches(word, e);

            foreach (Match m in mc)
            {
                word = word.Replace(m.Value, GetChineseNumberToInt(m.Value).ToString());
            }
            return word;
        }

        private static long GetChineseNumberToInt(string s)
        {
            
            long iResult = 0;

            s = s.Replace("零", "");
            int index = 0;
            long t_l = 0, _t_l = 0;
            string t_s;

            while (s.Length > index)
            {
                t_s = s.Substring(index++, 1);

                if (digit.ContainsKey(t_s))
                {
                    _t_l += digit[t_s];
                }
                else if (ten.ContainsKey(t_s))
                {
                    _t_l = _t_l == 0 ? 10 : _t_l * 10;
                }
                else if (word.ContainsKey(t_s))
                {
                    if (word[t_s] > word["千"])
                    {
                        iResult += (t_l + _t_l) * word[t_s];
                        t_l = 0;
                        _t_l = 0;
                        continue;
                    }
                    _t_l = _t_l * word[t_s];
                    t_l += _t_l;
                    _t_l = 0;
                }
            }
            iResult += t_l;
            iResult += _t_l;

            return iResult;
        }
    }
}
