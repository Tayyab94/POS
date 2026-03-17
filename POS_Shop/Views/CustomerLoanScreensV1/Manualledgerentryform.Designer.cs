//namespace POS_Shop.Views.CustomerLoanScreensV1
//{
//    partial class ManualLedgerEntryForm
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null)) components.Dispose();
//            if (disposing && _searchDebounce != null) { _searchDebounce.Stop(); _searchDebounce.Dispose(); }
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            this.pnlHeader = new System.Windows.Forms.Panel();
//            this.lblTitle = new System.Windows.Forms.Label();
//            this.lblSubtitle = new System.Windows.Forms.Label();
//            this.pnlBody = new System.Windows.Forms.Panel();
//            this.pnlEntryType = new System.Windows.Forms.Panel();
//            this.pnlLoanIndicator = new System.Windows.Forms.Panel();
//            this.rbLoan = new System.Windows.Forms.RadioButton();
//            this.rbAdvance = new System.Windows.Forms.RadioButton();
//            this.lblEntryTypeDesc = new System.Windows.Forms.Label();
//            this.lblCustomerLabel = new System.Windows.Forms.Label();
//            this.txtCustomerSearch = new System.Windows.Forms.TextBox();
//            this.pnlCustomerSuggestions = new System.Windows.Forms.Panel();
//            this.lbSuggestions = new System.Windows.Forms.ListBox();
//            this.pnlSelectedCustomer = new System.Windows.Forms.Panel();
//            this.lblSelectedName = new System.Windows.Forms.Label();
//            this.lblSelectedBalance = new System.Windows.Forms.Label();
//            this.lblAmountLabel = new System.Windows.Forms.Label();
//            this.txtAmount = new System.Windows.Forms.TextBox();
//            this.pnlPaymentMethod = new System.Windows.Forms.Panel();
//            this.lblPayMethod = new System.Windows.Forms.Label();
//            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
//            this.lblReferenceNo = new System.Windows.Forms.Label();
//            this.txtReferenceNo = new System.Windows.Forms.TextBox();
//            this.lblNoteLabel = new System.Windows.Forms.Label();
//            this.txtNote = new System.Windows.Forms.TextBox();
//            this.pnlPreviewStrip = new System.Windows.Forms.Panel();
//            this.lblPreviewTitle = new System.Windows.Forms.Label();
//            this.lblPreview = new System.Windows.Forms.Label();
//            this.pnlFooter = new System.Windows.Forms.Panel();
//            this.CancelBtn = new System.Windows.Forms.Button();
//            this.SaveBtn = new System.Windows.Forms.Button();
//            this.pnlHeader.SuspendLayout();
//            this.pnlBody.SuspendLayout();
//            this.pnlEntryType.SuspendLayout();
//            this.pnlCustomerSuggestions.SuspendLayout();
//            this.pnlSelectedCustomer.SuspendLayout();
//            this.pnlPaymentMethod.SuspendLayout();
//            this.pnlPreviewStrip.SuspendLayout();
//            this.pnlFooter.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // pnlHeader
//            // 
//            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
//            this.pnlHeader.Controls.Add(this.lblTitle);
//            this.pnlHeader.Controls.Add(this.lblSubtitle);
//            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
//            this.pnlHeader.Name = "pnlHeader";
//            this.pnlHeader.Padding = new System.Windows.Forms.Padding(22, 10, 22, 10);
//            this.pnlHeader.Size = new System.Drawing.Size(873, 75);
//            this.pnlHeader.TabIndex = 0;
//            // 
//            // lblTitle
//            // 
//            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
//            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
//            this.lblTitle.ForeColor = System.Drawing.Color.White;
//            this.lblTitle.Location = new System.Drawing.Point(22, 10);
//            this.lblTitle.Name = "lblTitle";
//            this.lblTitle.Size = new System.Drawing.Size(829, 36);
//            this.lblTitle.TabIndex = 0;
//            this.lblTitle.Text = "➕ Manual Loan / Advance Entry";
//            // 
//            // lblSubtitle
//            // 
//            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
//            this.lblSubtitle.Location = new System.Drawing.Point(22, 10);
//            this.lblSubtitle.Name = "lblSubtitle";
//            this.lblSubtitle.Size = new System.Drawing.Size(829, 55);
//            this.lblSubtitle.TabIndex = 1;
//            this.lblSubtitle.Text = "Record a loan or advance for any customer — without an invoice";
//            // 
//            // pnlBody
//            // 
//            this.pnlBody.BackColor = System.Drawing.Color.White;
//            this.pnlBody.Controls.Add(this.pnlEntryType);
//            this.pnlBody.Controls.Add(this.lblCustomerLabel);
//            this.pnlBody.Controls.Add(this.txtCustomerSearch);
//            this.pnlBody.Controls.Add(this.pnlCustomerSuggestions);
//            this.pnlBody.Controls.Add(this.pnlSelectedCustomer);
//            this.pnlBody.Controls.Add(this.lblAmountLabel);
//            this.pnlBody.Controls.Add(this.txtAmount);
//            this.pnlBody.Controls.Add(this.pnlPaymentMethod);
//            this.pnlBody.Controls.Add(this.lblNoteLabel);
//            this.pnlBody.Controls.Add(this.txtNote);
//            this.pnlBody.Controls.Add(this.pnlPreviewStrip);
//            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlBody.Location = new System.Drawing.Point(0, 0);
//            this.pnlBody.Name = "pnlBody";
//            this.pnlBody.Size = new System.Drawing.Size(873, 582);
//            this.pnlBody.TabIndex = 1;
//            // 
//            // pnlEntryType
//            // 
//            this.pnlEntryType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
//            this.pnlEntryType.Controls.Add(this.pnlLoanIndicator);
//            this.pnlEntryType.Controls.Add(this.rbLoan);
//            this.pnlEntryType.Controls.Add(this.rbAdvance);
//            this.pnlEntryType.Controls.Add(this.lblEntryTypeDesc);
//            this.pnlEntryType.Location = new System.Drawing.Point(22, 77);
//            this.pnlEntryType.Name = "pnlEntryType";
//            this.pnlEntryType.Size = new System.Drawing.Size(851, 70);
//            this.pnlEntryType.TabIndex = 0;
//            // 
//            // pnlLoanIndicator
//            // 
//            this.pnlLoanIndicator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
//            this.pnlLoanIndicator.Location = new System.Drawing.Point(0, 0);
//            this.pnlLoanIndicator.Name = "pnlLoanIndicator";
//            this.pnlLoanIndicator.Size = new System.Drawing.Size(6, 70);
//            this.pnlLoanIndicator.TabIndex = 0;
//            // 
//            // rbLoan
//            // 
//            this.rbLoan.AutoSize = true;
//            this.rbLoan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.rbLoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
//            this.rbLoan.Location = new System.Drawing.Point(18, 12);
//            this.rbLoan.Name = "rbLoan";
//            this.rbLoan.Size = new System.Drawing.Size(281, 27);
//            this.rbLoan.TabIndex = 1;
//            this.rbLoan.Text = "🔴  Loan  (Customer owes you)";
//            // 
//            // rbAdvance
//            // 
//            this.rbAdvance.AutoSize = true;
//            this.rbAdvance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.rbAdvance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
//            this.rbAdvance.Location = new System.Drawing.Point(335, 12);
//            this.rbAdvance.Name = "rbAdvance";
//            this.rbAdvance.Size = new System.Drawing.Size(340, 27);
//            this.rbAdvance.TabIndex = 2;
//            this.rbAdvance.Text = "🔵  Advance  (Customer pays upfront)";
//            // 
//            // lblEntryTypeDesc
//            // 
//            this.lblEntryTypeDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
//            this.lblEntryTypeDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
//            this.lblEntryTypeDesc.Location = new System.Drawing.Point(18, 38);
//            this.lblEntryTypeDesc.Name = "lblEntryTypeDesc";
//            this.lblEntryTypeDesc.Size = new System.Drawing.Size(475, 22);
//            this.lblEntryTypeDesc.TabIndex = 3;
//            this.lblEntryTypeDesc.Text = "Customer owes you money — adds to their outstanding loan balance.";
//            // 
//            // lblCustomerLabel
//            // 
//            this.lblCustomerLabel.AutoSize = true;
//            this.lblCustomerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
//            this.lblCustomerLabel.Location = new System.Drawing.Point(22, 196);
//            this.lblCustomerLabel.Name = "lblCustomerLabel";
//            this.lblCustomerLabel.Size = new System.Drawing.Size(82, 20);
//            this.lblCustomerLabel.TabIndex = 1;
//            this.lblCustomerLabel.Text = "Customer:";
//            // 
//            // txtCustomerSearch
//            // 
//            this.txtCustomerSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.txtCustomerSearch.Location = new System.Drawing.Point(22, 215);
//            this.txtCustomerSearch.Name = "txtCustomerSearch";
//            this.txtCustomerSearch.Size = new System.Drawing.Size(445, 30);
//            this.txtCustomerSearch.TabIndex = 2;
//            this.txtCustomerSearch.TextChanged += new System.EventHandler(this.txtCustomerSearch_TextChanged);
//            this.txtCustomerSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerSearch_KeyDown);
//            // 
//            // pnlCustomerSuggestions
//            // 
//            this.pnlCustomerSuggestions.BackColor = System.Drawing.Color.White;
//            this.pnlCustomerSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.pnlCustomerSuggestions.Controls.Add(this.lbSuggestions);
//            this.pnlCustomerSuggestions.Location = new System.Drawing.Point(22, 167);
//            this.pnlCustomerSuggestions.Name = "pnlCustomerSuggestions";
//            this.pnlCustomerSuggestions.Size = new System.Drawing.Size(445, 160);
//            this.pnlCustomerSuggestions.TabIndex = 3;
//            this.pnlCustomerSuggestions.Visible = false;
//            // 
//            // lbSuggestions
//            // 
//            this.lbSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.lbSuggestions.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.lbSuggestions.ItemHeight = 20;
//            this.lbSuggestions.Location = new System.Drawing.Point(0, 80);
//            this.lbSuggestions.Name = "lbSuggestions";
//            this.lbSuggestions.Size = new System.Drawing.Size(623, 80);
//            this.lbSuggestions.TabIndex = 0;
//            this.lbSuggestions.Click += new System.EventHandler(this.lbSuggestions_Click);
//            this.lbSuggestions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbSuggestions_KeyDown);
//            // 
//            // pnlSelectedCustomer
//            // 
//            this.pnlSelectedCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(240)))));
//            this.pnlSelectedCustomer.Controls.Add(this.lblSelectedName);
//            this.pnlSelectedCustomer.Controls.Add(this.lblSelectedBalance);
//            this.pnlSelectedCustomer.Location = new System.Drawing.Point(488, 167);
//            this.pnlSelectedCustomer.Name = "pnlSelectedCustomer";
//            this.pnlSelectedCustomer.Size = new System.Drawing.Size(361, 52);
//            this.pnlSelectedCustomer.TabIndex = 4;
//            this.pnlSelectedCustomer.Visible = false;
//            // 
//            // lblSelectedName
//            // 
//            this.lblSelectedName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.lblSelectedName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
//            this.lblSelectedName.Location = new System.Drawing.Point(5, 8);
//            this.lblSelectedName.Name = "lblSelectedName";
//            this.lblSelectedName.Size = new System.Drawing.Size(350, 20);
//            this.lblSelectedName.TabIndex = 0;
//            // 
//            // lblSelectedBalance
//            // 
//            this.lblSelectedBalance.Font = new System.Drawing.Font("Segoe UI", 8F);
//            this.lblSelectedBalance.ForeColor = System.Drawing.Color.Gray;
//            this.lblSelectedBalance.Location = new System.Drawing.Point(5, 28);
//            this.lblSelectedBalance.Name = "lblSelectedBalance";
//            this.lblSelectedBalance.Size = new System.Drawing.Size(400, 18);
//            this.lblSelectedBalance.TabIndex = 1;
//            // 
//            // lblAmountLabel
//            // 
//            this.lblAmountLabel.AutoSize = true;
//            this.lblAmountLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
//            this.lblAmountLabel.Location = new System.Drawing.Point(22, 337);
//            this.lblAmountLabel.Name = "lblAmountLabel";
//            this.lblAmountLabel.Size = new System.Drawing.Size(154, 20);
//            this.lblAmountLabel.TabIndex = 5;
//            this.lblAmountLabel.Text = "Loan Amount (PKR):";
//            // 
//            // txtAmount
//            // 
//            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
//            this.txtAmount.Location = new System.Drawing.Point(22, 357);
//            this.txtAmount.Name = "txtAmount";
//            this.txtAmount.Size = new System.Drawing.Size(445, 52);
//            this.txtAmount.TabIndex = 6;
//            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
//            // 
//            // pnlPaymentMethod
//            // 
//            this.pnlPaymentMethod.BackColor = System.Drawing.Color.White;
//            this.pnlPaymentMethod.Controls.Add(this.lblPayMethod);
//            this.pnlPaymentMethod.Controls.Add(this.cmbPaymentMethod);
//            this.pnlPaymentMethod.Controls.Add(this.lblReferenceNo);
//            this.pnlPaymentMethod.Controls.Add(this.txtReferenceNo);
//            this.pnlPaymentMethod.Location = new System.Drawing.Point(488, 248);
//            this.pnlPaymentMethod.Name = "pnlPaymentMethod";
//            this.pnlPaymentMethod.Size = new System.Drawing.Size(366, 60);
//            this.pnlPaymentMethod.TabIndex = 7;
//            this.pnlPaymentMethod.Visible = false;
//            // 
//            // lblPayMethod
//            // 
//            this.lblPayMethod.AutoSize = true;
//            this.lblPayMethod.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
//            this.lblPayMethod.Location = new System.Drawing.Point(0, 2);
//            this.lblPayMethod.Name = "lblPayMethod";
//            this.lblPayMethod.Size = new System.Drawing.Size(134, 20);
//            this.lblPayMethod.TabIndex = 0;
//            this.lblPayMethod.Text = "Payment Method:";
//            // 
//            // cmbPaymentMethod
//            // 
//            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cmbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.cmbPaymentMethod.Location = new System.Drawing.Point(11, 27);
//            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
//            this.cmbPaymentMethod.Size = new System.Drawing.Size(180, 28);
//            this.cmbPaymentMethod.TabIndex = 1;
//            // 
//            // lblReferenceNo
//            // 
//            this.lblReferenceNo.AutoSize = true;
//            this.lblReferenceNo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
//            this.lblReferenceNo.Location = new System.Drawing.Point(200, 0);
//            this.lblReferenceNo.Name = "lblReferenceNo";
//            this.lblReferenceNo.Size = new System.Drawing.Size(129, 20);
//            this.lblReferenceNo.TabIndex = 2;
//            this.lblReferenceNo.Text = "Cheque / Ref No:";
//            this.lblReferenceNo.Visible = false;
//            // 
//            // txtReferenceNo
//            // 
//            this.txtReferenceNo.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.txtReferenceNo.Location = new System.Drawing.Point(204, 27);
//            this.txtReferenceNo.Name = "txtReferenceNo";
//            this.txtReferenceNo.Size = new System.Drawing.Size(157, 27);
//            this.txtReferenceNo.TabIndex = 3;
//            this.txtReferenceNo.Visible = false;
//            // 
//            // lblNoteLabel
//            // 
//            this.lblNoteLabel.AutoSize = true;
//            this.lblNoteLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
//            this.lblNoteLabel.Location = new System.Drawing.Point(498, 337);
//            this.lblNoteLabel.Name = "lblNoteLabel";
//            this.lblNoteLabel.Size = new System.Drawing.Size(122, 20);
//            this.lblNoteLabel.TabIndex = 8;
//            this.lblNoteLabel.Text = "Note (optional):";
//            // 
//            // txtNote
//            // 
//            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.txtNote.Location = new System.Drawing.Point(498, 373);
//            this.txtNote.Multiline = true;
//            this.txtNote.Name = "txtNote";
//            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
//            this.txtNote.Size = new System.Drawing.Size(356, 70);
//            this.txtNote.TabIndex = 9;
//            // 
//            // pnlPreviewStrip
//            // 
//            this.pnlPreviewStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
//            this.pnlPreviewStrip.Controls.Add(this.lblPreviewTitle);
//            this.pnlPreviewStrip.Controls.Add(this.lblPreview);
//            this.pnlPreviewStrip.Location = new System.Drawing.Point(28, 461);
//            this.pnlPreviewStrip.Name = "pnlPreviewStrip";
//            this.pnlPreviewStrip.Size = new System.Drawing.Size(439, 52);
//            this.pnlPreviewStrip.TabIndex = 10;
//            // 
//            // lblPreviewTitle
//            // 
//            this.lblPreviewTitle.AutoSize = true;
//            this.lblPreviewTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
//            this.lblPreviewTitle.ForeColor = System.Drawing.Color.Gray;
//            this.lblPreviewTitle.Location = new System.Drawing.Point(14, 8);
//            this.lblPreviewTitle.Name = "lblPreviewTitle";
//            this.lblPreviewTitle.Size = new System.Drawing.Size(152, 19);
//            this.lblPreviewTitle.TabIndex = 0;
//            this.lblPreviewTitle.Text = "Balance after this entry:";
//            // 
//            // lblPreview
//            // 
//            this.lblPreview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.lblPreview.ForeColor = System.Drawing.Color.Gray;
//            this.lblPreview.Location = new System.Drawing.Point(14, 26);
//            this.lblPreview.Name = "lblPreview";
//            this.lblPreview.Size = new System.Drawing.Size(470, 20);
//            this.lblPreview.TabIndex = 1;
//            this.lblPreview.Text = "—";
//            // 
//            // pnlFooter
//            // 
//            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
//            this.pnlFooter.Controls.Add(this.CancelBtn);
//            this.pnlFooter.Controls.Add(this.SaveBtn);
//            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.pnlFooter.Location = new System.Drawing.Point(0, 582);
//            this.pnlFooter.Name = "pnlFooter";
//            this.pnlFooter.Size = new System.Drawing.Size(873, 64);
//            this.pnlFooter.TabIndex = 2;
//            // 
//            // CancelBtn
//            // 
//            this.CancelBtn.BackColor = System.Drawing.Color.White;
//            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.CancelBtn.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.CancelBtn.Location = new System.Drawing.Point(340, 14);
//            this.CancelBtn.Name = "CancelBtn";
//            this.CancelBtn.Size = new System.Drawing.Size(90, 36);
//            this.CancelBtn.TabIndex = 0;
//            this.CancelBtn.Text = "Cancel";
//            this.CancelBtn.UseVisualStyleBackColor = false;
//            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
//            // 
//            // SaveBtn
//            // 
//            this.SaveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
//            this.SaveBtn.Enabled = false;
//            this.SaveBtn.FlatAppearance.BorderSize = 0;
//            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.SaveBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.SaveBtn.ForeColor = System.Drawing.Color.White;
//            this.SaveBtn.Location = new System.Drawing.Point(438, 14);
//            this.SaveBtn.Name = "SaveBtn";
//            this.SaveBtn.Size = new System.Drawing.Size(100, 36);
//            this.SaveBtn.TabIndex = 1;
//            this.SaveBtn.Text = "💾  Save Entry";
//            this.SaveBtn.UseVisualStyleBackColor = false;
//            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
//            // 
//            // ManualLedgerEntryForm
//            // 
//            this.BackColor = System.Drawing.Color.White;
//            this.ClientSize = new System.Drawing.Size(873, 646);
//            this.Controls.Add(this.pnlHeader);
//            this.Controls.Add(this.pnlBody);
//            this.Controls.Add(this.pnlFooter);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.Name = "ManualLedgerEntryForm";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.Text = "➕ Manual Loan / Advance Entry";
//            this.Load += new System.EventHandler(this.ManualLedgerEntryForm_Load);
//            this.pnlHeader.ResumeLayout(false);
//            this.pnlBody.ResumeLayout(false);
//            this.pnlBody.PerformLayout();
//            this.pnlEntryType.ResumeLayout(false);
//            this.pnlEntryType.PerformLayout();
//            this.pnlCustomerSuggestions.ResumeLayout(false);
//            this.pnlSelectedCustomer.ResumeLayout(false);
//            this.pnlPaymentMethod.ResumeLayout(false);
//            this.pnlPaymentMethod.PerformLayout();
//            this.pnlPreviewStrip.ResumeLayout(false);
//            this.pnlPreviewStrip.PerformLayout();
//            this.pnlFooter.ResumeLayout(false);
//            this.ResumeLayout(false);

