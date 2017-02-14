using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace exam4
{
    public partial class FrmMain : Form
    {
        public string strSQL;
        public FrmMain()
        {
            InitializeComponent();
            cmbIdValidate.Text = cmbIdValidate.Items[0].ToString();
            cmbServerName.Text = cmbServerName.Items[0].ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbIdValidate.Text == "SQL Server 身份验证")
                {
                    strSQL = "server=" + cmbServerName.Text.Trim() + ";database=" + cmbDBname.Text.Trim() + ";uid=" + txtUserLogin.Text.Trim() + ";pwd=" + txtUserPwd.Text.Trim();       
                }
                else
                {
                    strSQL = "server=" + cmbServerName.Text.Trim() + ";database=" + cmbDBname.Text.Trim() + ";Trusted_Connection=SSPI";
                }
                DBHelper.M_str_sqlcon = strSQL;
                DBHelper.My_con = DBHelper.getcon();
                if (DBHelper.My_con.State == ConnectionState.Open)
                {
                    //DBHelper.My_con.Close();
                    this.Hide();
                    FrmIntersect frm = new FrmIntersect();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("数据库连接失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("数据库连接失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(DBHelper.My_con!=null)
            {
                DBHelper.con_close();
            }
            
            Application.Exit();
        }

        private void cmbIdValidate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdValidate.Text == "Windows 身份验证")
            {
                txtUserLogin.Enabled = false;
                txtUserPwd.Enabled = false;
            }
            else
            {
                txtUserLogin.Enabled = true;
                txtUserPwd.Enabled = true;
            }
        }
    }
}
