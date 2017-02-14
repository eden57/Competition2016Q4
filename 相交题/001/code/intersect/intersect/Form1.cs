using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace intersect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            //label5.Text = "分析中，请稍候！";
            //Thread fThread = new Thread(new ThreadStart(SleepT));
            //fThread.Start();


            if (MessageBox.Show("开始分析，数据较多，是否等待！") != DialogResult.OK)
                return;


            string strConn = this.GetConstr();
            if (strConn == "")
                return;

            SqlConnection conn = new SqlConnection(strConn);
            try
            {
                String sqlId = "EXEC IntersectionTB";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlId, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "MyTable");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MyTable";
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("分析失败:" + ex.Message);
                label5.Text = "";
                return;
            }
            finally
            {
                conn.Close();
            }//关闭数据库

            MessageBox.Show("分析结束");
            label5.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strConn=this.GetConstr();
            if (strConn == "")
                return;

            SqlConnection conn = new SqlConnection(strConn);
            try
            {
                conn.Open();
                String sqlId = GetSQL();

                //sqlId.Replace("\r\n", "  ");
                //sqlId.Replace('\n', "                                                  ");
                
                SqlCommand cmd = new SqlCommand(sqlId, conn);

            cmd.ExecuteNonQuery();

            conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("分析失败:" + ex.Message);
                return;
            }
            finally
            {
                conn.Close();
            }//关闭数据库
            MessageBox.Show("分析结束");

            //try
            //{
            //    String sqlId = richTextBox1.Text;
            //    conn.Open();

            //    SqlCommand cmd = new SqlCommand(sqlId, conn);

            //    cmd.ExecuteNonQuery();

            //    conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("分析失败:" + ex.Message);
            //    return;
            //}
            //finally
            //{
            //    conn.Close();
            //}//关闭数据库
            //MessageBox.Show("分析结束");
           
        }

        /// <summary>
        /// 得到数据库连接
        /// </summary>
        /// <returns></returns>
        private string GetConstr()
        {
            //连接数据库读取数据，为DataGridView赋值。</p>
            string lServer = txtServer.Text.Trim();
            string lserId = txtUserId.Text.Trim();
            string lPassword = txtPassword.Text.Trim();
            string lDatabase = txtDatabase.Text.Trim();

            if (lServer == "" || lserId == "" || lPassword == "" || lDatabase == "")
            {
                MessageBox.Show("数据库连接不可为空");
                return "";
            }

            String strConn = @"server=" + lServer + ";uid=" + lserId + ";pwd=" + lPassword + "; database = " + lDatabase;
            return strConn;
        }


        private string GetSQL()
        {
            string path =  Application.StartupPath+@"\SQL.txt"; 

            // Open the file to read from. 
            using (StreamReader sr = File.OpenText(path)) 
            { 
            //string s = ""; 
            //while ((s = sr.ReadLine()) != null) 
            //{ 
            //    Console.WriteLine(s); 
            //} 
                    return sr.ReadToEnd();
            }
        }


        #region 进度条
       /* //代理
       private delegate void SetPos(int ipos,string vinfo);


        //第三步：进度条值更新函数（参数必须跟声明的代理参数一样）

        private void SetTextMesssage(int ipos,string vinfo)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMesssage);
                this.Invoke(setpos, new object[] { ipos,vinfo });
            }
            else
            {
                this.label1.Text = ipos.ToString() + "/1000";
                this.progressBar1.Value = Convert.ToInt32(ipos);
                this.textBox1.AppendText(vinfo);
            }
        }


        //第四步：函数实现

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

//第五步：新的线程执行函数：

        private void SleepT()
        {
            for (int i = 0; i < 500; i++)
            {
                System.Threading.Thread.Sleep(10);
                SetTextMesssage(100*i/500,i.ToString()+"\r\n");
            }
        }
        */
        #endregion j进度条
    }
}
