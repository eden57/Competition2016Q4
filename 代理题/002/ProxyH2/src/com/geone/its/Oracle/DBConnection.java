package com.geone.its.Oracle;

import java.sql.Connection;
import java.sql.DriverManager;

public class DBConnection { 
    private final static String DB_URL = "jdbc:oracle:thin:@10.100.13.210:1521/itsdata";
    private final static String DB_DRIVER = "oracle.jdbc.driver.OracleDriver"; 
    private final static String DB_USERNAME = "itsprot"; 
    private final static String DB_PASSWORD = "itsprot"; 
    public Connection getConnection(){ 
        Connection conn = null; 
        try{ 
            Class.forName(DB_DRIVER); 
            conn = DriverManager.getConnection(DB_URL,DB_USERNAME,DB_PASSWORD); 
        }catch(Exception ex){ 
            ex.printStackTrace(); 
        } 
        return conn; 
    } 
    public void closeConnection(Connection conn){ 
        try{ 
            if(conn!=null){ 
                if(!conn.isClosed()){ 
                    conn.close(); 
                } 
            } 
        }catch(Exception ex){ 
            ex.printStackTrace(); 
        } 
    } 
     
}
