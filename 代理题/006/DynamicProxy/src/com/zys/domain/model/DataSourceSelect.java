/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.zys.domain.model;

/**
 * 
 * @author zhuys
 * @date 2017年1月12日
 * @version 1.0
 */

public interface DataSourceSelect {
	void selectH2() throws Exception;
	void selectOracle() throws Exception;
}
