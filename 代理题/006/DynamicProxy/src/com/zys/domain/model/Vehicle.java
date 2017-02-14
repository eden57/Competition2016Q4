/**
 * Copyright @ 2014-2015 GEONE Pte. Ltd. All Rights Reserved
 *
 **/

package com.zys.domain.model;

import java.sql.Date;

/**
 * 
 * @author zhuys
 * @date 2017年1月12日
 * @version 1.0
 */

public class Vehicle {
	private String plateNo;
	private Date passTm;
	private String color;
	private Date createTm;
	private String createBy;
	private Date updateTm;
	private String updateBy;
	private int version;

	public Vehicle() {
		super();
		// TODO Auto-generated constructor stub
	}

	public Vehicle(String plateNo, Date passTm, String color, Date createTm, String createBy, Date updateTm, String updateBy, int version) {
		super();
		this.plateNo = plateNo;
		this.passTm = passTm;
		this.color = color;
		this.createTm = createTm;
		this.createBy = createBy;
		this.updateTm = updateTm;
		this.updateBy = updateBy;
		this.version = version;
	}

	public String getPlateNo() {
		return plateNo;
	}

	public void setPlateNo(String plateNo) {
		this.plateNo = plateNo;
	}

	public Date getPassTm() {
		return passTm;
	}

	public void setPassTm(Date passTm) {
		this.passTm = passTm;
	}

	public String getColor() {
		return color;
	}

	public void setColor(String color) {
		this.color = color;
	}

	public Date getCreateTm() {
		return createTm;
	}

	public void setCreateTm(Date createTm) {
		this.createTm = createTm;
	}

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public Date getUpdateTm() {
		return updateTm;
	}

	public void setUpdateTm(Date updateTm) {
		this.updateTm = updateTm;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}

	public int getVersion() {
		return version;
	}

	public void setVersion(int version) {
		this.version = version;
	}

}
