#pragma once

#include "RasterData.h"

//class __declspec(dllexport) CRasterReadWrite
class CRasterReadWrite
{
public:	
  CRasterReadWrite(void);
  ~CRasterReadWrite(void);

  // 读取栅格数据
   CRasterData* Read(const char* fileName);

  // CRasterData* OpenDataset(GDALDataset * Dataset);

  bool Write(CRasterData* result, const char *resultFileName);
  
  bool CreateFile(CRasterData* result, const char * resultFileName, const char *FileFormat);    // 创建文件操作
  // 写栅格数据
  void WriteToTxt(char* fileName, CRasterData* source);
};