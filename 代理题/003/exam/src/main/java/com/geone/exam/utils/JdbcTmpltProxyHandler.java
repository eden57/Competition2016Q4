/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.utils;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

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
public class JdbcTmpltProxyHandler implements InvocationHandler {
	public Logger logger = Logger.getLogger(this.getClass());
	//　默认持有H2 数据源
	@Resource(name = "h2NamedJdbcTmplt")
    private NamedParameterJdbcTemplate h2NamedJdbcTmplt;
	@Resource(name = "oraNamedJdbcTmplt")
	private NamedParameterJdbcTemplate oraNamedJdbcTmplt;
	
    public JdbcTmpltProxyHandler() {
	}
	
    public Object bind(){
    	return Proxy.newProxyInstance(h2NamedJdbcTmplt.getClass().getClassLoader(), 
    			h2NamedJdbcTmplt.getClass().getInterfaces(), this);
    }
    
	@Override
	public Object invoke(Object proxy, Method method, Object[] args)
			throws Throwable {
		Object rst = null; 
		logger.info("========H2 数据源代理开始========");
		try {
			//测试强制故障
			//throw new DSFaultException("H2 故障");
			logger.info("采用H2 数据源");
			rst = method.invoke(h2NamedJdbcTmplt, args);
		} catch (Exception e) {
			//H2 数据源故障则采用ORACLE数据源
			try {
				logger.info("H2 数据源故障");
				//测试强制故障
				//throw new DSFaultException("H2 故障");
				logger.info("采用ORACLE 数据源");
				rst = method.invoke(oraNamedJdbcTmplt, args);
			} catch (Exception e2) {
				logger.info("H2/ORACLE 数据源全部故障");
				throw new DSFaultException("H2/ORACLE 数据源全部故障");
			}
			
		}finally{
			logger.info("========H2 数据源代理结束========");
		}
		return rst;
	}
	
}
