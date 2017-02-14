#include "RasterReadWrite.h"

#include <gdal_priv.h>
#pragma comment(lib,"gdal_i.lib")
#include <iostream>
#include<exception>
using namespace std;

CRasterReadWrite::CRasterReadWrite(void){};
CRasterReadWrite::~CRasterReadWrite(void){};



//读数据
CRasterData* CRasterReadWrite::Read(const char* fileName)
{
	GDALAllRegister();     //注册数据类型驱动
	GDALDataset *poDataset = (GDALDataset*)GDALOpen(fileName, GA_ReadOnly);  //打开数据
	if (poDataset == NULL)
	{
		GDALDestroyDriverManager();
		return NULL;
	}
	int nCol = poDataset->GetRasterXSize();    //获取列数
	int nRow = poDataset->GetRasterYSize();    //获取行数

	double xTopLeft, yTopLeft;
	double cellSizeX;
	double cellSizeY;
	double adfGeoTransform[6];
	if (poDataset->GetGeoTransform(adfGeoTransform) == CE_None)
	{
		xTopLeft = adfGeoTransform[0];
		cellSizeX = adfGeoTransform[1];
		//adfGeoTransform[2] /* rotation, 0 if image is "north up" */
		yTopLeft = adfGeoTransform[3];
		cellSizeY = adfGeoTransform[5];
		// adfGeoTransform[4] /* rotation, 0 if image is "north up" */
	}
	else
	{
		GDALClose(poDataset);
		GDALDestroyDriverManager();
		return NULL;
	}

	GDALRasterBand *poBand = poDataset->GetRasterBand(1);	    //获取第一波段数据
	int* pafScanline = (int*)CPLMalloc(sizeof(float)*nCol*nRow);
	poBand->RasterIO(GF_Read, 0, 0, nCol, nRow, pafScanline, nCol, nRow, GDT_Int32, 0, 0);

	int noDataVaue = (float)poBand->GetNoDataValue();
	CRasterData *raster = new CRasterData(xTopLeft, yTopLeft, -1 , -1 ,cellSizeX, cellSizeY, nRow, nCol, noDataVaue);   //结构体初始化

	for (int row = 0; row < nRow; row++)
	{
		for (int col = 0; col < nCol; col++)
		{
			int v = pafScanline[row*nCol + col];
			raster->setValue(row, col, v);
		}
	}

	try
	{
		CPLFree(pafScanline);
		pafScanline = NULL;

		GDALClose(poDataset);
		poDataset = NULL;

 		//GDALClose (poBand);
 		//poBand = NULL;
	}

	catch (exception* e)
	{
		cout << e->what ()<< endl;
	}

	GDALDestroyDriverManager();
	return raster;
}


//写数据
bool CRasterReadWrite::Write(CRasterData* result, const char *resultFileName)
{
	GDALAllRegister();     //注册数据类型驱动
	GDALDataset *poDataset = (GDALDataset*)GDALOpen(resultFileName, GA_Update);  //打开数据
	if (poDataset == NULL)
	{
		//创建结果
		CreateFile(result, resultFileName, (char*)"GTiff");
		poDataset = (GDALDataset*)GDALOpen(resultFileName, GA_Update);  //打开数据
	}


	GDALRasterBand *poBand = poDataset->GetRasterBand(1);	    //获取第一波段数据
	if (poBand == NULL){
		cout << "error: bad createFile()" << endl;
		return false;
	}
	poBand->SetNoDataValue(result->noDataValue());
	int* pafScanline = (int*)CPLMalloc(sizeof(float)* result->nRow()*result->nCol());
	for (int row = 0; row < result->nRow(); row++)
	{
		for (int col = 0; col < result->nCol(); col++)
		{
			int v = result->getValue(row, col);
			pafScanline[row*result->nCol() + col] = v;
		}
	}

	poBand->RasterIO(GF_Write, 0, 0, result->nCol(), result->nRow(), pafScanline, result->nCol(), result->nRow(), GDT_Int32, 0, 0);
	poBand->SetNoDataValue(result->noDataValue());
	CPLFree(pafScanline);
	GDALClose(poDataset);
	poDataset = NULL;
	GDALDestroyDriverManager();
	return true;
}

// 创建文件操作
bool CRasterReadWrite::CreateFile(CRasterData* result, const char * resultFileName, const char *FileFormat)
{
	const char *pszFormat = (char*)"GTiff";//GTiff
	GDALDriver *poDriver;
	char **papszMetadata;
	GDALAllRegister();
	poDriver = GetGDALDriverManager()->GetDriverByName(pszFormat);

	if (poDriver == NULL)
	{
		GDALDestroyDriverManager();
		return false;
	}

	papszMetadata = poDriver->GetMetadata();

	char **papszOptions = NULL;
	GDALDataset *poDstDS = poDriver->Create(resultFileName, result->nCol(), result->nRow(), 1, GDT_Int32, papszOptions);

	double adfGeoTransform[6] = { result->xTopLeft(), result->xCellSize(), 0, result->yTopLeft(), 0, result->yCellSize() };
	poDstDS->SetGeoTransform(adfGeoTransform);

	GDALClose(poDstDS);
	poDstDS = NULL;
	GDALDestroyDriverManager();
	return true;
}
