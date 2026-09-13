namespace POS_Shop.Views.Controllers.Reports
{
    partial class SalesProfitReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesProfitReport));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.lblGrpBy = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblBar = new System.Windows.Forms.Label();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.colSalPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalOrders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalProfit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalAvg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1100, 72);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.lblSubtitle.Location = new System.Drawing.Point(21, 42);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(800, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Total orders and revenue grouped by Daily / Weekly / Monthly  ·  Cash vs Credit b" +
    "reakdown  ·  Actual Bill & Profit";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(700, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sales Summary Report";
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.Controls.Add(this.lblGrpBy);
            this.pnlControls.Controls.Add(this.cmbGroup);
            this.pnlControls.Controls.Add(this.lblFrom);
            this.pnlControls.Controls.Add(this.dtpFrom);
            this.pnlControls.Controls.Add(this.lblTo);
            this.pnlControls.Controls.Add(this.dtpTo);
            this.pnlControls.Controls.Add(this.btnRun);
            this.pnlControls.Controls.Add(this.btnPrint);
            this.pnlControls.Controls.Add(this.btnClose);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 72);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1100, 60);
            this.pnlControls.TabIndex = 1;
            // 
            // lblGrpBy
            // 
            this.lblGrpBy.AutoSize = true;
            this.lblGrpBy.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblGrpBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblGrpBy.Location = new System.Drawing.Point(14, 10);
            this.lblGrpBy.Name = "lblGrpBy";
            this.lblGrpBy.Size = new System.Drawing.Size(80, 19);
            this.lblGrpBy.TabIndex = 0;
            this.lblGrpBy.Text = "GROUP BY";
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Items.AddRange(new object[] {
            "Monthly",
            "Weekly",
            "Daily"});
            this.cmbGroup.Location = new System.Drawing.Point(14, 28);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(110, 29);
            this.cmbGroup.TabIndex = 1;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblFrom.Location = new System.Drawing.Point(140, 10);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(49, 19);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "FROM";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(140, 28);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(130, 29);
            this.dtpFrom.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblTo.Location = new System.Drawing.Point(286, 10);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 19);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "TO";
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(286, 28);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(130, 29);
            this.dtpTo.TabIndex = 5;
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(434, 18);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(130, 34);
            this.btnRun.TabIndex = 6;
            this.btnRun.Text = "Run Report";
            this.btnRun.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(574, 18);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 34);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(674, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 34);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // lblBar
            // 
            this.lblBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.lblBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBar.ForeColor = System.Drawing.Color.White;
            this.lblBar.Location = new System.Drawing.Point(0, 132);
            this.lblBar.Name = "lblBar";
            this.lblBar.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblBar.Size = new System.Drawing.Size(1100, 30);
            this.lblBar.TabIndex = 2;
            this.lblBar.Text = "  Run a report to see results.";
            this.lblBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSalPeriod,
            this.colSalOrders,
            this.colSalRevenue,
            this.colSalCash,
            this.colSalCredit,
            this.colSalActual,
            this.colSalProfit,
            this.colSalAvg});
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Location = new System.Drawing.Point(0, 162);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.RowTemplate.Height = 28;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1100, 438);
            this.dgvReport.TabIndex = 3;
            // 
            // colSalPeriod
            // 
            this.colSalPeriod.HeaderText = "Period";
            this.colSalPeriod.MinimumWidth = 120;
            this.colSalPeriod.Name = "colSalPeriod";
            this.colSalPeriod.ReadOnly = true;
            // 
            // colSalOrders
            // 
            this.colSalOrders.HeaderText = "Orders";
            this.colSalOrders.MinimumWidth = 60;
            this.colSalOrders.Name = "colSalOrders";
            this.colSalOrders.ReadOnly = true;
            // 
            // colSalRevenue
            // 
            this.colSalRevenue.HeaderText = "Revenue (Rs.)";
            this.colSalRevenue.MinimumWidth = 100;
            this.colSalRevenue.Name = "colSalRevenue";
            this.colSalRevenue.ReadOnly = true;
            // 
            // colSalCash
            // 
            this.colSalCash.HeaderText = "Cash (Rs.)";
            this.colSalCash.MinimumWidth = 90;
            this.colSalCash.Name = "colSalCash";
            this.colSalCash.ReadOnly = true;
            // 
            // colSalCredit
            // 
            this.colSalCredit.HeaderText = "Credit/Other (Rs.)";
            this.colSalCredit.MinimumWidth = 110;
            this.colSalCredit.Name = "colSalCredit";
            this.colSalCredit.ReadOnly = true;
            // 
            // colSalActual
            // 
            this.colSalActual.HeaderText = "Actual Bill (Rs.)";
            this.colSalActual.MinimumWidth = 100;
            this.colSalActual.Name = "colSalActual";
            this.colSalActual.ReadOnly = true;
            // 
            // colSalProfit
            // 
            this.colSalProfit.HeaderText = "Profit (Rs.)";
            this.colSalProfit.MinimumWidth = 90;
            this.colSalProfit.Name = "colSalProfit";
            this.colSalProfit.ReadOnly = true;
            // 
            // colSalAvg
            // 
            this.colSalAvg.HeaderText = "Avg Order (Rs.)";
            this.colSalAvg.MinimumWidth = 100;
            this.colSalAvg.Name = "colSalAvg";
            this.colSalAvg.ReadOnly = true;
            // 
            // lblEmpty
            // 
            this.lblEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmpty.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblEmpty.ForeColor = System.Drawing.Color.Silver;
            this.lblEmpty.Location = new System.Drawing.Point(0, 162);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(1100, 438);
            this.lblEmpty.TabIndex = 4;
            this.lblEmpty.Text = "No data found for the selected date range.";
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            // 
            // SalesProfitReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.lblEmpty);
            this.Controls.Add(this.lblBar);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "SalesProfitReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report — Sales Profit Summary";
            this.pnlHeader.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        // ── Field declarations ────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Label lblGrpBy;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblBar;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalRevenue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalProfit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalAvg;
        #endregion
    }
}