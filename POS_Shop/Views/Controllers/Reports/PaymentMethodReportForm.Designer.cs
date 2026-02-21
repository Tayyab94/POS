using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    partial class PaymentMethodReportForm
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
            this.btnRun = new Button();
            this.btnPrint = new Button();
            this.btnClose = new Button();
            this.pnlGrid = new Panel();
            this.lblBar = new Label();
            this.lblEmpty = new Label();
            this.dgvReport = new DataGridView();
            this.colPMMethod = new DataGridViewTextBoxColumn();
            this.colPMRuns = new DataGridViewTextBoxColumn();
            this.colPMTotal = new DataGridViewTextBoxColumn();
            this.colPMAvg = new DataGridViewTextBoxColumn();
            this.colPMShare = new DataGridViewTextBoxColumn();
            this.colPMLast = new DataGridViewTextBoxColumn();

            ((ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────────────────────
            this.Text = "Report — Payment Method Analysis";
            this.Size = new Size(1050, 640);
            this.MinimumSize = new Size(860, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.Font = new Font("Segoe UI", 9f);

            // ── Header ───────────────────────────────────────────────────────────
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 64;
            this.pnlHeader.BackColor = Color.FromArgb(21, 101, 192); // acc
            this.lblTitle.Text = "Payment Method Analysis";
            this.lblTitle.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(16, 10);
            this.lblSub.Text = "How much paid via Cash  ·  Bank Transfer  ·  Cheque  ·  Online in the selected period";
            this.lblSub.Font = new Font("Segoe UI", 9f);
            this.lblSub.ForeColor = Color.FromArgb(187, 222, 251);
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

            this.btnRun.Text = "▶  Run Report";
            this.btnRun.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnRun.ForeColor = Color.White;
            this.btnRun.BackColor = Color.FromArgb(21, 101, 192);
            this.btnRun.FlatStyle = FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = Cursors.Hand;
            this.btnRun.Size = new Size(150, 34);
            this.btnRun.Location = new Point(362, 12);

            this.btnPrint.Text = "🖨  Print";
            this.btnPrint.Font = new Font("Segoe UI", 10f);
            this.btnPrint.ForeColor = Color.White;
            this.btnPrint.BackColor = Color.FromArgb(80, 100, 110);
            this.btnPrint.FlatStyle = FlatStyle.Flat;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.Cursor = Cursors.Hand;
            this.btnPrint.Size = new Size(110, 34);
            this.btnPrint.Location = new Point(522, 12);

            this.btnClose.Text = "Close";
            this.btnClose.Font = new Font("Segoe UI", 10f);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.BackColor = Color.FromArgb(198, 40, 40);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Size = new Size(110, 34);
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Location = new Point(910, 12);

            this.pnlFilter.Controls.AddRange(new Control[] {
                this.lblFromCap, this.dtpFrom, this.lblToCap, this.dtpTo,
                this.btnRun, this.btnPrint, this.btnClose });

            // ── Grid panel ───────────────────────────────────────────────────────
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = Color.White;
            this.pnlGrid.Padding = new Padding(14, 0, 14, 14);

            this.lblBar.Text = "  Run report to see results";
            this.lblBar.Dock = DockStyle.Top;
            this.lblBar.Height = 34;
            this.lblBar.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblBar.ForeColor = Color.White;
            this.lblBar.BackColor = Color.FromArgb(21, 101, 192);
            this.lblBar.TextAlign = ContentAlignment.MiddleLeft;

            this.lblEmpty.Text = "No payments found in the selected period.";
            this.lblEmpty.Font = new Font("Segoe UI", 12f, FontStyle.Italic);
            this.lblEmpty.ForeColor = Color.FromArgb(120, 144, 156);
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
            this.dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(21, 101, 192);
            this.dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(21, 101, 192);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            this.dgvReport.ColumnHeadersHeight = 40;
            this.dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Default cell style (replaces 'cell' variable)
            this.dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            this.dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 222, 251);
            this.dgvReport.DefaultCellStyle.SelectionForeColor = Color.FromArgb(13, 71, 161);

            // Alternating rows (replaces 'alt' variable)
            this.dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);

            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.MultiSelect = false;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowTemplate.Height = 36;
            this.dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ── Columns ──────────────────────────────────────────────────────────
            this.colPMMethod.Name = "colPMMethod";
            this.colPMMethod.HeaderText = "Payment Method";
            this.colPMMethod.FillWeight = 20f;
            this.colPMMethod.ReadOnly = true;

            this.colPMRuns.Name = "colPMRuns";
            this.colPMRuns.HeaderText = "Payment Runs";
            this.colPMRuns.FillWeight = 12f;
            this.colPMRuns.ReadOnly = true;
            this.colPMRuns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colPMRuns.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colPMTotal.Name = "colPMTotal";
            this.colPMTotal.HeaderText = "Total Paid";
            this.colPMTotal.FillWeight = 18f;
            this.colPMTotal.ReadOnly = true;
            this.colPMTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colPMTotal.DefaultCellStyle.Format = "N2";
            this.colPMTotal.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.colPMTotal.DefaultCellStyle.ForeColor = Color.FromArgb(21, 101, 192);

            this.colPMAvg.Name = "colPMAvg";
            this.colPMAvg.HeaderText = "Avg Payment";
            this.colPMAvg.FillWeight = 16f;
            this.colPMAvg.ReadOnly = true;
            this.colPMAvg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colPMAvg.DefaultCellStyle.Format = "N2";
            this.colPMAvg.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colPMShare.Name = "colPMShare";
            this.colPMShare.HeaderText = "Share %";
            this.colPMShare.FillWeight = 10f;
            this.colPMShare.ReadOnly = true;
            this.colPMShare.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colPMShare.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colPMLast.Name = "colPMLast";
            this.colPMLast.HeaderText = "Last Payment";
            this.colPMLast.FillWeight = 14f;
            this.colPMLast.ReadOnly = true;

            this.dgvReport.Columns.AddRange(new DataGridViewColumn[] {
                this.colPMMethod, this.colPMRuns, this.colPMTotal, this.colPMAvg,
                this.colPMShare, this.colPMLast });

            this.pnlGrid.Controls.AddRange(new Control[] { this.dgvReport, this.lblEmpty, this.lblBar });
            this.Controls.AddRange(new Control[] { this.pnlGrid, this.pnlFilter, this.pnlHeader });

            ((ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }

        // Fields
        private Panel pnlHeader, pnlFilter, pnlGrid;
        private Label lblTitle, lblSub, lblFromCap, lblToCap, lblBar, lblEmpty;
        private DateTimePicker dtpFrom, dtpTo;
        private Button btnRun, btnPrint, btnClose;
        private DataGridView dgvReport;
        private DataGridViewTextBoxColumn colPMMethod, colPMRuns, colPMTotal, colPMAvg, colPMShare, colPMLast;
    }
}