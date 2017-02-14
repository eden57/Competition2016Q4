#include "CVectData.h"
#include "RasterData.h"
#include "RasterReadWrite.h"
Vector2Rater::~Vector2Rater()
{
	if (m_raster1 != NULL){
		delete m_raster1;
		m_raster1 = NULL;
	}
	if (m_raster2 != NULL){
		delete m_raster2;
		m_raster2 = NULL;
	}


}

CRasterData* Vector2Rater::Run_Vect2Raster(string shpFile, double resolution)
{

	GDALAllRegister();
	OGRRegisterAll();


	//��ʸ��ͼ��  
	OGRDataSource *pOgrSrc = NULL;
	pOgrSrc = OGRSFDriverRegistrar::Open(shpFile.c_str(), false);

	if (pOgrSrc == NULL)
	{
		return false;
	}
	OGRLayer *pOgrLyr;
	pOgrLyr = pOgrSrc->GetLayer(0);

	//  
	OGREnvelope env;
	pOgrLyr->GetExtent(&env);

	int m_nImageWidth = (int)((env.MaxX-env.MinX)/resolution);
	int m_nImageHeight = (int)((env.MaxY-env.MinY)/resolution);

	OGRSpatialReference *pOgrSRS = NULL;
	pOgrSRS = pOgrLyr->GetSpatialRef();

	char *pPrj = NULL;
	if (pOgrSRS == NULL)
	{
		cout << "��ͶӰ��Ϣ...\n";
	}
	else
	{
		pOgrSRS->exportToWkt(&pPrj);
	}




	GDALDriver *poDriver = NULL;
	poDriver = GetGDALDriverManager()->GetDriverByName("MEM");
	if (poDriver == NULL){
		return NULL;
	}
	GDALDataset *poNewDS = poDriver->Create("",
		m_nImageWidth,
		m_nImageHeight,
		1,
		GDT_Int32,
		NULL);


	if (poNewDS == NULL){
		return NULL;
	}


	double adfGeoTransform[6];
	adfGeoTransform[0] = env.MinX;
	adfGeoTransform[1] = (env.MaxX - env.MinX) / m_nImageWidth;
	adfGeoTransform[2] = 0;
	adfGeoTransform[3] = env.MinY;
	adfGeoTransform[4] = 0;
	adfGeoTransform[5] = (env.MaxY-env.MinY) / m_nImageHeight;

	GDALSetGeoTransform(poNewDS, adfGeoTransform);

	if (pOgrSRS != NULL)
	{
		poNewDS->SetProjection(pPrj);
	}

	int * pnbandlist = NULL;
	pnbandlist = new int[1];
	pnbandlist[0] = 1;
	double *dburnValues = NULL;
	dburnValues = new double[3];
	dburnValues[0] = 255;
	dburnValues[1] = 111;
	dburnValues[2] = 34;

	OGRLayerH * player;
	player = new OGRLayerH[1];
	player[0] = (OGRLayerH)pOgrLyr;

	char **papszOptions = NULL;
	papszOptions = CSLSetNameValue(papszOptions, "CHUNKSIZE", "1");
	papszOptions = CSLSetNameValue(papszOptions, "ATTRIBUTE", "OBJECTID_1");

	void * pTransformArg = NULL;
	void * m_hGenTransformArg = NULL;
	m_hGenTransformArg = GDALCreateGenImgProjTransformer(NULL,
		pPrj,
		(GDALDatasetH)poNewDS,
		poNewDS->GetProjectionRef(),
		false, 1000.0, 3);

	pTransformArg = GDALCreateApproxTransformer(GDALGenImgProjTransform,
		m_hGenTransformArg,
		0.125);

	CPLErr err = GDALRasterizeLayers((GDALDatasetH)poNewDS,
		1,
		pnbandlist,
		1, player,
		GDALGenImgProjTransform,
		m_hGenTransformArg,
		dburnValues,
		papszOptions,
		GDALTermProgress,
		"vector2raster");

	GDALDestroyGenImgProjTransformer(m_hGenTransformArg);
	GDALDestroyApproxTransformer(pTransformArg);

	OGRDataSource::DestroyDataSource(pOgrSrc);
	delete[]player;
	delete[]pnbandlist;
	delete[]dburnValues;


	///////////////////////////////////////////////////////////
	int nCol = poNewDS->GetRasterXSize();    //��ȡ����
	int nRow = poNewDS->GetRasterYSize();    //��ȡ����

	double xTopLeft, yTopLeft;
	double xRightBottom, yRightBottom;
	double cellSizeX;
	double cellSizeY;
	xRightBottom = env.MaxX;
	yRightBottom = env.MaxY;

	if (poNewDS->GetGeoTransform(adfGeoTransform) == CE_None)
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
		GDALClose(poNewDS);
		GDALDestroyDriverManager();
		return NULL;
	}

	GDALRasterBand *poBand = poNewDS->GetRasterBand(1);	    //��ȡ��һ��������
	int* pafScanline = (int*)CPLMalloc(sizeof(float)*nCol*nRow);
	poBand->RasterIO(GF_Read, 0, 0, nCol, nRow, pafScanline, nCol, nRow, GDT_Int32, 0, 0);


	int noDataVaue = (float)poBand->GetNoDataValue();
	CRasterData *raster = new CRasterData(xTopLeft, yTopLeft,xRightBottom, yRightBottom, cellSizeX, cellSizeY, nRow, nCol, noDataVaue);   //�ṹ���ʼ��

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
		GDALClose(poNewDS);
	}

	catch (exception* e)
	{
		cout << e->what() << endl;
	}

	GDALDestroyDriverManager();


	return raster;

}

