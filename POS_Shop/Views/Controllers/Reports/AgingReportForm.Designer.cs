using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    partial class AgingReportForm
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
            this.pnlBar = new Panel();
            this.btnRun = new Button();
            this.btnPrint = new Button();
            this.btnClose = new Button();
            this.lblAsOf = new Label();
            this.pnlGrid = new Panel();
            this.lblBar = new Label();
            this.dgvReport = new DataGridView();
            this.colAGSup = new DataGridViewTextBoxColumn();
            this.colAGInv = new DataGridViewTextBoxColumn();
            this.colAGCur = new DataGridViewTextBoxColumn();
            this.colAG30 = new DataGridViewTextBoxColumn();
            this.colAG60 = new DataGridViewTextBoxColumn();
            this.colAG90 = new DataGridViewTextBoxColumn();
            this.colAGTot = new DataGridViewTextBoxColumn();
            this.lblEmpty = new Label();

            ((ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────────────────────
            this.Text = "Report — Supplier Aging";
            this.Size = new Size(1200, 680);
            this.MinimumSize = new Size(950, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.Font = new Font("Segoe UI", 9f);

            // ── Header (red) ─────────────────────────────────────────────────────
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 64;
            this.pnlHeader.BackColor = Color.FromArgb(198, 40, 40);
            this.lblTitle.Text = "Supplier Aging Report";
            this.lblTitle.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(16, 10);
            this.lblSub.Text = "Outstanding payables grouped by age  ·  Real-time  (no date filter required)";
            this.lblSub.Font = new Font("Segoe UI", 9f);
            this.lblSub.ForeColor = Color.FromArgb(255, 205, 210);
            this.lblSub.AutoSize = true;
            this.lblSub.Location = new Point(18, 40);
            this.pnlHeader.Controls.AddRange(new Control[] { this.lblTitle, this.lblSub });

            // ── Tool bar ─────────────────────────────────────────────────────────
            this.pnlBar.Dock = DockStyle.Top;
            this.pnlBar.Height = 56;
            this.pnlBar.BackColor = Color.White;

            this.lblAsOf.Text = "Run report to calculate outstanding debts as of TODAY.";
            this.lblAsOf.Font = new Font("Segoe UI", 9.5f, FontStyle.Italic);
            this.lblAsOf.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblAsOf.AutoSize = true;
            this.lblAsOf.Location = new Point(14, 20);

            this.btnRun.Text = "▶  Run Report";
            this.btnRun.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnRun.ForeColor = Color.White;
            this.btnRun.BackColor = Color.FromArgb(198, 40, 40);
            this.btnRun.FlatStyle = FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = Cursors.Hand;
            this.btnRun.Size = new Size(150, 36);
            this.btnRun.Location = new Point(640, 10);

            this.btnPrint.Text = "🖨  Print";
            this.btnPrint.Font = new Font("Segoe UI", 10f);
            this.btnPrint.ForeColor = Color.White;
            this.btnPrint.BackColor = Color.FromArgb(80, 100, 110);
            this.btnPrint.FlatStyle = FlatStyle.Flat;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.Cursor = Cursors.Hand;
            this.btnPrint.Size = new Size(110, 36);
            this.btnPrint.Location = new Point(800, 10);

            this.btnClose.Text = "Close";
            this.btnClose.Font = new Font("Segoe UI", 10f);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.BackColor = Color.FromArgb(80, 100, 110);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Size = new Size(110, 36);
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Location = new Point(1060, 10);

            this.pnlBar.Controls.AddRange(new Control[] {
                this.lblAsOf, this.btnRun, this.btnPrint, this.btnClose });

            // ── Grid panel ───────────────────────────────────────────────────────
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = Color.White;
            this.pnlGrid.Padding = new Padding(14, 0, 14, 14);

            this.lblBar.Text = "  Click 'Run Report' to load aging data";
            this.lblBar.Dock = DockStyle.Top;
            this.lblBar.Height = 34;
            this.lblBar.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblBar.ForeColor = Color.White;
            this.lblBar.BackColor = Color.FromArgb(198, 40, 40);
            this.lblBar.TextAlign = ContentAlignment.MiddleLeft;

            this.lblEmpty.Text = "✔  No outstanding payables — all invoices are fully paid!";
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
            this.colAGSup.Name = "colAGSup";
            this.colAGSup.HeaderText = "Supplier";
            this.colAGSup.FillWeight = 26f;
            this.colAGSup.ReadOnly = true;

            this.colAGInv.Name = "colAGInv";
            this.colAGInv.HeaderText = "Open Inv.";
            this.colAGInv.FillWeight = 7f;
            this.colAGInv.ReadOnly = true;
            this.colAGInv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colAGInv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colAGCur.Name = "colAGCur";
            this.colAGCur.HeaderText = "0 – 30 Days";
            this.colAGCur.FillWeight = 14f;
            this.colAGCur.ReadOnly = true;
            this.colAGCur.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colAGCur.DefaultCellStyle.Format = "N2";
            this.colAGCur.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colAG30.Name = "colAG30";
            this.colAG30.HeaderText = "31 – 60 Days";
            this.colAG30.FillWeight = 14f;
            this.colAG30.ReadOnly = true;
            this.colAG30.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colAG30.DefaultCellStyle.Format = "N2";
            this.colAG30.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colAG60.Name = "colAG60";
            this.colAG60.HeaderText = "61 – 90 Days";
            this.colAG60.FillWeight = 14f;
            this.colAG60.ReadOnly = true;
            this.colAG60.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colAG60.DefaultCellStyle.Format = "N2";
            this.colAG60.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colAG90.Name = "colAG90";
            this.colAG90.HeaderText = "90+ Days ⚠";
            this.colAG90.FillWeight = 14f;
            this.colAG90.ReadOnly = true;
            this.colAG90.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colAG90.DefaultCellStyle.Format = "N2";
            this.colAG90.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colAGTot.Name = "colAGTot";
            this.colAGTot.HeaderText = "Total Outstanding";
            this.colAGTot.FillWeight = 16f;
            this.colAGTot.ReadOnly = true;
            this.colAGTot.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colAGTot.DefaultCellStyle.Format = "N2";
            this.colAGTot.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.colAGTot.DefaultCellStyle.ForeColor = Color.FromArgb(198, 40, 40);

            this.dgvReport.Columns.AddRange(new DataGridViewColumn[] {
                this.colAGSup, this.colAGInv, this.colAGCur, this.colAG30,
                this.colAG60, this.colAG90, this.colAGTot });

            this.pnlGrid.Controls.AddRange(new Control[] {
                this.dgvReport, this.lblEmpty, this.lblBar });

            this.Controls.AddRange(new Control[] {
                this.pnlGrid, this.pnlBar, this.pnlHeader });

            ((ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }

        // Fields
        private Panel pnlHeader, pnlBar, pnlGrid;
        private Label lblTitle, lblSub, lblAsOf, lblBar, lblEmpty;
        private Button btnRun, btnPrint, btnClose;
        private DataGridView dgvReport;
        private DataGridViewTextBoxColumn colAGSup, colAGInv, colAGCur, colAG30, colAG60, colAG90, colAGTot;
    }
}