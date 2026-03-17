//namespace POS_Shop.Views.CustomerLoanScreensV1
//{
//    partial class Customerledgerreportform
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null)) components.Dispose();
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            this.pnlHeader = new System.Windows.Forms.Panel();
//            this.lblReportTitle = new System.Windows.Forms.Label();
//            this.lblDateRange = new System.Windows.Forms.Label();
//            this.lblEntries = new System.Windows.Forms.Label();
//            this.lblSummary = new System.Windows.Forms.Label();
//            this.pnlActions = new System.Windows.Forms.Panel();
//            this.PrintPreviewBtn = new System.Windows.Forms.Button();
//            this.PrintBtn = new System.Windows.Forms.Button();
//            this.ExportExcelBtn = new System.Windows.Forms.Button();
//            this.CloseBtn = new System.Windows.Forms.Button();

//            this.pnlHeader.SuspendLayout();
//            this.pnlActions.SuspendLayout();
//            this.SuspendLayout();

//            // Form
//            this.Text = "📊 Print / Export Ledger Report";
//            this.Size = new System.Drawing.Size(600, 320);
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.BackColor = System.Drawing.Color.White;
//            this.Load += new System.EventHandler(this.Customerledgerreportform_Load);

//            // Header
//            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlHeader.Height = 130;
//            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
//            this.pnlHeader.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);

//            this.lblReportTitle.AutoSize = false;
//            this.lblReportTitle.Dock = System.Windows.Forms.DockStyle.Top;
//            this.lblReportTitle.Height = 40;
//            this.lblReportTitle.Text = "Ledger Statement";
//            this.lblReportTitle.Font = new System.Drawing.Font("Segoe UI", 15, System.Drawing.FontStyle.Bold);
//            this.lblReportTitle.ForeColor = System.Drawing.Color.White;

//            this.lblDateRange.AutoSize = false;
//            this.lblDateRange.Dock = System.Windows.Forms.DockStyle.Top;
//            this.lblDateRange.Height = 26;
//            this.lblDateRange.Text = "";
//            this.lblDateRange.Font = new System.Drawing.Font("Segoe UI", 10);
//            this.lblDateRange.ForeColor = System.Drawing.Color.FromArgb(189, 195, 199);

//            this.lblEntries.AutoSize = false;
//            this.lblEntries.Dock = System.Windows.Forms.DockStyle.Top;
//            this.lblEntries.Height = 22;
//            this.lblEntries.Text = "";
//            this.lblEntries.Font = new System.Drawing.Font("Segoe UI", 9);
//            this.lblEntries.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);

//            this.lblSummary.AutoSize = false;
//            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.lblSummary.Text = "";
//            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 8);
//            this.lblSummary.ForeColor = System.Drawing.Color.FromArgb(189, 195, 199);

//            this.pnlHeader.Controls.AddRange(new System.Windows.Forms.Control[] {
//                this.lblReportTitle, this.lblDateRange, this.lblEntries, this.lblSummary });

//            // Actions panel
//            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlActions.Padding = new System.Windows.Forms.Padding(25, 25, 25, 25);
//            this.pnlActions.BackColor = System.Drawing.Color.White;

//            // Print Preview
//            this.PrintPreviewBtn.Location = new System.Drawing.Point(25, 25);
//            this.PrintPreviewBtn.Size = new System.Drawing.Size(520, 45);
//            this.PrintPreviewBtn.Text = "🔍  Print Preview (view before printing)";
//            this.PrintPreviewBtn.Font = new System.Drawing.Font("Segoe UI", 11);
//            this.PrintPreviewBtn.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
//            this.PrintPreviewBtn.ForeColor = System.Drawing.Color.White;
//            this.PrintPreviewBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.PrintPreviewBtn.FlatAppearance.BorderSize = 0;
//            this.PrintPreviewBtn.Click += new System.EventHandler(this.PrintPreviewBtn_Click);

//            // Print
//            this.PrintBtn.Location = new System.Drawing.Point(25, 78);
//            this.PrintBtn.Size = new System.Drawing.Size(250, 45);
//            this.PrintBtn.Text = "🖨️  Print Directly";
//            this.PrintBtn.Font = new System.Drawing.Font("Segoe UI", 11);
//            this.PrintBtn.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
//            this.PrintBtn.ForeColor = System.Drawing.Color.White;
//            this.PrintBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.PrintBtn.FlatAppearance.BorderSize = 0;
//            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);

