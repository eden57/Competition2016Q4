using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Geone.Test.Web
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            //context.Response.Write("Hello World");
            string ss = context.Request.QueryString["data"].ToString();
            string str = ServiceHelper.InvokeHandler(ConfigurationManager.AppSettings["url"] + "data=" + ss);
            context.Response.Write(str);
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