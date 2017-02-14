using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        getTextInfo();
    }

 

    public void getTextInfo()
    {
        string[] JH;
        string[] DH;
        string[] SHUZI = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "双", "两" };
        string SHUZIinfo = "一二三四五六七八九十双两";
        string filePath = Server.MapPath(@"~\files\SDYXZ.txt");
        string line = string.Empty;
        string sb = string.Empty;
        string sb2 = string.Empty;
        string lc = string.Empty;
        string lcck = string.Empty;
        string[] mylc;
        string[] mylcck;
        string sb3 = string.Empty;
        string imgInfo = string.Empty;
        string bjx = string.Empty;
        string sd = string.Empty;
        string[] mybjx;
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("GB2312"));





        //百家姓词库
        string filepath_bjx = Server.MapPath(@"~\files\BJX.txt");
        FileStream fs_bjx = new FileStream(filepath_bjx, FileMode.Open, FileAccess.Read);
        StreamReader sr_bjx = new StreamReader(fs_bjx, Encoding.GetEncoding("GB2312"));
        while (!sr_bjx.EndOfStream)
        {
            bjx += sr_bjx.ReadLine().ToString();
        }
        mybjx = bjx.Split(' ');

        //遍历量词库
        string filepath_lcck = Server.MapPath(@"~\files\LCCK.txt");
        FileStream fs_lcck = new FileStream(filepath_lcck, FileMode.Open, FileAccess.Read);
        StreamReader sr_lcck = new StreamReader(fs_lcck, Encoding.GetEncoding("GB2312"));
        while (!sr_lcck.EndOfStream)
        {
            lcck += sr_lcck.ReadLine().ToString() + "，";
        }

        mylcck = lcck.Split('，');


        //遍历名词库
        string filepath_lc = Server.MapPath(@"~\files\MCCK.txt");
        FileStream fs_lc = new FileStream(filepath_lc, FileMode.Open, FileAccess.Read);
        StreamReader sr_lc = new StreamReader(fs_lc, Encoding.GetEncoding("GB2312"));
        while (!sr_lc.EndOfStream)
        {
            lc += sr_lc.ReadLine().ToString() + ",";
        }
        mylc = lc.Split(',');

        //将逗号句竖排
        while (!sr.EndOfStream)
        {
            sb += sr.ReadLine();
        }


        JH = sb.Split('。');
        for (int i = 0; i < JH.Length; i++)
        {
            //逗号之间为一句话
            DH = JH[i].Split('，');
            for (int j = 0; j < DH.Length; j++)
            {

                for (int k = 0; k < SHUZI.Length; k++)
                {
                    //逗号之间的话 中是否出现一到二十中的数字
                    if (DH[j].ToString().Contains(SHUZI[k].ToString()))
                    {
                        //排除没有量词的
                        for (int lc2 = 0; lc2 < mylcck.Length; lc2++)
                        {
                            if (DH[j].ToString().Contains(mylcck[lc2].ToString()) && mylcck[lc2].ToString() != "")
                            {
                                sb2 += DH[j].ToString() + "\r\n";
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        TextBox1.Text = sb2;
        string[] sArray = sb2.Split('\n');
        string myshuzi = string.Empty;
        int shuziinfo = 0;
        imgInfo += "<table id='table-1'>";
        for (int s = 0; s < sArray.Length; s++)
        {
            string sArrayInfo = sArray[s].ToString();
            char[] charList = sArrayInfo.ToArray();

            for (int mc = 0; mc < mylc.Length; mc++)
            {
                if (sArrayInfo.Contains(mylc[mc].ToString()) && mylc[mc].ToString() != "")
                {

                    int count = sArrayInfo.IndexOf(mylc[mc].ToString());
                    for (int result = count - 1; result >= 0; result--)
                    {
                        for (int kk = 0; kk < SHUZI.Length; kk++)
                        {
                            if (charList[result].ToString() == SHUZI[kk].ToString())
                            {

                                myshuzi = SHUZI[kk].ToString();
                                //遍历百家姓，如果数字前面是一个或前面第二个是姓，则取消
                                if (result - 1 >= 0)
                                {
                                    for (int bjx2 = 0; bjx2 < mybjx.Length; bjx2++)
                                    {
                                        if (charList[result - 1].ToString() == mybjx[bjx2].ToString())
                                        {
                                            myshuzi = "";
                                            break;
                                        }
                                    }
                                    if (result - 2 >= 0)
                                    {
                                        for (int bjx3 = 0; bjx3 < mybjx.Length; bjx3++)
                                        {
                                            if (charList[result - 2].ToString() == mybjx[bjx3].ToString())
                                            {
                                                myshuzi = "";
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        if (myshuzi != "")
                        {
                            break;
                        }
                    }

                    shuziinfo = getTransfor(myshuzi);
                    if (shuziinfo != 0)
                    {
                        imgInfo += "<tr><td>";

                        imgInfo += "<h1>数量:" + shuziinfo.ToString() + "</h1>";
                        imgInfo += "</td>";
                        imgInfo += "<td>";
                        imgInfo += "<h1>" + mylc[mc].ToString() + "</h1>";
                        imgInfo += "</td>";
                        imgInfo += "<td>";
                        for (int tt = 0; tt < shuziinfo; tt++)
                        {
                            imgInfo += "<img src='image/" + mylc[mc].ToString() + ".jpg'/>";
                        }
                        imgInfo += "</td></tr>";
                        myshuzi = string.Empty;
                    }
                }
            }
        }

        imgInfo += "</table>";
        divInfo.InnerHtml = imgInfo;
    }

    public int getTransfor(string str)
    {
        if (str == "一")
        {
            return 1;
        }
        else if (str == "二")
        {
            return 2;
        }
        else if (str == "三")
        {
            return 3;
        }
        else if (str == "四")
        {
            return 4;
        }
        else if (str == "五")
        {
            return 5;
        }
        else if (str == "六")
        {
            return 6;
        }
        else if (str == "七")
        {
            return 7;
        }
        else if (str == "八")
        {
            return 8;
        }
        else if (str == "九")
        {
            return 9;
        }
        else if (str == "十")
        {
            return 10;
        }
        else if (str == "双")
        {
            return 2;
        }
        else if (str == "两")
        {
            return 2;
        }
        else if (str == "")
        {
            return 0;
        }


        return 0;
    }
}