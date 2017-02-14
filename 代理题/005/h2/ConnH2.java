package com.test.h2;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

public class ConnH2 implements ConnDB{
    private static final String JDBC_URL = "jdbc:h2:tcp://localhost/mem:test";
    private static final String USER = "test";
    private static final String PASSWORD = "123";
    private static final String DRIVER_CLASS="org.h2.Driver";
    Statement stmt = null;
	Connection conn = null;
    public ConnH2() {
    	try{
    		Class.forName("org.h2.Driver");
    		conn = DriverManager.getConnection(JDBC_URL, USER, PASSWORD);
    		stmt = conn.createStatement();
    	}catch(Exception e){
    		e.printStackTrace();
    	}
	}
	@Override
	public boolean createDB() {
		String sql="CREATE DATABASE database_name";
		try {
			stmt.execute(sql);
			return true;
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		}finally{
			try {
				if (stmt != null) {
					stmt.close();
					stmt=null;
				} 
				if(conn!=null){
					conn.close();
					conn=null;
				}
			} catch (Exception e2) {
				e2.printStackTrace();
			}
		}
		
	}

	@Override
	public boolean createTable() {
		String sql="CREATE TABLE TBL_VEHICLE_PASS(UID VARCHAR(4),PLATE_NO VARCHAR(4),PASS_TM VARCHAR(4),COLOR VARCHAR(4),CREATE_TM VARCHAR(4),CREATE_BY VARCHAR(4),UPDATE_TM VARCHAR(4),UPDATE_BY VARCHAR(4),VERSION VARCHAR(4))";
		try {
			stmt.execute(sql);
			return true;
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		}finally{
			try {
				if (stmt != null) {
					stmt.close();
					stmt=null;
				} 
				if(conn!=null){
					conn.close();
					conn=null;
				}
			} catch (Exception e2) {
				e2.printStackTrace();
			}
		}
	}

	@Override
	public boolean insert() {
		String sql="INSERT INTO TBL_VEHICLE_PASS VALUES('UID',PLATE_NO,PASS_TM,COLOR,CREATE_TM,CREATE_BY,UPDATE_TM,UPDATE_BY,VERSION)";
		try {
			stmt.execute(sql);
			return true;
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		}finally{
			try {
				if (stmt != null) {
					stmt.close();
					stmt=null;
				} 
				if(conn!=null){
					conn.close();
					conn=null;
				}
			} catch (Exception e2) {
				e2.printStackTrace();
			}
		}
		
	}

}
