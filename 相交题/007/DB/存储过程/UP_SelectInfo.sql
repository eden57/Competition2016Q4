USE [A_Qiancy]
GO
/****** Object:  StoredProcedure [dbo].[up_analyse]    Script Date: 2016/12/30 15:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<yangzw>
-- Create date: <20161230>
-- Description:	<查询>
-- EXEC [dbo].[UP_SelectInfo]
-- =============================================
CREATE PROCEDURE [dbo].[UP_SelectInfo]
AS
BEGIN

	DECLARE @Shape				Geometry
	DECLARE @xmin				FLOAT
	DECLARE @ymin				FLOAT
	DECLARE @xmax				FLOAT
	DECLARE @ymax				FLOAT
	
	
	DECLARE @T TABLE 
	(
		AID			NVARCHAR(50),
		BID			NVARCHAR(50),
		DJMJ		FLOAT
	)
                                  
	DECLARE @n INT 
	DECLARE @rows INT                         
	SELECT @rows=COUNT(1) FROM AA01交通水利用地图斑2014
	SET @rows=@rows+1
	SET @n=1
	WHILE @n < @rows
	BEGIN
		SELECT @Shape=SHAPE,@xmin=xmin,@xmax=xmax,@ymin=ymin,@ymax=ymax
		FROM AA01交通水利用地图斑2014 WHERE OBJECTID=@n
		
		INSERT INTO @T
		SELECT @n,a.OBJECTID,round(a.SHAPE.STIntersection(@Shape).STArea(),2)
		FROM (
			SELECT a.OBJECTID,a.SHAPE
			FROM AB02非建设用地图斑2009 a
			WHERE (a.xmax>@xmin AND a.xmax<@xmax AND a.ymax>@ymin AND a.ymax<@ymax)
				OR (a.xmax>@xmin AND a.xmax<@xmax AND a.ymin>@ymin AND a.ymin<@ymax)
				OR (a.xmin>@xmin AND a.xmin<@xmax AND a.ymax>@ymin AND a.ymax<@ymax)
				OR (a.xmin>@xmin AND a.xmin<@xmax AND a.ymin>@ymin AND a.ymin<@ymax)
		) a
		WHERE a.SHAPE.STIntersects(@Shape) =1 and a.SHAPE.STIntersection(@Shape).STArea() > 0.01	
		SET @n = @n + 1
	END

	SELECT * FROM @T
END
