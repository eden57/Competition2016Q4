1.给表1"AA01交通水利用地图斑2014"添加xmax，xmin，ymax，ymin列
2.给表2"AB02非建设用地图斑2009"添加xmax，xmin，ymax，ymin列.
3.预先给两个表生成xmax，xmin，ymax，ymin
4.给表1和表2添加索引
5.假设认为表1和表2被一个外包的矩形包围起来那么它的四个点即为
（xmin，ymax），（xmax，ymax），（xmax，ymin），（xmin，ymin）
6.我们认为表1的四个点中只要有一个点在表2的外包矩形中，那么这两个图形就有相交。
7.通过上一步我们删除很大一部分不想交的情况，接下里用sql server自带的空间函数做精确的相交判断，得出结果。