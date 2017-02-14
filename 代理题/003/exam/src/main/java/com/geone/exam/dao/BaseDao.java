/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.dao;

import javax.annotation.Resource;

import org.apache.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcOperations;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;

import com.geone.exam.exception.DSFaultException;
import com.geone.exam.utils.DsHolder;
import com.geone.exam.utils.JdbcTmpltProxyHandler;

/**
 * 
 * @author aaa
 * @date 2017年1月12日
 * @version 1.0
 */
@Repository
public class BaseDao {
	public Logger logger = Logger.getLogger(this.getClass());
	@Autowired
	protected JdbcTmpltProxyHandler jdbcTmpltProxyHandler;
	protected NamedParameterJdbcOperations namedJdbcTmpltProxy;
	
	@Autowired
	private JdbcTemplateSelector jdbcTemplateSelector;
	
	@Resource(name = "dynNamedJdbcTmplt")
	private NamedParameterJdbcTemplate dynNamedJdbcTmplt;
	
	public BaseDao() {
	}
	
	protected NamedParameterJdbcTemplate getNamedJdbcTmplt() throws DSFaultException{
		return jdbcTemplateSelector.getNamedJdbcTmplt();
	}

	protected NamedParameterJdbcTemplate getNamedJdbcTmplt2() throws DSFaultException{
		DsHolder.setDBType(DsHolder.ORA_DATA_SOURCE);
		DsHolder.setDBType(DsHolder.H2_DATA_SOURCE);
		return dynNamedJdbcTmplt;
	}
}
