/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.its.db;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

/**
 * 
 * @author xudi
 * @date 2017年1月12日
 * @version 1.0
 */

public class OracleFactory {
	private static Connection conn = null;
	private static Statement stmt = null;

	public static Connection getConn() {
		return conn;
	}

	public static void setConn(Connection conn) {
		OracleFactory.conn = conn;
	}

	public static Statement getStmt() {
		return stmt;
	}

	public static void setStmt(Statement stmt) {
		OracleFactory.stmt = stmt;
	}

	private OracleFactory() {
	}

	public static Connection getInstance() throws Exception {
		Class.forName("oracle.jdbc.driver.OracleDriver").newInstance();
		conn = DriverManager.getConnection("jdbc:oracle:thin:@//10.100.13.210:1521/itsdata", "itsprot", "itsprot");
		stmt = conn.createStatement();
		return conn;
	}

	public static void CloseConnection() throws SQLException {
		if (conn != null) {
			conn.close();
			conn = null;
		}
		if (stmt != null) {
			stmt.close();
			stmt = null;
		}
	}
}
