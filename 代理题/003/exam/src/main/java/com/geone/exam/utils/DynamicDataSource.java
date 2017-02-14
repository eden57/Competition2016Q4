/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.utils;

import org.apache.log4j.Logger;
import org.springframework.jdbc.datasource.lookup.AbstractRoutingDataSource;

/**
 * 
 * @author aaa
 * @date 2017年1月12日
 * @version 1.0
 */

public class DynamicDataSource extends AbstractRoutingDataSource {
	public Logger logger = Logger.getLogger(this.getClass());

	@Override
	protected Object determineCurrentLookupKey() {
		return DsHolder.getDBType();
	}

}
