namespace POS_Shop.Views.Controllers.Supplier
{
    partial class PendingPurchasesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvPurchases = new System.Windows.Forms.DataGridView();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();

            this.pnlTop.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).BeginInit();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();

            // ── Form ──────────────────────────────────────────────────────────
            this.Text = "Pending & Partially Paid Purchases";
            this.Size = new System.Drawing.Size(1250, 740);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.BackColor = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.PendingPurchasesForm_Load);

            // ── Top title bar ─────────────────────────────────────────────────
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 55;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(46, 64, 87);

            this.lblTitle.Text = "⚠  Pending & Partially Paid Purchases";
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 14);
            this.pnlTop.Controls.Add(this.lblTitle);

            // ── Filter bar ────────────────────────────────────────────────────
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 52;
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(240, 245, 255);

            this.lblSearch.Text = "Search:";
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(15, 16);

            this.txtSearch.Location = new System.Drawing.Point(75, 13);
            this.txtSearch.Size = new System.Drawing.Size(250, 26);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.lblFilter.Text = "Status:";
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(345, 16);

            this.cmbStatusFilter.Location = new System.Drawing.Point(400, 13);
            this.cmbStatusFilter.Size = new System.Drawing.Size(165, 26);
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Items.AddRange(new object[] { "All", "Pending", "Partially Paid" });
            this.cmbStatusFilter.SelectedIndex = 0;
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.cmbStatusFilter_SelectedIndexChanged);

            this.btnRefresh.Text = "↻  Refresh";
            this.btnRefresh.Location = new System.Drawing.Point(582, 11);
            this.btnRefresh.Size = new System.Drawing.Size(95, 30);
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(23, 162, 184);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.pnlFilter.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSearch, this.txtSearch,
                this.lblFilter, this.cmbStatusFilter,
                this.btnRefresh
            });

            // ── DataGridView ──────────────────────────────────────────────────
            this.dgvPurchases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchases.Margin = new System.Windows.Forms.Padding(0);

            // ── Bottom button bar ─────────────────────────────────────────────
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Height = 58;
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(240, 245, 255);

            this.btnSelectAll.Text = "☑  Select All";
            this.btnSelectAll.Size = new System.Drawing.Size(120, 34);
            this.btnSelectAll.Location = new System.Drawing.Point(15, 12);
            this.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnSelectAll.ForeColor = System.Drawing.Color.White;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.FlatAppearance.BorderSize = 0;
            this.btnSelectAll.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnSelectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);

            this.btnDeselectAll.Text = "☐  Deselect All";
            this.btnDeselectAll.Size = new System.Drawing.Size(125, 34);
            this.btnDeselectAll.Location = new System.Drawing.Point(145, 12);
            this.btnDeselectAll.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnDeselectAll.ForeColor = System.Drawing.Color.White;
            this.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeselectAll.FlatAppearance.BorderSize = 0;
            this.btnDeselectAll.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnDeselectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);

            this.btnExport.Text = "📥  Export to Excel";
            this.btnExport.Size = new System.Drawing.Size(165, 34);
            this.btnExport.Location = new System.Drawing.Point(285, 12);
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            this.pnlButtons.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnSelectAll, this.btnDeselectAll, this.btnExport
            });

            // ── Status strip ──────────────────────────────────────────────────
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Height = 28;
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(46, 64, 87);

            this.lblStatus.Text = "Ready";
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 6);
            this.pnlStatus.Controls.Add(this.lblStatus);

            // ── Assemble (order matters for DockStyle) ────────────────────────
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvPurchases,   // Fill — must be added before docked panels
                this.pnlStatus,
                this.pnlButtons,
                this.pnlFilter,
                this.pnlTop
            });

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).EndInit();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.ResumeLayout(false);
        }

        // ── Field declarations ────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridView dgvPurchases;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatus;
    }
}