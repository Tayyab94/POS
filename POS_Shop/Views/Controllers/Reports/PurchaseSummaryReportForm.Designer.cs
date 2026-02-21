using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    partial class PurchaseSummaryReportForm
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        // ── Fields ───────────────────────────────────────────────────────────────
        private Panel pnlHeader, pnlFilter, pnlBody;
        private Label lblTitle, lblSub, lblFromCap, lblToCap, lblGrpCap, lblBar, lblEmpty;
        private DateTimePicker dtpFrom, dtpTo;
        private ComboBox cmbGroup;
        private Button btnRun, btnPrint, btnClose;
        private DataGridView dgvReport;
        private DataGridViewTextBoxColumn colS1Period, colS1Inv, colS1Bill, colS1Disc, colS1Net, colS1Avg;

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
            this.lblGrpCap = new Label();
            this.cmbGroup = new ComboBox();
            this.btnRun = new Button();
            this.btnPrint = new Button();
            this.btnClose = new Button();
            this.pnlBody = new Panel();
            this.lblBar = new Label();
            this.lblEmpty = new Label();
            this.dgvReport = new DataGridView();
            this.colS1Period = new DataGridViewTextBoxColumn();
            this.colS1Inv = new DataGridViewTextBoxColumn();
            this.colS1Bill = new DataGridViewTextBoxColumn();
            this.colS1Disc = new DataGridViewTextBoxColumn();
            this.colS1Net = new DataGridViewTextBoxColumn();
            this.colS1Avg = new DataGridViewTextBoxColumn();

            ((ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────────────────────
            this.Text = "Report 1 — Purchase Summary by Period";
            this.Size = new Size(1100, 700);
            this.MinimumSize = new Size(900, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.Font = new Font("Segoe UI", 9f);

            // ── Header ───────────────────────────────────────────────────────────
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 62;
            this.pnlHeader.BackColor = Color.FromArgb(21, 101, 192); // ReportBase.Blue
            this.lblTitle.Text = "Purchase Summary by Period";
            this.lblTitle.Font = new Font("Segoe UI", 15f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(16, 13);
            this.lblSub.Text = "Group purchases by Daily / Weekly / Monthly and see spend totals";
            this.lblSub.Font = new Font("Segoe UI", 8.5f);
            this.lblSub.ForeColor = Color.FromArgb(187, 222, 251);
            this.lblSub.AutoSize = true;
            this.lblSub.Location = new Point(18, 40);
            this.pnlHeader.Controls.AddRange(new Control[] { this.lblTitle, this.lblSub });

            // ── Filter strip ─────────────────────────────────────────────────────
            this.pnlFilter.Dock = DockStyle.Top;
            this.pnlFilter.Height = 58;
            this.pnlFilter.BackColor = Color.White;

            this.lblFromCap.Text = "FROM DATE";
            this.lblFromCap.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            this.lblFromCap.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblFromCap.AutoSize = true;
            this.lblFromCap.Location = new Point(14, 8);

            this.dtpFrom.Location = new Point(14, 24);
            this.dtpFrom.Size = new Size(148, 26);
            this.dtpFrom.Font = new Font("Segoe UI", 10f);
            this.dtpFrom.Format = DateTimePickerFormat.Short;

            this.lblToCap.Text = "TO DATE";
            this.lblToCap.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            this.lblToCap.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblToCap.AutoSize = true;
            this.lblToCap.Location = new Point(174, 8);

            this.dtpTo.Location = new Point(174, 24);
            this.dtpTo.Size = new Size(148, 26);
            this.dtpTo.Font = new Font("Segoe UI", 10f);
            this.dtpTo.Format = DateTimePickerFormat.Short;

            this.lblGrpCap.Text = "GROUP BY";
            this.lblGrpCap.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            this.lblGrpCap.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblGrpCap.AutoSize = true;
            this.lblGrpCap.Location = new Point(334, 8);

            this.cmbGroup.Location = new Point(334, 24);
            this.cmbGroup.Size = new Size(130, 26);
            this.cmbGroup.Font = new Font("Segoe UI", 10f);
            this.cmbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbGroup.Items.AddRange(new object[] { "Daily", "Monthly", "Weekly" });

            // Run Button
            this.btnRun.Text = "▶  Run Report";
            this.btnRun.BackColor = Color.FromArgb(21, 101, 192);
            this.btnRun.ForeColor = Color.White;
            this.btnRun.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnRun.FlatStyle = FlatStyle.Flat;
            this.btnRun.Cursor = Cursors.Hand;
            this.btnRun.Location = new Point(480, 12);
            this.btnRun.Size = new Size(150, 34);
            this.btnRun.FlatAppearance.BorderSize = 0;

            // Print Button
            this.btnPrint.Text = "🖨  Print";
            this.btnPrint.BackColor = Color.FromArgb(80, 100, 110);
            this.btnPrint.ForeColor = Color.White;
            this.btnPrint.Font = new Font("Segoe UI", 10f);
            this.btnPrint.FlatStyle = FlatStyle.Flat;
            this.btnPrint.Cursor = Cursors.Hand;
            this.btnPrint.Location = new Point(640, 12);
            this.btnPrint.Size = new Size(110, 34);
            this.btnPrint.FlatAppearance.BorderSize = 0;

            // Close Button
            this.btnClose.Text = "✕  Close";
            this.btnClose.BackColor = Color.FromArgb(198, 40, 40); // ReportBase.Red
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Font = new Font("Segoe UI", 10f);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Size = new Size(110, 34);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Location = new Point(965, 12);

            this.pnlFilter.Controls.AddRange(new Control[] {
                this.lblFromCap, this.dtpFrom, this.lblToCap, this.dtpTo,
                this.lblGrpCap, this.cmbGroup, this.btnRun, this.btnPrint, this.btnClose });

            // ── Body ─────────────────────────────────────────────────────────────
            this.pnlBody.Dock = DockStyle.Fill;
            this.pnlBody.BackColor = Color.White;
            this.pnlBody.Padding = new Padding(14, 0, 14, 14);

            this.lblBar.Text = "  Select filters and click ▶ Run Report";
            this.lblBar.Dock = DockStyle.Top;
            this.lblBar.Height = 36;
            this.lblBar.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblBar.ForeColor = Color.White;
            this.lblBar.BackColor = Color.FromArgb(21, 101, 192);
            this.lblBar.TextAlign = ContentAlignment.MiddleLeft;

            this.lblEmpty.Text = "No data found for the selected period.";
            this.lblEmpty.Font = new Font("Segoe UI", 13f, FontStyle.Italic);
            this.lblEmpty.ForeColor = Color.FromArgb(150, 150, 150);
            this.lblEmpty.Dock = DockStyle.Fill;
            this.lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;

            // ── Grid ─────────────────────────────────────────────────────────────
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

            // Header style
            this.dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(21, 101, 192);
            this.dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(21, 101, 192);
            this.dgvReport.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            this.dgvReport.ColumnHeadersHeight = 40;
            this.dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cell style
            this.dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            this.dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(178, 235, 242);
            this.dgvReport.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 96, 100);

            // Alternating rows
            this.dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 253, 254);

            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.MultiSelect = false;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowTemplate.Height = 36;
            this.dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ── Columns ──────────────────────────────────────────────────────────
            this.colS1Period.Name = "colS1Period";
            this.colS1Period.HeaderText = "Period";
            this.colS1Period.FillWeight = 20f;
            this.colS1Period.ReadOnly = true;

            this.colS1Inv.Name = "colS1Inv";
            this.colS1Inv.HeaderText = "Invoices";
            this.colS1Inv.FillWeight = 10f;
            this.colS1Inv.ReadOnly = true;
            this.colS1Inv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.colS1Bill.Name = "colS1Bill";
            this.colS1Bill.HeaderText = "Total Bill (Rs.)";
            this.colS1Bill.FillWeight = 18f;
            this.colS1Bill.ReadOnly = true;
            this.colS1Bill.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colS1Bill.DefaultCellStyle.Format = "N2";
            this.colS1Bill.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colS1Disc.Name = "colS1Disc";
            this.colS1Disc.HeaderText = "Discount (Rs.)";
            this.colS1Disc.FillWeight = 14f;
            this.colS1Disc.ReadOnly = true;
            this.colS1Disc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colS1Disc.DefaultCellStyle.Format = "N2";
            this.colS1Disc.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colS1Net.Name = "colS1Net";
            this.colS1Net.HeaderText = "Net Spend (Rs.)";
            this.colS1Net.FillWeight = 18f;
            this.colS1Net.ReadOnly = true;
            this.colS1Net.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colS1Net.DefaultCellStyle.Format = "N2";
            this.colS1Net.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.colS1Net.DefaultCellStyle.ForeColor = Color.FromArgb(21, 101, 192);

            this.colS1Avg.Name = "colS1Avg";
            this.colS1Avg.HeaderText = "Avg Invoice (Rs.)";
            this.colS1Avg.FillWeight = 18f;
            this.colS1Avg.ReadOnly = true;
            this.colS1Avg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colS1Avg.DefaultCellStyle.Format = "N2";
            this.colS1Avg.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.dgvReport.Columns.AddRange(new DataGridViewColumn[] {
                this.colS1Period, this.colS1Inv, this.colS1Bill, this.colS1Disc,
                this.colS1Net, this.colS1Avg });

            this.pnlBody.Controls.AddRange(new Control[] { this.dgvReport, this.lblEmpty, this.lblBar });
            this.Controls.AddRange(new Control[] { this.pnlBody, this.pnlFilter, this.pnlHeader });

            ((ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }
    }
}