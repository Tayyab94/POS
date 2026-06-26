namespace POS_Shop.Views.CustomerLoanScreensV1
{
    partial class OrderDetailViewForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMeta = new System.Windows.Forms.Panel();
            this.pnlMetaInner = new System.Windows.Forms.Panel();
            this.lblPaymentTypeLbl = new System.Windows.Forms.Label();
            this.lblPaymentTypeVal = new System.Windows.Forms.Label();
            this.lblDateLbl = new System.Windows.Forms.Label();
            this.lblDateVal = new System.Windows.Forms.Label();
            this.lblCustomerLbl = new System.Windows.Forms.Label();
            this.lblCustomerVal = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.OrderItemsGrid = new System.Windows.Forms.DataGridView();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.pnlTotalsInner = new System.Windows.Forms.Panel();
            this.lblTotalLbl = new System.Windows.Forms.Label();
            this.lblTotalVal = new System.Windows.Forms.Label();
            this.lblPaidLbl = new System.Windows.Forms.Label();
            this.lblPaidVal = new System.Windows.Forms.Label();
            this.lblBalanceLbl = new System.Windows.Forms.Label();
            this.lblBalanceVal = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMeta.SuspendLayout();
            this.pnlMetaInner.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderItemsGrid)).BeginInit();
            this.pnlTotals.SuspendLayout();
            this.pnlTotalsInner.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblInvoiceNo);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 14, 24, 14);
            this.pnlHeader.Size = new System.Drawing.Size(911, 90);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInvoiceNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.lblInvoiceNo.Location = new System.Drawing.Point(24, 46);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(863, 30);
            this.lblInvoiceNo.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(24, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(863, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🧾 Order Details";
            // 
            // pnlMeta
            // 
            this.pnlMeta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlMeta.Controls.Add(this.pnlMetaInner);
            this.pnlMeta.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMeta.Location = new System.Drawing.Point(0, 90);
            this.pnlMeta.Name = "pnlMeta";
            this.pnlMeta.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlMeta.Size = new System.Drawing.Size(911, 80);
            this.pnlMeta.TabIndex = 1;
            // 
            // pnlMetaInner
            // 
            this.pnlMetaInner.BackColor = System.Drawing.Color.White;
            this.pnlMetaInner.Controls.Add(this.lblPaymentTypeLbl);
            this.pnlMetaInner.Controls.Add(this.lblPaymentTypeVal);
            this.pnlMetaInner.Controls.Add(this.lblDateLbl);
            this.pnlMetaInner.Controls.Add(this.lblDateVal);
            this.pnlMetaInner.Controls.Add(this.lblCustomerLbl);
            this.pnlMetaInner.Controls.Add(this.lblCustomerVal);
            this.pnlMetaInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMetaInner.Location = new System.Drawing.Point(16, 10);
            this.pnlMetaInner.Name = "pnlMetaInner";
            this.pnlMetaInner.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlMetaInner.Size = new System.Drawing.Size(879, 60);
            this.pnlMetaInner.TabIndex = 0;
            // 
            // lblPaymentTypeLbl
            // 
            this.lblPaymentTypeLbl.AutoSize = true;
            this.lblPaymentTypeLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblPaymentTypeLbl.ForeColor = System.Drawing.Color.Gray;
            this.lblPaymentTypeLbl.Location = new System.Drawing.Point(520, 8);
            this.lblPaymentTypeLbl.Name = "lblPaymentTypeLbl";
            this.lblPaymentTypeLbl.Size = new System.Drawing.Size(103, 17);
            this.lblPaymentTypeLbl.TabIndex = 0;
            this.lblPaymentTypeLbl.Text = "PAYMENT TYPE";
            // 
            // lblPaymentTypeVal
            // 
            this.lblPaymentTypeVal.AutoSize = true;
            this.lblPaymentTypeVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPaymentTypeVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblPaymentTypeVal.Location = new System.Drawing.Point(520, 24);
            this.lblPaymentTypeVal.Name = "lblPaymentTypeVal";
            this.lblPaymentTypeVal.Size = new System.Drawing.Size(27, 23);
            this.lblPaymentTypeVal.TabIndex = 1;
            this.lblPaymentTypeVal.Text = "—";
            // 
            // lblDateLbl
            // 
            this.lblDateLbl.AutoSize = true;
            this.lblDateLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblDateLbl.ForeColor = System.Drawing.Color.Gray;
            this.lblDateLbl.Location = new System.Drawing.Point(280, 8);
            this.lblDateLbl.Name = "lblDateLbl";
            this.lblDateLbl.Size = new System.Drawing.Size(88, 17);
            this.lblDateLbl.TabIndex = 2;
            this.lblDateLbl.Text = "ORDER DATE";
            // 
            // lblDateVal
            // 
            this.lblDateVal.AutoSize = true;
            this.lblDateVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDateVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblDateVal.Location = new System.Drawing.Point(280, 24);
            this.lblDateVal.Name = "lblDateVal";
            this.lblDateVal.Size = new System.Drawing.Size(27, 23);
            this.lblDateVal.TabIndex = 3;
            this.lblDateVal.Text = "—";
            // 
            // lblCustomerLbl
            // 
            this.lblCustomerLbl.AutoSize = true;
            this.lblCustomerLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblCustomerLbl.ForeColor = System.Drawing.Color.Gray;
            this.lblCustomerLbl.Location = new System.Drawing.Point(14, 8);
            this.lblCustomerLbl.Name = "lblCustomerLbl";
            this.lblCustomerLbl.Size = new System.Drawing.Size(77, 17);
            this.lblCustomerLbl.TabIndex = 4;
            this.lblCustomerLbl.Text = "CUSTOMER";
            // 
            // lblCustomerVal
            // 
            this.lblCustomerVal.AutoSize = true;
            this.lblCustomerVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustomerVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblCustomerVal.Location = new System.Drawing.Point(14, 24);
            this.lblCustomerVal.Name = "lblCustomerVal";
            this.lblCustomerVal.Size = new System.Drawing.Size(27, 23);
            this.lblCustomerVal.TabIndex = 5;
            this.lblCustomerVal.Text = "—";
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.OrderItemsGrid);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 170);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlGrid.Size = new System.Drawing.Size(911, 320);
            this.pnlGrid.TabIndex = 2;
            // 
            // OrderItemsGrid
            // 
            this.OrderItemsGrid.AllowUserToAddRows = false;
            this.OrderItemsGrid.AllowUserToDeleteRows = false;
            this.OrderItemsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderItemsGrid.BackgroundColor = System.Drawing.Color.White;
            this.OrderItemsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OrderItemsGrid.ColumnHeadersHeight = 36;
            this.OrderItemsGrid.EnableHeadersVisualStyles = false;
            this.OrderItemsGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.OrderItemsGrid.Location = new System.Drawing.Point(13, 36);
            this.OrderItemsGrid.Name = "OrderItemsGrid";
            this.OrderItemsGrid.ReadOnly = true;
            this.OrderItemsGrid.RowHeadersVisible = false;
            this.OrderItemsGrid.RowHeadersWidth = 51;
            this.OrderItemsGrid.RowTemplate.Height = 34;
            this.OrderItemsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrderItemsGrid.Size = new System.Drawing.Size(883, 264);
            this.OrderItemsGrid.TabIndex = 0;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblGridTitle.Location = new System.Drawing.Point(16, 12);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(107, 20);
            this.lblGridTitle.TabIndex = 1;
            this.lblGridTitle.Text = "ORDER ITEMS";
            // 
            // pnlTotals
            // 
            this.pnlTotals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlTotals.Controls.Add(this.pnlTotalsInner);
            this.pnlTotals.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotals.Location = new System.Drawing.Point(0, 490);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlTotals.Size = new System.Drawing.Size(911, 90);
            this.pnlTotals.TabIndex = 3;
            // 
            // pnlTotalsInner
            // 
            this.pnlTotalsInner.BackColor = System.Drawing.Color.White;
            this.pnlTotalsInner.Controls.Add(this.lblTotalLbl);
            this.pnlTotalsInner.Controls.Add(this.lblTotalVal);
            this.pnlTotalsInner.Controls.Add(this.lblPaidLbl);
            this.pnlTotalsInner.Controls.Add(this.lblPaidVal);
            this.pnlTotalsInner.Controls.Add(this.lblBalanceLbl);
            this.pnlTotalsInner.Controls.Add(this.lblBalanceVal);
            this.pnlTotalsInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalsInner.Location = new System.Drawing.Point(16, 10);
            this.pnlTotalsInner.Name = "pnlTotalsInner";
            this.pnlTotalsInner.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlTotalsInner.Size = new System.Drawing.Size(879, 70);
            this.pnlTotalsInner.TabIndex = 0;
            // 
            // lblTotalLbl
            // 
            this.lblTotalLbl.AutoSize = true;
            this.lblTotalLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalLbl.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalLbl.Location = new System.Drawing.Point(14, 8);
            this.lblTotalLbl.Name = "lblTotalLbl";
            this.lblTotalLbl.Size = new System.Drawing.Size(78, 17);
            this.lblTotalLbl.TabIndex = 0;
            this.lblTotalLbl.Text = "TOTAL BILL";
            // 
            // lblTotalVal
            // 
            this.lblTotalVal.AutoSize = true;
            this.lblTotalVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTotalVal.Location = new System.Drawing.Point(14, 24);
            this.lblTotalVal.Name = "lblTotalVal";
            this.lblTotalVal.Size = new System.Drawing.Size(117, 32);
            this.lblTotalVal.TabIndex = 1;
            this.lblTotalVal.Text = "PKR 0.00";
            // 
            // lblPaidLbl
            // 
            this.lblPaidLbl.AutoSize = true;
            this.lblPaidLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblPaidLbl.ForeColor = System.Drawing.Color.Gray;
            this.lblPaidLbl.Location = new System.Drawing.Point(280, 8);
            this.lblPaidLbl.Name = "lblPaidLbl";
            this.lblPaidLbl.Size = new System.Drawing.Size(100, 17);
            this.lblPaidLbl.TabIndex = 2;
            this.lblPaidLbl.Text = "AMOUNT PAID";
            // 
            // lblPaidVal
            // 
            this.lblPaidVal.AutoSize = true;
            this.lblPaidVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPaidVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblPaidVal.Location = new System.Drawing.Point(280, 24);
            this.lblPaidVal.Name = "lblPaidVal";
            this.lblPaidVal.Size = new System.Drawing.Size(117, 32);
            this.lblPaidVal.TabIndex = 3;
            this.lblPaidVal.Text = "PKR 0.00";
            // 
            // lblBalanceLbl
            // 
            this.lblBalanceLbl.AutoSize = true;
            this.lblBalanceLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblBalanceLbl.ForeColor = System.Drawing.Color.Gray;
            this.lblBalanceLbl.Location = new System.Drawing.Point(520, 8);
            this.lblBalanceLbl.Name = "lblBalanceLbl";
            this.lblBalanceLbl.Size = new System.Drawing.Size(96, 17);
            this.lblBalanceLbl.TabIndex = 4;
            this.lblBalanceLbl.Text = "BALANCE DUE";
            // 
            // lblBalanceVal
            // 
            this.lblBalanceVal.AutoSize = true;
            this.lblBalanceVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblBalanceVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblBalanceVal.Location = new System.Drawing.Point(520, 24);
            this.lblBalanceVal.Name = "lblBalanceVal";
            this.lblBalanceVal.Size = new System.Drawing.Size(117, 32);
            this.lblBalanceVal.TabIndex = 5;
            this.lblBalanceVal.Text = "PKR 0.00";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnPrint);
            this.pnlFooter.Controls.Add(this.btnClose);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 580);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlFooter.Size = new System.Drawing.Size(911, 54);
            this.pnlFooter.TabIndex = 4;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(140, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 34);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "🖨️ Print Receipt";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(16, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 34);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "✖ Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // OrderDetailViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(911, 634);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlTotals);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlMeta);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderDetailViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Order Details";
            this.Load += new System.EventHandler(this.OrderDetailViewForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlMeta.ResumeLayout(false);
            this.pnlMetaInner.ResumeLayout(false);
            this.pnlMetaInner.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderItemsGrid)).EndInit();
            this.pnlTotals.ResumeLayout(false);
            this.pnlTotalsInner.ResumeLayout(false);
            this.pnlTotalsInner.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // ── Controls ─────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInvoiceNo;

        private System.Windows.Forms.Panel pnlMeta;
        private System.Windows.Forms.Panel pnlMetaInner;
        private System.Windows.Forms.Label lblCustomerLbl;
        private System.Windows.Forms.Label lblCustomerVal;
        private System.Windows.Forms.Label lblDateLbl;
        private System.Windows.Forms.Label lblDateVal;
        private System.Windows.Forms.Label lblPaymentTypeLbl;
        private System.Windows.Forms.Label lblPaymentTypeVal;

        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView OrderItemsGrid;

        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Panel pnlTotalsInner;
        private System.Windows.Forms.Label lblTotalLbl;
        private System.Windows.Forms.Label lblTotalVal;
        private System.Windows.Forms.Label lblPaidLbl;
        private System.Windows.Forms.Label lblPaidVal;
        private System.Windows.Forms.Label lblBalanceLbl;
        private System.Windows.Forms.Label lblBalanceVal;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
    }
}