//            // Export Excel
//            this.ExportExcelBtn.Location = new System.Drawing.Point(285, 78);
//            this.ExportExcelBtn.Size = new System.Drawing.Size(260, 45);
//            this.ExportExcelBtn.Text = "📊  Export to Excel (.xlsx)";
//            this.ExportExcelBtn.Font = new System.Drawing.Font("Segoe UI", 11);
//            this.ExportExcelBtn.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
//            this.ExportExcelBtn.ForeColor = System.Drawing.Color.White;
//            this.ExportExcelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.ExportExcelBtn.FlatAppearance.BorderSize = 0;
//            this.ExportExcelBtn.Click += new System.EventHandler(this.ExportExcelBtn_Click);

//            // Close
//            this.CloseBtn.Location = new System.Drawing.Point(435, 133);
//            this.CloseBtn.Size = new System.Drawing.Size(110, 34);
//            this.CloseBtn.Text = "Close";
//            this.CloseBtn.Font = new System.Drawing.Font("Segoe UI", 9);
//            this.CloseBtn.BackColor = System.Drawing.Color.White;
//            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);

//            this.pnlActions.Controls.AddRange(new System.Windows.Forms.Control[] {
//                this.PrintPreviewBtn, this.PrintBtn, this.ExportExcelBtn, this.CloseBtn });

//            this.Controls.AddRange(new System.Windows.Forms.Control[] {
//                this.pnlHeader, this.pnlActions });

//            this.pnlHeader.ResumeLayout(false);
//            this.pnlActions.ResumeLayout(false);
//            this.ResumeLayout(false);
//        }

//        private System.Windows.Forms.Panel pnlHeader;
//        private System.Windows.Forms.Label lblReportTitle;
//        private System.Windows.Forms.Label lblDateRange;
//        private System.Windows.Forms.Label lblEntries;
//        private System.Windows.Forms.Label lblSummary;
//        private System.Windows.Forms.Panel pnlActions;
//        private System.Windows.Forms.Button PrintPreviewBtn;
//        private System.Windows.Forms.Button PrintBtn;
//        private System.Windows.Forms.Button ExportExcelBtn;
//        private System.Windows.Forms.Button CloseBtn;
//    }
//}

namespace POS_Shop.Views.CustomerLoanScreensV1
{
    partial class Customerledgerreportform
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

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.lblEntries = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.PrintPreviewBtn = new System.Windows.Forms.Button();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.ExportExcelBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.pnlHeader.Controls.Add(this.lblReportTitle);
            this.pnlHeader.Controls.Add(this.lblDateRange);
            this.pnlHeader.Controls.Add(this.lblEntries);
            this.pnlHeader.Controls.Add(this.lblSummary);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.pnlHeader.Size = new System.Drawing.Size(650, 150);
            this.pnlHeader.TabIndex = 0;

