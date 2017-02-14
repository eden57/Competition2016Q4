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
    public partial class FrmIntersect : Form
    {
        public FrmIntersect()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DBHelper.My_con != null)
            {
                DBHelper.con_close();
            }
            Application.Exit();
        }

        private void FrmIntersect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DBHelper.My_con != null)
            {
                DBHelper.con_close();
            }
            Application.Exit();
        }

        private void FrmIntersect_Load(object sender, EventArgs e)
        {
            //执行存储过程 
            //SqlCommand cmd = new SqlCommand("sp_tables", DBHelper.My_con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    lsbLayer1.Items.Add(dr["TABLE_NAME"].ToString());
            //    lsbLayer2.Items.Add(dr["TABLE_NAME"].ToString());
            //    //MessageBox.Show(dr["TABLE_NAME"].ToString());
            //}

            //执行存储过程
            SqlCommand sqlcmd = new SqlCommand("SELECT OBJECT_NAME (id) FROM sysobjects WHERE xtype = 'U' AND OBJECTPROPERTY (id, 'IsMSShipped') = 0", DBHelper.My_con);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            while (dr.Read())
            {
                lsbLayer1.Items.Add(dr.GetString(0));
                lsbLayer2.Items.Add(dr.GetString(0));
                //MessageBox.Show(dr.GetString(0));
            }
        }

        private void GetExtent()
        {
            DBHelper.minX1 = DBHelper.getDataSet("select MIN(shape.STEnvelope().STPointN(1).STX)-1 from " + lsbLayer1.SelectedItem.ToString(),
                    lsbLayer1.SelectedItem.ToString()).Tables[0].Rows[0][0].ToString();
            DBHelper.minY1 = DBHelper.getDataSet("select MIN(shape.STEnvelope().STPointN(1).STY)-1 from " + lsbLayer1.SelectedItem.ToString(),
                lsbLayer1.SelectedItem.ToString()).Tables[0].Rows[0][0].ToString();
            DBHelper.maxX1 = DBHelper.getDataSet("select MAX(shape.STEnvelope().STPointN(3).STX)+1 from " + lsbLayer1.SelectedItem.ToString(),
                lsbLayer1.SelectedItem.ToString()).Tables[0].Rows[0][0].ToString();
            DBHelper.maxY1 = DBHelper.getDataSet("select MAX(shape.STEnvelope().STPointN(3).STY)+1 from " + lsbLayer1.SelectedItem.ToString(),
                lsbLayer1.SelectedItem.ToString()).Tables[0].Rows[0][0].ToString();

            DBHelper.minX2 = DBHelper.getDataSet("select MIN(shape.STEnvelope().STPointN(1).STX)-1 from " + lsbLayer2.SelectedItem.ToString(),
                lsbLayer1.SelectedItem.ToString()).Tables[0].Rows[0][0].ToString();
            DBHelper.minY2 = DBHelper.getDataSet("select MIN(shape.STEnvelope().STPointN(1).STY)-1 from " + lsbLayer2.SelectedItem.ToString(),
                lsbLayer1.SelectedItem.ToString()).Tables[0].Rows[0][0].ToString();
            DBHelper.maxX2 = DBHelper.getDataSet("select MAX(shape.STEnvelope().STPointN(3).STX)+1 from " + lsbLayer2.SelectedItem.ToString(),
                lsbLayer1.SelectedItem.ToString()).Tables[0].Rows[0][0].ToString();
            DBHelper.maxY2 = DBHelper.getDataSet("select MAX(shape.STEnvelope().STPointN(3).STY)+1 from " + lsbLayer2.SelectedItem.ToString(),
                lsbLayer1.SelectedItem.ToString()).Tables[0].Rows[0][0].ToString();
        }

        private void CreateSpatialIndexLyr1()
        {
            string strSQL1 = "ALTER TABLE "+lsbLayer1.SelectedItem.ToString()+" ADD CONSTRAINT [R266_pk] PRIMARY KEY CLUSTERED ([OBJECTID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75) ON [PRIMARY]";
            DBHelper.getsqlcom(strSQL1);
        }

        private void CreateSpatialIndexLyr1_()
        {
            string strSQL3 = "CREATE SPATIAL INDEX [S288_idx] ON " + lsbLayer1.SelectedItem.ToString() + " ([SHAPE])USING GEOMETRY_AUTO_GRID WITH(BOUNDING_BOX = (" + DBHelper.minX1 + ", " + DBHelper.minY1 + ", " + DBHelper.maxX1 + ", " + DBHelper.maxY1 + "), CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
            DBHelper.getsqlcom(strSQL3);
        }

        private void CreateSpatialIndexLyr2()
        {
            string strSQL2 = "ALTER TABLE " + lsbLayer2.SelectedItem.ToString() + " ADD CONSTRAINT [R566_pk] PRIMARY KEY CLUSTERED ([OBJECTID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75) ON [PRIMARY]";
            DBHelper.getsqlcom(strSQL2);
        }

        private void CreateSpatialIndexLyr2_()
        {
            string strSQL4 = "CREATE SPATIAL INDEX [S588_idx] ON " + lsbLayer2.SelectedItem.ToString() + " ([SHAPE])USING GEOMETRY_AUTO_GRID WITH(BOUNDING_BOX = (" + DBHelper.minX2 + ", " + DBHelper.minY2 + ", " + DBHelper.maxX2 + ", " + DBHelper.maxY2 + "), CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
            DBHelper.getsqlcom(strSQL4);
        }

        private void CreateSpatialIndex()
        {
            string strSQL1 = "ALTER TABLE " + lsbLayer1.SelectedItem.ToString() + " ADD CONSTRAINT [R266_pk] PRIMARY KEY CLUSTERED ([OBJECTID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75) ON [PRIMARY]";
            string strSQL2 = "ALTER TABLE " + lsbLayer2.SelectedItem.ToString() + " ADD CONSTRAINT [R566_pk] PRIMARY KEY CLUSTERED ([OBJECTID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75) ON [PRIMARY]";
            DBHelper.getsqlcom(strSQL1);
            DBHelper.getsqlcom(strSQL2);
            string strSQL3 = "CREATE SPATIAL INDEX [S288_idx] ON " + lsbLayer1.SelectedItem.ToString() + " ([SHAPE])USING GEOMETRY_AUTO_GRID WITH(BOUNDING_BOX = (" + DBHelper.minX1 + ", " + DBHelper.minY1 + ", " + DBHelper.maxX1 + ", " + DBHelper.maxY1 + "), CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
            string strSQL4 = "CREATE SPATIAL INDEX [S588_idx] ON " + lsbLayer2.SelectedItem.ToString() + " ([SHAPE])USING GEOMETRY_AUTO_GRID WITH(BOUNDING_BOX = (" + DBHelper.minX2 + ", " + DBHelper.minY2 + ", " + DBHelper.maxX2 + ", " + DBHelper.maxY2 + "), CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
            DBHelper.getsqlcom(strSQL3);
            DBHelper.getsqlcom(strSQL4);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {

                if (lsbLayer1.SelectedItem != null && lsbLayer2.SelectedItem != null)
                {
                    GetExtent();
                   
                    string strLyr1 = "select object_name(object_id) tableName,name,type_desc from sys.indexes where object_name(object_id) = '"+ lsbLayer1.SelectedItem.ToString() + "'";
                    string strLyr2 = "select object_name(object_id) tableName,name,type_desc from sys.indexes where object_name(object_id) = '" + lsbLayer2.SelectedItem.ToString() + "'";
                    DataSet ds1 = DBHelper.getDataSet(strLyr1, "Lyr1");
                    DataSet ds2 = DBHelper.getDataSet(strLyr2, "Lyr2");

                    int idx1= ds1.Tables[0].Rows.Count;
                    int idx2= ds2.Tables[0].Rows.Count;

                    bool bLyr1=false;
                    bool bLyr1_ = false;
                    bool bLyr2 = false;
                    bool bLyr2_ = false;

                    for (int i=0;i<idx1;i++)
                    {
                        if (ds1.Tables[0].Rows[i][2].ToString()== "CLUSTERED")
                        {
                            bLyr1 = true;
                        }
                        if (ds1.Tables[0].Rows[i][2].ToString() == "SPATIAL")
                        {
                            bLyr1_ = true;
                        }
                    }

                    for (int i = 0; i < idx2; i++)
                    {
                        if (ds1.Tables[0].Rows[i][2].ToString() == "CLUSTERED")
                        {
                            bLyr2 = true;
                        }
                        if (ds1.Tables[0].Rows[i][2].ToString() == "SPATIAL")
                        {
                            bLyr2_ = true;
                        }
                    }

                    if(!bLyr1)
                    {
                        CreateSpatialIndexLyr1();
                    }
                    if (!bLyr1_)
                    {
                        CreateSpatialIndexLyr1_();
                    }
                    if (!bLyr2)
                    {
                        CreateSpatialIndexLyr2();
                    }
                    if (!bLyr2_)
                    {
                        CreateSpatialIndexLyr2_();
                    }

                    //CreateSpatialIndex();
                }

                //string strQuery = "select t.A表唯一编码,t.B表唯一编码,t.重叠面积 from  (select a.OBJECTID as A表唯一编码,b.OBJECTID as B表唯一编码,a.SHAPE.STIntersection(b.SHAPE).STArea() as 重叠面积 from "+ lsbLayer1.SelectedItem.ToString() + " a,"+ lsbLayer2.SelectedItem.ToString() + " b where a.shape.STIntersects(b.SHAPE) = 1) t where t.重叠面积 > 0 order by t.重叠面积 desc";

                string strQuery = "select row_number() OVER (ORDER BY t.重叠面积 DESC) as 序号,t.A表唯一编码,t.B表唯一编码,t.重叠面积 from  (select a.OBJECTID as A表唯一编码,b.OBJECTID as B表唯一编码,a.SHAPE.STIntersection(b.SHAPE).STArea() as 重叠面积 from " + lsbLayer1.SelectedItem.ToString() + " a," + lsbLayer2.SelectedItem.ToString() + " b where a.shape.STIntersects(b.SHAPE) = 1) t where t.重叠面积 > 0";
                dgvMain.DataSource = DBHelper.getDataSet(strQuery, "IntersectResult").Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }           
        }
    }
}
