思路：
	1、(去掉OBJECTID重复键)建立OBJECTID主键，建立空间索引
		（1）AA01交通水利用地图斑2014 
			CREATE SPATIAL INDEX Index2014 ON [dbo].[AA01交通水利用地图斑2014](shape)  
    				WITH (BOUNDING_BOX=(XMIN=13271.3261,YMIN=-15453.2074,XMAX=120141.3099,YMAX=125406.0071))  
		（2）AB02非建设用地图斑2009
			CREATE SPATIAL INDEX Index2009 ON [dbo].[AB02非建设用地图斑2009](shape)  
    				WITH (BOUNDING_BOX=(XMIN=14578.6047 ,YMIN=35530.6381,XMAX=77981.5915,YMAX=117235.5185))  
	2、简化图形(获取图像外包矩形)
	2、简化图形(获取图像外包矩形)
	3、外包图形相交运算(过滤外包不相交图形)
	4、计算过滤后的实际图形公共部分
	5、计算公共部分面积及图形