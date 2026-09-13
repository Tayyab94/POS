namespace POS_Shop.Views.Controllers.Reports
{
    partial class SalesSummaryReportForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblGrpCap = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
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
            this.colSalPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalOrders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalAvg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(27, 94, 32);
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 64;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 10);
            this.lblTitle.Text = "Sales Summary Report";
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(165, 214, 167);
            this.lblSub.Location = new System.Drawing.Point(18, 42);
            this.lblSub.Text = "Total orders and revenue grouped by Daily / Weekly / Monthly  ·  Cash vs Credit breakdown";
            // pnlFilter
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.btnClose);
            this.pnlFilter.Controls.Add(this.btnPrint);
            this.pnlFilter.Controls.Add(this.btnRun);
            this.pnlFilter.Controls.Add(this.dtpTo);
            this.pnlFilter.Controls.Add(this.lblToCap);
            this.pnlFilter.Controls.Add(this.dtpFrom);
            this.pnlFilter.Controls.Add(this.lblFromCap);
            this.pnlFilter.Controls.Add(this.cmbGroup);
            this.pnlFilter.Controls.Add(this.lblGrpCap);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 56;
            this.lblGrpCap.AutoSize = true;
            this.lblGrpCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblGrpCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblGrpCap.Location = new System.Drawing.Point(14, 8);
            this.lblGrpCap.Text = "GROUP BY";
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbGroup.Items.AddRange(new object[] { "Monthly", "Weekly", "Daily" });
            this.cmbGroup.Location = new System.Drawing.Point(14, 24);
            this.cmbGroup.Size = new System.Drawing.Size(120, 28);
            this.lblFromCap.AutoSize = true;
            this.lblFromCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblFromCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblFromCap.Location = new System.Drawing.Point(148, 8);
            this.lblFromCap.Text = "FROM";
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(148, 24);
            this.dtpFrom.Size = new System.Drawing.Size(160, 28);
            this.lblToCap.AutoSize = true;
            this.lblToCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblToCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
            this.lblToCap.Location = new System.Drawing.Point(322, 8);
            this.lblToCap.Text = "TO";
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(322, 24);
            this.dtpTo.Size = new System.Drawing.Size(160, 28);
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(27, 94, 32);
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(496, 12);
            this.btnRun.Size = new System.Drawing.Size(150, 34);
            this.btnRun.Text = "Run Report";
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(80, 100, 110);
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(656, 12);
            this.btnPrint.Size = new System.Drawing.Size(110, 34);
            this.btnPrint.Text = "Print";
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(960, 12);
            this.btnClose.Size = new System.Drawing.Size(110, 34);
            this.btnClose.Text = "Close";
            // pnlGrid
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvReport);
            this.pnlGrid.Controls.Add(this.lblEmpty);
            this.pnlGrid.Controls.Add(this.lblBar);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(14, 0, 14, 14);
            this.lblBar.BackColor = System.Drawing.Color.FromArgb(27, 94, 32);
            this.lblBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBar.ForeColor = System.Drawing.Color.White;
            this.lblBar.Height = 34;
            this.lblBar.Text = "  Select grouping and date range, then click Run Report";
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
                this.colSalPeriod, this.colSalOrders, this.colSalRevenue, this.colSalCash, this.colSalCredit, this.colSalAvg });
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.GridColor = System.Drawing.Color.FromArgb(236, 239, 241);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowTemplate.Height = 36;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // Columns
            this.colSalPeriod.FillWeight = 22F; this.colSalPeriod.HeaderText = "Period"; this.colSalPeriod.Name = "colSalPeriod";
            this.colSalOrders.FillWeight = 10F; this.colSalOrders.HeaderText = "Orders"; this.colSalOrders.Name = "colSalOrders";
            this.colSalRevenue.FillWeight = 18F; this.colSalRevenue.HeaderText = "Revenue (Rs.)"; this.colSalRevenue.Name = "colSalRevenue";
            this.colSalCash.FillWeight = 16F; this.colSalCash.HeaderText = "Cash (Rs.)"; this.colSalCash.Name = "colSalCash";
            this.colSalCredit.FillWeight = 16F; this.colSalCredit.HeaderText = "Credit/Other (Rs.)"; this.colSalCredit.Name = "colSalCredit";
            this.colSalAvg.FillWeight = 14F; this.colSalAvg.HeaderText = "Avg Order (Rs.)"; this.colSalAvg.Name = "colSalAvg";
            // Form
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(860, 500);
            this.Size = new System.Drawing.Size(1100, 660);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report - Sales Summary";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader, pnlFilter, pnlGrid;
        private System.Windows.Forms.Label lblTitle, lblSub, lblGrpCap, lblFromCap, lblToCap, lblBar, lblEmpty;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
        private System.Windows.Forms.Button btnRun, btnPrint, btnClose;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalPeriod, colSalOrders, colSalRevenue, colSalCash, colSalCredit, colSalAvg;
    }
}



