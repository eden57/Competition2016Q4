using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProPolygonIntersection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tableNmae1 = "AA01交通水利用地图斑2014";
            string tableNmae2 = "AB02非建设用地图斑2009";
            string[] sqls1 = SplitTable(tableNmae1).ToArray();
            string[] sqls2 = SplitTable(tableNmae2).ToArray();

            File.WriteAllLines("AA01交通水利用地图斑2014.txt", sqls1);
            File.WriteAllLines("AB02非建设用地图斑2009.txt", sqls2);
        }


        private static IEnumerable<string> SplitTable(string tableNmae)
        {
            int minxo = 14837;
            int minyo = 34427;
            for (int i = 0; i < 32; i++)
            {
                int minx = minxo + 2000 * i;
                int maxx = minx + 2000;
                for (int j = 0; j < 42; j++)
                {
                    int miny = minyo + 1000 * j;
                    int maxy = miny + 1000;
                    string geo = string.Format("geometry::STGeomFromText('POLYGON(({0} {1}, {2} {3}, {4} {5}, {6} {7}, {8} {9}))', 0)",
                        minx, miny, maxx, miny, maxx, maxy, minx, maxy, minx, miny);

                    string sql = string.Format("select * into {0}_{1}{2} from {0} where shape.STOverlaps({3}) > 0", tableNmae, i.ToString().PadLeft(2, '0'), j.ToString().PadLeft(2, '0'), geo);
                    yield return sql;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string finalsql = FinalSql();
            File.WriteAllText("sqls.txt", finalsql);
        }

        private static string FinalSql()
        {
            string[] sqls = NewMethod().ToArray();
            string finalsql = "select IDENTITY(INT,1,1) as seq, f.* into #result from (" + string.Join(" union ", sqls) + ") f where f.area > 0; select * from #result; drop table #result;";
            return finalsql;
        }

        private static IEnumerable<string> NewMethod()
        {
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 42; j++)
                {
                    string sql = string.Format("(select a.objectid as objectid_a,b.objectid objectid_b, Round(a.shape.STIntersection(b.shape).STArea(),2) as area from AB02非建设用地图斑2009_{0}{1} b, AA01交通水利用地图斑2014_{0}{1} a)", i.ToString().PadLeft(2, '0'), j.ToString().PadLeft(2, '0'));

                    yield return sql;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string finalsql = FinalSql();
            string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = finalsql;
                com.CommandTimeout = 240;
                dt = DataReaderToDt(com);
            }
            dataGridView1.DataSource = dt;

        }

        private static DataTable DataReaderToDt(SqlCommand com)
        {
            DataTable objDataTable = new DataTable();
            using (SqlDataReader reader = com.ExecuteReader())
            {
                int intFieldCount = reader.FieldCount;
                for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
                {
                    objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
                }
                objDataTable.BeginLoadData();

                object[] objValues = new object[intFieldCount];
                while (reader.Read())
                {
                    reader.GetValues(objValues);
                    objDataTable.LoadDataRow(objValues, true);
                }
                reader.Close();
                objDataTable.EndLoadData();
            }

            return objDataTable;
        }
    }
}
