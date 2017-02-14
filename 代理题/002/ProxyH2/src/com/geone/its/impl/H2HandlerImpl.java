package com.geone.its.impl;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import org.h2.tools.Server;

import com.geone.its.H2.H2Handler;
import com.geone.its.entity.VehiclePass;

public class H2HandlerImpl implements H2Handler {
	private static final String JDBC_URL = "jdbc:h2:./h2db/itstest";
	private static final String USER = "sa";
	private static final String PASSWORD = "sa";
	private static final String port = "11194";   
	private static final String DRIVER_CLASS = "org.h2.Driver";
	private static final String DROP_TABLE_ = "DROP TABLE IF EXISTS ";
	private Server server = null;
	@Override
	public boolean createH2Database() {
		try {
			Class.forName(DRIVER_CLASS);
			server = Server.createTcpServer(new String[] {"-tcp", "-tcpAllowOthers", "-tcpPort",port}).start();
			System.out.println("启动成功：" + server.getStatus());
			return true;
		} catch (ClassNotFoundException e) {
			System.out.println("启动h2出错：" + e.toString());
			e.printStackTrace();
			return false;
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		}
	}

	@Override
	public boolean createH2Table(String sql) {
		Connection conn = null;
		Statement stmt = null;
		try {
			conn = DriverManager.getConnection(JDBC_URL, USER, PASSWORD);
			stmt = conn.createStatement();
			stmt.execute(sql);
			return true;
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		}finally {
			try {
				stmt.close();
				conn.close();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
	}

	@Override
	public boolean updateData(String sql) {
		Connection conn = null;
		Statement stmt = null;
		try {
			conn = DriverManager.getConnection(JDBC_URL, USER, PASSWORD);
			stmt = conn.createStatement();
			int result = stmt.executeUpdate(sql);
			return result==1;
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		}finally {
			try {
				stmt.close();
				conn.close();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
	}

	@Override
	public Object queryData(String sql) {
		Connection conn = null;
		Statement stmt = null;
		try {
			conn = DriverManager.getConnection(JDBC_URL, USER, PASSWORD);
			stmt = conn.createStatement();
			ResultSet result = stmt.executeQuery(sql);
			
			return parseVehiclePassFromResultSet(result);
		} catch (SQLException e) {
			e.printStackTrace();
			return null;
		}finally {
			try {
				stmt.close();
				conn.close();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
	}

	@Override
	public boolean deleteData(String sql) {
		Connection conn = null;
		Statement stmt = null;
		try {
			conn = DriverManager.getConnection(JDBC_URL, USER, PASSWORD);
			stmt = conn.createStatement();
			int result = stmt.executeUpdate(sql);
			return result==1;
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		}finally {
			try {
				stmt.close();
				conn.close();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
	}

	@Override
	public void destroyH2Database() {
		if(null != this.server){
			this.server.stop();
			this.server = null;
		}
	}

	@Override
	public boolean dropH2Table(String tableName) {
		Connection conn = null;
		Statement stmt = null;
		try {
			conn = DriverManager.getConnection(JDBC_URL, USER, PASSWORD);
			stmt = conn.createStatement();
			stmt.execute(DROP_TABLE_+tableName);
			return true;
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		}finally {
			try {
				stmt.close();
				conn.close();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
	}
	
	private List<VehiclePass> parseVehiclePassFromResultSet(ResultSet rs){
		List<VehiclePass> list = null;
		if (null != rs) {
			list = new ArrayList<>();
			try {
				while (rs.next()) {
					VehiclePass vp = new VehiclePass();
					vp.setUid(rs.getString("UID"));
					vp.setPlateNo(rs.getString("PLATE_NO"));
					vp.setPassTime(rs.getTimestamp("PASS_TM"));
					vp.setColor(rs.getString("COLOR"));
					vp.setCreatBy(rs.getString("CREATE_BY"));
					vp.setCreatTm(rs.getTimestamp("CREATE_TM"));
					vp.setUpdateBy(rs.getString("UPDATE_BY"));
					vp.setUpdateTm(rs.getTimestamp("UPDATE_TM"));
					vp.setVersion(rs.getString("VERSION"));
					list.add(vp);
				}
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
		return list;
	}
}
