namespace POS_Shop.Views.Controllers.Supplier
{
    partial class DeletePaidPurchasesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlWarning = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWarningText = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.lblDateFieldCap = new System.Windows.Forms.Label();
            this.cmbDateField = new System.Windows.Forms.ComboBox();
            this.pnlDates = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblOr = new System.Windows.Forms.Label();
            this.chkOld = new System.Windows.Forms.CheckBox();
            this.lblPreview = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlWarning.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlDates.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWarning
            // 
            this.pnlWarning.BackColor = System.Drawing.Color.White;
            this.pnlWarning.Controls.Add(this.lblTitle);
            this.pnlWarning.Controls.Add(this.lblWarningText);
            this.pnlWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWarning.Location = new System.Drawing.Point(0, 0);
            this.pnlWarning.Name = "pnlWarning";
            this.pnlWarning.Size = new System.Drawing.Size(480, 130);
            this.pnlWarning.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(357, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "\"Warning\" Delete Paid Purchases";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarningText
            // 
            this.lblWarningText.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblWarningText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblWarningText.Location = new System.Drawing.Point(20, 58);
            this.lblWarningText.Name = "lblWarningText";
            this.lblWarningText.Size = new System.Drawing.Size(440, 60);
            this.lblWarningText.TabIndex = 1;
            this.lblWarningText.Text = "Using this form you can permanently delete PAID purchase records\r\nwithin a date r" +
    "ange, or those older than 1.5 months.\r\nThis action deletes ALL linked payment da" +
    "ta and cannot be undone.";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.lblDateFieldCap);
            this.pnlBody.Controls.Add(this.cmbDateField);
            this.pnlBody.Controls.Add(this.pnlDates);
            this.pnlBody.Controls.Add(this.lblOr);
            this.pnlBody.Controls.Add(this.chkOld);
            this.pnlBody.Controls.Add(this.lblPreview);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 130);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(24, 10, 24, 10);
            this.pnlBody.Size = new System.Drawing.Size(480, 270);
            this.pnlBody.TabIndex = 1;
            // 
            // lblDateFieldCap
            // 
            this.lblDateFieldCap.AutoSize = true;
            this.lblDateFieldCap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDateFieldCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblDateFieldCap.Location = new System.Drawing.Point(24, 14);
            this.lblDateFieldCap.Name = "lblDateFieldCap";
            this.lblDateFieldCap.Size = new System.Drawing.Size(108, 20);
            this.lblDateFieldCap.TabIndex = 0;
            this.lblDateFieldCap.Text = "Filter Date By:";
            // 
            // cmbDateField
            // 
            this.cmbDateField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateField.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbDateField.Items.AddRange(new object[] {
            "Purchase Date",
            "Payment Date"});
            this.cmbDateField.Location = new System.Drawing.Point(150, 10);
            this.cmbDateField.Name = "cmbDateField";
            this.cmbDateField.Size = new System.Drawing.Size(160, 29);
            this.cmbDateField.TabIndex = 1;
            // 
            // pnlDates
            // 
            this.pnlDates.BackColor = System.Drawing.Color.White;
            this.pnlDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDates.Controls.Add(this.lblFrom);
            this.pnlDates.Controls.Add(this.dtpFrom);
            this.pnlDates.Controls.Add(this.lblTo);
            this.pnlDates.Controls.Add(this.dtpTo);
            this.pnlDates.Location = new System.Drawing.Point(24, 48);
            this.pnlDates.Name = "pnlDates";
            this.pnlDates.Size = new System.Drawing.Size(430, 100);
            this.pnlDates.TabIndex = 2;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFrom.Location = new System.Drawing.Point(14, 18);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(93, 21);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "From Date:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(120, 14);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(290, 30);
            this.dtpFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTo.Location = new System.Drawing.Point(14, 58);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(72, 21);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "To Date:";
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(120, 54);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(290, 30);
            this.dtpTo.TabIndex = 3;
            // 
            // lblOr
            // 
            this.lblOr.AutoSize = true;
            this.lblOr.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblOr.Location = new System.Drawing.Point(200, 156);
            this.lblOr.Name = "lblOr";
            this.lblOr.Size = new System.Drawing.Size(73, 20);
            this.lblOr.TabIndex = 3;
            this.lblOr.Text = "--- OR ---";
            // 
            // chkOld
            // 
            this.chkOld.AutoSize = true;
            this.chkOld.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkOld.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.chkOld.Location = new System.Drawing.Point(24, 182);
            this.chkOld.Name = "chkOld";
            this.chkOld.Size = new System.Drawing.Size(346, 25);
            this.chkOld.TabIndex = 4;
            this.chkOld.Text = "Delete Paid Purchases Older Than 1.5 Months";
            // 
            // lblPreview
            // 
            this.lblPreview.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblPreview.Location = new System.Drawing.Point(24, 218);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(430, 30);
            this.lblPreview.TabIndex = 5;
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 400);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(480, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(110, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 36);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(250, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 36);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // DeletePaidPurchasesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 460);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlWarning);
            this.Controls.Add(this.pnlButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeletePaidPurchasesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Delete Paid Purchases";
            this.pnlWarning.ResumeLayout(false);
            this.pnlWarning.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlDates.ResumeLayout(false);
            this.pnlDates.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWarning, pnlBody, pnlDates, pnlButtons;
        private System.Windows.Forms.Label lblTitle, lblWarningText;
        private System.Windows.Forms.Label lblDateFieldCap, lblFrom, lblTo, lblOr, lblPreview;
        private System.Windows.Forms.ComboBox cmbDateField;
        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
        private System.Windows.Forms.CheckBox chkOld;
        private System.Windows.Forms.Button btnDelete, btnCancel;
    }
}