//// ════════════════════════════════════════════════════════════════════════════
//// SalesSummaryReportForm.Designer.cs
//// ════════════════════════════════════════════════════════════════════════════
//namespace POS_Shop.Views.Controllers.Reports
//{
//    partial class SalesSummaryReportForm
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//                components.Dispose();
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            this.pnlHeader = new System.Windows.Forms.Panel();
//            this.lblTitle = new System.Windows.Forms.Label();
//            this.lblSubtitle = new System.Windows.Forms.Label();
//            this.pnlControls = new System.Windows.Forms.Panel();
//            this.lblGrpBy = new System.Windows.Forms.Label();
//            this.cmbGroup = new System.Windows.Forms.ComboBox();
//            this.lblFrom = new System.Windows.Forms.Label();
//            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
//            this.lblTo = new System.Windows.Forms.Label();
//            this.dtpTo = new System.Windows.Forms.DateTimePicker();
//            this.btnRun = new System.Windows.Forms.Button();
//            this.btnPrint = new System.Windows.Forms.Button();
//            this.btnClose = new System.Windows.Forms.Button();
//            this.lblBar = new System.Windows.Forms.Label();
//            this.dgvReport = new System.Windows.Forms.DataGridView();
//            this.lblEmpty = new System.Windows.Forms.Label();

//            // ── Columns ──────────────────────────────────────────────────────
//            this.colSalPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colSalOrders = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colSalRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colSalCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colSalCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colSalActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colSalProfit = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colSalAvg = new System.Windows.Forms.DataGridViewTextBoxColumn();

//            this.pnlHeader.SuspendLayout();
//            this.pnlControls.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
//            this.SuspendLayout();

//            // ════════════════════════════════════════════════════════════════
//            // pnlHeader
//            // ════════════════════════════════════════════════════════════════
//            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 92, 40);
//            this.pnlHeader.Controls.Add(this.lblSubtitle);
//            this.pnlHeader.Controls.Add(this.lblTitle);
//            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlHeader.Height = 72;
//            this.pnlHeader.Name = "pnlHeader";
//            this.pnlHeader.TabIndex = 0;

//            // lblTitle
//            this.lblTitle.AutoSize = false;
//            this.lblTitle.Dock = System.Windows.Forms.DockStyle.None;
//            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16f, System.Drawing.FontStyle.Bold);
//            this.lblTitle.ForeColor = System.Drawing.Color.White;
//            this.lblTitle.Location = new System.Drawing.Point(16, 10);
//            this.lblTitle.Size = new System.Drawing.Size(700, 30);
//            this.lblTitle.Name = "lblTitle";
//            this.lblTitle.Text = "Sales Summary Report";
//            this.lblTitle.TabIndex = 0;

//            // lblSubtitle
//            this.lblSubtitle.AutoSize = false;
//            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9f);
//            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(200, 255, 200);
//            this.lblSubtitle.Location = new System.Drawing.Point(18, 42);
//            this.lblSubtitle.Size = new System.Drawing.Size(800, 20);
//            this.lblSubtitle.Name = "lblSubtitle";
//            this.lblSubtitle.Text = "Total orders and revenue grouped by Daily / Weekly / Monthly  ·  Cash vs Credit breakdown  ·  Actual Bill & Profit";
//            this.lblSubtitle.TabIndex = 1;

