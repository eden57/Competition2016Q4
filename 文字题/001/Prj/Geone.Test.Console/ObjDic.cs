using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geone.Test.ParseConsole
{
    public class ObjDic
    {
        public ObjDic() { }

        public static List<string> InitObjList()
        {
            try
            {
                List<string> list = new List<string>(); 
                string v = ConfigurationManager.AppSettings["obj"];
                string[] vAttr = v.Split(',');
                foreach (string str in vAttr)
                {
                    StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\data\\" + str + ".txt", Encoding.UTF8);
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string content = line.ToString();
                        var d = list.Union(content.Split(',').ToList());
                        list = d.ToList<string>();
                    } 
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
