namespace POS_Shop.Views.DB_Screens
{
    partial class ImportSupplierExcelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSupplierExcelForm));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.updatePriceGroup = new System.Windows.Forms.GroupBox();
            this.BrowsFileLbl = new System.Windows.Forms.Label();
            this.SaveUpdatedDataBtn = new System.Windows.Forms.Button();
            this.LoadUpdatedDataBtn = new System.Windows.Forms.Button();
            this.ImportUpdatedFilePathTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.BrowsUpdatedExcelFile = new System.Windows.Forms.Button();
            this.CustomerListGroup = new System.Windows.Forms.GroupBox();
            this.updatedSupplierListGrid = new System.Windows.Forms.DataGridView();
            this.updatePriceGroup.SuspendLayout();
            this.CustomerListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updatedSupplierListGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // updatePriceGroup
            // 
            this.updatePriceGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updatePriceGroup.Controls.Add(this.SaveUpdatedDataBtn);
            this.updatePriceGroup.Controls.Add(this.LoadUpdatedDataBtn);
            this.updatePriceGroup.Controls.Add(this.ImportUpdatedFilePathTxt);
            this.updatePriceGroup.Controls.Add(this.BrowsUpdatedExcelFile);
            this.updatePriceGroup.Controls.Add(this.BrowsFileLbl);
            this.updatePriceGroup.Location = new System.Drawing.Point(12, 12);
            this.updatePriceGroup.Name = "updatePriceGroup";
            this.updatePriceGroup.Size = new System.Drawing.Size(1103, 94);
            this.updatePriceGroup.TabIndex = 3;
            this.updatePriceGroup.TabStop = false;
            this.updatePriceGroup.Text = "Import Excel";
            // 
            // BrowsFileLbl
            // 
            this.BrowsFileLbl.AutoSize = true;
            this.BrowsFileLbl.Location = new System.Drawing.Point(16, 20);
            this.BrowsFileLbl.Name = "BrowsFileLbl";
            this.BrowsFileLbl.Size = new System.Drawing.Size(275, 16);
            this.BrowsFileLbl.TabIndex = 6;
            this.BrowsFileLbl.Text = "Brows Suppliers file to Load the data into Grid";
            // 
            // SaveUpdatedDataBtn
            // 
            this.SaveUpdatedDataBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveUpdatedDataBtn.BackColor = System.Drawing.Color.DarkKhaki;
            this.SaveUpdatedDataBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveUpdatedDataBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveUpdatedDataBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveUpdatedDataBtn.Image = global::POS_Shop.Properties.Resources.iconSave;
            this.SaveUpdatedDataBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveUpdatedDataBtn.Location = new System.Drawing.Point(894, 36);
            this.SaveUpdatedDataBtn.Name = "SaveUpdatedDataBtn";
            this.SaveUpdatedDataBtn.Size = new System.Drawing.Size(151, 48);
            this.SaveUpdatedDataBtn.TabIndex = 11;
            this.SaveUpdatedDataBtn.Text = "Save to DB";
            this.SaveUpdatedDataBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveUpdatedDataBtn.UseVisualStyleBackColor = false;
            this.SaveUpdatedDataBtn.Visible = false;
            this.SaveUpdatedDataBtn.Click += new System.EventHandler(this.SaveUpdatedDataBtn_Click);
            // 
            // LoadUpdatedDataBtn
            // 
            this.LoadUpdatedDataBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadUpdatedDataBtn.BackColor = System.Drawing.Color.Peru;
            this.LoadUpdatedDataBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadUpdatedDataBtn.Enabled = false;
            this.LoadUpdatedDataBtn.Image = global::POS_Shop.Properties.Resources.iconLoad;
            this.LoadUpdatedDataBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LoadUpdatedDataBtn.Location = new System.Drawing.Point(731, 37);
            this.LoadUpdatedDataBtn.Name = "LoadUpdatedDataBtn";
            this.LoadUpdatedDataBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LoadUpdatedDataBtn.Size = new System.Drawing.Size(157, 48);
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
            stateProperties5.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties5.FillColor = System.Drawing.Color.Empty;
            stateProperties5.ForeColor = System.Drawing.Color.Empty;
            stateProperties5.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ImportUpdatedFilePathTxt.OnActiveState = stateProperties5;
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties6.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.ImportUpdatedFilePathTxt.OnDisabledState = stateProperties6;
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties7.FillColor = System.Drawing.Color.Empty;
            stateProperties7.ForeColor = System.Drawing.Color.Empty;
            stateProperties7.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ImportUpdatedFilePathTxt.OnHoverState = stateProperties7;
            stateProperties8.BorderColor = System.Drawing.Color.Silver;
            stateProperties8.FillColor = System.Drawing.Color.White;
            stateProperties8.ForeColor = System.Drawing.Color.Empty;
            stateProperties8.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ImportUpdatedFilePathTxt.OnIdleState = stateProperties8;
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
            this.ImportUpdatedFilePathTxt.Size = new System.Drawing.Size(544, 41);
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
            this.BrowsUpdatedExcelFile.Location = new System.Drawing.Point(574, 34);
            this.BrowsUpdatedExcelFile.Margin = new System.Windows.Forms.Padding(1);
            this.BrowsUpdatedExcelFile.Name = "BrowsUpdatedExcelFile";
            this.BrowsUpdatedExcelFile.Size = new System.Drawing.Size(144, 48);
            this.BrowsUpdatedExcelFile.TabIndex = 7;
            this.BrowsUpdatedExcelFile.Text = "Brows File";
            this.BrowsUpdatedExcelFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BrowsUpdatedExcelFile.UseVisualStyleBackColor = false;
            this.BrowsUpdatedExcelFile.Click += new System.EventHandler(this.BrowsUpdatedExcelFile_Click);
            // 
            // CustomerListGroup
            // 
            this.CustomerListGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomerListGroup.Controls.Add(this.updatedSupplierListGrid);
            this.CustomerListGroup.Location = new System.Drawing.Point(12, 116);
            this.CustomerListGroup.Name = "CustomerListGroup";
            this.CustomerListGroup.Size = new System.Drawing.Size(1109, 409);
            this.CustomerListGroup.TabIndex = 4;
            this.CustomerListGroup.TabStop = false;
            this.CustomerListGroup.Text = "Supplier List";
            // 
            // updatedSupplierListGrid
            // 
            this.updatedSupplierListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.updatedSupplierListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.updatedSupplierListGrid.Location = new System.Drawing.Point(6, 43);
            this.updatedSupplierListGrid.Name = "updatedSupplierListGrid";
            this.updatedSupplierListGrid.RowHeadersWidth = 51;
            this.updatedSupplierListGrid.RowTemplate.Height = 24;
            this.updatedSupplierListGrid.Size = new System.Drawing.Size(1098, 360);
            this.updatedSupplierListGrid.TabIndex = 0;
            // 
            // ImportSupplierExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 532);
            this.Controls.Add(this.CustomerListGroup);
            this.Controls.Add(this.updatePriceGroup);
            this.Name = "ImportSupplierExcelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Supplier Excel File";
            this.updatePriceGroup.ResumeLayout(false);
            this.updatePriceGroup.PerformLayout();
            this.CustomerListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updatedSupplierListGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox updatePriceGroup;
        private System.Windows.Forms.Button SaveUpdatedDataBtn;
        private System.Windows.Forms.Button LoadUpdatedDataBtn;
        private Bunifu.UI.WinForms.BunifuTextBox ImportUpdatedFilePathTxt;
        private System.Windows.Forms.Button BrowsUpdatedExcelFile;
        private System.Windows.Forms.Label BrowsFileLbl;
        private System.Windows.Forms.GroupBox CustomerListGroup;
        private System.Windows.Forms.DataGridView updatedSupplierListGrid;
    }
}