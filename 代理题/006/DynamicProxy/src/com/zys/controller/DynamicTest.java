/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.zys.controller;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;

import com.zys.domain.model.DataSourceSelect;
import com.zys.domain.service.Impl.MyInvocation;
import com.zys.persistence.DataSourceSelectImpl;

/**
 * 
 * @author zhuys
 * @date 2017年1月12日
 * @version 1.0
 */

public class DynamicTest {
	public static void main(String[] args) throws Exception {
		DataSourceSelect dsSelect=new DataSourceSelectImpl();
		MyInvocation myInvocation=new MyInvocation(dsSelect);
		DataSourceSelect proxy=(DataSourceSelect)myInvocation.getProxy();
		proxy.selectH2();
		System.out.println("                               ");
		proxy.selectOracle();
	}
}
