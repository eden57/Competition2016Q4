using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace EvanDemo
{
    /// <summary>
    /// AddHandler 的摘要说明
    /// </summary>
    public class AddHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string outString = string.Empty;
            string filepath_lcck = HttpContext.Current.Server.MapPath(@"~\myciku.txt");
            string resultStr = string.Empty;
            string tn = HttpContext.Current.Request.Form["tn"].Trim();
            if (!tn.Equals(string.Empty))
            {
                try
                {
                    //var Encoding= TxtFileEncoding.GetEncoding(filepath_lcck);
                    string[] strArray = tn.Split('，');
                    StreamReader sr = new StreamReader(filepath_lcck, System.Text.Encoding.Default);
                    string strck = sr.ReadToEnd();
                    sr.Close();
                    foreach (string str in strArray)
                    {
                        if (strck.Contains(str))
                        {
                            outString = "已经包含该名词【" + str + "】";
                            break;
                        }
                        else
                        {
                            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath_lcck, true, Encoding.Default))
                            {
                                sw.WriteLine(str);
                                sw.Close();
                            }
                            StreamReader sradd = new StreamReader(filepath_lcck, System.Text.Encoding.Default);
                            string sty = sradd.ReadToEnd();
                            if (sty.Contains(str))
                            {
                                outString = "新增成功";
                            }
                            else
                            {
                                outString = "新增失败";
                            }
                            sradd.Close();
                        }
                    }
                }
                catch (Exception ex)
                {

                    outString = "新增失败:" + ex.Message;
                }

            }
            context.Response.Write(outString);
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