--准备：
--postgresql96.1
--数据库连接：192.168.34.219 端口5432  库名sipbusdw
--1、创建目标表
CREATE TABLE public.AA01交通水利用地图斑2014(
	OBJECTID integer NOT NULL DEFAULT 0,
	ifq character varying(100) NULL,
	DLBM character varying(4) NULL,
	DLMC character varying(60) NULL,
	BSM integer NULL,
	YSDM character varying(10) NULL,
	XZDWMJ numeric(38, 8) NULL,
	QSXZ character varying(3) NULL,
	SHAPE geometry NULL,
	geotmp bytea
) 

CREATE TABLE public.AB02非建设用地图斑2009(
	OBJECTID integer NOT NULL DEFAULT 0,
        SHAPE geometry NULL,
	geotmp bytea
)

--2、导数据kettle工具
在etl工具中执行 test4.ktr 包
--3、更新geometry，及生产最小外包矩形
----public.AA01交通水利用地图斑2014
update public.AA01交通水利用地图斑2014 a set shape= geometry(a.geotmp)

ALTER TABLE public.AA01交通水利用地图斑2014 ADD bounding_box geometry;
update public.AA01交通水利用地图斑2014 a set bounding_box= ST_Envelope(shape)

----public.AB02非建设用地图斑2009
update public.AB02非建设用地图斑2009 a set shape= geometry(a.geotmp)

ALTER TABLE public.AB02非建设用地图斑2009 ADD bounding_box geometry;
update public.AB02非建设用地图斑2009 a set bounding_box= ST_Envelope(shape)

--4、创建索引
----public.AA01交通水利用地图斑2014
CREATE INDEX idx_spatial_sltb ON public.AA01交通水利用地图斑2014 USING GIST ( shape );
CREATE INDEX idx_spatial_sltbbox ON public.AA01交通水利用地图斑2014 USING GIST ( bounding_box );
VACUUM ANALYZE public.AA01交通水利用地图斑2014 (shape);
VACUUM ANALYZE public.AA01交通水利用地图斑2014 (bounding_box);

----public.AB02非建设用地图斑2009
CREATE INDEX idx_spatial_jsydtb ON public.AB02非建设用地图斑2009 USING GIST ( shape );
CREATE INDEX idx_spatial_jsydtbbox ON public.AB02非建设用地图斑2009 USING GIST ( bounding_box );
VACUUM ANALYZE public.AB02非建设用地图斑2009(shape);
VACUUM ANALYZE public.AB02非建设用地图斑2009(bounding_box);


--5、计算，运行时间12分10秒
--,ST_AsBinary(ST_GeomFromText('LINESTRING(1 2, 3 4)'))
--,ST_AsText(ST_Envelope(shape))
--,ST_GeomFromText('POLYGON (' || box(shape) || ')',0)
--,ST_GeomFromText(box(shape)) 
drop table t_Intersect
WITH Filter AS ( 
select a.*

from public.AA01交通水利用地图斑2014 a 
--limit 5 offset 0
),
Filter2 AS ( 
select a.*

from public.AB02非建设用地图斑2009 a
)
select * into t_Intersect from (
SELECT a.OBJECTID AS A表唯一编码,
       b.OBJECTID AS B表唯一编码,
      BSM
,ST_Intersection(a.shape,b.shape) AS IntersectShape
,ST_Area(ST_Intersection(a.shape,b.shape)) AS 重叠面积
  FROM Filter a join Filter2 b
   ON 
   ST_Intersects(a.bounding_box, b.bounding_box) ='true'
   AND
  ST_Intersects(a.shape,b.shape)='true'
  ) a

--6、检核计算结果
select * from t_Intersect

select * from public.AB02非建设用地图斑2009
SELECT a.OBJECTID AS A表唯一编码,
       b.OBJECTID AS B表唯一编码,
      BSM
  FROM public.AA01交通水利用地图斑2014 a join public.AB02非建设用地图斑2009 b
   ON ST_Intersects(a.shape,a.shape)='true'

ST_Overlaps(geometry A, geometry B);