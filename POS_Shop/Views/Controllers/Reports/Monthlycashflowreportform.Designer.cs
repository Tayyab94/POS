using System;

namespace POS_Shop.Views.Controllers.Reports
{
    partial class MonthlyCashFlowReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Declare form controls as class fields
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblFromCap;
        private System.Windows.Forms.Label lblToCap;
        private System.Windows.Forms.Label lblBar;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.Label lblTotalObligations;
        private System.Windows.Forms.Label lblTotalPaid;
        private System.Windows.Forms.Label lblTotalDiff;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCFMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCFOblig;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCFPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCFDiff;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCFCumOblig;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCFCumPaid;

        private void InitializeComponent()
        {
            // Initialize components
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
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblTotalObligations = new System.Windows.Forms.Label();
            this.lblTotalPaid = new System.Windows.Forms.Label();
            this.lblTotalDiff = new System.Windows.Forms.Label();

            this.colCFMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCFOblig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCFPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCFDiff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCFCumOblig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCFCumPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();

            // Define colors as static values (designer friendly)
            System.Drawing.Color primaryColor = System.Drawing.Color.FromArgb(0, 105, 92);
            System.Drawing.Color primaryLight = System.Drawing.Color.FromArgb(178, 223, 219);
            System.Drawing.Color white = System.Drawing.Color.White;
            System.Drawing.Color textSecondary = System.Drawing.Color.FromArgb(127, 140, 141);
            System.Drawing.Color borderColor = System.Drawing.Color.FromArgb(236, 240, 241);
            System.Drawing.Color successColor = System.Drawing.Color.FromArgb(39, 174, 96);

            // Form
            this.Text = "Monthly Cash Flow Report";
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(245, 248, 250);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // Header Panel
            this.pnlHeader.BackColor = primaryColor;
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 80;
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);

            this.lblTitle.Text = "Monthly Cash Flow Statement";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20f, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = white;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(500, 37);

