namespace POS_Shop.Views.CustomerLoanScreensV1
{
    partial class Customerpaymentform
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCurrentBalance = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.lblAmountHint = new System.Windows.Forms.Label();
            this.AmountTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.pnlReferenceNo = new System.Windows.Forms.Panel();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.pnlAfterBalance = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAfterBalance = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlReferenceNo.SuspendLayout();
            this.pnlAfterBalance.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.pnlHeader.Controls.Add(this.lblFormTitle);
            this.pnlHeader.Controls.Add(this.lblCustomerName);
            this.pnlHeader.Controls.Add(this.lblCurrentBalance);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlHeader.Size = new System.Drawing.Size(462, 101);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(20, 34);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(422, 38);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Receive Loan Payment";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.lblCustomerName.Location = new System.Drawing.Point(20, 10);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(422, 24);
            this.lblCustomerName.TabIndex = 1;
            // 
            // lblCurrentBalance
            // 
            this.lblCurrentBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentBalance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCurrentBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.lblCurrentBalance.Location = new System.Drawing.Point(20, 10);
            this.lblCurrentBalance.Name = "lblCurrentBalance";
            this.lblCurrentBalance.Size = new System.Drawing.Size(422, 81);
            this.lblCurrentBalance.TabIndex = 2;
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.lblAmountHint);
            this.pnlBody.Controls.Add(this.AmountTxt);
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Controls.Add(this.cmbPaymentMethod);
            this.pnlBody.Controls.Add(this.pnlReferenceNo);
            this.pnlBody.Controls.Add(this.label4);
            this.pnlBody.Controls.Add(this.txtNote);
            this.pnlBody.Controls.Add(this.pnlAfterBalance);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(25, 20, 25, 10);
            this.pnlBody.Size = new System.Drawing.Size(462, 469);
            this.pnlBody.TabIndex = 1;
            // 
            // lblAmountHint
            // 
            this.lblAmountHint.AutoSize = true;
            this.lblAmountHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAmountHint.ForeColor = System.Drawing.Color.Gray;
            this.lblAmountHint.Location = new System.Drawing.Point(23, 118);
            this.lblAmountHint.Name = "lblAmountHint";
            this.lblAmountHint.Size = new System.Drawing.Size(193, 20);
            this.lblAmountHint.TabIndex = 0;
            this.lblAmountHint.Text = "Amount customer is paying:";
            // 
            // AmountTxt
            // 
            this.AmountTxt.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.AmountTxt.Location = new System.Drawing.Point(23, 140);
            this.AmountTxt.Name = "AmountTxt";
            this.AmountTxt.Size = new System.Drawing.Size(410, 47);
            this.AmountTxt.TabIndex = 1;
            this.AmountTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AmountTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AmountTxt_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(23, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Payment Method:";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPaymentMethod.Location = new System.Drawing.Point(23, 213);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(200, 31);
            this.cmbPaymentMethod.TabIndex = 3;
            // 
            // pnlReferenceNo
            // 
            this.pnlReferenceNo.Controls.Add(this.lblReferenceNo);
            this.pnlReferenceNo.Controls.Add(this.txtReferenceNo);
            this.pnlReferenceNo.Location = new System.Drawing.Point(239, 193);
            this.pnlReferenceNo.Name = "pnlReferenceNo";
            this.pnlReferenceNo.Size = new System.Drawing.Size(195, 55);
            this.pnlReferenceNo.TabIndex = 4;
            this.pnlReferenceNo.Visible = false;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReferenceNo.Location = new System.Drawing.Point(0, 0);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(166, 20);
            this.lblReferenceNo.TabIndex = 0;
            this.lblReferenceNo.Text = "Reference / Cheque No:";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReferenceNo.Location = new System.Drawing.Point(0, 20);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(195, 30);
            this.txtReferenceNo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(19, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Note (optional):";
            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNote.Location = new System.Drawing.Point(19, 286);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNote.Size = new System.Drawing.Size(410, 70);
            this.txtNote.TabIndex = 6;
            // 
            // pnlAfterBalance
            // 
            this.pnlAfterBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlAfterBalance.Controls.Add(this.label5);
            this.pnlAfterBalance.Controls.Add(this.lblAfterBalance);
            this.pnlAfterBalance.Location = new System.Drawing.Point(19, 379);
            this.pnlAfterBalance.Name = "pnlAfterBalance";
            this.pnlAfterBalance.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlAfterBalance.Size = new System.Drawing.Size(410, 60);
            this.pnlAfterBalance.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(12, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Balance after this entry:";
            // 
            // lblAfterBalance
            // 
            this.lblAfterBalance.AutoSize = true;
            this.lblAfterBalance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAfterBalance.ForeColor = System.Drawing.Color.Gray;
            this.lblAfterBalance.Location = new System.Drawing.Point(12, 28);
            this.lblAfterBalance.Name = "lblAfterBalance";
            this.lblAfterBalance.Size = new System.Drawing.Size(20, 25);
            this.lblAfterBalance.TabIndex = 1;
            this.lblAfterBalance.Text = "-";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlFooter.Controls.Add(this.CancelBtn);
            this.pnlFooter.Controls.Add(this.SaveBtn);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 469);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlFooter.Size = new System.Drawing.Size(462, 65);
            this.pnlFooter.TabIndex = 2;
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.White;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBtn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CancelBtn.Location = new System.Drawing.Point(290, 12);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(80, 38);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.SaveBtn.FlatAppearance.BorderSize = 0;
            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.SaveBtn.ForeColor = System.Drawing.Color.White;
            this.SaveBtn.Location = new System.Drawing.Point(380, 12);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(65, 38);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // Customerpaymentform
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(462, 534);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Customerpaymentform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Receive Payment";
            this.Load += new System.EventHandler(this.Customerpaymentform_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlReferenceNo.ResumeLayout(false);
            this.pnlReferenceNo.PerformLayout();
            this.pnlAfterBalance.ResumeLayout(false);
            this.pnlAfterBalance.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // Controls
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCurrentBalance;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblAmountHint;
        private System.Windows.Forms.TextBox AmountTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Panel pnlReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Panel pnlAfterBalance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAfterBalance;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
    }
}