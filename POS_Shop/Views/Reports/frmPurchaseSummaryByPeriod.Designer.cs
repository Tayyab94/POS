using System;
using System.Windows.Forms;

namespace POS_Shop.Views.Reports
{
    partial class frmPurchaseSummaryByPeriod
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dtpStart = new DateTimePicker();
            this.dtpEnd = new DateTimePicker();
            this.lblStart = new Label();
            this.lblEnd = new Label();
            this.cmbPeriod = new ComboBox();
            this.lblPeriod = new Label();
            this.btnGenerate = new Button();
            this.dgvReport = new DataGridView();
            this.lblRecordCount = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            // dtpStart
            this.dtpStart.Location = new System.Drawing.Point(110, 22);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(140, 23);
            this.dtpStart.TabIndex = 0;

            // dtpEnd
            this.dtpEnd.Location = new System.Drawing.Point(110, 52);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(140, 23);
            this.dtpEnd.TabIndex = 1;

            // lblStart
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(12, 25);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(92, 15);
            this.lblStart.TabIndex = 2;
            this.lblStart.Text = "Start Date:";

            // lblEnd
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(12, 55);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(80, 15);
            this.lblEnd.TabIndex = 3;
            this.lblEnd.Text = "End Date:";

            // cmbPeriod
            this.cmbPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPeriod.FormattingEnabled = true;
            this.cmbPeriod.Items.AddRange(new object[] { "Daily", "Weekly", "Monthly" });
            this.cmbPeriod.Location = new System.Drawing.Point(110, 82);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new System.Drawing.Size(140, 23);
            this.cmbPeriod.TabIndex = 4;

            // lblPeriod
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(12, 85);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(92, 15);
            this.lblPeriod.TabIndex = 5;
            this.lblPeriod.Text = "Group By:";

            // btnGenerate
            this.btnGenerate.Location = new System.Drawing.Point(270, 50);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 55);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate Report";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new EventHandler(this.btnGenerate_Click);

            // dgvReport
            this.dgvReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(12, 120);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.RowTemplate.Height = 24;
            this.dgvReport.Size = new System.Drawing.Size(760, 380);
            this.dgvReport.TabIndex = 7;

            // lblRecordCount
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Location = new System.Drawing.Point(12, 506);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(0, 15);
            this.lblRecordCount.TabIndex = 8;

            // frmPurchaseSummaryByPeriod
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 531);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblPeriod);
            this.Controls.Add(this.cmbPeriod);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmPurchaseSummaryByPeriod";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Purchase Summary by Period";
            this.Load += new EventHandler(this.frmPurchaseSummaryByPeriod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Label lblStart;
        private Label lblEnd;
        private ComboBox cmbPeriod;
        private Label lblPeriod;
        private Button btnGenerate;
        private DataGridView dgvReport;
        private Label lblRecordCount;
    }
}