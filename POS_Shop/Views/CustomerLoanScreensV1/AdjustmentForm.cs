//using POS_Shop.Models;
//using POS_Shop.Models.LoanModelsV1;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace POS_Shop.Views.CustomerLoanScreensV1
//{
//    /// <summary>
//    /// Manual ledger adjustment form.
//    /// Used by admin to correct errors, write-offs, discounts, or migrate opening balances.
//    /// </summary>
//    public partial class AdjustmentForm : Form
//    {
//        private readonly int _customerId;
//        private readonly string _customerName;
//        private readonly decimal _currentBalance;
//        private bool _isSaving;

//        public bool AdjustmentPosted { get; private set; }

//        public AdjustmentForm(int customerId, string customerName, decimal currentBalance)
//        {
//            InitializeComponent();
//            _customerId = customerId;
//            _customerName = customerName;
//            _currentBalance = currentBalance;
//        }

//        private void AdjustmentForm_Load(object sender, EventArgs e)
//        {
//            lblCustomerName.Text = _customerName;
//            lblCurrentBalance.Text = FormatBalance(_currentBalance);
//            lblCurrentBalance.ForeColor = _currentBalance > 0
//                ? Color.FromArgb(192, 0, 0)
//                : _currentBalance < 0
//                    ? Color.FromArgb(0, 102, 204)
//                    : Color.Gray;

//            SetupAdjustmentTypeDropdown();
//            SetupValidation();
//            txtAmount.Focus();
//        }

//        private void SetupAdjustmentTypeDropdown()
//        {
//            cmbAdjType.Items.Clear();
//            cmbAdjType.Items.Add("➕ Increase Loan (Customer owes more)");
//            cmbAdjType.Items.Add("➖ Decrease Loan (Write-off / Discount)");
//            cmbAdjType.Items.Add("🔵 Add Advance Credit");
//            cmbAdjType.Items.Add("📋 Set Opening Balance");
//            cmbAdjType.SelectedIndex = 0;
//            cmbAdjType.SelectedIndexChanged += (s, e) => UpdatePreview();
//        }

//        private void SetupValidation()
//        {
//            txtAmount.KeyPress += (s, e) =>
//            {
//                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
//                    e.Handled = true;
//                if (e.KeyChar == '.' && txtAmount.Text.Contains('.'))
//                    e.Handled = true;
//            };
//            txtAmount.TextChanged += (s, e) => UpdatePreview();
//        }

//        private void UpdatePreview()
//        {
//            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
//            {
//                lblPreview.Text = "-";
//                lblPreview.ForeColor = Color.Gray;
//                return;
//            }

//            decimal adjustment = GetSignedAdjustment(amount);
//            decimal newBalance = _currentBalance + adjustment;

//            lblPreview.Text = $"New Balance: {FormatBalance(newBalance)}";
//            lblPreview.ForeColor = newBalance > 0
//                ? Color.FromArgb(192, 0, 0)
//                : newBalance < 0
//                    ? Color.FromArgb(0, 102, 204)
//                    : Color.FromArgb(39, 174, 96);
//        }

//        private decimal GetSignedAdjustment(decimal amount)
//        {
//            switch (cmbAdjType.SelectedIndex)
//            {
//                case 0:   // Increase loan
//                    return +amount;
//                case 1:   // Decrease loan / discount
//                    return -amount;
//                case 2:   // Add advance (makes balance more negative)
//                    return -amount;
//                case 3:   // Opening balance — handled separately
//                    return 0;
//                default:
//                    return 0;
//            }
//        }

//        private async void SaveBtn_Click(object sender, EventArgs e)
//        {
//            if (_isSaving) return;
//            if (!ValidateInput()) return;

//            _isSaving = true;
//            SaveBtn.Enabled = false;
//            SaveBtn.Text = "Saving...";

//            try
//            {
//                decimal amount = decimal.Parse(txtAmount.Text);
//                string reason = txtReason.Text.Trim();
//                string createdBy = "Admin"; // Replace with session user

//                using (var context = new POSDbContext())
//                using (var tx = context.Database.BeginTransaction())
//                {
//                    try
//                    {
//                        var repo = new CustomerLedgerRepository(context);

//                        if (cmbAdjType.SelectedIndex == 3) // Opening Balance
//                        {
//                            await repo.SetOpeningBalanceAsync(_customerId, amount, reason, createdBy);
//                        }
//                        else
//                        {
//                            decimal signedAmt = GetSignedAdjustment(amount);
//                            await repo.PostAdjustmentAsync(_customerId, signedAmt, reason, createdBy);
//                        }

//                        tx.Commit();
//                    }
//                    catch
//                    {
//                        tx.Rollback();
//                        throw;
//                    }
//                }

//                AdjustmentPosted = true;
//                MessageBox.Show("Adjustment saved successfully.", "Success",
//                    MessageBoxButtons.OK, MessageBoxIcon.Information);

//                this.DialogResult = DialogResult.OK;
//                this.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error saving adjustment:\n{ex.Message}", "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {
//                _isSaving = false;
//                SaveBtn.Enabled = true;
//                SaveBtn.Text = "💾  Save Adjustment";
//            }
//        }

//        private bool ValidateInput()
//        {
//            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
//            {
//                MessageBox.Show("Please enter a valid amount.", "Validation",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                txtAmount.Focus();
//                return false;
//            }

//            if (string.IsNullOrWhiteSpace(txtReason.Text))
//            {
//                MessageBox.Show("Please enter a reason for this adjustment.", "Validation",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                txtReason.Focus();
//                return false;
//            }

