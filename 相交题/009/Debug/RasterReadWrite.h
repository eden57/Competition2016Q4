#pragma once

#include "RasterData.h"

//class __declspec(dllexport) CRasterReadWrite
class CRasterReadWrite
{
public:	
  CRasterReadWrite(void);
  ~CRasterReadWrite(void);

  // ��ȡդ������
   CRasterData* Read(const char* fileName);

  // CRasterData* OpenDataset(GDALDataset * Dataset);

  bool Write(CRasterData* result, const char *resultFileName);
  
  bool CreateFile(CRasterData* result, const char * resultFileName, const char *FileFormat);    // �����ļ�����
  // дդ������
  void WriteToTxt(char* fileName, CRasterData* source);
};