using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace Update
{
	/// <summary>
	/// Form_update 的摘要说明。
	/// </summary>
	public class Form_update : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Label label_text;
		
		public string m_ClientHouseAndLandRegMISPath=null;
		public string m_UpdatePath=null;
        //public string m_ServerHouseAndLandRegMISPath=null;

		
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form_update()
		{ 
			InitializeComponent(); 
		}


		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.label_text = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label_text
			// 
			this.label_text.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label_text.Location = new System.Drawing.Point(8, 12);
			this.label_text.Name = "label_text";
			this.label_text.Size = new System.Drawing.Size(312, 15);
			this.label_text.TabIndex = 0;
			this.label_text.Text = "正在连接更新服务器....";
			// 
			// Form_update
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(328, 40);
			this.Controls.Add(this.label_text);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form_update";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form_update";
			this.ResumeLayout(false);

		}
		#endregion

		 


		public void open()
		{ 

			clsThread oAlpha = new clsThread(this); 
			
            oAlpha.sum = 0;
            oAlpha.no = 0;

　　　　　　//Thread oThread = new Thread(new ThreadStart(oAlpha.Do));

			Thread oThread = new Thread(new ThreadStart(oAlpha.Do));
　　　　　　oThread.Start(); 
			 
			//oThread.Abort();
　　　　　　//oThread.Join();  
		} 
		 

		 
		

		
	}
}
