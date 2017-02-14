using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoneWordProcess
{
    class ObjectExact
    {
        /// <summary>
        /// 分句提取
        /// </summary>
        /// <param name="words"></param>
        /// <param name="Object"></param>
        static public void DataExact(string[] words, List<TxtObject> Objects)
        {
            // 字符型数量
            string[] NumCharBank = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十",
                                "1","2","3","4","5","6","7","8","9","10"};
            //数值型数量
            int[] NumBank = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //量词词库简要版
            string[] measureWord = { "串", "个", "亩", "颗", "位", "条", "只", "台", "份", "棵",
                                      "碟","具","壶"};

            //名词词库简要版
            //string[] nounWord = { "瘦削的老者", "", "", "", "", "", "", "", "", "" };




            //Object = new List<string>();
            //Object = new string[] { };
            
            for (int i = 0; i < words.Length; i++)
            {
                TxtObject Object = new TxtObject();

                for (int j=0;j< NumCharBank.Length;j++)
                {
                    int judgeNum = words[i].IndexOf(NumCharBank[j]);
                   
                    if (judgeNum>-1&&words[i].Length- judgeNum>=3)
                    {
                        string tempNumWord = words[i].Substring(judgeNum);
                       


                        for (int m=0;m<measureWord.Length;m++)
                        {
                            int judgeMeasure = tempNumWord.IndexOf(measureWord[m]);
                            if(judgeMeasure>-1&& tempNumWord.Length- judgeMeasure>3)
                            {
                                Object.CharNum = NumCharBank[j];
                                Object.Num = NumBank[j];
                                Object.MeasureWord = measureWord[m];
                                Object.NounWord = words[i].Substring(judgeNum + 2, 2);
                                Objects.Add(Object);
                            }

                        }
                    
                        
                     

                }


            
            }

        }

        /// <summary>
        
        /// </summary>
        //static public void WordExact(string[] words, List<string> NumberObject)
        //{

        //    string[] measureWord = { "串","个","亩","颗", "位", "条" , "只" , "台" ,"份","棵"};
        //    string tempword = "";
        //    for(int i=0;i< words.Length;i++)
        //    {
        //        for (int j = 0; j < measureWord.Length; j++)
        //        {
        //            int judge = words[i].IndexOf(measureWord[j]);
        //            if (judge>0)
        //            {
        //                tempword = words[i].Substring(judge-1,judge+2);
        //            }

        //        }

        //    }
            

        }
    }
}
