using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Sql;

namespace Geone.Test.DAL
{
    public class DbHelper
    {
        public static string ConnectString = string.Empty;


        public static IDbConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectString);
            if (connection.State == ConnectionState.Closed) connection.Open();
            return connection;
        }


        static DbHelper()
        {
            //TODO:初始化连接字符串
           
            try
            {
                DbHelper.ConnectString = ConfigurationManager.ConnectionStrings["DLDatabase"].ConnectionString;
            }
            catch (Exception)
            {
            }
            DapperExtensions.DapperExtensions.SqlDialect = new SqlServerDialect();


            DapperExtensions.DapperExtensions.SetMappingAssemblies(new Assembly[] { Assembly.GetExecutingAssembly() });
        }

    }
}