            // lblReportTitle
            this.lblReportTitle.AutoSize = false;
            this.lblReportTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReportTitle.Font = new System.Drawing.Font("Segoe UI", 15, System.Drawing.FontStyle.Bold);
            this.lblReportTitle.ForeColor = System.Drawing.Color.White;
            this.lblReportTitle.Height = 40;
            this.lblReportTitle.Location = new System.Drawing.Point(25, 15);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(600, 40);
            this.lblReportTitle.TabIndex = 0;
            this.lblReportTitle.Text = "Ledger Statement";
            this.lblReportTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblDateRange
            this.lblDateRange.AutoSize = false;
            this.lblDateRange.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDateRange.Font = new System.Drawing.Font("Segoe UI", 10);
            this.lblDateRange.ForeColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.lblDateRange.Height = 26;
            this.lblDateRange.Location = new System.Drawing.Point(25, 55);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(600, 26);
            this.lblDateRange.TabIndex = 1;
            this.lblDateRange.Text = "";
            this.lblDateRange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblEntries
            this.lblEntries.AutoSize = false;
            this.lblEntries.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEntries.Font = new System.Drawing.Font("Segoe UI", 9);
            this.lblEntries.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.lblEntries.Height = 22;
            this.lblEntries.Location = new System.Drawing.Point(25, 81);
            this.lblEntries.Name = "lblEntries";
            this.lblEntries.Size = new System.Drawing.Size(600, 22);
            this.lblEntries.TabIndex = 2;
            this.lblEntries.Text = "";
            this.lblEntries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblSummary
            this.lblSummary.AutoSize = false;
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 8);
            this.lblSummary.ForeColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.lblSummary.Location = new System.Drawing.Point(25, 103);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(600, 32);
            this.lblSummary.TabIndex = 3;
            this.lblSummary.Text = "";
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // pnlActions
            this.pnlActions.BackColor = System.Drawing.Color.White;
            this.pnlActions.Controls.Add(this.PrintPreviewBtn);
            this.pnlActions.Controls.Add(this.PrintBtn);
            this.pnlActions.Controls.Add(this.ExportExcelBtn);
            this.pnlActions.Controls.Add(this.CloseBtn);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActions.Location = new System.Drawing.Point(0, 150);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(25, 25, 25, 15);
            this.pnlActions.Size = new System.Drawing.Size(650, 270);
            this.pnlActions.TabIndex = 1;

            // PrintPreviewBtn
            this.PrintPreviewBtn.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.PrintPreviewBtn.FlatAppearance.BorderSize = 0;
            this.PrintPreviewBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrintPreviewBtn.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            this.PrintPreviewBtn.ForeColor = System.Drawing.Color.White;
            this.PrintPreviewBtn.Location = new System.Drawing.Point(25, 25);
            this.PrintPreviewBtn.Name = "PrintPreviewBtn";
            this.PrintPreviewBtn.Size = new System.Drawing.Size(600, 45);
            this.PrintPreviewBtn.TabIndex = 0;
            this.PrintPreviewBtn.Text = "🔍  Print Preview (view before printing)";
            this.PrintPreviewBtn.UseVisualStyleBackColor = false;
            this.PrintPreviewBtn.Click += new System.EventHandler(this.PrintPreviewBtn_Click);

            // PrintBtn
            this.PrintBtn.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.PrintBtn.FlatAppearance.BorderSize = 0;
            this.PrintBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrintBtn.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            this.PrintBtn.ForeColor = System.Drawing.Color.White;
            this.PrintBtn.Location = new System.Drawing.Point(25, 82);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(290, 45);
            this.PrintBtn.TabIndex = 1;
            this.PrintBtn.Text = "🖨️  Print Directly";
            this.PrintBtn.UseVisualStyleBackColor = false;
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);

            // ExportExcelBtn
            this.ExportExcelBtn.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.ExportExcelBtn.FlatAppearance.BorderSize = 0;
            this.ExportExcelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportExcelBtn.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            this.ExportExcelBtn.ForeColor = System.Drawing.Color.White;
            this.ExportExcelBtn.Location = new System.Drawing.Point(325, 82);
            this.ExportExcelBtn.Name = "ExportExcelBtn";
            this.ExportExcelBtn.Size = new System.Drawing.Size(300, 45);
            this.ExportExcelBtn.TabIndex = 2;
            this.ExportExcelBtn.Text = "📊  Export to Excel (.xlsx)";
            this.ExportExcelBtn.UseVisualStyleBackColor = false;
            this.ExportExcelBtn.Click += new System.EventHandler(this.ExportExcelBtn_Click);

            // CloseBtn
            this.CloseBtn.BackColor = System.Drawing.Color.White;
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            this.CloseBtn.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.CloseBtn.Location = new System.Drawing.Point(520, 150);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(105, 35);
            this.CloseBtn.TabIndex = 3;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);

            // Customerledgerreportform
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(650, 420);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Customerledgerreportform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📊 Print / Export Ledger Report";
            this.pnlHeader.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblReportTitle;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.Label lblEntries;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button PrintPreviewBtn;
        private System.Windows.Forms.Button PrintBtn;
        private System.Windows.Forms.Button ExportExcelBtn;
        private System.Windows.Forms.Button CloseBtn;
    }
}