namespace POS_Shop.Views.Controllers.Reports
{
    partial class ProductPurchaseHistoryReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
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
            this.colPHProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPHCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPHTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPHQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPHSpend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPHMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPHMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPHAvg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPHLast = new System.Windows.Forms.DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────────────────────
            this.Text = "Report — Product Purchase History";
            this.Size = new System.Drawing.Size(1280, 700);
            this.MinimumSize = new System.Drawing.Size(1050, 560);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.Font = new System.Drawing.Font("Segoe UI", 9f);

            // ── Header ───────────────────────────────────────────────────────────
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 64;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(109, 76, 65);
            this.lblTitle.Text = "Product Purchase History";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16f, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(16, 10);
            this.lblSub.Text = "What products you bought  ·  How many times  ·  At what prices  ·  ⚠ Red Max = >20% price variance";
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(215, 204, 200);
            this.lblSub.AutoSize = true;
            this.lblSub.Location = new System.Drawing.Point(18, 40);
            this.pnlHeader.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblTitle, this.lblSub });

            // ── Filter Panel ─────────────────────────────────────────────────────
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 56;
            this.pnlFilter.BackColor = System.Drawing.Color.White;

            this.lblFromCap.Text = "FROM";
            this.lblFromCap.Font = new System.Drawing.Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
            this.lblFromCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblFromCap.AutoSize = true;
            this.lblFromCap.Location = new System.Drawing.Point(14, 8);

            this.dtpFrom.Location = new System.Drawing.Point(14, 24);
            this.dtpFrom.Size = new System.Drawing.Size(160, 28);
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblToCap.Text = "TO";
            this.lblToCap.Font = new System.Drawing.Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
            this.lblToCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblToCap.AutoSize = true;
            this.lblToCap.Location = new System.Drawing.Point(188, 8);

            this.dtpTo.Location = new System.Drawing.Point(188, 24);
            this.dtpTo.Size = new System.Drawing.Size(160, 28);
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.btnRun.Text = "▶  Run Report";
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(109, 76, 65);
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Size = new System.Drawing.Size(150, 34);
            this.btnRun.Location = new System.Drawing.Point(362, 12);

            this.btnPrint.Text = "🖨  Print";
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(80, 100, 110);
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Size = new System.Drawing.Size(110, 34);
            this.btnPrint.Location = new System.Drawing.Point(522, 12);

            this.btnClose.Text = "Close";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Size = new System.Drawing.Size(110, 34);
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(1140, 12);

            this.pnlFilter.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFromCap, this.dtpFrom, this.lblToCap, this.dtpTo,
                this.btnRun, this.btnPrint, this.btnClose });

            // ── Grid Panel ───────────────────────────────────────────────────────
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(14, 0, 14, 14);

            this.lblBar.Text = "  Run report to see results";
            this.lblBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBar.Height = 34;
            this.lblBar.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.lblBar.ForeColor = System.Drawing.Color.White;
            this.lblBar.BackColor = System.Drawing.Color.FromArgb(109, 76, 65);
            this.lblBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblEmpty.Text = "No product purchases found.";
            this.lblEmpty.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Italic);
            this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;

            // ── DataGridView ─────────────────────────────────────────────────────
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.ReadOnly = true;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReport.GridColor = System.Drawing.Color.FromArgb(236, 239, 241);

            // Column header style
            this.dgvReport.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(109, 76, 65);
            this.dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvReport.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(109, 76, 65);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvReport.ColumnHeadersHeight = 40;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Default cell style
            this.dgvReport.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.dgvReport.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(215, 204, 200);
            this.dgvReport.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(78, 52, 46);

            // Alternating row style
            this.dgvReport.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(251, 249, 248);

            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.MultiSelect = false;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowTemplate.Height = 36;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // ── Columns ──────────────────────────────────────────────────────────
            this.colPHProduct.Name = "colPHProduct";
            this.colPHProduct.HeaderText = "Product Name";
            this.colPHProduct.FillWeight = 22f;

            this.colPHCode.Name = "colPHCode";
            this.colPHCode.HeaderText = "Code";
            this.colPHCode.FillWeight = 10f;

            this.colPHTimes.Name = "colPHTimes";
            this.colPHTimes.HeaderText = "Orders";
            this.colPHTimes.FillWeight = 6f;
            this.colPHTimes.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPHTimes.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            this.colPHQty.Name = "colPHQty";
            this.colPHQty.HeaderText = "Total Qty";
            this.colPHQty.FillWeight = 8f;
            this.colPHQty.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPHQty.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            this.colPHSpend.Name = "colPHSpend";
            this.colPHSpend.HeaderText = "Total Spend";
            this.colPHSpend.FillWeight = 13f;
            this.colPHSpend.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPHSpend.DefaultCellStyle.Format = "N2";
            this.colPHSpend.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.colPHSpend.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(109, 76, 65);

            this.colPHMin.Name = "colPHMin";
            this.colPHMin.HeaderText = "Min Price";
            this.colPHMin.FillWeight = 11f;
            this.colPHMin.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPHMin.DefaultCellStyle.Format = "N2";
            this.colPHMin.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            this.colPHMax.Name = "colPHMax";
            this.colPHMax.HeaderText = "Max Price ⚠";
            this.colPHMax.FillWeight = 11f;
            this.colPHMax.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPHMax.DefaultCellStyle.Format = "N2";
            this.colPHMax.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            this.colPHAvg.Name = "colPHAvg";
            this.colPHAvg.HeaderText = "Avg Price";
            this.colPHAvg.FillWeight = 11f;
            this.colPHAvg.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPHAvg.DefaultCellStyle.Format = "N2";
            this.colPHAvg.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            this.colPHLast.Name = "colPHLast";
            this.colPHLast.HeaderText = "Last Ordered";
            this.colPHLast.FillWeight = 11f;

            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colPHProduct, this.colPHCode, this.colPHTimes, this.colPHQty,
                this.colPHSpend, this.colPHMin, this.colPHMax, this.colPHAvg, this.colPHLast });

            this.pnlGrid.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvReport, this.lblEmpty, this.lblBar });

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pnlGrid, this.pnlFilter, this.pnlHeader });

            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }

        // Fields
        private System.Windows.Forms.Panel pnlHeader, pnlFilter, pnlGrid;
        private System.Windows.Forms.Label lblTitle, lblSub, lblFromCap, lblToCap, lblBar, lblEmpty;
        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
        private System.Windows.Forms.Button btnRun, btnPrint, btnClose;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPHProduct, colPHCode, colPHTimes, colPHQty, colPHSpend, colPHMin, colPHMax, colPHAvg, colPHLast;
    }
}