// դ��ʸ����
/**
@param rasterfile �����դ���ļ�����������ļ��Ƕನ���ļ�����ֻ�����һ����
@param shpfile �������ļ���һ��ʸ���ļ���
@param pszFormat ����ļ���ʽ
@return �ɹ�����1�����򷵻�0
*/
bool Vector2Rater::Run_Raster2Vector(string vectResult)
{
	GDALAllRegister();     //ע��������������
	// ��դ���ļ�
	//GDALDataset* poSrcDS = (GDALDataset*)GDALOpen(rasterFrame.c_str (), GA_ReadOnly);


	GDALDriver *poDriverGDAL = NULL;
	poDriverGDAL = GetGDALDriverManager()->GetDriverByName("MEM");

	GDALDataset *poSrcDS = poDriverGDAL->Create("",
		m_raster1->nCol(),
		m_raster1->nRow(),
		1,
		GDT_Int32,
		NULL);

	double adfGeoTransform[6];
	adfGeoTransform[0] = m_raster1->xTopLeft();
	adfGeoTransform[1] = m_raster1->xCellSize();
	adfGeoTransform[2] = 0; /* rotation, 0 if image is "north up" */
	adfGeoTransform[4] = 0; /* rotation, 0 if image is "north up" */
	adfGeoTransform[3] = m_raster1->yTopLeft();
	adfGeoTransform[5] = m_raster1->yCellSize();

	poSrcDS->SetGeoTransform(adfGeoTransform);


	if (poSrcDS == NULL)
	{
		GDALClose((GDALDatasetH)poSrcDS);
		return false;
	}
	// �������ʸ���ļ�
	OGRSFDriver *poDriver;
	poDriver = OGRSFDriverRegistrar::GetRegistrar()->GetDriverByName("ESRI shapefile");
	if (poDriver == NULL)
	{
		GDALClose((GDALDatasetH)poSrcDS);
		return false;
	}
	//�����ļ����������ʸ���ļ�



	OGRDataSource* poDstDS = poDriver->Open(vectResult.c_str(), GA_Update);

	if (poDstDS == NULL){
		poDstDS = poDriver->CreateDataSource(vectResult.c_str());
	}
	else{
		poDstDS->DeleteLayer(0);
	}

	if (poDstDS == NULL)
	{
		GDALClose((GDALDatasetH)poSrcDS);
		return false;
	}
	// ����ռ�ο���������ͼ����ͬ
	OGRSpatialReference *poSpatialRef = new OGRSpatialReference(poSrcDS->GetProjectionRef());
	OGRLayer* poLayer = poDstDS->CreateLayer("Result", poSpatialRef, wkbPolygon, NULL);
	if (poDstDS == NULL)
	{
		GDALClose((GDALDatasetH)poSrcDS);
		OGRDataSource::DestroyDataSource(poDstDS);
		delete poSpatialRef;
		poSpatialRef = NULL;
		return false;
	}

	OGRFieldDefn ofieldDef("ID", OFTInteger); //�������Ա�ֻ��һ���ֶμ���Segment�������汣���Ӧ��դ�����Ԫֵ
	poLayer->CreateField(&ofieldDef);
	//GDALRasterBandH hSrcBand = (GDALRasterBandH)poSrcDS->GetRasterBand(1); //��ȡͼ��ĵ�һ������

	GDALRasterBand* hSrcBand = poSrcDS->GetRasterBand(1); //��ȡͼ��ĵ�һ������
	hSrcBand->SetNoDataValue(m_raster1->noDataValue());
	hSrcBand->RasterIO(GF_Write, 0, 0, m_raster1->nCol(), m_raster1->nRow(), m_raster1->dataPtr(), m_raster1->nCol(), m_raster1->nRow(), GDT_Int32, 0, 0);


	GDALPolygonize(hSrcBand, hSrcBand, (OGRLayerH)poLayer, 0, NULL, NULL, NULL); //����դ��ʸ����

	GDALClose((GDALDatasetH)poSrcDS); //�ر��ļ� 
	OGRDataSource::DestroyDataSource(poDstDS);
	GDALDestroyDriverManager();

	return true;
}

