package com.geone.its.entity;

import java.sql.Timestamp;

public class VehiclePass {
	private String uid;
	private String plateNo;
	private Timestamp passTime;
	private String color;
	private Timestamp creatTm;
	private String creatBy;
	private Timestamp updateTm;
	private String updateBy;
	private String version;
	public String getUid() {
		return uid;
	}
	public void setUid(String uid) {
		this.uid = uid;
	}
	public String getPlateNo() {
		return plateNo;
	}
	public void setPlateNo(String plateNo) {
		this.plateNo = plateNo;
	}
	public Timestamp getPassTime() {
		return passTime;
	}
	public void setPassTime(Timestamp passTime) {
		this.passTime = passTime;
	}
	public String getColor() {
		return color;
	}
	public void setColor(String color) {
		this.color = color;
	}
	public Timestamp getCreatTm() {
		return creatTm;
	}
	public void setCreatTm(Timestamp creatTm) {
		this.creatTm = creatTm;
	}
	public String getCreatBy() {
		return creatBy;
	}
	public void setCreatBy(String creatBy) {
		this.creatBy = creatBy;
	}
	public Timestamp getUpdateTm() {
		return updateTm;
	}
	public void setUpdateTm(Timestamp updateTm) {
		this.updateTm = updateTm;
	}
	public String getUpdateBy() {
		return updateBy;
	}
	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}
	public String getVersion() {
		return version;
	}
	public void setVersion(String version) {
		this.version = version;
	}
}
