/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.utils;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

import org.apache.log4j.Logger;

/**
 * 
 * @author aaa
 * @date 2017年1月12日
 * @version 1.0
 */

public class ProxyHandler implements InvocationHandler {
	public Logger logger = Logger.getLogger(this.getClass());
	
	//　这个就是我们要代理的真实对象
    private Object subject;
    
    public Object bind(Object subject){
    	this.subject = subject;
    	return Proxy.newProxyInstance(subject.getClass().getClassLoader(), 
    			subject.getClass().getInterfaces(), this);
    }
    
	@Override
	public Object invoke(Object proxy, Method method, Object[] args)
			throws Throwable {
		Object rst = null; 
		logger.info("代理开始");
		rst = method.invoke(subject, args);
		logger.info("代理结束");
		return rst;
	}
	
}
