using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 正则查词
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class buff
        {
            public int Number { get; set; }
            public string NumberValueType { get; set; }
        }

        public class sbuff
        {
            public string Value { get; set; }
            public int Number { get; set; }
            public string NumberValueType { get; set; }
        }
        private void btn_fenxi_Click(object sender, EventArgs e)
        {
            List<sbuff> alldata = new List<sbuff>();
            Dictionary<string, buff> allmatchdir = new Dictionary<string, buff>();
            string buff = textBox1.Text;
            MatchCollection allmatch = Regex.Matches(buff, @"([一二三四五六七八九十]{1})[串事册丘乘下丈丝两举具美包厘刀分列则剂副些匝队陌陔部出个介令份伙件任倍儋卖亩记双发叠节茎莛荮落蓬蔸巡过进通造遍道遭对尊头套弓引张弯开庄床座庹帖帧席常幅幢口句号台只吊合名吨和味响骑门间阕宗客家彪层尾届声扎打扣把抛批抔抱拨担拉抬拃挂挑挺捆掬排捧掐搭提握摊摞撇撮汪泓泡注浔派湾溜滩滴级纸线组绞统绺综缕缗场块坛垛堵堆堂塔墩回团围圈孔贴点煎熟车轮转载辆料卷截户房所扇炉炷觉斤笔本朵杆束条杯枚枝柄栋架根桄梃样株桩梭桶棵榀槽犋爿片版歇手拳段沓班文曲替股肩脬腔支步武瓣秒秩钟钱铢锊铺锤锭锱章盆盏盘眉眼石码砣碗磴票罗畈番窝联缶耦粒索累緉般艘竿筥筒筹管篇箱簇角重身躯酲起趟面首项领顶颗顷袭群袋]{1}(.+?)[看盯瞪瞧瞥眯望视拖脱摇摆蹦跳走跑晃点喘穿吃喝拉撒住行吞咀嚼咽舔拾捡建射修绣销浇翻滚爬趴扒划滑叫喊哭笑改盖拿放扔抛弃抬举保护呼焊烧带取领铲割挖埋拍排运动送唱包训说敲架断停听读写画瞄描张涨展站坐开关闭挑调提踢存囤打答按阅越跃扩洗查插擦拆砍剁夺剥拔辞刺丢起启刷花溜赚转砌折遮掩呈递付服赴附扶浮寄记计签斩粘盏镀担谈弹推退逃掏曳背诵整理扫漱戳搓跺绑松解竖切缴搅教捶靠挂升降钉接骑登夹裹散铺扑叠钉贴合流移飘漂装倒吵抄炒煮哼焖蹲炖煎炸捞拌挂刮松扬找招报抱\\pP,，。.]{1}");
            foreach (Match item in allmatch)
            {
                if (item.Success)
                {
                    string key = item.Groups[2].Value;
                    string valuetype = item.Groups[1].Value;
                    if (allmatchdir.ContainsKey(key))
                    {
                        allmatchdir[key].Number++;
                    }
                    else
                    {
                        allmatchdir.Add(key, new buff() { NumberValueType = valuetype, Number = 1 });
                    }
                }
            }
            foreach (var item in allmatchdir)
            {
                alldata.Add(new sbuff() { Number = item.Value.Number, NumberValueType = item.Value.NumberValueType, Value = item.Key });
            }
            dataGridView1.DataSource = alldata;
        }
    }
}
