/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.utils;

/**
 * 
 * @author aaa
 * @date 2017年1月12日
 * @version 1.0
 */

public class DsHolder {
    public static final String H2_DATA_SOURCE = "h2Ds";  
    public static final String ORA_DATA_SOURCE = "oraDs";  
      
    private static final ThreadLocal<String> threadLocal = new ThreadLocal<String>();  
      
    public static void setDBType(String dbType) {  
    	threadLocal.set(dbType);  
    }  
      
    public static String getDBType() {  
        return threadLocal.get();  
    }  
      
    public static void clearDBType() {  
    	threadLocal.remove();  
    }  
}
