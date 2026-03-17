namespace POS_Shop.Views.CustomerLoanScreensV1
{
    partial class AdjustmentForm
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
            this.label0 = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCurrentBalance = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAdjType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.pnlHeader.Controls.Add(this.label0);
            this.pnlHeader.Controls.Add(this.lblCustomerName);
            this.pnlHeader.Controls.Add(this.lblCurrentBalance);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.pnlHeader.Size = new System.Drawing.Size(462, 71);
            this.pnlHeader.TabIndex = 0;
            // 
            // label0
            // 
            this.label0.Dock = System.Windows.Forms.DockStyle.Top;
            this.label0.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label0.ForeColor = System.Drawing.Color.White;
            this.label0.Location = new System.Drawing.Point(20, 20);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(422, 36);
            this.label0.TabIndex = 0;
            this.label0.Text = "⚙️ Manual Ledger Adjustment";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.lblCustomerName.Location = new System.Drawing.Point(20, 8);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(422, 12);
            this.lblCustomerName.TabIndex = 1;
            // 
            // lblCurrentBalance
            // 
            this.lblCurrentBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentBalance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCurrentBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblCurrentBalance.Location = new System.Drawing.Point(20, 8);
            this.lblCurrentBalance.Name = "lblCurrentBalance";
            this.lblCurrentBalance.Size = new System.Drawing.Size(422, 55);
            this.lblCurrentBalance.TabIndex = 2;
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.label1);
            this.pnlBody.Controls.Add(this.cmbAdjType);
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Controls.Add(this.txtAmount);
            this.pnlBody.Controls.Add(this.label3);
            this.pnlBody.Controls.Add(this.txtReason);
            this.pnlBody.Controls.Add(this.pnlPreview);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(462, 348);
            this.pnlBody.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adjustment Type:";
            // 
            // cmbAdjType
            // 
            this.cmbAdjType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdjType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAdjType.Location = new System.Drawing.Point(25, 40);
            this.cmbAdjType.Name = "cmbAdjType";
            this.cmbAdjType.Size = new System.Drawing.Size(410, 31);
            this.cmbAdjType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(25, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amount (PKR):";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.txtAmount.Location = new System.Drawing.Point(25, 110);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(410, 43);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(25, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Reason (required):";
            // 
            // txtReason
            // 
            this.txtReason.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReason.Location = new System.Drawing.Point(25, 185);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReason.Size = new System.Drawing.Size(410, 70);
            this.txtReason.TabIndex = 5;
            // 
            // pnlPreview
            // 
            this.pnlPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlPreview.Controls.Add(this.label4);
            this.pnlPreview.Controls.Add(this.lblPreview);
            this.pnlPreview.Location = new System.Drawing.Point(25, 270);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(410, 55);
            this.pnlPreview.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(12, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "After adjustment:";
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPreview.ForeColor = System.Drawing.Color.Gray;
            this.lblPreview.Location = new System.Drawing.Point(12, 26);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(20, 25);
            this.lblPreview.TabIndex = 1;
            this.lblPreview.Text = "-";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlFooter.Controls.Add(this.CancelBtn);
            this.pnlFooter.Controls.Add(this.SaveBtn);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 348);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(462, 65);
            this.pnlFooter.TabIndex = 2;
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.White;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CancelBtn.Location = new System.Drawing.Point(290, 14);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(80, 36);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.SaveBtn.FlatAppearance.BorderSize = 0;
            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.SaveBtn.ForeColor = System.Drawing.Color.White;
            this.SaveBtn.Location = new System.Drawing.Point(378, 14);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(78, 36);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "💾 Save";
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // AdjustmentForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(462, 413);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdjustmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "⚙️ Manual Adjustment";
            this.Load += new System.EventHandler(this.AdjustmentForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlPreview.ResumeLayout(false);
            this.pnlPreview.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCurrentBalance;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAdjType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
    }
}