namespace POS_Shop.Views.CustomerLoanScreensV1
{
    partial class Customerledgerform
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.pnlKpi = new System.Windows.Forms.Panel();
            this.pnlDebitKpi = new System.Windows.Forms.Panel();
            this.lblKpi1 = new System.Windows.Forms.Label();
            this.lblTotalDebit = new System.Windows.Forms.Label();
            this.pnlCreditKpi = new System.Windows.Forms.Panel();
            this.lblKpi2 = new System.Windows.Forms.Label();
            this.lblTotalCredit = new System.Windows.Forms.Label();
            this.pnlBalanceKpi = new System.Windows.Forms.Panel();
            this.lblBalanceStatus = new System.Windows.Forms.Label();
            this.lblCurrentBalance = new System.Windows.Forms.Label();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.ResetDatesBtn = new System.Windows.Forms.Button();
            this.ReceivePaymentBtn = new System.Windows.Forms.Button();
            this.AddAdvanceBtn = new System.Windows.Forms.Button();
            this.AdjustmentBtn = new System.Windows.Forms.Button();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.LedgerGrid = new System.Windows.Forms.DataGridView();
            this.lblLoading = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlKpi.SuspendLayout();
            this.pnlDebitKpi.SuspendLayout();
            this.pnlCreditKpi.SuspendLayout();
            this.pnlBalanceKpi.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LedgerGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblCustomerName);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.pnlHeader.Size = new System.Drawing.Size(1257, 110);
            this.pnlHeader.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1217, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📒 Customer Ledger";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.lblCustomerName.Location = new System.Drawing.Point(20, 8);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(1217, 94);
            this.lblCustomerName.TabIndex = 1;
            // 
            // pnlKpi
            // 
            this.pnlKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlKpi.Controls.Add(this.pnlDebitKpi);
            this.pnlKpi.Controls.Add(this.pnlCreditKpi);
            this.pnlKpi.Controls.Add(this.pnlBalanceKpi);
            this.pnlKpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpi.Location = new System.Drawing.Point(0, 110);
            this.pnlKpi.Name = "pnlKpi";
            this.pnlKpi.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlKpi.Size = new System.Drawing.Size(1257, 141);
            this.pnlKpi.TabIndex = 3;
            // 
            // pnlDebitKpi
            // 
            this.pnlDebitKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlDebitKpi.Controls.Add(this.lblKpi1);
            this.pnlDebitKpi.Controls.Add(this.lblTotalDebit);
            this.pnlDebitKpi.Location = new System.Drawing.Point(15, 11);
            this.pnlDebitKpi.Name = "pnlDebitKpi";
            this.pnlDebitKpi.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlDebitKpi.Size = new System.Drawing.Size(220, 68);
            this.pnlDebitKpi.TabIndex = 0;
            // 
            // lblKpi1
            // 
            this.lblKpi1.AutoSize = true;
            this.lblKpi1.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblKpi1.ForeColor = System.Drawing.Color.Gray;
            this.lblKpi1.Location = new System.Drawing.Point(12, 8);
            this.lblKpi1.Name = "lblKpi1";
            this.lblKpi1.Size = new System.Drawing.Size(138, 15);
            this.lblKpi1.TabIndex = 0;
            this.lblKpi1.Text = "TOTAL DEBITED (LOAN)";
            // 
            // lblTotalDebit
            // 
            this.lblTotalDebit.AutoSize = true;
            this.lblTotalDebit.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalDebit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalDebit.Location = new System.Drawing.Point(12, 26);
            this.lblTotalDebit.Name = "lblTotalDebit";
            this.lblTotalDebit.Size = new System.Drawing.Size(118, 35);
            this.lblTotalDebit.TabIndex = 1;
            this.lblTotalDebit.Text = "PKR 0.00";
            // 
            // pnlCreditKpi
            // 
            this.pnlCreditKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(240)))));
            this.pnlCreditKpi.Controls.Add(this.lblKpi2);
            this.pnlCreditKpi.Controls.Add(this.lblTotalCredit);
            this.pnlCreditKpi.Location = new System.Drawing.Point(248, 11);
            this.pnlCreditKpi.Name = "pnlCreditKpi";
            this.pnlCreditKpi.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlCreditKpi.Size = new System.Drawing.Size(220, 68);
            this.pnlCreditKpi.TabIndex = 1;
            // 
            // lblKpi2
            // 
            this.lblKpi2.AutoSize = true;
            this.lblKpi2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblKpi2.ForeColor = System.Drawing.Color.Gray;
            this.lblKpi2.Location = new System.Drawing.Point(12, 8);
            this.lblKpi2.Name = "lblKpi2";
            this.lblKpi2.Size = new System.Drawing.Size(140, 15);
            this.lblKpi2.TabIndex = 0;
            this.lblKpi2.Text = "TOTAL CREDITED (PAID)";
            // 
            // lblTotalCredit
            // 
            this.lblTotalCredit.AutoSize = true;
            this.lblTotalCredit.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalCredit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblTotalCredit.Location = new System.Drawing.Point(12, 26);
            this.lblTotalCredit.Name = "lblTotalCredit";
            this.lblTotalCredit.Size = new System.Drawing.Size(118, 35);
            this.lblTotalCredit.TabIndex = 1;
            this.lblTotalCredit.Text = "PKR 0.00";
            // 
            // pnlBalanceKpi
            // 
            this.pnlBalanceKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlBalanceKpi.Controls.Add(this.lblBalanceStatus);
            this.pnlBalanceKpi.Controls.Add(this.lblCurrentBalance);
            this.pnlBalanceKpi.Location = new System.Drawing.Point(481, 11);
            this.pnlBalanceKpi.Name = "pnlBalanceKpi";
            this.pnlBalanceKpi.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlBalanceKpi.Size = new System.Drawing.Size(260, 68);
            this.pnlBalanceKpi.TabIndex = 2;
            // 
            // lblBalanceStatus
            // 
            this.lblBalanceStatus.AutoSize = true;
            this.lblBalanceStatus.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblBalanceStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblBalanceStatus.Location = new System.Drawing.Point(12, 8);
            this.lblBalanceStatus.Name = "lblBalanceStatus";
            this.lblBalanceStatus.Size = new System.Drawing.Size(116, 15);
            this.lblBalanceStatus.TabIndex = 0;
            this.lblBalanceStatus.Text = "CURRENT BALANCE";
            // 
            // lblCurrentBalance
            // 
            this.lblCurrentBalance.AutoSize = true;
            this.lblCurrentBalance.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblCurrentBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCurrentBalance.Location = new System.Drawing.Point(12, 26);
            this.lblCurrentBalance.Name = "lblCurrentBalance";
            this.lblCurrentBalance.Size = new System.Drawing.Size(118, 35);
            this.lblCurrentBalance.TabIndex = 1;
            this.lblCurrentBalance.Text = "PKR 0.00";
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.label1);
            this.pnlToolbar.Controls.Add(this.dtpFrom);
            this.pnlToolbar.Controls.Add(this.label2);
            this.pnlToolbar.Controls.Add(this.dtpTo);
            this.pnlToolbar.Controls.Add(this.SearchBtn);
            this.pnlToolbar.Controls.Add(this.ResetDatesBtn);
            this.pnlToolbar.Controls.Add(this.ReceivePaymentBtn);
            this.pnlToolbar.Controls.Add(this.AddAdvanceBtn);
            this.pnlToolbar.Controls.Add(this.AdjustmentBtn);
            this.pnlToolbar.Controls.Add(this.PrintBtn);
            this.pnlToolbar.Controls.Add(this.lblRowCount);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 251);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.pnlToolbar.Size = new System.Drawing.Size(1257, 81);
            this.pnlToolbar.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "From:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(55, 12);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(130, 27);
            this.dtpFrom.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(195, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "To:";
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(218, 12);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(130, 27);
            this.dtpTo.TabIndex = 3;
            // 
            // SearchBtn
            // 
            this.SearchBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.SearchBtn.FlatAppearance.BorderSize = 0;
            this.SearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SearchBtn.ForeColor = System.Drawing.Color.White;
            this.SearchBtn.Location = new System.Drawing.Point(358, 10);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(80, 30);
            this.SearchBtn.TabIndex = 4;
            this.SearchBtn.Text = "🔍 Search";
            this.SearchBtn.UseVisualStyleBackColor = false;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // ResetDatesBtn
            // 
            this.ResetDatesBtn.BackColor = System.Drawing.Color.White;
            this.ResetDatesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetDatesBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ResetDatesBtn.Location = new System.Drawing.Point(446, 10);
            this.ResetDatesBtn.Name = "ResetDatesBtn";
            this.ResetDatesBtn.Size = new System.Drawing.Size(65, 30);
            this.ResetDatesBtn.TabIndex = 5;
            this.ResetDatesBtn.Text = "All";
            this.ResetDatesBtn.UseVisualStyleBackColor = false;
            this.ResetDatesBtn.Click += new System.EventHandler(this.ResetDatesBtn_Click);
            // 
            // ReceivePaymentBtn
            // 
            this.ReceivePaymentBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.ReceivePaymentBtn.FlatAppearance.BorderSize = 0;
            this.ReceivePaymentBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReceivePaymentBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ReceivePaymentBtn.ForeColor = System.Drawing.Color.White;
            this.ReceivePaymentBtn.Location = new System.Drawing.Point(540, 8);
            this.ReceivePaymentBtn.Name = "ReceivePaymentBtn";
            this.ReceivePaymentBtn.Size = new System.Drawing.Size(140, 34);
            this.ReceivePaymentBtn.TabIndex = 6;
            this.ReceivePaymentBtn.Text = "✅ Receive Payment";
            this.ReceivePaymentBtn.UseVisualStyleBackColor = false;
            this.ReceivePaymentBtn.Click += new System.EventHandler(this.ReceivePaymentBtn_Click);
            // 
            // AddAdvanceBtn
            // 
            this.AddAdvanceBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.AddAdvanceBtn.FlatAppearance.BorderSize = 0;
            this.AddAdvanceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddAdvanceBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.AddAdvanceBtn.ForeColor = System.Drawing.Color.White;
            this.AddAdvanceBtn.Location = new System.Drawing.Point(688, 8);
            this.AddAdvanceBtn.Name = "AddAdvanceBtn";
            this.AddAdvanceBtn.Size = new System.Drawing.Size(130, 34);
            this.AddAdvanceBtn.TabIndex = 7;
            this.AddAdvanceBtn.Text = "🔵 Add Advance";
            this.AddAdvanceBtn.UseVisualStyleBackColor = false;
            this.AddAdvanceBtn.Click += new System.EventHandler(this.AddAdvanceBtn_Click);
            // 
            // AdjustmentBtn
            // 
            this.AdjustmentBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.AdjustmentBtn.FlatAppearance.BorderSize = 0;
            this.AdjustmentBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AdjustmentBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AdjustmentBtn.ForeColor = System.Drawing.Color.White;
            this.AdjustmentBtn.Location = new System.Drawing.Point(826, 8);
            this.AdjustmentBtn.Name = "AdjustmentBtn";
            this.AdjustmentBtn.Size = new System.Drawing.Size(120, 34);
            this.AdjustmentBtn.TabIndex = 8;
            this.AdjustmentBtn.Text = "⚙️ Adjustment";
            this.AdjustmentBtn.UseVisualStyleBackColor = false;
            this.AdjustmentBtn.Click += new System.EventHandler(this.AdjustmentBtn_Click);
            // 
            // PrintBtn
            // 
            this.PrintBtn.BackColor = System.Drawing.Color.White;
            this.PrintBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrintBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PrintBtn.Location = new System.Drawing.Point(954, 8);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(110, 34);
            this.PrintBtn.TabIndex = 9;
            this.PrintBtn.Text = "🖨️ Print / Export";
            this.PrintBtn.UseVisualStyleBackColor = false;
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // lblRowCount
            // 
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRowCount.ForeColor = System.Drawing.Color.Gray;
            this.lblRowCount.Location = new System.Drawing.Point(15, 36);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(62, 19);
            this.lblRowCount.TabIndex = 10;
            this.lblRowCount.Text = "0 entries";
            // 
            // LedgerGrid
            // 
            this.LedgerGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LedgerGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.LedgerGrid.BackgroundColor = System.Drawing.Color.White;
            this.LedgerGrid.ColumnHeadersHeight = 29;
            this.LedgerGrid.Location = new System.Drawing.Point(0, 332);
            this.LedgerGrid.Name = "LedgerGrid";
            this.LedgerGrid.RowHeadersWidth = 51;
            this.LedgerGrid.Size = new System.Drawing.Size(1257, 329);
            this.LedgerGrid.TabIndex = 0;
            // 
            // lblLoading
            // 
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblLoading.ForeColor = System.Drawing.Color.Gray;
            this.lblLoading.Location = new System.Drawing.Point(0, 332);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(1257, 341);
            this.lblLoading.TabIndex = 1;
            this.lblLoading.Text = "Loading...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoading.Visible = false;
            // 
            // Customerledgerform
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1257, 673);
            this.Controls.Add(this.LedgerGrid);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlKpi);
            this.Controls.Add(this.pnlHeader);
            this.Name = "Customerledgerform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Ledger";
            this.Load += new System.EventHandler(this.Customerledgerform_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlKpi.ResumeLayout(false);
            this.pnlDebitKpi.ResumeLayout(false);
            this.pnlDebitKpi.PerformLayout();
            this.pnlCreditKpi.ResumeLayout(false);
            this.pnlCreditKpi.PerformLayout();
            this.pnlBalanceKpi.ResumeLayout(false);
            this.pnlBalanceKpi.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LedgerGrid)).EndInit();
            this.ResumeLayout(false);

        }

        // Controls
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Panel pnlKpi;
        private System.Windows.Forms.Panel pnlDebitKpi;
        private System.Windows.Forms.Label lblKpi1;
        private System.Windows.Forms.Label lblTotalDebit;
        private System.Windows.Forms.Panel pnlCreditKpi;
        private System.Windows.Forms.Label lblKpi2;
        private System.Windows.Forms.Label lblTotalCredit;
        private System.Windows.Forms.Panel pnlBalanceKpi;
        private System.Windows.Forms.Label lblBalanceStatus;
        private System.Windows.Forms.Label lblCurrentBalance;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Button ResetDatesBtn;
        private System.Windows.Forms.Button ReceivePaymentBtn;
        private System.Windows.Forms.Button AddAdvanceBtn;
        private System.Windows.Forms.Button AdjustmentBtn;
        private System.Windows.Forms.Button PrintBtn;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.DataGridView LedgerGrid;
        private System.Windows.Forms.Label lblLoading;
    }
}