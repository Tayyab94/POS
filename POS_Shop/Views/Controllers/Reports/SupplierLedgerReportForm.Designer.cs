using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    partial class SupplierLedgerReportForm
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
            this.lblSupCap = new Label();
            this.txtSup = new TextBox();
            this.pnlSupBadge = new Panel();
            this.lblSelSup = new Label();
            this.btnClrSup = new Button();
            this.lblFromCap = new Label();
            this.dtpFrom = new DateTimePicker();
            this.lblToCap = new Label();
            this.dtpTo = new DateTimePicker();
            this.btnRun = new Button();
            this.btnPrint = new Button();
            this.btnClose = new Button();
            this.pnlGrid = new Panel();
            this.lblBar = new Label();
            this.dgvReport = new DataGridView();
            this.colLDate = new DataGridViewTextBoxColumn();
            this.colLType = new DataGridViewTextBoxColumn();
            this.colLRef = new DataGridViewTextBoxColumn();
            this.colLDebit = new DataGridViewTextBoxColumn();
            this.colLCredit = new DataGridViewTextBoxColumn();
            this.colLBalance = new DataGridViewTextBoxColumn();
            this.lblEmpty = new Label();
            this.lstSupSugg = new ListBox();

            ((ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────────────────────
            this.Text = "Report — Supplier Ledger";
            this.Size = new Size(1100, 720);
            this.MinimumSize = new Size(900, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.Font = new Font("Segoe UI", 9f);

            // ── Header ───────────────────────────────────────────────────────────
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 64;
            this.pnlHeader.BackColor = Color.FromArgb(46, 125, 50); // acc
            this.lblTitle.Text = "Supplier Ledger";
            this.lblTitle.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(16, 10);
            this.lblSub.Text = "Full transaction history per supplier  ·  Invoices + Payments + Running Balance  ·  Click Invoice No to drill down";
            this.lblSub.Font = new Font("Segoe UI", 9f);
            this.lblSub.ForeColor = Color.FromArgb(165, 214, 167);
            this.lblSub.AutoSize = true;
            this.lblSub.Location = new Point(18, 40);
            this.pnlHeader.Controls.AddRange(new Control[] { this.lblTitle, this.lblSub });

            // ── Filter bar ───────────────────────────────────────────────────────
            this.pnlFilter.Dock = DockStyle.Top;
            this.pnlFilter.Height = 100;
            this.pnlFilter.BackColor = Color.White;

            this.lblSupCap.Text = "SUPPLIER  (required)";
            this.lblSupCap.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            this.lblSupCap.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblSupCap.AutoSize = true;
            this.lblSupCap.Location = new Point(14, 10);

            this.txtSup.Location = new Point(14, 26);
            this.txtSup.Size = new Size(240, 28);
            this.txtSup.Font = new Font("Segoe UI", 10f);
            this.txtSup.BorderStyle = BorderStyle.FixedSingle;

            this.pnlSupBadge.BackColor = Color.FromArgb(232, 245, 233);
            this.pnlSupBadge.Location = new Point(14, 62);
            this.pnlSupBadge.Size = new Size(260, 26);
            this.pnlSupBadge.Visible = false;
            this.lblSelSup.AutoSize = false;
            this.lblSelSup.Size = new Size(228, 26);
            this.lblSelSup.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            this.lblSelSup.ForeColor = Color.FromArgb(27, 94, 32);
            this.lblSelSup.TextAlign = ContentAlignment.MiddleLeft;
            this.lblSelSup.Location = new Point(6, 0);
            this.btnClrSup.Text = "✕";
            this.btnClrSup.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            this.btnClrSup.ForeColor = Color.FromArgb(198, 40, 40);
            this.btnClrSup.BackColor = Color.Transparent;
            this.btnClrSup.FlatStyle = FlatStyle.Flat;
            this.btnClrSup.FlatAppearance.BorderSize = 0;
            this.btnClrSup.Cursor = Cursors.Hand;
            this.btnClrSup.Size = new Size(26, 26);
            this.btnClrSup.Location = new Point(234, 0);
            this.pnlSupBadge.Controls.AddRange(new Control[] { this.lblSelSup, this.btnClrSup });

            this.lblFromCap.Text = "FROM";
            this.lblFromCap.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            this.lblFromCap.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblFromCap.AutoSize = true;
            this.lblFromCap.Location = new Point(270, 10);
            this.dtpFrom.Location = new Point(270, 26);
            this.dtpFrom.Size = new Size(160, 28);
            this.dtpFrom.Font = new Font("Segoe UI", 10f);
            this.dtpFrom.Format = DateTimePickerFormat.Short;

            this.lblToCap.Text = "TO";
            this.lblToCap.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            this.lblToCap.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblToCap.AutoSize = true;
            this.lblToCap.Location = new Point(445, 10);
            this.dtpTo.Location = new Point(445, 26);
            this.dtpTo.Size = new Size(160, 28);
            this.dtpTo.Font = new Font("Segoe UI", 10f);
            this.dtpTo.Format = DateTimePickerFormat.Short;

            this.btnRun.Text = "▶  Run Report";
            this.btnRun.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnRun.ForeColor = Color.White;
            this.btnRun.BackColor = Color.FromArgb(46, 125, 50);
            this.btnRun.FlatStyle = FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = Cursors.Hand;
            this.btnRun.Size = new Size(150, 36);
            this.btnRun.Location = new Point(620, 32);

            this.btnPrint.Text = "🖨  Print";
            this.btnPrint.Font = new Font("Segoe UI", 10f);
            this.btnPrint.ForeColor = Color.White;
            this.btnPrint.BackColor = Color.FromArgb(80, 100, 110);
            this.btnPrint.FlatStyle = FlatStyle.Flat;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.Cursor = Cursors.Hand;
            this.btnPrint.Size = new Size(110, 36);
            this.btnPrint.Location = new Point(780, 32);

            this.btnClose.Text = "Close";
            this.btnClose.Font = new Font("Segoe UI", 10f);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.BackColor = Color.FromArgb(198, 40, 40);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Size = new Size(110, 36);
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Location = new Point(960, 32);

            this.pnlFilter.Controls.AddRange(new Control[] {
                this.lblSupCap, this.txtSup, this.pnlSupBadge,
                this.lblFromCap, this.dtpFrom, this.lblToCap, this.dtpTo,
                this.btnRun, this.btnPrint, this.btnClose });

            // ── Grid panel ───────────────────────────────────────────────────────
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = Color.White;
            this.pnlGrid.Padding = new Padding(14, 0, 14, 14);

            this.lblBar.Text = "  Select a supplier and click Run Report";
            this.lblBar.Dock = DockStyle.Top;
            this.lblBar.Height = 34;
            this.lblBar.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblBar.ForeColor = Color.White;
            this.lblBar.BackColor = Color.FromArgb(46, 125, 50);
            this.lblBar.TextAlign = ContentAlignment.MiddleLeft;

            this.lblEmpty.Text = "No transactions found for the selected period.";
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
            this.dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 125, 50);
            this.dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 125, 50);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            this.dgvReport.ColumnHeadersHeight = 40;
            this.dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Default cell style (replaces 'cell' variable)
            this.dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            this.dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            this.dgvReport.DefaultCellStyle.SelectionForeColor = Color.FromArgb(27, 94, 32);

            // Alternating rows (replaces 'alt' variable)
            this.dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 251, 245);

            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.MultiSelect = false;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowTemplate.Height = 36;
            this.dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ── Columns ──────────────────────────────────────────────────────────
            this.colLDate.Name = "colLDate";
            this.colLDate.HeaderText = "Date";
            this.colLDate.FillWeight = 13f;
            this.colLDate.ReadOnly = true;

            this.colLType.Name = "colLType";
            this.colLType.HeaderText = "Type";
            this.colLType.FillWeight = 10f;
            this.colLType.ReadOnly = true;

            this.colLRef.Name = "colLRef";
            this.colLRef.HeaderText = "Reference / Inv#";
            this.colLRef.FillWeight = 14f;
            this.colLRef.ReadOnly = true;

            this.colLDebit.Name = "colLDebit";
            this.colLDebit.HeaderText = "Debit (Rs.)";
            this.colLDebit.FillWeight = 16f;
            this.colLDebit.ReadOnly = true;
            this.colLDebit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colLDebit.DefaultCellStyle.Format = "N2";
            this.colLDebit.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colLCredit.Name = "colLCredit";
            this.colLCredit.HeaderText = "Credit (Rs.)";
            this.colLCredit.FillWeight = 16f;
            this.colLCredit.ReadOnly = true;
            this.colLCredit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colLCredit.DefaultCellStyle.Format = "N2";
            this.colLCredit.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colLBalance.Name = "colLBalance";
            this.colLBalance.HeaderText = "Balance (Rs.)";
            this.colLBalance.FillWeight = 18f;
            this.colLBalance.ReadOnly = true;
            this.colLBalance.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colLBalance.DefaultCellStyle.Format = "N2";
            this.colLBalance.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.colLBalance.DefaultCellStyle.ForeColor = Color.FromArgb(46, 125, 50);

            this.dgvReport.Columns.AddRange(new DataGridViewColumn[] {
                this.colLDate, this.colLType, this.colLRef, this.colLDebit,
                this.colLCredit, this.colLBalance });

            // Suggestion list (floats on top)
            this.lstSupSugg.DrawMode = DrawMode.OwnerDrawFixed;
            this.lstSupSugg.ItemHeight = 42;
            this.lstSupSugg.BorderStyle = BorderStyle.FixedSingle;
            this.lstSupSugg.BackColor = Color.White;
            this.lstSupSugg.Visible = false;

            this.pnlGrid.Controls.AddRange(new Control[] { this.dgvReport, this.lblEmpty, this.lblBar });
            this.Controls.AddRange(new Control[] {
                this.pnlGrid, this.pnlFilter, this.pnlHeader, this.lstSupSugg });

            ((ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }

        // Fields
        private Panel pnlHeader, pnlFilter, pnlGrid, pnlSupBadge;
        private Label lblTitle, lblSub, lblSupCap, lblSelSup, lblFromCap, lblToCap, lblBar, lblEmpty;
        private TextBox txtSup;
        private Button btnClrSup, btnRun, btnPrint, btnClose;
        private DateTimePicker dtpFrom, dtpTo;
        private DataGridView dgvReport;
        private DataGridViewTextBoxColumn colLDate, colLType, colLRef, colLDebit, colLCredit, colLBalance;
        private ListBox lstSupSugg;
    }
}