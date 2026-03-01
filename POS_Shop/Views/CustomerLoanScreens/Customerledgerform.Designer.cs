namespace POS_Shop.Views.CustomerLoanScreens
{
    partial class CustomerLedgerForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();

            this.pnlBalance = new System.Windows.Forms.Panel();
            this.lblBalanceVal = new System.Windows.Forms.Label();
            this.lblBalanceLabel = new System.Windows.Forms.Label();
            this.btnReceivePayment = new System.Windows.Forms.Button();
            this.btnPostAdjustment = new System.Windows.Forms.Button();

            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();

            this.dgvTransactions = new System.Windows.Forms.DataGridView();

            this.pnlPager = new System.Windows.Forms.Panel();
            this.btnPrev = new System.Windows.Forms.Button();
            this.lblPager = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // ════════════════════════════════════════════════════════════════
            //  FORM
            // ════════════════════════════════════════════════════════════════
            this.Text = "Customer Ledger";
            this.Size = new System.Drawing.Size(900, 660);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MinimumSize = new System.Drawing.Size(820, 560);
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Load += new System.EventHandler(this.CustomerLedgerForm_Load);

            // ════════════════════════════════════════════════════════════════
            //  HEADER
            // ════════════════════════════════════════════════════════════════
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 64;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Height = 28;
            this.lblTitle.Text = "📒  Customer Ledger";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;

            this.lblCustomerName.AutoSize = false;
            this.lblCustomerName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomerName.Height = 20;
            this.lblCustomerName.Text = "";
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(180, 210, 255);

            this.pnlHeader.Controls.Add(this.lblCustomerName);
            this.pnlHeader.Controls.Add(this.lblTitle);

            // ════════════════════════════════════════════════════════════════
            //  BALANCE BANNER
            // ════════════════════════════════════════════════════════════════
            this.pnlBalance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBalance.Height = 64;
            this.pnlBalance.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);

            this.lblBalanceVal.AutoSize = false;
            this.lblBalanceVal.Location = new System.Drawing.Point(16, 8);
            this.lblBalanceVal.Size = new System.Drawing.Size(200, 30);
            this.lblBalanceVal.Text = "Rs. 0";
            this.lblBalanceVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);

            this.lblBalanceLabel.AutoSize = false;
            this.lblBalanceLabel.Location = new System.Drawing.Point(220, 18);
            this.lblBalanceLabel.Size = new System.Drawing.Size(300, 22);
            this.lblBalanceLabel.Text = "";
            this.lblBalanceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBalanceLabel.ForeColor = System.Drawing.Color.Gray;

            this.btnReceivePayment.Location = new System.Drawing.Point(560, 12);
            this.btnReceivePayment.Size = new System.Drawing.Size(150, 38);
            this.btnReceivePayment.Text = "💳  Receive Payment";
            this.btnReceivePayment.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnReceivePayment.ForeColor = System.Drawing.Color.White;
            this.btnReceivePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceivePayment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReceivePayment.FlatAppearance.BorderSize = 0;
            this.btnReceivePayment.Click += new System.EventHandler(this.btnReceivePayment_Click);

            this.btnPostAdjustment.Location = new System.Drawing.Point(720, 12);
            this.btnPostAdjustment.Size = new System.Drawing.Size(140, 38);
            this.btnPostAdjustment.Text = "⚙  Adjustment";
            this.btnPostAdjustment.BackColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnPostAdjustment.ForeColor = System.Drawing.Color.White;
            this.btnPostAdjustment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPostAdjustment.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPostAdjustment.FlatAppearance.BorderSize = 0;
            this.btnPostAdjustment.Click += new System.EventHandler(this.btnPostAdjustment_Click);

            this.pnlBalance.Controls.Add(this.lblBalanceVal);
            this.pnlBalance.Controls.Add(this.lblBalanceLabel);
            this.pnlBalance.Controls.Add(this.btnReceivePayment);
            this.pnlBalance.Controls.Add(this.btnPostAdjustment);

            // ════════════════════════════════════════════════════════════════
            //  FILTER BAR
            // ════════════════════════════════════════════════════════════════
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Height = 48;
            this.pnlFilters.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);

            this.lblFrom.AutoSize = false;
            this.lblFrom.Location = new System.Drawing.Point(16, 14);
            this.lblFrom.Size = new System.Drawing.Size(70, 22);
            this.lblFrom.Text = "From:";
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dtpFrom.Location = new System.Drawing.Point(90, 11);
            this.dtpFrom.Size = new System.Drawing.Size(140, 26);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblTo.AutoSize = false;
            this.lblTo.Location = new System.Drawing.Point(240, 14);
            this.lblTo.Size = new System.Drawing.Size(30, 22);
            this.lblTo.Text = "To:";
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dtpTo.Location = new System.Drawing.Point(275, 11);
            this.dtpTo.Size = new System.Drawing.Size(140, 26);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.btnSearch.Location = new System.Drawing.Point(425, 9);
            this.btnSearch.Size = new System.Drawing.Size(90, 30);
            this.btnSearch.Text = "🔍  Search";
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.pnlFilters.Controls.Add(this.lblFrom);
            this.pnlFilters.Controls.Add(this.dtpFrom);
            this.pnlFilters.Controls.Add(this.lblTo);
            this.pnlFilters.Controls.Add(this.dtpTo);
            this.pnlFilters.Controls.Add(this.btnSearch);

            // ════════════════════════════════════════════════════════════════
            //  TRANSACTION GRID
            // ════════════════════════════════════════════════════════════════
            this.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.RowHeadersVisible = false;
            this.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.GridColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvTransactions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransactions.RowTemplate.Height = 28;
            this.dgvTransactions.ColumnHeadersHeight = 34;

            // Header style
            var hdrStyle = this.dgvTransactions.ColumnHeadersDefaultCellStyle;
            hdrStyle.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            hdrStyle.ForeColor = System.Drawing.Color.White;
            hdrStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            hdrStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvTransactions.EnableHeadersVisualStyles = false;

            // Alternating rows
            this.dgvTransactions.AlternatingRowsDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(248, 249, 252);

            // Columns: Date | Type | Order# | Debit | Credit | Balance | Notes
            AddCol(dgvTransactions, "Date", "Date", 90, false);
            AddCol(dgvTransactions, "Type", "Type", 120, false);
            AddCol(dgvTransactions, "OrderNo", "Order #", 70, false);
            AddCol(dgvTransactions, "Debit", "Debit (↑)", 100, false);
            AddCol(dgvTransactions, "Credit", "Credit (↓)", 100, false);
            AddCol(dgvTransactions, "Balance", "Balance", 110, false);
            AddCol(dgvTransactions, "Notes", "Notes", 200, true);

            // ════════════════════════════════════════════════════════════════
            //  PAGER
            // ════════════════════════════════════════════════════════════════
            this.pnlPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPager.Height = 52;
            this.pnlPager.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);

            this.btnPrev.Location = new System.Drawing.Point(16, 10);
            this.btnPrev.Size = new System.Drawing.Size(100, 32);
            this.btnPrev.Text = "◀  Previous";
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.btnPrev.ForeColor = System.Drawing.Color.White;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);

            this.lblPager.AutoSize = false;
            this.lblPager.Location = new System.Drawing.Point(130, 18);
            this.lblPager.Size = new System.Drawing.Size(520, 20);
            this.lblPager.Text = "";
            this.lblPager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPager.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.btnNext.Location = new System.Drawing.Point(660, 10);
            this.btnNext.Size = new System.Drawing.Size(100, 32);
            this.btnNext.Text = "Next  ▶";
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            this.btnClose.Location = new System.Drawing.Point(776, 10);
            this.btnClose.Size = new System.Drawing.Size(96, 32);
            this.btnClose.Text = "Close";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.pnlPager.Controls.Add(this.btnPrev);
            this.pnlPager.Controls.Add(this.lblPager);
            this.pnlPager.Controls.Add(this.btnNext);
            this.pnlPager.Controls.Add(this.btnClose);

            // ════════════════════════════════════════════════════════════════
            //  ASSEMBLE
            // ════════════════════════════════════════════════════════════════
            this.Controls.Add(this.dgvTransactions);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlBalance);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlPager);

            this.ResumeLayout(false);
        }

        private static void AddCol(System.Windows.Forms.DataGridView dgv,
            string name, string header, int minWidth, bool fill)
        {
            var col = new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                MinimumWidth = minWidth,
                SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            };
            if (!fill) col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            col.Width = minWidth;
            dgv.Columns.Add(col);
        }

        // ── Control declarations ──────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Panel pnlBalance;
        private System.Windows.Forms.Label lblBalanceVal;
        private System.Windows.Forms.Label lblBalanceLabel;
        private System.Windows.Forms.Button btnReceivePayment;
        private System.Windows.Forms.Button btnPostAdjustment;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Panel pnlPager;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label lblPager;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnClose;
    }
}