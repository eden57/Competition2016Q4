using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;


public class ServiceHelper
{
    /// <summary>
    /// 访问handler
    /// </summary>
    /// <param name="serverUrl"></param>
    /// <returns></returns>
    public static string InvokeHandler(string serverUrl)
    {
        try
        {
            System.Net.WebRequest wrt = WebRequest.Create(serverUrl);
            WebResponse wrs = wrt.GetResponse();
            Stream fs = wrs.GetResponseStream();
            StreamReader sr = new StreamReader(fs);
            string s = sr.ReadToEnd();
            return s;
        }
        catch
        {
            return "";
        }
    }
    /// <summary>
    /// 访问webservice
    /// </summary>
    /// <param name="url_wsdl"></param>
    /// <param name="ServiceName"></param>
    /// <param name="method"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static string InvokeMethod(string url_wsdl, string ServiceName, string method, object[] param)
    {
        try
        {
            System.Net.WebClient web = new System.Net.WebClient();
            Stream stream = web.OpenRead(url_wsdl);
            ServiceDescription description = ServiceDescription.Read(stream);
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap";
            importer.Style = ServiceDescriptionImportStyle.Client;
            importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties | System.Xml.Serialization.CodeGenerationOptions.GenerateNewAsync;

            importer.AddServiceDescription(description, null, null);

            CodeNamespace nmspace = new CodeNamespace();
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);

            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.GenerateInMemory = true;
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");

            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);

            if (!result.Errors.HasErrors)
            {
                //return description.TargetNamespace;
                Assembly asm = result.CompiledAssembly;
                Type t = asm.GetType(ServiceName);
                object o = Activator.CreateInstance(t);

                //调用，返回固定格式的xml字符串
                MethodInfo method_InvokeService = t.GetMethod(method);

                object obj_Result = method_InvokeService.Invoke(o, param);

                return obj_Result.ToString();

            }
            else
            {
                return "";
            }

        }
        catch (Exception ex)
        {
            return "";
            //throw new CustomException("201");
        }
    }


    public static string PostMoths(string url, string param)
    {
        string strURL = url;
        System.Net.HttpWebRequest request;
        request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
        request.Method = "POST";
        request.ContentType = "application/json;charset=UTF-8";
        string paraUrlCoded = param;
        byte[] payload;
        payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
        request.ContentLength = payload.Length;
        Stream writer = request.GetRequestStream();
        writer.Write(payload, 0, payload.Length);
        writer.Close();
        System.Net.HttpWebResponse response;
        response = (System.Net.HttpWebResponse)request.GetResponse();
        System.IO.Stream s;
        s = response.GetResponseStream();
        string StrDate = "";
        string strValue = "";
        StreamReader Reader = new StreamReader(s, Encoding.UTF8);
        while ((StrDate = Reader.ReadLine()) != null)
        {
            strValue += StrDate + "\r\n";
        }
        return strValue;
    }

    public static string PostData(string url, string json)
    {
        try
        {
            WebRequest request = WebRequest.Create(url);

            request.Method = "POST";

            string postData = json;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/json;charset=UTF-8";

            request.ContentLength = byteArray.Length;
            
            Stream dataStream = request.GetRequestStream();


            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
        catch
        {
            return "";
        }
    }
}
