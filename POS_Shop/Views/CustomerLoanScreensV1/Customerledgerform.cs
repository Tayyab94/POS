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
//    /// Full ledger statement for a single customer.
//    /// Shows all debits, credits, running balance, KPI summary.
//    /// Allows receiving payments, adding advances, and adjustments.
//    /// </summary>
//    public partial class Customerledgerform : Form
//    {
//        // ─── Fields ──────────────────────────────────────────────────────────
//        private readonly int _customerId;
//        private readonly string _customerName;
//        private List<CustomerLedgerRow> _allRows = new List<CustomerLedgerRow>();
//        private decimal _currentBalance;

//        // ─── Constructor ─────────────────────────────────────────────────────
//        public Customerledgerform(int customerId, string customerName)
//        {
//            InitializeComponent();
//            _customerId = customerId;
//            _customerName = customerName;
//        }

//        // ─── Load ─────────────────────────────────────────────────────────────
//        private async void Customerledgerform_Load(object sender, EventArgs e)
//        {
//            this.Text = $"📒 Ledger — {_customerName}";
//            lblCustomerName.Text = _customerName;
//            SetupGrid();
//            SetupDefaultDates();
//            await RefreshAsync();
//        }

//        private void SetupDefaultDates()
//        {
//            dtpFrom.Value = DateTime.Today.AddMonths(-2);
//            dtpTo.Value = DateTime.Today;
//        }

//        private void SetupGrid()
//        {
//            LedgerGrid.AutoGenerateColumns = false;
//            LedgerGrid.AllowUserToAddRows = false;
//            LedgerGrid.AllowUserToDeleteRows = false;
//            LedgerGrid.ReadOnly = true;
//            LedgerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
//            LedgerGrid.RowHeadersVisible = false;
//            LedgerGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            LedgerGrid.BackgroundColor = System.Drawing.Color.White;
//            LedgerGrid.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);
//            LedgerGrid.Font = new System.Drawing.Font("Segoe UI", 9);
//            LedgerGrid.RowTemplate.Height = 32;
//            LedgerGrid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
//            LedgerGrid.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
//            LedgerGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
//            LedgerGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
//            LedgerGrid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
//            LedgerGrid.ColumnHeadersHeight = 36;
//            LedgerGrid.EnableHeadersVisualStyles = false;
//            LedgerGrid.CellFormatting += LedgerGrid_CellFormatting;

//            LedgerGrid.Columns.Clear();

//            AddCol("EntryDate", "Date", 100, "dd-MMM-yyyy");
//            AddCol("EntryTypeDisplay", "Type", 170);
//            AddCol("DebitDisplay", "Debit (PKR)", 120, null, DataGridViewContentAlignment.MiddleRight);
//            AddCol("CreditDisplay", "Credit (PKR)", 120, null, DataGridViewContentAlignment.MiddleRight);
//            AddCol("BalanceDisplay", "Balance (PKR)", 130, null, DataGridViewContentAlignment.MiddleRight);
//            AddCol("BalanceTypeDisplay", "Status", 80, null, DataGridViewContentAlignment.MiddleCenter);
//            AddCol("Note", "Note", 200);
//        }

//        private void AddCol(string prop, string header, int width,
//            string format = null, DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleLeft)
//        {
//            var col = new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = prop,
//                HeaderText = header,
//                Width = width,
//                DefaultCellStyle = new DataGridViewCellStyle
//                {
//                    Alignment = align,
//                    Format = format ?? ""
//                }
//            };
//            LedgerGrid.Columns.Add(col);
//        }

//        // ─── Grid Formatting ─────────────────────────────────────────────────
//        private void LedgerGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
//        {
//            if (e.RowIndex < 0 || e.RowIndex >= _allRows.Count) return;
//            var row = _allRows[e.RowIndex];

//            // Color debit cells red
//            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "DebitDisplay" && row.Debit > 0)
//                e.CellStyle.ForeColor = Color.FromArgb(192, 0, 0);

//            // Color credit cells green
//            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "CreditDisplay" && row.Credit > 0)
//                e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);

//            // Color balance cells
//            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "BalanceDisplay")
//            {
//                if (row.Balance > 0) e.CellStyle.ForeColor = Color.FromArgb(192, 0, 0);
//                else if (row.Balance < 0) e.CellStyle.ForeColor = Color.FromArgb(0, 102, 204);
//                else e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
//            }

//            // Color status badge
//            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "BalanceTypeDisplay")
//            {
//                switch (row.BalanceTypeDisplay)
//                {
//                    case "Loan":
//                        e.CellStyle.ForeColor = Color.White;
//                        e.CellStyle.BackColor = Color.FromArgb(192, 0, 0);
//                        break;
//                    case "Advance":
//                        e.CellStyle.ForeColor = Color.White;
//                        e.CellStyle.BackColor = Color.FromArgb(0, 102, 204);
//                        break;
//                    case "Clear":
//                        e.CellStyle.ForeColor = Color.White;
//                        e.CellStyle.BackColor = Color.FromArgb(39, 174, 96);
//                        break;
//                }
//            }
//        }

//        // ─── Data Load ────────────────────────────────────────────────────────
//        private async Task RefreshAsync()
//        {
//            SetLoading(true);
//            try
//            {
//                using (var context = new POSDbContext())
//                {
//                    var repo = new CustomerLedgerRepository(context);
//                    _allRows = await repo.GetLedgerAsync(_customerId, dtpFrom.Value, dtpTo.Value);
//                    var summary = await repo.GetLedgerSummaryAsync(_customerId, dtpFrom.Value, dtpTo.Value);
//                    _currentBalance = summary.CurrentBalance;

//                    BindGrid();
//                    UpdateKPIs(summary);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading ledger:\n{ex.Message}", "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {
//                SetLoading(false);
//            }
//        }

//        private void BindGrid()
//        {
//            LedgerGrid.DataSource = null;
//            LedgerGrid.DataSource = _allRows;

//            lblRowCount.Text = $"{_allRows.Count} entries";
//        }

//        private void UpdateKPIs(LedgerSummary summary)
//        {
//            lblTotalDebit.Text = $"PKR {summary.TotalDebit:N2}";
//            lblTotalCredit.Text = $"PKR {summary.TotalCredit:N2}";

//            decimal balance = summary.CurrentBalance;
//            lblCurrentBalance.Text = $"PKR {Math.Abs(balance):N2}";

