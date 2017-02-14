using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geone.Test.Model;
using Geone.Test.Util;
using System.Data;
using Dapper;
using DapperExtensions;

namespace Geone.Test.DAL
{
    public class IntersectionDAL
    {
        public const string TABLENAME = "AA01交通水利用地图斑2014";


        /// <summary>
        /// 获取交通水利 索引概要
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoadEntity> GetRoadSummry()
        {
            string sql = "SELECT DISTINCT ifq,DLBM FROM {0} order by ifq,DLBM".Fmt(TABLENAME);
            using (IDbConnection db = DbHelper.GetConnection())
            {
                return db.Query<RoadEntity>(sql);
            }
        }



        /// <summary>
        /// 清空结果数据
        /// </summary>
        /// <returns></returns>
        public bool ResetResult()
        {
            using (IDbConnection db = DbHelper.GetConnection())
            {
                return db.Execute("TRUNCATE TABLE T_Result") > 0;
            }
        }


        /// <summary>
        /// 相交分析计算
        /// </summary>
        /// <param name="ifq">行政区域图</param>
        /// <param name="dlbm">地理编码</param>
        /// <returns></returns>
        public bool AnalysisIntersection(string ifq, string dlbm)
        {
            string sql = @"	INSERT T_Result 
                            SELECT A.OBJECTID AID,b.OBJECTID BID,A.SHAPE.STIntersection(B.SHAPE).STArea() AREA
                             FROM [AA01交通水利用地图斑2014] A 
                            INNER JOIN  [AB02非建设用地图斑2009] B  
                            ON A.[SHAPE].STOverlaps(B.[SHAPE])=1
                            WHERE A.ifq='{0}' AND A.DLBM='{1}'".Fmt(ifq, dlbm);


            using (IDbConnection db = DbHelper.GetConnection())
            {
                return db.Execute(sql, null, null, int.MaxValue, CommandType.Text) > 0;
            }
        }



        public IEnumerable<ResultEntity> GetResult()
        {

            string sql = "SELECT TOP 1000 [AID],[BID],[AREA] FROM [T_Result]";
            using (IDbConnection db = DbHelper.GetConnection())
            {
                return db.Query<ResultEntity>(sql);
            }
        }







    }
}
