//using POS_Shop.Models;
//using POS_Shop.Models.Suppliers;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Controllers.Supplier
//{
//    /// <summary>
//    /// Supplier Payment Form
//    /// ─────────────────────────────────────────────────────────────────────
//    /// Use Case handled:
//    ///   Supplier sends products on Monday, Wednesday and Friday (3 invoices).
//    ///   User pays once on Friday covering all 3, or covers 2 invoices fully
//    ///   and 1 partially. This form handles both cases.
//    ///
//    /// Flow:
//    ///   1. Select supplier → all their Pending / PartiallyPaid invoices load
//    ///   2. Enter total amount paid (e.g. Rs 50,000)
//    ///   3. Either click "Auto Allocate" (oldest-first) or manually type
//    ///      amounts in the green "Allocate" column
//    ///   4. Unallocated amount must reach 0 before Save is allowed
//    ///   5. On Save:
//    ///        - SupplierPayment + SupplierPaymentDetails saved
//    ///        - Each touched Purchase.RecalculateFromPayments() called
//    ///        - Invoice statuses update: Pending → PartiallyPaid → Paid
//    /// </summary>
//    public partial class SupplierPaymentForm : Form
//    {
//        private readonly POSDbContext _db;

//        private int? _selectedSupplierId;
//        private string _selectedSupplierName;

//        private readonly Timer _supTimer = new Timer { Interval = 300 };

//        private bool _suppressSupEvent;
//        private bool _suppressGridEvent;
//        private bool _suppressAmtEvent;

//        public SupplierPaymentForm()
//        {
//            InitializeComponent();
//            _db = new POSDbContext();
//            WireEvents();
//            SetupForm();
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  SETUP
//        // ══════════════════════════════════════════════════════════════════

//        private void SetupForm()
//        {
//            lblHeaderDate.Text = "Date: " + DateTime.Now.ToString("dd MMM yyyy");
//            lblPayNo.Text = GeneratePaymentNumber();
//            dtpPayDate.Value = DateTime.Now;
//        }

//        private void WireEvents()
//        {
//            // Basic events are wired in Designer.cs.
//            // Only lambdas and things the designer cannot handle go here:

//            // Leave event for suggestion list focus management
//            lstSupSugg.Leave += LstSupSugg_Leave;

//            // Debounce timer (lambda - must stay out of designer)
//            _supTimer.Tick += (s, e) => { _supTimer.Stop(); SearchSuppliers(txtSupSearch.Text.Trim()); };

//            // Cancel button (lambda)
//            btnCancel.Click += (s, e) => this.Close();

//            // Hover colour effects
//            HoverBtn(btnAutoAllocate, Color.FromArgb(13, 71, 161), Color.FromArgb(21, 101, 192));
//            HoverBtn(btnSave, Color.FromArgb(27, 94, 32), Color.FromArgb(46, 125, 50));

//            // Resize - reposition floating dropdown
//            this.Resize += (s, e) => RepositionDropdown();
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  SUPPLIER SEARCH
//        // ══════════════════════════════════════════════════════════════════

//        private void TxtSupSearch_TextChanged(object sender, EventArgs e)
//        {
//            if (_suppressSupEvent) return;
//            if (_selectedSupplierId.HasValue) ClearSupplier(false);
//            _supTimer.Stop();
//            _supTimer.Start();
//        }

//        private void SearchSuppliers(string q)
//        {
//            if (q.Length < 1) { HideSugg(); return; }
//            try
//            {
//                var list = _db.Suppliers
//                    .Where(s => !s.IsDeleted &&
//                               (s.SupplierName.Contains(q) || s.ShopName.Contains(q) || s.ContactNo.Contains(q)))
//                    .OrderBy(s => s.SupplierName).Take(8).ToList();

//                lstSupSugg.DataSource = list.Count > 0 ? (object)list : null;
//                lstSupSugg.DisplayMember = "SupplierName";
//                lstSupSugg.ValueMember = "Id";
//                ShowSugg(list.Count);
//            }
//            catch (Exception ex) { MessageBox.Show("Search error: " + ex.Message); }
//        }

//        private void TxtSupSearch_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (!lstSupSugg.Visible) return;
//            if (e.KeyCode == Keys.Down) { lstSupSugg.Focus(); if (lstSupSugg.Items.Count > 0) lstSupSugg.SelectedIndex = 0; e.Handled = true; }
//            else if (e.KeyCode == Keys.Escape) { HideSugg(); e.Handled = true; }
//        }

//        private void LstSupSugg_MouseClick(object sender, MouseEventArgs e) => SelectSupplier();
//        private void LstSupSugg_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter) { SelectSupplier(); e.Handled = true; }
//            else if (e.KeyCode == Keys.Escape) { HideSugg(); txtSupSearch.Focus(); e.Handled = true; }
//        }

//        private void TxtSupSearch_Leave(object sender, EventArgs e) { if (!lstSupSugg.Focused) HideSugg(); }
//        private void LstSupSugg_Leave(object sender, EventArgs e) { if (!txtSupSearch.Focused) HideSugg(); }