//            if (balance > 0)
//            {
//                lblCurrentBalance.ForeColor = Color.FromArgb(192, 0, 0);
//                lblBalanceStatus.Text = "🔴 LOAN OUTSTANDING";
//                lblBalanceStatus.ForeColor = Color.FromArgb(192, 0, 0);
//                pnlBalanceKpi.BackColor = Color.FromArgb(255, 240, 240);
//            }
//            else if (balance < 0)
//            {
//                lblCurrentBalance.ForeColor = Color.FromArgb(0, 102, 204);
//                lblBalanceStatus.Text = "🔵 ADVANCE CREDIT";
//                lblBalanceStatus.ForeColor = Color.FromArgb(0, 102, 204);
//                pnlBalanceKpi.BackColor = Color.FromArgb(235, 245, 255);
//            }
//            else
//            {
//                lblCurrentBalance.ForeColor = Color.FromArgb(39, 174, 96);
//                lblBalanceStatus.Text = "✅ FULLY SETTLED";
//                lblBalanceStatus.ForeColor = Color.FromArgb(39, 174, 96);
//                pnlBalanceKpi.BackColor = Color.FromArgb(240, 255, 240);
//            }

//            // Show/hide action buttons based on state
//            ReceivePaymentBtn.Enabled = balance > 0;
//            ReceivePaymentBtn.BackColor = balance > 0
//                ? Color.FromArgb(39, 174, 96)
//                : Color.FromArgb(180, 180, 180);
//        }

//        private void SetLoading(bool loading)
//        {
//            LedgerGrid.Visible = !loading;
//            lblLoading.Visible = loading;
//            SearchBtn.Enabled = !loading;
//        }

//        // ─── Toolbar Actions ─────────────────────────────────────────────────
//        private async void SearchBtn_Click(object sender, EventArgs e)
//        {
//            await RefreshAsync();
//        }

//        private async void ReceivePaymentBtn_Click(object sender, EventArgs e)
//        {
//            var frm = new Customerpaymentform(_customerId, _customerName, _currentBalance, false);
//            if (frm.ShowDialog(this) == DialogResult.OK && frm.PaymentPosted)
//                await RefreshAsync();
//        }

//        private async void AddAdvanceBtn_Click(object sender, EventArgs e)
//        {
//            var frm = new Customerpaymentform(_customerId, _customerName, _currentBalance, true);
//            if (frm.ShowDialog(this) == DialogResult.OK && frm.PaymentPosted)
//                await RefreshAsync();
//        }

//        private async void AdjustmentBtn_Click(object sender, EventArgs e)
//        {
//            var frm = new AdjustmentForm(_customerId, _customerName, _currentBalance);
//            if (frm.ShowDialog(this) == DialogResult.OK && frm.AdjustmentPosted)
//                await RefreshAsync();
//        }

//        private void PrintBtn_Click(object sender, EventArgs e)
//        {
//            // Open report form for print/export
//            var report = new Customerledgerreportform(_customerId, _customerName,
//                dtpFrom.Value, dtpTo.Value, _allRows);
//            report.ShowDialog(this);
//        }

//        private async void ResetDatesBtn_Click(object sender, EventArgs e)
//        {
//            dtpFrom.Value = DateTime.Today.AddMonths(-3);
//            dtpTo.Value = DateTime.Today;
//            await RefreshAsync();
//        }
//    }
//}



using DocumentFormat.OpenXml.Spreadsheet;

