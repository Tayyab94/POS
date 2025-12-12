namespace POS_Shop.Views.DB_Screens
{
    partial class ImportCustomerFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportCustomerFile));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.updatePriceGroup = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveUpdatedPriceBtn = new System.Windows.Forms.Button();
            this.LoadUpdatedDataBtn = new System.Windows.Forms.Button();
            this.ImportUpdatedFilePathTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.BrowsUpdatedExcelFile = new System.Windows.Forms.Button();
            this.CustomerListGroup = new System.Windows.Forms.GroupBox();
            this.updatedCustomerListGrid = new System.Windows.Forms.DataGridView();
            this.updatePriceGroup.SuspendLayout();
            this.CustomerListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updatedCustomerListGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // updatePriceGroup
            // 
            this.updatePriceGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updatePriceGroup.Controls.Add(this.SaveUpdatedPriceBtn);
            this.updatePriceGroup.Controls.Add(this.LoadUpdatedDataBtn);
            this.updatePriceGroup.Controls.Add(this.ImportUpdatedFilePathTxt);
            this.updatePriceGroup.Controls.Add(this.BrowsUpdatedExcelFile);
            this.updatePriceGroup.Controls.Add(this.label2);
            this.updatePriceGroup.Location = new System.Drawing.Point(10, 20);
            this.updatePriceGroup.Name = "updatePriceGroup";
            this.updatePriceGroup.Size = new System.Drawing.Size(1019, 95);
            this.updatePriceGroup.TabIndex = 2;
            this.updatePriceGroup.TabStop = false;
            this.updatePriceGroup.Text = "Import Excel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Brows Customer file to Load the data into Grid";
            // 
            // SaveUpdatedPriceBtn
            // 
            this.SaveUpdatedPriceBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveUpdatedPriceBtn.BackColor = System.Drawing.Color.DarkKhaki;
            this.SaveUpdatedPriceBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveUpdatedPriceBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveUpdatedPriceBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveUpdatedPriceBtn.Image = global::POS_Shop.Properties.Resources.iconSave;
            this.SaveUpdatedPriceBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveUpdatedPriceBtn.Location = new System.Drawing.Point(810, 41);
            this.SaveUpdatedPriceBtn.Name = "SaveUpdatedPriceBtn";
            this.SaveUpdatedPriceBtn.Size = new System.Drawing.Size(151, 50);
            this.SaveUpdatedPriceBtn.TabIndex = 11;
            this.SaveUpdatedPriceBtn.Text = "Save to DB";
            this.SaveUpdatedPriceBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveUpdatedPriceBtn.UseVisualStyleBackColor = false;
            this.SaveUpdatedPriceBtn.Visible = false;
            this.SaveUpdatedPriceBtn.Click += new System.EventHandler(this.SaveUpdatedPriceBtn_Click);
            // 
            // LoadUpdatedDataBtn
            // 
            this.LoadUpdatedDataBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadUpdatedDataBtn.BackColor = System.Drawing.Color.Peru;
            this.LoadUpdatedDataBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadUpdatedDataBtn.Enabled = false;
            this.LoadUpdatedDataBtn.Image = global::POS_Shop.Properties.Resources.iconLoad;
            this.LoadUpdatedDataBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LoadUpdatedDataBtn.Location = new System.Drawing.Point(647, 41);
            this.LoadUpdatedDataBtn.Name = "LoadUpdatedDataBtn";
            this.LoadUpdatedDataBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LoadUpdatedDataBtn.Size = new System.Drawing.Size(157, 51);
            this.LoadUpdatedDataBtn.TabIndex = 9;
            this.LoadUpdatedDataBtn.Text = "Load Data";
            this.LoadUpdatedDataBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LoadUpdatedDataBtn.UseVisualStyleBackColor = false;
            this.LoadUpdatedDataBtn.Click += new System.EventHandler(this.LoadUpdatedDataBtn_Click);
            // 
            // ImportUpdatedFilePathTxt
            // 
            this.ImportUpdatedFilePathTxt.AcceptsReturn = false;
            this.ImportUpdatedFilePathTxt.AcceptsTab = false;
            this.ImportUpdatedFilePathTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportUpdatedFilePathTxt.AnimationSpeed = 200;
            this.ImportUpdatedFilePathTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.ImportUpdatedFilePathTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.ImportUpdatedFilePathTxt.BackColor = System.Drawing.Color.Transparent;
            this.ImportUpdatedFilePathTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ImportUpdatedFilePathTxt.BackgroundImage")));
            this.ImportUpdatedFilePathTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.ImportUpdatedFilePathTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ImportUpdatedFilePathTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.ImportUpdatedFilePathTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.ImportUpdatedFilePathTxt.BorderRadius = 1;
            this.ImportUpdatedFilePathTxt.BorderThickness = 1;
            this.ImportUpdatedFilePathTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.ImportUpdatedFilePathTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ImportUpdatedFilePathTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.ImportUpdatedFilePathTxt.DefaultText = "";
            this.ImportUpdatedFilePathTxt.FillColor = System.Drawing.Color.White;
            this.ImportUpdatedFilePathTxt.HideSelection = true;
            this.ImportUpdatedFilePathTxt.IconLeft = null;
            this.ImportUpdatedFilePathTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.ImportUpdatedFilePathTxt.IconPadding = 10;
            this.ImportUpdatedFilePathTxt.IconRight = null;
            this.ImportUpdatedFilePathTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.ImportUpdatedFilePathTxt.Lines = new string[0];
            this.ImportUpdatedFilePathTxt.Location = new System.Drawing.Point(18, 41);
            this.ImportUpdatedFilePathTxt.MaxLength = 32767;
            this.ImportUpdatedFilePathTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.ImportUpdatedFilePathTxt.Modified = false;
            this.ImportUpdatedFilePathTxt.Multiline = false;
            this.ImportUpdatedFilePathTxt.Name = "ImportUpdatedFilePathTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ImportUpdatedFilePathTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.ImportUpdatedFilePathTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ImportUpdatedFilePathTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ImportUpdatedFilePathTxt.OnIdleState = stateProperties4;
            this.ImportUpdatedFilePathTxt.Padding = new System.Windows.Forms.Padding(3);
            this.ImportUpdatedFilePathTxt.PasswordChar = '\0';
            this.ImportUpdatedFilePathTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.ImportUpdatedFilePathTxt.PlaceholderText = "Enter text";
            this.ImportUpdatedFilePathTxt.ReadOnly = false;
            this.ImportUpdatedFilePathTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ImportUpdatedFilePathTxt.SelectedText = "";
            this.ImportUpdatedFilePathTxt.SelectionLength = 0;
            this.ImportUpdatedFilePathTxt.SelectionStart = 0;
            this.ImportUpdatedFilePathTxt.ShortcutsEnabled = true;
            this.ImportUpdatedFilePathTxt.Size = new System.Drawing.Size(460, 41);
            this.ImportUpdatedFilePathTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.ImportUpdatedFilePathTxt.TabIndex = 8;
            this.ImportUpdatedFilePathTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ImportUpdatedFilePathTxt.TextMarginBottom = 0;
            this.ImportUpdatedFilePathTxt.TextMarginLeft = 3;
            this.ImportUpdatedFilePathTxt.TextMarginTop = 0;
            this.ImportUpdatedFilePathTxt.TextPlaceholder = "Enter text";
            this.ImportUpdatedFilePathTxt.UseSystemPasswordChar = false;
            this.ImportUpdatedFilePathTxt.WordWrap = true;
            // 
            // BrowsUpdatedExcelFile
            // 
            this.BrowsUpdatedExcelFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowsUpdatedExcelFile.BackColor = System.Drawing.Color.SlateBlue;
            this.BrowsUpdatedExcelFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrowsUpdatedExcelFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowsUpdatedExcelFile.ForeColor = System.Drawing.SystemColors.Control;
            this.BrowsUpdatedExcelFile.Image = global::POS_Shop.Properties.Resources.iconExcel;
            this.BrowsUpdatedExcelFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BrowsUpdatedExcelFile.Location = new System.Drawing.Point(490, 41);
            this.BrowsUpdatedExcelFile.Margin = new System.Windows.Forms.Padding(1);
            this.BrowsUpdatedExcelFile.Name = "BrowsUpdatedExcelFile";
            this.BrowsUpdatedExcelFile.Size = new System.Drawing.Size(144, 53);
            this.BrowsUpdatedExcelFile.TabIndex = 7;
            this.BrowsUpdatedExcelFile.Text = "Brows File";
            this.BrowsUpdatedExcelFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BrowsUpdatedExcelFile.UseVisualStyleBackColor = false;
            this.BrowsUpdatedExcelFile.Click += new System.EventHandler(this.BrowsUpdatedExcelFile_Click);
            // 
            // CustomerListGroup
            // 
            this.CustomerListGroup.Controls.Add(this.updatedCustomerListGrid);
            this.CustomerListGroup.Location = new System.Drawing.Point(10, 133);
            this.CustomerListGroup.Name = "CustomerListGroup";
            this.CustomerListGroup.Size = new System.Drawing.Size(1116, 420);
            this.CustomerListGroup.TabIndex = 3;
            this.CustomerListGroup.TabStop = false;
            this.CustomerListGroup.Text = "Customer List";
            // 
            // updatedCustomerListGrid
            // 
            this.updatedCustomerListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.updatedCustomerListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.updatedCustomerListGrid.Location = new System.Drawing.Point(6, 36);
            this.updatedCustomerListGrid.Name = "updatedCustomerListGrid";
            this.updatedCustomerListGrid.RowHeadersWidth = 51;
            this.updatedCustomerListGrid.RowTemplate.Height = 24;
            this.updatedCustomerListGrid.Size = new System.Drawing.Size(1098, 360);
            this.updatedCustomerListGrid.TabIndex = 0;
            // 
            // ImportCustomerFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 565);
            this.Controls.Add(this.CustomerListGroup);
            this.Controls.Add(this.updatePriceGroup);
            this.Name = "ImportCustomerFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportCustomerFile";
            this.updatePriceGroup.ResumeLayout(false);
            this.updatePriceGroup.PerformLayout();
            this.CustomerListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updatedCustomerListGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox updatePriceGroup;
        private System.Windows.Forms.Button SaveUpdatedPriceBtn;
        private System.Windows.Forms.Button LoadUpdatedDataBtn;
        private Bunifu.UI.WinForms.BunifuTextBox ImportUpdatedFilePathTxt;
        private System.Windows.Forms.Button BrowsUpdatedExcelFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox CustomerListGroup;
        private System.Windows.Forms.DataGridView updatedCustomerListGrid;
    }
}