//            // ════════════════════════════════════════════════════════════════
//            // pnlControls
//            // ════════════════════════════════════════════════════════════════
//            this.pnlControls.BackColor = System.Drawing.Color.White;
//            this.pnlControls.Controls.Add(this.lblGrpBy);
//            this.pnlControls.Controls.Add(this.cmbGroup);
//            this.pnlControls.Controls.Add(this.lblFrom);
//            this.pnlControls.Controls.Add(this.dtpFrom);
//            this.pnlControls.Controls.Add(this.lblTo);
//            this.pnlControls.Controls.Add(this.dtpTo);
//            this.pnlControls.Controls.Add(this.btnRun);
//            this.pnlControls.Controls.Add(this.btnPrint);
//            this.pnlControls.Controls.Add(this.btnClose);
//            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlControls.Height = 60;
//            this.pnlControls.Name = "pnlControls";
//            this.pnlControls.TabIndex = 1;

//            // lblGrpBy
//            this.lblGrpBy.AutoSize = true;
//            this.lblGrpBy.Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
//            this.lblGrpBy.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
//            this.lblGrpBy.Location = new System.Drawing.Point(14, 10);
//            this.lblGrpBy.Name = "lblGrpBy";
//            this.lblGrpBy.Text = "GROUP BY";
//            this.lblGrpBy.TabIndex = 0;

//            // cmbGroup
//            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cmbGroup.Font = new System.Drawing.Font("Segoe UI", 9.5f);
//            this.cmbGroup.FormattingEnabled = true;
//            this.cmbGroup.Items.AddRange(new object[] { "Monthly", "Weekly", "Daily" });
//            this.cmbGroup.Location = new System.Drawing.Point(14, 28);
//            this.cmbGroup.Name = "cmbGroup";
//            this.cmbGroup.Size = new System.Drawing.Size(110, 26);
//            this.cmbGroup.TabIndex = 1;

//            // lblFrom
//            this.lblFrom.AutoSize = true;
//            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
//            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
//            this.lblFrom.Location = new System.Drawing.Point(140, 10);
//            this.lblFrom.Name = "lblFrom";
//            this.lblFrom.Text = "FROM";
//            this.lblFrom.TabIndex = 2;

//            // dtpFrom
//            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9.5f);
//            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
//            this.dtpFrom.Location = new System.Drawing.Point(140, 28);
//            this.dtpFrom.Name = "dtpFrom";
//            this.dtpFrom.Size = new System.Drawing.Size(130, 26);
//            this.dtpFrom.TabIndex = 3;

//            // lblTo
//            this.lblTo.AutoSize = true;
//            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
//            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
//            this.lblTo.Location = new System.Drawing.Point(286, 10);
//            this.lblTo.Name = "lblTo";
//            this.lblTo.Text = "TO";
//            this.lblTo.TabIndex = 4;

//            // dtpTo
//            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9.5f);
//            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
//            this.dtpTo.Location = new System.Drawing.Point(286, 28);
//            this.dtpTo.Name = "dtpTo";
//            this.dtpTo.Size = new System.Drawing.Size(130, 26);
//            this.dtpTo.TabIndex = 5;

//            // btnRun
//            this.btnRun.BackColor = System.Drawing.Color.FromArgb(30, 92, 40);
//            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
//            this.btnRun.ForeColor = System.Drawing.Color.White;
//            this.btnRun.Location = new System.Drawing.Point(434, 18);
//            this.btnRun.Name = "btnRun";
//            this.btnRun.Size = new System.Drawing.Size(130, 34);
//            this.btnRun.TabIndex = 6;
//            this.btnRun.Text = "Run Report";
//            this.btnRun.UseVisualStyleBackColor = false;

