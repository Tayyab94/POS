namespace POS_Shop.Views.CustomerLoanScreens
{
    partial class CustomerLedgerReportForm
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
            this.lblSubtitle = new System.Windows.Forms.Label();

            this.pnlFilters = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkOnlyBalance = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReceivePayment = new System.Windows.Forms.Button();

            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblSummaryLoan = new System.Windows.Forms.Label();
            this.lblSummaryAdvance = new System.Windows.Forms.Label();

            this.dgvCustomers = new System.Windows.Forms.DataGridView();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // ════════════════════════════════════════════════════════════════
            //  FORM
            // ════════════════════════════════════════════════════════════════
            this.Text = "Customer Ledger Report";
            this.Size = new System.Drawing.Size(860, 640);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(740, 520);
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Load += new System.EventHandler(this.CustomerLedgerReportForm_Load);

            // ════════════════════════════════════════════════════════════════
            //  HEADER
            // ════════════════════════════════════════════════════════════════
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 68;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Height = 30;
            this.lblTitle.Text = "📊  Customer Ledger Report";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubtitle.Height = 22;
            this.lblSubtitle.Text = "All customers — outstanding loans & advance balances";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 210, 255);

            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);

            // ════════════════════════════════════════════════════════════════
            //  FILTER BAR
            // ════════════════════════════════════════════════════════════════
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Height = 52;
            this.pnlFilters.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);

            this.txtSearch.Location = new System.Drawing.Point(16, 13);
            this.txtSearch.Size = new System.Drawing.Size(240, 26);
            //this.txtSearch.PlaceholderText = "Search customer or phone…";
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);

            this.chkOnlyBalance.Location = new System.Drawing.Point(268, 15);
            this.chkOnlyBalance.Size = new System.Drawing.Size(170, 22);
            this.chkOnlyBalance.Text = "Only with balance";
            this.chkOnlyBalance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkOnlyBalance.CheckedChanged += new System.EventHandler(this.chkOnlyBalance_CheckedChanged);

            this.btnSearch.Location = new System.Drawing.Point(444, 10);
            this.btnSearch.Size = new System.Drawing.Size(90, 32);
            this.btnSearch.Text = "🔍  Search";
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.btnReceivePayment.Location = new System.Drawing.Point(544, 10);
            this.btnReceivePayment.Size = new System.Drawing.Size(155, 32);
            this.btnReceivePayment.Text = "💳  Receive Payment";
            this.btnReceivePayment.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnReceivePayment.ForeColor = System.Drawing.Color.White;
            this.btnReceivePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceivePayment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReceivePayment.FlatAppearance.BorderSize = 0;
            this.btnReceivePayment.Click += new System.EventHandler(this.btnReceivePayment_Click);

            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Controls.Add(this.chkOnlyBalance);
            this.pnlFilters.Controls.Add(this.btnSearch);
            this.pnlFilters.Controls.Add(this.btnReceivePayment);

            // ════════════════════════════════════════════════════════════════
            //  SUMMARY STRIP
            // ════════════════════════════════════════════════════════════════
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Height = 36;
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(255, 248, 225);
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);

            this.lblSummaryLoan.AutoSize = false;
            this.lblSummaryLoan.Location = new System.Drawing.Point(16, 8);
            this.lblSummaryLoan.Size = new System.Drawing.Size(380, 22);
            this.lblSummaryLoan.Text = "";
            this.lblSummaryLoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSummaryLoan.ForeColor = System.Drawing.Color.FromArgb(198, 40, 40);

            this.lblSummaryAdvance.AutoSize = false;
            this.lblSummaryAdvance.Location = new System.Drawing.Point(410, 8);
            this.lblSummaryAdvance.Size = new System.Drawing.Size(380, 22);
            this.lblSummaryAdvance.Text = "";
            this.lblSummaryAdvance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSummaryAdvance.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);

            this.pnlSummary.Controls.Add(this.lblSummaryLoan);
            this.pnlSummary.Controls.Add(this.lblSummaryAdvance);

            // ════════════════════════════════════════════════════════════════
            //  CUSTOMER GRID
            // ════════════════════════════════════════════════════════════════
            this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomers.GridColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvCustomers.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.RowTemplate.Height = 30;
            this.dgvCustomers.ColumnHeadersHeight = 36;
            this.dgvCustomers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(
                                                        this.dgvCustomers_CellDoubleClick);

            var hdr = this.dgvCustomers.ColumnHeadersDefaultCellStyle;
            hdr.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            hdr.ForeColor = System.Drawing.Color.White;
            hdr.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            hdr.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvCustomers.EnableHeadersVisualStyles = false;
            this.dgvCustomers.AlternatingRowsDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(248, 249, 252);

            // Columns
            var c1 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Customer", MinimumWidth = 180 };
            var c2 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Phone", HeaderText = "Phone", MinimumWidth = 120 };
            var c3 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "City", HeaderText = "City", MinimumWidth = 100 };
            var c4 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Balance", HeaderText = "Balance", MinimumWidth = 140 };
            var c5 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "LastTx", HeaderText = "Last Transaction", MinimumWidth = 130 };
            c4.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            foreach (var c in new[] { c1, c2, c3, c4, c5 })
                c.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomers.Columns.AddRange(c1, c2, c3, c4, c5);

            // ════════════════════════════════════════════════════════════════
            //  FOOTER
            // ════════════════════════════════════════════════════════════════
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 48;
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);

            this.lblHint.AutoSize = false;
            this.lblHint.Location = new System.Drawing.Point(16, 14);
            this.lblHint.Size = new System.Drawing.Size(600, 20);
            this.lblHint.Text = "💡  Double-click any customer to view full ledger history";
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblHint.ForeColor = System.Drawing.Color.Gray;

            this.btnClose.Location = new System.Drawing.Point(740, 8);
            this.btnClose.Size = new System.Drawing.Size(96, 32);
            this.btnClose.Text = "Close";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.pnlFooter.Controls.Add(this.lblHint);
            this.pnlFooter.Controls.Add(this.btnClose);

            // ════════════════════════════════════════════════════════════════
            //  ASSEMBLE
            // ════════════════════════════════════════════════════════════════
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);

            this.ResumeLayout(false);
        }

        // ── Controls ─────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyBalance;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReceivePayment;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblSummaryLoan;
        private System.Windows.Forms.Label lblSummaryAdvance;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnClose;
    }
}