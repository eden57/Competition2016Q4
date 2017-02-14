<%@ WebHandler Language="C#" Class="GHandler" %>

using System;
using System.Web;
using System.Net;
using System.IO;
using System.Configuration;
using Geone.Data;

public class GHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string postStr = context.Server.UrlDecode((new System.IO.StreamReader(context.Request.InputStream)).ReadToEnd());

        if (postStr.ToLower().IndexOf("http") == -1)
        {
            postStr = ConfigurationManager.AppSettings["serverUrl"] + postStr;
        }

        System.Net.WebRequest wrt = WebRequest.Create(postStr);
        WebResponse wrs = wrt.GetResponse();
        Stream fs = wrs.GetResponseStream();
        StreamReader sr = new StreamReader(fs);
        string s = sr.ReadToEnd();

        context.Response.Write(s);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}