//            // btnPrint
//            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(80, 100, 110);
//            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.5f);
//            this.btnPrint.ForeColor = System.Drawing.Color.White;
//            this.btnPrint.Location = new System.Drawing.Point(574, 18);
//            this.btnPrint.Name = "btnPrint";
//            this.btnPrint.Size = new System.Drawing.Size(90, 34);
//            this.btnPrint.TabIndex = 7;
//            this.btnPrint.Text = "Print";
//            this.btnPrint.UseVisualStyleBackColor = false;

//            // btnClose
//            this.btnClose.BackColor = System.Drawing.Color.FromArgb(180, 30, 30);
//            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5f);
//            this.btnClose.ForeColor = System.Drawing.Color.White;
//            this.btnClose.Location = new System.Drawing.Point(674, 18);
//            this.btnClose.Name = "btnClose";
//            this.btnClose.Size = new System.Drawing.Size(80, 34);
//            this.btnClose.TabIndex = 8;
//            this.btnClose.Text = "Close";
//            this.btnClose.UseVisualStyleBackColor = false;

//            // ════════════════════════════════════════════════════════════════
//            // lblBar  (status bar)
//            // ════════════════════════════════════════════════════════════════
//            this.lblBar.BackColor = System.Drawing.Color.FromArgb(30, 92, 40);
//            this.lblBar.Dock = System.Windows.Forms.DockStyle.Top;
//            this.lblBar.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
//            this.lblBar.ForeColor = System.Drawing.Color.White;
//            this.lblBar.Height = 30;
//            this.lblBar.Name = "lblBar";
//            this.lblBar.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
//            this.lblBar.TabIndex = 2;
//            this.lblBar.Text = "  Run a report to see results.";
//            this.lblBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

//            // ════════════════════════════════════════════════════════════════
//            // Columns
//            // ════════════════════════════════════════════════════════════════

//            // colSalPeriod
//            this.colSalPeriod.HeaderText = "Period";
//            this.colSalPeriod.Name = "colSalPeriod";
//            this.colSalPeriod.ReadOnly = true;
//            this.colSalPeriod.Width = 160;
//            this.colSalPeriod.MinimumWidth = 120;

//            // colSalOrders
//            this.colSalOrders.HeaderText = "Orders";
//            this.colSalOrders.Name = "colSalOrders";
//            this.colSalOrders.ReadOnly = true;
//            this.colSalOrders.Width = 75;
//            this.colSalOrders.MinimumWidth = 60;

//            // colSalRevenue
//            this.colSalRevenue.HeaderText = "Revenue (Rs.)";
//            this.colSalRevenue.Name = "colSalRevenue";
//            this.colSalRevenue.ReadOnly = true;
//            this.colSalRevenue.Width = 140;
//            this.colSalRevenue.MinimumWidth = 100;

//            // colSalCash
//            this.colSalCash.HeaderText = "Cash (Rs.)";
//            this.colSalCash.Name = "colSalCash";
//            this.colSalCash.ReadOnly = true;
//            this.colSalCash.Width = 120;
//            this.colSalCash.MinimumWidth = 90;

//            // colSalCredit
//            this.colSalCredit.HeaderText = "Credit/Other (Rs.)";
//            this.colSalCredit.Name = "colSalCredit";
//            this.colSalCredit.ReadOnly = true;
//            this.colSalCredit.Width = 140;
//            this.colSalCredit.MinimumWidth = 110;

//            // colSalActual  ← NEW
//            this.colSalActual.HeaderText = "Actual Bill (Rs.)";
//            this.colSalActual.Name = "colSalActual";
//            this.colSalActual.ReadOnly = true;
//            this.colSalActual.Width = 135;
//            this.colSalActual.MinimumWidth = 100;

//            // colSalProfit  ← NEW
//            this.colSalProfit.HeaderText = "Profit (Rs.)";
//            this.colSalProfit.Name = "colSalProfit";
//            this.colSalProfit.ReadOnly = true;
//            this.colSalProfit.Width = 120;
//            this.colSalProfit.MinimumWidth = 90;

