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

	//�������������Ϊ ReplaceValue
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

	//��ȡһ���뵱ǰ����һ���ߴ������ϵͳ�Ŀհ�DEM����
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

	// ��ȡ���Ͻ�X����
	double xTopLeft() const { return _xTopLeft; }

	// ��ȡ���Ͻ�Y����
	double yTopLeft() const { return _yTopLeft; }

	double xRightBottom() const { return _xRightBottom; }

	double yRightBottom() const { return _yRightBottom; }

	// ��ȡX���������С
	double xCellSize() const { return _xCellSize; }

	//��ȡY���������С
	double yCellSize() const { return _yCellSize; }
	// ��ȡ��Чֵ����
	float noDataValue() const { return _noDataValue; }

	// ��ȡ����ָ��
	int* dataPtr() const { return _dataPtr; }

	// ��ȡ����
	int nRow() const { return _nRow; }

	// ��ȡ����
	int nCol() const { return _nCol; }

	// �����±��ȡ�߳�ֵ
	int getValue(int row, int col)
	{
		if (isValidIndex(row, col))
		{
			return _dataPtr[col + nCol() * row];
		}
		return noDataValue();
	}


	// �����±����ø߳�ֵ
	void setValue(int row, int col, int value)
	{
		if (isValidIndex(row, col))
		{
			_dataPtr[col + nCol() * row] = value;
		}
	}

	//�ж��Ƿ���NodataValue
	bool isNodataValue(int row, int col)
	{
		if (fabs(this->getValue(row, col) - noDataValue()) > 0.0001)
			return false;
		else
			return true;
	}

	//�ж��±��Ƿ�Ϊ��Чֵ
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

	//�趨���Ͻ�X����
	void setXTopLeft(double X_TopLeft)
	{
		_xTopLeft = X_TopLeft;
	}

	// �趨���Ͻ�Y����
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

	// �趨X����դ���С
	void setXCellSize(double cell_size)
	{
		_xCellSize = cell_size;
	}

	// �趨Y����դ���С
	void setYCellSize(double cell_size)
	{
		_yCellSize = cell_size;
	}

	// �趨��Чֵ����
	void setNoDataValue(float no_data)
	{
		_noDataValue = no_data;
	}

	// �趨����
	void setNRow(int n_row)
	{
		_nRow = n_row;
	}

	//�趨����
	void setNCol(int n_col)
	{
		_nCol = n_col;
	}

private:
	double _xTopLeft;    //��ʼ��X����
	double _yTopLeft;    //��ʼ��Y����
	double _xRightBottom;
	double _yRightBottom;
	double _xCellSize;    //x���������С
	double _yCellSize;   //Y���������С
	int _nRow;           //դ����������
	int _nCol;           //դ����������
	int _noDataValue;  //դ��������ֵ������
	int* _dataPtr;	   //դ���������ݾ���
};