            this.lblSub.Text = "Track purchase obligations vs payments made";
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.lblSub.ForeColor = primaryLight;
            this.lblSub.Location = new System.Drawing.Point(20, 52);
            this.lblSub.Size = new System.Drawing.Size(500, 19);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSub);

            // Filter Panel
            this.pnlFilter.BackColor = white;
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 90;
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(20, 15, 20, 0);

            this.lblFromCap.Text = "FROM DATE";
            this.lblFromCap.Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
            this.lblFromCap.ForeColor = textSecondary;
            this.lblFromCap.Location = new System.Drawing.Point(20, 20);
            this.lblFromCap.Size = new System.Drawing.Size(70, 13);

            this.dtpFrom.Location = new System.Drawing.Point(20, 36);
            this.dtpFrom.Size = new System.Drawing.Size(170, 27);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Value = new System.DateTime(DateTime.Now.Year, 1, 1);

            this.lblToCap.Text = "TO DATE";
            this.lblToCap.Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
            this.lblToCap.ForeColor = textSecondary;
            this.lblToCap.Location = new System.Drawing.Point(200, 20);
            this.lblToCap.Size = new System.Drawing.Size(70, 13);

            this.dtpTo.Location = new System.Drawing.Point(200, 36);
            this.dtpTo.Size = new System.Drawing.Size(170, 27);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Value = DateTime.Now;

            this.btnRun.Text = "Generate Report";
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = white;
            this.btnRun.BackColor = primaryColor;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Size = new System.Drawing.Size(150, 35);
            this.btnRun.Location = new System.Drawing.Point(390, 30);
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.btnPrint.Text = "Export PDF";
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnPrint.BackColor = white;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.FlatAppearance.BorderColor = borderColor;
            this.btnPrint.FlatAppearance.BorderSize = 1;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Size = new System.Drawing.Size(120, 35);
            this.btnPrint.Location = new System.Drawing.Point(550, 30);
            this.btnPrint.UseVisualStyleBackColor = false;

            this.btnClose.Text = "Close";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnClose.BackColor = white;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderColor = borderColor;
            this.btnClose.FlatAppearance.BorderSize = 1;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Size = new System.Drawing.Size(90, 35);
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(1070, 30);
            this.btnClose.UseVisualStyleBackColor = false;

            this.pnlFilter.Controls.Add(this.lblFromCap);
            this.pnlFilter.Controls.Add(this.dtpFrom);
            this.pnlFilter.Controls.Add(this.lblToCap);
            this.pnlFilter.Controls.Add(this.dtpTo);
            this.pnlFilter.Controls.Add(this.btnRun);
            this.pnlFilter.Controls.Add(this.btnPrint);
            this.pnlFilter.Controls.Add(this.btnClose);

            // Summary Panel (Bottom)
            this.pnlSummary.BackColor = white;
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummary.Height = 60;
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;

            this.lblTotalObligations.Text = "Total Obligations: Rs. 0.00";
            this.lblTotalObligations.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Regular);
            this.lblTotalObligations.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTotalObligations.Location = new System.Drawing.Point(20, 20);
            this.lblTotalObligations.Size = new System.Drawing.Size(200, 20);
            this.lblTotalObligations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblTotalPaid.Text = "Total Paid: Rs. 0.00";
            this.lblTotalPaid.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
            this.lblTotalPaid.ForeColor = successColor;
            this.lblTotalPaid.Location = new System.Drawing.Point(240, 20);
            this.lblTotalPaid.Size = new System.Drawing.Size(200, 20);
            this.lblTotalPaid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblTotalDiff.Text = "Difference: Rs. 0.00";
            this.lblTotalDiff.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
            this.lblTotalDiff.ForeColor = primaryColor;
            this.lblTotalDiff.Location = new System.Drawing.Point(460, 20);
            this.lblTotalDiff.Size = new System.Drawing.Size(200, 20);
            this.lblTotalDiff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.pnlSummary.Controls.Add(this.lblTotalObligations);
            this.pnlSummary.Controls.Add(this.lblTotalPaid);
            this.pnlSummary.Controls.Add(this.lblTotalDiff);

            // Grid Panel
            this.pnlGrid.BackColor = white;
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);

            // Status Bar
            this.lblBar.Text = "  Ready • Select date range and click Generate Report";
            this.lblBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBar.Height = 35;
            this.lblBar.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lblBar.ForeColor = textSecondary;
            this.lblBar.BackColor = System.Drawing.Color.FromArgb(249, 251, 253);
            this.lblBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBar.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // Empty State
            this.lblEmpty.Text = "No data available for the selected period";
            this.lblEmpty.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Italic);
            this.lblEmpty.ForeColor = textSecondary;
            this.lblEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;

            // DataGridView
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.ReadOnly = true;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = white;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReport.GridColor = borderColor;
            this.dgvReport.ColumnHeadersHeight = 45;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowTemplate.Height = 40;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // Column Header Style
            this.dgvReport.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvReport.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            this.dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = white;
            this.dgvReport.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.dgvReport.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            // Default Cell Style
            this.dgvReport.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvReport.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.dgvReport.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.dgvReport.DefaultCellStyle.SelectionBackColor = primaryLight;
            this.dgvReport.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(0, 77, 64);
            this.dgvReport.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);

            // Alternating Row Style
            this.dgvReport.AlternatingRowsDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvReport.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 251, 253);

            // Columns
            this.colCFMonth.HeaderText = "Month";
            this.colCFMonth.Name = "colCFMonth";
            this.colCFMonth.FillWeight = 15f;

            this.colCFOblig.HeaderText = "Obligations (Rs.)";
            this.colCFOblig.Name = "colCFOblig";
            this.colCFOblig.FillWeight = 17f;
            this.colCFOblig.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCFOblig.DefaultCellStyle.Format = "N2";

            this.colCFPaid.HeaderText = "Paid (Rs.)";
            this.colCFPaid.Name = "colCFPaid";
            this.colCFPaid.FillWeight = 17f;
            this.colCFPaid.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCFPaid.DefaultCellStyle.Format = "N2";
            this.colCFPaid.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.colCFPaid.DefaultCellStyle.ForeColor = primaryColor;

            this.colCFDiff.HeaderText = "Difference (Rs.)";
            this.colCFDiff.Name = "colCFDiff";
            this.colCFDiff.FillWeight = 17f;
            this.colCFDiff.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCFDiff.DefaultCellStyle.Format = "N2";

            this.colCFCumOblig.HeaderText = "Cumulative Obligations";
            this.colCFCumOblig.Name = "colCFCumOblig";
            this.colCFCumOblig.FillWeight = 17f;
            this.colCFCumOblig.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCFCumOblig.DefaultCellStyle.Format = "N2";
            this.colCFCumOblig.DefaultCellStyle.ForeColor = textSecondary;

            this.colCFCumPaid.HeaderText = "Cumulative Paid";
            this.colCFCumPaid.Name = "colCFCumPaid";
            this.colCFCumPaid.FillWeight = 17f;
            this.colCFCumPaid.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCFCumPaid.DefaultCellStyle.Format = "N2";
            this.colCFCumPaid.DefaultCellStyle.ForeColor = textSecondary;

            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colCFMonth,
                this.colCFOblig,
                this.colCFPaid,
                this.colCFDiff,
                this.colCFCumOblig,
                this.colCFCumPaid
            });

            // Add controls to grid panel
            this.pnlGrid.Controls.Add(this.dgvReport);
            this.pnlGrid.Controls.Add(this.lblEmpty);
            this.pnlGrid.Controls.Add(this.lblBar);

            // Add all controls to form
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);

            // Resume layouts
            this.pnlHeader.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.pnlSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }
    }
}