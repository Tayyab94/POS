namespace POS_Shop.Views.DB_Screens
{
    partial class SettingsForm
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
            this.txtCurrentPath = new System.Windows.Forms.Label();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.btnChangeLocation = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnShowInExplorer = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCurrentPath
            // 
            this.txtCurrentPath.AutoSize = true;
            this.txtCurrentPath.Location = new System.Drawing.Point(21, 28);
            this.txtCurrentPath.Name = "txtCurrentPath";
            this.txtCurrentPath.Size = new System.Drawing.Size(98, 16);
            this.txtCurrentPath.TabIndex = 0;
            this.txtCurrentPath.Text = "DataBase Path";
            // 
            // lblFileInfo
            // 
            this.lblFileInfo.AutoSize = true;
            this.lblFileInfo.Location = new System.Drawing.Point(21, 99);
            this.lblFileInfo.Name = "lblFileInfo";
            this.lblFileInfo.Size = new System.Drawing.Size(118, 16);
            this.lblFileInfo.TabIndex = 1;
            this.lblFileInfo.Text = "Db File Information";
            // 
            // btnChangeLocation
            // 
            this.btnChangeLocation.Location = new System.Drawing.Point(14, 198);
            this.btnChangeLocation.Name = "btnChangeLocation";
            this.btnChangeLocation.Size = new System.Drawing.Size(175, 37);
            this.btnChangeLocation.TabIndex = 2;
            this.btnChangeLocation.Text = "Change Db File Location";
            this.btnChangeLocation.UseVisualStyleBackColor = true;
            this.btnChangeLocation.Click += new System.EventHandler(this.btnChangeLocation_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(198, 198);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(119, 37);
            this.btnBackup.TabIndex = 3;
            this.btnBackup.Text = "Back Up";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(326, 198);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(144, 37);
            this.btnTestConnection.TabIndex = 4;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnShowInExplorer
            // 
            this.btnShowInExplorer.Location = new System.Drawing.Point(59, 241);
            this.btnShowInExplorer.Name = "btnShowInExplorer";
            this.btnShowInExplorer.Size = new System.Drawing.Size(140, 37);
            this.btnShowInExplorer.TabIndex = 5;
            this.btnShowInExplorer.Text = "Show In Explorer";
            this.btnShowInExplorer.UseVisualStyleBackColor = true;
            this.btnShowInExplorer.Click += new System.EventHandler(this.btnShowInExplorer_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(217, 241);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 37);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 340);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnShowInExplorer);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnChangeLocation);
            this.Controls.Add(this.lblFileInfo);
            this.Controls.Add(this.txtCurrentPath);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtCurrentPath;
        private System.Windows.Forms.Label lblFileInfo;
        private System.Windows.Forms.Button btnChangeLocation;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnShowInExplorer;
        private System.Windows.Forms.Button btnClose;
    }
}