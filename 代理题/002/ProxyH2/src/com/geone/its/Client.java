package com.geone.its;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Proxy;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

import com.geone.its.H2.H2Handler;
import com.geone.its.Oracle.DBFactory;
import com.geone.its.entity.VehiclePass;
import com.geone.its.impl.H2HandlerImpl;
import com.geone.its.service.ItsService;
import com.geone.its.service.ItsServiceDynProxy;
import com.geone.its.service.impl.ItsServiceImpl;

public class Client {
	public static final String CREAT_VEHICLE_PASS_TABLE_SQL = "CREATE TABLE TBL_VEHICLE_PASS "
			+ "(\"UID\" VARCHAR2(32) PRIMARY KEY, PLATE_NO VARCHAR2(50), "
			+ "PASS_TM DATE, COLOR VARCHAR2(50), CREATE_TM DATE, "
			+ "CREATE_BY VARCHAR2(50), UPDATE_TM DATE, "
			+ "UPDATE_BY VARCHAR2(50), VERSION NUMBER(38,0))";
	public static void main(String[] args) {
		H2Handler h2Handler = new H2HandlerImpl();
		ItsService itsServ = new ItsServiceImpl();
        InvocationHandler handler = new ItsServiceDynProxy(itsServ);

        ItsService itsServHandler = (ItsService)Proxy.newProxyInstance(handler.getClass().getClassLoader(), 
        		itsServ.getClass().getInterfaces(), handler);
        //1. 建立H2内存数据库
        h2Handler.createH2Database();
        //2. 建立Table TBL_VEHICLE_PASS
        h2Handler.dropH2Table("TBL_VEHICLE_PASS");
        h2Handler.createH2Table(CREAT_VEHICLE_PASS_TABLE_SQL);
        //3. 取出oracle中的数据 放置H2中
        DBFactory  factory = new DBFactory();
        Connection conn = factory.getDBConnectionInstance().getConnection();
        PreparedStatement ps;
		try {
			ps = conn.prepareStatement("select * from TBL_VEHICLE_PASS");
			ResultSet rs = ps.executeQuery();
		
	        while (rs.next())
	        {
	        	String sql = "INSERT INTO TBL_VEHICLE_PASS VALUES('" + rs.getString("UID")+ "','"
	        			+rs.getString("PLATE_NO")+ "',to_date('"+rs.getString("PASS_TM")+"','YYYY-MM-DD HH24:MI:SS')"+",'" + rs.getString("COLOR")+"',"
	        			+ "to_date('"+rs.getString("CREATE_TM")+"','YYYY-MM-DD HH24:MI:SS')"+",'" + rs.getString("CREATE_BY")+ "'," 
	        			+ "to_date('"+rs.getString("UPDATE_TM")+"','YYYY-MM-DD HH24:MI:SS')"+",'"
	        			+rs.getString("UPDATE_BY")+ "'," + rs.getString("VERSION")+ ")";
	        	System.out.println(sql);
	        	h2Handler.updateData(sql);
	        }
	        rs.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		List<VehiclePass> list = itsServHandler.getAllVehiclePass();
		System.out.println(list.isEmpty());
	}
}