//            // colSalAvg
//            this.colSalAvg.HeaderText = "Avg Order (Rs.)";
//            this.colSalAvg.Name = "colSalAvg";
//            this.colSalAvg.ReadOnly = true;
//            this.colSalAvg.Width = 130;
//            this.colSalAvg.MinimumWidth = 100;

//            // ════════════════════════════════════════════════════════════════
//            // dgvReport
//            // ════════════════════════════════════════════════════════════════
//            this.dgvReport.AllowUserToAddRows = false;
//            this.dgvReport.AllowUserToDeleteRows = false;
//            this.dgvReport.AllowUserToResizeRows = false;
//            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
//            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
//            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
//            {
//                this.colSalPeriod,
//                this.colSalOrders,
//                this.colSalRevenue,
//                this.colSalCash,
//                this.colSalCredit,
//                this.colSalActual,
//                this.colSalProfit,
//                this.colSalAvg
//            });
//            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.dgvReport.MultiSelect = false;
//            this.dgvReport.Name = "dgvReport";
//            this.dgvReport.ReadOnly = true;
//            this.dgvReport.RowHeadersVisible = false;
//            this.dgvReport.RowTemplate.Height = 28;
//            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
//            this.dgvReport.TabIndex = 3;

//            // ════════════════════════════════════════════════════════════════
//            // lblEmpty
//            // ════════════════════════════════════════════════════════════════
//            this.lblEmpty.AutoSize = false;
//            this.lblEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.lblEmpty.Font = new System.Drawing.Font("Segoe UI", 11f);
//            this.lblEmpty.ForeColor = System.Drawing.Color.Silver;
//            this.lblEmpty.Name = "lblEmpty";
//            this.lblEmpty.TabIndex = 4;
//            this.lblEmpty.Text = "No data found for the selected date range.";
//            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            this.lblEmpty.Visible = false;

//            // ════════════════════════════════════════════════════════════════
//            // SalesSummaryReportForm
//            // ════════════════════════════════════════════════════════════════
//            this.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.BackColor = System.Drawing.Color.White;
//            this.ClientSize = new System.Drawing.Size(1100, 600);
//            this.Controls.Add(this.dgvReport);
//            this.Controls.Add(this.lblEmpty);
//            this.Controls.Add(this.lblBar);
//            this.Controls.Add(this.pnlControls);
//            this.Controls.Add(this.pnlHeader);
//            this.Font = new System.Drawing.Font("Segoe UI", 9.5f);
//            this.MinimumSize = new System.Drawing.Size(900, 500);
//            this.Name = "SalesSummaryReportForm";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.Text = "Report — Sales Summary";

//            this.pnlHeader.ResumeLayout(false);
//            this.pnlControls.ResumeLayout(false);
//            this.pnlControls.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
//            this.ResumeLayout(false);
//        }

//        // ── Field declarations ────────────────────────────────────────────────
//        private System.Windows.Forms.Panel pnlHeader;
//        private System.Windows.Forms.Label lblTitle;
//        private System.Windows.Forms.Label lblSubtitle;
//        private System.Windows.Forms.Panel pnlControls;
//        private System.Windows.Forms.Label lblGrpBy;
//        private System.Windows.Forms.ComboBox cmbGroup;
//        private System.Windows.Forms.Label lblFrom;
//        private System.Windows.Forms.DateTimePicker dtpFrom;
//        private System.Windows.Forms.Label lblTo;
//        private System.Windows.Forms.DateTimePicker dtpTo;
//        private System.Windows.Forms.Button btnRun;
//        private System.Windows.Forms.Button btnPrint;
//        private System.Windows.Forms.Button btnClose;
//        private System.Windows.Forms.Label lblBar;
//        private System.Windows.Forms.DataGridView dgvReport;
//        private System.Windows.Forms.Label lblEmpty;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colSalPeriod;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colSalOrders;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colSalRevenue;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colSalCash;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colSalCredit;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colSalActual;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colSalProfit;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colSalAvg;
//    }
//}