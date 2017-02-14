#include <ogrsf_frmts.h>
#include <gdal_priv.h>
#include <gdal_alg.h>
#include <iostream>
#include <string>
//#pragma comment(lib,"gdal_i.lib")
#include "RasterData.h"
using namespace std;

class Vector2Rater
{
public:
	~Vector2Rater();
	CRasterData* Run_Vect2Raster(string shpfile, double resolution);
	bool Run_Raster2Vector(string vectResult);
	bool Intersect(double resolution);


	CRasterData *m_raster1;
	CRasterData *m_raster2;

protected:
private:


};

