package com.geone.its.service;

import java.sql.Timestamp;
import java.util.List;

import com.geone.its.entity.VehiclePass;

public interface ItsService {
	public List<VehiclePass> getAllVehiclePass();
	public List<VehiclePass> getVehiclePassByPlateNo(String plateNo);
	public List<VehiclePass> getVehiclePassByPassTime(Timestamp startTM,Timestamp endTM);
	
	public List<VehiclePass> getAllVehiclePassByOracle();
}
