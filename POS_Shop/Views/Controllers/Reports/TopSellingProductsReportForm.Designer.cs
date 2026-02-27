namespace POS_Shop.Views.Controllers.Reports
{
    partial class TopSellingProductsReportForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblSortCap = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.lblFromCap = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblToCap = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.lblBar = new System.Windows.Forms.Label();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.colTPRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTPName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTPCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTPQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTPOrders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTPRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTPAvgPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTPShare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 105, 92);
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 64;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 10);
            this.lblTitle.Text = "Top Selling Products";
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(178, 223, 219);
            this.lblSub.Location = new System.Drawing.Point(18, 42);
            this.lblSub.Text = "Products ranked by revenue or quantity  ·  Revenue share  ·  Average selling price";
            // pnlFilter
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.btnClose);
            this.pnlFilter.Controls.Add(this.btnPrint);
            this.pnlFilter.Controls.Add(this.btnRun);
            this.pnlFilter.Controls.Add(this.dtpTo);
            this.pnlFilter.Controls.Add(this.lblToCap);
            this.pnlFilter.Controls.Add(this.dtpFrom);
            this.pnlFilter.Controls.Add(this.lblFromCap);
            this.pnlFilter.Controls.Add(this.cmbSort);
            this.pnlFilter.Controls.Add(this.lblSortCap);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 56;
            this.lblSortCap.AutoSize = true;
            this.lblSortCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSortCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblSortCap.Location = new System.Drawing.Point(14, 8);
            this.lblSortCap.Text = "RANK BY";
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSort.Items.AddRange(new object[] { "By Revenue", "By Quantity" });
            this.cmbSort.Location = new System.Drawing.Point(14, 24);
            this.cmbSort.Size = new System.Drawing.Size(130, 28);
            this.lblFromCap.AutoSize = true;
            this.lblFromCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblFromCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblFromCap.Location = new System.Drawing.Point(158, 8);
            this.lblFromCap.Text = "FROM";
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(158, 24);
            this.dtpFrom.Size = new System.Drawing.Size(160, 28);
            this.lblToCap.AutoSize = true;
            this.lblToCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblToCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblToCap.Location = new System.Drawing.Point(332, 8);
            this.lblToCap.Text = "TO";
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(332, 24);
            this.dtpTo.Size = new System.Drawing.Size(160, 28);
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(0, 105, 92);
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(506, 12);
            this.btnRun.Size = new System.Drawing.Size(150, 34);
            this.btnRun.Text = "Run Report";
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(80, 100, 110);
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(666, 12);
            this.btnPrint.Size = new System.Drawing.Size(110, 34);
            this.btnPrint.Text = "Print";
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1040, 12);
            this.btnClose.Size = new System.Drawing.Size(110, 34);
            this.btnClose.Text = "Close";
            // pnlGrid
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvReport);
            this.pnlGrid.Controls.Add(this.lblEmpty);
            this.pnlGrid.Controls.Add(this.lblBar);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(14, 0, 14, 14);
            this.lblBar.BackColor = System.Drawing.Color.FromArgb(0, 105, 92);
            this.lblBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBar.ForeColor = System.Drawing.Color.White;
            this.lblBar.Height = 34;
            this.lblBar.Text = "  Run report to see results";
            this.lblBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmpty.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblEmpty.Text = "No sales found in the selected period.";
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            // dgvReport
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReport.ColumnHeadersHeight = 40;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colTPRank, this.colTPName, this.colTPCode, this.colTPQty, this.colTPOrders, this.colTPRevenue, this.colTPAvgPrice, this.colTPShare });
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.GridColor = System.Drawing.Color.FromArgb(236, 239, 241);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowTemplate.Height = 36;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // Columns
            this.colTPRank.FillWeight = 4F; this.colTPRank.HeaderText = "#"; this.colTPRank.Name = "colTPRank";
            this.colTPName.FillWeight = 24F; this.colTPName.HeaderText = "Product"; this.colTPName.Name = "colTPName";
            this.colTPCode.FillWeight = 9F; this.colTPCode.HeaderText = "Code"; this.colTPCode.Name = "colTPCode";
            this.colTPQty.FillWeight = 9F; this.colTPQty.HeaderText = "Qty Sold"; this.colTPQty.Name = "colTPQty";
            this.colTPOrders.FillWeight = 8F; this.colTPOrders.HeaderText = "Orders"; this.colTPOrders.Name = "colTPOrders";
            this.colTPRevenue.FillWeight = 16F; this.colTPRevenue.HeaderText = "Revenue (Rs.)"; this.colTPRevenue.Name = "colTPRevenue";
            this.colTPAvgPrice.FillWeight = 14F; this.colTPAvgPrice.HeaderText = "Avg Price (Rs.)"; this.colTPAvgPrice.Name = "colTPAvgPrice";
            this.colTPShare.FillWeight = 9F; this.colTPShare.HeaderText = "Share %"; this.colTPShare.Name = "colTPShare";
            // Form
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 520);
            this.Size = new System.Drawing.Size(1180, 680);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report - Top Selling Products";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader, pnlFilter, pnlGrid;
        private System.Windows.Forms.Label lblTitle, lblSub, lblSortCap, lblFromCap, lblToCap, lblBar, lblEmpty;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
        private System.Windows.Forms.Button btnRun, btnPrint, btnClose;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTPRank, colTPName, colTPCode, colTPQty, colTPOrders, colTPRevenue, colTPAvgPrice, colTPShare;
    }
}