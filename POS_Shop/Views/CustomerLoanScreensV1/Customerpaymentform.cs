using POS_Shop.Models;
using POS_Shop.Models.LoanModelsV1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.CustomerLoanScreensV1
{
    /// <summary>
    /// Receive a loan payment OR an advance deposit from a customer.
    /// Opened from CustomerLedgerForm or AllCustomerBalancesForm.
    /// </summary>
    public partial class Customerpaymentform : Form
    {
        // ─── Fields ──────────────────────────────────────────────────────────

        private readonly int _customerId;
        private readonly string _customerName;
        private readonly decimal _currentBalance;   // > 0 = owes us, < 0 = we owe them
        private readonly bool _isAdvanceMode;       // false = receive payment, true = take advance
        private bool _isSaving;

        // Set by caller after ShowDialog to get result
        public bool PaymentPosted { get; private set; }

        // ─── Constructor ─────────────────────────────────────────────────────

        public Customerpaymentform(
            int customerId,
            string customerName,
            decimal currentBalance,
            bool advanceMode = false)
        {
            InitializeComponent();
            _customerId = customerId;
            _customerName = customerName;
            _currentBalance = currentBalance;
            _isAdvanceMode = advanceMode;
        }

        // ─── Load ─────────────────────────────────────────────────────────────

        private void Customerpaymentform_Load(object sender, EventArgs e)
        {
            SetupFormMode();
            PopulateCustomerInfo();
            SetupPaymentMethodDropdown();
            SetupValidation();
            AmountTxt.Focus();
        }

        private void SetupFormMode()
        {
            if (_isAdvanceMode)
            {
                this.Text = "💵 Receive Advance Deposit";
                lblFormTitle.Text = "Advance Deposit";
                lblFormTitle.ForeColor = Color.FromArgb(0, 102, 204);
                SaveBtn.Text = "💾  Save Advance";
                SaveBtn.BackColor = Color.FromArgb(0, 102, 204);
                lblAmountHint.Text = "Advance amount customer is depositing:";
            }
            else
            {
                this.Text = "✅ Receive Payment";
                lblFormTitle.Text = "Receive Loan Payment";
                lblFormTitle.ForeColor = Color.FromArgb(39, 174, 96);
                SaveBtn.Text = "💾  Save Payment";
                SaveBtn.BackColor = Color.FromArgb(39, 174, 96);
                lblAmountHint.Text = "Amount customer is paying:";
            }
        }

        private void PopulateCustomerInfo()
        {
            lblCustomerName.Text = _customerName;

            if (_currentBalance > 0)
            {
                lblCurrentBalance.Text = $"PKR {_currentBalance:N2} Loan Outstanding";
                lblCurrentBalance.ForeColor = Color.FromArgb(192, 0, 0);
                if (!_isAdvanceMode)
                    AmountTxt.Text = _currentBalance.ToString("F2");
            }
            else if (_currentBalance < 0)
            {
                lblCurrentBalance.Text = $"PKR {Math.Abs(_currentBalance):N2} Advance Credit";
                lblCurrentBalance.ForeColor = Color.FromArgb(0, 102, 204);
            }
            else
            {
                lblCurrentBalance.Text = "No Outstanding Balance";
                lblCurrentBalance.ForeColor = Color.Gray;
            }
        }

        private void SetupPaymentMethodDropdown()
        {
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.Add("Cash");
            cmbPaymentMethod.Items.Add("Bank Transfer");
            cmbPaymentMethod.Items.Add("Cheque");
            cmbPaymentMethod.Items.Add("JazzCash");
            cmbPaymentMethod.Items.Add("EasyPaisa");
            cmbPaymentMethod.SelectedIndex = 0;
        }

        private void SetupValidation()
        {
            AmountTxt.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
                    e.Handled = true;
                if (e.KeyChar == '.' && AmountTxt.Text.Contains('.'))
                    e.Handled = true;
            };

            AmountTxt.TextChanged += (s, e) => UpdateAfterBalance();
            cmbPaymentMethod.SelectedIndexChanged += (s, e) => ToggleReferenceField();
        }

        // ─── UI Events ────────────────────────────────────────────────────────

        private void UpdateAfterBalance()
        {
            if (!decimal.TryParse(AmountTxt.Text, out decimal amount) || amount <= 0)
            {
                lblAfterBalance.Text = "-";
                lblAfterBalance.ForeColor = Color.Gray;
                return;
            }

            decimal afterBalance = _isAdvanceMode
                ? _currentBalance - amount   // Advance reduces balance further (more credit)
                : _currentBalance - amount;  // Payment reduces loan balance

            string label = afterBalance > 0
                ? $"PKR {afterBalance:N2} Loan Remaining"
                : afterBalance < 0
                    ? $"PKR {Math.Abs(afterBalance):N2} Advance Credit"
                    : "✅ Fully Settled";

            lblAfterBalance.Text = label;
            lblAfterBalance.ForeColor = afterBalance > 0
                ? Color.FromArgb(192, 0, 0)
                : afterBalance < 0
                    ? Color.FromArgb(0, 102, 204)
                    : Color.FromArgb(39, 174, 96);
        }

        private void ToggleReferenceField()
        {
            bool needsRef = cmbPaymentMethod.SelectedItem?.ToString() == "Cheque"
                         || cmbPaymentMethod.SelectedItem?.ToString() == "Bank Transfer";

            lblReferenceNo.Visible = needsRef;
            txtReferenceNo.Visible = needsRef;
            pnlReferenceNo.Visible = needsRef;
        }

        // ─── Save ─────────────────────────────────────────────────────────────

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (_isSaving) return;
            if (!ValidateInput()) return;

            _isSaving = true;
            SaveBtn.Enabled = false;
            SaveBtn.Text = "Saving...";

            try
            {
                decimal amount = decimal.Parse(AmountTxt.Text);
                string method = cmbPaymentMethod.SelectedItem.ToString();
                string refNo = txtReferenceNo.Text.Trim();
                string note = txtNote.Text.Trim();
                string createdBy = "User"; // Replace with your session user

                using (var context = new POSDbContext())
                using (var tx = context.Database.BeginTransaction())
                {
                    try
                    {
                        var repo = new CustomerLedgerRepository(context);

                        if (_isAdvanceMode)
                            await repo.PostAdvanceDepositAsync(_customerId, amount, method, refNo, note, createdBy);
                        else
                            await repo.PostPaymentAsync(_customerId, amount, method, refNo, note, createdBy);

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }

                PaymentPosted = true;

                string msg = _isAdvanceMode
                    ? $"Advance of PKR {amount:N2} recorded successfully."
                    : $"Payment of PKR {amount:N2} received and recorded.";

                MessageBox.Show(msg, "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving payment:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isSaving = false;
                SaveBtn.Enabled = true;
                SaveBtn.Text = _isAdvanceMode ? "💾  Save Advance" : "💾  Save Payment";
            }
        }

        private bool ValidateInput()
        {
            if (!decimal.TryParse(AmountTxt.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AmountTxt.Focus();
                return false;
            }

            if (!_isAdvanceMode && _currentBalance > 0 && amount > _currentBalance)
            {
                var r = MessageBox.Show(
                    $"Payment amount PKR {amount:N2} exceeds outstanding loan PKR {_currentBalance:N2}.\n" +
                    $"The difference PKR {amount - _currentBalance:N2} will become advance credit.\n\nContinue?",
                    "Overpayment Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.No) return false;
            }

            if (cmbPaymentMethod.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a payment method.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPaymentMethod.Focus();
                return false;
            }

            return true;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AmountTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SaveBtn.PerformClick();
        }
    }
}
