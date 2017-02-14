/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.its.dynamicproxy;

import java.sql.ResultSet;
import java.sql.Statement;

import org.apache.log4j.BasicConfigurator;
import org.apache.log4j.Logger;

import com.geone.its.db.H2Factory;
import com.geone.its.db.OracleFactory;

/**
 * 
 * @author xudi
 * @date 2017年1月12日
 * @version 1.0
 */

public class RealSubject implements AbstractSubject {
	public Statement stmt = null;
	public static Logger logger = Logger.getLogger(RealSubject.class);
	@Override
	public void request() throws Exception {
		BasicConfigurator.configure();
		//服务启动时，创建H2数据库
		H2Factory.getInstance();
		stmt = H2Factory.getStmt();
		if (OracleFactory.getInstance() != null) {
			stmt = OracleFactory.getStmt();
			ResultSet rs = stmt.executeQuery("select * from TBL_VEHICLE_PASS");
			while (rs.next()) {
				logger.info(rs.getInt(1) + " " + rs.getString(2) + " " + rs.getString(3) + " " + rs.getString(4)
						+ " " + rs.getString(5) + " " + rs.getString(6) + " " + rs.getString(7) + " "
						+ rs.getString(8));
			}
		} else {
			stmt = H2Factory.getStmt();
			stmt.executeUpdate(
					"create table TBL_VEHICLE_PASS(UID INT PRIMARY KEY,PLATE_NO VARCHAR(30) NOT NULL,PASS_TM VARCHAR(30),COLOR VARCHAR(6),CREATE_TM VARCHAR(30),CREATE_BY VARCHAR(10),UPDATE_TM VARCHAR(30),UPDATE_BY VARCHAR(10),VERSION VARCHAR(30))");
			//insert
			stmt.executeUpdate(
					"insert into TBL_VEHICLE_PASS values(1,'123456789','10000000','ffffff','20170112','xd','20170113','zys','2.0.0')");
			stmt.executeUpdate(
					"insert into TBL_VEHICLE_PASS values(2,'223456789','10000001','ffeeef','20170112','xxp','20170113','lc','2.0.1')");
			//query
			ResultSet rs = stmt.executeQuery("select * from TBL_VEHICLE_PASS");
			while (rs.next()) {
				logger.info(rs.getInt(1) + " " + rs.getString(2) + " " + rs.getString(3) + " " + rs.getString(4)
						+ " " + rs.getString(5) + " " + rs.getString(6) + " " + rs.getString(7) + " "
						+ rs.getString(8));
			}
			//update 
			stmt.executeUpdate("update  TBL_VEHICLE_PASS set PLATE_NO='苏E293842' where UID=2");
			//delete
			stmt.executeUpdate("delete from TBL_VEHICLE_PASS where UID=1");
		}
	}
}