//        private void SelectSupplier()
//        {
//            if (!(lstSupSugg.SelectedItem is Models.Supplier s)) return;
//            _selectedSupplierId = s.Id;
//            _selectedSupplierName = $"{s.SupplierName}  —  {s.ShopName}";

//            _suppressSupEvent = true;
//            txtSupSearch.Text = string.Empty;
//            _suppressSupEvent = false;

//            lblSelSup.Text = _selectedSupplierName;
//            pnlSupBadge.Visible = true;
//            HideSugg();

//            LoadPendingInvoices(s.Id);
//            txtTotalAmt.Focus();
//            txtTotalAmt.SelectAll();
//        }

//        private void BtnClrSup_Click(object sender, EventArgs e) => ClearSupplier(true);
//        private void ClearSupplier(bool focus)
//        {
//            _selectedSupplierId = null;
//            pnlSupBadge.Visible = false;
//            dgvInvoices.Rows.Clear();
//            RefreshSummary();
//            if (focus) txtSupSearch.Focus();
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  LOAD PENDING / PARTIALLY PAID INVOICES FOR SUPPLIER
//        // ══════════════════════════════════════════════════════════════════

//        private void LoadPendingInvoices(int supplierId)
//        {
//            dgvInvoices.Rows.Clear();
//            try
//            {
//                var invoices = _db.Purchases
//                    .Where(p => p.SupplierId == supplierId
//                             && !p.IsDeleted
//                             && p.PaymentStatus != PurchasePaymentStatus.Paid)
//                    .OrderBy(p => p.PurchaseDate)  // oldest first
//                    .ToList();

//                foreach (var inv in invoices)
//                {
//                    int idx = dgvInvoices.Rows.Add();
//                    var row = dgvInvoices.Rows[idx];
//                    row.Tag = inv.Id;   // PurchaseId stored in Tag

//                    row.Cells["colInvNo"].Value = inv.InvoiceNumber;
//                    row.Cells["colInvDate"].Value = inv.PurchaseDate.ToString("dd/MM/yyyy");
//                    row.Cells["colNetAmt"].Value = inv.NetAmount;
//                    row.Cells["colPaid"].Value = inv.TotalPaid;
//                    row.Cells["colBalance"].Value = inv.Balance;
//                    row.Cells["colStatus"].Value = StatusLabel(inv.PaymentStatus);
//                    row.Cells["colAllocate"].Value = "0.00";
//                }

//                RefreshSummary();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error loading invoices: " + ex.Message);
//            }
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  TOTAL AMOUNT CHANGED → refresh remaining
//        // ══════════════════════════════════════════════════════════════════

//        private void TxtTotalAmt_TextChanged(object sender, EventArgs e)
//        {
//            if (_suppressAmtEvent) return;
//            RefreshSummary();
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  AUTO ALLOCATE  (oldest invoice first, fills balance)
//        // ══════════════════════════════════════════════════════════════════

//        private void BtnAutoAllocate_Click(object sender, EventArgs e)
//        {
//            decimal total = D(txtTotalAmt.Text);
//            if (total <= 0) { Error("Enter the total amount paid first."); txtTotalAmt.Focus(); return; }
//            if (dgvInvoices.Rows.Count == 0) { Error("No invoices to allocate."); return; }

//            decimal remaining = total;

//            _suppressGridEvent = true;
//            foreach (DataGridViewRow row in dgvInvoices.Rows)
//            {
//                if (remaining <= 0) { row.Cells["colAllocate"].Value = "0.00"; continue; }
//                decimal balance = D(row.Cells["colBalance"].Value?.ToString());
//                decimal allocate = Math.Min(remaining, balance);
//                row.Cells["colAllocate"].Value = allocate.ToString("N2");
//                remaining -= allocate;
//            }
//            _suppressGridEvent = false;

//            RefreshSummary();
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  GRID — manual allocate editing
//        // ══════════════════════════════════════════════════════════════════

//        private void DgvInvoices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex < 0 || _suppressGridEvent) return;
//            if (dgvInvoices.Columns[e.ColumnIndex].Name != "colAllocate") return;

//            var row = dgvInvoices.Rows[e.RowIndex];
//            decimal amt = D(row.Cells["colAllocate"].Value?.ToString());
//            decimal bal = D(row.Cells["colBalance"].Value?.ToString());

//            // Cannot allocate more than the invoice balance
//            if (amt > bal)
//            {
//                amt = bal;
//                row.Cells["colAllocate"].Value = amt.ToString("N2");
//                MessageBox.Show($"Allocation adjusted to invoice balance: Rs. {bal:N2}",
//                    "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }

//            RefreshSummary();
//        }

//        private void DgvInvoices_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
//        {
//            e.Control.KeyPress -= CellDec_KeyPress;
//            if (dgvInvoices.Columns[dgvInvoices.CurrentCell.ColumnIndex].Name == "colAllocate")
//                e.Control.KeyPress += CellDec_KeyPress;
//        }

//        private void CellDec_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            var tb = sender as TextBox;
//            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b') { e.Handled = true; return; }
//            if (e.KeyChar == '.' && tb?.Text.Contains('.') == true) e.Handled = true;
//        }

