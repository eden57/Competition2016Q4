using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Providers.Entities;

namespace EvanDemo
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class Handler : IHttpHandler
    {
        private string lingci = "串事册丘乘下丈丝两举具美包厘刀分列则剂副些匝队陌陔部出个介令份伙件任倍儋卖亩记双发叠节茎莛荮落蓬蔸巡过进通造遍道遭对尊头套弓引张弯开庄床座庹帖帧席常幅幢口句号台只吊合名吨和味响骑门间阕宗客家彪层尾届声扎打扣把抛批抔抱拨担拉抬拃挂挑挺捆掬排捧掐搭提握摊摞撇撮汪泓泡注浔派湾溜滩滴级纸线组绞统绺综缕缗场块坛垛堵堆堂塔墩回团围圈孔贴点煎熟车轮转载辆料卷截户房所扇炉炷觉斤笔本朵杆束条杯枚枝柄栋架根桄梃样株桩梭桶棵榀槽犋爿片版歇手拳段沓班文曲替股肩脬腔支步武秒秩钟钱铢锊铺锤锭锱章盆盏盘眉眼石码砣碗磴票罗畈番窝联缶耦粒索累緉般艘竿筥筒筹管篇箱簇角重身躯酲起趟面首项领顶颗顷袭群袋";
        public void ProcessRequest(HttpContext context)
        {
            string resultStr = string.Empty;
            string tn = "。" + HttpContext.Current.Request.Form["tn"] + "。";
            if (!tn.Equals(string.Empty))
            {
                resultStr = btnSub(tn);
            }
            context.Response.Write(resultStr);
        }

        /// <summary>
        /// 处理文本数据
        /// </summary>
        /// <param name="tn">文本值</param>
        protected string btnSub(string tn)
        {
            string[] eachLingci = lingci.ToString().Select(x => x.ToString()).ToArray();


            List<object> listDc = new List<object>();

            //string filepath = HttpContext.Current.Server.MapPath(@"~\金庸小说第一回.txt");
            //StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default);
            //string str = sr.ReadToEnd();
            //sr.Close();
            string str = tn;
            var list = Regex.Matches(str, @"([\u4e00-\u9fa5]+|[a-zA-Z0-9]+|、+)+?[、，：。\”！]").OfType<Match>().ToList();
            try
            {

                foreach (var listr in list)
                {
                    //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log.txt", true))
                    //{
                    //    sw.WriteLine(listr.ToString());
                    //}

                    string[] eachchar = listr.ToString().Select(x => x.ToString()).ToArray();
                    string char1 = string.Empty;
                    string char2 = string.Empty;
                    string oresult = string.Empty;
                    foreach (string charli in eachchar)
                    {
                        char1 = outStr(charli);
                        if (char1.Equals("10"))
                        {
                            char2 = char1;
                            continue;
                        }
                        if (char2.Equals("10") && "123456789".Contains(char1))
                        {
                            char1 = "1" + char1;
                            char2 = string.Empty;
                        }
                        else if (char2.Equals("10") && !"123456789".Contains(char1))
                        {
                            char1 = char2 + char1;
                            char2 = string.Empty;
                        }
                        oresult += char1;
                    }
                    List<string> listStr = new List<string>();
                    string filepath_lcck = HttpContext.Current.Server.MapPath(@"~\myciku.txt");
                    Regex r = new Regex(@"\d{1,20}([\u4e00-\u9fa5]+|[a-zA-Z])");
                    Regex rs = new Regex(@"[串事册丘乘下丈丝两举具美包厘刀分列则剂副些匝队陌陔部出个介令份伙件任倍儋卖亩记双发叠节茎莛荮落蓬蔸巡过进通造遍道遭对尊头套弓引张弯开庄床座庹帖帧席常幅幢口句号台只吊合名吨和味响骑门间阕宗客家彪层尾届声扎打扣把抛批抔抱拨担拉抬拃挂挑挺捆掬排捧掐搭提握摊摞撇撮汪泓泡注浔派湾溜滩滴级纸线组绞统绺综缕缗场块坛垛堵堆堂塔墩回团围圈孔贴点煎熟车轮转载辆料卷截户房所扇炉炷觉斤笔本朵杆束条杯枚枝柄栋架根桄梃样株桩梭桶棵榀槽犋爿片版歇手拳段沓班文曲替股肩脬腔支步武秒秩钟钱铢锊铺锤锭锱章盆盏盘眉眼石码砣碗磴票罗畈番窝联缶耦粒索累緉般艘竿筥筒筹管篇箱簇角重身躯酲起趟面首项领顶颗顷袭群袋]");
                    Regex re = new Regex(@"\d{1,20}");
                    MatchCollection ms = r.Matches(oresult);
                    string lsstr = string.Empty;
                    string lsswtr = string.Empty;
                    string myresult = string.Empty;
                    if (ms.Count > 0)
                    {
                        //截取包含数字到中文的一段
                        List<Match> result = ms.OfType<Match>().ToList();
                        foreach (var st in result)
                        {
                            //取出数词
                            lsstr = re.Matches(st.ToString()).OfType<Match>().Last().ToString();
                            if (!lsstr.Equals(string.Empty) && (Convert.ToInt32(lsstr) > 20 || Convert.ToInt32(lsstr) < 1)) { continue; }
                            if (lsstr.Equals(string.Empty)) { continue; }
                            MatchCollection msrs = rs.Matches(st.ToString());
                            if (msrs.Count > 0)
                            {//有量词
                                lsswtr = msrs.OfType<Match>().Last().ToString();
                                if (!lsstr.Equals(string.Empty))
                                {
                                    //有量词&&匹配名词库
                                    myresult = Read(filepath_lcck, st.ToString());
                                    int len = st.ToString().IndexOf(lsswtr);
                                    int llen = st.ToString().Length;
                                    if (len + 1 == llen) { continue; }//判断有量词没有名词
                                    if (!myresult.Equals(string.Empty) && !st.ToString().Contains("数"))//剔除特殊字符
                                    {
                                        //取到名词
                                        StringBuilder sb = new StringBuilder();
                                        for (int i = 0; i < Convert.ToInt32(lsstr); i++)
                                        {
                                            if (!File.Exists(HttpContext.Current.Server.MapPath(@"~\Images\" + myresult.Trim() + ".png")))
                                            {
                                                sb.Append("<label type=text style=\"font-size:12px;color:red\">不存在该图片！</label>");
                                                break;
                                            }
                                            else
                                            {
                                                sb.Append("<img src=Images/" + myresult.Trim() + ".png style=height:50px;padding-right:10px;  alt=" + myresult + " />");
                                            }
                                        }

                                        Dictionary<string, object> resultDic = new Dictionary<string, object>();
                                        resultDic.Add("Count", lsstr);
                                        resultDic.Add("Lingci", 1);
                                        resultDic.Add("Ciku", 1);
                                        resultDic.Add("Name", myresult);
                                        resultDic.Add("Img", sb.ToString());
                                        listDc.Add(resultDic);
                                    }
                                    else if(!st.ToString().Contains("数"))
                                    {
                                        //没取到名词  -有量词 存在不属于词库的名词
                                        StringBuilder sb = new StringBuilder();
                                        sb.Append("<label type=text style=\"font-size:12px;color:red\">此句中可能存在名词，但词库中不存在，可以添加！</label>");

                                        Dictionary<string, object> resultDic = new Dictionary<string, object>();
                                        resultDic.Add("Count", lsstr);
                                        resultDic.Add("Lingci", 1);
                                        resultDic.Add("Ciku", 0);
                                        resultDic.Add("Name", st.ToString());
                                        resultDic.Add("Img", sb.ToString());
                                        listDc.Add(resultDic);
                                    }
                                }
                            }
                            else
                            {
                                //没有量词&&匹配名词库
                                myresult = Read(filepath_lcck, st.ToString());

                                if (!myresult.Equals(string.Empty))
                                {
                                    StringBuilder sb = new StringBuilder();
                                    for (int i = 0; i < Convert.ToInt32(lsstr); i++)
                                    {
                                        sb.Append("<img src=Images/" + myresult + ".png style=height:50px;padding-right:10px;  alt=" + myresult + " />");
                                    }

                                    Dictionary<string, object> resultDic = new Dictionary<string, object>();
                                    resultDic.Add("Count", lsstr);
                                    resultDic.Add("Lingci", 0);
                                    resultDic.Add("Ciku", 1);
                                    resultDic.Add("Name", myresult);
                                    resultDic.Add("Img", sb.ToString());
                                    listDc.Add(resultDic);
                                }
                                else
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("<label type=text style=\"font-size:12px;color:red\">此句中可能存在名词，但词库中不存在，可以添加！</label>");

                                    Dictionary<string, object> resultDic = new Dictionary<string, object>();
                                    resultDic.Add("Count", lsstr);
                                    resultDic.Add("Lingci", 0);
                                    resultDic.Add("Ciku", 0);
                                    resultDic.Add("Name", st.ToString());
                                    resultDic.Add("Img", sb.ToString());
                                    listDc.Add(resultDic);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            string dsdswq = JsonConvert.SerializeObject(listDc);
            return JsonConvert.SerializeObject(listDc);
        }

        /// <summary>
        /// 处理单字符，大写转小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string outStr(string str)
        {
            switch (str)   //switch语句,等于delphi中的case语句
            {
                case ("一"):
                    str = "1";
                    break;
                case ("二"):
                    str = "2";
                    break;
                case ("三"):
                    str = "3";
                    break;
                case ("四"):
                    str = "4";
                    break;
                case ("五"):
                    str = "5";
                    break;
                case ("六"):
                    str = "6";
                    break;
                case ("七"):
                    str = "7";
                    break;
                case ("八"):
                    str = "8";
                    break;
                case ("九"):
                    str = "9";
                    break;
                case ("十"):
                    str = "10";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 读取文件，然后一行一行的输出。
        /// </summary>
        /// <param name="path"></param>
        public string Read(string path, string oResult)
        {
            string restr = string.Empty;
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {

                if (oResult.Contains(line.ToString()))
                {
                    return line.ToString();
                }
            }
            sr.Close();
            return string.Empty;
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