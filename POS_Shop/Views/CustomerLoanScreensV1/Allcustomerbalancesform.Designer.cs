namespace POS_Shop.Views.CustomerLoanScreensV1
{
    //partial class AllCustomerBalancesForm
    //{
    //    private System.ComponentModel.IContainer components = null;

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing && (components != null)) components.Dispose();
    //        base.Dispose(disposing);
    //    }

    //    private void InitializeComponent()
    //    {
    //        this.pnlHeader = new System.Windows.Forms.Panel();
    //        this.lblTitle = new System.Windows.Forms.Label();
    //        this.lblSubtitle = new System.Windows.Forms.Label();
    //        this.pnlKpi = new System.Windows.Forms.Panel();
    //        this.pnlLoanKpi = new System.Windows.Forms.Panel();
    //        this.lblLoanKpiTitle = new System.Windows.Forms.Label();
    //        this.lblTotalLoanAmount = new System.Windows.Forms.Label();
    //        this.lblLoanCount = new System.Windows.Forms.Label();
    //        this.pnlAdvanceKpi = new System.Windows.Forms.Panel();
    //        this.lblAdvanceKpiTitle = new System.Windows.Forms.Label();
    //        this.lblTotalAdvanceAmount = new System.Windows.Forms.Label();
    //        this.lblAdvanceCount = new System.Windows.Forms.Label();
    //        this.pnlToolbar = new System.Windows.Forms.Panel();
    //        this.txtSearch = new System.Windows.Forms.TextBox();
    //        this.rbAll = new System.Windows.Forms.RadioButton();
    //        this.rbLoan = new System.Windows.Forms.RadioButton();
    //        this.rbAdvance = new System.Windows.Forms.RadioButton();
    //        this.rbClear = new System.Windows.Forms.RadioButton();
    //        this.OpenLedgerBtn = new System.Windows.Forms.Button();
    //        this.RefreshBtn = new System.Windows.Forms.Button();
    //        this.lblCount = new System.Windows.Forms.Label();
    //        this.BalanceGrid = new System.Windows.Forms.DataGridView();
    //        this.lblLoading = new System.Windows.Forms.Label();
    //        this.pnlHeader.SuspendLayout();
    //        this.pnlKpi.SuspendLayout();
    //        this.pnlLoanKpi.SuspendLayout();
    //        this.pnlAdvanceKpi.SuspendLayout();
    //        this.pnlToolbar.SuspendLayout();
    //        ((System.ComponentModel.ISupportInitialize)(this.BalanceGrid)).BeginInit();
    //        this.SuspendLayout();
    //        // 
    //        // pnlHeader
    //        // 
    //        this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
    //        this.pnlHeader.Controls.Add(this.lblTitle);
    //        this.pnlHeader.Controls.Add(this.lblSubtitle);
    //        this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
    //        this.pnlHeader.Location = new System.Drawing.Point(0, 0);
    //        this.pnlHeader.Name = "pnlHeader";
    //        this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
    //        this.pnlHeader.Size = new System.Drawing.Size(979, 65);
    //        this.pnlHeader.TabIndex = 4;
    //        // 
    //        // lblTitle
    //        // 
    //        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
    //        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
    //        this.lblTitle.ForeColor = System.Drawing.Color.White;
    //        this.lblTitle.Location = new System.Drawing.Point(20, 8);
    //        this.lblTitle.Name = "lblTitle";
    //        this.lblTitle.Size = new System.Drawing.Size(939, 34);
    //        this.lblTitle.TabIndex = 0;
    //        this.lblTitle.Text = "💳 Customer Loan & Advance Dashboard";
    //        // 
    //        // lblSubtitle
    //        // 
    //        this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Fill;
    //        this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
    //        this.lblSubtitle.Location = new System.Drawing.Point(20, 8);
    //        this.lblSubtitle.Name = "lblSubtitle";
    //        this.lblSubtitle.Size = new System.Drawing.Size(939, 49);
    //        this.lblSubtitle.TabIndex = 1;
    //        this.lblSubtitle.Text = "Double-click any customer to view full ledger";
    //        // 
    //        // pnlKpi
    //        // 
    //        this.pnlKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
    //        this.pnlKpi.Controls.Add(this.pnlLoanKpi);
    //        this.pnlKpi.Controls.Add(this.pnlAdvanceKpi);
    //        this.pnlKpi.Dock = System.Windows.Forms.DockStyle.Top;
    //        this.pnlKpi.Location = new System.Drawing.Point(0, 65);
    //        this.pnlKpi.Name = "pnlKpi";
    //        this.pnlKpi.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
    //        this.pnlKpi.Size = new System.Drawing.Size(979, 90);
    //        this.pnlKpi.TabIndex = 3;
    //        // 
    //        // pnlLoanKpi
    //        // 
    //        this.pnlLoanKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
    //        this.pnlLoanKpi.Controls.Add(this.lblLoanKpiTitle);
    //        this.pnlLoanKpi.Controls.Add(this.lblTotalLoanAmount);
    //        this.pnlLoanKpi.Controls.Add(this.lblLoanCount);
    //        this.pnlLoanKpi.Location = new System.Drawing.Point(15, 12);
    //        this.pnlLoanKpi.Name = "pnlLoanKpi";
    //        this.pnlLoanKpi.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
    //        this.pnlLoanKpi.Size = new System.Drawing.Size(260, 66);
    //        this.pnlLoanKpi.TabIndex = 0;
    //        // 
    //        // lblLoanKpiTitle
    //        // 
    //        this.lblLoanKpiTitle.AutoSize = true;
    //        this.lblLoanKpiTitle.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
    //        this.lblLoanKpiTitle.ForeColor = System.Drawing.Color.Gray;
    //        this.lblLoanKpiTitle.Location = new System.Drawing.Point(14, 8);
    //        this.lblLoanKpiTitle.Name = "lblLoanKpiTitle";
    //        this.lblLoanKpiTitle.Size = new System.Drawing.Size(183, 15);
    //        this.lblLoanKpiTitle.TabIndex = 0;
    //        this.lblLoanKpiTitle.Text = "🔴 TOTAL LOAN OUTSTANDING";
    //        // 
    //        // lblTotalLoanAmount
    //        // 
    //        this.lblTotalLoanAmount.AutoSize = true;
    //        this.lblTotalLoanAmount.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
    //        this.lblTotalLoanAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
    //        this.lblTotalLoanAmount.Location = new System.Drawing.Point(14, 26);
    //        this.lblTotalLoanAmount.Name = "lblTotalLoanAmount";
    //        this.lblTotalLoanAmount.Size = new System.Drawing.Size(118, 35);
    //        this.lblTotalLoanAmount.TabIndex = 1;
    //        this.lblTotalLoanAmount.Text = "PKR 0.00";
    //        // 
    //        // lblLoanCount
    //        // 
    //        this.lblLoanCount.AutoSize = true;
    //        this.lblLoanCount.Font = new System.Drawing.Font("Segoe UI", 8F);
    //        this.lblLoanCount.ForeColor = System.Drawing.Color.Gray;
    //        this.lblLoanCount.Location = new System.Drawing.Point(14, 50);
    //        this.lblLoanCount.Name = "lblLoanCount";
    //        this.lblLoanCount.Size = new System.Drawing.Size(84, 19);
    //        this.lblLoanCount.TabIndex = 2;
    //        this.lblLoanCount.Text = "0 customers";
    //        // 
    //        // pnlAdvanceKpi
    //        // 
    //        this.pnlAdvanceKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
    //        this.pnlAdvanceKpi.Controls.Add(this.lblAdvanceKpiTitle);
    //        this.pnlAdvanceKpi.Controls.Add(this.lblTotalAdvanceAmount);
    //        this.pnlAdvanceKpi.Controls.Add(this.lblAdvanceCount);
    //        this.pnlAdvanceKpi.Location = new System.Drawing.Point(290, 12);
    //        this.pnlAdvanceKpi.Name = "pnlAdvanceKpi";
    //        this.pnlAdvanceKpi.Size = new System.Drawing.Size(260, 66);
    //        this.pnlAdvanceKpi.TabIndex = 1;
    //        // 
    //        // lblAdvanceKpiTitle
    //        // 
    //        this.lblAdvanceKpiTitle.AutoSize = true;
    //        this.lblAdvanceKpiTitle.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
    //        this.lblAdvanceKpiTitle.ForeColor = System.Drawing.Color.Gray;
    //        this.lblAdvanceKpiTitle.Location = new System.Drawing.Point(14, 8);
    //        this.lblAdvanceKpiTitle.Name = "lblAdvanceKpiTitle";
    //        this.lblAdvanceKpiTitle.Size = new System.Drawing.Size(160, 15);
    //        this.lblAdvanceKpiTitle.TabIndex = 0;
    //        this.lblAdvanceKpiTitle.Text = "🔵 TOTAL ADVANCE CREDIT";
    //        // 
    //        // lblTotalAdvanceAmount
    //        // 
    //        this.lblTotalAdvanceAmount.AutoSize = true;
    //        this.lblTotalAdvanceAmount.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
    //        this.lblTotalAdvanceAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
    //        this.lblTotalAdvanceAmount.Location = new System.Drawing.Point(14, 26);
    //        this.lblTotalAdvanceAmount.Name = "lblTotalAdvanceAmount";
    //        this.lblTotalAdvanceAmount.Size = new System.Drawing.Size(118, 35);
    //        this.lblTotalAdvanceAmount.TabIndex = 1;
    //        this.lblTotalAdvanceAmount.Text = "PKR 0.00";
    //        // 
    //        // lblAdvanceCount
    //        // 
    //        this.lblAdvanceCount.AutoSize = true;
    //        this.lblAdvanceCount.Font = new System.Drawing.Font("Segoe UI", 8F);
    //        this.lblAdvanceCount.ForeColor = System.Drawing.Color.Gray;
    //        this.lblAdvanceCount.Location = new System.Drawing.Point(14, 50);
    //        this.lblAdvanceCount.Name = "lblAdvanceCount";
    //        this.lblAdvanceCount.Size = new System.Drawing.Size(84, 19);
    //        this.lblAdvanceCount.TabIndex = 2;
    //        this.lblAdvanceCount.Text = "0 customers";
    //        // 
    //        // pnlToolbar
    //        // 
    //        this.pnlToolbar.BackColor = System.Drawing.Color.White;
    //        this.pnlToolbar.Controls.Add(this.txtSearch);
    //        this.pnlToolbar.Controls.Add(this.rbAll);
    //        this.pnlToolbar.Controls.Add(this.rbLoan);
    //        this.pnlToolbar.Controls.Add(this.rbAdvance);
    //        this.pnlToolbar.Controls.Add(this.rbClear);
    //        this.pnlToolbar.Controls.Add(this.OpenLedgerBtn);
    //        this.pnlToolbar.Controls.Add(this.RefreshBtn);
    //        this.pnlToolbar.Controls.Add(this.lblCount);
    //        this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
    //        this.pnlToolbar.Location = new System.Drawing.Point(0, 155);
    //        this.pnlToolbar.Name = "pnlToolbar";
    //        this.pnlToolbar.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
    //        this.pnlToolbar.Size = new System.Drawing.Size(979, 50);
    //        this.pnlToolbar.TabIndex = 2;
    //        // 
    //        // txtSearch
    //        // 
    //        this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.txtSearch.Location = new System.Drawing.Point(15, 12);
    //        this.txtSearch.Name = "txtSearch";
    //        this.txtSearch.Size = new System.Drawing.Size(200, 27);
    //        this.txtSearch.TabIndex = 0;
    //        // 
    //        // rbAll
    //        // 
    //        this.rbAll.AutoSize = true;
    //        this.rbAll.Checked = true;
    //        this.rbAll.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.rbAll.Location = new System.Drawing.Point(228, 14);
    //        this.rbAll.Name = "rbAll";
    //        this.rbAll.Size = new System.Drawing.Size(48, 24);
    //        this.rbAll.TabIndex = 1;
    //        this.rbAll.TabStop = true;
    //        this.rbAll.Text = "All";
    //        // 
    //        // rbLoan
    //        // 
    //        this.rbLoan.AutoSize = true;
    //        this.rbLoan.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.rbLoan.Location = new System.Drawing.Point(272, 14);
    //        this.rbLoan.Name = "rbLoan";
    //        this.rbLoan.Size = new System.Drawing.Size(87, 24);
    //        this.rbLoan.TabIndex = 2;
    //        this.rbLoan.Text = "🔴 Loan";
    //        // 
    //        // rbAdvance
    //        // 
    //        this.rbAdvance.AutoSize = true;
    //        this.rbAdvance.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.rbAdvance.Location = new System.Drawing.Point(340, 14);
    //        this.rbAdvance.Name = "rbAdvance";
    //        this.rbAdvance.Size = new System.Drawing.Size(112, 24);
    //        this.rbAdvance.TabIndex = 3;
    //        this.rbAdvance.Text = "🔵 Advance";
    //        // 
    //        // rbClear
    //        // 
    //        this.rbClear.AutoSize = true;
    //        this.rbClear.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.rbClear.Location = new System.Drawing.Point(428, 14);
    //        this.rbClear.Name = "rbClear";
    //        this.rbClear.Size = new System.Drawing.Size(89, 24);
    //        this.rbClear.TabIndex = 4;
    //        this.rbClear.Text = "✅ Clear";
    //        // 
    //        // OpenLedgerBtn
    //        // 
    //        this.OpenLedgerBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
    //        this.OpenLedgerBtn.FlatAppearance.BorderSize = 0;
    //        this.OpenLedgerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    //        this.OpenLedgerBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.OpenLedgerBtn.ForeColor = System.Drawing.Color.White;
    //        this.OpenLedgerBtn.Location = new System.Drawing.Point(620, 10);
    //        this.OpenLedgerBtn.Name = "OpenLedgerBtn";
    //        this.OpenLedgerBtn.Size = new System.Drawing.Size(130, 30);
    //        this.OpenLedgerBtn.TabIndex = 5;
    //        this.OpenLedgerBtn.Text = "📒 Open Ledger";
    //        this.OpenLedgerBtn.UseVisualStyleBackColor = false;
    //        this.OpenLedgerBtn.Click += new System.EventHandler(this.OpenLedgerBtn_Click);
    //        // 
    //        // RefreshBtn
    //        // 
    //        this.RefreshBtn.BackColor = System.Drawing.Color.White;
    //        this.RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    //        this.RefreshBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.RefreshBtn.Location = new System.Drawing.Point(758, 10);
    //        this.RefreshBtn.Name = "RefreshBtn";
    //        this.RefreshBtn.Size = new System.Drawing.Size(90, 30);
    //        this.RefreshBtn.TabIndex = 6;
    //        this.RefreshBtn.Text = "🔄 Refresh";
    //        this.RefreshBtn.UseVisualStyleBackColor = false;
    //        this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
    //        // 
    //        // lblCount
    //        // 
    //        this.lblCount.AutoSize = true;
    //        this.lblCount.Font = new System.Drawing.Font("Segoe UI", 8F);
    //        this.lblCount.ForeColor = System.Drawing.Color.Gray;
    //        this.lblCount.Location = new System.Drawing.Point(860, 16);
    //        this.lblCount.Name = "lblCount";
    //        this.lblCount.Size = new System.Drawing.Size(84, 19);
    //        this.lblCount.TabIndex = 7;
    //        this.lblCount.Text = "0 customers";
    //        // 
    //        // BalanceGrid
    //        // 
    //        this.BalanceGrid.BackgroundColor = System.Drawing.Color.White;
    //        this.BalanceGrid.ColumnHeadersHeight = 29;
    //        this.BalanceGrid.Dock = System.Windows.Forms.DockStyle.Fill;
    //        this.BalanceGrid.Location = new System.Drawing.Point(0, 205);
    //        this.BalanceGrid.Name = "BalanceGrid";
    //        this.BalanceGrid.RowHeadersWidth = 51;
    //        this.BalanceGrid.Size = new System.Drawing.Size(979, 428);
    //        this.BalanceGrid.TabIndex = 0;
    //        // 
    //        // lblLoading
    //        // 
    //        this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
    //        this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 14F);
    //        this.lblLoading.ForeColor = System.Drawing.Color.Gray;
    //        this.lblLoading.Location = new System.Drawing.Point(0, 205);
    //        this.lblLoading.Name = "lblLoading";
    //        this.lblLoading.Size = new System.Drawing.Size(979, 428);
    //        this.lblLoading.TabIndex = 1;
    //        this.lblLoading.Text = "Loading...";
    //        this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    //        this.lblLoading.Visible = false;
    //        // 
    //        // AllCustomerBalancesForm
    //        // 
    //        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
    //        this.ClientSize = new System.Drawing.Size(979, 633);
    //        this.Controls.Add(this.BalanceGrid);
    //        this.Controls.Add(this.lblLoading);
    //        this.Controls.Add(this.pnlToolbar);
    //        this.Controls.Add(this.pnlKpi);
    //        this.Controls.Add(this.pnlHeader);
    //        this.Name = "AllCustomerBalancesForm";
    //        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
    //        this.Text = "💳 Customer Loan & Advance Balances";
    //        this.Load += new System.EventHandler(this.AllCustomerBalancesForm_Load);
    //        this.pnlHeader.ResumeLayout(false);
    //        this.pnlKpi.ResumeLayout(false);
    //        this.pnlLoanKpi.ResumeLayout(false);
    //        this.pnlLoanKpi.PerformLayout();
    //        this.pnlAdvanceKpi.ResumeLayout(false);
    //        this.pnlAdvanceKpi.PerformLayout();
    //        this.pnlToolbar.ResumeLayout(false);
    //        this.pnlToolbar.PerformLayout();
    //        ((System.ComponentModel.ISupportInitialize)(this.BalanceGrid)).EndInit();
    //        this.ResumeLayout(false);

    //    }

    //    private System.Windows.Forms.Panel pnlHeader;
    //    private System.Windows.Forms.Label lblTitle;
    //    private System.Windows.Forms.Label lblSubtitle;
    //    private System.Windows.Forms.Panel pnlKpi;
    //    private System.Windows.Forms.Panel pnlLoanKpi;
    //    private System.Windows.Forms.Label lblLoanKpiTitle;
    //    private System.Windows.Forms.Label lblTotalLoanAmount;
    //    private System.Windows.Forms.Label lblLoanCount;
    //    private System.Windows.Forms.Panel pnlAdvanceKpi;
    //    private System.Windows.Forms.Label lblAdvanceKpiTitle;
    //    private System.Windows.Forms.Label lblTotalAdvanceAmount;
    //    private System.Windows.Forms.Label lblAdvanceCount;
    //    private System.Windows.Forms.Panel pnlToolbar;
    //    private System.Windows.Forms.TextBox txtSearch;
    //    private System.Windows.Forms.RadioButton rbAll;
    //    private System.Windows.Forms.RadioButton rbLoan;
    //    private System.Windows.Forms.RadioButton rbAdvance;
    //    private System.Windows.Forms.RadioButton rbClear;
    //    private System.Windows.Forms.Button RefreshBtn;
    //    private System.Windows.Forms.Button OpenLedgerBtn;
    //    private System.Windows.Forms.Label lblCount;
    //    private System.Windows.Forms.DataGridView BalanceGrid;
    //    private System.Windows.Forms.Label lblLoading;
    //}



    //partial class AllCustomerBalancesForm
    //{
    //    private System.ComponentModel.IContainer components = null;

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing && (components != null)) components.Dispose();
    //        base.Dispose(disposing);
    //    }

    //    private void InitializeComponent()
    //    {
    //        this.pnlHeader = new System.Windows.Forms.Panel();
    //        this.lblTitle = new System.Windows.Forms.Label();
    //        this.lblSubtitle = new System.Windows.Forms.Label();
    //        this.pnlKpi = new System.Windows.Forms.Panel();
    //        this.pnlLoanKpi = new System.Windows.Forms.Panel();
    //        this.lblLoanKpiTitle = new System.Windows.Forms.Label();
    //        this.lblTotalLoanAmount = new System.Windows.Forms.Label();
    //        this.lblLoanCount = new System.Windows.Forms.Label();
    //        this.pnlAdvanceKpi = new System.Windows.Forms.Panel();
    //        this.lblAdvanceKpiTitle = new System.Windows.Forms.Label();
    //        this.lblTotalAdvanceAmount = new System.Windows.Forms.Label();
    //        this.lblAdvanceCount = new System.Windows.Forms.Label();
    //        this.pnlToolbar = new System.Windows.Forms.Panel();
    //        this.txtSearch = new System.Windows.Forms.TextBox();
    //        this.rbAll = new System.Windows.Forms.RadioButton();
    //        this.rbLoan = new System.Windows.Forms.RadioButton();
    //        this.rbAdvance = new System.Windows.Forms.RadioButton();
    //        this.rbClear = new System.Windows.Forms.RadioButton();
    //        this.ManualEntryBtn = new System.Windows.Forms.Button();
    //        this.RefreshBtn = new System.Windows.Forms.Button();
    //        this.lblCount = new System.Windows.Forms.Label();
    //        this.OpenLedgerBtn = new System.Windows.Forms.Button();
    //        this.BalanceGrid = new System.Windows.Forms.DataGridView();
    //        this.lblLoading = new System.Windows.Forms.Label();
    //        this.pnlHeader.SuspendLayout();
    //        this.pnlKpi.SuspendLayout();
    //        this.pnlLoanKpi.SuspendLayout();
    //        this.pnlAdvanceKpi.SuspendLayout();
    //        this.pnlToolbar.SuspendLayout();
    //        ((System.ComponentModel.ISupportInitialize)(this.BalanceGrid)).BeginInit();
    //        this.SuspendLayout();
    //        // 
    //        // pnlHeader
    //        // 
    //        this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
    //        this.pnlHeader.Controls.Add(this.lblTitle);
    //        this.pnlHeader.Controls.Add(this.lblSubtitle);
    //        this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
    //        this.pnlHeader.Location = new System.Drawing.Point(0, 0);
    //        this.pnlHeader.Name = "pnlHeader";
    //        this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
    //        this.pnlHeader.Size = new System.Drawing.Size(884, 65);
    //        this.pnlHeader.TabIndex = 4;
    //        // 
    //        // lblTitle
    //        // 
    //        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
    //        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
    //        this.lblTitle.ForeColor = System.Drawing.Color.White;
    //        this.lblTitle.Location = new System.Drawing.Point(20, 8);
    //        this.lblTitle.Name = "lblTitle";
    //        this.lblTitle.Size = new System.Drawing.Size(844, 34);
    //        this.lblTitle.TabIndex = 0;
    //        this.lblTitle.Text = "💳 Customer Loan & Advance Dashboard";
    //        // 
    //        // lblSubtitle
    //        // 
    //        this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Fill;
    //        this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
    //        this.lblSubtitle.Location = new System.Drawing.Point(20, 8);
    //        this.lblSubtitle.Name = "lblSubtitle";
    //        this.lblSubtitle.Size = new System.Drawing.Size(844, 49);
    //        this.lblSubtitle.TabIndex = 1;
    //        this.lblSubtitle.Text = "Double-click any customer to view full ledger";
    //        // 
    //        // pnlKpi
    //        // 
    //        this.pnlKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
    //        this.pnlKpi.Controls.Add(this.pnlLoanKpi);
    //        this.pnlKpi.Controls.Add(this.pnlAdvanceKpi);
    //        this.pnlKpi.Dock = System.Windows.Forms.DockStyle.Top;
    //        this.pnlKpi.Location = new System.Drawing.Point(0, 65);
    //        this.pnlKpi.Name = "pnlKpi";
    //        this.pnlKpi.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
    //        this.pnlKpi.Size = new System.Drawing.Size(884, 90);
    //        this.pnlKpi.TabIndex = 3;
    //        // 
    //        // pnlLoanKpi
    //        // 
    //        this.pnlLoanKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
    //        this.pnlLoanKpi.Controls.Add(this.lblLoanKpiTitle);
    //        this.pnlLoanKpi.Controls.Add(this.lblTotalLoanAmount);
    //        this.pnlLoanKpi.Controls.Add(this.lblLoanCount);
    //        this.pnlLoanKpi.Location = new System.Drawing.Point(15, 12);
    //        this.pnlLoanKpi.Name = "pnlLoanKpi";
    //        this.pnlLoanKpi.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
    //        this.pnlLoanKpi.Size = new System.Drawing.Size(260, 66);
    //        this.pnlLoanKpi.TabIndex = 0;
    //        // 
    //        // lblLoanKpiTitle
    //        // 
    //        this.lblLoanKpiTitle.AutoSize = true;
    //        this.lblLoanKpiTitle.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
    //        this.lblLoanKpiTitle.ForeColor = System.Drawing.Color.Gray;
    //        this.lblLoanKpiTitle.Location = new System.Drawing.Point(14, 8);
    //        this.lblLoanKpiTitle.Name = "lblLoanKpiTitle";
    //        this.lblLoanKpiTitle.Size = new System.Drawing.Size(183, 15);
    //        this.lblLoanKpiTitle.TabIndex = 0;
    //        this.lblLoanKpiTitle.Text = "🔴 TOTAL LOAN OUTSTANDING";
    //        // 
    //        // lblTotalLoanAmount
    //        // 
    //        this.lblTotalLoanAmount.AutoSize = true;
    //        this.lblTotalLoanAmount.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
    //        this.lblTotalLoanAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
    //        this.lblTotalLoanAmount.Location = new System.Drawing.Point(14, 26);
    //        this.lblTotalLoanAmount.Name = "lblTotalLoanAmount";
    //        this.lblTotalLoanAmount.Size = new System.Drawing.Size(118, 35);
    //        this.lblTotalLoanAmount.TabIndex = 1;
    //        this.lblTotalLoanAmount.Text = "PKR 0.00";
    //        // 
    //        // lblLoanCount
    //        // 
    //        this.lblLoanCount.AutoSize = true;
    //        this.lblLoanCount.Font = new System.Drawing.Font("Segoe UI", 8F);
    //        this.lblLoanCount.ForeColor = System.Drawing.Color.Gray;
    //        this.lblLoanCount.Location = new System.Drawing.Point(14, 50);
    //        this.lblLoanCount.Name = "lblLoanCount";
    //        this.lblLoanCount.Size = new System.Drawing.Size(84, 19);
    //        this.lblLoanCount.TabIndex = 2;
    //        this.lblLoanCount.Text = "0 customers";
    //        // 
    //        // pnlAdvanceKpi
    //        // 
    //        this.pnlAdvanceKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
    //        this.pnlAdvanceKpi.Controls.Add(this.lblAdvanceKpiTitle);
    //        this.pnlAdvanceKpi.Controls.Add(this.lblTotalAdvanceAmount);
    //        this.pnlAdvanceKpi.Controls.Add(this.lblAdvanceCount);
    //        this.pnlAdvanceKpi.Location = new System.Drawing.Point(290, 12);
    //        this.pnlAdvanceKpi.Name = "pnlAdvanceKpi";
    //        this.pnlAdvanceKpi.Size = new System.Drawing.Size(260, 66);
    //        this.pnlAdvanceKpi.TabIndex = 1;
    //        // 
    //        // lblAdvanceKpiTitle
    //        // 
    //        this.lblAdvanceKpiTitle.AutoSize = true;
    //        this.lblAdvanceKpiTitle.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
    //        this.lblAdvanceKpiTitle.ForeColor = System.Drawing.Color.Gray;
    //        this.lblAdvanceKpiTitle.Location = new System.Drawing.Point(14, 8);
    //        this.lblAdvanceKpiTitle.Name = "lblAdvanceKpiTitle";
    //        this.lblAdvanceKpiTitle.Size = new System.Drawing.Size(160, 15);
    //        this.lblAdvanceKpiTitle.TabIndex = 0;
    //        this.lblAdvanceKpiTitle.Text = "🔵 TOTAL ADVANCE CREDIT";
    //        // 
    //        // lblTotalAdvanceAmount
    //        // 
    //        this.lblTotalAdvanceAmount.AutoSize = true;
    //        this.lblTotalAdvanceAmount.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
    //        this.lblTotalAdvanceAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
    //        this.lblTotalAdvanceAmount.Location = new System.Drawing.Point(14, 26);
    //        this.lblTotalAdvanceAmount.Name = "lblTotalAdvanceAmount";
    //        this.lblTotalAdvanceAmount.Size = new System.Drawing.Size(118, 35);
    //        this.lblTotalAdvanceAmount.TabIndex = 1;
    //        this.lblTotalAdvanceAmount.Text = "PKR 0.00";
    //        // 
    //        // lblAdvanceCount
    //        // 
    //        this.lblAdvanceCount.AutoSize = true;
    //        this.lblAdvanceCount.Font = new System.Drawing.Font("Segoe UI", 8F);
    //        this.lblAdvanceCount.ForeColor = System.Drawing.Color.Gray;
    //        this.lblAdvanceCount.Location = new System.Drawing.Point(14, 50);
    //        this.lblAdvanceCount.Name = "lblAdvanceCount";
    //        this.lblAdvanceCount.Size = new System.Drawing.Size(84, 19);
    //        this.lblAdvanceCount.TabIndex = 2;
    //        this.lblAdvanceCount.Text = "0 customers";
    //        // 
    //        // pnlToolbar
    //        // 
    //        this.pnlToolbar.BackColor = System.Drawing.Color.White;
    //        this.pnlToolbar.Controls.Add(this.txtSearch);
    //        this.pnlToolbar.Controls.Add(this.rbAll);
    //        this.pnlToolbar.Controls.Add(this.rbLoan);
    //        this.pnlToolbar.Controls.Add(this.rbAdvance);
    //        this.pnlToolbar.Controls.Add(this.rbClear);
    //        this.pnlToolbar.Controls.Add(this.ManualEntryBtn);
    //        this.pnlToolbar.Controls.Add(this.RefreshBtn);
    //        this.pnlToolbar.Controls.Add(this.lblCount);
    //        this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
    //        this.pnlToolbar.Location = new System.Drawing.Point(0, 155);
    //        this.pnlToolbar.Name = "pnlToolbar";
    //        this.pnlToolbar.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
    //        this.pnlToolbar.Size = new System.Drawing.Size(884, 50);
    //        this.pnlToolbar.TabIndex = 2;
    //        // 
    //        // txtSearch
    //        // 
    //        this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.txtSearch.Location = new System.Drawing.Point(15, 12);
    //        this.txtSearch.Name = "txtSearch";
    //        this.txtSearch.Size = new System.Drawing.Size(200, 27);
    //        this.txtSearch.TabIndex = 0;
    //        // 
    //        // rbAll
    //        // 
    //        this.rbAll.AutoSize = true;
    //        this.rbAll.Checked = true;
    //        this.rbAll.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.rbAll.Location = new System.Drawing.Point(228, 14);
    //        this.rbAll.Name = "rbAll";
    //        this.rbAll.Size = new System.Drawing.Size(48, 24);
    //        this.rbAll.TabIndex = 1;
    //        this.rbAll.TabStop = true;
    //        this.rbAll.Text = "All";
    //        // 
    //        // rbLoan
    //        // 
    //        this.rbLoan.AutoSize = true;
    //        this.rbLoan.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.rbLoan.Location = new System.Drawing.Point(276, 14);
    //        this.rbLoan.Name = "rbLoan";
    //        this.rbLoan.Size = new System.Drawing.Size(87, 24);
    //        this.rbLoan.TabIndex = 2;
    //        this.rbLoan.Text = "🔴 Loan";
    //        // 
    //        // rbAdvance
    //        // 
    //        this.rbAdvance.AutoSize = true;
    //        this.rbAdvance.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.rbAdvance.Location = new System.Drawing.Point(340, 14);
    //        this.rbAdvance.Name = "rbAdvance";
    //        this.rbAdvance.Size = new System.Drawing.Size(112, 24);
    //        this.rbAdvance.TabIndex = 3;
    //        this.rbAdvance.Text = "🔵 Advance";
    //        // 
    //        // rbClear
    //        // 
    //        this.rbClear.AutoSize = true;
    //        this.rbClear.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.rbClear.Location = new System.Drawing.Point(428, 14);
    //        this.rbClear.Name = "rbClear";
    //        this.rbClear.Size = new System.Drawing.Size(89, 24);
    //        this.rbClear.TabIndex = 4;
    //        this.rbClear.Text = "✅ Clear";
    //        // 
    //        // ManualEntryBtn
    //        // 
    //        this.ManualEntryBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
    //        this.ManualEntryBtn.FlatAppearance.BorderSize = 0;
    //        this.ManualEntryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    //        this.ManualEntryBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
    //        this.ManualEntryBtn.ForeColor = System.Drawing.Color.White;
    //        this.ManualEntryBtn.Location = new System.Drawing.Point(530, 8);
    //        this.ManualEntryBtn.Name = "ManualEntryBtn";
    //        this.ManualEntryBtn.Size = new System.Drawing.Size(135, 34);
    //        this.ManualEntryBtn.TabIndex = 5;
    //        this.ManualEntryBtn.Text = "➕ Add Loan/Advance";
    //        this.ManualEntryBtn.UseVisualStyleBackColor = false;
    //        this.ManualEntryBtn.Click += new System.EventHandler(this.ManualEntryBtn_Click);
    //        // 
    //        // RefreshBtn
    //        // 
    //        this.RefreshBtn.BackColor = System.Drawing.Color.White;
    //        this.RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    //        this.RefreshBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.RefreshBtn.Location = new System.Drawing.Point(674, 10);
    //        this.RefreshBtn.Name = "RefreshBtn";
    //        this.RefreshBtn.Size = new System.Drawing.Size(90, 30);
    //        this.RefreshBtn.TabIndex = 6;
    //        this.RefreshBtn.Text = "🔄 Refresh";
    //        this.RefreshBtn.UseVisualStyleBackColor = false;
    //        this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
    //        // 
    //        // lblCount
    //        // 
    //        this.lblCount.AutoSize = true;
    //        this.lblCount.Font = new System.Drawing.Font("Segoe UI", 8F);
    //        this.lblCount.ForeColor = System.Drawing.Color.Gray;
    //        this.lblCount.Location = new System.Drawing.Point(775, 16);
    //        this.lblCount.Name = "lblCount";
    //        this.lblCount.Size = new System.Drawing.Size(84, 19);
    //        this.lblCount.TabIndex = 7;
    //        this.lblCount.Text = "0 customers";
    //        // 
    //        // OpenLedgerBtn
    //        // 
    //        this.OpenLedgerBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
    //        this.OpenLedgerBtn.FlatAppearance.BorderSize = 0;
    //        this.OpenLedgerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    //        this.OpenLedgerBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
    //        this.OpenLedgerBtn.ForeColor = System.Drawing.Color.White;
    //        this.OpenLedgerBtn.Location = new System.Drawing.Point(620, 10);
    //        this.OpenLedgerBtn.Name = "OpenLedgerBtn";
    //        this.OpenLedgerBtn.Size = new System.Drawing.Size(130, 30);
    //        this.OpenLedgerBtn.TabIndex = 0;
    //        this.OpenLedgerBtn.Text = "📒 Open Ledger";
    //        this.OpenLedgerBtn.UseVisualStyleBackColor = false;
    //        this.OpenLedgerBtn.Click += new System.EventHandler(this.OpenLedgerBtn_Click);
    //        // 
    //        // BalanceGrid
    //        // 
    //        this.BalanceGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
    //        this.BalanceGrid.BackgroundColor = System.Drawing.Color.White;
    //        this.BalanceGrid.ColumnHeadersHeight = 29;
    //        this.BalanceGrid.Dock = System.Windows.Forms.DockStyle.Fill;
    //        this.BalanceGrid.Location = new System.Drawing.Point(0, 205);
    //        this.BalanceGrid.Name = "BalanceGrid";
    //        this.BalanceGrid.RowHeadersWidth = 51;
    //        this.BalanceGrid.Size = new System.Drawing.Size(884, 428);
    //        this.BalanceGrid.TabIndex = 0;
    //        // 
    //        // lblLoading
    //        // 
    //        this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
    //        this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 14F);
    //        this.lblLoading.ForeColor = System.Drawing.Color.Gray;
    //        this.lblLoading.Location = new System.Drawing.Point(0, 205);
    //        this.lblLoading.Name = "lblLoading";
    //        this.lblLoading.Size = new System.Drawing.Size(884, 428);
    //        this.lblLoading.TabIndex = 1;
    //        this.lblLoading.Text = "Loading...";
    //        this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    //        this.lblLoading.Visible = false;
    //        // 
    //        // AllCustomerBalancesForm
    //        // 
    //        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
    //        this.ClientSize = new System.Drawing.Size(884, 633);
    //        this.Controls.Add(this.BalanceGrid);
    //        this.Controls.Add(this.lblLoading);
    //        this.Controls.Add(this.pnlToolbar);
    //        this.Controls.Add(this.pnlKpi);
    //        this.Controls.Add(this.pnlHeader);
    //        this.Name = "AllCustomerBalancesForm";
    //        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
    //        this.Text = "💳 Customer Loan & Advance Balances";
    //        this.Load += new System.EventHandler(this.AllCustomerBalancesForm_Load);
    //        this.pnlHeader.ResumeLayout(false);
    //        this.pnlKpi.ResumeLayout(false);
    //        this.pnlLoanKpi.ResumeLayout(false);
    //        this.pnlLoanKpi.PerformLayout();
    //        this.pnlAdvanceKpi.ResumeLayout(false);
    //        this.pnlAdvanceKpi.PerformLayout();
    //        this.pnlToolbar.ResumeLayout(false);
    //        this.pnlToolbar.PerformLayout();
    //        ((System.ComponentModel.ISupportInitialize)(this.BalanceGrid)).EndInit();
    //        this.ResumeLayout(false);

    //    }

    //    private System.Windows.Forms.Panel pnlHeader;
    //    private System.Windows.Forms.Label lblTitle;
    //    private System.Windows.Forms.Label lblSubtitle;
    //    private System.Windows.Forms.Panel pnlKpi;
    //    private System.Windows.Forms.Panel pnlLoanKpi;
    //    private System.Windows.Forms.Label lblLoanKpiTitle;
    //    private System.Windows.Forms.Label lblTotalLoanAmount;
    //    private System.Windows.Forms.Label lblLoanCount;
    //    private System.Windows.Forms.Panel pnlAdvanceKpi;
    //    private System.Windows.Forms.Label lblAdvanceKpiTitle;
    //    private System.Windows.Forms.Label lblTotalAdvanceAmount;
    //    private System.Windows.Forms.Label lblAdvanceCount;
    //    private System.Windows.Forms.Panel pnlToolbar;
    //    private System.Windows.Forms.TextBox txtSearch;
    //    private System.Windows.Forms.RadioButton rbAll;
    //    private System.Windows.Forms.RadioButton rbLoan;
    //    private System.Windows.Forms.RadioButton rbAdvance;
    //    private System.Windows.Forms.RadioButton rbClear;
    //    private System.Windows.Forms.Button ManualEntryBtn;
    //    private System.Windows.Forms.Button RefreshBtn;
    //    private System.Windows.Forms.Button OpenLedgerBtn;
    //    private System.Windows.Forms.Label lblCount;
    //    private System.Windows.Forms.DataGridView BalanceGrid;
    //    private System.Windows.Forms.Label lblLoading;
    //}





    partial class AllCustomerBalancesForm
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
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlKpi = new System.Windows.Forms.Panel();
            this.pnlLoanKpi = new System.Windows.Forms.Panel();
            this.lblLoanKpiTitle = new System.Windows.Forms.Label();
            this.lblTotalLoanAmount = new System.Windows.Forms.Label();
            this.lblLoanCount = new System.Windows.Forms.Label();
            this.pnlAdvanceKpi = new System.Windows.Forms.Panel();
            this.lblAdvanceKpiTitle = new System.Windows.Forms.Label();
            this.lblTotalAdvanceAmount = new System.Windows.Forms.Label();
            this.lblAdvanceCount = new System.Windows.Forms.Label();
            this.PrevBtn = new System.Windows.Forms.Button();
            this.NextBtn = new System.Windows.Forms.Button();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbLoan = new System.Windows.Forms.RadioButton();
            this.rbAdvance = new System.Windows.Forms.RadioButton();
            this.rbClear = new System.Windows.Forms.RadioButton();
            this.ManualEntryBtn = new System.Windows.Forms.Button();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.OpenLedgerBtn = new System.Windows.Forms.Button();
            this.BalanceGrid = new System.Windows.Forms.DataGridView();
            this.lblLoading = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlKpi.SuspendLayout();
            this.pnlLoanKpi.SuspendLayout();
            this.pnlAdvanceKpi.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BalanceGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.pnlHeader.Size = new System.Drawing.Size(1275, 65);
            this.pnlHeader.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1235, 34);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💳 Customer Loan & Advance Dashboard";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblSubtitle.Location = new System.Drawing.Point(20, 8);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(1235, 49);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Double-click any customer to view full ledger";
            // 
            // pnlKpi
            // 
            this.pnlKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlKpi.Controls.Add(this.pnlLoanKpi);
            this.pnlKpi.Controls.Add(this.pnlAdvanceKpi);
            this.pnlKpi.Controls.Add(this.PrevBtn);
            this.pnlKpi.Controls.Add(this.NextBtn);
            this.pnlKpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpi.Location = new System.Drawing.Point(0, 65);
            this.pnlKpi.Name = "pnlKpi";
            this.pnlKpi.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.pnlKpi.Size = new System.Drawing.Size(1275, 90);
            this.pnlKpi.TabIndex = 3;
            // 
            // pnlLoanKpi
            // 
            this.pnlLoanKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlLoanKpi.Controls.Add(this.lblLoanKpiTitle);
            this.pnlLoanKpi.Controls.Add(this.lblTotalLoanAmount);
            this.pnlLoanKpi.Controls.Add(this.lblLoanCount);
            this.pnlLoanKpi.Location = new System.Drawing.Point(15, 8);
            this.pnlLoanKpi.Name = "pnlLoanKpi";
            this.pnlLoanKpi.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlLoanKpi.Size = new System.Drawing.Size(260, 77);
            this.pnlLoanKpi.TabIndex = 0;
            // 
            // lblLoanKpiTitle
            // 
            this.lblLoanKpiTitle.AutoSize = true;
            this.lblLoanKpiTitle.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblLoanKpiTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblLoanKpiTitle.Location = new System.Drawing.Point(14, 8);
            this.lblLoanKpiTitle.Name = "lblLoanKpiTitle";
            this.lblLoanKpiTitle.Size = new System.Drawing.Size(183, 15);
            this.lblLoanKpiTitle.TabIndex = 0;
            this.lblLoanKpiTitle.Text = "🔴 TOTAL LOAN OUTSTANDING";
            // 
            // lblTotalLoanAmount
            // 
            this.lblTotalLoanAmount.AutoSize = true;
            this.lblTotalLoanAmount.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalLoanAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalLoanAmount.Location = new System.Drawing.Point(14, 26);
            this.lblTotalLoanAmount.Name = "lblTotalLoanAmount";
            this.lblTotalLoanAmount.Size = new System.Drawing.Size(118, 35);
            this.lblTotalLoanAmount.TabIndex = 1;
            this.lblTotalLoanAmount.Text = "PKR 0.00";
            // 
            // lblLoanCount
            // 
            this.lblLoanCount.AutoSize = true;
            this.lblLoanCount.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblLoanCount.ForeColor = System.Drawing.Color.Gray;
            this.lblLoanCount.Location = new System.Drawing.Point(14, 57);
            this.lblLoanCount.Name = "lblLoanCount";
            this.lblLoanCount.Size = new System.Drawing.Size(84, 19);
            this.lblLoanCount.TabIndex = 2;
            this.lblLoanCount.Text = "0 customers";
            // 
            // pnlAdvanceKpi
            // 
            this.pnlAdvanceKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.pnlAdvanceKpi.Controls.Add(this.lblAdvanceKpiTitle);
            this.pnlAdvanceKpi.Controls.Add(this.lblTotalAdvanceAmount);
            this.pnlAdvanceKpi.Controls.Add(this.lblAdvanceCount);
            this.pnlAdvanceKpi.Location = new System.Drawing.Point(290, 9);
            this.pnlAdvanceKpi.Name = "pnlAdvanceKpi";
            this.pnlAdvanceKpi.Size = new System.Drawing.Size(260, 76);
            this.pnlAdvanceKpi.TabIndex = 1;
            // 
            // lblAdvanceKpiTitle
            // 
            this.lblAdvanceKpiTitle.AutoSize = true;
            this.lblAdvanceKpiTitle.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblAdvanceKpiTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblAdvanceKpiTitle.Location = new System.Drawing.Point(14, 8);
            this.lblAdvanceKpiTitle.Name = "lblAdvanceKpiTitle";
            this.lblAdvanceKpiTitle.Size = new System.Drawing.Size(160, 15);
            this.lblAdvanceKpiTitle.TabIndex = 0;
            this.lblAdvanceKpiTitle.Text = "🔵 TOTAL ADVANCE CREDIT";
            // 
            // lblTotalAdvanceAmount
            // 
            this.lblTotalAdvanceAmount.AutoSize = true;
            this.lblTotalAdvanceAmount.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalAdvanceAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTotalAdvanceAmount.Location = new System.Drawing.Point(14, 26);
            this.lblTotalAdvanceAmount.Name = "lblTotalAdvanceAmount";
            this.lblTotalAdvanceAmount.Size = new System.Drawing.Size(118, 35);
            this.lblTotalAdvanceAmount.TabIndex = 1;
            this.lblTotalAdvanceAmount.Text = "PKR 0.00";
            // 
            // lblAdvanceCount
            // 
            this.lblAdvanceCount.AutoSize = true;
            this.lblAdvanceCount.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAdvanceCount.ForeColor = System.Drawing.Color.Gray;
            this.lblAdvanceCount.Location = new System.Drawing.Point(14, 56);
            this.lblAdvanceCount.Name = "lblAdvanceCount";
            this.lblAdvanceCount.Size = new System.Drawing.Size(84, 19);
            this.lblAdvanceCount.TabIndex = 2;
            this.lblAdvanceCount.Text = "0 customers";
            // 
            // PrevBtn
            // 
            this.PrevBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.PrevBtn.Enabled = false;
            this.PrevBtn.FlatAppearance.BorderSize = 0;
            this.PrevBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrevBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.PrevBtn.ForeColor = System.Drawing.Color.White;
            this.PrevBtn.Location = new System.Drawing.Point(1050, 59);
            this.PrevBtn.Name = "PrevBtn";
            this.PrevBtn.Size = new System.Drawing.Size(90, 26);
            this.PrevBtn.TabIndex = 8;
            this.PrevBtn.Text = "◀ Previous";
            this.PrevBtn.UseVisualStyleBackColor = false;
            this.PrevBtn.Click += new System.EventHandler(this.PrevBtn_Click);
            // 
            // NextBtn
            // 
            this.NextBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.NextBtn.Enabled = false;
            this.NextBtn.FlatAppearance.BorderSize = 0;
            this.NextBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.NextBtn.ForeColor = System.Drawing.Color.White;
            this.NextBtn.Location = new System.Drawing.Point(1165, 58);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(90, 26);
            this.NextBtn.TabIndex = 9;
            this.NextBtn.Text = "Next ▶";
            this.NextBtn.UseVisualStyleBackColor = false;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.txtSearch);
            this.pnlToolbar.Controls.Add(this.rbAll);
            this.pnlToolbar.Controls.Add(this.rbLoan);
            this.pnlToolbar.Controls.Add(this.rbAdvance);
            this.pnlToolbar.Controls.Add(this.rbClear);
            this.pnlToolbar.Controls.Add(this.ManualEntryBtn);
            this.pnlToolbar.Controls.Add(this.RefreshBtn);
            this.pnlToolbar.Controls.Add(this.lblCount);
            this.pnlToolbar.Controls.Add(this.lblPageInfo);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 155);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.pnlToolbar.Size = new System.Drawing.Size(1275, 70);
            this.pnlToolbar.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(15, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 27);
            this.txtSearch.TabIndex = 0;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbAll.Location = new System.Drawing.Point(228, 14);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(48, 24);
            this.rbAll.TabIndex = 1;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            // 
            // rbLoan
            // 
            this.rbLoan.AutoSize = true;
            this.rbLoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbLoan.Location = new System.Drawing.Point(277, 14);
            this.rbLoan.Name = "rbLoan";
            this.rbLoan.Size = new System.Drawing.Size(87, 24);
            this.rbLoan.TabIndex = 2;
            this.rbLoan.Text = "🔴 Loan";
            // 
            // rbAdvance
            // 
            this.rbAdvance.AutoSize = true;
            this.rbAdvance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbAdvance.Location = new System.Drawing.Point(340, 14);
            this.rbAdvance.Name = "rbAdvance";
            this.rbAdvance.Size = new System.Drawing.Size(112, 24);
            this.rbAdvance.TabIndex = 3;
            this.rbAdvance.Text = "🔵 Advance";
            // 
            // rbClear
            // 
            this.rbClear.AutoSize = true;
            this.rbClear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbClear.Location = new System.Drawing.Point(428, 14);
            this.rbClear.Name = "rbClear";
            this.rbClear.Size = new System.Drawing.Size(89, 24);
            this.rbClear.TabIndex = 4;
            this.rbClear.Text = "✅ Clear";
            // 
            // ManualEntryBtn
            // 
            this.ManualEntryBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.ManualEntryBtn.FlatAppearance.BorderSize = 0;
            this.ManualEntryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManualEntryBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ManualEntryBtn.ForeColor = System.Drawing.Color.White;
            this.ManualEntryBtn.Location = new System.Drawing.Point(530, 8);
            this.ManualEntryBtn.Name = "ManualEntryBtn";
            this.ManualEntryBtn.Size = new System.Drawing.Size(135, 34);
            this.ManualEntryBtn.TabIndex = 5;
            this.ManualEntryBtn.Text = "➕ Add Loan/Advance";
            this.ManualEntryBtn.UseVisualStyleBackColor = false;
            this.ManualEntryBtn.Click += new System.EventHandler(this.ManualEntryBtn_Click);
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.BackColor = System.Drawing.Color.White;
            this.RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RefreshBtn.Location = new System.Drawing.Point(674, 10);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(90, 30);
            this.RefreshBtn.TabIndex = 6;
            this.RefreshBtn.Text = "🔄 Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = false;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCount.ForeColor = System.Drawing.Color.Gray;
            this.lblCount.Location = new System.Drawing.Point(775, 16);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(84, 19);
            this.lblCount.TabIndex = 7;
            this.lblCount.Text = "0 customers";
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPageInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblPageInfo.Location = new System.Drawing.Point(730, 40);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(0, 19);
            this.lblPageInfo.TabIndex = 10;
            // 
            // OpenLedgerBtn
            // 
            this.OpenLedgerBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.OpenLedgerBtn.FlatAppearance.BorderSize = 0;
            this.OpenLedgerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenLedgerBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.OpenLedgerBtn.ForeColor = System.Drawing.Color.White;
            this.OpenLedgerBtn.Location = new System.Drawing.Point(620, 10);
            this.OpenLedgerBtn.Name = "OpenLedgerBtn";
            this.OpenLedgerBtn.Size = new System.Drawing.Size(130, 30);
            this.OpenLedgerBtn.TabIndex = 0;
            this.OpenLedgerBtn.Text = "📒 Open Ledger";
            this.OpenLedgerBtn.UseVisualStyleBackColor = false;
            this.OpenLedgerBtn.Click += new System.EventHandler(this.OpenLedgerBtn_Click);
            // 
            // BalanceGrid
            // 
            this.BalanceGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.BalanceGrid.BackgroundColor = System.Drawing.Color.White;
            this.BalanceGrid.ColumnHeadersHeight = 29;
            this.BalanceGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BalanceGrid.Location = new System.Drawing.Point(0, 225);
            this.BalanceGrid.Name = "BalanceGrid";
            this.BalanceGrid.RowHeadersWidth = 51;
            this.BalanceGrid.Size = new System.Drawing.Size(1275, 441);
            this.BalanceGrid.TabIndex = 0;
            // 
            // lblLoading
            // 
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblLoading.ForeColor = System.Drawing.Color.Gray;
            this.lblLoading.Location = new System.Drawing.Point(0, 225);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(1275, 441);
            this.lblLoading.TabIndex = 1;
            this.lblLoading.Text = "Loading...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoading.Visible = false;
            // 
            // AllCustomerBalancesForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1275, 666);
            this.Controls.Add(this.BalanceGrid);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlKpi);
            this.Controls.Add(this.pnlHeader);
            this.Name = "AllCustomerBalancesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "💳 Customer Loan & Advance Balances";
            this.Load += new System.EventHandler(this.AllCustomerBalancesForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlKpi.ResumeLayout(false);
            this.pnlLoanKpi.ResumeLayout(false);
            this.pnlLoanKpi.PerformLayout();
            this.pnlAdvanceKpi.ResumeLayout(false);
            this.pnlAdvanceKpi.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BalanceGrid)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlKpi;
        private System.Windows.Forms.Panel pnlLoanKpi;
        private System.Windows.Forms.Label lblLoanKpiTitle;
        private System.Windows.Forms.Label lblTotalLoanAmount;
        private System.Windows.Forms.Label lblLoanCount;
        private System.Windows.Forms.Panel pnlAdvanceKpi;
        private System.Windows.Forms.Label lblAdvanceKpiTitle;
        private System.Windows.Forms.Label lblTotalAdvanceAmount;
        private System.Windows.Forms.Label lblAdvanceCount;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbLoan;
        private System.Windows.Forms.RadioButton rbAdvance;
        private System.Windows.Forms.RadioButton rbClear;
        private System.Windows.Forms.Button ManualEntryBtn;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.Button OpenLedgerBtn;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button PrevBtn;
        private System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.DataGridView BalanceGrid;
        private System.Windows.Forms.Label lblLoading;
    }
}