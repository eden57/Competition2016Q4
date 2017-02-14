/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.its.dynamicproxy;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.sql.ResultSet;
import java.sql.Statement;

import com.geone.its.db.H2Factory;
import com.geone.its.db.OracleFactory;

/**
 * 
 * @author xudi
 * @date 2017年1月12日
 * @version 1.0
 */

public interface AbstractSubject {
	public abstract void request() throws Exception;

}
