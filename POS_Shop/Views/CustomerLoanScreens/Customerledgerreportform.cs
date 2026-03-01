using POS_Shop.Models;
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
    /// Shows ALL customers with their running balances.
    /// Double-click any row → opens CustomerLedgerForm for that customer.
    /// </summary>
    public partial class CustomerLedgerReportForm : Form
    {
        private List<CustomerLedgerSummaryDto> _allRows;

        public CustomerLedgerReportForm() => InitializeComponent();

        private async void CustomerLedgerReportForm_Load(object sender, EventArgs e)
            => await LoadAsync();

        private async Task LoadAsync()
        {
            SetLoading(true);
            try
            {
                using (var db = new POSDbContext())
                {
                    var repo = new CustomerLedgerRepository(db);
                    _allRows = await repo.GetAllBalancesAsync(
                        txtSearch.Text.Trim(),
                        chkOnlyBalance.Checked);
                }
                BindGrid();
                RefreshSummaryStrip();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { SetLoading(false); }
        }

        private void BindGrid()
        {
            dgvCustomers.Rows.Clear();
            foreach (var r in _allRows)
            {
                int idx = dgvCustomers.Rows.Add(
                    r.CustomerName,
                    r.ContactNo,
                    r.City,
                    r.BalanceDisplay,
                    r.LastTransactionDate == default ? "-" : r.LastTransactionDate.ToString("dd MMM yyyy")
                );

                var row = dgvCustomers.Rows[idx];
                row.Tag = r; // store for double-click

                if (r.IsDebit)
                    row.Cells[3].Style.ForeColor = Color.FromArgb(198, 40, 40);
                else if (r.IsCredit)
                    row.Cells[3].Style.ForeColor = Color.FromArgb(21, 101, 192);
                else
                    row.Cells[3].Style.ForeColor = Color.FromArgb(46, 125, 50);

                row.Cells[3].Style.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            }
        }

        private void RefreshSummaryStrip()
        {
            decimal totalLoan = 0m, totalAdvance = 0m;
            int cntLoan = 0, cntAdv = 0;

            foreach (var r in _allRows)
            {
                if (r.IsDebit) { totalLoan += r.RunningBalance; cntLoan++; }
                if (r.IsCredit) { totalAdvance += Math.Abs(r.RunningBalance); cntAdv++; }
            }

            lblSummaryLoan.Text = $"Total Loans: Rs. {totalLoan:N0}  ({cntLoan} customers)";
            lblSummaryAdvance.Text = $"Total Advances: Rs. {totalAdvance:N0}  ({cntAdv} customers)";
        }

        // ── Grid double-click → open ledger ──────────────────────────────────
        private void dgvCustomers_CellDoubleClick(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var dto = dgvCustomers.Rows[e.RowIndex].Tag as CustomerLedgerSummaryDto;
            if (dto == null) return;

            using (var frm = new CustomerLedgerForm(dto.CustomerId, dto.CustomerName))
                frm.ShowDialog(this);

            // Refresh after returning (payment may have been recorded)
            _ = LoadAsync();
        }

        // ── Receive Payment directly from report ──────────────────────────────
        private async void btnReceivePayment_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) return;
            var dto = dgvCustomers.CurrentRow.Tag as CustomerLedgerSummaryDto;
            if (dto == null) return;

            using (var dlg = new CustomerPaymentForm(
                dto.CustomerId, dto.CustomerName, dto.RunningBalance))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    await LoadAsync();
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e) => await LoadAsync();

        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) await LoadAsync();
        }

        private async void chkOnlyBalance_CheckedChanged(object sender, EventArgs e)
            => await LoadAsync();

        private void SetLoading(bool on)
        {
            btnSearch.Enabled = !on;
            btnReceivePayment.Enabled = !on;
            dgvCustomers.Enabled = !on;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
