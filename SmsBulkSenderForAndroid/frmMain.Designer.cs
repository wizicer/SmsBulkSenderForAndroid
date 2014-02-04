namespace SmsBulkSenderForAndroid
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstSms = new System.Windows.Forms.ListView();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.colheadPhonenumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colheadName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colheadContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSend = new System.Windows.Forms.Button();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstSms
            // 
            this.lstSms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colheadPhonenumber,
            this.colheadName,
            this.colheadContent,
            this.columnHeader1});
            this.lstSms.FullRowSelect = true;
            this.lstSms.Location = new System.Drawing.Point(12, 44);
            this.lstSms.Name = "lstSms";
            this.lstSms.Size = new System.Drawing.Size(642, 380);
            this.lstSms.TabIndex = 0;
            this.lstSms.UseCompatibleStateImageBehavior = false;
            this.lstSms.View = System.Windows.Forms.View.Details;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(579, 12);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Location = new System.Drawing.Point(12, 17);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(77, 12);
            this.lblConnectionStatus.TabIndex = 2;
            this.lblConnectionStatus.Text = "Disconnected";
            // 
            // colheadPhonenumber
            // 
            this.colheadPhonenumber.Text = "Phonenumber";
            this.colheadPhonenumber.Width = 88;
            // 
            // colheadName
            // 
            this.colheadName.Text = "Name";
            this.colheadName.Width = 69;
            // 
            // colheadContent
            // 
            this.colheadContent.Text = "Content";
            this.colheadContent.Width = 457;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(579, 431);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Status";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 466);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblConnectionStatus);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.lstSms);
            this.Name = "frmMain";
            this.Text = "Sms Bulk Sender";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstSms;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.ColumnHeader colheadPhonenumber;
        private System.Windows.Forms.ColumnHeader colheadName;
        private System.Windows.Forms.ColumnHeader colheadContent;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

