package com.geone.its.Oracle;

import java.sql.Connection;

public class DBFactory { 
    private DBConnection dbConn = new DBConnection(); 
    public DBConnection getDBConnectionInstance(){ 
        if(dbConn==null){ 
            dbConn = new DBConnection(); 
            return dbConn; 
        } 
        else{ 
            return dbConn; 
        } 
    } 
    
    public void closeConnection(Connection conn){ 
        if(dbConn==null){ 
            dbConn = new DBConnection(); 
        } 
        dbConn.closeConnection(conn);
    } 
}
