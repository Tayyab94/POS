using POS_Shop.Models;
using POS_Shop.Repositories.LoanRepositories;
using System;
using System.Windows.Forms;

namespace POS_Shop.Views.CustomerLoanScreens
{
    /// <summary>
    /// Simple admin dialog to post a manual debit or credit adjustment.
    /// Opened from CustomerLedgerForm → "Adjustment" button.
    /// </summary>
    public partial class AdjustmentForm : Form
    {
        private readonly int _customerId;
        private readonly string _customerName;
        private readonly decimal _currentBalance;

        public AdjustmentForm(int customerId, string customerName, decimal balance)
        {
            _customerId = customerId;
            _customerName = customerName;
            _currentBalance = balance;

            InitializeComponent();

            // Set dynamic data after initialization
            lblCust.Text = _customerName;

            // Set balance display with appropriate color
            if (_currentBalance > 0)
            {
                lblBal.Text = $"Dr Rs. {_currentBalance:N0}";
                lblBal.ForeColor = System.Drawing.Color.FromArgb(198, 40, 40);
            }
            else if (_currentBalance < 0)
            {
                lblBal.Text = $"Cr Rs. {Math.Abs(_currentBalance):N0}";
                lblBal.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);
            }
            else
            {
                lblBal.Text = "Nil";
                lblBal.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50);
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amt) || amt <= 0)
            {
                MessageBox.Show("Enter a valid amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNotes.Text))
            {
                MessageBox.Show("Notes are required for adjustments.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dc = rbDebit.Checked ? "D" : "C";

            try
            {
                using (var db = new POSDbContext())
                {
                    var repo = new CustomerLedgerRepository(db);
                    repo.PostAdjustmentAsync(
                        _customerId, amt, dc,
                        txtNotes.Text.Trim(),
                        Environment.UserName);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}