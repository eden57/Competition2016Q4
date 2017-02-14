#pragma once
#include <cmath>
#include <iostream>

struct CRasterData
{
public:
	CRasterData(double xTopLeft, double yTopLeft,double xRightBottom, double yRightBottom, double xCellSize, double yCellSize, int nRow, int nCol, int noDataValue)
	{
		this->_xTopLeft = xTopLeft;
		this->_yTopLeft = yTopLeft;
		this->_xRightBottom = xRightBottom;
		this->_yRightBottom = yRightBottom;
		this->_xCellSize = xCellSize;
		this->_yCellSize = yCellSize;
		this->_nRow = nRow;
		this->_nCol = nCol;
		this->_noDataValue = noDataValue;
		this->_dataPtr = new int[nRow * nCol];
	}

	//将所有数据清除为 ReplaceValue
	void cleanData(float ReplaceValue)
	{
		for (int r = 0; r < _nRow; r++)
		{
			for (int c = 0; c < _nCol; c++)
			{
				if (this->isNodataValue (r,c)==false)
				{
					this->setValue(r, c, ReplaceValue);
				}
			}
		}
	}

	//获取一个与当前数据一样尺寸和坐标系统的空白DEM数据
	CRasterData* frameCopy()
	{
		CRasterData* CopyVersion = new CRasterData(this->_xTopLeft, this->_yTopLeft,this->xRightBottom(),this->yRightBottom(), this->_xCellSize, this->_yCellSize, this->_nRow, this->_nCol, this->_noDataValue);
		for (int r = 0; r < _nRow; r++)
		{
			for (int c = 0; c < _nCol; c++)
			{
				CopyVersion->setValue(r, c, _noDataValue);
			}
		}
		return CopyVersion;
	}

	~CRasterData()
	{
		delete this->_dataPtr;
	}

	// 获取左上角X坐标
	double xTopLeft() const { return _xTopLeft; }

	// 获取左上角Y坐标
	double yTopLeft() const { return _yTopLeft; }

	double xRightBottom() const { return _xRightBottom; }

	double yRightBottom() const { return _yRightBottom; }

	// 获取X方向格网大小
	double xCellSize() const { return _xCellSize; }

	//获取Y方向格网大小
	double yCellSize() const { return _yCellSize; }
	// 获取无效值类型
	float noDataValue() const { return _noDataValue; }

	// 获取数据指针
	int* dataPtr() const { return _dataPtr; }

	// 获取行数
	int nRow() const { return _nRow; }

	// 获取列数
	int nCol() const { return _nCol; }

	// 根据下标获取高程值
	int getValue(int row, int col)
	{
		if (isValidIndex(row, col))
		{
			return _dataPtr[col + nCol() * row];
		}
		return noDataValue();
	}


	// 根据下标设置高程值
	void setValue(int row, int col, int value)
	{
		if (isValidIndex(row, col))
		{
			_dataPtr[col + nCol() * row] = value;
		}
	}

	//判断是否是NodataValue
	bool isNodataValue(int row, int col)
	{
		if (fabs(this->getValue(row, col) - noDataValue()) > 0.0001)
			return false;
		else
			return true;
	}

	//判断下标是否为有效值
	bool isValidIndex(int row, int col)
	{
		if (row < 0 || row >= nRow() || col < 0 || col >= nCol())
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	//设定左上角X坐标
	void setXTopLeft(double X_TopLeft)
	{
		_xTopLeft = X_TopLeft;
	}

	// 设定左上角Y坐标
	void setYTopLeft(double Y_topLeft)
	{
		_yTopLeft = Y_topLeft;
	}

	void setXRightBottom(double X_RightBottom)
	{
		_xRightBottom = X_RightBottom;
	}

	void setYRightBottom(double Y_RightBottom)
	{
		_yRightBottom = Y_RightBottom;
	}

	// 设定X方向栅格大小
	void setXCellSize(double cell_size)
	{
		_xCellSize = cell_size;
	}

	// 设定Y方向栅格大小
	void setYCellSize(double cell_size)
	{
		_yCellSize = cell_size;
	}

	// 设定无效值类型
	void setNoDataValue(float no_data)
	{
		_noDataValue = no_data;
	}

	// 设定行数
	void setNRow(int n_row)
	{
		_nRow = n_row;
	}

	//设定列数
	void setNCol(int n_col)
	{
		_nCol = n_col;
	}

private:
	double _xTopLeft;    //起始点X坐标
	double _yTopLeft;    //起始点Y坐标
	double _xRightBottom;
	double _yRightBottom;
	double _xCellSize;    //x方向格网大小
	double _yCellSize;   //Y方向格网大小
	int _nRow;           //栅格数据行数
	int _nCol;           //栅格数据列数
	int _noDataValue;  //栅格数据无值区数据
	int* _dataPtr;	   //栅格数据数据矩阵
};

