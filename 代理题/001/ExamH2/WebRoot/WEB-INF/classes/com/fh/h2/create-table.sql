DROP TABLE "TBL_VEHICLE_PASS_lic" cascade constraints;

  CREATE TABLE "TBL_VEHICLE_PASS_lic" 
   (	"UID" VARCHAR2(32), 
		"PLATE_NO" VARCHAR2(32),
		"PASS_TM" VARCHAR2(32), 
		"COLOR" VARCHAR2(32),
		"CREATE_TM" VARCHAR2(32), 
		"CREATE_BY" VARCHAR2(32),
		"UPDATE_TM" VARCHAR2(32), 
		"UPDATE_BY" VARCHAR2(32),
		"VERSION" VARCHAR2(32), 
   ) ;
 
--------------------------------------------------------
--  DDL for Index TBL_H2_TEST
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_TBL_VEHICLE_PASS_lic" ON "TBL_VEHICLE_PASS_lic" ("UID") 
  ;
--------------------------------------------------------
--  Constraints for Table TBL_H2_TEST
--------------------------------------------------------

  ALTER TABLE "TBL_VEHICLE_PASS_lic" ADD CONSTRAINT "PK_TBL_VEHICLE_PASS_lic" PRIMARY KEY ("UID") ENABLE;
 
  ALTER TABLE "TBL_VEHICLE_PASS_lic" MODIFY ("UID" NOT NULL ENABLE);
