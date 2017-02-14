using System; 
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Data; 
using System.Diagnostics;

namespace Update
{
	/// <summary>
	/// clsThread ��ժҪ˵����
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
        //    fm.label_text.Text = "�������Ӹ��·�����....";

        //    //1)�õ�HouseAndLandRegMIS.exe���ڵı���·����Update.exe���ڵı���·���ɹ�
        //    if (this.m_ClientHouseAndLandRegMISPath != string.Empty && this.m_UpdatePath != string.Empty)
        //    {
        //        if (this.ReadProjConfigUpdate())
        //        {

        //            fm.label_text.Text = "���ڼ�����״̬....";
        //            int k;
        //            k = this.CompareUpdateText();

        //            if (k == 1)//1:ʱ�䲻ͬ ��ϵͳ����
        //            {
        //                fm.label_text.Text = "���ڻ�ȡ�����б����....";

        //                this.m_CopyDocDataTable.Clear();

        //                if (GetServerCopyFilesInfo(this.m_ServerHouseAndLandRegMISPath, @"\"))
        //                {
        //                    int l_iFilesCount = this.m_CopyDocDataTable.Rows.Count;

        //                    if (!CopyServerCopyFiles())
        //                    {
        //                        MessageBox.Show("�����ļ�ʧ��!", "����", 0x00);
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("��ȡ�����б�ʧ��!", "����", 0x00);
        //                }
        //            }
        //            else if (k == 0)  //0:ʱ��ͬ
        //            {
        //                fm.label_text.Text = "ϵͳΪ����״̬....";
        //            }
        //            else if (k == -1)//-1:����
        //            {
        //                MessageBox.Show("��ȡ����״̬ʧ��!", "����", 0x00);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("��ȡ���·�����·��ʧ��!", "����", 0x00);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("������·��Ϊ��!", "����", 0x00);
        //    }


        //    fm.label_text.Text = "���²������....";
        //    fm.label_text.Text = "ϵͳ��������....";

        //    //6)ִ��HouseAndLandRegMIS.exe��
        //    ExecHouseAndLandRegMIS();

        //    //7)�رյ�ǰ��ִ���ļ���update.exe��
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
                //����copyout���ҵ�ѡ���ļ���copyout���еĶ�Ӧ��
                foreach (DataRow l_tempRow in this.m_CopyDocDataTable.Rows)
                {
                    fm.label_text.Text = "���ڸ���" + l_itemp.ToString() + "/" + l_icount.ToString() + "....";


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
                MessageBox.Show("�����ļ�" + l_strClientFile + "����!", "����", 0x00);
                return (false);
            }

            return (true);
        }

        public void UpdateIns()
        {
            fm.label_text.Text = "��ʼ����....";

            //1) �õ���������
            if (this.sum > 0)
            {

                if (!CopyServerCopyFiles())
                {
                    MessageBox.Show("�����ļ�ʧ��!", "����", 0x00);
                }


            }
            else
            {
                MessageBox.Show("������·��Ϊ��!", "����", 0x00);
            }


            fm.label_text.Text = "���²������....";
            fm.label_text.Text = "ϵͳ��������....";


            //7)�رյ�ǰ��ִ���ļ���update.exe��
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
				//����copyout���ҵ�ѡ���ļ���copyout���еĶ�Ӧ��
				foreach(DataRow l_tempRow in this.m_CopyDocDataTable.Rows) 
				{
					fm.label_text.Text="���ڸ���"+l_itemp.ToString()+"/"+l_icount.ToString()+"....";
					
					
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
				MessageBox.Show("�����ļ�"+l_strClientFile +"����!","����",0x00);
				return(false);
			}

			return(true);				
		}


	}
}
