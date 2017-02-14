/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.zys.persistence;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;

import com.zys.domain.model.DataSourceSelect;

/**
 * 
 * @author zhuys
 * @date 2017年1月12日
 * @version 1.0
 */

public class DataSourceSelectImpl implements DataSourceSelect {

	@Override
	public void selectH2() throws Exception {
		Class.forName("org.h2.Driver");
		Connection conn = DriverManager.getConnection("jdbc:h2:D:\\Develop\\Workspace\\Workspace_JEE\\H2", "sa", "");
		if(conn==null)
		  {
			  System.out.println("Connect to H2 Failed");
		  }else{
			  System.out.println("Connect to H2 Success");
		  }
		Statement stmt = conn.createStatement();
		String sql="DROP TABLE IF EXISTS TBL_VEHICLE_PASS;"
				+ "CREATE TABLE TBL_VEHICLE_PASS("
				+ "UID VARCHAR2(32),"
				+ "PLATE_NO VARCHAR2(50),"
				+ "PASS_TM TIMESTAMP(6),"
				+ "COLOR VARCHAR2(50),"
				+ "CREATE_TM TIMESTAMP(6),"
				+ "CREATE_BY VARCHAR2(50),"
				+ "UPDATE_TM TIMESTAMP(6),"
				+ "UPDATE_BY VARCHAR2(50),"
				+ "VERSION NUMBER);"
				+ "Insert into TBL_VEHICLE_PASS (UID,PLATE_NO,PASS_TM,COLOR,CREATE_TM,CREATE_BY,UPDATE_TM,UPDATE_BY,VERSION) values ('0001','苏E00001','2017-01-12 06:46:50','Yellow','2017-01-12 06:47:00','SYS','2017-01-12 06:47:05','SYS',1);"
				+ "Insert into TBL_VEHICLE_PASS (UID,PLATE_NO,PASS_TM,COLOR,CREATE_TM,CREATE_BY,UPDATE_TM,UPDATE_BY,VERSION) values ('0002','苏E00002','2017-01-12 06:46:50','Red','2017-01-12 06:47:00','SYS','2017-01-12 06:47:05','SYS',1);";
			int stmtRs=stmt.executeUpdate(sql);
			System.out.println(stmtRs);
		   ResultSet rs = stmt.executeQuery("SELECT * FROM TBL_VEHICLE_PASS ");   
		   System.out.println("***********Preload Data***********");
		      while(rs.next()) {   
		       System.out.println(rs.getInt("UID")+","+rs.getString("PLATE_NO")+","
		    		   +rs.getDate("PASS_TM")+","+rs.getString("COLOR")+","
		    		   +rs.getDate("CREATE_TM")+","+rs.getString("CREATE_BY")+","
		    		   +rs.getDate("UPDATE_TM")+","+rs.getString("UPDATE_BY")+","
		    		   +rs.getInt("VERSION"));
		      }
		      System.out.println("******Data PreLoad Complete!******");
		   conn.close();
	}


	@Override
	public void selectOracle() throws Exception {
		Connection conn = null;
		  Class.forName("oracle.jdbc.driver.OracleDriver");
		  String url = "jdbc:oracle:thin:@10.100.13.210:1521/itsdata";
		  String UserName = "itsprot";
		  String Password = "itsprot";

		  conn = DriverManager.getConnection(url, UserName, Password);
		  String sql="select * from TBL_VEHICLE_PASS";
		  if(conn==null)
		  {
			  System.out.println("Connect to Oracle failed");
		  }else{
			  System.out.println("Connect to Oracle Success");
		  }
		  Statement stmt = conn.createStatement();
		  ResultSet rs = stmt.executeQuery("SELECT * FROM TBL_VEHICLE_PASS ");   
		   System.out.println("***********Preload Data***********");
		      while(rs.next()) {   
		       System.out.println(rs.getInt("UID")+","+rs.getString("PLATE_NO")+","
		    		   +rs.getDate("PASS_TM")+","+rs.getString("COLOR")+","
		    		   +rs.getDate("CREATE_TM")+","+rs.getString("CREATE_BY")+","
		    		   +rs.getDate("UPDATE_TM")+","+rs.getString("UPDATE_BY")+","
		    		   +rs.getInt("VERSION"));
		      }
		      System.out.println("******Data PreLoad Complete!******");
		   conn.close();
	}

}
