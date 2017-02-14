
/****** Object:  UserDefinedFunction [dbo].[Cross_Rect]    Script Date: 2016/12/29 17:02:58 ******/
DROP FUNCTION [dbo].[Cross_Rect]
GO

/****** Object:  UserDefinedFunction [dbo].[Cross_Rect]    Script Date: 2016/12/29 17:02:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		caoj
-- Create date: 2016-12-29
-- Description:	得到矩形是否相交
-- =============================================
CREATE FUNCTION  [dbo].[Cross_Rect]
(@AXMin FLOAT,@AXMax FLOAT ,@AYMin FLOAT ,@AYMax FLOAT,
@BXMin FLOAT,@BXMax FLOAT ,@BYMin FLOAT ,@BYMax FLOAT)

RETURNS BIT
AS
BEGIN
		
	DECLARE @zx FLOAT
	DECLARE @x FLOAT
	DECLARE @zy FLOAT
	DECLARE @y FLOAT
	
    SET @zx = ABS(@AXMin+@AXMax-@BXMin-@BXMin)--两个矩形重心在x轴上的距离的两倍
    SET @x = ABS(@AXMin-@AXMax)+ABS(@BXMin-@BXMax)--两矩形在x方向的边长的和
    SET @zy = ABS(@AYMin+@AYMax-@BYMin-@BYMax)--重心在y轴上距离的两倍
    SET @y = ABS(@AYMin-@AYMax)+ABS(@BYMin-@BYMax)--y方向边长的和
                                                 
                                                  
    IF((@zx <= @x) AND (@zy <= @y))
       RETURN 1
       
    RETURN 0
    	
END

GO



/****** Object:  StoredProcedure [dbo].[IntersectionTB]    Script Date: 2016/12/29 17:02:40 ******/
DROP PROCEDURE [dbo].[IntersectionTB]
GO

/****** Object:  StoredProcedure [dbo].[IntersectionTB]    Script Date: 2016/12/29 17:02:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		caoj
-- Create date: 2016-12-29
-- Description:	得到[dbo].[AA01交通水利用地图斑2014]与[dbo].[AB02非建设用地图斑2009] 叠加面积
-- =============================================
CREATE PROCEDURE [dbo].[IntersectionTB]
	
AS
BEGIN
	
	CREATE TABLE #sl
	(
		[ids] [int] IDENTITY(1,1) NOT NULL,
		[OBJECTID] INT ,
		[SHAPE] GEOMETRY,
		JXSHAPE GEOMETRY,
		XMin FLOAT,
		XMax FLOAT,
		YMin FLOAT,
		YMax FLOAT,
		temp FLOAT)

	INSERT INTO #sl([OBJECTID],[SHAPE],JXSHAPE)
		SELECT [OBJECTID],[SHAPE], [SHAPE].STEnvelope()
		FROM [dbo].[AA01交通水利用地图斑2014]

	UPDATE #sl SET XMin=JXSHAPE.STPointN(1).STX,YMin= JXSHAPE.STPointN(1).STY,XMax=JXSHAPE.STPointN(3).STX,YMax= JXSHAPE.STPointN(3).STY

	UPDATE #sl SET temp=XMin  WHERE XMin>XMax
	UPDATE #sl SET XMin=XMax  WHERE temp IS NOT null
	UPDATE #sl SET XMax=temp  WHERE temp IS NOT NULL
	UPDATE #sl SET temp =null WHERE temp IS NOT NULL


	UPDATE #sl SET temp=YMin  WHERE YMin>YMax
	UPDATE #sl SET YMin=YMax  WHERE temp IS NOT null
	UPDATE #sl SET YMax=temp  WHERE temp IS NOT NULL



	CREATE TABLE #js
	(
		[ids] [int] IDENTITY(1,1) NOT NULL,
		[OBJECTID] INT ,
		[SHAPE] GEOMETRY,
		JXSHAPE GEOMETRY,
		XMin FLOAT,
		XMax FLOAT,
		YMin FLOAT,
		YMax FLOAT,
		temp FLOAT
	)

	INSERT INTO #js([OBJECTID],[SHAPE],JXSHAPE)
		SELECT [OBJECTID],[SHAPE], [SHAPE].STEnvelope()
		FROM [dbo].[AB02非建设用地图斑2009]

	UPDATE #js SET XMin=JXSHAPE.STPointN(1).STX,YMin= JXSHAPE.STPointN(1).STY,XMax=JXSHAPE.STPointN(3).STX,YMax= JXSHAPE.STPointN(3).STY

	UPDATE #js SET temp=XMin  WHERE XMin>XMax
	UPDATE #js SET XMin=XMax  WHERE temp IS NOT null
	UPDATE #js SET XMax=temp  WHERE temp IS NOT NULL

	UPDATE #js SET temp =null WHERE temp IS NOT NULL


	UPDATE #js SET temp=YMin  WHERE YMin>YMax
	UPDATE #js SET YMin=YMax  WHERE temp IS NOT null
	UPDATE #js SET YMax=temp  WHERE temp IS NOT NULL



	CREATE TABLE #result 
	(
		[ids] [int] IDENTITY(1,1) NOT NULL,
		AOBJECTID int,
		BOBJECTID INT ,
		CArea FLOAT
	)

	DECLARE @OBJECTID int
	DECLARE @shape GEOMETRY
	DECLARE @XMin FLOAT
	DECLARE @XMax  FLOAT
	DECLARE @YMin  FLOAT
	DECLARE @YMax FLOAT
	
	DECLARE myCursor1 CURSOR
		FOR 
		SELECT OBJECTID,[SHAPE] ,XMin ,XMax  ,YMin  ,YMax FROM #sl --WHERE OBJECTID IN(SELECT OBJECTID FROM [dbo].[AA01交通水利用地图斑2014] where ifq='相城区')
		ORDER BY OBJECTID--=	788181--[OBJECTID],[SHAPE]    
	OPEN myCursor1
	FETCH NEXT FROM myCursor1 INTO  @OBJECTID,@shape,@XMin ,@XMax  ,@YMin  ,@YMax
	WHILE @@FETCH_STATUS = 0
	BEGIN	
	
		SELECT [OBJECTID],SHAPE into #a FROM #js where [dbo].[Cross_Rect](@XMin ,@XMax  ,@YMin  ,@YMax,#js.XMin ,#js.XMax  ,#js.YMin  ,#js.YMax)=1 --SHAPE.Filter(@shape)=1
			
		INSERT INTO #result(AOBJECTID ,BOBJECTID  ,CArea )
			SELECT @OBJECTID AS AOBJECTID,[OBJECTID]  AS BOBJECTI,SHAPE.STIntersection(@shape).STArea() FROM #a WHERE SHAPE.STIntersection(@shape).STArea()>0
 
		DROP TABLE #a
		
		FETCH NEXT FROM myCursor1 INTO   @OBJECTID,@shape,@XMin ,@XMax  ,@YMin  ,@YMax
	END
	CLOSE myCursor1
	DEALLOCATE myCursor1
	
	
	SELECT * FROM #result ORDER BY [ids]-- AOBJECTID,BOBJECTID
END

GO