bool Vector2Rater::Intersect(double resolution)
{
	if (m_raster1 == NULL || m_raster2 == NULL){
		return false;
	}

	double xMin1 = m_raster1->xTopLeft();
	double yMin1 = m_raster1->yTopLeft();
	double xMin2 = m_raster2->xTopLeft();
	double yMin2 = m_raster2->yTopLeft();
	double xMax1 = m_raster1->xRightBottom();
	double yMax1 = m_raster1->yRightBottom();
	double xMax2 = m_raster2->xRightBottom();
	double yMax2 = m_raster2->yRightBottom();


	// TODO: ��ֻ����ͼ����Ӿ����໥���������
	double xStart,xEnd,yStart,yEnd;
	xStart = xMin1>xMin2?xMin1:xMin2;
	yStart = yMin1>yMin2?yMin1:yMin2;
	xEnd = xMax1<xMax2?xMax1:xMax2;
	yEnd = yMax1<yMax2?yMax1:yMax2;


	int startRow1,startRow2;
	int startCol1,startCol2;
	startRow1 = (m_raster1->yTopLeft()-yStart)/resolution;
	startCol1 = (m_raster1->xTopLeft()-xStart)/resolution;
	startRow2 = (m_raster2->yTopLeft()-yStart)/resolution;
	startCol2 = (m_raster2->xTopLeft()-xStart)/resolution;

	xStart = m_raster1->xTopLeft();
	yStart = m_raster1->yTopLeft();
	startRow1 = 0;
	startCol1 = 0;
	startRow2 = abs((m_raster2->yTopLeft()-yStart))/resolution;
	startCol2 = abs((m_raster2->xTopLeft()-xStart))/resolution;

	int nRows,nCols;
	nRows = (yEnd-yStart)/resolution;
	nCols = (xEnd-xStart)/resolution;

	

	for (int r = 0; r < nRows; r++){
		for (int c = 0; c < nCols; c++){
			if (m_raster1->isNodataValue(r+startRow1, c+startCol1) == false && m_raster2->isNodataValue(r+startRow2, c+startCol2) == false){
				m_raster1->setValue(r+startRow1, c+startCol1, 1);
			}
			else{
				m_raster1->setValue(r+startRow1, c+startCol1, m_raster1->noDataValue());
			}
		}
	}
	return true;
}



