/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.its.dynamicproxy;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;

/**
 * 
 * @author xudi
 * @date 2017年1月12日
 * @version 1.0
 */

class DynamicProxy implements InvocationHandler {

	Object obj = null;

	public DynamicProxy(Object obj) {
		this.obj = obj;
	}

	@Override
	public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
		// TODO Auto-generated method stub
		Object result = method.invoke(this.obj, args);
		return result;
	}

}