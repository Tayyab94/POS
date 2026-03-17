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
    /// Full ledger statement for a single customer.
    /// Shows all debits, credits, running balance, KPI summary.
    /// Allows receiving payments, adding advances, and adjustments.
    /// </summary>
    public partial class Customerledgerform : Form
    {
        // ─── Fields ──────────────────────────────────────────────────────────
        private readonly int _customerId;
        private readonly string _customerName;
        private List<CustomerLedgerRow> _allRows = new List<CustomerLedgerRow>();
        private decimal _currentBalance;

        // ─── Constructor ─────────────────────────────────────────────────────
        public Customerledgerform(int customerId, string customerName)
        {
            InitializeComponent();
            _customerId = customerId;
            _customerName = customerName;
        }

        // ─── Load ─────────────────────────────────────────────────────────────
        private async void Customerledgerform_Load(object sender, EventArgs e)
        {
            this.Text = $"📒 Ledger — {_customerName}";
            lblCustomerName.Text = _customerName;
            SetupGrid();
            SetupDefaultDates();
            await RefreshAsync();
        }

        private void SetupDefaultDates()
        {
            dtpFrom.Value = DateTime.Today.AddMonths(-2);
            dtpTo.Value = DateTime.Today;
        }

        private void SetupGrid()
        {
            LedgerGrid.AutoGenerateColumns = false;
            LedgerGrid.AllowUserToAddRows = false;
            LedgerGrid.AllowUserToDeleteRows = false;
            LedgerGrid.ReadOnly = true;
            LedgerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            LedgerGrid.RowHeadersVisible = false;
            LedgerGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            LedgerGrid.BackgroundColor = System.Drawing.Color.White;
            LedgerGrid.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);
            LedgerGrid.Font = new System.Drawing.Font("Segoe UI", 9);
            LedgerGrid.RowTemplate.Height = 32;
            LedgerGrid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            LedgerGrid.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LedgerGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            LedgerGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            LedgerGrid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            LedgerGrid.ColumnHeadersHeight = 36;
            LedgerGrid.EnableHeadersVisualStyles = false;
            LedgerGrid.CellFormatting += LedgerGrid_CellFormatting;

            LedgerGrid.Columns.Clear();

            AddCol("EntryDate", "Date", 100, "dd-MMM-yyyy");
            AddCol("EntryTypeDisplay", "Type", 170);
            AddCol("DebitDisplay", "Debit (PKR)", 120, null, DataGridViewContentAlignment.MiddleRight);
            AddCol("CreditDisplay", "Credit (PKR)", 120, null, DataGridViewContentAlignment.MiddleRight);
            AddCol("BalanceDisplay", "Balance (PKR)", 130, null, DataGridViewContentAlignment.MiddleRight);
            AddCol("BalanceTypeDisplay", "Status", 80, null, DataGridViewContentAlignment.MiddleCenter);
            AddCol("Note", "Note", 200);
        }

        private void AddCol(string prop, string header, int width,
            string format = null, DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleLeft)
        {
            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = prop,
                HeaderText = header,
                Width = width,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = align,
                    Format = format ?? ""
                }
            };
            LedgerGrid.Columns.Add(col);
        }

        // ─── Grid Formatting ─────────────────────────────────────────────────
        private void LedgerGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _allRows.Count) return;
            var row = _allRows[e.RowIndex];

            // Color debit cells red
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "DebitDisplay" && row.Debit > 0)
                e.CellStyle.ForeColor = Color.FromArgb(192, 0, 0);

            // Color credit cells green
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "CreditDisplay" && row.Credit > 0)
                e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);

            // Color balance cells
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "BalanceDisplay")
            {
                if (row.Balance > 0) e.CellStyle.ForeColor = Color.FromArgb(192, 0, 0);
                else if (row.Balance < 0) e.CellStyle.ForeColor = Color.FromArgb(0, 102, 204);
                else e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
            }

            // Color status badge
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "BalanceTypeDisplay")
            {
                switch (row.BalanceTypeDisplay)
                {
                    case "Loan":
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.BackColor = Color.FromArgb(192, 0, 0);
                        break;
                    case "Advance":
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.BackColor = Color.FromArgb(0, 102, 204);
                        break;
                    case "Clear":
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.BackColor = Color.FromArgb(39, 174, 96);
                        break;
                }
            }
        }

        // ─── Data Load ────────────────────────────────────────────────────────
        private async Task RefreshAsync()
        {
            SetLoading(true);
            try
            {
                using (var context = new POSDbContext())
                {
                    var repo = new CustomerLedgerRepository(context);
                    _allRows = await repo.GetLedgerAsync(_customerId, dtpFrom.Value, dtpTo.Value);
                    var summary = await repo.GetLedgerSummaryAsync(_customerId, dtpFrom.Value, dtpTo.Value);
                    _currentBalance = summary.CurrentBalance;

                    BindGrid();
                    UpdateKPIs(summary);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ledger:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void BindGrid()
        {
            LedgerGrid.DataSource = null;
            LedgerGrid.DataSource = _allRows;

            lblRowCount.Text = $"{_allRows.Count} entries";
        }

        private void UpdateKPIs(LedgerSummary summary)
        {
            lblTotalDebit.Text = $"PKR {summary.TotalDebit:N2}";
            lblTotalCredit.Text = $"PKR {summary.TotalCredit:N2}";

            decimal balance = summary.CurrentBalance;
            lblCurrentBalance.Text = $"PKR {Math.Abs(balance):N2}";

            if (balance > 0)
            {
                lblCurrentBalance.ForeColor = Color.FromArgb(192, 0, 0);
                lblBalanceStatus.Text = "🔴 LOAN OUTSTANDING";
                lblBalanceStatus.ForeColor = Color.FromArgb(192, 0, 0);
                pnlBalanceKpi.BackColor = Color.FromArgb(255, 240, 240);
            }
            else if (balance < 0)
            {
                lblCurrentBalance.ForeColor = Color.FromArgb(0, 102, 204);
                lblBalanceStatus.Text = "🔵 ADVANCE CREDIT";
                lblBalanceStatus.ForeColor = Color.FromArgb(0, 102, 204);
                pnlBalanceKpi.BackColor = Color.FromArgb(235, 245, 255);
            }
            else
            {
                lblCurrentBalance.ForeColor = Color.FromArgb(39, 174, 96);
                lblBalanceStatus.Text = "✅ FULLY SETTLED";
                lblBalanceStatus.ForeColor = Color.FromArgb(39, 174, 96);
                pnlBalanceKpi.BackColor = Color.FromArgb(240, 255, 240);
            }

            // Show/hide action buttons based on state
            ReceivePaymentBtn.Enabled = balance > 0;
            ReceivePaymentBtn.BackColor = balance > 0
                ? Color.FromArgb(39, 174, 96)
                : Color.FromArgb(180, 180, 180);
        }

        private void SetLoading(bool loading)
        {
            LedgerGrid.Visible = !loading;
            lblLoading.Visible = loading;
            SearchBtn.Enabled = !loading;
        }

        // ─── Toolbar Actions ─────────────────────────────────────────────────
        private async void SearchBtn_Click(object sender, EventArgs e)
        {
            await RefreshAsync();
        }

        private async void ReceivePaymentBtn_Click(object sender, EventArgs e)
        {
            var frm = new Customerpaymentform(_customerId, _customerName, _currentBalance, false);
            if (frm.ShowDialog(this) == DialogResult.OK && frm.PaymentPosted)
                await RefreshAsync();
        }

        private async void AddAdvanceBtn_Click(object sender, EventArgs e)
        {
            var frm = new Customerpaymentform(_customerId, _customerName, _currentBalance, true);
            if (frm.ShowDialog(this) == DialogResult.OK && frm.PaymentPosted)
                await RefreshAsync();
        }

        private async void AdjustmentBtn_Click(object sender, EventArgs e)
        {
            var frm = new AdjustmentForm(_customerId, _customerName, _currentBalance);
            if (frm.ShowDialog(this) == DialogResult.OK && frm.AdjustmentPosted)
                await RefreshAsync();
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            // Open report form for print/export
            var report = new Customerledgerreportform(_customerId, _customerName,
                dtpFrom.Value, dtpTo.Value, _allRows);
            report.ShowDialog(this);
        }

        private async void ResetDatesBtn_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddMonths(-3);
            dtpTo.Value = DateTime.Today;
            await RefreshAsync();
        }
    }
}
