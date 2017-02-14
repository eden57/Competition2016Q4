package com.geone.exam;

import org.apache.commons.lang3.exception.ExceptionUtils;
import org.apache.log4j.Logger;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.geone.exam.exception.DSFaultException;
import com.geone.exam.model.VehiclePass;
import com.geone.exam.svs.VehiclePassService;

/**
 * App
 *
 */
public class App {
	public static final Logger logger = Logger.getLogger(App.class);

	@SuppressWarnings("resource")
	public static void main(String[] args) {
		logger.info("##################执行开始##################");
		ApplicationContext context = new ClassPathXmlApplicationContext("classpath:spring/app-ctx.xml");// 此文件放在SRC目录下
		VehiclePassService vehiclePassService = (VehiclePassService) context.getBean("vehiclePassService");
		try {
			vehiclePassService.findList(new VehiclePass());
		} catch (DSFaultException e) {
			logger.error(ExceptionUtils.getStackTrace(e));
		}
		logger.info("##################执行结束##################");
	}
}
