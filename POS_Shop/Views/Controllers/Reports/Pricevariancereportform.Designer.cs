using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    partial class PriceVarianceReportForm
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new Panel();
            this.lblTitle = new Label();
            this.lblSub = new Label();
            this.pnlFilter = new Panel();
            this.lblFromCap = new Label();
            this.dtpFrom = new DateTimePicker();
            this.lblToCap = new Label();
            this.dtpTo = new DateTimePicker();
            this.lblMinVar = new Label();
            this.nudMinVar = new NumericUpDown();
            this.btnRun = new Button();
            this.btnPrint = new Button();
            this.btnClose = new Button();
            this.pnlGrid = new Panel();
            this.lblBar = new Label();
            this.lblEmpty = new Label();
            this.dgvReport = new DataGridView();
            this.colVProduct = new DataGridViewTextBoxColumn();
            this.colVCode = new DataGridViewTextBoxColumn();
            this.colVMin = new DataGridViewTextBoxColumn();
            this.colVMax = new DataGridViewTextBoxColumn();
            this.colVAvg = new DataGridViewTextBoxColumn();
            this.colVVarPct = new DataGridViewTextBoxColumn();
            this.colVVarRs = new DataGridViewTextBoxColumn();
            this.colVTimes = new DataGridViewTextBoxColumn();
            this.colVLast = new DataGridViewTextBoxColumn();

            ((ISupportInitialize)(this.dgvReport)).BeginInit();
            ((ISupportInitialize)(this.nudMinVar)).BeginInit();
            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────────────────────
            this.Text = "Report — Purchase Price Variance";
            this.Size = new Size(1280, 700);
            this.MinimumSize = new Size(1050, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.Font = new Font("Segoe UI", 9f);

            // ── Header ───────────────────────────────────────────────────────────
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 64;
            this.pnlHeader.BackColor = Color.FromArgb(198, 40, 40); // acc
            this.lblTitle.Text = "Purchase Price Variance";
            this.lblTitle.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(16, 10);
            this.lblSub.Text = "Same product bought at different prices — catches supplier price creep  ·  Sorted by highest variance first";
            this.lblSub.Font = new Font("Segoe UI", 9f);
            this.lblSub.ForeColor = Color.FromArgb(255, 205, 210);
            this.lblSub.AutoSize = true;
            this.lblSub.Location = new Point(18, 40);
            this.pnlHeader.Controls.AddRange(new Control[] { this.lblTitle, this.lblSub });

            // ── Filter bar ───────────────────────────────────────────────────────
            this.pnlFilter.Dock = DockStyle.Top;
            this.pnlFilter.Height = 56;
            this.pnlFilter.BackColor = Color.White;

            this.lblFromCap.Text = "FROM";
            this.lblFromCap.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            this.lblFromCap.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblFromCap.AutoSize = true;
            this.lblFromCap.Location = new Point(14, 8);
            this.dtpFrom.Location = new Point(14, 24);
            this.dtpFrom.Size = new Size(160, 28);
            this.dtpFrom.Font = new Font("Segoe UI", 10f);
            this.dtpFrom.Format = DateTimePickerFormat.Short;

            this.lblToCap.Text = "TO";
            this.lblToCap.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            this.lblToCap.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblToCap.AutoSize = true;
            this.lblToCap.Location = new Point(188, 8);
            this.dtpTo.Location = new Point(188, 24);
            this.dtpTo.Size = new Size(160, 28);
            this.dtpTo.Font = new Font("Segoe UI", 10f);
            this.dtpTo.Format = DateTimePickerFormat.Short;

            this.lblMinVar.Text = "MIN VARIANCE %";
            this.lblMinVar.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            this.lblMinVar.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblMinVar.AutoSize = true;
            this.lblMinVar.Location = new Point(364, 8);
            this.nudMinVar.Location = new Point(364, 24);
            this.nudMinVar.Size = new Size(90, 28);
            this.nudMinVar.Font = new Font("Segoe UI", 10f);
            this.nudMinVar.Minimum = 0;
            this.nudMinVar.Maximum = 100;
            this.nudMinVar.Value = 10;
            this.nudMinVar.DecimalPlaces = 0;

            this.btnRun.Text = "▶  Run Report";
            this.btnRun.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnRun.ForeColor = Color.White;
            this.btnRun.BackColor = Color.FromArgb(198, 40, 40);
            this.btnRun.FlatStyle = FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = Cursors.Hand;
            this.btnRun.Size = new Size(150, 34);
            this.btnRun.Location = new Point(468, 12);

            this.btnPrint.Text = "🖨  Print";
            this.btnPrint.Font = new Font("Segoe UI", 10f);
            this.btnPrint.ForeColor = Color.White;
            this.btnPrint.BackColor = Color.FromArgb(80, 100, 110);
            this.btnPrint.FlatStyle = FlatStyle.Flat;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.Cursor = Cursors.Hand;
            this.btnPrint.Size = new Size(110, 34);
            this.btnPrint.Location = new Point(628, 12);

            this.btnClose.Text = "Close";
            this.btnClose.Font = new Font("Segoe UI", 10f);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.BackColor = Color.FromArgb(80, 100, 110);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Size = new Size(110, 34);
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Location = new Point(1140, 12);

            this.pnlFilter.Controls.AddRange(new Control[] {
                this.lblFromCap, this.dtpFrom, this.lblToCap, this.dtpTo,
                this.lblMinVar, this.nudMinVar, this.btnRun, this.btnPrint, this.btnClose });

            // ── Grid panel ───────────────────────────────────────────────────────
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = Color.White;
            this.pnlGrid.Padding = new Padding(14, 0, 14, 14);

            this.lblBar.Text = "  Run report to detect products with inconsistent pricing";
            this.lblBar.Dock = DockStyle.Top;
            this.lblBar.Height = 34;
            this.lblBar.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblBar.ForeColor = Color.White;
            this.lblBar.BackColor = Color.FromArgb(198, 40, 40);
            this.lblBar.TextAlign = ContentAlignment.MiddleLeft;

            this.lblEmpty.Text = "No price variance found ≥ the selected threshold. Pricing is consistent!";
            this.lblEmpty.Font = new Font("Segoe UI", 12f, FontStyle.Italic);
            this.lblEmpty.ForeColor = Color.FromArgb(46, 125, 50);
            this.lblEmpty.Dock = DockStyle.Fill;
            this.lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;

            // ── DataGridView ─────────────────────────────────────────────────────
            this.dgvReport.Dock = DockStyle.Fill;
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.ReadOnly = true;
            this.dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = Color.White;
            this.dgvReport.BorderStyle = BorderStyle.None;
            this.dgvReport.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReport.GridColor = Color.FromArgb(236, 239, 241);

            // Header style (replaces 'hdr' variable)
            this.dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(198, 40, 40);
            this.dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(198, 40, 40);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            this.dgvReport.ColumnHeadersHeight = 40;
            this.dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Default cell style (replaces 'cell' variable)
            this.dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            this.dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 205, 210);
            this.dgvReport.DefaultCellStyle.SelectionForeColor = Color.FromArgb(183, 28, 28);

            // Alternating rows (replaces 'alt' variable)
            this.dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 250, 250);

            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.MultiSelect = false;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowTemplate.Height = 36;
            this.dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ── Columns ──────────────────────────────────────────────────────────
            this.colVProduct.Name = "colVProduct";
            this.colVProduct.HeaderText = "Product Name";
            this.colVProduct.FillWeight = 22f;
            this.colVProduct.ReadOnly = true;

            this.colVCode.Name = "colVCode";
            this.colVCode.HeaderText = "Code";
            this.colVCode.FillWeight = 10f;
            this.colVCode.ReadOnly = true;

            this.colVMin.Name = "colVMin";
            this.colVMin.HeaderText = "Min Price";
            this.colVMin.FillWeight = 11f;
            this.colVMin.ReadOnly = true;
            this.colVMin.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colVMin.DefaultCellStyle.Format = "N2";
            this.colVMin.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colVMax.Name = "colVMax";
            this.colVMax.HeaderText = "Max Price";
            this.colVMax.FillWeight = 11f;
            this.colVMax.ReadOnly = true;
            this.colVMax.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colVMax.DefaultCellStyle.Format = "N2";
            this.colVMax.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colVAvg.Name = "colVAvg";
            this.colVAvg.HeaderText = "Avg Price";
            this.colVAvg.FillWeight = 11f;
            this.colVAvg.ReadOnly = true;
            this.colVAvg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colVAvg.DefaultCellStyle.Format = "N2";
            this.colVAvg.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colVVarPct.Name = "colVVarPct";
            this.colVVarPct.HeaderText = "Variance %  ⚠";
            this.colVVarPct.FillWeight = 10f;
            this.colVVarPct.ReadOnly = true;
            this.colVVarPct.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colVVarPct.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colVVarRs.Name = "colVVarRs";
            this.colVVarRs.HeaderText = "Variance Rs.";
            this.colVVarRs.FillWeight = 11f;
            this.colVVarRs.ReadOnly = true;
            this.colVVarRs.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colVVarRs.DefaultCellStyle.Format = "N2";
            this.colVVarRs.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colVTimes.Name = "colVTimes";
            this.colVTimes.HeaderText = "Purchase Lines";
            this.colVTimes.FillWeight = 9f;
            this.colVTimes.ReadOnly = true;
            this.colVTimes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colVTimes.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colVLast.Name = "colVLast";
            this.colVLast.HeaderText = "Last Ordered";
            this.colVLast.FillWeight = 11f;
            this.colVLast.ReadOnly = true;

            this.dgvReport.Columns.AddRange(new DataGridViewColumn[] {
                this.colVProduct, this.colVCode, this.colVMin, this.colVMax,
                this.colVAvg, this.colVVarPct, this.colVVarRs, this.colVTimes, this.colVLast });

            this.pnlGrid.Controls.AddRange(new Control[] { this.dgvReport, this.lblEmpty, this.lblBar });
            this.Controls.AddRange(new Control[] { this.pnlGrid, this.pnlFilter, this.pnlHeader });

            ((ISupportInitialize)(this.nudMinVar)).EndInit();
            ((ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }

        // Fields
        private Panel pnlHeader, pnlFilter, pnlGrid;
        private Label lblTitle, lblSub, lblFromCap, lblToCap, lblMinVar, lblBar, lblEmpty;
        private DateTimePicker dtpFrom, dtpTo;
        private NumericUpDown nudMinVar;
        private Button btnRun, btnPrint, btnClose;
        private DataGridView dgvReport;
        private DataGridViewTextBoxColumn colVProduct, colVCode, colVMin, colVMax, colVAvg, colVVarPct, colVVarRs, colVTimes, colVLast;
    }
}