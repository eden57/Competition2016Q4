/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.dao;

import java.util.List;

import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcOperations;
import org.springframework.stereotype.Repository;

import com.geone.exam.exception.DSFaultException;
import com.geone.exam.model.VehiclePass;

/**
 * 
 * @author aaa
 * @date 2017年1月12日
 * @version 1.0
 */
@Repository
public class VehiclePassDao  extends BaseDao{
	/**
	 * 第1种方法 --- 代理H2 数据源
	 * @param vehiclePass
	 * @return
	 * @throws DSFaultException
	 */
	public List<VehiclePass> findList(VehiclePass vehiclePass) throws DSFaultException{
		String sql = "SELECT \"UID\", PLATE_NO,PASS_TM,COLOR,CREATE_TM,CREATE_BY,UPDATE_TM,UPDATE_BY,VERSION FROM TBL_VEHICLE_PASS";
		namedJdbcTmpltProxy = (NamedParameterJdbcOperations) jdbcTmpltProxyHandler.bind();
		List<VehiclePass> list = namedJdbcTmpltProxy.query(sql, new BeanPropertyRowMapper<VehiclePass>(VehiclePass.class));
		return list;
	}
	
	/**
	 * 第2种方法--- 选择器选择数据源
	 * @param vehiclePass
	 * @return
	 * @throws DSFaultException
	 */
	public List<VehiclePass> findList2(VehiclePass vehiclePass) throws DSFaultException{
		String sql = "SELECT \"UID\", PLATE_NO,PASS_TM,COLOR,CREATE_TM,CREATE_BY,UPDATE_TM,UPDATE_BY,VERSION FROM TBL_VEHICLE_PASS";
		List<VehiclePass> list = getNamedJdbcTmplt().query(sql, new BeanPropertyRowMapper<VehiclePass>(VehiclePass.class));
		return list;
	}
	
	/**
	 * 第3种方法---动态数据源
	 * @param vehiclePass
	 * @return
	 * @throws DSFaultException
	 */
	public List<VehiclePass> findList3(VehiclePass vehiclePass) throws DSFaultException{
		String sql = "SELECT \"UID\", PLATE_NO,PASS_TM,COLOR,CREATE_TM,CREATE_BY,UPDATE_TM,UPDATE_BY,VERSION FROM TBL_VEHICLE_PASS";
		List<VehiclePass> list = getNamedJdbcTmplt2().query(sql, new BeanPropertyRowMapper<VehiclePass>(VehiclePass.class));
		return list;
	}
}
