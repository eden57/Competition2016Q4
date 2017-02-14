using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geone.Test.ParseConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            ChineseParse parse = new ChineseParse();
            string _v = "2只黄鹂鸣翠柳，一行白鹭上青天";
            Console.WriteLine(_v);
            List<NumAndObject> list = parse.ParseManager(_v);

            List<string> d = new List<string>();
            foreach (NumAndObject numObj in list)
            {
                Console.WriteLine(numObj.Num.ToString() + "*" + numObj.ObjName);
            }
            
            var url = new Url(ConfigurationManager.AppSettings["hostAddress"]);
            var hostConfig = new HostConfiguration();
            hostConfig.UrlReservations = new UrlReservations { CreateAutomatically = true };
            using (var host = new NancyHost(hostConfig, url))
            {
                host.Start();

                Console.WriteLine("解析已启动 " + url);
                Console.ReadLine();
            }
        }
    }
}
