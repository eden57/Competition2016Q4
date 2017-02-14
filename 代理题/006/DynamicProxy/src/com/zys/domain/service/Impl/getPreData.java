/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.zys.domain.service.Impl;

import java.util.List;

import com.zys.domain.model.Vehicle;
import com.zys.persistence.OracleData;

/**
 * 
 * @author zhuys
 * @date 2017年1月12日
 * @version 1.0
 */

public class getPreData {
	public List<Vehicle> getData(){
		List<Vehicle> list=null;
		OracleData od=new OracleData();
		list=od.query();
		return list;
	}
}