//            var confirm = MessageBox.Show(
//                $"Are you sure you want to post this adjustment?\n\n" +
//                $"Type: {cmbAdjType.SelectedItem}\n" +
//                $"Amount: PKR {decimal.Parse(txtAmount.Text):N2}\n" +
//                $"Reason: {txtReason.Text}",
//                "Confirm Adjustment",
//                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//            return confirm == DialogResult.Yes;
//        }

//        private string FormatBalance(decimal balance)
//        {
//            if (balance > 0) return $"PKR {balance:N2} Loan";
//            if (balance < 0) return $"PKR {Math.Abs(balance):N2} Advance";
//            return "Clear (PKR 0.00)";
//        }

//        private void CancelBtn_Click(object sender, EventArgs e)
//        {
//            this.DialogResult = DialogResult.Cancel;
//            this.Close();
//        }
//    }
//}



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
    /// Manual ledger adjustment form.
    /// Used by admin to correct errors, write-offs, discounts, or migrate opening balances.
    /// </summary>
    public partial class AdjustmentForm : Form
    {
        private readonly int _customerId;
        private readonly string _customerName;
        private readonly decimal _currentBalance;
        private bool _isSaving;

        public bool AdjustmentPosted { get; private set; }

        public AdjustmentForm(int customerId, string customerName, decimal currentBalance)
        {
            InitializeComponent();
            _customerId = customerId;
            _customerName = customerName;
            _currentBalance = currentBalance;
        }

        private void AdjustmentForm_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = _customerName;
            lblCurrentBalance.Text = FormatBalance(_currentBalance);
            lblCurrentBalance.ForeColor = _currentBalance > 0
                ? Color.FromArgb(192, 0, 0)
                : _currentBalance < 0
                    ? Color.FromArgb(0, 102, 204)
                    : Color.Gray;

            SetupAdjustmentTypeDropdown();
            SetupValidation();
            txtAmount.Focus();
        }

        private void SetupAdjustmentTypeDropdown()
        {
            cmbAdjType.Items.Clear();
            cmbAdjType.Items.Add("➕ Increase Loan (Customer owes more)");
            cmbAdjType.Items.Add("➖ Decrease Loan (Write-off / Discount)");
            cmbAdjType.Items.Add("🔵 Add Advance Credit");
            cmbAdjType.Items.Add("📋 Set Opening Balance");
            cmbAdjType.SelectedIndex = 0;
            cmbAdjType.SelectedIndexChanged += (s, e) => UpdatePreview();
        }

        private void SetupValidation()
        {
            txtAmount.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
                    e.Handled = true;
                if (e.KeyChar == '.' && txtAmount.Text.Contains('.'))
                    e.Handled = true;
            };
            txtAmount.TextChanged += (s, e) => UpdatePreview();
        }

        private void UpdatePreview()
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                lblPreview.Text = "-";
                lblPreview.ForeColor = Color.Gray;
                return;
            }

            decimal adjustment = GetSignedAdjustment(amount);
            decimal newBalance = _currentBalance + adjustment;

            lblPreview.Text = $"New Balance: {FormatBalance(newBalance)}";
            lblPreview.ForeColor = newBalance > 0
                ? Color.FromArgb(192, 0, 0)
                : newBalance < 0
                    ? Color.FromArgb(0, 102, 204)
                    : Color.FromArgb(39, 174, 96);
        }

        private decimal GetSignedAdjustment(decimal amount)
        {
            switch (cmbAdjType.SelectedIndex)
            {
                case 0:   // Increase loan
                    return +amount;
                case 1:   // Decrease loan / discount
                    return -amount;
                case 2:   // Add advance (makes balance more negative)
                    return -amount;
                case 3:   // Opening balance — handled separately
                    return 0;
                default:
                    return 0;
            }
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (_isSaving) return;
            if (!ValidateInput()) return;

            _isSaving = true;
            SaveBtn.Enabled = false;
            SaveBtn.Text = "Saving...";

            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                string reason = txtReason.Text.Trim();
                string createdBy = "Admin"; // Replace with session user

                using (var context = new POSDbContext())
                using (var tx = context.Database.BeginTransaction())
                {
                    try
                    {
                        var repo = new CustomerLedgerRepository(context);

                        if (cmbAdjType.SelectedIndex == 3) // Opening Balance
                        {
                            await repo.SetOpeningBalanceAsync(_customerId, amount, reason, createdBy);
                        }
                        else
                        {
                            decimal signedAmt = GetSignedAdjustment(amount);
                            await repo.PostAdjustmentAsync(_customerId, signedAmt, reason, createdBy);
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }

                AdjustmentPosted = true;
                MessageBox.Show("Adjustment saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving adjustment:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isSaving = false;
                SaveBtn.Enabled = true;
                SaveBtn.Text = "💾  Save Adjustment";
            }
        }

        private bool ValidateInput()
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please enter a reason for this adjustment.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReason.Focus();
                return false;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to post this adjustment?\n\n" +
                $"Type: {cmbAdjType.SelectedItem}\n" +
                $"Amount: PKR {decimal.Parse(txtAmount.Text):N2}\n" +
                $"Reason: {txtReason.Text}",
                "Confirm Adjustment",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return confirm == DialogResult.Yes;
        }

        private string FormatBalance(decimal balance)
        {
            if (balance > 0) return $"PKR {balance:N2} Loan";
            if (balance < 0) return $"PKR {Math.Abs(balance):N2} Advance";
            return "Clear (PKR 0.00)";
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S: SaveBtn.PerformClick(); return true;
                case Keys.Control | Keys.N: CancelBtn.PerformClick(); return true;
                case Keys.Alt | Keys.F4: this.Close(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

