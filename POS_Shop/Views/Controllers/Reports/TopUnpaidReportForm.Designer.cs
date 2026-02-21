using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    partial class TopUnpaidReportForm
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
            this.lblNote = new Label();
            this.btnRun = new Button();
            this.btnPrint = new Button();
            this.btnClose = new Button();
            this.pnlGrid = new Panel();
            this.lblBar = new Label();
            this.lblEmpty = new Label();
            this.dgvReport = new DataGridView();
            this.colTURank = new DataGridViewTextBoxColumn();
            this.colTUSupplier = new DataGridViewTextBoxColumn();
            this.colTUContact = new DataGridViewTextBoxColumn();
            this.colTUOpenInv = new DataGridViewTextBoxColumn();
            this.colTUOwed = new DataGridViewTextBoxColumn();
            this.colTUOldest = new DataGridViewTextBoxColumn();
            this.colTUPending = new DataGridViewTextBoxColumn();
            this.colTUPartial = new DataGridViewTextBoxColumn();

            ((ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────────────────────
            this.Text = "Report — Top Unpaid Suppliers";
            this.Size = new Size(1200, 680);
            this.MinimumSize = new Size(1000, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.Font = new Font("Segoe UI", 9f);

            // ── Header ───────────────────────────────────────────────────────────
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 64;
            this.pnlHeader.BackColor = Color.FromArgb(198, 40, 40); // acc
            this.lblTitle.Text = "Top Unpaid Suppliers";
            this.lblTitle.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(16, 10);
            this.lblSub.Text = "Ranked by outstanding balance  ·  Real-time as of TODAY  ·  Who do you owe the most?";
            this.lblSub.Font = new Font("Segoe UI", 9f);
            this.lblSub.ForeColor = Color.FromArgb(255, 205, 210);
            this.lblSub.AutoSize = true;
            this.lblSub.Location = new Point(18, 40);
            this.pnlHeader.Controls.AddRange(new Control[] { this.lblTitle, this.lblSub });

            // ── Action bar ───────────────────────────────────────────────────────
            this.pnlBar.Dock = DockStyle.Top;
            this.pnlBar.Height = 56;
            this.pnlBar.BackColor = Color.White;

            this.lblNote.Text = "No date filter needed — shows all currently open balances in real time.";
            this.lblNote.Font = new Font("Segoe UI", 9.5f, FontStyle.Italic);
            this.lblNote.ForeColor = Color.FromArgb(120, 144, 156);
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new Point(14, 20);

            this.btnRun.Text = "▶  Run Report";
            this.btnRun.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnRun.ForeColor = Color.White;
            this.btnRun.BackColor = Color.FromArgb(198, 40, 40);
            this.btnRun.FlatStyle = FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = Cursors.Hand;
            this.btnRun.Size = new Size(150, 36);
            this.btnRun.Location = new Point(620, 10);

            this.btnPrint.Text = "🖨  Print";
            this.btnPrint.Font = new Font("Segoe UI", 10f);
            this.btnPrint.ForeColor = Color.White;
            this.btnPrint.BackColor = Color.FromArgb(80, 100, 110);
            this.btnPrint.FlatStyle = FlatStyle.Flat;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.Cursor = Cursors.Hand;
            this.btnPrint.Size = new Size(110, 36);
            this.btnPrint.Location = new Point(780, 10);

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

            this.pnlBar.Controls.AddRange(new Control[] { this.lblNote, this.btnRun, this.btnPrint, this.btnClose });

            // ── Grid panel ───────────────────────────────────────────────────────
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = Color.White;
            this.pnlGrid.Padding = new Padding(14, 0, 14, 14);

            this.lblBar.Text = "  Click 'Run Report' to load current outstanding payables";
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
            this.colTURank.Name = "colTURank";
            this.colTURank.HeaderText = "#";
            this.colTURank.FillWeight = 4f;
            this.colTURank.ReadOnly = true;
            this.colTURank.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colTURank.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colTUSupplier.Name = "colTUSupplier";
            this.colTUSupplier.HeaderText = "Supplier";
            this.colTUSupplier.FillWeight = 22f;
            this.colTUSupplier.ReadOnly = true;

            this.colTUContact.Name = "colTUContact";
            this.colTUContact.HeaderText = "Contact";
            this.colTUContact.FillWeight = 11f;
            this.colTUContact.ReadOnly = true;

            this.colTUOpenInv.Name = "colTUOpenInv";
            this.colTUOpenInv.HeaderText = "Open Inv.";
            this.colTUOpenInv.FillWeight = 7f;
            this.colTUOpenInv.ReadOnly = true;
            this.colTUOpenInv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colTUOpenInv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colTUOwed.Name = "colTUOwed";
            this.colTUOwed.HeaderText = "Total Owed";
            this.colTUOwed.FillWeight = 16f;
            this.colTUOwed.ReadOnly = true;
            this.colTUOwed.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colTUOwed.DefaultCellStyle.Format = "N2";
            this.colTUOwed.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colTUOldest.Name = "colTUOldest";
            this.colTUOldest.HeaderText = "Oldest Inv.";
            this.colTUOldest.FillWeight = 10f;
            this.colTUOldest.ReadOnly = true;
            this.colTUOldest.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colTUOldest.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colTUPending.Name = "colTUPending";
            this.colTUPending.HeaderText = "Pending";
            this.colTUPending.FillWeight = 8f;
            this.colTUPending.ReadOnly = true;
            this.colTUPending.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colTUPending.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.colTUPartial.Name = "colTUPartial";
            this.colTUPartial.HeaderText = "Partial";
            this.colTUPartial.FillWeight = 8f;
            this.colTUPartial.ReadOnly = true;
            this.colTUPartial.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.colTUPartial.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            this.dgvReport.Columns.AddRange(new DataGridViewColumn[] {
                this.colTURank, this.colTUSupplier, this.colTUContact, this.colTUOpenInv,
                this.colTUOwed, this.colTUOldest, this.colTUPending, this.colTUPartial });

            this.pnlGrid.Controls.AddRange(new Control[] { this.dgvReport, this.lblEmpty, this.lblBar });
            this.Controls.AddRange(new Control[] { this.pnlGrid, this.pnlBar, this.pnlHeader });

            ((ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }

        // Fields
        private Panel pnlHeader, pnlBar, pnlGrid;
        private Label lblTitle, lblSub, lblNote, lblBar, lblEmpty;
        private Button btnRun, btnPrint, btnClose;
        private DataGridView dgvReport;
        private DataGridViewTextBoxColumn colTURank, colTUSupplier, colTUContact, colTUOpenInv, colTUOwed, colTUOldest, colTUPending, colTUPartial;
    }
}