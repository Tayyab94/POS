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