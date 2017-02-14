using Nancy;
using Nancy.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Geone.Test.ParseConsole.Common
{
    public class NancyHelper
    {
        /// <summary>
        /// 获取body中的参数
        /// </summary>
        /// <returns></returns>
        public static string GetParmsStr(RequestStream body)
        {
            try
            {
                Nancy.IO.RequestStream stream = body;
                byte[] buffer = new byte[(int)stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                string parms = System.Text.Encoding.UTF8.GetString(buffer);
                return parms;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将body中的参数字符转换为对象
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetParmsObj(string str)
        {
            try
            {
                Dictionary<string, string> obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
                return obj;
            }
            catch (Exception)
            {
                return null;
            }

        }

       
    }
}