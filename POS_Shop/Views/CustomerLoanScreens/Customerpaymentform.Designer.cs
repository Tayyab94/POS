namespace POS_Shop.Views.CustomerLoanScreens
{
    partial class CustomerPaymentForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Controls ─────────────────────────────────────────────────────
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();

            this.pnlBody = new System.Windows.Forms.Panel();

            this.lblCustomerLbl = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCurrentBalLbl = new System.Windows.Forms.Label();
            this.lblCurrentBalance = new System.Windows.Forms.Label();

            this.lblAmountLbl = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnReceiveFull = new System.Windows.Forms.Button();
            this.lblProjectedBalance = new System.Windows.Forms.Label();

            this.grpPaymentMethod = new System.Windows.Forms.GroupBox();
            this.rbCash = new System.Windows.Forms.RadioButton();
            this.rbBankTransfer = new System.Windows.Forms.RadioButton();
            this.rbCheque = new System.Windows.Forms.RadioButton();
            this.rbMobilePayment = new System.Windows.Forms.RadioButton();

            this.lblTransactionId = new System.Windows.Forms.Label();
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.lblRefNoLbl = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblNotesLbl = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            // ── Layout suspend ────────────────────────────────────────────────
            this.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.grpPaymentMethod.SuspendLayout();
            this.pnlFooter.SuspendLayout();

            // ════════════════════════════════════════════════════════════════
            //  FORM
            // ════════════════════════════════════════════════════════════════
            this.Text = "Receive Customer Payment";
            this.Size = new System.Drawing.Size(520, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Load += new System.EventHandler(this.CustomerPaymentForm_Load);

            // ════════════════════════════════════════════════════════════════
            //  HEADER
            // ════════════════════════════════════════════════════════════════
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 70;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Height = 32;
            this.lblTitle.Text = "💳  Receive Customer Payment";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubtitle.Height = 22;
            this.lblSubtitle.Text = "Record standalone payment — updates ledger automatically";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 210, 255);

            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);

            // ════════════════════════════════════════════════════════════════
            //  BODY
            // ════════════════════════════════════════════════════════════════
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlBody.Padding = new System.Windows.Forms.Padding(20, 14, 20, 0);

            int y = 14;

            // ── Customer info card ────────────────────────────────────────
            var pnlCard = new System.Windows.Forms.Panel();
            pnlCard.Location = new System.Drawing.Point(20, y);
            pnlCard.Size = new System.Drawing.Size(460, 68);
            pnlCard.BackColor = System.Drawing.Color.White;
            pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblCustomerLbl.AutoSize = false;
            this.lblCustomerLbl.Location = new System.Drawing.Point(10, 8);
            this.lblCustomerLbl.Size = new System.Drawing.Size(90, 18);
            this.lblCustomerLbl.Text = "Customer:";
            this.lblCustomerLbl.ForeColor = System.Drawing.Color.Gray;
            this.lblCustomerLbl.Font = new System.Drawing.Font("Segoe UI", 8F);

            this.lblCustomerName.AutoSize = false;
            this.lblCustomerName.Location = new System.Drawing.Point(100, 6);
            this.lblCustomerName.Size = new System.Drawing.Size(350, 22);
            this.lblCustomerName.Text = "";
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(30, 80, 162);

            this.lblCurrentBalLbl.AutoSize = false;
            this.lblCurrentBalLbl.Location = new System.Drawing.Point(10, 36);
            this.lblCurrentBalLbl.Size = new System.Drawing.Size(90, 18);
            this.lblCurrentBalLbl.Text = "Balance:";
            this.lblCurrentBalLbl.ForeColor = System.Drawing.Color.Gray;
            this.lblCurrentBalLbl.Font = new System.Drawing.Font("Segoe UI", 8F);

            this.lblCurrentBalance.AutoSize = false;
            this.lblCurrentBalance.Location = new System.Drawing.Point(100, 34);
            this.lblCurrentBalance.Size = new System.Drawing.Size(350, 22);
            this.lblCurrentBalance.Text = "";
            this.lblCurrentBalance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            pnlCard.Controls.Add(this.lblCustomerLbl);
            pnlCard.Controls.Add(this.lblCustomerName);
            pnlCard.Controls.Add(this.lblCurrentBalLbl);
            pnlCard.Controls.Add(this.lblCurrentBalance);
            this.pnlBody.Controls.Add(pnlCard);

            y += 82;

            // ── Amount ────────────────────────────────────────────────────
            this.lblAmountLbl.AutoSize = false;
            this.lblAmountLbl.Location = new System.Drawing.Point(20, y);
            this.lblAmountLbl.Size = new System.Drawing.Size(120, 22);
            this.lblAmountLbl.Text = "Amount Received:";
            this.lblAmountLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pnlBody.Controls.Add(this.lblAmountLbl);
            y += 24;

            this.txtAmount.Location = new System.Drawing.Point(20, y);
            this.txtAmount.Size = new System.Drawing.Size(200, 30);
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            this.pnlBody.Controls.Add(this.txtAmount);

            this.btnReceiveFull.Location = new System.Drawing.Point(228, y);
            this.btnReceiveFull.Size = new System.Drawing.Size(120, 30);
            this.btnReceiveFull.Text = "Full Amount";
            this.btnReceiveFull.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnReceiveFull.ForeColor = System.Drawing.Color.White;
            this.btnReceiveFull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceiveFull.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReceiveFull.Click += new System.EventHandler(this.btnReceiveFull_Click);
            this.btnReceiveFull.FlatAppearance.BorderSize = 0;
            this.pnlBody.Controls.Add(this.btnReceiveFull);

            y += 36;

            this.lblProjectedBalance.AutoSize = false;
            this.lblProjectedBalance.Location = new System.Drawing.Point(20, y);
            this.lblProjectedBalance.Size = new System.Drawing.Size(440, 20);
            this.lblProjectedBalance.Text = "";
            this.lblProjectedBalance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.pnlBody.Controls.Add(this.lblProjectedBalance);
            y += 28;

            // ── Payment Method ─────────────────────────────────────────────
            this.grpPaymentMethod.Location = new System.Drawing.Point(20, y);
            this.grpPaymentMethod.Size = new System.Drawing.Size(460, 54);
            this.grpPaymentMethod.Text = "Payment Method";
            this.grpPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);

            this.rbCash.Location = new System.Drawing.Point(10, 22);
            this.rbCash.Size = new System.Drawing.Size(80, 22);
            this.rbCash.Text = "💵 Cash";
            this.rbCash.Checked = true;
            this.rbCash.CheckedChanged += new System.EventHandler(this.PaymentMethod_CheckedChanged);

            this.rbBankTransfer.Location = new System.Drawing.Point(100, 22);
            this.rbBankTransfer.Size = new System.Drawing.Size(120, 22);
            this.rbBankTransfer.Text = "🏦 Bank Transfer";
            this.rbBankTransfer.CheckedChanged += new System.EventHandler(this.PaymentMethod_CheckedChanged);

            this.rbCheque.Location = new System.Drawing.Point(230, 22);
            this.rbCheque.Size = new System.Drawing.Size(90, 22);
            this.rbCheque.Text = "📄 Cheque";
            this.rbCheque.CheckedChanged += new System.EventHandler(this.PaymentMethod_CheckedChanged);

            this.rbMobilePayment.Location = new System.Drawing.Point(325, 22);
            this.rbMobilePayment.Size = new System.Drawing.Size(130, 22);
            this.rbMobilePayment.Text = "📱 Mobile Pay";
            this.rbMobilePayment.CheckedChanged += new System.EventHandler(this.PaymentMethod_CheckedChanged);

            this.grpPaymentMethod.Controls.Add(this.rbCash);
            this.grpPaymentMethod.Controls.Add(this.rbBankTransfer);
            this.grpPaymentMethod.Controls.Add(this.rbCheque);
            this.grpPaymentMethod.Controls.Add(this.rbMobilePayment);
            this.pnlBody.Controls.Add(this.grpPaymentMethod);
            y += 64;

            // ── Transaction ID (conditional) ──────────────────────────────
            this.lblTransactionId.AutoSize = false;
            this.lblTransactionId.Location = new System.Drawing.Point(20, y);
            this.lblTransactionId.Size = new System.Drawing.Size(120, 22);
            this.lblTransactionId.Text = "Transaction ID:";
            this.lblTransactionId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTransactionId.Visible = false;
            this.pnlBody.Controls.Add(this.lblTransactionId);

            this.txtTransactionId.Location = new System.Drawing.Point(150, y);
            this.txtTransactionId.Size = new System.Drawing.Size(330, 26);
            this.txtTransactionId.Visible = false;
            this.pnlBody.Controls.Add(this.txtTransactionId);
            y += 34;

            // ── Reference No ──────────────────────────────────────────────
            this.lblRefNoLbl.AutoSize = false;
            this.lblRefNoLbl.Location = new System.Drawing.Point(20, y);
            this.lblRefNoLbl.Size = new System.Drawing.Size(120, 22);
            this.lblRefNoLbl.Text = "Reference No:";
            this.lblRefNoLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pnlBody.Controls.Add(this.lblRefNoLbl);

            this.txtReferenceNo.Location = new System.Drawing.Point(150, y);
            this.txtReferenceNo.Size = new System.Drawing.Size(330, 26);
            this.pnlBody.Controls.Add(this.txtReferenceNo);
            y += 34;

            // ── Notes ─────────────────────────────────────────────────────
            this.lblNotesLbl.AutoSize = false;
            this.lblNotesLbl.Location = new System.Drawing.Point(20, y);
            this.lblNotesLbl.Size = new System.Drawing.Size(120, 22);
            this.lblNotesLbl.Text = "Notes:";
            this.lblNotesLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pnlBody.Controls.Add(this.lblNotesLbl);

            this.txtNotes.Location = new System.Drawing.Point(150, y);
            this.txtNotes.Size = new System.Drawing.Size(330, 60);
            this.txtNotes.Multiline = true;
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.pnlBody.Controls.Add(this.txtNotes);

            // ════════════════════════════════════════════════════════════════
            //  FOOTER
            // ════════════════════════════════════════════════════════════════
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 60;
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);

            this.btnSave.Location = new System.Drawing.Point(270, 12);
            this.btnSave.Size = new System.Drawing.Size(110, 36);
            this.btnSave.Text = "✔  Save Payment";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(390, 12);
            this.btnCancel.Size = new System.Drawing.Size(88, 36);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Controls.Add(this.btnCancel);

            // ════════════════════════════════════════════════════════════════
            //  ASSEMBLE FORM
            // ════════════════════════════════════════════════════════════════
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.grpPaymentMethod.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Controls ─────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlFooter;

        private System.Windows.Forms.Label lblCustomerLbl;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCurrentBalLbl;
        private System.Windows.Forms.Label lblCurrentBalance;

        private System.Windows.Forms.Label lblAmountLbl;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnReceiveFull;
        private System.Windows.Forms.Label lblProjectedBalance;

        private System.Windows.Forms.GroupBox grpPaymentMethod;
        private System.Windows.Forms.RadioButton rbCash;
        private System.Windows.Forms.RadioButton rbBankTransfer;
        private System.Windows.Forms.RadioButton rbCheque;
        private System.Windows.Forms.RadioButton rbMobilePayment;

        private System.Windows.Forms.Label lblTransactionId;
        private System.Windows.Forms.TextBox txtTransactionId;
        private System.Windows.Forms.Label lblRefNoLbl;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblNotesLbl;
        private System.Windows.Forms.TextBox txtNotes;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}