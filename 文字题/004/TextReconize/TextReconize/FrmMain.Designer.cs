namespace TextReconize
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox_Resource = new System.Windows.Forms.TextBox();
            this.rtf_Images = new System.Windows.Forms.RichTextBox();
            this.btn_Transform = new System.Windows.Forms.Button();
            this.btn_AddImage = new System.Windows.Forms.Button();
            this.textBox_SplitResult = new System.Windows.Forms.TextBox();
            this.lbc_resutl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_mWords = new System.Windows.Forms.TextBox();
            this.btn_ShowImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Resource
            // 
            this.textBox_Resource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_Resource.Location = new System.Drawing.Point(12, 29);
            this.textBox_Resource.Multiline = true;
            this.textBox_Resource.Name = "textBox_Resource";
            this.textBox_Resource.Size = new System.Drawing.Size(454, 503);
            this.textBox_Resource.TabIndex = 0;
            // 
            // rtf_Images
            // 
            this.rtf_Images.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtf_Images.Location = new System.Drawing.Point(534, 325);
            this.rtf_Images.Name = "rtf_Images";
            this.rtf_Images.Size = new System.Drawing.Size(477, 207);
            this.rtf_Images.TabIndex = 1;
            this.rtf_Images.Text = "";
            // 
            // btn_Transform
            // 
            this.btn_Transform.Location = new System.Drawing.Point(472, 87);
            this.btn_Transform.Name = "btn_Transform";
            this.btn_Transform.Size = new System.Drawing.Size(55, 23);
            this.btn_Transform.TabIndex = 2;
            this.btn_Transform.Text = "转换";
            this.btn_Transform.UseVisualStyleBackColor = true;
            this.btn_Transform.Click += new System.EventHandler(this.btn_Transform_Click);
            // 
            // btn_AddImage
            // 
            this.btn_AddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddImage.Location = new System.Drawing.Point(648, 538);
            this.btn_AddImage.Name = "btn_AddImage";
            this.btn_AddImage.Size = new System.Drawing.Size(72, 23);
            this.btn_AddImage.TabIndex = 2;
            this.btn_AddImage.Text = "添加图片";
            this.btn_AddImage.UseVisualStyleBackColor = true;
            this.btn_AddImage.Click += new System.EventHandler(this.btn_AddImage_Click);
            // 
            // textBox_SplitResult
            // 
            this.textBox_SplitResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SplitResult.Location = new System.Drawing.Point(534, 29);
            this.textBox_SplitResult.Multiline = true;
            this.textBox_SplitResult.Name = "textBox_SplitResult";
            this.textBox_SplitResult.Size = new System.Drawing.Size(477, 161);
            this.textBox_SplitResult.TabIndex = 3;
            // 
            // lbc_resutl
            // 
            this.lbc_resutl.AutoSize = true;
            this.lbc_resutl.Location = new System.Drawing.Point(534, 11);
            this.lbc_resutl.Name = "lbc_resutl";
            this.lbc_resutl.Size = new System.Drawing.Size(53, 12);
            this.lbc_resutl.TabIndex = 4;
            this.lbc_resutl.Text = "分词结果";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(532, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "显示结果";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "输入";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(41, 538);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "打开文件...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(534, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "词对";
            // 
            // textBox_mWords
            // 
            this.textBox_mWords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_mWords.Location = new System.Drawing.Point(534, 215);
            this.textBox_mWords.Multiline = true;
            this.textBox_mWords.Name = "textBox_mWords";
            this.textBox_mWords.Size = new System.Drawing.Size(477, 62);
            this.textBox_mWords.TabIndex = 5;
            // 
            // btn_ShowImage
            // 
            this.btn_ShowImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ShowImage.Location = new System.Drawing.Point(834, 538);
            this.btn_ShowImage.Name = "btn_ShowImage";
            this.btn_ShowImage.Size = new System.Drawing.Size(72, 23);
            this.btn_ShowImage.TabIndex = 2;
            this.btn_ShowImage.Text = "显示结果";
            this.btn_ShowImage.UseVisualStyleBackColor = true;
            this.btn_ShowImage.Click += new System.EventHandler(this.btn_ShowImage_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 573);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_mWords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbc_resutl);
            this.Controls.Add(this.textBox_SplitResult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_ShowImage);
            this.Controls.Add(this.btn_AddImage);
            this.Controls.Add(this.btn_Transform);
            this.Controls.Add(this.rtf_Images);
            this.Controls.Add(this.textBox_Resource);
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.Text = "语义";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Resource;
        private System.Windows.Forms.RichTextBox rtf_Images;
        private System.Windows.Forms.Button btn_Transform;
        private System.Windows.Forms.Button btn_AddImage;
        private System.Windows.Forms.TextBox textBox_SplitResult;
        private System.Windows.Forms.Label lbc_resutl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_mWords;
        private System.Windows.Forms.Button btn_ShowImage;
    }
}

