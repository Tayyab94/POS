using POS_Shop.Models;
using POS_Shop.Models.LoanModels;
using POS_Shop.Repositories.LoanRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.CustomerLoanScreens
{

    /// <summary>
    /// Standalone payment dialog — opened from BillPadForm or CustomerLedgerForm.
    /// Records a customer payment and updates the ledger automatically.
    /// </summary>
    public partial class CustomerPaymentForm : Form
    {
        // ── State ────────────────────────────────────────────────────────────
        private readonly int _customerId;
        private readonly string _customerName;
        private decimal _currentBalance;

        public CustomerPayment SavedPayment { get; private set; }

        // ── Constructor ──────────────────────────────────────────────────────
        public CustomerPaymentForm(int customerId, string customerName, decimal currentBalance)
        {
            _customerId = customerId;
            _customerName = customerName;
            _currentBalance = currentBalance;
            InitializeComponent();
        }

        // ── Load ─────────────────────────────────────────────────────────────
        private void CustomerPaymentForm_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = _customerName;
            RefreshBalanceDisplay();
            txtAmount.Focus();
            txtAmount.SelectAll();
        }

        private void RefreshBalanceDisplay()
        {
            if (_currentBalance > 0)
            {
                lblCurrentBalance.Text = $"Rs. {_currentBalance:N0}  (Loan — owes you)";
                lblCurrentBalance.ForeColor = Color.FromArgb(198, 40, 40);
            }
            else if (_currentBalance < 0)
            {
                lblCurrentBalance.Text = $"Rs. {Math.Abs(_currentBalance):N0}  (Advance — you owe)";
                lblCurrentBalance.ForeColor = Color.FromArgb(21, 101, 192);
            }
            else
            {
                lblCurrentBalance.Text = "Rs. 0  (Settled)";
                lblCurrentBalance.ForeColor = Color.FromArgb(46, 125, 50);
            }
        }

        // ── Amount change → show projected balance ────────────────────────
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amt) && amt > 0)
            {
                decimal projected = _currentBalance - amt;
                if (projected > 0)
                {
                    lblProjectedBalance.Text = $"After payment: Rs. {projected:N0} still owed";
                    lblProjectedBalance.ForeColor = Color.FromArgb(198, 40, 40);
                }
                else if (projected < 0)
                {
                    lblProjectedBalance.Text = $"After payment: Rs. {Math.Abs(projected):N0} advance stored";
                    lblProjectedBalance.ForeColor = Color.FromArgb(21, 101, 192);
                }
                else
                {
                    lblProjectedBalance.Text = "After payment: Account fully settled";
                    lblProjectedBalance.ForeColor = Color.FromArgb(46, 125, 50);
                }
            }
            else
            {
                lblProjectedBalance.Text = "";
            }
        }

        // ── Receive Full Loan shortcut ────────────────────────────────────
        private void btnReceiveFull_Click(object sender, EventArgs e)
        {
            if (_currentBalance > 0)
                txtAmount.Text = _currentBalance.ToString("F0");
        }

        // ── Save ─────────────────────────────────────────────────────────────
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;

            btnSave.Enabled = false;
            btnSave.Text = "Saving…";

            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                string method = GetSelectedPaymentMethod();
                string refNo = txtReferenceNo.Text.Trim();
                string transactionId = txtTransactionId.Text.Trim();
                string notes = txtNotes.Text.Trim();

                using (var db = new POSDbContext())
                using (var tx = db.Database.BeginTransaction())
                {
                    var repo = new CustomerLedgerRepository(db);
                    SavedPayment = await repo.RecordPaymentAsync(
                        _customerId, amount, method,
                        string.IsNullOrEmpty(refNo) ? null : refNo,
                        string.IsNullOrEmpty(transactionId) ? null : transactionId,
                        string.IsNullOrEmpty(notes) ? null : notes,
                        createdBy: Environment.UserName);
                    tx.Commit();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save payment:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
                btnSave.Text = "Save Payment";
            }
        }

        private new bool Validate()
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amt) || amt <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than zero.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }
            return true;
        }

        private string GetSelectedPaymentMethod()
        {
            if (rbBankTransfer.Checked) return PaymentMethods.BankTransfer;
            if (rbCheque.Checked) return PaymentMethods.Cheque;
            if (rbMobilePayment.Checked) return PaymentMethods.MobilePayment;
            return PaymentMethods.Cash;
        }

        // ── Payment method → show/hide reference field ────────────────────
        private void PaymentMethod_CheckedChanged(object sender, EventArgs e)
        {
            bool needRef = rbBankTransfer.Checked || rbCheque.Checked || rbMobilePayment.Checked;
            lblTransactionId.Visible = needRef;
            txtTransactionId.Visible = needRef;

            if (rbCheque.Checked)
                lblTransactionId.Text = "Cheque No:";
            else if (rbBankTransfer.Checked)
                lblTransactionId.Text = "Bank TID:";
            else if (rbMobilePayment.Checked)
                lblTransactionId.Text = "Transaction ID:";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ── Enter on amount → move to notes ──────────────────────────────
        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNotes.Focus();
            }
        }
    }
}
