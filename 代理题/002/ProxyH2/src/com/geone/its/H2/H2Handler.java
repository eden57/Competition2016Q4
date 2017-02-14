package com.geone.its.H2;

import java.sql.ResultSet;

public interface H2Handler {
	public boolean createH2Database();
	public boolean dropH2Table(String tableName);
	public boolean createH2Table(String sql);
	public void destroyH2Database();
	public boolean updateData(String sql);
	public Object queryData(String sql);
	public boolean deleteData(String sql);
}
