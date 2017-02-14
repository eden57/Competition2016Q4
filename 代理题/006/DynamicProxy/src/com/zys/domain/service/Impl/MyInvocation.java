/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.zys.domain.service.Impl;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

/**
 * 
 * @author zhuys
 * @date 2017年1月12日
 * @version 1.0
 */

public class MyInvocation implements InvocationHandler {
	private Object target;

    public MyInvocation(Object target) {
        this.target = target;
    }
	
	@Override
	public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
		System.out.println("************before**************");
		System.out.println(method.getName());
		
        Object obj = method.invoke(target, args);
        System.out.println("************after**************");
        return obj;
	}

	public Object getProxy() {
        return Proxy.newProxyInstance(Thread.currentThread().getContextClassLoader(),
                target.getClass().getInterfaces(), this);
    }

    public void print() {
        Class cl = Proxy.getProxyClass(Thread.currentThread().getContextClassLoader(),
                target.getClass().getInterfaces());
    }
	
}
