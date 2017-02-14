/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.its.dynamicproxy;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Proxy;
import java.sql.SQLException;

import com.geone.its.db.H2Factory;
import com.geone.its.db.OracleFactory;

/**
 * 
 * @author xudi
 * @date 2017年1月12日
 * @version 1.0
 */

public class TestDynamicProxy {

	public static void main(String[] args) throws Exception {
		AbstractSubject realSubject = new RealSubject();
		ClassLoader loader = realSubject.getClass().getClassLoader();
		Class<?>[] interfaces = realSubject.getClass().getInterfaces();
		InvocationHandler handler = new DynamicProxy(realSubject);
		AbstractSubject proxy = (AbstractSubject) Proxy.newProxyInstance(loader, interfaces, handler);
		proxy.request();
		CloseConnection();

	}

	public static void CloseConnection() throws Exception {
		H2Factory.CloseConnection();
		OracleFactory.CloseConnection();
	}

}
