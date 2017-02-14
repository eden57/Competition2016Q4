#include "CVectData.h"
#include <algorithm>
#include <sstream>
#include "RasterReadWrite.h"
#include <fstream>

#define INTERSECT 1
#define UNION 2
#define DIFFER 3

typedef struct Parameters
{
	string vectFile1;
	string vectFile2;
	string resultFile;
	int calcType;
	int resolution;
};

void WriteLog(string s_logfile_directory, string s_loginfo)
{
	time_t Date_Time;
	char achBufferTime[20];
	time(&Date_Time);
	strftime(achBufferTime, 20, "%Y-%m-%d %H:%M:%S", localtime(&Date_Time));
	//FILE* fid=fopen("/opt/HaiBao/Info.log","a+");
	string sLogFilePath = s_logfile_directory + "myInfo.Log";
	FILE* File = fopen(sLogFilePath.data(), "a+");//a+:没找到myInfo.log文件，则创建。myInfo.log是与可执行文件(Debug,ARM,Relese)在同一目录
	if (File == NULL)
	{
		printf("the myInfo.log is not exist!!!\n");
		return;
	}

	fseek(File, 0L, SEEK_END);	//定位到文件末尾
	if (ftell(File) == 0)
	{
		fprintf(File, "Date Time Algorithm DataName TotalTime FillTime OptiPoints TotalPoints\n");
	}

	fprintf(File, "%s ", achBufferTime);
	fprintf(File, "%s", s_loginfo.data());
	fprintf(File, "\n");
	fclose(File);
}
string m_GetDirectory(const char* ach_fullname)
{
	if (ach_fullname[0] == '\0')
	{
		return string("");
	}
	string str(ach_fullname);
	int nPos = str.find_last_of('/');
	if (nPos == -1)
	{
		nPos = str.find_last_of('\\');
	}
	str = str.substr(0, nPos + 1);
	return str;
}
string m_DoubleToStr(double d_number)
{
	string sOutString;
	stringstream StringStream;
	StringStream << d_number;
	StringStream >> sOutString;
	return sOutString;
}

bool init(int argc, char**argv, Parameters &params){
	if (argc<11){
		cout << "init error: please check the parameters" << endl;
		return false;
	}

	string calcType;

	for (int i = 1; i < argc; ++i){
		if (string(argv[i]) == "-c"){
			++i;
			if (i < argc){
				istringstream(argv[i]) >> calcType;
				transform(calcType.begin(), calcType.end(), calcType.begin(), tolower);
				if (calcType=="intersect"){
					params.calcType = INTERSECT;
				}
				else if (calcType == "union"){
					params.calcType = UNION;
				}
				else if (calcType == "differ"){
					params.calcType = DIFFER;
				}
				else{
					cout << "unkonwn operation. please check the parameter of '-c'." << endl;
					return false;
				}
			}
			else{
				cout << "missing parameters, please check '-c'." << endl;
				return false;
			}
		}

		else if (string(argv[i]) == "-v1"){
			++i;
			if (i < argc){
				istringstream(argv[i]) >> params.vectFile1;
			}
			else{
				cout << "missing parameters, please check '-v1'." << endl;
				return false;
			}
		}
		else if (string(argv[i]) == "-v2"){
			++i;
			if (i < argc){
				istringstream(argv[i]) >> params.vectFile2;
			}
			else{
				cout << "missing parameters, please check '-v2'." << endl;
				return false;
			}
		}
		else if (string(argv[i]) == "-o"){
			++i;
			if (i < argc){
				istringstream(argv[i]) >> params.resultFile;
			}
			else{
				cout << "missing parameters, please check '-o'." << endl;
				return false;
			}
		}
		else if (string(argv[i]) == "-r"){
			++i;
			if (i < argc){
				istringstream(argv[i]) >> params.resolution;
			}
			else{
				cout << "missing parameters, please check '-r'." << endl;
				return false;
			}
		}
		else{
			cout << "Uknown arg:" << argv[i] << endl;
			return false;
		}
	}
	return true;
}


int main(int argc,char *argv[])
{
	cout << "Starting ······" << endl;
	string cfg = argv[1];
	ifstream infile;
	infile.open(cfg.data());
	//assert(infile.is_open());


	string shpFile1 = "";
	string shpFile2 = "";
	string resultVect = "";
	double resolution = 10;
	string res = "";
	if(getline(infile, shpFile1))
	{
		cout << "shpFile1:" <<shpFile1<< endl;
	}
	if(getline(infile,shpFile2))
	{
		cout << "shpFile2:" <<shpFile2<< endl;
	}
	if(getline(infile,resultVect))
	{
		cout << "resultVect:" <<resultVect<< endl;
	}
	if(getline(infile,res))
	{
		resolution = atof(res.c_str());
		cout << "resolution:" <<resolution<< endl;
	}
	if(shpFile1 == "" || shpFile2 == "" || resultVect == "" || resolution <0)
	{
		cout << "err:input" << endl;
		return 0;
	}
	infile.close();
	Vector2Rater v2r;

	

	v2r.m_raster1 = v2r.Run_Vect2Raster( shpFile1, resolution);
	v2r.m_raster2 = v2r.Run_Vect2Raster( shpFile2, resolution);

	if (v2r.m_raster1==NULL){
		cout << "err: bad vector reading" << endl;
		return 0;
	}
	if (v2r.m_raster2==NULL){
		cout << "err: bad vector reading" << endl;
		return 0;
	}


	v2r.Intersect(resolution);
	v2r.Run_Raster2Vector(resultVect);

	cout << "Finished" << endl;
	system("pause");
	return 1;
}