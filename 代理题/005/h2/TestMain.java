package com.test.h2;

public class TestMain {

	public static void main(String[] args) {
		ConnH2 h2 = new ConnH2();
		Proxy proxy =new Proxy(h2);
		boolean createDB = proxy.createDB();
		System.out.println(createDB);
		boolean createTable = proxy.createTable();
		System.out.println(createTable);
		boolean insert = proxy.insert();
		System.out.println(insert);

	}

}
