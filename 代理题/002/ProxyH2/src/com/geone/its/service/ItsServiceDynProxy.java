package com.geone.its.service;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;

public class ItsServiceDynProxy implements InvocationHandler {

    private ItsService itsService;
    
    public ItsServiceDynProxy(Object obj)
    {
        this.itsService = (ItsService) obj;
    }
	
	@Override
	public Object invoke(Object object, Method method, Object[] args) throws Throwable {
        
        System.out.println("Method:" + method);
        
        Object result = method.invoke(itsService, args);
        if(null == result){
        	if(method.getName().contains("getAllVehiclePass")){
        		result = itsService.getAllVehiclePassByOracle();
        	}
        }
        
        System.out.println("after");
        
        return result;
	}
}
