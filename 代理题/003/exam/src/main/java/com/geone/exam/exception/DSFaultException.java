/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.geone.exam.exception;

/**
 * 
 * @author aaa
 * @date 2017年1月12日
 * @version 1.0
 */

public class DSFaultException extends Exception {
	private static final long serialVersionUID = -6566209748883051127L;

	public DSFaultException() {
		super();
	}

	public DSFaultException(String message) {
		super(message);
	}

	public DSFaultException(Throwable cause) {
		super(cause);
	}

	public DSFaultException(String message, Throwable cause) {
		super(message, cause);
	}
}
