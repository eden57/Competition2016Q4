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

public class H2Factory {
	private static Connection conn = null;
	private static Statement stmt = null;

	public static Connection getConn() {
		return conn;
	}

	public static void setConn(Connection conn) {
		H2Factory.conn = conn;
	}

	public static Statement getStmt() {
		return stmt;
	}

	public static void setStmt(Statement stmt) {
		H2Factory.stmt = stmt;
	}

	private H2Factory() {
	}

	public static Connection getInstance() throws ClassNotFoundException, SQLException {
		Class.forName("org.h2.Driver");
		conn = DriverManager.getConnection("jdbc:h2:D:\\Code\\WorkSpace\\DynamicProxy\\db\\IntelligentTransportation",
				"root", "1234");
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
