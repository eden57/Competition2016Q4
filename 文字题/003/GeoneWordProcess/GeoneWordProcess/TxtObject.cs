using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoneWordProcess
{
    /// <summary>
    /// 对象类
    /// </summary>
    public class TxtObject
    {
        /// <summary>
        /// 数字数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 字符数量
        /// </summary>
        public string CharNum { get; set; }
        /// <summary>
        /// 量词
        /// </summary>
        public string MeasureWord { get; set; }
        /// <summary>
        /// 名词
        /// </summary>
        public string NounWord { get; set; }

        /// <summary>
        /// 返回对象
        /// </summary>
        /// <returns></returns>
        public string GetTxtObject()
        {
            return CharNum + MeasureWord + NounWord;
        }

    }
}
