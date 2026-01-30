namespace POS_Shop.Views.DB_Screens
{
    partial class ImportForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProducts;
        private System.Windows.Forms.TabPage tabPrices;
        private System.Windows.Forms.DataGridView dgvProductsPreview;
        private System.Windows.Forms.DataGridView dgvPricesPreview;
        private System.Windows.Forms.Label lblProductsCount;
        private System.Windows.Forms.Label lblPricesCount;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtImportSummary;
        private System.Windows.Forms.Label lblImportSummary;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabProducts = new System.Windows.Forms.TabPage();
            this.dgvProductsPreview = new System.Windows.Forms.DataGridView();
            this.lblProductsCount = new System.Windows.Forms.Label();
            this.tabPrices = new System.Windows.Forms.TabPage();
            this.dgvPricesPreview = new System.Windows.Forms.DataGridView();
            this.lblPricesCount = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtImportSummary = new System.Windows.Forms.TextBox();
            this.lblImportSummary = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsPreview)).BeginInit();
            this.tabPrices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPricesPreview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(16, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(221, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Import Products";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilePath.Location = new System.Drawing.Point(19, 25);
            this.lblFilePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(75, 18);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "Excel File:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(103, 22);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(799, 22);
            this.txtFilePath.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(911, 20);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 28);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(804, 15);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(133, 43);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Start Import";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(945, 15);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 43);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabProducts);
            this.tabControl1.Controls.Add(this.tabPrices);
            this.tabControl1.Location = new System.Drawing.Point(16, 143);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1075, 369);
            this.tabControl1.TabIndex = 6;
            // 
            // tabProducts
            // 
            this.tabProducts.Controls.Add(this.dgvProductsPreview);
            this.tabProducts.Controls.Add(this.lblProductsCount);
            this.tabProducts.Location = new System.Drawing.Point(4, 25);
            this.tabProducts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabProducts.Size = new System.Drawing.Size(1067, 340);
            this.tabProducts.TabIndex = 0;
            this.tabProducts.Text = "Products Preview";
            this.tabProducts.UseVisualStyleBackColor = true;
            // 
            // dgvProductsPreview
            // 
            this.dgvProductsPreview.AllowUserToAddRows = false;
            this.dgvProductsPreview.AllowUserToDeleteRows = false;
            this.dgvProductsPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductsPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductsPreview.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductsPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProductsPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductsPreview.Location = new System.Drawing.Point(8, 43);
            this.dgvProductsPreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvProductsPreview.Name = "dgvProductsPreview";
            this.dgvProductsPreview.ReadOnly = true;
            this.dgvProductsPreview.RowHeadersVisible = false;
            this.dgvProductsPreview.RowHeadersWidth = 51;
            this.dgvProductsPreview.Size = new System.Drawing.Size(1048, 287);
            this.dgvProductsPreview.TabIndex = 0;
            // 
            // lblProductsCount
            // 
            this.lblProductsCount.AutoSize = true;
            this.lblProductsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductsCount.Location = new System.Drawing.Point(8, 12);
            this.lblProductsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductsCount.Name = "lblProductsCount";
            this.lblProductsCount.Size = new System.Drawing.Size(95, 18);
            this.lblProductsCount.TabIndex = 1;
            this.lblProductsCount.Text = "Products: 0";
            // 
            // tabPrices
            // 
            this.tabPrices.Controls.Add(this.dgvPricesPreview);
            this.tabPrices.Controls.Add(this.lblPricesCount);
            this.tabPrices.Location = new System.Drawing.Point(4, 25);
            this.tabPrices.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPrices.Name = "tabPrices";
            this.tabPrices.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPrices.Size = new System.Drawing.Size(1067, 340);
            this.tabPrices.TabIndex = 1;
            this.tabPrices.Text = "Prices Preview";
            this.tabPrices.UseVisualStyleBackColor = true;
            // 
            // dgvPricesPreview
            // 
            this.dgvPricesPreview.AllowUserToAddRows = false;
            this.dgvPricesPreview.AllowUserToDeleteRows = false;
            this.dgvPricesPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPricesPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPricesPreview.BackgroundColor = System.Drawing.Color.White;
            this.dgvPricesPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPricesPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPricesPreview.Location = new System.Drawing.Point(8, 43);
            this.dgvPricesPreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPricesPreview.Name = "dgvPricesPreview";
            this.dgvPricesPreview.ReadOnly = true;
            this.dgvPricesPreview.RowHeadersVisible = false;
            this.dgvPricesPreview.RowHeadersWidth = 51;
            this.dgvPricesPreview.Size = new System.Drawing.Size(1048, 287);
            this.dgvPricesPreview.TabIndex = 0;
            // 
            // lblPricesCount
            // 
            this.lblPricesCount.AutoSize = true;
            this.lblPricesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPricesCount.Location = new System.Drawing.Point(8, 12);
            this.lblPricesCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPricesCount.Name = "lblPricesCount";
            this.lblPricesCount.Size = new System.Drawing.Size(75, 18);
            this.lblPricesCount.TabIndex = 1;
            this.lblPricesCount.Text = "Prices: 0";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(16, 15);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.lblStatus.Size = new System.Drawing.Size(773, 43);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtImportSummary);
            this.groupBox1.Controls.Add(this.lblImportSummary);
            this.groupBox1.Location = new System.Drawing.Point(16, 519);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1075, 222);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import Results";
            // 
            // txtImportSummary
            // 
            this.txtImportSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImportSummary.BackColor = System.Drawing.Color.White;
            this.txtImportSummary.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImportSummary.Location = new System.Drawing.Point(13, 49);
            this.txtImportSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtImportSummary.Multiline = true;
            this.txtImportSummary.Name = "txtImportSummary";
            this.txtImportSummary.ReadOnly = true;
            this.txtImportSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtImportSummary.Size = new System.Drawing.Size(1052, 164);
            this.txtImportSummary.TabIndex = 1;
            // 
            // lblImportSummary
            // 
            this.lblImportSummary.AutoSize = true;
            this.lblImportSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportSummary.Location = new System.Drawing.Point(9, 25);
            this.lblImportSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImportSummary.Name = "lblImportSummary";
            this.lblImportSummary.Size = new System.Drawing.Size(132, 18);
            this.lblImportSummary.TabIndex = 0;
            this.lblImportSummary.Text = "Import Summary";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblFilePath);
            this.panel1.Controls.Add(this.txtFilePath);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Location = new System.Drawing.Point(16, 62);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1074, 73);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.btnImport);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Location = new System.Drawing.Point(16, 748);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1074, 73);
            this.panel2.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblTitle);
            this.panel3.Location = new System.Drawing.Point(16, 15);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1074, 61);
            this.panel3.TabIndex = 11;
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1107, 837);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1122, 874);
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Products from Excel";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabProducts.ResumeLayout(false);
            this.tabProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsPreview)).EndInit();
            this.tabPrices.ResumeLayout(false);
            this.tabPrices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPricesPreview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}