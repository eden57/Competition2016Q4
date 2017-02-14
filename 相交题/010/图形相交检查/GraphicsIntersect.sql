with A as(
	select   OBJECTID,SHAPE,SHAPE.STEnvelope() as extentA from [A_Qiancy].[dbo].[AA01交通水利用地图斑2014]
),
B as (
	select   OBJECTID,SHAPE,SHAPE.STEnvelope() as extentB from [A_Qiancy].[dbo].[AB02非建设用地图斑2009]
),
AB AS (
	select   A.OBJECTID as AID,B.OBJECTID as BID,A.SHAPE AS Ashape,B.SHAPE as Bshape from A,B  where A.extentA.STIntersects(B.extentB)=1
),
UnionShapeS as (
	SELECT AID,BID,Ashape.STUnion(Bshape) as UShape FROM AB
)
select AID,BID,UShape,UShape.STArea() as unionArea  from UnionShapeS
