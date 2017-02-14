using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace GraphicsIntersectWin
{
    public partial class Form1 : Form
    {
        private string connStr;
        public Form1()
        {
            InitializeComponent();
        }

        private void beginCheck_Click(object sender, System.EventArgs e)
        {
            connStr = string.Format("server={0};database={1};uid={2};pwd={3};Connect Timeout=0", txtIP.Text.Trim(), txtdbName.Text.Trim(), txtUserName.Text.Trim(), txtPassword.Text.Trim());
            try
            {
                if (SqlHelper.CheckDbState(connStr))
                {
                    //ProgressBar ProgressBar = new ProgressBar();
                    //ProgressBar.ShowDialog();
                    string sql = @"with A as(
	                                    select   OBJECTID,SHAPE,SHAPE.STEnvelope() as extentA from [A_Qiancy].[dbo].[AA01交通水利用地图斑2014]
                                    ),
                                    B as (
	                                    select   OBJECTID,SHAPE,SHAPE.STEnvelope() as extentB from [A_Qiancy].[dbo].[AB02非建设用地图斑2009]
                                    ),
                                    AB AS (
	                                    select   A.OBJECTID as AID,B.OBJECTID as BID,A.SHAPE AS Ashape,B.SHAPE as Bshape from A,B  where A.extentA.STIntersects(B.extentB)=1
                                    ),
                                    UnionShapeS as (
	                                    SELECT AID,BID,Ashape.STUnion(Bshape) as UShape FROM AB
                                    )
                                    select AID,BID,UShape,UShape.STArea() as unionArea  from UnionShapeS";
                    this.dgv.DataSource = SqlHelper.QueryTable(sql, connStr);
                    //ProgressBar.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private DataTable QueryA(string sql)
        {
            DataTable dt = SqlHelper.QueryTable(sql, connStr);
            return dt;
        }
    }
}
