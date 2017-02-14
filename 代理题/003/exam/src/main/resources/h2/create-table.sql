DROP TABLE "TBL_VEHICLE_PASS" cascade constraints;
--------------------------------------------------------
--  DDL for Table TBL_VEHICLE_PASS
--------------------------------------------------------

  CREATE TABLE "TBL_VEHICLE_PASS" 
   (	
		"UID" VARCHAR2(32), 
		"PLATE_NO" VARCHAR2(50), 
		"PASS_TM" DATE, 
		"COLOR" VARCHAR2(50), 
		"CREATE_TM" DATE, 
		"CREATE_BY" VARCHAR2(50),
		"UPDATE_TM" DATE, 
		"UPDATE_BY" VARCHAR2(50), 
		"VERSION" INTEGER
   ) ;
 
--------------------------------------------------------
--  DDL for Index TBL_VEHICLE_PASS
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_TBL_VEHICLE_PASS" ON "TBL_VEHICLE_PASS" ("UID") 
  ;
--------------------------------------------------------
--  Constraints for Table TBL_VEHICLE_PASS
--------------------------------------------------------

  ALTER TABLE "TBL_VEHICLE_PASS" ADD CONSTRAINT "PK_TBL_VEHICLE_PASS" PRIMARY KEY ("UID") ENABLE;
 
  ALTER TABLE "TBL_VEHICLE_PASS" MODIFY ("UID" NOT NULL ENABLE);
