package com.geone.its.service.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.List;

import com.geone.its.H2.H2Handler;
import com.geone.its.Oracle.DBFactory;
import com.geone.its.entity.VehiclePass;
import com.geone.its.impl.H2HandlerImpl;
import com.geone.its.service.ItsService;

public class ItsServiceImpl implements ItsService {
	H2Handler h2Handler = new H2HandlerImpl();

	@Override
	public List<VehiclePass> getAllVehiclePass() {
		@SuppressWarnings("unchecked")
		List<VehiclePass> rs = (List<VehiclePass>) h2Handler.queryData("select * from TBL_VEHICLE_PASS");
		return rs;
	}

	@Override
	public List<VehiclePass> getVehiclePassByPlateNo(String plateNo) {
		return null;
	}

	@Override
	public List<VehiclePass> getVehiclePassByPassTime(Timestamp startTM, Timestamp endTM) {
		return null;
	}

	@Override
	public List<VehiclePass> getAllVehiclePassByOracle() {
		List<VehiclePass> list = null;
		// 从oracle里面取
		DBFactory factory = new DBFactory();
		Connection conn = factory.getDBConnectionInstance().getConnection();
		PreparedStatement ps;
		ResultSet rs = null;
		try {
			ps = conn.prepareStatement("select * from TBL_VEHICLE_PASS");
			rs = ps.executeQuery();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		if (null != rs) {
			try {
				while (rs.next()) {
					String sql = "INSERT INTO TBL_VEHICLE_PASS VALUES('" + rs.getString("UID") + "','"
							+ rs.getString("PLATE_NO") + "',to_date('" + rs.getString("PASS_TM")
							+ "','YYYY-MM-DD HH24:MI:SS')" + ",'" + rs.getString("COLOR") + "'," + "to_date('"
							+ rs.getString("CREATE_TM") + "','YYYY-MM-DD HH24:MI:SS')" + ",'"
							+ rs.getString("CREATE_BY") + "'," + "to_date('" + rs.getString("UPDATE_TM")
							+ "','YYYY-MM-DD HH24:MI:SS')" + ",'" + rs.getString("UPDATE_BY") + "',"
							+ rs.getString("VERSION") + ")";
					h2Handler.updateData(sql);
				}
				list =  parseVehiclePassFromResultSet(rs);
			} catch (SQLException e) {
				e.printStackTrace();
			} finally {
				try {
					rs.close();
				} catch (SQLException e) {
					e.printStackTrace();
				}
			}
		}
		return list;
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
