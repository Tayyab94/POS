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
    /// Per-customer ledger view.
    /// Shows running balance, full transaction history, allows payment entry.
    /// Can be opened standalone or from the CustomerLedgerReportForm.
    /// </summary>
    public partial class CustomerLedgerForm : Form
    {
        // ── State ────────────────────────────────────────────────────────────
        private readonly int _customerId;
        private readonly string _customerName;
        private decimal _currentBalance;
        private int _page = 1;
        private const int PAGE_SIZE = 30;
        private int _totalRows = 0;

        private DateTime _fromDate = DateTime.Today.AddMonths(-3);
        private DateTime _toDate = DateTime.Today;

        // ── Constructor ──────────────────────────────────────────────────────
        public CustomerLedgerForm(int customerId, string customerName)
        {
            _customerId = customerId;
            _customerName = customerName;
            InitializeComponent();
        }

        // ── Load ─────────────────────────────────────────────────────────────
        private async void CustomerLedgerForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Ledger — {_customerName}";
            lblCustomerName.Text = _customerName;
            dtpFrom.Value = _fromDate;
            dtpTo.Value = _toDate;
            await LoadDataAsync();
        }

        // ── Load data ─────────────────────────────────────────────────────────
        private async Task LoadDataAsync()
        {
            SetLoading(true);
            try
            {
                using (var db = new POSDbContext())
                {
                    var repo = new CustomerLedgerRepository(db);

                    _currentBalance = await repo.GetRunningBalanceAsync(_customerId);
                    RefreshBalanceBanner();

                    var (rows, total) = await repo.GetHistoryAsync(
                        _customerId, _page, PAGE_SIZE,
                        dtpFrom.Value, dtpTo.Value);

                    _totalRows = total;
                    BindGrid(rows);
                    UpdatePager();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load ledger:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void RefreshBalanceBanner()
        {
            if (_currentBalance > 0)
            {
                pnlBalance.BackColor = Color.FromArgb(255, 235, 238);
                lblBalanceVal.Text = $"Rs. {_currentBalance:N0}";
                lblBalanceLabel.Text = "LOAN  (customer owes you)";
                lblBalanceVal.ForeColor = Color.FromArgb(198, 40, 40);
            }
            else if (_currentBalance < 0)
            {
                pnlBalance.BackColor = Color.FromArgb(227, 242, 253);
                lblBalanceVal.Text = $"Rs. {Math.Abs(_currentBalance):N0}";
                lblBalanceLabel.Text = "ADVANCE  (you owe customer)";
                lblBalanceVal.ForeColor = Color.FromArgb(21, 101, 192);
            }
            else
            {
                pnlBalance.BackColor = Color.FromArgb(232, 245, 233);
                lblBalanceVal.Text = "Rs. 0";
                lblBalanceLabel.Text = "SETTLED";
                lblBalanceVal.ForeColor = Color.FromArgb(46, 125, 50);
            }
        }

        // ── Grid binding ──────────────────────────────────────────────────────
        private void BindGrid(List<CustomerTransaction> rows)
        {
            dgvTransactions.Rows.Clear();

            foreach (var tx in rows)
            {
                string debitAmt = tx.IsDebit ? $"Rs. {tx.Amount:N0}" : "";
                string creditAmt = tx.IsCredit ? $"Rs. {tx.Amount:N0}" : "";

                string balStr = tx.BalanceAfter == 0 ? "Nil"
                    : tx.BalanceAfter > 0
                        ? $"Dr {tx.BalanceAfter:N0}"
                        : $"Cr {Math.Abs(tx.BalanceAfter):N0}";

                int rowIdx = dgvTransactions.Rows.Add(
                    tx.TransactionDate.ToString("dd MMM yy"),
                    tx.TypeDisplay,
                    tx.OrderId.HasValue ? $"#{tx.OrderId}" : "",
                    debitAmt,
                    creditAmt,
                    balStr,
                    tx.Notes ?? ""
                );

                var row = dgvTransactions.Rows[rowIdx];

                // Color rows by type
                if (tx.IsDebit)
                {
                    row.Cells[3].Style.ForeColor = Color.FromArgb(198, 40, 40); // debit = red
                    row.Cells[3].Style.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }
                else
                {
                    row.Cells[4].Style.ForeColor = Color.FromArgb(46, 125, 50); // credit = green
                    row.Cells[4].Style.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }

                // Balance cell color
                if (tx.BalanceAfter > 0)
                    row.Cells[5].Style.ForeColor = Color.FromArgb(198, 40, 40);
                else if (tx.BalanceAfter < 0)
                    row.Cells[5].Style.ForeColor = Color.FromArgb(21, 101, 192);
                else
                    row.Cells[5].Style.ForeColor = Color.FromArgb(46, 125, 50);
            }
        }

        private void UpdatePager()
        {
            int pages = Math.Max(1, (int)Math.Ceiling(_totalRows / (double)PAGE_SIZE));
            lblPager.Text = $"Page {_page} of {pages}  ·  {_totalRows} records";
            btnPrev.Enabled = _page > 1;
            btnNext.Enabled = _page < pages;
        }

        // ── Buttons ───────────────────────────────────────────────────────────
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _page = 1;
            await LoadDataAsync();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_page > 1) { _page--; await LoadDataAsync(); }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            _page++;
            await LoadDataAsync();
        }

        private async void btnReceivePayment_Click(object sender, EventArgs e)
        {
            using (var dlg = new CustomerPaymentForm(_customerId, _customerName, _currentBalance))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _page = 1;
                    await LoadDataAsync();

                    // Confirm toast
                    MessageBox.Show(
                        $"Payment of Rs. {dlg.SavedPayment.AmountPaid:N0} recorded.\n" +
                        $"New balance: {GetBalanceText()}",
                        "Payment Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnPostAdjustment_Click(object sender, EventArgs e)
        {
            using (var dlg = new AdjustmentForm1(_customerId, _customerName, _currentBalance))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _page = 1;
                    await LoadDataAsync();
                }
            }
        }

        private string GetBalanceText()
        {
            if (_currentBalance == 0) return "Settled";
            return _currentBalance > 0
                ? $"Rs. {_currentBalance:N0} (Loan)"
                : $"Rs. {Math.Abs(_currentBalance):N0} (Advance)";
        }

        private void SetLoading(bool loading)
        {
            btnSearch.Enabled = !loading;
            btnReceivePayment.Enabled = !loading;
            dgvTransactions.Enabled = !loading;
            lblPager.Text = loading ? "Loading…" : lblPager.Text;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
