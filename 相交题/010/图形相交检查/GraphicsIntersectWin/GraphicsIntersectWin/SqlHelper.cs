using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace GraphicsIntersectWin
{
    public class SqlHelper
    {
        #region  全局变量
        public static SqlConnection My_con;  //定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功

        #endregion
        private static readonly string _connStr;
        static SqlHelper()
        {
            //_connStr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        }

        public static bool CheckDbState(string connStr)
        {
            using (SqlConnection sqlCon = new SqlConnection(connStr))
            {
                sqlCon.Open();
                return sqlCon.State == ConnectionState.Open;
            }
        }

        public static DataTable QueryTable(string sql,string connStr)
        {
            using (SqlConnection sqlCon = new SqlConnection(connStr))
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
        }
    }
}