//        /// <summary>Colour the status cell based on value.</summary>
//        private void DgvInvoices_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
//        {
//            if (dgvInvoices.Columns[e.ColumnIndex].Name != "colStatus") return;
//            if (e.Value == null) return;

//            string status = e.Value.ToString();
//            if (status.Contains("Pending"))
//            {
//                e.CellStyle.BackColor = Color.FromArgb(255, 243, 224);
//                e.CellStyle.ForeColor = Color.FromArgb(245, 124, 0);
//            }
//            else if (status.Contains("Partial"))
//            {
//                e.CellStyle.BackColor = Color.FromArgb(225, 245, 254);
//                e.CellStyle.ForeColor = Color.FromArgb(2, 119, 189);
//            }
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  REFRESH SUMMARY
//        // ══════════════════════════════════════════════════════════════════

//        private void RefreshSummary()
//        {
//            decimal totalDue = 0;
//            decimal totalAlloc = 0;

//            foreach (DataGridViewRow row in dgvInvoices.Rows)
//            {
//                totalDue += D(row.Cells["colBalance"].Value?.ToString());
//                totalAlloc += D(row.Cells["colAllocate"].Value?.ToString());
//            }

//            decimal totalPaid = D(txtTotalAmt.Text);
//            decimal unallocated = totalPaid - totalAlloc;

//            lblTotalDueVal.Text = totalDue.ToString("N2");
//            lblTotalAllocVal.Text = totalAlloc.ToString("N2");
//            lblRemainingVal.Text = unallocated.ToString("N2");

//            // Colour feedback
//            if (unallocated < 0)
//            {
//                lblRemainingVal.ForeColor = Color.FromArgb(198, 40, 40);
//                lblRemainingCaption.Text = "Over-allocated (reduce allocations):";
//            }
//            else if (unallocated == 0 && totalPaid > 0)
//            {
//                lblRemainingVal.ForeColor = Color.FromArgb(46, 125, 50);
//                lblRemainingCaption.Text = "✔ Fully allocated — ready to save:";
//            }
//            else
//            {
//                lblRemainingVal.ForeColor = Color.FromArgb(198, 40, 40);
//                lblRemainingCaption.Text = "Unallocated (must be 0 to save):";
//            }
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  SAVE PAYMENT
//        // ══════════════════════════════════════════════════════════════════

//        private void BtnSave_Click(object sender, EventArgs e)
//        {
//            // ── Validation ─────────────────────────────────────────────────
//            if (!_selectedSupplierId.HasValue)
//            { Error("Please select a supplier."); txtSupSearch.Focus(); return; }

//            decimal totalPaid = D(txtTotalAmt.Text);
//            if (totalPaid <= 0)
//            { Error("Please enter the total amount paid."); txtTotalAmt.Focus(); return; }

//            decimal totalAlloc = 0;
//            foreach (DataGridViewRow row in dgvInvoices.Rows)
//                totalAlloc += D(row.Cells["colAllocate"].Value?.ToString());

//            if (totalAlloc != totalPaid)
//            {
//                Error($"Total Allocated (Rs. {totalAlloc:N2}) must equal Total Paid (Rs. {totalPaid:N2}).\n\nUse 'Auto Allocate' or adjust the Allocate column.");
//                return;
//            }

//            if (totalAlloc == 0)
//            { Error("No amounts allocated to any invoice."); return; }

//            // ── Build payment entity ────────────────────────────────────────
//            var payment = new SupplierPayment
//            {
//                PaymentNumber = lblPayNo.Text,
//                SupplierId = _selectedSupplierId.Value,
//                PaymentDate = dtpPayDate.Value,
//                TotalAmountPaid = totalPaid,
//                TotalAllocated = totalAlloc,
//                PaymentMethod = (PaymentMethod)cmbMethod.SelectedIndex,
//                TransactionReference = txtTxnRef.Text.Trim(),
//                Notes = txtNotes.Text.Trim(),
//                CreatedAt = DateTime.UtcNow
//            };

//            using (var txn = _db.Database.BeginTransaction())
//            {
//                try
//                {
//                    _db.SupplierPayments.Add(payment);
//                    _db.SaveChanges();  // get payment.Id

//                    int settledCount = 0;
//                    int partialCount = 0;

//                    foreach (DataGridViewRow row in dgvInvoices.Rows)
//                    {
//                        decimal alloc = D(row.Cells["colAllocate"].Value?.ToString());
//                        if (alloc <= 0) continue;

//                        int purchaseId = (int)row.Tag;

//                        // Add detail record
//                        var detail = new SupplierPaymentDetail
//                        {
//                            SupplierPaymentId = payment.Id,
//                            PurchaseId = purchaseId,
//                            AmountAllocated = alloc,
//                            CreatedAt = DateTime.UtcNow
//                        };
//                        _db.SupplierPaymentDetails.Add(detail);
//                        _db.SaveChanges();

//                        // Reload the Purchase with its PaymentDetails and recalculate
//                        var purchase = _db.Purchases
//                            .Include("PaymentDetails")
//                            .FirstOrDefault(p => p.Id == purchaseId);

