/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using PanGu;
using PanGu.Dict;
using System.IO;

namespace Demo
{
    public partial class FormDemo : Form
    {
        string _InitSource = string.Empty;

        private PanGu.Match.MatchOptions _Options;
        private PanGu.Match.MatchParameter _Parameters;

        public FormDemo()
        {
            InitializeComponent();

            StreamReader sr = new StreamReader("sdyxz.txt");
            _InitSource = sr.ReadToEnd();
            sr.Close();
        }

        private void FormDemo_Load(object sender, EventArgs e)
        {
            textBoxSource.Text = _InitSource;
            PanGu.Segment.Init();

            PanGu.Match.MatchOptions options = PanGu.Setting.PanGuSettings.Config.MatchOptions;
            checkBoxFreqFirst.Checked = options.FrequencyFirst;
            checkBoxFilterStopWords.Checked = options.FilterStopWords;
            checkBoxMatchName.Checked = options.ChineseNameIdentify;
            checkBoxMultiSelect.Checked = options.MultiDimensionality;
            checkBoxEnglishMultiSelect.Checked = options.EnglishMultiDimensionality;
            checkBoxForceSingleWord.Checked = options.ForceSingleWord;
            checkBoxTraditionalChs.Checked = options.TraditionalChineseEnabled;
            checkBoxST.Checked = options.OutputSimplifiedTraditional;
            checkBoxUnknownWord.Checked = options.UnknownWordIdentify;
            checkBoxFilterEnglish.Checked = options.FilterEnglish;
            checkBoxFilterNumeric.Checked = options.FilterNumeric;
            checkBoxIgnoreCapital.Checked = options.IgnoreCapital;
            checkBoxEnglishSegment.Checked = options.EnglishSegment;
            checkBoxSynonymOutput.Checked = options.SynonymOutput;
            checkBoxWildcard.Checked = options.WildcardOutput;
            checkBoxWildcardSegment.Checked = options.WildcardSegment;
            checkBoxCustomRule.Checked = options.CustomRule;

            if (checkBoxMultiSelect.Checked)
            {
                checkBoxDisplayPosition.Checked = true;
            }

            PanGu.Match.MatchParameter parameters = PanGu.Setting.PanGuSettings.Config.Parameters;

            numericUpDownRedundancy.Value = parameters.Redundancy;
            numericUpDownFilterEnglishLength.Value = parameters.FilterEnglishLength;
            numericUpDownFilterNumericLength.Value = parameters.FilterNumericLength;

            //str = Microsoft.VisualBasic.Strings.StrConv(str, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);

        }

        private void DisplaySegment()
        {
            DisplaySegment(false);
        }

        ICollection<WordInfo> words = null;
        private void DisplaySegment(bool showPosition)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Segment segment = new Segment();
            wordList = null;
            words = segment.DoSegment(textBoxSource.Text, _Options, _Parameters);

            watch.Stop();

            labelSrcLength.Text = textBoxSource.Text.Length.ToString();

            labelSegTime.Text = watch.Elapsed.ToString();
            if (watch.ElapsedMilliseconds == 0)
            {
                labelRegRate.Text = "无穷大";
            }
            else
            {
                labelRegRate.Text = ((double)(textBoxSource.Text.Length / watch.ElapsedMilliseconds) * 1000).ToString();
            }


            if (checkBoxShowTimeOnly.Checked)
            {
                return;
            }

            StringBuilder wordsString = new StringBuilder();
            foreach (WordInfo wordInfo in words)
            {
                if (wordInfo == null)
                {
                    continue;
                }

                if (showPosition)
                {

                    wordsString.AppendFormat("{0}({1},{2})/", wordInfo.Word, wordInfo.Position, wordInfo.Rank);
                    //if (_Options.MultiDimensionality)
                    //{
                    //}
                    //else
                    //{
                    //    wordsString.AppendFormat("{0}({1})/", wordInfo.Word, wordInfo.Position);
                    //}
                }
                else
                {
                    wordsString.AppendFormat("{0}/", wordInfo.Word);
                }
            }

            textBoxSegwords.Text = wordsString.ToString();


        }

        private void DisplaySegmentAndPostion()
        {
            DisplaySegment(true);
        }

