namespace POS_Shop.Views.LicenseManagement
{
    partial class LicenseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseForm));
            this.panelActivation = new System.Windows.Forms.Panel();
            this.linkLabelHelp = new System.Windows.Forms.LinkLabel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnActivate = new System.Windows.Forms.Button();
            this.txtLicenseKey = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblLicenseKey = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblActivationTitle = new System.Windows.Forms.Label();
            this.panelLicenseInfo = new System.Windows.Forms.Panel();
            this.btnCopyLicenseInfo = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnActivateNew = new System.Windows.Forms.Button();
            this.txtLicenseInfo = new System.Windows.Forms.TextBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.lblLicenseInfoTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelActivation.SuspendLayout();
            this.panelLicenseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelActivation
            // 
            this.panelActivation.BackColor = System.Drawing.Color.White;
            this.panelActivation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelActivation.Controls.Add(this.linkLabelHelp);
            this.panelActivation.Controls.Add(this.lblStatus);
            this.panelActivation.Controls.Add(this.btnExit);
            this.panelActivation.Controls.Add(this.btnActivate);
            this.panelActivation.Controls.Add(this.txtLicenseKey);
            this.panelActivation.Controls.Add(this.txtUserName);
            this.panelActivation.Controls.Add(this.lblLicenseKey);
            this.panelActivation.Controls.Add(this.lblUserName);
            this.panelActivation.Controls.Add(this.lblActivationTitle);
            this.panelActivation.Location = new System.Drawing.Point(30, 120);
            this.panelActivation.Name = "panelActivation";
            this.panelActivation.Size = new System.Drawing.Size(520, 300);
            this.panelActivation.TabIndex = 0;
            // 
            // linkLabelHelp
            // 
            this.linkLabelHelp.AutoSize = true;
            this.linkLabelHelp.Location = new System.Drawing.Point(200, 270);
            this.linkLabelHelp.Name = "linkLabelHelp";
            this.linkLabelHelp.Size = new System.Drawing.Size(120, 17);
            this.linkLabelHelp.TabIndex = 8;
            this.linkLabelHelp.TabStop = true;
            this.linkLabelHelp.Text = "Need help? Click here";
            this.linkLabelHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHelp_LinkClicked);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(70, 190);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.TextChanged += new System.EventHandler(this.lblStatus_TextChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(290, 220);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 40);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnActivate
            // 
            this.btnActivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnActivate.ForeColor = System.Drawing.Color.White;
            this.btnActivate.Location = new System.Drawing.Point(130, 220);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(150, 40);
            this.btnActivate.TabIndex = 4;
            this.btnActivate.Text = "Activate License";
            this.btnActivate.UseVisualStyleBackColor = false;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // txtLicenseKey
            // 
            this.txtLicenseKey.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLicenseKey.Location = new System.Drawing.Point(200, 120);
            this.txtLicenseKey.Name = "txtLicenseKey";
            this.txtLicenseKey.Size = new System.Drawing.Size(250, 30);
            this.txtLicenseKey.TabIndex = 2;
            this.txtLicenseKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseKey_KeyPress);
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUserName.Location = new System.Drawing.Point(200, 70);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(250, 30);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserName_KeyPress);
            // 
            // lblLicenseKey
            // 
            this.lblLicenseKey.AutoSize = true;
            this.lblLicenseKey.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLicenseKey.Location = new System.Drawing.Point(70, 123);
            this.lblLicenseKey.Name = "lblLicenseKey";
            this.lblLicenseKey.Size = new System.Drawing.Size(100, 23);
            this.lblLicenseKey.TabIndex = 2;
            this.lblLicenseKey.Text = "License Key:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserName.Location = new System.Drawing.Point(70, 73);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(96, 23);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "User Name:";
            // 
            // lblActivationTitle
            // 
            this.lblActivationTitle.AutoSize = true;
            this.lblActivationTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblActivationTitle.Location = new System.Drawing.Point(150, 20);
            this.lblActivationTitle.Name = "lblActivationTitle";
            this.lblActivationTitle.Size = new System.Drawing.Size(220, 32);
            this.lblActivationTitle.TabIndex = 0;
            this.lblActivationTitle.Text = "Activate Your License";
            // 
            // panelLicenseInfo
            // 
            this.panelLicenseInfo.BackColor = System.Drawing.Color.White;
            this.panelLicenseInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLicenseInfo.Controls.Add(this.btnCopyLicenseInfo);
            this.panelLicenseInfo.Controls.Add(this.btnDeactivate);
            this.panelLicenseInfo.Controls.Add(this.btnActivateNew);
            this.panelLicenseInfo.Controls.Add(this.txtLicenseInfo);
            this.panelLicenseInfo.Controls.Add(this.btnContinue);
            this.panelLicenseInfo.Controls.Add(this.lblLicenseInfoTitle);
            this.panelLicenseInfo.Location = new System.Drawing.Point(30, 120);
            this.panelLicenseInfo.Name = "panelLicenseInfo";
            this.panelLicenseInfo.Size = new System.Drawing.Size(520, 400);
            this.panelLicenseInfo.TabIndex = 1;
            // 
            // btnCopyLicenseInfo
            // 
            this.btnCopyLicenseInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCopyLicenseInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyLicenseInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyLicenseInfo.ForeColor = System.Drawing.Color.White;
            this.btnCopyLicenseInfo.Location = new System.Drawing.Point(20, 340);
            this.btnCopyLicenseInfo.Name = "btnCopyLicenseInfo";
            this.btnCopyLicenseInfo.Size = new System.Drawing.Size(150, 40);
            this.btnCopyLicenseInfo.TabIndex = 2;
            this.btnCopyLicenseInfo.Text = "Copy License Info";
            this.btnCopyLicenseInfo.UseVisualStyleBackColor = false;
            this.btnCopyLicenseInfo.Click += new System.EventHandler(this.btnCopyLicenseInfo_Click);
            // 
            // btnDeactivate
            // 
            this.btnDeactivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeactivate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeactivate.ForeColor = System.Drawing.Color.White;
            this.btnDeactivate.Location = new System.Drawing.Point(190, 340);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(150, 40);
            this.btnDeactivate.TabIndex = 3;
            this.btnDeactivate.Text = "Deactivate";
            this.btnDeactivate.UseVisualStyleBackColor = false;
            this.btnDeactivate.Click += new System.EventHandler(this.btnDeactivate_Click);
            // 
            // btnActivateNew
            // 
            this.btnActivateNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnActivateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivateNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivateNew.ForeColor = System.Drawing.Color.White;
            this.btnActivateNew.Location = new System.Drawing.Point(360, 340);
            this.btnActivateNew.Name = "btnActivateNew";
            this.btnActivateNew.Size = new System.Drawing.Size(150, 40);
            this.btnActivateNew.TabIndex = 4;
            this.btnActivateNew.Text = "Activate New";
            this.btnActivateNew.UseVisualStyleBackColor = false;
            this.btnActivateNew.Click += new System.EventHandler(this.btnActivateNew_Click);
            // 
            // txtLicenseInfo
            // 
            this.txtLicenseInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLicenseInfo.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicenseInfo.Location = new System.Drawing.Point(20, 70);
            this.txtLicenseInfo.Multiline = true;
            this.txtLicenseInfo.Name = "txtLicenseInfo";
            this.txtLicenseInfo.ReadOnly = true;
            this.txtLicenseInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLicenseInfo.Size = new System.Drawing.Size(480, 250);
            this.txtLicenseInfo.TabIndex = 1;
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.Location = new System.Drawing.Point(200, 280);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(150, 40);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "Continue to App";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // lblLicenseInfoTitle
            // 
            this.lblLicenseInfoTitle.AutoSize = true;
            this.lblLicenseInfoTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLicenseInfoTitle.Location = new System.Drawing.Point(150, 20);
            this.lblLicenseInfoTitle.Name = "lblLicenseInfoTitle";
            this.lblLicenseInfoTitle.Size = new System.Drawing.Size(220, 32);
            this.lblLicenseInfoTitle.TabIndex = 0;
            this.lblLicenseInfoTitle.Text = "License Information";
            // 
            // pictureBox1
            // 
            //this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // LicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(580, 550);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelLicenseInfo);
            this.Controls.Add(this.panelActivation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          //  this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP/POS System - License Management";
            this.Load += new System.EventHandler(this.LicenseForm_Load);
            this.panelActivation.ResumeLayout(false);
            this.panelActivation.PerformLayout();
            this.panelLicenseInfo.ResumeLayout(false);
            this.panelLicenseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelActivation;
        private System.Windows.Forms.Panel panelLicenseInfo;
        private System.Windows.Forms.Label lblActivationTitle;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblLicenseKey;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtLicenseKey;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLicenseInfoTitle;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TextBox txtLicenseInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelHelp;
        private System.Windows.Forms.Button btnActivateNew;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnCopyLicenseInfo;
    }
}
