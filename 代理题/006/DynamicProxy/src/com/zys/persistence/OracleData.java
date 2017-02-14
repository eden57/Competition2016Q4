/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.zys.persistence;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.List;

import com.zys.domain.model.Vehicle;

/**
 * 
 * @author zhuys
 * @date 2017年1月12日
 * @version 1.0
 */

public class OracleData {
	private Connection conn = null;
	private String url = "jdbc:oracle:thin:@10.100.13.210:1521/itsdata";
	private String UserName = "itsprot";
	private String Password = "itsprot";
	private Statement stmt = null;
	private ResultSet rs = null;

	public List<Vehicle> query() {
		List<Vehicle> list = null;
		try {
			Class.forName("oracle.jdbc.driver.OracleDriver");
			rs = stmt.executeQuery("SELECT * FROM TBL_VEHICLE_PASS ");
			stmt = conn.createStatement();
			conn = DriverManager.getConnection(url, UserName, Password);
			String sql = "select * from TBL_VEHICLE_PASS";
			if (conn == null) {
				System.out.println("Connect to Oracle failed");
			} else {
				System.out.println("Connect to Oracle Success");
			}
			System.out.println("***********Preload Data***********");
			while (rs.next()) {
				Vehicle vh = new Vehicle();
				vh.setPlateNo(rs.getString("PLATE_NO"));
				vh.setPassTm(rs.getDate("PASS_TM"));
				vh.setColor(rs.getString("COLOR"));
				vh.setCreateTm(rs.getDate("CREATE_TM"));
				vh.setCreateBy(rs.getString("CREATE_BY"));
				vh.setUpdateTm(rs.getDate("UPDATE_TM"));
				vh.setUpdateBy(rs.getString("UPDATE_BY"));
				vh.setVersion(rs.getInt("VERSION"));
				// System.out.println(rs.getInt("UID") + "," + rs.getString("PLATE_NO") + "," + rs.getDate("PASS_TM") + "," + rs.getString("COLOR") + ","
				// + rs.getDate("CREATE_TM") + "," + rs.getString("CREATE_BY") + "," + rs.getDate("UPDATE_TM") + "," + rs.getString("UPDATE_BY") + ","
				// + rs.getInt("VERSION"));
				list.add(vh);
			}
			System.out.println("******Data PreLoad Complete!******");
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			try {
				if (rs != null)
					rs.close();
				if (conn != null)
					conn.close();
				System.out.println("数据库连接已关闭！");
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		return list;
	}
}