        private void UpdateSettings()
        {
            _Options.FrequencyFirst = checkBoxFreqFirst.Checked;
            _Options.FilterStopWords = checkBoxFilterStopWords.Checked;
            _Options.ChineseNameIdentify = checkBoxMatchName.Checked;
            _Options.MultiDimensionality = checkBoxMultiSelect.Checked;
            _Options.EnglishMultiDimensionality = checkBoxEnglishMultiSelect.Checked;
            _Options.ForceSingleWord = checkBoxForceSingleWord.Checked;
            _Options.TraditionalChineseEnabled = checkBoxTraditionalChs.Checked;
            _Options.OutputSimplifiedTraditional = checkBoxST.Checked;
            _Options.UnknownWordIdentify = checkBoxUnknownWord.Checked;
            _Options.FilterEnglish = checkBoxFilterEnglish.Checked;
            _Options.FilterNumeric = checkBoxFilterNumeric.Checked;
            _Options.IgnoreCapital = checkBoxIgnoreCapital.Checked;
            _Options.EnglishSegment = checkBoxEnglishSegment.Checked;
            _Options.SynonymOutput = checkBoxSynonymOutput.Checked;
            _Options.WildcardOutput = checkBoxWildcard.Checked;
            _Options.WildcardSegment = checkBoxWildcardSegment.Checked;
            _Options.CustomRule = checkBoxCustomRule.Checked;

            _Parameters.Redundancy = (int)numericUpDownRedundancy.Value;
            _Parameters.FilterEnglishLength = (int)numericUpDownFilterEnglishLength.Value;
            _Parameters.FilterNumericLength = (int)numericUpDownFilterNumericLength.Value;

        }

        private void buttonSegment_Click(object sender, EventArgs e)
        {
            _Options = PanGu.Setting.PanGuSettings.Config.MatchOptions.Clone();
            _Parameters = PanGu.Setting.PanGuSettings.Config.Parameters.Clone();

            UpdateSettings();

            if (checkBoxDisplayPosition.Checked)
            {
                DisplaySegmentAndPostion();
            }
            else
            {
                DisplaySegment();
            }
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            _Options = PanGu.Setting.PanGuSettings.Config.MatchOptions;
            _Parameters = PanGu.Setting.PanGuSettings.Config.Parameters;

            UpdateSettings();

            PanGu.Setting.PanGuSettings.Save("PanGu.xml");
        }


        List<WordInfo> wordList = null;
        private void btnShowResult_Click(object sender, EventArgs e)
        {
            if (words == null)
                return;

            wordList = new List<WordInfo>();
            wordList.AddRange(words);

            List<WordInfo> digitalWord = new List<WordInfo>();
            int index = 0;
            foreach (WordInfo item in words)
            {
                //词性判读
                //if (item.Pos == POS.POS_A_M || item.Pos == POS.POS_A_Q || item.Pos == POS.POS_D_MQ)
                //{

                if (ConvertToDigital(item.Word[0]) != "-1")
                {
                    //digitalWord.Add(item);

                    ProcessWord(index, item, wordList);
                }
                //}

                index++;
            }


            List<string> colorList = new List<string>()
            {
                "aliceBlue",
                "yellow",
                "green",
                "pink",
                "red",
                "maroon",
                "olive",
                "orange",
                "purple",
                "springGreen"
            };
            string result = string.Empty;
            foreach (var item in segmentResult)
            {
                int colorIndex = new Random().Next(0, colorList.Count);
                for (int i = 0; i < item.Value; i++)
                {
                    this.richTextBox2.Text += string.Format("{0}", item.Key);
                }

                this.richTextBox1.Text += "\r\n";

            }
        }


        private string ConvertToDigital(char ch)
        {
            switch (ch)
            {
                case '一': return "1";
                case '二': return "2";
                case '两': return "2";
                //case '双': return "2";
                case '三': return "3";
                case '四': return "4";
                case '五': return "5";
                case '六': return "6";
                case '七': return "7";
                case '八': return "8";
                case '九': return "9";
                case '十': return "10";
                case '百': return "INF";
                case '千': return "INF";
                case '万': return "INF";
                default: return "-1";
            }
        }
        //单位量词
        List<string> DN = new List<string> { "个", "把", "只", "头", "颗", "座", "件", "朵", "块", "瓶", "本", "双", "匹", "堆", "伙", "排", "天", "面", "家", "对", "角", "命", "刀", "推", "段", "般", "阵", "碟", "眼", "对", "心", "旦", "杯", "口" };//根据真实统计扩充
        Dictionary<string, int> segmentResult = new Dictionary<string, int>();
        private void ProcessWord(int pos, WordInfo word, List<WordInfo> words)
        {
            string digitalStr = string.Empty;
            int chpos = 0;
            foreach (var ch in word.Word.ToCharArray())
            {
                string intResult = ConvertToDigital(ch);
                if (intResult == "-1")
                    break;
                digitalStr += intResult;
                chpos++;
            }

            int digital = -1;
            int.TryParse(digitalStr, out digital);

            //string chStr = word.Word;
            //if(pos+1 < words.Count -1)
            //{
            //    chStr += ":" + words[pos + 1];
            //}
            //if (segmentResult.ContainsKey(chStr))
            //    segmentResult[chStr] = segmentResult[chStr] + 1;
            //else
            //    segmentResult.Add(chStr, digital);
            //return;

            char spetialCh = word.Word.ToCharArray().Length > 1 ? word.Word.ToCharArray()[1] : '姓';
            if (spetialCh == '姓' || spetialCh == '倍')
            {
                return;
            }

            string tempStr = string.Empty;
            if (chpos != word.Word.Length)
            {
                if (word.Word.ToCharArray().Length == 2 && (!DN.Contains(word.Word.ToCharArray()[1].ToString())))
                {
                    //判断第二个词是不是名词
                    //如果是，则添加到集合；如果不是，则跳过
                    if (!segmentResult.ContainsKey(word.Word))
                        segmentResult.Add(word.Word, digital);

                }
                else if (word.Word.ToCharArray().Length > 3)
                {
                    //if (!segmentResult.ContainsKey(word.Word))
                    //    segmentResult.Add(word.Word, digital);
                    //return;

                    tempStr = word.Word;
                }


            }


            int startIndex = pos + 1;
            POS wpos = words[startIndex].Pos;
            for (int i = startIndex, cnt = 0; cnt < 5 && i < words.Count; i++, cnt++)//向后最多寻找30个词
            {
                if (words[i].Pos == POS.POS_D_N)
                {
                    //if ((i - pos) == 1)
                    //{
                    //    tempStr += words[i].Word;
                    //    break;
                    //}
                    //else if (words[i - 1].Pos == POS.POS_D_A)
                    //{
                    //    tempStr += "," + words[i].Word;
                    //    break;
                    //}

                    tempStr = words[i].Word;
                    break;
                }
                else
                {
                    wpos = words[i].Pos;
                }

            }
            if (!string.IsNullOrEmpty(tempStr) && (!segmentResult.ContainsKey(tempStr)))
                segmentResult.Add(tempStr, digital);

        }

