namespace POS_Shop.Views.Controllers.Reports
{
    partial class ProfitLossReportForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblToCap = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFromCap = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.colPLPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPLOrders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPLRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPLInv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPLCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPLGross = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPLMargin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.lblBar = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(20)))), ((int)(((byte)(140)))));
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1162, 64);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(147)))), ((int)(((byte)(216)))));
            this.lblSub.Location = new System.Drawing.Point(18, 42);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(454, 20);
            this.lblSub.TabIndex = 0;
            this.lblSub.Text = "Sales Revenue vs Purchase Cost per month  ·  Gross Profit & Margin %";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(256, 37);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Profit & Loss Report";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.btnClose);
            this.pnlFilter.Controls.Add(this.btnPrint);
            this.pnlFilter.Controls.Add(this.btnRun);
            this.pnlFilter.Controls.Add(this.dtpTo);
            this.pnlFilter.Controls.Add(this.lblToCap);
            this.pnlFilter.Controls.Add(this.dtpFrom);
            this.pnlFilter.Controls.Add(this.lblFromCap);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 64);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1162, 56);
            this.pnlFilter.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(2002, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 34);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(522, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 34);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(20)))), ((int)(((byte)(140)))));
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(362, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(150, 34);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Run Report";
            this.btnRun.UseVisualStyleBackColor = false;
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(188, 24);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(160, 30);
            this.dtpTo.TabIndex = 3;
            // 
            // lblToCap
            // 
            this.lblToCap.AutoSize = true;
            this.lblToCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblToCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblToCap.Location = new System.Drawing.Point(188, 8);
            this.lblToCap.Name = "lblToCap";
            this.lblToCap.Size = new System.Drawing.Size(26, 17);
            this.lblToCap.TabIndex = 4;
            this.lblToCap.Text = "TO";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(14, 24);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(160, 30);
            this.dtpFrom.TabIndex = 5;
            // 
            // lblFromCap
            // 
            this.lblFromCap.AutoSize = true;
            this.lblFromCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblFromCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblFromCap.Location = new System.Drawing.Point(14, 8);
            this.lblFromCap.Name = "lblFromCap";
            this.lblFromCap.Size = new System.Drawing.Size(45, 17);
            this.lblFromCap.TabIndex = 6;
            this.lblFromCap.Text = "FROM";
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvReport);
            this.pnlGrid.Controls.Add(this.lblEmpty);
            this.pnlGrid.Controls.Add(this.lblBar);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 120);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(14, 0, 14, 14);
            this.pnlGrid.Size = new System.Drawing.Size(1162, 513);
            this.pnlGrid.TabIndex = 0;
            // 
            // dgvReport
            // 
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
            this.colPLPeriod,
            this.colPLOrders,
            this.colPLRevenue,
            this.colPLInv,
            this.colPLCost,
            this.colPLGross,
            this.colPLMargin});
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.dgvReport.Location = new System.Drawing.Point(14, 34);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.RowTemplate.Height = 36;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1134, 465);
            this.dgvReport.TabIndex = 0;
            // 
            // colPLPeriod
            // 
            this.colPLPeriod.FillWeight = 16F;
            this.colPLPeriod.HeaderText = "Period";
            this.colPLPeriod.MinimumWidth = 6;
            this.colPLPeriod.Name = "colPLPeriod";
            this.colPLPeriod.ReadOnly = true;
            // 
            // colPLOrders
            // 
            this.colPLOrders.FillWeight = 8F;
            this.colPLOrders.HeaderText = "Sales Orders";
            this.colPLOrders.MinimumWidth = 6;
            this.colPLOrders.Name = "colPLOrders";
            this.colPLOrders.ReadOnly = true;
            // 
            // colPLRevenue
            // 
            this.colPLRevenue.FillWeight = 16F;
            this.colPLRevenue.HeaderText = "Revenue (Rs.)";
            this.colPLRevenue.MinimumWidth = 6;
            this.colPLRevenue.Name = "colPLRevenue";
            this.colPLRevenue.ReadOnly = true;
            // 
            // colPLInv
            // 
            this.colPLInv.FillWeight = 8F;
            this.colPLInv.HeaderText = "Purchase Inv.";
            this.colPLInv.MinimumWidth = 6;
            this.colPLInv.Name = "colPLInv";
            this.colPLInv.ReadOnly = true;
            // 
            // colPLCost
            // 
            this.colPLCost.FillWeight = 16F;
            this.colPLCost.HeaderText = "Purchase Cost (Rs.)";
            this.colPLCost.MinimumWidth = 6;
            this.colPLCost.Name = "colPLCost";
            this.colPLCost.ReadOnly = true;
            // 
            // colPLGross
            // 
            this.colPLGross.FillWeight = 16F;
            this.colPLGross.HeaderText = "Gross Profit (Rs.)";
            this.colPLGross.MinimumWidth = 6;
            this.colPLGross.Name = "colPLGross";
            this.colPLGross.ReadOnly = true;
            // 
            // colPLMargin
            // 
            this.colPLMargin.FillWeight = 10F;
            this.colPLMargin.HeaderText = "Margin %";
            this.colPLMargin.MinimumWidth = 6;
            this.colPLMargin.Name = "colPLMargin";
            this.colPLMargin.ReadOnly = true;
            // 
            // lblEmpty
            // 
            this.lblEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmpty.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblEmpty.Location = new System.Drawing.Point(14, 34);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(1134, 465);
            this.lblEmpty.TabIndex = 1;
            this.lblEmpty.Text = "No data found for the selected period.";
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            // 
            // lblBar
            // 
            this.lblBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(20)))), ((int)(((byte)(140)))));
            this.lblBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBar.ForeColor = System.Drawing.Color.White;
            this.lblBar.Location = new System.Drawing.Point(14, 0);
            this.lblBar.Name = "lblBar";
            this.lblBar.Size = new System.Drawing.Size(1134, 34);
            this.lblBar.TabIndex = 2;
            this.lblBar.Text = "  Select date range and click Run Report";
            this.lblBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProfitLossReportForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1162, 633);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 520);
            this.Name = "ProfitLossReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report - Profit & Loss";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader, pnlFilter, pnlGrid;
        private System.Windows.Forms.Label lblTitle, lblSub, lblFromCap, lblToCap, lblBar, lblEmpty;
        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
        private System.Windows.Forms.Button btnRun, btnPrint, btnClose;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPLPeriod, colPLOrders, colPLRevenue, colPLInv, colPLCost, colPLGross, colPLMargin;
    }
}