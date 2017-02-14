package com.test.h2;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;

public class Proxy implements ConnDB{
    ConnH2 h2;
    
    public Proxy(ConnH2 h2) {
		this.h2 = h2;
	}
	@Override
	public boolean createDB() {
		if(!h2.createDB()){
			
		 String sql="CREATE DATABASE database_name";
		 String url = "jdbc:oracle:" + "thin:@127.0.0.1:1521:XE";
		 String user = "system";
		 String password = "147";
		 PreparedStatement pstmt=null;
		 Connection conn=null;
		 try {
			Class.forName("oracle.jdbc.driver.OracleDriver");
		    conn = DriverManager.getConnection(url, user, password);
			pstmt = conn.prepareStatement(sql);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}finally{
			try {
				if (pstmt != null) {
					pstmt.close();
					pstmt=null;
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
		return true;
	}

	@Override
	public boolean createTable() {
		if(!h2.createTable()){
			
			 String sql="CREATE TABLE TBL_VEHICLE_PASS(UID VARCHAR(4),PLATE_NO VARCHAR(4),PASS_TM VARCHAR(4),COLOR VARCHAR(4),CREATE_TM VARCHAR(4),CREATE_BY VARCHAR(4),UPDATE_TM VARCHAR(4),UPDATE_BY VARCHAR(4),VERSION VARCHAR(4))";
			 String url = "jdbc:oracle:" + "thin:@127.0.0.1:1521:XE";
			 String user = "system";
			 String password = "147";
			 PreparedStatement pstmt=null;
			 Connection conn=null;
			 try {
				Class.forName("oracle.jdbc.driver.OracleDriver");
			    conn = DriverManager.getConnection(url, user, password);
				pstmt = conn.prepareStatement(sql);
			} catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}finally{
				try {
					if (pstmt != null) {
						pstmt.close();
						pstmt=null;
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
		return true;
	}

	@Override
	public boolean insert() {
		if(h2.insert()){
			
			 String sql="INSERT INTO TBL_VEHICLE_PASS VALUES('UID',PLATE_NO,PASS_TM,COLOR,CREATE_TM,CREATE_BY,UPDATE_TM,UPDATE_BY,VERSION)";
			 String url = "jdbc:oracle:" + "thin:@127.0.0.1:1521:XE";
			 String user = "system";
			 String password = "147";
			 PreparedStatement pstmt=null;
			 Connection conn=null;
			 try {
				Class.forName("oracle.jdbc.driver.OracleDriver");
			    conn = DriverManager.getConnection(url, user, password);
				pstmt = conn.prepareStatement(sql);
			} catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}finally{
				try {
					if (pstmt != null) {
						pstmt.close();
						pstmt=null;
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
		return true;
	}

}
