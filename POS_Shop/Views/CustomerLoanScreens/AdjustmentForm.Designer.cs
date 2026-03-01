namespace POS_Shop.Views.CustomerLoanScreens
{
    partial class AdjustmentForm
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
            // Initialize all controls first
            this.lblCust = new System.Windows.Forms.Label();
            this.lblBal = new System.Windows.Forms.Label();
            this.rbDebit = new System.Windows.Forms.RadioButton();
            this.rbCredit = new System.Windows.Forms.RadioButton();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpDirection = new System.Windows.Forms.GroupBox();
            
            // Initialize label controls
            this.lblCustomerLabel = new System.Windows.Forms.Label();
            this.lblBalanceLabel = new System.Windows.Forms.Label();
            this.lblAmountLabel = new System.Windows.Forms.Label();
            this.lblNotesLabel = new System.Windows.Forms.Label();
            
            this.grpDirection.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblCustomerLabel
            // 
            this.lblCustomerLabel.AutoSize = false;
            this.lblCustomerLabel.Location = new System.Drawing.Point(20, 20);
            this.lblCustomerLabel.Name = "lblCustomerLabel";
            this.lblCustomerLabel.Size = new System.Drawing.Size(100, 22);
            this.lblCustomerLabel.TabIndex = 0;
            this.lblCustomerLabel.Text = "Customer:";
            this.lblCustomerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerLabel.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblCustomerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblCust
            // 
            this.lblCust.AutoSize = false;
            this.lblCust.Location = new System.Drawing.Point(130, 20);
            this.lblCust.Name = "lblCust";
            this.lblCust.Size = new System.Drawing.Size(200, 22);
            this.lblCust.TabIndex = 1;
            this.lblCust.Text = "";
            this.lblCust.ForeColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.lblCust.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCust.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblBalanceLabel
            // 
            this.lblBalanceLabel.AutoSize = false;
            this.lblBalanceLabel.Location = new System.Drawing.Point(20, 50);
            this.lblBalanceLabel.Name = "lblBalanceLabel";
            this.lblBalanceLabel.Size = new System.Drawing.Size(130, 22);
            this.lblBalanceLabel.TabIndex = 2;
            this.lblBalanceLabel.Text = "Current Balance:";
            this.lblBalanceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceLabel.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblBalanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblBal
            // 
            this.lblBal.AutoSize = false;
            this.lblBal.Location = new System.Drawing.Point(160, 50);
            this.lblBal.Name = "lblBal";
            this.lblBal.Size = new System.Drawing.Size(200, 22);
            this.lblBal.TabIndex = 3;
            this.lblBal.Text = "";
            this.lblBal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // grpDirection
            // 
            this.grpDirection.Controls.Add(this.rbDebit);
            this.grpDirection.Controls.Add(this.rbCredit);
            this.grpDirection.Location = new System.Drawing.Point(20, 86);
            this.grpDirection.Name = "grpDirection";
            this.grpDirection.Size = new System.Drawing.Size(380, 50);
            this.grpDirection.TabIndex = 4;
            this.grpDirection.TabStop = false;
            this.grpDirection.Text = "Direction";
            this.grpDirection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // 
            // rbDebit
            // 
            this.rbDebit.Location = new System.Drawing.Point(10, 22);
            this.rbDebit.Name = "rbDebit";
            this.rbDebit.Size = new System.Drawing.Size(170, 22);
            this.rbDebit.TabIndex = 0;
            this.rbDebit.Text = "Debit (↑ customer owes more)";
            this.rbDebit.Checked = true;
            this.rbDebit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // 
            // rbCredit
            // 
            this.rbCredit.Location = new System.Drawing.Point(200, 22);
            this.rbCredit.Name = "rbCredit";
            this.rbCredit.Size = new System.Drawing.Size(170, 22);
            this.rbCredit.TabIndex = 1;
            this.rbCredit.Text = "Credit (↓ reduce balance)";
            this.rbCredit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // 
            // lblAmountLabel
            // 
            this.lblAmountLabel.AutoSize = false;
            this.lblAmountLabel.Location = new System.Drawing.Point(20, 146);
            this.lblAmountLabel.Name = "lblAmountLabel";
            this.lblAmountLabel.Size = new System.Drawing.Size(120, 26);
            this.lblAmountLabel.TabIndex = 5;
            this.lblAmountLabel.Text = "Amount (Rs):";
            this.lblAmountLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountLabel.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(150, 144);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(160, 26);
            this.txtAmount.TabIndex = 5;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // 
            // lblNotesLabel
            // 
            this.lblNotesLabel.AutoSize = false;
            this.lblNotesLabel.Location = new System.Drawing.Point(20, 182);
            this.lblNotesLabel.Name = "lblNotesLabel";
            this.lblNotesLabel.Size = new System.Drawing.Size(120, 60);
            this.lblNotesLabel.TabIndex = 6;
            this.lblNotesLabel.Text = "Notes:";
            this.lblNotesLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotesLabel.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblNotesLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(150, 180);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(250, 60);
            this.txtNotes.TabIndex = 6;
            this.txtNotes.Multiline = true;
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(200, 256);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 36);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "✔  Post Adjustment";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(330, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 36);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // 
            // AdjustmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(440, 360);
            this.Controls.Add(this.lblNotesLabel);
            this.Controls.Add(this.lblAmountLabel);
            this.Controls.Add(this.lblBalanceLabel);
            this.Controls.Add(this.lblCustomerLabel);
            this.Controls.Add(this.grpDirection);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblBal);
            this.Controls.Add(this.lblCust);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdjustmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Post Manual Adjustment";
            
            this.grpDirection.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Control declarations
        private System.Windows.Forms.Label lblCust;
        private System.Windows.Forms.Label lblBal;
        private System.Windows.Forms.RadioButton rbDebit;
        private System.Windows.Forms.RadioButton rbCredit;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpDirection;
        
        // Label control declarations
        private System.Windows.Forms.Label lblCustomerLabel;
        private System.Windows.Forms.Label lblBalanceLabel;
        private System.Windows.Forms.Label lblAmountLabel;
        private System.Windows.Forms.Label lblNotesLabel;
    }
}