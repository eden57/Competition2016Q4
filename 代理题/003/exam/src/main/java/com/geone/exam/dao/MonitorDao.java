/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.dao;

import java.util.HashMap;
import java.util.Map;

import javax.annotation.Resource;

import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;

/**
 * 
 * @author aaa
 * @date 2017年1月12日
 * @version 1.0
 */
@Repository
public class MonitorDao  extends BaseDao{
	@Resource(name="h2NamedJdbcTmplt")
	private NamedParameterJdbcTemplate h2NamedJdbcTmplt;
	
	@Resource(name="oraNamedJdbcTmplt")
	private NamedParameterJdbcTemplate oraNamedJdbcTmplt;
	
	public boolean getOraDsStatus(){
		String sql = "SELECT 1 FROM DUAL";
		Map<String, String> map = new HashMap<String, String>();
		int cnt = oraNamedJdbcTmplt.queryForObject(sql, map, Integer.class);
		return cnt ==1;
	}
	
	public boolean getH2DsStatus(){
		String sql = "SELECT 1 FROM DUAL";
		Map<String, String> map = new HashMap<String, String>();
		int cnt = h2NamedJdbcTmplt.queryForObject(sql, map, Integer.class);
		return cnt ==1;
	}
}