//                        if (purchase != null)
//                        {
//                            purchase.RecalculateFromPayments();
//                            _db.SaveChanges();

//                            if (purchase.PaymentStatus == PurchasePaymentStatus.Paid) settledCount++;
//                            else if (purchase.PaymentStatus == PurchasePaymentStatus.PartiallyPaid) partialCount++;
//                        }
//                    }

//                    txn.Commit();

//                    MessageBox.Show(
//                        $"✔  Payment saved!\n\n" +
//                        $"Payment No     :  {payment.PaymentNumber}\n" +
//                        $"Supplier       :  {_selectedSupplierName}\n" +
//                        $"Amount Paid    :  Rs. {totalPaid:N2}\n\n" +
//                        $"Invoices fully settled  :  {settledCount}\n" +
//                        $"Invoices partially paid  :  {partialCount}",
//                        "Payment Saved",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);

//                    this.Close();
//                }
//                catch (Exception ex)
//                {
//                    txn.Rollback();
//                    MessageBox.Show("Save failed — rolled back.\n\n" + ex.Message,
//                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  SUPPLIER SUGGESTION HELPERS
//        // ══════════════════════════════════════════════════════════════════

//        private void ShowSugg(int count)
//        {
//            if (count == 0) { HideSugg(); return; }
//            Point pt = PointToClient(txtSupSearch.Parent.PointToScreen(txtSupSearch.Location));
//            lstSupSugg.Location = new Point(pt.X, pt.Y + txtSupSearch.Height);
//            lstSupSugg.Width = txtSupSearch.Width;
//            lstSupSugg.Height = Math.Min(count, 6) * lstSupSugg.ItemHeight + 2;
//            lstSupSugg.BringToFront();
//            lstSupSugg.Visible = true;
//        }

//        private void HideSugg()
//        {
//            lstSupSugg.Visible = false;
//            lstSupSugg.DataSource = null;
//        }

//        private void RepositionDropdown()
//        {
//            if (lstSupSugg.Visible) ShowSugg(lstSupSugg.Items.Count);
//        }

//        private void LstSupSugg_DrawItem(object sender, DrawItemEventArgs e)
//        {
//            if (e.Index < 0) return;
//            var item = lstSupSugg.Items[e.Index];
//            bool sel = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
//            e.Graphics.FillRectangle(
//                sel ? new SolidBrush(Color.FromArgb(232, 245, 233)) : Brushes.White, e.Bounds);
//            if (e.Index > 0)
//                e.Graphics.DrawLine(new Pen(Color.FromArgb(236, 239, 241)),
//                    e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);

//            var boldFont = new Font("Segoe UI", 10F, FontStyle.Bold);
//            var subFont = new Font("Segoe UI", 8.5F);
//            int pad = 10;

//            if (item is Models.Supplier sup)
//            {
//                e.Graphics.DrawString(sup.SupplierName, boldFont,
//                    new SolidBrush(Color.FromArgb(27, 94, 32)), e.Bounds.Left + pad, e.Bounds.Top + 4);
//                e.Graphics.DrawString($"{sup.ShopName}  ·  {sup.ContactNo}", subFont,
//                    new SolidBrush(Color.FromArgb(120, 144, 156)), e.Bounds.Left + pad, e.Bounds.Top + 22);
//            }
//            boldFont.Dispose();
//            subFont.Dispose();
//            e.DrawFocusRectangle();
//        }

//        // ══════════════════════════════════════════════════════════════════
//        //  HELPERS
//        // ══════════════════════════════════════════════════════════════════

//        private string GeneratePaymentNumber()
//        {
//            try
//            {
//                int last = _db.SupplierPayments.Any() ? _db.SupplierPayments.Max(p => p.Id) : 0;
//                return $"PAY-{(last + 1):D5}";
//            }
//            catch { return $"PAY-{DateTime.Now:yyyyMMddHHmm}"; }
//        }

//        private static string StatusLabel(PurchasePaymentStatus status)
//        {
//            switch (status)
//            {
//                case PurchasePaymentStatus.Pending:
//                    return "⏳ Pending";
//                case PurchasePaymentStatus.PartiallyPaid:
//                    return "🔵 Partial";
//                case PurchasePaymentStatus.Paid:
//                    return "✔ Paid";
//                default:
//                    return status.ToString();
//            }
//        }

//        private static decimal D(string s) => decimal.TryParse(s, out decimal v) ? v : 0m;
//        private static void Error(string msg) => MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        private static void HoverBtn(Button b, Color hover, Color normal)
//        {
//            b.MouseEnter += (s, e) => b.BackColor = hover;
//            b.MouseLeave += (s, e) => b.BackColor = normal;
//        }

//        private void DecimalOnly(object sender, KeyPressEventArgs e)
//        {
//            var tb = sender as TextBox;
//            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b') { e.Handled = true; return; }
//            if (e.KeyChar == '.' && tb?.Text.Contains('.') == true) e.Handled = true;
//        }

