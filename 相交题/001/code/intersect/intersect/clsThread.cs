using System; 
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Data; 
using System.Diagnostics;

namespace Update
{
	/// <summary>
	/// clsThread 的摘要说明。
	/// </summary>
	public class clsThread
	{
	
		public Form_update fm;

        public int sum = 0;
        public  int no = 0;
		//private  DataTable m_CopyDocDataTable=new DataTable();

		public clsThread()
		{  
			
		}
		public clsThread(Form_update f)
		{  
			fm=f;			
		}

		public void Do()
		{
            UpdateIns();
		}


        //public void CountToSum()
        //{
        //    fm.label_text.Text = "正在连接更新服务器....";

        //    //1)得到HouseAndLandRegMIS.exe所在的本地路径和Update.exe所在的本地路径成功
        //    if (this.m_ClientHouseAndLandRegMISPath != string.Empty && this.m_UpdatePath != string.Empty)
        //    {
        //        if (this.ReadProjConfigUpdate())
        //        {

        //            fm.label_text.Text = "正在检查更新状态....";
        //            int k;
        //            k = this.CompareUpdateText();

        //            if (k == 1)//1:时间不同 需系统更新
        //            {
        //                fm.label_text.Text = "正在获取更新列表个数....";

        //                this.m_CopyDocDataTable.Clear();

        //                if (GetServerCopyFilesInfo(this.m_ServerHouseAndLandRegMISPath, @"\"))
        //                {
        //                    int l_iFilesCount = this.m_CopyDocDataTable.Rows.Count;

        //                    if (!CopyServerCopyFiles())
        //                    {
        //                        MessageBox.Show("更新文件失败!", "错误", 0x00);
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("获取更新列表失败!", "错误", 0x00);
        //                }
        //            }
        //            else if (k == 0)  //0:时间同
        //            {
        //                fm.label_text.Text = "系统为最新状态....";
        //            }
        //            else if (k == -1)//-1:错误
        //            {
        //                MessageBox.Show("获取更新状态失败!", "错误", 0x00);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("获取更新服务器路径失败!", "错误", 0x00);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("服务器路径为空!", "错误", 0x00);
        //    }


        //    fm.label_text.Text = "更新操作完成....";
        //    fm.label_text.Text = "系统正在启动....";

        //    //6)执行HouseAndLandRegMIS.exe。
        //    ExecHouseAndLandRegMIS();

        //    //7)关闭当前可执行文件（update.exe）
        //    Application.Exit();

        //}
        private bool CopyServerCopyFiles()
        {
            string l_strServerFile = null;
            string l_strClientFile = null;

            int l_itemp = 1;
            int l_icount = this.m_CopyDocDataTable.Rows.Count;

            try
            {
                //遍历copyout表，找到选中文件在copyout表中的对应行
                foreach (DataRow l_tempRow in this.m_CopyDocDataTable.Rows)
                {
                    fm.label_text.Text = "正在更新" + l_itemp.ToString() + "/" + l_icount.ToString() + "....";


                    if (l_tempRow["FileName"] != DBNull.Value && l_tempRow["FilePathpart"] != DBNull.Value && l_tempRow["FileOppositePathpart"] != DBNull.Value)
                    {
                        l_strServerFile = l_tempRow["FilePathpart"].ToString();
                        l_strClientFile = this.m_ClientHouseAndLandRegMISPath + l_tempRow["FileOppositePathpart"].ToString() + l_tempRow["FileName"].ToString();



                        //File.Delete(l_strClientFile);
                        if (File.Exists(l_strClientFile))
                            File.SetAttributes(l_strClientFile, System.IO.FileAttributes.Normal);


                        System.IO.FileInfo file = new System.IO.FileInfo(l_strClientFile);
                        //string dir = file.Directory;
                        string dirpath = file.DirectoryName;

                        if (Directory.Exists(dirpath) == false)
                        {

                            Directory.CreateDirectory(dirpath);
                        }

                        File.Copy(l_strServerFile, l_strClientFile, true);
                    }

                    l_itemp++;
                }
            }
            catch
            {
                MessageBox.Show("更新文件" + l_strClientFile + "错误!", "错误", 0x00);
                return (false);
            }

            return (true);
        }

        public void UpdateIns()
        {
            fm.label_text.Text = "开始分析....";

            //1) 得到数据数量
            if (this.sum > 0)
            {

                if (!CopyServerCopyFiles())
                {
                    MessageBox.Show("更新文件失败!", "错误", 0x00);
                }


            }
            else
            {
                MessageBox.Show("服务器路径为空!", "错误", 0x00);
            }


            fm.label_text.Text = "更新操作完成....";
            fm.label_text.Text = "系统正在启动....";


            //7)关闭当前可执行文件（update.exe）
            //Application.Exit(); 

        }




	
		private bool CopyServerCopyFiles()
		{
			string l_strServerFile=null;
			string l_strClientFile=null;

			int l_itemp = 1;
			int l_icount = this.m_CopyDocDataTable.Rows.Count;

			try
			{
				//遍历copyout表，找到选中文件在copyout表中的对应行
				foreach(DataRow l_tempRow in this.m_CopyDocDataTable.Rows) 
				{
					fm.label_text.Text="正在更新"+l_itemp.ToString()+"/"+l_icount.ToString()+"....";
					
					
					if (l_tempRow["FileName"]!= DBNull.Value && l_tempRow["FilePathpart"]!= DBNull.Value && l_tempRow["FileOppositePathpart"]!= DBNull.Value)
					{						
						l_strServerFile = l_tempRow["FilePathpart"].ToString();
						l_strClientFile = this.m_ClientHouseAndLandRegMISPath+l_tempRow["FileOppositePathpart"].ToString()+l_tempRow["FileName"].ToString(); 



						//File.Delete(l_strClientFile);
						if (File.Exists(l_strClientFile))
							File.SetAttributes(l_strClientFile,System.IO.FileAttributes.Normal);


						System.IO.FileInfo file = new System.IO.FileInfo(l_strClientFile); 
						//string dir = file.Directory;
						string dirpath = file.DirectoryName;

						if(Directory.Exists(dirpath)==false)
						{

							Directory.CreateDirectory(dirpath);
						}

						File.Copy(l_strServerFile,l_strClientFile,true);
					}

					l_itemp++;
				}
			}
			catch
			{
				MessageBox.Show("更新文件"+l_strClientFile +"错误!","错误",0x00);
				return(false);
			}

			return(true);				
		}


	}
}