        private void btnTextAnalysis_Click(object sender, EventArgs e)
        {
            char[] seperatorWord = new char[] { '，', '。', '？', '！', '；',',','.',';','?','!' };

            string sourceText = string.Empty;
            using (var sr = new StreamReader("sdyxz.txt"))
            {
                sourceText = sr.ReadToEnd();
            }
            string[] sententceList = sourceText.Split(seperatorWord, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in sententceList)
            {
                ProcessSentence(item);
            }

            //this.richTextBox1.Text = "";
            //foreach (var item in matchedWords)
            //{
            //    this.richTextBox1.Text += item.Word + "\r\n";
            //}

        }

        Dictionary<WordInfo, string> matchedDic = new Dictionary<WordInfo, string>();
        List<WordInfo> matchedWords = new List<WordInfo>();
        //特殊词语集合
        List<char> spetialWordChar = new List<char>()
        {
            '月',
            '年',
            '日',
            '姓',
            '季',
            '命',
            '起'
        };
        private void ProcessSentence(string sentence)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Segment segment = new Segment();
            var wordCol = new List<WordInfo>();
            wordCol.AddRange(segment.DoSegment(sentence, _Options, _Parameters));
           
            watch.Stop();

            int index = 0;
            foreach (var item in wordCol)
            {
                if (ConvertToDigital(item.Word[0]) != "-1")
                {
                    matchedWords.Add(item);
                    matchedDic.Add(item, sentence);

                    if (item.Word.ToCharArray().Length > 1)
                    {
                        char ch = item.Word[1];
                        if (spetialWordChar.Contains(ch))
                            return;
                    }

                    //获取数量
                    string digitalStr = string.Empty;
                    int chpos = 0;
                    foreach (var ch in item.Word.ToCharArray())
                    {
                        string intResult = ConvertToDigital(ch);
                        if (intResult == "-1")
                            break;
                        if (intResult != "10" && intResult != "INF")
                            digitalStr = intResult;
                        else if (chpos > 1 && intResult == "10")
                            digitalStr += "0";
                        else
                            return;
                        chpos++;
                    }
                    int digital = -1;
                    int.TryParse(digitalStr, out digital);
                    if (digital > 20)
                        return;

                    if(wordCol.Count == 1)
                    {
                        this.richTextBox1.Text += string.Format("{0}:{1}\r\n", digital, item.Word.Substring(chpos, item.Word.Length - chpos - 1));
                        for (int i = 0; i < digital; i++)
                        {
                            this.richTextBox2.Text += item.Word.Substring(chpos, item.Word.Length - chpos - 1) + " ";
                        }
                        this.richTextBox2.Text += "\r\n";
                        return;
                    }


                    //寻找量词之后的名词，
                    //(1)如果量词之后是动词，则返回
                    //（2）如果在量词之后找到不是紧接着量词的名词，则判断该量词前面是否是动词，
                    //如果是动词，说明该句子是定于成分，继续往后面寻找前置词不是动词的名词

                    if (wordCol.Count < index && wordCol.Count > 1 && wordCol[index+1].Pos == POS.POS_D_V)
                        break;

                    for (int i = index+1; i < wordCol.Count; i++)
                    {
                        if(wordCol[i].Pos == POS.POS_D_N && wordCol[i-1].Pos != POS.POS_D_V)
                        {
                            this.richTextBox1.Text += string.Format("{0}:{1}\r\n", digital, wordCol[i].Word);

                            for (int j = 0; j < digital; j++)
                            {
                                this.richTextBox2.Text += wordCol[i] + " ";
                            }
                            this.richTextBox2.Text += "\r\n";
                            return;
                        }
                    }
                }

                index++;
            }
        }
    }
}