using POS_Shop.Models;
using POS_Shop.Models.LoanModelsV1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
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
            this.Text = $"📒Ledger — {_customerName}";
            this.lblTitle.Text = $"📒Ledger Report — {_customerName} ";
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

            LedgerGrid.CellContentClick += LedgerGrid_CellContentClick;
            LedgerGrid.CellMouseEnter += LedgerGrid_CellMouseEnter;
            LedgerGrid.CellMouseLeave += LedgerGrid_CellMouseLeave;


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
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);

            // Color credit cells green
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "CreditDisplay" && row.Credit > 0)
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);

            // Color balance cells
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "BalanceDisplay")
            {
                if (row.Balance > 0) e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
                else if (row.Balance < 0) e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
                else e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            }

            // Color status badge
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "BalanceTypeDisplay")
            {
                switch (row.BalanceTypeDisplay)
                {
                    case "Loan":
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
                        break;
                    case "Advance":
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
                        break;
                    case "Clear":
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
                        break;
                }
            }

            // Note column — style as link if it contains INV
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "Note")
            {
                string noteVal = e.Value?.ToString() ?? "";
                if (noteVal.Contains("INV-"))
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
                    e.CellStyle.Font = new System.Drawing.Font("Segoe UI", 9,
                        System.Drawing.FontStyle.Underline);
                    e.CellStyle.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
                }
            }
        }

        private void LedgerGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _allRows.Count) return;
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName != "Note") return;

            string note = _allRows[e.RowIndex].Note ?? "";
            if (note.Contains("INV-"))
                LedgerGrid.Cursor = Cursors.Hand;
        }

        private void LedgerGrid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 &&
                LedgerGrid.Columns[e.ColumnIndex].DataPropertyName == "Note")
                LedgerGrid.Cursor = Cursors.Default;
        }

        private void LedgerGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _allRows.Count) return;
            if (LedgerGrid.Columns[e.ColumnIndex].DataPropertyName != "Note") return;

            string note = _allRows[e.RowIndex].Note ?? "";
            if (!note.Contains("INV-")) return;

            try
            {
                string[] parts = note.Split(new[] { "INV-" }, StringSplitOptions.None);
                if (parts.Length < 2) return;

                // Get just the invoice number token (stop at first space)
                string invoiceNo = parts[1].Split(' ')[0];

                OpenOrderDetail(invoiceNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open order: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenOrderDetail(string invoiceNo)
        {
            try
            {
                using (var context = new POSDbContext())
                {
                    var order = context.Orders.AsNoTracking()
                        .Include(o => o.OrderDetails)
                        .Include(o => o.OrderDetails.Select(od => od.Product))
                        .Include(o => o.Customer)
                        .FirstOrDefault(o => o.InvoiceNumber == invoiceNo);

                    if (order == null)
                    {
                        MessageBox.Show(
                            $"Invoice '{invoiceNo}' not found.\n\nIt may have been deleted.",
                            "Not Found",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    var detailForm = new OrderDetailViewForm(order);
                    detailForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                lblCurrentBalance.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
                lblBalanceStatus.Text = "🔴 LOAN OUTSTANDING";
                lblBalanceStatus.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
                pnlBalanceKpi.BackColor = System.Drawing.Color.FromArgb(255, 240, 240);
            }
            else if (balance < 0)
            {
                lblCurrentBalance.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
                lblBalanceStatus.Text = "🔵 ADVANCE CREDIT";
                lblBalanceStatus.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
                pnlBalanceKpi.BackColor = System.Drawing.Color.FromArgb(235, 245, 255);
            }
            else
            {
                lblCurrentBalance.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
                lblBalanceStatus.Text = "✅ FULLY SETTLED";
                lblBalanceStatus.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
                pnlBalanceKpi.BackColor = System.Drawing.Color.FromArgb(240, 255, 240);
            }

            // Show/hide action buttons based on state
            ReceivePaymentBtn.Enabled = balance > 0;
            ReceivePaymentBtn.BackColor = balance > 0
                ? System.Drawing.Color.FromArgb(39, 174, 96)
                : System.Drawing.Color.FromArgb(180, 180, 180);
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.R: ReceivePaymentBtn.PerformClick(); return true;
                case Keys.Control | Keys.A: AddAdvanceBtn.PerformClick(); return true;
                case Keys.Control | Keys.L: AdjustmentBtn.PerformClick(); return true;
                case Keys.Control | Keys.P: PrintBtn.PerformClick(); return true;
                case Keys.Control | Keys.I: ImageBtn.PerformClick(); return true;
                case Keys.Alt | Keys.F4: this.Close(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }




        //private void ImageBtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (SaveFileDialog sfd = new SaveFileDialog())
        //        {
        //            sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
        //            sfd.Title = "Save Ledger Statement as Image";
        //            sfd.FileName = $"Ledger_{_customerName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

        //            if (sfd.ShowDialog() == DialogResult.OK)
        //            {
        //                Bitmap finalImage = null;
        //                try
        //                {
        //                    finalImage = GenerateLedgerImage();
        //                    if (finalImage != null)
        //                    {
        //                        finalImage.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);

        //                        if (MessageBox.Show(
        //                                "Image saved successfully!\n\nDo you want to open it?",
        //                                "Success",
        //                                MessageBoxButtons.YesNo,
        //                                MessageBoxIcon.Question) == DialogResult.Yes)
        //                        {
        //                            System.Diagnostics.Process.Start(sfd.FileName);
        //                        }
        //                    }
        //                }
        //                finally
        //                {
        //                    finalImage?.Dispose();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message, "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        //private Bitmap GenerateLedgerImage()
        //{
        //    Bitmap bitmap = null;
        //    try
        //    {
        //        var rows = GetDataFromGrid();
        //        if (rows == null || rows.Count == 0)
        //        {
        //            MessageBox.Show("No data to display", "Warning",
        //                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return null;
        //        }

        //        // Layout constants
        //        const int colDate = 180;
        //        const int colDebit = 150;
        //        const int colCredit = 150;
        //        const int colBalance = 150;
        //        const int colStatus = 120;
        //        const int leftMargin = 20;
        //        const int rightMargin = 20;
        //        const int rowHeight = 20;
        //        const int headerHeight = 22;
        //        const int topSection = 80;   // title + period + summary
        //        const int footerSpace = 30;

        //        int totalWidth = leftMargin + colDate + colDebit + colCredit + colBalance + colStatus + rightMargin;
        //        int totalHeight = Math.Max(topSection + headerHeight + (rows.Count * rowHeight) + footerSpace, 400);

        //        // Column X positions — calculated once
        //        int col1X = leftMargin;
        //        int col2X = col1X + colDate;
        //        int col3X = col2X + colDebit;
        //        int col4X = col3X + colCredit;
        //        int col5X = col4X + colBalance;

        //        bitmap = new Bitmap(totalWidth, totalHeight);

        //        using (Graphics g = Graphics.FromImage(bitmap))
        //        {
        //            g.SmoothingMode = SmoothingMode.AntiAlias;
        //            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        //            g.Clear(Color.White);

        //            // All GDI objects created ONCE, outside the row loop
        //            using (Font titleFont = new Font("Arial", 14, FontStyle.Bold))
        //            using (Font normalFont = new Font("Arial", 9))
        //            using (Font smallFont = new Font("Arial", 8))
        //            using (Font tableHeaderFont = new Font("Arial", 9, FontStyle.Bold))
        //            using (Font tableFont = new Font("Arial", 8))
        //            using (Font boldTableFont = new Font("Arial", 8, FontStyle.Bold))
        //            using (Font summaryFont = new Font("Arial", 8, FontStyle.Bold))
        //            using (Pen lightPen = new Pen(Color.LightGray, 1))
        //            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
        //            using (SolidBrush grayBrush = new SolidBrush(Color.FromArgb(80, 80, 80)))
        //            using (SolidBrush blueBrush = new SolidBrush(Color.FromArgb(0, 102, 204)))
        //            using (SolidBrush headerFill = new SolidBrush(Color.FromArgb(0, 102, 204)))
        //            using (SolidBrush altRowBrush = new SolidBrush(Color.FromArgb(245, 245, 245)))
        //            // Use system brushes for common colors — no need to allocate new ones
        //            // Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.White are static, no dispose needed
        //            using (StringFormat centerFormat = new StringFormat
        //            {
        //                Alignment = StringAlignment.Center,
        //                LineAlignment = StringAlignment.Center
        //            })
        //            using (StringFormat rightFormat = new StringFormat
        //            {
        //                Alignment = StringAlignment.Far,
        //                LineAlignment = StringAlignment.Center
        //            })
        //            {
        //                int currentY = 15;

        //                // --- Header Section ---
        //                g.DrawString(_customerName ?? "Unknown", titleFont, blueBrush,
        //                    new PointF(leftMargin, currentY));
        //                currentY += 20;

        //                g.DrawString(
        //                    $"Period: {dtpFrom.Value:dd-MM-yyyy} to {dtpTo.Value:dd-MM-yyyy}",
        //                    normalFont, grayBrush, new PointF(leftMargin, currentY));
        //                currentY += 20;

        //                // Safe: use last row's status instead of rows.First()
        //                string lastStatus = rows[rows.Count - 1].Status ?? "";
        //                string summary = $"Debit: {lblTotalDebit.Text} | Credit: {lblTotalCredit.Text} | Balance: {lblCurrentBalance.Text} {lastStatus}";
        //                g.DrawString(summary, summaryFont, Brushes.Blue,
        //                    new PointF(leftMargin, currentY));
        //                currentY += 25;

        //                // --- Table Header ---
        //                int headerStartY = currentY; // Save this for vertical lines later
        //                Rectangle headerRect = new Rectangle(
        //                    leftMargin, currentY,
        //                    totalWidth - leftMargin - rightMargin, headerHeight);
        //                g.FillRectangle(headerFill, headerRect);

        //                g.DrawString("تاریخ", tableHeaderFont, Brushes.White, new Rectangle(col1X, currentY, colDate, headerHeight), centerFormat);
        //                // Debit
        //                g.DrawString("ادھار", tableHeaderFont, Brushes.White, new Rectangle(col2X, currentY, colDebit, headerHeight), rightFormat);
        //                // Credit
        //                g.DrawString("ایڈوانس", tableHeaderFont, Brushes.White, new Rectangle(col3X, currentY, colCredit, headerHeight), rightFormat);
        //           // Balanace
        //                g.DrawString("بیلنس", tableHeaderFont, Brushes.White, new Rectangle(col4X, currentY, colBalance, headerHeight), rightFormat);

        //                // Status
        //                g.DrawString("اسٹیٹس", tableHeaderFont, Brushes.White, new Rectangle(col5X, currentY, colStatus, headerHeight), centerFormat);
        //                currentY += headerHeight;

        //                // --- Table Rows ---
        //                bool alternate = false;
        //                foreach (var row in rows)
        //                {
        //                    if (alternate)
        //                        g.FillRectangle(altRowBrush,
        //                            new Rectangle(leftMargin, currentY,
        //                                totalWidth - leftMargin - rightMargin, rowHeight));

        //                    g.DrawString(row.Date ?? "", tableFont, blackBrush,
        //                        new Rectangle(col1X, currentY, colDate, rowHeight), centerFormat);

        //                    // Debit
        //                    if (row.Debit > 0)
        //                        g.DrawString(row.Debit.ToString("N0"), tableFont, Brushes.Red,
        //                            new Rectangle(col2X, currentY, colDebit, rowHeight), rightFormat);
        //                    else
        //                        g.DrawString("-", tableFont, grayBrush,
        //                            new Rectangle(col2X, currentY, colDebit, rowHeight), rightFormat);

        //                    // Credit
        //                    if (row.Credit > 0)
        //                        g.DrawString(row.Credit.ToString("N0"), tableFont, Brushes.Green,
        //                            new Rectangle(col3X, currentY, colCredit, rowHeight), rightFormat);
        //                    else
        //                        g.DrawString("-", tableFont, grayBrush,
        //                            new Rectangle(col3X, currentY, colCredit, rowHeight), rightFormat);

        //                    // Balance — no cast needed, use local variable
        //                    Brush balanceBrush = row.Balance > 0 ? Brushes.Blue : (Brush)blackBrush;
        //                    g.DrawString(row.Balance.ToString("N0"), boldTableFont, balanceBrush,
        //                        new Rectangle(col4X, currentY, colBalance, rowHeight), rightFormat);

        //                    // Status — reuse pre-created brushes, zero allocations in loop
        //                    Brush statusBrush = (row.Status ?? "").Contains("Loan")
        //                        ? Brushes.Orange
        //                        : Brushes.Green;
        //                    g.DrawString(row.Status ?? "", tableFont, statusBrush,
        //                        new Rectangle(col5X, currentY, colStatus, rowHeight), centerFormat);

        //                    g.DrawLine(lightPen,
        //                        leftMargin, currentY + rowHeight - 1,
        //                        totalWidth - rightMargin, currentY + rowHeight - 1);

        //                    currentY += rowHeight;
        //                    alternate = !alternate;
        //                }

        //                // --- Vertical Lines ---
        //                // Use headerStartY — no math tricks, clear and correct
        //                g.DrawLine(lightPen, col1X, headerStartY, col1X, currentY);
        //                g.DrawLine(lightPen, col2X, headerStartY, col2X, currentY);
        //                g.DrawLine(lightPen, col3X, headerStartY, col3X, currentY);
        //                g.DrawLine(lightPen, col4X, headerStartY, col4X, currentY);
        //                g.DrawLine(lightPen, col5X, headerStartY, col5X, currentY);

        //                // --- Footer ---
        //                g.DrawString($"{_customerName ??""} Printed: {DateTime.Now:dd-MMM-yyyy HH:mm}",
        //                    smallFont, grayBrush, new PointF(leftMargin, totalHeight - 18));

        //            }
        //        }

        //        return bitmap;
        //    }
        //    catch (Exception ex)
        //    {
        //        bitmap?.Dispose();
        //        MessageBox.Show("Error generating image: " + ex.Message, "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //}


        //private List<LedgerRow> GetDataFromGrid()
        //{
        //    var rows = new List<LedgerRow>();
        //    try
        //    {
        //        // Build column index map ONCE using a dictionary — O(n) build, O(1) lookup
        //        var colMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        //        foreach (DataGridViewColumn col in LedgerGrid.Columns)
        //        {
        //            if (!colMap.ContainsKey(col.Name))
        //                colMap[col.Name] = col.Index;
        //            if (!colMap.ContainsKey(col.HeaderText))
        //                colMap[col.HeaderText] = col.Index;
        //        }

        //        // Resolve indexes with fallback
        //        int dateColIdx = colMap.TryGetValue("Date", out int d) ? d : 0;
        //        int debitColIdx = colMap.TryGetValue("Debit", out int db) ? db : 2;
        //        int creditColIdx = colMap.TryGetValue("Credit", out int cr) ? cr : 3;
        //        int balanceColIdx = colMap.TryGetValue("Balance", out int bl) ? bl : 4;
        //        int statusColIdx = colMap.TryGetValue("Status", out int st) ? st : 5;

        //        foreach (DataGridViewRow row in LedgerGrid.Rows)
        //        {
        //            if (row.IsNewRow) continue;

        //            rows.Add(new LedgerRow
        //            {
        //                Date = ParseDateCell(row.Cells[dateColIdx].Value),
        //                Debit = ParseDecimalCell(row.Cells[debitColIdx].Value),
        //                Credit = ParseDecimalCell(row.Cells[creditColIdx].Value),
        //                Balance = ParseDecimalCell(row.Cells[balanceColIdx].Value),
        //                Status = row.Cells[statusColIdx].Value?.ToString() ?? ""
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error reading grid data: " + ex.Message, "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return rows;
        //}

        //private string ParseDateCell(object value)
        //{
        //    if (value == null || value == DBNull.Value) return "";
        //    return DateTime.TryParse(value.ToString(), out DateTime date)
        //        ? date.ToString("dd-MMM-yy")
        //        : value.ToString();
        //}

        //private decimal ParseDecimalCell(object value)
        //{
        //    if (value == null || value == DBNull.Value) return 0;
        //    string val = value.ToString()
        //        .Replace(",", "")
        //        .Replace("PKR", "")
        //        .Trim();
        //    return !string.IsNullOrEmpty(val) && val != "-"
        //        && decimal.TryParse(val, out decimal result) ? result : 0;
        //}

        //public class LedgerRow
        //{
        //    public string Date { get; set; }
        //    public decimal Debit { get; set; }
        //    public decimal Credit { get; set; }
        //    public decimal Balance { get; set; }
        //    public string Status { get; set; }
        //}





        // ============================================================
        // IMAGE BUTTON CLICK — async to prevent UI freeze
        // ============================================================
        private async void ImageBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = GetDataFromGrid();
                if (rows == null || rows.Count == 0)
                {
                    MessageBox.Show("No data to display", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    string safeName = string.IsNullOrWhiteSpace(_customerName)
                        ? "Customer"
                        : _customerName.Trim().Replace(" ", "_");

                    sfd.Filter = "PNG Image|*.png";
                    sfd.Title = "Save Ledger Statement as Image";
                    sfd.FileName = $"Ledger_{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    string basePath = System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(sfd.FileName),
                        System.IO.Path.GetFileNameWithoutExtension(sfd.FileName));

                    // Snapshot UI values BEFORE going async
                    var snapshot = new LedgerSnapshot
                    {
                        CustomerName = _customerName ?? "Unknown",
                        PeriodFrom = dtpFrom.Value.ToString("dd-MM-yyyy"),
                        PeriodTo = dtpTo.Value.ToString("dd-MM-yyyy"),
                        TotalDebit = lblTotalDebit.Text,
                        TotalCredit = lblTotalCredit.Text,
                        CurrentBalance = lblCurrentBalance.Text,
                        PrintedOn = DateTime.Now.ToString("dd-MMM-yyyy HH:mm"),
                        Rows = rows
                    };

                    // Disable button — prevent double click
                    ImageBtn.Enabled = false;

                    var savedFiles = new List<string>();

                    try
                    {
                        const int RowsPerPage = 100;
                        int totalPages = (int)Math.Ceiling(
                            rows.Count / (double)RowsPerPage);

                        // Generate all pages on background thread
                        await Task.Run(() =>
                        {
                            for (int page = 0; page < totalPages; page++)
                            {
                                var pageRows = snapshot.Rows
                                    .Skip(page * RowsPerPage)
                                    .Take(RowsPerPage)
                                    .ToList();

                                string filePath = totalPages == 1
                                    ? basePath + ".png"
                                    : $"{basePath}_Page{page + 1}of{totalPages}.png";

                                Bitmap pageImage = null;
                                try
                                {
                                    pageImage = GenerateLedgerImage(
                                        snapshot, pageRows, page + 1, totalPages);

                                    if (pageImage != null)
                                    {
                                        pageImage.Save(filePath,
                                            System.Drawing.Imaging.ImageFormat.Png);
                                        savedFiles.Add(filePath);
                                    }
                                }
                                finally
                                {
                                    pageImage?.Dispose();
                                }
                            }
                        });

                        if (savedFiles.Count > 0)
                        {
                            string fileList = string.Join("\n",
                                savedFiles.Select(f =>
                                    System.IO.Path.GetFileName(f)));

                            if (MessageBox.Show(
                                    $"{savedFiles.Count} image(s) saved!\n\n{fileList}\n\nOpen folder?",
                                    "Success",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Process.Start(new ProcessStartInfo("explorer.exe",
                                    $"/select,\"{savedFiles[0]}\"")
                                {
                                    UseShellExecute = true
                                });
                            }
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show(
                            "Cannot save — the file is already open.\n\nClose it and try again.",
                            "File In Use",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    finally
                    {
                        ImageBtn.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // SNAPSHOT CLASS — safely carries UI data off UI thread
        // ============================================================
        private class LedgerSnapshot
        {
            public string CustomerName { get; set; }
            public string PeriodFrom { get; set; }
            public string PeriodTo { get; set; }
            public string TotalDebit { get; set; }
            public string TotalCredit { get; set; }
            public string CurrentBalance { get; set; }
            public string PrintedOn { get; set; }
            public List<LedgerRow> Rows { get; set; }
        }


        // ============================================================
        // GENERATE LEDGER IMAGE — ONE PAGE
        // ============================================================
        //private Bitmap GenerateLedgerImage(LedgerSnapshot s,
        //    List<LedgerRow> rows, int currentPage, int totalPages)
        //        {
        //            Bitmap bitmap = null;
        //            try
        //            {
        //                // Layout constants
        //                const int colDate = 180;
        //                const int colDebit = 150;
        //                const int colCredit = 150;
        //                const int colBalance = 150;
        //                const int colStatus = 120;
        //                const int leftMargin = 20;
        //                const int rightMargin = 20;
        //                const int rowHeight = 20;
        //                const int headerHeight = 22;
        //                const int topSection = 80;
        //                const int footerSpace = 30;

        //                int totalWidth = leftMargin + colDate + colDebit + colCredit
        //                                + colBalance + colStatus + rightMargin;
        //                int totalHeight = Math.Max(
        //                    topSection + headerHeight + (rows.Count * rowHeight)
        //                    + footerSpace, 400);

        //                int col1X = leftMargin;
        //                int col2X = col1X + colDate;
        //                int col3X = col2X + colDebit;
        //                int col4X = col3X + colCredit;
        //                int col5X = col4X + colBalance;

        //                bitmap = new Bitmap(totalWidth, totalHeight);

        //                using (Graphics g = Graphics.FromImage(bitmap))
        //                {
        //                    g.SmoothingMode = SmoothingMode.AntiAlias;
        //                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
        //                    g.Clear(Color.White);

        //                    // All GDI objects created ONCE — zero allocations in row loop
        //                    using (Font titleFont = new Font("Arial", 14, FontStyle.Bold))
        //                    using (Font normalFont = new Font("Arial", 9))
        //                    using (Font smallFont = new Font("Arial", 8))
        //                    using (Font tableHeaderFont = new Font("Arial", 9, FontStyle.Bold))
        //                    using (Font tableFont = new Font("Arial", 8))
        //                    using (Font boldTableFont = new Font("Arial", 8, FontStyle.Bold))
        //                    using (Font summaryFont = new Font("Arial", 8, FontStyle.Bold))
        //                    using (Pen lightPen = new Pen(Color.LightGray, 1))
        //                    using (SolidBrush blackBrush = new SolidBrush(Color.Black))
        //                    using (SolidBrush grayBrush = new SolidBrush(Color.FromArgb(80, 80, 80)))
        //                    using (SolidBrush blueBrush = new SolidBrush(Color.FromArgb(0, 102, 204)))
        //                    using (SolidBrush headerFill = new SolidBrush(Color.FromArgb(0, 102, 204)))
        //                    using (SolidBrush altRowBrush = new SolidBrush(Color.FromArgb(245, 245, 245)))
        //                    using (StringFormat centerFormat = new StringFormat
        //                    {
        //                        Alignment = StringAlignment.Center,
        //                        LineAlignment = StringAlignment.Center
        //                    })
        //                    using (StringFormat rightFormat = new StringFormat
        //                    {
        //                        Alignment = StringAlignment.Far,
        //                        LineAlignment = StringAlignment.Center
        //                    })
        //                    {
        //                        int currentY = 15;

        //                        // ── Title + page number ───────────────────────
        //                        g.DrawString(s.CustomerName, titleFont, blueBrush,
        //                            new PointF(leftMargin, currentY));

        //                        if (totalPages > 1)
        //                            g.DrawString(
        //                                $"Page {currentPage} of {totalPages}",
        //                                normalFont, grayBrush,
        //                                new PointF(totalWidth - rightMargin - 120, currentY));
        //                        currentY += 20;

        //                        // ── Period ────────────────────────────────────
        //                        g.DrawString(
        //                            $"Period: {s.PeriodFrom} to {s.PeriodTo}",
        //                            normalFont, grayBrush,
        //                            new PointF(leftMargin, currentY));
        //                        currentY += 20;

        //                        // ── Summary (first page only) ─────────────────
        //                        if (currentPage == 1)
        //                        {
        //                            string lastStatus = rows.Count > 0
        //                                ? rows[rows.Count - 1].Status ?? "" : "";
        //                            string summary =
        //                                $"Debit: {s.TotalDebit}  |  " +
        //                                $"Credit: {s.TotalCredit}  |  " +
        //                                $"Balance: {s.CurrentBalance}  {lastStatus}";
        //                            g.DrawString(summary, summaryFont, Brushes.Blue,
        //                                new PointF(leftMargin, currentY));
        //                        }
        //                        currentY += 25;

        //                        // ── Table header ──────────────────────────────
        //                        int headerStartY = currentY;
        //                        g.FillRectangle(headerFill,
        //                            new Rectangle(leftMargin, currentY,
        //                                totalWidth - leftMargin - rightMargin,
        //                                headerHeight));

        //                        g.DrawString("DATE (تاریخ)", tableHeaderFont, Brushes.White, new Rectangle(col1X, currentY, colDate, headerHeight), centerFormat);
        //                        g.DrawString("CREDIT(ادھار)", tableHeaderFont, Brushes.White, new Rectangle(col2X, currentY, colDebit, headerHeight), rightFormat);
        //                        g.DrawString("DEBIT(ایڈوانس)", tableHeaderFont, Brushes.White, new Rectangle(col3X, currentY, colCredit, headerHeight), rightFormat);
        //                        g.DrawString("BALANCE(بیلنس)", tableHeaderFont, Brushes.White, new Rectangle(col4X, currentY, colBalance, headerHeight), rightFormat);
        //                        g.DrawString("STATUS", tableHeaderFont, Brushes.White, new Rectangle(col5X, currentY, colStatus, headerHeight), centerFormat);
        //                        currentY += headerHeight;

        //                        // ── Data rows ─────────────────────────────────
        //                        bool alternate = false;
        //                        foreach (var row in rows)
        //                        {
        //                            if (alternate)
        //                                g.FillRectangle(altRowBrush,
        //                                    new Rectangle(leftMargin, currentY,
        //                                        totalWidth - leftMargin - rightMargin,
        //                                        rowHeight));

        //                            g.DrawString(row.Date ?? "", tableFont, blackBrush,
        //                                new Rectangle(col1X, currentY, colDate, rowHeight),
        //                                centerFormat);

        //                            if (row.Debit > 0)
        //                                g.DrawString(row.Debit.ToString("N0"), tableFont,
        //                                    Brushes.Red,
        //                                    new Rectangle(col2X, currentY, colDebit, rowHeight),
        //                                    rightFormat);
        //                            else
        //                                g.DrawString("-", tableFont, grayBrush,
        //                                    new Rectangle(col2X, currentY, colDebit, rowHeight),
        //                                    rightFormat);

        //                            if (row.Credit > 0)
        //                                g.DrawString(row.Credit.ToString("N0"), tableFont,
        //                                    Brushes.Green,
        //                                    new Rectangle(col3X, currentY, colCredit, rowHeight),
        //                                    rightFormat);
        //                            else
        //                                g.DrawString("-", tableFont, grayBrush,
        //                                    new Rectangle(col3X, currentY, colCredit, rowHeight),
        //                                    rightFormat);

        //                            Brush balanceBrush = row.Balance > 0
        //                                ? Brushes.Blue : (Brush)blackBrush;
        //                            g.DrawString(row.Balance.ToString("N0"), boldTableFont,
        //                                balanceBrush,
        //                                new Rectangle(col4X, currentY, colBalance, rowHeight),
        //                                rightFormat);

        //                            Brush statusBrush = (row.Status ?? "").Contains("Loan")
        //                                ? Brushes.Orange : Brushes.Green;
        //                            g.DrawString(row.Status ?? "", tableFont, statusBrush,
        //                                new Rectangle(col5X, currentY, colStatus, rowHeight),
        //                                centerFormat);

        //                            g.DrawLine(lightPen,
        //                                leftMargin, currentY + rowHeight - 1,
        //                                totalWidth - rightMargin, currentY + rowHeight - 1);

        //                            currentY += rowHeight;
        //                            alternate = !alternate;
        //                        }

        //                        // ── Vertical lines ────────────────────────────
        //                        g.DrawLine(lightPen, col1X, headerStartY, col1X, currentY);
        //                        g.DrawLine(lightPen, col2X, headerStartY, col2X, currentY);
        //                        g.DrawLine(lightPen, col3X, headerStartY, col3X, currentY);
        //                        g.DrawLine(lightPen, col4X, headerStartY, col4X, currentY);
        //                        g.DrawLine(lightPen, col5X, headerStartY, col5X, currentY);

        //                        // ── Footer ────────────────────────────────────
        //                        string footerRight = totalPages > 1
        //                            ? $"Page {currentPage} of {totalPages}"
        //                            : s.CustomerName;

        //                        g.DrawString($"Printed: {s.PrintedOn}",
        //                            smallFont, grayBrush,
        //                            new PointF(leftMargin, totalHeight - 18));
        //                        g.DrawString(footerRight,
        //                            smallFont, grayBrush,
        //                            new PointF(totalWidth - 150, totalHeight - 18));
        //                    }
        //                }

        //                return bitmap;
        //            }
        //            catch (Exception ex)
        //            {
        //                bitmap?.Dispose();
        //                MessageBox.Show($"Error generating page {currentPage}: " + ex.Message,
        //                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return null;
        //            }
        //        }


        // ============================================================
        // GET DATA FROM GRID
        // ============================================================


        // ============================================================
        // GENERATE LEDGER IMAGE — TIGHT PORTRAIT WIDTH
        // ============================================================
        private Bitmap GenerateLedgerImage(LedgerSnapshot s,
            List<LedgerRow> rows, int currentPage, int totalPages)
        {
            Bitmap bitmap = null;
            try
            {
                // ── Column widths (tuned tight for portrait) ──────────
                const int colDate = 80;   // dd-MMM-yy   → 80 is enough
                const int colDebit = 80;   // right-align numbers
                const int colCredit = 80;   // right-align numbers
                const int colBalance = 85;   // slightly wider — bold numbers
                const int colStatus = 70;   // "Loan" / "Clear" short words
                const int leftMargin = 12;
                const int rightMargin = 12;
                const int rowHeight = 20;
                const int headerHeight = 22;
                const int topSection = 75;
                const int footerSpace = 25;

                // Total width = 12 + 80 + 80 + 80 + 85 + 70 + 12 = 419px
                // Clean portrait width — like a receipt/statement
                int totalWidth = leftMargin + colDate + colDebit + colCredit
                               + colBalance + colStatus + rightMargin;

                int totalHeight = Math.Max(
                    topSection + headerHeight + (rows.Count * rowHeight)
                    + footerSpace, 300);

                // Column X start positions
                int col1X = leftMargin;
                int col2X = col1X + colDate;
                int col3X = col2X + colDebit;
                int col4X = col3X + colCredit;
                int col5X = col4X + colBalance;

                bitmap = new Bitmap(totalWidth, totalHeight);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    g.Clear(System.Drawing.Color.White);

                    // All GDI objects — created once, zero allocs in loop
                    using (System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 11, FontStyle.Bold))
                    using (System.Drawing.Font normalFont = new System.Drawing.Font("Arial", 7))
                    using (System.Drawing.Font smallFont = new System.Drawing.Font("Arial", 6))
                    using (System.Drawing.Font tableHeaderFont = new System.Drawing.Font("Arial", 7, FontStyle.Bold))
                    using (System.Drawing.Font tableFont = new System.Drawing.Font("Arial", 7))
                    using (System.Drawing.Font boldTableFont = new System.Drawing.Font("Arial", 7, FontStyle.Bold))
                    using (System.Drawing.Font summaryFont = new System.Drawing.Font("Arial", 6, FontStyle.Bold))
                    using (Pen lightPen = new Pen(System.Drawing.Color.LightGray, 0.5f))
                    using (Pen borderPen = new Pen(System.Drawing.Color.FromArgb(180, 180, 180), 0.5f))
                    using (SolidBrush blackBrush = new SolidBrush(System.Drawing.Color.Black))
                    using (SolidBrush grayBrush = new SolidBrush(System.Drawing.Color.FromArgb(140, 140, 140)))
                    using (SolidBrush blueBrush = new SolidBrush(System.Drawing.Color.FromArgb(0, 102, 204)))
                    using (SolidBrush headerFill = new SolidBrush(System.Drawing.Color.FromArgb(0, 102, 204)))
                    using (SolidBrush altRowBrush = new SolidBrush(System.Drawing.Color.FromArgb(247, 247, 247)))
                    using (StringFormat centerFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter
                    })
                    using (StringFormat rightFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Far,
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter
                    })
                    using (StringFormat leftFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter
                    })
                    {
                        int currentY = 10;

                        // ── Title ─────────────────────────────────────
                        g.DrawString(s.CustomerName, titleFont, blueBrush,
                            new PointF(leftMargin, currentY));
                        currentY += 16;

                        // ── Period ────────────────────────────────────
                        g.DrawString(
                            $"Period: {s.PeriodFrom}  to  {s.PeriodTo}",
                            normalFont, grayBrush,
                            new PointF(leftMargin, currentY));
                        currentY += 14;

                        // ── Page number (only if multi-page) ──────────
                        if (totalPages > 1)
                        {
                            g.DrawString(
                                $"Page {currentPage} of {totalPages}",
                                smallFont, grayBrush,
                                new PointF(leftMargin, currentY));
                            currentY += 12;
                        }

                        // ── Summary line ──────────────────────────────
                        string lastStatus = rows.Count > 0
                            ? rows[rows.Count - 1].Status ?? "" : "";
                        string summary =
                            $"Dr: {s.TotalDebit}  Cr: {s.TotalCredit}" +
                            $"  Bal: {s.CurrentBalance}";
                        g.DrawString(summary, summaryFont, Brushes.Blue,
                            new PointF(leftMargin, currentY));
                        currentY += 14;

                        // ── Thin divider under header section ─────────
                        g.DrawLine(borderPen,
                            leftMargin, currentY,
                            totalWidth - rightMargin, currentY);
                        currentY += 5;

                        // ── Table header bar ──────────────────────────
                        int headerStartY = currentY;
                        g.FillRectangle(headerFill,
                            new Rectangle(leftMargin, currentY,
                                totalWidth - leftMargin - rightMargin,
                                headerHeight));

                        // Inner padding for header text
                        const int hp = 3; // horizontal padding inside cell
                        g.DrawString("DATE (تاریخ)",
                            tableHeaderFont, Brushes.White,
                            new Rectangle(col1X + hp, currentY,
                                colDate - hp, headerHeight),
                            leftFormat);

                        g.DrawString("CREDIT(ادھار)",
                            tableHeaderFont, Brushes.White,
                            new Rectangle(col2X, currentY,
                                colDebit - hp, headerHeight),
                            rightFormat);

                        g.DrawString("DEBIT(وصول)",
                            tableHeaderFont, Brushes.White,
                            new Rectangle(col3X, currentY,
                                colCredit - hp, headerHeight),
                            rightFormat);

                        g.DrawString("BALANCE(بقایا)",
                            tableHeaderFont, Brushes.White,
                            new Rectangle(col4X, currentY,
                                colBalance - hp, headerHeight),
                            rightFormat);

                        g.DrawString("STATUS",
                            tableHeaderFont, Brushes.White,
                            new Rectangle(col5X, currentY,
                                colStatus - hp, headerHeight),
                            centerFormat);

                        currentY += headerHeight;

                        // ── Data rows ─────────────────────────────────
                        bool alternate = false;
                        foreach (var row in rows)
                        {
                            if (alternate)
                                g.FillRectangle(altRowBrush,
                                    new Rectangle(leftMargin, currentY,
                                        totalWidth - leftMargin - rightMargin,
                                        rowHeight));

                            // Date — left aligned, compact
                            g.DrawString(row.Date ?? "", tableFont, blackBrush,
                                new Rectangle(col1X + hp, currentY,
                                    colDate - hp, rowHeight),
                                leftFormat);

                            // Debit
                            if (row.Debit > 0)
                                g.DrawString(
                                    row.Debit.ToString("N0"), tableFont, Brushes.Red,
                                    new Rectangle(col2X, currentY,
                                        colDebit - hp, rowHeight),
                                    rightFormat);
                            else
                                g.DrawString("-", tableFont, grayBrush,
                                    new Rectangle(col2X, currentY,
                                        colDebit - hp, rowHeight),
                                    rightFormat);

                            // Credit
                            if (row.Credit > 0)
                                g.DrawString(
                                    row.Credit.ToString("N0"), tableFont, Brushes.Green,
                                    new Rectangle(col3X, currentY,
                                        colCredit - hp, rowHeight),
                                    rightFormat);
                            else
                                g.DrawString("-", tableFont, grayBrush,
                                    new Rectangle(col3X, currentY,
                                        colCredit - hp, rowHeight),
                                    rightFormat);

                            // Balance — bold
                            Brush balanceBrush = row.Balance > 0
                                ? Brushes.Blue : (Brush)blackBrush;
                            g.DrawString(
                                row.Balance.ToString("N0"), boldTableFont, balanceBrush,
                                new Rectangle(col4X, currentY,
                                    colBalance - hp, rowHeight),
                                rightFormat);

                            // Status
                            Brush statusBrush = (row.Status ?? "").Contains("Loan")
                                ? Brushes.DarkRed : Brushes.Green;
                            g.DrawString(row.Status ?? "", tableFont, statusBrush,
                                new Rectangle(col5X, currentY,
                                    colStatus - hp, rowHeight),
                                centerFormat);

                            // Row divider
                            g.DrawLine(lightPen,
                                leftMargin, currentY + rowHeight - 1,
                                totalWidth - rightMargin, currentY + rowHeight - 1);

                            currentY += rowHeight;
                            alternate = !alternate;
                        }

                        // ── Vertical column separators ─────────────────
                        g.DrawLine(lightPen, col2X, headerStartY, col2X, currentY);
                        g.DrawLine(lightPen, col3X, headerStartY, col3X, currentY);
                        g.DrawLine(lightPen, col4X, headerStartY, col4X, currentY);
                        g.DrawLine(lightPen, col5X, headerStartY, col5X, currentY);

                        // Outer border around entire table
                        g.DrawRectangle(borderPen,
                            new Rectangle(leftMargin, headerStartY,
                                totalWidth - leftMargin - rightMargin,
                                currentY - headerStartY));

                        // ── Footer ────────────────────────────────────
                        currentY += 6;
                        g.DrawLine(borderPen,
                            leftMargin, currentY,
                            totalWidth - rightMargin, currentY);
                        currentY += 4;

                        g.DrawString($"Printed: {s.PrintedOn}",
                            smallFont, grayBrush,
                            new PointF(leftMargin, currentY));

                        if (totalPages > 1)
                            g.DrawString(
                                $"Page {currentPage} of {totalPages}",
                                smallFont, grayBrush,
                                new PointF(totalWidth - rightMargin - 70, currentY));
                    }
                }

                return bitmap;
            }
            catch (Exception ex)
            {
                bitmap?.Dispose();
                MessageBox.Show($"Error generating page {currentPage}: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private List<LedgerRow> GetDataFromGrid()
        {
            var rows = new List<LedgerRow>();
            try
            {
                var colMap = new Dictionary<string, int>(
                    StringComparer.OrdinalIgnoreCase);

                foreach (DataGridViewColumn col in LedgerGrid.Columns)
                {
                    if (!colMap.ContainsKey(col.Name))
                        colMap[col.Name] = col.Index;
                    if (!colMap.ContainsKey(col.HeaderText))
                        colMap[col.HeaderText] = col.Index;
                }

                int dateColIdx = colMap.TryGetValue("Date", out int d) ? d : 0;
                int debitColIdx = colMap.TryGetValue("Debit", out int db) ? db : 2;
                int creditColIdx = colMap.TryGetValue("Credit", out int cr) ? cr : 3;
                int balanceColIdx = colMap.TryGetValue("Balance", out int bl) ? bl : 4;
                int statusColIdx = colMap.TryGetValue("Status", out int st) ? st : 5;

                foreach (DataGridViewRow row in LedgerGrid.Rows)
                {
                    if (row.IsNewRow) continue;
                    rows.Add(new LedgerRow
                    {
                        Date = ParseDateCell(row.Cells[dateColIdx].Value),
                        Debit = ParseDecimalCell(row.Cells[debitColIdx].Value),
                        Credit = ParseDecimalCell(row.Cells[creditColIdx].Value),
                        Balance = ParseDecimalCell(row.Cells[balanceColIdx].Value),
                        Status = row.Cells[statusColIdx].Value?.ToString() ?? ""
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading grid data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rows;
        }


        // ============================================================
        // HELPERS
        // ============================================================
        private string ParseDateCell(object value)
        {
            if (value == null || value == DBNull.Value) return "";
            return DateTime.TryParse(value.ToString(), out DateTime date)
                ? date.ToString("dd-MMM-yy")
                : value.ToString();
        }

        private decimal ParseDecimalCell(object value)
        {
            if (value == null || value == DBNull.Value) return 0;
            string val = value.ToString()
                .Replace(",", "")
                .Replace("PKR", "")
                .Trim();
            return !string.IsNullOrEmpty(val) && val != "-"
                && decimal.TryParse(val, out decimal result) ? result : 0;
        }


        // ============================================================
        // LEDGER ROW MODEL
        // ============================================================
        public class LedgerRow
        {
            public string Date { get; set; }
            public decimal Debit { get; set; }
            public decimal Credit { get; set; }
            public decimal Balance { get; set; }
            public string Status { get; set; }
        }


    }
}
