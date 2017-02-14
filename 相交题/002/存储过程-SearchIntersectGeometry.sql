USE [Intersect_Data]
GO

/****** Object:  StoredProcedure [dbo].[SearchIntersectGeometry]    Script Date: 2016/12/29 17:35:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SearchIntersectGeometry]
	-- Add the parameters for the stored procedure here
	-- @table1 NVARCHAR(50), 
	-- @table2 NVARCHAR(50)
AS
BEGIN

	


	declare @AA01_OBJECTID			int
	declare @AA01_SHAPE				geometry
	declare @AB02_OBJECTID			int
	declare @AB02_SHAPE				geometry


	CREATE TABLE #TEMP (
		[AA01_OBJECTID] [int] NOT NULL,
		[AB02_OBJECTID] [int] NOT NULL,
		[INTERSECT_AREA] [float] NULL
	)


	-- cursor_AA01
	DECLARE cursor_AA01 CURSOR
	   FOR select OBJECTID, SHAPE from AA01交通水利用地图斑2014
	


	OPEN cursor_AA01
	FETCH NEXT FROM cursor_AA01 INTO @AA01_OBJECTID, @AA01_SHAPE

	WHILE @@FETCH_STATUS = 0
	BEGIN
		-- cursor_AB02
		DECLARE cursor_AB02 CURSOR FOR
			SELECT OBJECTID, SHAPE FROM AB02非建设用地图斑2009

		OPEN cursor_AB02
		FETCH NEXT FROM cursor_AB02 INTO @AB02_OBJECTID, @AB02_SHAPE

		WHILE @@FETCH_STATUS = 0
		BEGIN
			-- query intersect
			IF @AA01_SHAPE.STIntersects(@AB02_SHAPE) = 1 
			BEGIN  
				INSERT INTO #TEMP
					SELECT @AA01_OBJECTID AS AA01_OBJECTID, @AB02_OBJECTID AS AB02_OBJECTID, @AA01_SHAPE.STIntersection(@AB02_SHAPE).STArea() AS INTERSECT_AREA
			END  

			

			FETCH NEXT FROM cursor_AB02 INTO @AB02_OBJECTID, @AB02_SHAPE
		END

		CLOSE cursor_AB02
		DEALLOCATE cursor_AB02
		
		 
		
		

		FETCH NEXT FROM cursor_AA01 INTO @AA01_OBJECTID, @AA01_SHAPE
	END

	CLOSE cursor_AA01
	DEALLOCATE cursor_AA01


	INSERT INTO [Intersect_Data].[dbo].[AA01_AB02_INTERSECT]
		SELECT * FROM #TEMP WHERE [INTERSECT_AREA]>0
END

GO


