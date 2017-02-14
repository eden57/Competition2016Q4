/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.svs;

import java.util.List;

import org.apache.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.geone.exam.dao.MonitorDao;
import com.geone.exam.dao.VehiclePassDao;
import com.geone.exam.exception.DSFaultException;
import com.geone.exam.model.VehiclePass;

/**
 * 
 * @author aaa
 * @date 2017年1月12日
 * @version 1.0
 */
@Service
public class VehiclePassService {
	public Logger logger = Logger.getLogger(this.getClass());
	@Autowired
	private MonitorDao monitorDao;
	@Autowired
	private VehiclePassDao vehiclePassDao;
	
	public List<VehiclePass> findList(VehiclePass vehiclePass) throws DSFaultException{
		List<VehiclePass> list= vehiclePassDao.findList(vehiclePass);
		logger.info("查询数据记录数: ["+(list == null? -1 : list.size())+"]");
		logger.info("Service查询结束!");
		return list;
	}
}
