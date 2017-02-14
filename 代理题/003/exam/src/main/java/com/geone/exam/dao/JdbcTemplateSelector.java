/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.dao;

import java.util.HashMap;
import java.util.Map;

import javax.annotation.Resource;

import org.apache.log4j.Logger;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Component;

import com.geone.exam.exception.DSFaultException;

/**
 * 
 * @author aaa
 * @date 2017年1月12日
 * @version 1.0
 */

@Component
public class JdbcTemplateSelector {
	public Logger logger = Logger.getLogger(this.getClass());

	@Resource(name = "h2NamedJdbcTmplt")
	private NamedParameterJdbcTemplate h2NamedJdbcTmplt;

	@Resource(name = "oraNamedJdbcTmplt")
	private NamedParameterJdbcTemplate oraNamedJdbcTmplt;

	protected NamedParameterJdbcTemplate getNamedJdbcTmplt() throws DSFaultException{
		if(getH2DsStatus()){
			logger.info("采用H2 数据源!");
			return h2NamedJdbcTmplt;
		}else if(getOraDsStatus()){
			logger.info("采用Oracle 数据源!");
			return oraNamedJdbcTmplt;
		}else{
			logger.error("H2/ORACLE数据库同时故障!");
			throw new DSFaultException("H2/ORACLE数据库同时故障!");
		}
	}

	public boolean getH2DsStatus() {
		try {
			String sql = "SELECT 1 FROM DUAL";
			//模拟故障
			sql = "SELECT 1 FROM XXXX";
			Map<String, String> map = new HashMap<String, String>();
			int cnt = h2NamedJdbcTmplt.queryForObject(sql, map, Integer.class);
			return cnt == 1;
		} catch (Exception e) {
			logger.error("H2 数据源故障!");
			return false;
		}
	}

	public boolean getOraDsStatus() {
		try {
			String sql = "SELECT 1 FROM DUAL";
			//模拟故障
			///sql = "SELECT 1 FROM XXXX";
			Map<String, String> map = new HashMap<String, String>();
			int cnt = oraNamedJdbcTmplt.queryForObject(sql, map, Integer.class);
			return cnt == 1;
		} catch (Exception e) {
			logger.error("Oracle 数据源故障!");
			return false;
		}
	}
}