//        protected override void OnFormClosed(FormClosedEventArgs e)
//        {
//            base.OnFormClosed(e);
//            _supTimer.Dispose();
//            _db.Dispose();
//        }
//    }
//}


using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Supplier
{
    /// <summary>
    /// Supplier Payment Form
    /// ─────────────────────────────────────────────────────────────────────
    /// Use-case:
    ///   Supplier delivers on Mon, Wed, Fri (3 invoices saved via PurchaseForm).
    ///   On Friday the user pays Rs 50,000 covering all three.
    ///   This form lets the user enter 50,000, then either auto-allocate across
    ///   oldest invoices first, or type amounts manually in the green column.
    ///
    /// On Save:
    ///   • SupplierPayment header + SupplierPaymentDetail rows are inserted.
    ///   • Purchase.RecalculateFromPayments() is called for every touched invoice.
    ///   • Invoice PaymentStatus transitions automatically:
    ///       Pending → PartiallyPaid → Paid
    /// </summary>
    public partial class SupplierPaymentForm : Form
    {
        private readonly POSDbContext _db;

        private int? _selectedSupplierId;
        private string _selectedSupplierName;

        private readonly Timer _supTimer = new Timer { Interval = 300 };

        private bool _suppressSupEvent;
        private bool _suppressGridEvent;

        public SupplierPaymentForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            WireEvents();
            SetupForm();
        }

        // ══════════════════════════════════════════════════════════════════
        //  SETUP
        // ══════════════════════════════════════════════════════════════════

        private void SetupForm()
        {
            lblHeaderDate.Text = "Date: " + DateTime.Now.ToString("dd MMM yyyy");
            lblPayNo.Text = GeneratePaymentNumber();
            dtpPayDate.Value = DateTime.Now;
            cmbMethod.SelectedIndex = 0;
        }

        private void WireEvents()
        {
            // Designer wires basic events. Only lambdas / leave events go here.

            lstSupSugg.Leave += LstSupSugg_Leave;

            _supTimer.Tick += (s, e) => { _supTimer.Stop(); SearchSuppliers(txtSupSearch.Text.Trim()); };

            btnCancel.Click += (s, e) => this.Close();

            HoverBtn(btnAutoAllocate, Color.FromArgb(13, 71, 161), Color.FromArgb(21, 101, 192));
            HoverBtn(btnSave, Color.FromArgb(27, 94, 32), Color.FromArgb(46, 125, 50));

            this.Resize += (s, e) => RepositionDropdown();

            // Invoice-number click → detail popup + cursor/link effects
            dgvInvoices.CellClick += DgvInvoices_CellClick;
            dgvInvoices.CellFormatting += DgvInvoices_CellFormatting;
            dgvInvoices.CellMouseEnter += DgvInvoices_CellMouseEnter;
            dgvInvoices.CellMouseLeave += DgvInvoices_CellMouseLeave;
        }

        // ══════════════════════════════════════════════════════════════════
        //  SUPPLIER SEARCH
        // ══════════════════════════════════════════════════════════════════

        private void TxtSupSearch_TextChanged(object sender, EventArgs e)
        {
            if (_suppressSupEvent) return;
            if (_selectedSupplierId.HasValue) ClearSupplier(false);
            _supTimer.Stop();
            _supTimer.Start();
        }

        private void SearchSuppliers(string q)
        {
            if (q.Length < 1) { HideSugg(); return; }
            try
            {
                var list = _db.Suppliers
                    .Where(s => !s.IsDeleted &&
                               (s.SupplierName.Contains(q) ||
                                s.ShopName.Contains(q) ||
                                s.ContactNo.Contains(q)))
                    .OrderBy(s => s.SupplierName)
                    .Take(8).ToList();

                lstSupSugg.DataSource = list.Count > 0 ? (object)list : null;
                lstSupSugg.DisplayMember = "SupplierName";
                lstSupSugg.ValueMember = "Id";
                ShowSugg(list.Count);
            }
            catch (Exception ex) { MessageBox.Show("Search error: " + ex.Message); }
        }

        private void TxtSupSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (!lstSupSugg.Visible) return;
            if (e.KeyCode == Keys.Down)
            {
                lstSupSugg.Focus();
                if (lstSupSugg.Items.Count > 0) lstSupSugg.SelectedIndex = 0;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape) { HideSugg(); e.Handled = true; }
        }

        private void LstSupSugg_MouseClick(object sender, MouseEventArgs e) => SelectSupplier();
        private void LstSupSugg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { SelectSupplier(); e.Handled = true; }
            else if (e.KeyCode == Keys.Escape) { HideSugg(); txtSupSearch.Focus(); e.Handled = true; }
        }

        private void TxtSupSearch_Leave(object sender, EventArgs e) { if (!lstSupSugg.Focused) HideSugg(); }
        private void LstSupSugg_Leave(object sender, EventArgs e) { if (!txtSupSearch.Focused) HideSugg(); }

        private void SelectSupplier()
        {
            if (!(lstSupSugg.SelectedItem is Models.Supplier s)) return;

            _selectedSupplierId = s.Id;
            _selectedSupplierName = $"{s.SupplierName}  —  {s.ShopName}";

            _suppressSupEvent = true;
            txtSupSearch.Text = string.Empty;
            _suppressSupEvent = false;

            lblSelSup.Text = _selectedSupplierName;
            pnlSupBadge.Visible = true;
            HideSugg();

            LoadPendingInvoices(s.Id);

            txtTotalAmt.Focus();
            txtTotalAmt.SelectAll();
        }

        private void BtnClrSup_Click(object sender, EventArgs e) => ClearSupplier(true);

        private void ClearSupplier(bool focus)
        {
            _selectedSupplierId = null;
            _selectedSupplierName = null;
            pnlSupBadge.Visible = false;
            dgvInvoices.Rows.Clear();
            RefreshSummary();
            if (focus) txtSupSearch.Focus();
        }

        // ══════════════════════════════════════════════════════════════════
        //  LOAD INVOICES
        // ══════════════════════════════════════════════════════════════════

        private void LoadPendingInvoices(int supplierId)
        {
            dgvInvoices.Rows.Clear();
            try
            {
                var invoices = _db.Purchases
                    .Where(p => p.SupplierId == supplierId
                             && !p.IsDeleted
                             && p.PaymentStatus != PurchasePaymentStatus.Paid)
                    .OrderBy(p => p.PurchaseDate)   // oldest first
                    .ToList();

                foreach (var inv in invoices)
                {
                    int idx = dgvInvoices.Rows.Add();
                    var row = dgvInvoices.Rows[idx];
                    row.Tag = inv.Id;   // PurchaseId stored for Save

                    row.Cells["colInvNo"].Value = inv.InvoiceNumber;
                    row.Cells["colInvDate"].Value = inv.PurchaseDate.ToString("dd/MM/yyyy");
                    row.Cells["colNetAmt"].Value = inv.NetAmount;
                    row.Cells["colPaid"].Value = inv.TotalPaid;
                    row.Cells["colBalance"].Value = inv.Balance;
                    row.Cells["colStatus"].Value = StatusLabel(inv.PaymentStatus);
                    row.Cells["colAllocate"].Value = "0.00";
                }

                lblGridTitle.Text = invoices.Count > 0
                    ? $"  Unpaid Invoices for {_selectedSupplierName}  —  enter amount to allocate per invoice"
                    : $"  No pending invoices found for {_selectedSupplierName}";

                RefreshSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoices: " + ex.Message);
            }
        }

        // ══════════════════════════════════════════════════════════════════
        //  TOTAL AMOUNT CHANGED
        // ══════════════════════════════════════════════════════════════════

        private void TxtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            RefreshSummary();
        }

        // ══════════════════════════════════════════════════════════════════
        //  AUTO ALLOCATE  (oldest invoice first)
        // ══════════════════════════════════════════════════════════════════

        private void BtnAutoAllocate_Click(object sender, EventArgs e)
        {
            decimal total = D(txtTotalAmt.Text);
            if (total <= 0) { Error("Enter the total amount paid first."); txtTotalAmt.Focus(); return; }
            if (dgvInvoices.Rows.Count == 0) { Error("No invoices loaded."); return; }

            decimal remaining = total;

            _suppressGridEvent = true;
            foreach (DataGridViewRow row in dgvInvoices.Rows)
            {
                if (remaining <= 0) { row.Cells["colAllocate"].Value = "0.00"; continue; }
                decimal balance = D(row.Cells["colBalance"].Value?.ToString());
                decimal allocate = Math.Min(remaining, balance);
                row.Cells["colAllocate"].Value = allocate.ToString("N2");
                remaining -= allocate;
            }
            _suppressGridEvent = false;

            RefreshSummary();
        }

        // ══════════════════════════════════════════════════════════════════
        //  GRID — invoice-number click → detail popup
        // ══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Clicking any cell in the Invoice No column opens the read-only
        /// PurchaseDetailForm for that invoice.
        /// </summary>
        private void DgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvInvoices.Columns[e.ColumnIndex].Name != "colInvNo") return;

            // purchaseId is stored in row.Tag by LoadPendingInvoices()
            if (!(dgvInvoices.Rows[e.RowIndex].Tag is int purchaseId)) return;

            using (var detail = new PurchaseDetailForm(purchaseId))
                detail.ShowDialog(this);
        }

        /// <summary>
        /// Change cursor and paint Invoice No cells as hyperlinks so the user
        /// knows they are clickable.
        /// </summary>
        private void DgvInvoices_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string col = dgvInvoices.Columns[e.ColumnIndex].Name;

            if (col == "colInvNo")
            {
                // Blue underline = clickable link
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);
                e.CellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F,
                                            System.Drawing.FontStyle.Underline);
                e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(13, 71, 161);
                return;
            }

            // Status column colour coding
            if (col == "colStatus" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status.Contains("Pending"))
                {
                    e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(245, 124, 0);
                }
                else if (status.Contains("Partial"))
                {
                    e.CellStyle.BackColor = System.Drawing.Color.FromArgb(225, 245, 254);
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(2, 119, 189);
                }
            }
        }

        /// <summary>Switch cursor to hand over the Invoice No column.</summary>
        private void DgvInvoices_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            dgvInvoices.Cursor = dgvInvoices.Columns[e.ColumnIndex].Name == "colInvNo"
                ? System.Windows.Forms.Cursors.Hand
                : System.Windows.Forms.Cursors.Default;
        }

        private void DgvInvoices_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvInvoices.Cursor = System.Windows.Forms.Cursors.Default;
        }

        // ══════════════════════════════════════════════════════════════════
        //  GRID — manual allocation editing
        // ══════════════════════════════════════════════════════════════════

        private void DgvInvoices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _suppressGridEvent) return;
            if (dgvInvoices.Columns[e.ColumnIndex].Name != "colAllocate") return;

            var row = dgvInvoices.Rows[e.RowIndex];
            decimal amt = D(row.Cells["colAllocate"].Value?.ToString());
            decimal bal = D(row.Cells["colBalance"].Value?.ToString());

            if (amt > bal)
            {
                amt = bal;
                row.Cells["colAllocate"].Value = amt.ToString("N2");
                MessageBox.Show(
                    $"Allocation capped to invoice balance: Rs. {bal:N2}",
                    "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            RefreshSummary();
        }

        private void DgvInvoices_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= CellDec_KeyPress;
            if (dgvInvoices.Columns[dgvInvoices.CurrentCell.ColumnIndex].Name == "colAllocate")
                e.Control.KeyPress += CellDec_KeyPress;
        }

        private void CellDec_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b') { e.Handled = true; return; }
            if (e.KeyChar == '.' && tb?.Text.Contains('.') == true) e.Handled = true;
        }



        // ══════════════════════════════════════════════════════════════════
        //  REFRESH SUMMARY PANEL
        // ══════════════════════════════════════════════════════════════════

        private void RefreshSummary()
        {
            decimal totalDue = 0;
            decimal totalAlloc = 0;

            foreach (DataGridViewRow row in dgvInvoices.Rows)
            {
                totalDue += D(row.Cells["colBalance"].Value?.ToString());
                totalAlloc += D(row.Cells["colAllocate"].Value?.ToString());
            }

            decimal totalPaid = D(txtTotalAmt.Text);
            decimal unallocated = totalPaid - totalAlloc;

            lblTotalDueVal.Text = totalDue.ToString("N2");
            lblTotalAllocVal.Text = totalAlloc.ToString("N2");
            lblRemainingVal.Text = unallocated.ToString("N2");

            if (unallocated < 0)
            {
                lblRemainingVal.ForeColor = Color.FromArgb(198, 40, 40);
                lblRemainingCaption.Text = "Over-allocated — reduce some allocations:";
            }
            else if (unallocated == 0 && totalPaid > 0)
            {
                lblRemainingVal.ForeColor = Color.FromArgb(46, 125, 50);
                lblRemainingCaption.Text = "✔ Fully allocated — ready to save:";
            }
            else
            {
                lblRemainingVal.ForeColor = Color.FromArgb(198, 40, 40);
                lblRemainingCaption.Text = "Unallocated (must reach 0 to save):";
            }
        }

        // ══════════════════════════════════════════════════════════════════
        //  SAVE PAYMENT
        // ══════════════════════════════════════════════════════════════════

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // ── Validation ─────────────────────────────────────────────────
            if (!_selectedSupplierId.HasValue)
            { Error("Please select a supplier."); txtSupSearch.Focus(); return; }

            decimal totalPaid = D(txtTotalAmt.Text);
            if (totalPaid <= 0)
            { Error("Please enter the total amount paid."); txtTotalAmt.Focus(); return; }

            decimal totalAlloc = 0;
            foreach (DataGridViewRow row in dgvInvoices.Rows)
                totalAlloc += D(row.Cells["colAllocate"].Value?.ToString());

            if (totalAlloc == 0)
            { Error("No amount has been allocated to any invoice.\nUse 'Auto Allocate' or enter amounts manually."); return; }

            if (totalAlloc != totalPaid)
            {
                Error(
                    $"Allocated (Rs. {totalAlloc:N2}) must equal Amount Paid (Rs. {totalPaid:N2}).\n\n" +
                    "Adjust the Allocate column or use 'Auto Allocate'.");
                return;
            }

            // ── Build payment record ────────────────────────────────────────
            var payment = new SupplierPayment
            {
                PaymentNumber = lblPayNo.Text,
                SupplierId = _selectedSupplierId.Value,
                PaymentDate = dtpPayDate.Value,
                TotalAmountPaid = totalPaid,
                TotalAllocated = totalAlloc,
                PaymentMethod = (PaymentMethod)cmbMethod.SelectedIndex,
                TransactionReference = txtTxnRef.Text.Trim(),
                Notes = txtNotes.Text.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            using (var txn = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.SupplierPayments.Add(payment);
                    _db.SaveChanges();  // get payment.Id

                    int settledCount = 0;
                    int partialCount = 0;

                    foreach (DataGridViewRow row in dgvInvoices.Rows)
                    {
                        decimal alloc = D(row.Cells["colAllocate"].Value?.ToString());
                        if (alloc <= 0) continue;

                        int purchaseId = (int)row.Tag;

                        _db.SupplierPaymentDetails.Add(new SupplierPaymentDetail
                        {
                            SupplierPaymentId = payment.Id,
                            PurchaseId = purchaseId,
                            AmountAllocated = alloc,
                            CreatedAt = DateTime.UtcNow
                        });
                        _db.SaveChanges();

                        // Reload with all payment details and recalculate status
                        var purchase = _db.Purchases
                            .Include("PaymentDetails")
                            .FirstOrDefault(p => p.Id == purchaseId);

                        if (purchase != null)
                        {
                            purchase.RecalculateFromPayments();
                            _db.SaveChanges();

                            if (purchase.PaymentStatus == PurchasePaymentStatus.Paid)
                                settledCount++;
                            else if (purchase.PaymentStatus == PurchasePaymentStatus.PartiallyPaid)
                                partialCount++;
                        }
                    }

                    txn.Commit();

                    MessageBox.Show(
                        $"✔  Payment saved!\n\n" +
                        $"Payment No        :  {payment.PaymentNumber}\n" +
                        $"Supplier          :  {_selectedSupplierName}\n" +
                        $"Amount Paid       :  Rs. {totalPaid:N2}\n\n" +
                        $"Invoices settled  :  {settledCount}\n" +
                        $"Partially paid    :  {partialCount}",
                        "Payment Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                catch (Exception ex)
                {
                    txn.Rollback();
                    MessageBox.Show(
                        "Save failed — all changes rolled back.\n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ══════════════════════════════════════════════════════════════════
        //  SUGGESTION LIST HELPERS
        // ══════════════════════════════════════════════════════════════════

        private void ShowSugg(int count)
        {
            if (count == 0) { HideSugg(); return; }
            System.Drawing.Point pt = PointToClient(txtSupSearch.Parent.PointToScreen(txtSupSearch.Location));
            lstSupSugg.Location = new System.Drawing.Point(pt.X, pt.Y + txtSupSearch.Height);
            lstSupSugg.Width = txtSupSearch.Width;
            lstSupSugg.Height = Math.Min(count, 6) * lstSupSugg.ItemHeight + 2;
            lstSupSugg.BringToFront();
            lstSupSugg.Visible = true;
        }

        private void HideSugg()
        {
            lstSupSugg.Visible = false;
            lstSupSugg.DataSource = null;
        }

        private void RepositionDropdown()
        {
            if (lstSupSugg.Visible) ShowSugg(lstSupSugg.Items.Count);
        }

        private void LstSupSugg_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var item = lstSupSugg.Items[e.Index];
            bool sel = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            e.Graphics.FillRectangle(
                sel ? new SolidBrush(Color.FromArgb(232, 245, 233)) : Brushes.White, e.Bounds);

            if (e.Index > 0)
                e.Graphics.DrawLine(new Pen(Color.FromArgb(236, 239, 241)),
                    e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);

            var boldFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            var subFont = new Font("Segoe UI", 8.5F);
            int pad = 10;

            if (item is Models.Supplier sup)
            {
                e.Graphics.DrawString(sup.SupplierName, boldFont,
                    new SolidBrush(Color.FromArgb(27, 94, 32)),
                    e.Bounds.Left + pad, e.Bounds.Top + 4);
                e.Graphics.DrawString($"{sup.ShopName}  ·  {sup.ContactNo}", subFont,
                    new SolidBrush(Color.FromArgb(120, 144, 156)),
                    e.Bounds.Left + pad, e.Bounds.Top + 22);
            }

            boldFont.Dispose();
            subFont.Dispose();
            e.DrawFocusRectangle();
        }

        // ══════════════════════════════════════════════════════════════════
        //  HELPERS
        // ══════════════════════════════════════════════════════════════════

        private string GeneratePaymentNumber()
        {
            try
            {
                int last = _db.SupplierPayments.Any() ? _db.SupplierPayments.Max(p => p.Id) : 0;
                return $"PAY-{(last + 1):D5}";
            }
            catch { return $"PAY-{DateTime.Now:yyyyMMddHHmm}"; }
        }

        private static string StatusLabel(PurchasePaymentStatus status)
        {
            switch (status)
            {
                case PurchasePaymentStatus.Pending: return "⏳ Pending";
                case PurchasePaymentStatus.PartiallyPaid: return "🔵 Partial";
                case PurchasePaymentStatus.Paid: return "✔ Paid";
                default: return status.ToString();
            }
        }

        private static decimal D(string s) => decimal.TryParse(s, out decimal v) ? v : 0m;

        private static void Error(string msg)
            => MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private static void HoverBtn(Button b, Color hover, Color normal)
        {
            b.MouseEnter += (s, e) => b.BackColor = hover;
            b.MouseLeave += (s, e) => b.BackColor = normal;
        }

        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b') { e.Handled = true; return; }
            if (e.KeyChar == '.' && tb?.Text.Contains('.') == true) e.Handled = true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _supTimer.Dispose();
            _db.Dispose();
        }
    }
}