//        }

//        // ── Control declarations ──────────────────────────────────────────────
//        private System.Windows.Forms.Panel pnlHeader;
//        private System.Windows.Forms.Label lblTitle;
//        private System.Windows.Forms.Label lblSubtitle;
//        private System.Windows.Forms.Panel pnlBody;
//        private System.Windows.Forms.Panel pnlEntryType;
//        private System.Windows.Forms.Panel pnlLoanIndicator;
//        private System.Windows.Forms.RadioButton rbLoan;
//        private System.Windows.Forms.RadioButton rbAdvance;
//        private System.Windows.Forms.Label lblEntryTypeDesc;
//        private System.Windows.Forms.Label lblCustomerLabel;
//        private System.Windows.Forms.TextBox txtCustomerSearch;
//        private System.Windows.Forms.Panel pnlCustomerSuggestions;
//        private System.Windows.Forms.ListBox lbSuggestions;
//        private System.Windows.Forms.Panel pnlSelectedCustomer;
//        private System.Windows.Forms.Label lblSelectedName;
//        private System.Windows.Forms.Label lblSelectedBalance;
//        private System.Windows.Forms.Label lblAmountLabel;
//        private System.Windows.Forms.TextBox txtAmount;
//        private System.Windows.Forms.Panel pnlPaymentMethod;
//        private System.Windows.Forms.Label lblPayMethod;
//        private System.Windows.Forms.ComboBox cmbPaymentMethod;
//        private System.Windows.Forms.Label lblReferenceNo;
//        private System.Windows.Forms.TextBox txtReferenceNo;
//        private System.Windows.Forms.Label lblNoteLabel;
//        private System.Windows.Forms.TextBox txtNote;
//        private System.Windows.Forms.Panel pnlPreviewStrip;
//        private System.Windows.Forms.Label lblPreviewTitle;
//        private System.Windows.Forms.Label lblPreview;
//        private System.Windows.Forms.Panel pnlFooter;
//        private System.Windows.Forms.Button CancelBtn;
//        private System.Windows.Forms.Button SaveBtn;
//    }
//}


