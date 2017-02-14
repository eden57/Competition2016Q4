using Geone.Test.ParseConsole.Common;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geone.Test.ParseConsole.Module
{
    public class ParseModule : NancyModule
    {
        public ParseModule()
        {

            Get["/Parse"] = p =>
            {
                string data = Request.Query.data;
                Console.Write(data);
                 if(string.IsNullOrEmpty(data))
                     return "";
                ChineseParse parse = new ChineseParse();

                List<NumAndObject> list = parse.ParseManager(data);
                List<string> d = new List<string>();
                foreach (NumAndObject numObj in list)
                {
                    d.Add(numObj.Num.ToString() + "*" + numObj.ObjName);     
                }
                if (d.Count == 0)
                {
                    return "";
                }
                else
                {
                    return Response.AsJson<List<string>>(d);
                }
            };
        }
    }
     
}