namespace POS_Shop.Views.CustomerLoanScreensV1
{
    partial class ManualLedgerEntryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_searchDebounce != null)
                {
                    _searchDebounce.Stop();
                    _searchDebounce.Tick -= SearchDebounce_Tick;
                    _searchDebounce.Dispose();
                    _searchDebounce = null;
                }
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlEntryType = new System.Windows.Forms.Panel();
            this.pnlLoanIndicator = new System.Windows.Forms.Panel();
            this.rbLoan = new System.Windows.Forms.RadioButton();
            this.rbAdvance = new System.Windows.Forms.RadioButton();
            this.lblEntryTypeDesc = new System.Windows.Forms.Label();
            this.lblCustomerLabel = new System.Windows.Forms.Label();
            this.txtCustomerSearch = new System.Windows.Forms.TextBox();
            this.pnlCustomerSuggestions = new System.Windows.Forms.Panel();
            this.lbSuggestions = new System.Windows.Forms.ListBox();
            this.pnlSelectedCustomer = new System.Windows.Forms.Panel();
            this.lblSelectedName = new System.Windows.Forms.Label();
            this.lblSelectedBalance = new System.Windows.Forms.Label();
            this.lblAmountLabel = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.pnlPaymentMethod = new System.Windows.Forms.Panel();
            this.lblPayMethod = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblNoteLabel = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.pnlPreviewStrip = new System.Windows.Forms.Panel();
            this.lblPreviewTitle = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlEntryType.SuspendLayout();
            this.pnlCustomerSuggestions.SuspendLayout();
            this.pnlSelectedCustomer.SuspendLayout();
            this.pnlPaymentMethod.SuspendLayout();
            this.pnlPreviewStrip.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(22, 10, 22, 10);
            this.pnlHeader.Size = new System.Drawing.Size(873, 75);
            this.pnlHeader.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(22, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(829, 36);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "➕ Manual Loan / Advance Entry";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.lblSubtitle.Location = new System.Drawing.Point(22, 46);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(829, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Record a loan or advance for any customer — without an invoice";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.pnlEntryType);
            this.pnlBody.Controls.Add(this.lblCustomerLabel);
            this.pnlBody.Controls.Add(this.txtCustomerSearch);
            this.pnlBody.Controls.Add(this.pnlCustomerSuggestions);
            this.pnlBody.Controls.Add(this.pnlSelectedCustomer);
            this.pnlBody.Controls.Add(this.lblAmountLabel);
            this.pnlBody.Controls.Add(this.txtAmount);
            this.pnlBody.Controls.Add(this.pnlPaymentMethod);
            this.pnlBody.Controls.Add(this.lblNoteLabel);
            this.pnlBody.Controls.Add(this.txtNote);
            this.pnlBody.Controls.Add(this.pnlPreviewStrip);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 75);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(22, 15, 22, 15);
            this.pnlBody.Size = new System.Drawing.Size(873, 503);
            this.pnlBody.TabIndex = 1;

            // 
            // pnlEntryType
            // 
            this.pnlEntryType.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            this.pnlEntryType.Controls.Add(this.pnlLoanIndicator);
            this.pnlEntryType.Controls.Add(this.rbLoan);
            this.pnlEntryType.Controls.Add(this.rbAdvance);
            this.pnlEntryType.Controls.Add(this.lblEntryTypeDesc);
            this.pnlEntryType.Location = new System.Drawing.Point(22, 15);
            this.pnlEntryType.Name = "pnlEntryType";
            this.pnlEntryType.Size = new System.Drawing.Size(829, 70);
            this.pnlEntryType.TabIndex = 0;

            // 
            // pnlLoanIndicator
            // 
            this.pnlLoanIndicator.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.pnlLoanIndicator.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLoanIndicator.Location = new System.Drawing.Point(0, 0);
            this.pnlLoanIndicator.Name = "pnlLoanIndicator";
            this.pnlLoanIndicator.Size = new System.Drawing.Size(6, 70);
            this.pnlLoanIndicator.TabIndex = 0;

            // 
            // rbLoan
            // 
            this.rbLoan.AutoSize = true;
            this.rbLoan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.rbLoan.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.rbLoan.Location = new System.Drawing.Point(18, 12);
            this.rbLoan.Name = "rbLoan";
            this.rbLoan.Size = new System.Drawing.Size(281, 27);
            this.rbLoan.TabIndex = 1;
            this.rbLoan.TabStop = true;
            this.rbLoan.Text = "🔴  Loan  (Customer owes you)";
            this.rbLoan.UseVisualStyleBackColor = true;

            // 
            // rbAdvance
            // 
            this.rbAdvance.AutoSize = true;
            this.rbAdvance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.rbAdvance.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.rbAdvance.Location = new System.Drawing.Point(335, 12);
            this.rbAdvance.Name = "rbAdvance";
            this.rbAdvance.Size = new System.Drawing.Size(340, 27);
            this.rbAdvance.TabIndex = 2;
            this.rbAdvance.TabStop = true;
            this.rbAdvance.Text = "🔵  Advance  (Customer pays upfront)";
            this.rbAdvance.UseVisualStyleBackColor = true;

            // 
            // lblEntryTypeDesc
            // 
            this.lblEntryTypeDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblEntryTypeDesc.ForeColor = System.Drawing.Color.FromArgb(160, 0, 0);
            this.lblEntryTypeDesc.Location = new System.Drawing.Point(18, 42);
            this.lblEntryTypeDesc.Name = "lblEntryTypeDesc";
            this.lblEntryTypeDesc.Size = new System.Drawing.Size(475, 22);
            this.lblEntryTypeDesc.TabIndex = 3;
            this.lblEntryTypeDesc.Text = "Customer owes you money — adds to their outstanding loan balance.";

            // 
            // lblCustomerLabel
            // 
            this.lblCustomerLabel.AutoSize = true;
            this.lblCustomerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomerLabel.Location = new System.Drawing.Point(22, 100);
            this.lblCustomerLabel.Name = "lblCustomerLabel";
            this.lblCustomerLabel.Size = new System.Drawing.Size(82, 20);
            this.lblCustomerLabel.TabIndex = 1;
            this.lblCustomerLabel.Text = "Customer:";

            // 
            // txtCustomerSearch
            // 
            this.txtCustomerSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCustomerSearch.Location = new System.Drawing.Point(22, 123);
            this.txtCustomerSearch.Name = "txtCustomerSearch";
            this.txtCustomerSearch.Size = new System.Drawing.Size(445, 30);
            this.txtCustomerSearch.TabIndex = 2;
            this.txtCustomerSearch.TextChanged += new System.EventHandler(this.txtCustomerSearch_TextChanged);
            this.txtCustomerSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerSearch_KeyDown);
            this.txtCustomerSearch.Leave += new System.EventHandler(this.txtCustomerSearch_Leave);

            // 
            // pnlCustomerSuggestions
            // 
            this.pnlCustomerSuggestions.BackColor = System.Drawing.Color.White;
            this.pnlCustomerSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomerSuggestions.Controls.Add(this.lbSuggestions);
            this.pnlCustomerSuggestions.Location = new System.Drawing.Point(22, 153);
            this.pnlCustomerSuggestions.Name = "pnlCustomerSuggestions";
            this.pnlCustomerSuggestions.Size = new System.Drawing.Size(445, 150);
            this.pnlCustomerSuggestions.TabIndex = 3;
            this.pnlCustomerSuggestions.Visible = false;

            // 
            // lbSuggestions
            // 
            this.lbSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbSuggestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSuggestions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbSuggestions.FormattingEnabled = true;
            this.lbSuggestions.ItemHeight = 20;
            this.lbSuggestions.Location = new System.Drawing.Point(0, 0);
            this.lbSuggestions.Name = "lbSuggestions";
            this.lbSuggestions.Size = new System.Drawing.Size(443, 148);
            this.lbSuggestions.TabIndex = 0;
            this.lbSuggestions.Click += new System.EventHandler(this.lbSuggestions_Click);
            this.lbSuggestions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSuggestions_MouseDoubleClick);
            this.lbSuggestions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbSuggestions_KeyDown);

            // 
            // pnlSelectedCustomer
            // 
            this.pnlSelectedCustomer.BackColor = System.Drawing.Color.FromArgb(240, 255, 240);
            this.pnlSelectedCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelectedCustomer.Controls.Add(this.lblSelectedName);
            this.pnlSelectedCustomer.Controls.Add(this.lblSelectedBalance);
            this.pnlSelectedCustomer.Location = new System.Drawing.Point(488, 113);
            this.pnlSelectedCustomer.Name = "pnlSelectedCustomer";
            this.pnlSelectedCustomer.Size = new System.Drawing.Size(361, 60);
            this.pnlSelectedCustomer.TabIndex = 4;
            this.pnlSelectedCustomer.Visible = false;

            // 
            // lblSelectedName
            // 
            this.lblSelectedName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSelectedName.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblSelectedName.Location = new System.Drawing.Point(8, 8);
            this.lblSelectedName.Name = "lblSelectedName";
            this.lblSelectedName.Size = new System.Drawing.Size(350, 23);
            this.lblSelectedName.TabIndex = 0;
            this.lblSelectedName.Text = "Customer Name";

            // 
            // lblSelectedBalance
            // 
            this.lblSelectedBalance.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSelectedBalance.ForeColor = System.Drawing.Color.Gray;
            this.lblSelectedBalance.Location = new System.Drawing.Point(8, 31);
            this.lblSelectedBalance.Name = "lblSelectedBalance";
            this.lblSelectedBalance.Size = new System.Drawing.Size(350, 20);
            this.lblSelectedBalance.TabIndex = 1;
            this.lblSelectedBalance.Text = "Balance information";

            // 
            // lblAmountLabel
            // 
            this.lblAmountLabel.AutoSize = true;
            this.lblAmountLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAmountLabel.Location = new System.Drawing.Point(22, 200);
            this.lblAmountLabel.Name = "lblAmountLabel";
            this.lblAmountLabel.Size = new System.Drawing.Size(154, 20);
            this.lblAmountLabel.TabIndex = 5;
            this.lblAmountLabel.Text = "Loan Amount (PKR):";

            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.txtAmount.Location = new System.Drawing.Point(22, 223);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(445, 52);
            this.txtAmount.TabIndex = 6;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);

            // 
            // pnlPaymentMethod
            // 
            this.pnlPaymentMethod.BackColor = System.Drawing.Color.White;
            this.pnlPaymentMethod.Controls.Add(this.lblPayMethod);
            this.pnlPaymentMethod.Controls.Add(this.cmbPaymentMethod);
            this.pnlPaymentMethod.Controls.Add(this.lblReferenceNo);
            this.pnlPaymentMethod.Controls.Add(this.txtReferenceNo);
            this.pnlPaymentMethod.Location = new System.Drawing.Point(488, 200);
            this.pnlPaymentMethod.Name = "pnlPaymentMethod";
            this.pnlPaymentMethod.Size = new System.Drawing.Size(366, 75);
            this.pnlPaymentMethod.TabIndex = 7;
            this.pnlPaymentMethod.Visible = false;

            // 
            // lblPayMethod
            // 
            this.lblPayMethod.AutoSize = true;
            this.lblPayMethod.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPayMethod.Location = new System.Drawing.Point(5, 5);
            this.lblPayMethod.Name = "lblPayMethod";
            this.lblPayMethod.Size = new System.Drawing.Size(134, 20);
            this.lblPayMethod.TabIndex = 0;
            this.lblPayMethod.Text = "Payment Method:";

            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPaymentMethod.Location = new System.Drawing.Point(9, 28);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(180, 28);
            this.cmbPaymentMethod.TabIndex = 1;

            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReferenceNo.Location = new System.Drawing.Point(200, 5);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(129, 20);
            this.lblReferenceNo.TabIndex = 2;
            this.lblReferenceNo.Text = "Cheque / Ref No:";
            this.lblReferenceNo.Visible = false;

            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReferenceNo.Location = new System.Drawing.Point(204, 28);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(157, 27);
            this.txtReferenceNo.TabIndex = 3;
            this.txtReferenceNo.Visible = false;

            // 
            // lblNoteLabel
            // 
            this.lblNoteLabel.AutoSize = true;
            this.lblNoteLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNoteLabel.Location = new System.Drawing.Point(488, 290);
            this.lblNoteLabel.Name = "lblNoteLabel";
            this.lblNoteLabel.Size = new System.Drawing.Size(122, 20);
            this.lblNoteLabel.TabIndex = 8;
            this.lblNoteLabel.Text = "Note (optional):";

            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNote.Location = new System.Drawing.Point(488, 313);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNote.Size = new System.Drawing.Size(356, 70);
            this.txtNote.TabIndex = 9;

            // 
            // pnlPreviewStrip
            // 
            this.pnlPreviewStrip.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            this.pnlPreviewStrip.Controls.Add(this.lblPreviewTitle);
            this.pnlPreviewStrip.Controls.Add(this.lblPreview);
            this.pnlPreviewStrip.Location = new System.Drawing.Point(22, 300);
            this.pnlPreviewStrip.Name = "pnlPreviewStrip";
            this.pnlPreviewStrip.Size = new System.Drawing.Size(439, 60);
            this.pnlPreviewStrip.TabIndex = 10;

            // 
            // lblPreviewTitle
            // 
            this.lblPreviewTitle.AutoSize = true;
            this.lblPreviewTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPreviewTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblPreviewTitle.Location = new System.Drawing.Point(14, 8);
            this.lblPreviewTitle.Name = "lblPreviewTitle";
            this.lblPreviewTitle.Size = new System.Drawing.Size(152, 19);
            this.lblPreviewTitle.TabIndex = 0;
            this.lblPreviewTitle.Text = "Balance after this entry:";

            // 
            // lblPreview
            // 
            this.lblPreview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPreview.ForeColor = System.Drawing.Color.Gray;
            this.lblPreview.Location = new System.Drawing.Point(14, 30);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(415, 23);
            this.lblPreview.TabIndex = 1;
            this.lblPreview.Text = "—";

            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            this.pnlFooter.Controls.Add(this.CancelBtn);
            this.pnlFooter.Controls.Add(this.SaveBtn);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 578);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(873, 64);
            this.pnlFooter.TabIndex = 2;

            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.White;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBtn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CancelBtn.Location = new System.Drawing.Point(340, 14);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(90, 36);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);

            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.SaveBtn.Enabled = false;
            this.SaveBtn.FlatAppearance.BorderSize = 0;
            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.SaveBtn.ForeColor = System.Drawing.Color.White;
            this.SaveBtn.Location = new System.Drawing.Point(438, 14);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(100, 36);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "💾  Save Entry";
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);

            // 
            // ManualLedgerEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(873, 642);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualLedgerEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "➕ Manual Loan / Advance Entry";
            this.Load += new System.EventHandler(this.ManualLedgerEntryForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlEntryType.ResumeLayout(false);
            this.pnlEntryType.PerformLayout();
            this.pnlCustomerSuggestions.ResumeLayout(false);
            this.pnlSelectedCustomer.ResumeLayout(false);
            this.pnlPaymentMethod.ResumeLayout(false);
            this.pnlPaymentMethod.PerformLayout();
            this.pnlPreviewStrip.ResumeLayout(false);
            this.pnlPreviewStrip.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlEntryType;
        private System.Windows.Forms.Panel pnlLoanIndicator;
        private System.Windows.Forms.RadioButton rbLoan;
        private System.Windows.Forms.RadioButton rbAdvance;
        private System.Windows.Forms.Label lblEntryTypeDesc;
        private System.Windows.Forms.Label lblCustomerLabel;
        private System.Windows.Forms.TextBox txtCustomerSearch;
        private System.Windows.Forms.Panel pnlCustomerSuggestions;
        private System.Windows.Forms.ListBox lbSuggestions;
        private System.Windows.Forms.Panel pnlSelectedCustomer;
        private System.Windows.Forms.Label lblSelectedName;
        private System.Windows.Forms.Label lblSelectedBalance;
        private System.Windows.Forms.Label lblAmountLabel;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Panel pnlPaymentMethod;
        private System.Windows.Forms.Label lblPayMethod;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblNoteLabel;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Panel pnlPreviewStrip;
        private System.Windows.Forms.Label lblPreviewTitle;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
    }
}