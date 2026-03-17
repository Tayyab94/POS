
using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Supplier
{
    /// <summary>
    /// All Purchase History
    /// ─────────────────────────────────────────────────────────────────────
    /// Shows ALL Purchase records regardless of payment status.
    ///
    /// Filters  : Invoice No (contains) · Supplier · Status · Date range
    /// Columns  : Invoice No (→ PurchaseDetailForm) · Supplier · Date ·
    ///            Items · Total · Discount · Net · Paid · Balance · Status ·
    ///            📋 Flow/Detail button (→ InvoicePaymentFlowForm)
    ///
    /// After closing InvoicePaymentFlowForm the row is refreshed live so
    /// Status / Balance / button colour immediately reflect any payments made.
    ///
    /// Cursor-based pagination — O(1) per page at any depth.
    /// N+1 fix — item counts in ONE batch query.
    /// </summary>
    public partial class AllPurchasesForm : Form
    {
        // ── State ─────────────────────────────────────────────────────────────
        private int? _selectedSupplierId;
        private string _selectedSupplierName;

        private int _pageSize = 20;
        private int? _nextCursor = null;
        private int? _currentFrom = null;
        private int _pageNumber = 1;
        private int _totalFound = 0;
        private bool _hasSearched = false;

        private readonly Stack<int?> _prevCursors = new Stack<int?>();
        private readonly Timer _supTimer = new Timer { Interval = 300 };
        private bool _suppressSupEvent;

        // ── Constructor ───────────────────────────────────────────────────────
        public AllPurchasesForm()
        {
            InitializeComponent();
            dtpFrom.Value = DateTime.Today.AddMonths(-3);
            dtpTo.Value = DateTime.Today;
            WireEvents();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  WIRE EVENTS
        // ══════════════════════════════════════════════════════════════════════
        private void WireEvents()
        {
            _supTimer.Tick += (s, e) => { _supTimer.Stop(); SearchSuppliers(txtSupSearch.Text.Trim()); };
            lstSupSugg.Leave += LstSupSugg_Leave;
            this.Resize += (s, e) => RepositionDropdown();
            KeyPreview = true;
            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };

            HoverBtn(btnSearch, Color.FromArgb(13, 71, 161), Color.FromArgb(21, 101, 192));
            HoverBtn(btnPrev, Color.FromArgb(13, 71, 161), Color.FromArgb(21, 101, 192));
            HoverBtn(btnNext, Color.FromArgb(13, 71, 161), Color.FromArgb(21, 101, 192));
            HoverBtn(btnClose, Color.FromArgb(207, 216, 220), Color.FromArgb(236, 239, 241));
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SUPPLIER SEARCH
        // ══════════════════════════════════════════════════════════════════════
        private void TxtSupSearch_TextChanged(object sender, EventArgs e)
        {
            if (_suppressSupEvent) return;
            if (_selectedSupplierId.HasValue) ClearSupplier(false);
            _supTimer.Stop(); _supTimer.Start();
        }

        private void SearchSuppliers(string q)
        {
            if (q.Length < 1) { HideSugg(); return; }
            try
            {
                using (var db = new POSDbContext())
                {
                    var list = db.Suppliers
                        .Where(s => !s.IsDeleted &&
                                   (s.SupplierName.Contains(q) ||
                                    s.ShopName.Contains(q) ||
                                    s.ContactNo.Contains(q)))
                        .OrderBy(s => s.SupplierName).Take(8).ToList();

                    lstSupSugg.DataSource = list.Count > 0 ? (object)list : null;
                    lstSupSugg.DisplayMember = "SupplierName";
                    lstSupSugg.ValueMember = "Id";
                    ShowSugg(list.Count);
                }
            }
            catch (Exception ex) { MessageBox.Show("Supplier search error: " + ex.Message); }
        }

        private void TxtSupSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (!lstSupSugg.Visible) return;
            if (e.KeyCode == Keys.Down)
            { lstSupSugg.Focus(); if (lstSupSugg.Items.Count > 0) lstSupSugg.SelectedIndex = 0; e.Handled = true; }
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
        }

        private void BtnClrSup_Click(object sender, EventArgs e) => ClearSupplier(true);
        private void ClearSupplier(bool focusSearch)
        {
            _selectedSupplierId = null;
            _selectedSupplierName = null;
            pnlSupBadge.Visible = false;
            if (focusSearch) txtSupSearch.Focus();
        }

        private void ShowSugg(int count)
        {
            if (count == 0) { HideSugg(); return; }
            var pt = PointToClient(txtSupSearch.Parent.PointToScreen(txtSupSearch.Location));
            lstSupSugg.Location = new Point(pt.X, pt.Y + txtSupSearch.Height);
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
        { if (lstSupSugg.Visible) ShowSugg(lstSupSugg.Items.Count); }

        private void LstSupSugg_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var item = lstSupSugg.Items[e.Index];
            bool sel = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            e.Graphics.FillRectangle(
                sel ? new SolidBrush(Color.FromArgb(227, 242, 253)) : Brushes.White, e.Bounds);
            if (e.Index > 0)
                e.Graphics.DrawLine(new Pen(Color.FromArgb(236, 239, 241)),
                    e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);

            using (var bf = new Font("Segoe UI", 10F, FontStyle.Bold))
            using (var sf = new Font("Segoe UI", 8.5F))
            {
                if (item is Models.Supplier sup)
                {
                    e.Graphics.DrawString(sup.SupplierName, bf,
                        new SolidBrush(Color.FromArgb(21, 101, 192)), e.Bounds.Left + 10, e.Bounds.Top + 4);
                    e.Graphics.DrawString($"{sup.ShopName}  ·  {sup.ContactNo}", sf,
                        new SolidBrush(Color.FromArgb(120, 144, 156)), e.Bounds.Left + 10, e.Bounds.Top + 22);
                }
            }
            e.DrawFocusRectangle();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SEARCH — entry point
        // ══════════════════════════════════════════════════════════════════════
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            _prevCursors.Clear();
            _currentFrom = null;
            _pageNumber = 1;
            _hasSearched = true;
            LoadPage(fromCursor: null);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  CURSOR PAGINATION
        // ══════════════════════════════════════════════════════════════════════
        private void LoadPage(int? fromCursor)
        {
            _currentFrom = fromCursor;
            int take = _pageSize + 1;

            try
            {
                DateTime dateFrom = dtpFrom.Value.Date;
                DateTime dateTo = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
                string invSearch = txtInvSearch.Text.Trim();
                int statusFilter = cmbStatus.SelectedIndex; // 0=All 1=Pending 2=Partial 3=Paid

                using (var db = new POSDbContext())
                {
                    var query = db.Purchases
                        .Include("Supplier").AsNoTracking()
                        .Where(p => !p.IsDeleted
                                 && p.PurchaseDate >= dateFrom
                                 && p.PurchaseDate <= dateTo);

                    if (!string.IsNullOrWhiteSpace(invSearch))
                        query = query.Where(p => p.InvoiceNumber.Contains(invSearch));

                    if (_selectedSupplierId.HasValue)
                        query = query.Where(p => p.SupplierId == _selectedSupplierId.Value);

                    if (statusFilter == 1) query = query.Where(p => p.PaymentStatus == PurchasePaymentStatus.Pending);
                    else if (statusFilter == 2) query = query.Where(p => p.PaymentStatus == PurchasePaymentStatus.PartiallyPaid);
                    else if (statusFilter == 3) query = query.Where(p => p.PaymentStatus == PurchasePaymentStatus.Paid);

                    if (fromCursor == null) _totalFound = query.Count();

                    var paged = query.OrderByDescending(p => p.PurchaseDate).ThenByDescending(p => p.Id);

                    if (fromCursor.HasValue)
                        paged = paged.Where(p => p.Id <= fromCursor.Value)
                                     .OrderByDescending(p => p.PurchaseDate).ThenByDescending(p => p.Id);

                    var rows = paged.Take(take).ToList();

                    bool hasNext = rows.Count > _pageSize;
                    if (hasNext) { _nextCursor = rows[_pageSize].Id; rows = rows.Take(_pageSize).ToList(); }
                    else _nextCursor = null;

                    // Batch item counts — 1 query for all rows
                    var ids = rows.Select(p => p.Id).ToList();
                    var itemCounts = db.PurchaseItems
                        .Where(i => ids.Contains(i.PurchaseId) && !i.IsDeleted)
                        .GroupBy(i => i.PurchaseId)
                        .ToDictionary(g => g.Key, g => g.Count());

                    PopulateGrid(rows, itemCounts);
                    UpdatePagerUI(hasNext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  POPULATE GRID
        // ══════════════════════════════════════════════════════════════════════
        private void PopulateGrid(List<Purchase> rows, Dictionary<int, int> itemCounts)
        {
            dgvPurchases.Rows.Clear();

            foreach (var p in rows)
            {
                string supplierName = p.Supplier != null
                    ? $"{p.Supplier.SupplierName}  —  {p.Supplier.ShopName}"
                    : $"Supplier #{p.SupplierId}";

                int itemCount = itemCounts.ContainsKey(p.Id) ? itemCounts[p.Id] : 0;
                decimal balance = p.NetAmount - p.TotalPaid;

                string statusText = p.PaymentStatus == PurchasePaymentStatus.Paid ? "✔ Paid"
                                  : p.PaymentStatus == PurchasePaymentStatus.PartiallyPaid ? "◑ Partial"
                                  : "○ Pending";

                int idx = dgvPurchases.Rows.Add();
                var row = dgvPurchases.Rows[idx];
                row.Tag = p.Id;   // used by CellClick for both Invoice and Flow

                row.Cells["colAPInvNo"].Value = p.InvoiceNumber;
                row.Cells["colAPSupplier"].Value = supplierName;
                row.Cells["colAPDate"].Value = p.PurchaseDate.ToString("dd MMM yyyy");
                row.Cells["colAPItems"].Value = itemCount;
                row.Cells["colAPTotalBill"].Value = p.TotalAmount;
                row.Cells["colAPDiscount"].Value = p.Discount;
                row.Cells["colAPNetAmt"].Value = p.NetAmount;
                row.Cells["colAPPaid"].Value = p.TotalPaid;
                row.Cells["colAPBalance"].Value = balance;
                row.Cells["colAPStatus"].Value = statusText;

                // Flow button: colour + label by payment status
                StyleFlowCell((DataGridViewButtonCell)row.Cells["colAPFlow"], p.PaymentStatus);
            }

            lblGridTitle.Text = rows.Count == 0
                ? "  No invoices found matching your criteria."
                : $"  All Purchases  —  {_totalFound:N0} total  (showing page {_pageNumber})";
        }

        /// <summary>Sets Flow button text + colour based on payment status.</summary>
        private static void StyleFlowCell(DataGridViewButtonCell cell, PurchasePaymentStatus status)
        {
            switch (status)
            {
                case PurchasePaymentStatus.Pending:
                    cell.Value = "📋 Flow";
                    cell.Style.BackColor = Color.FromArgb(198, 40, 40);   // red
                    cell.Style.ForeColor = Color.White;
                    break;
                case PurchasePaymentStatus.PartiallyPaid:
                    cell.Value = "📋 Flow";
                    cell.Style.BackColor = Color.FromArgb(230, 81, 0);    // amber
                    cell.Style.ForeColor = Color.White;
                    break;
                default:  // Paid
                    cell.Value = "📋 Detail";
                    cell.Style.BackColor = Color.FromArgb(55, 71, 79);    // dark grey
                    cell.Style.ForeColor = Color.White;
                    break;
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  PAGINATION
        // ══════════════════════════════════════════════════════════════════════
        private void UpdatePagerUI(bool hasNext)
        {
            btnNext.Enabled = hasNext;
            btnPrev.Enabled = _prevCursors.Count > 0;
            lblPageInfo.Text = _totalFound > 0
                ? $"Page {_pageNumber}  •  {dgvPurchases.Rows.Count} of {_totalFound:N0} records"
                : $"Page {_pageNumber}";
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (!_nextCursor.HasValue) return;
            _prevCursors.Push(_currentFrom);
            _pageNumber++;
            LoadPage(fromCursor: _nextCursor);
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (_prevCursors.Count == 0) return;
            _pageNumber--;
            LoadPage(fromCursor: _prevCursors.Pop());
        }

        private void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_hasSearched) return;
            if (!int.TryParse(cmbPageSize.SelectedItem?.ToString(), out int sz)) return;
            _pageSize = sz;
            _prevCursors.Clear();
            _pageNumber = 1;
            _currentFrom = null;
            LoadPage(fromCursor: null);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  GRID EVENTS
        // ══════════════════════════════════════════════════════════════════════
        private void DgvPurchases_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!(dgvPurchases.Rows[e.RowIndex].Tag is int purchaseId)) return;

            string col = dgvPurchases.Columns[e.ColumnIndex].Name;

            // Invoice No → PurchaseDetailForm (read-only view)
            if (col == "colAPInvNo")
            {
                using (var detail = new PurchaseDetailForm(purchaseId))
                    detail.ShowDialog(this);
                return;
            }

            // 📋 Flow / Detail → InvoicePaymentFlowForm
            if (col == "colAPFlow")
            {
                using (var flow = new InvoicePaymentFlowForm(purchaseId))
                    flow.ShowDialog(this);

                // Live-refresh just this row so Status/Balance/button colour update instantly
                RefreshRow(e.RowIndex, purchaseId);
            }
        }

        /// <summary>
        /// Re-queries one purchase and refreshes its grid row in-place.
        /// Called after InvoicePaymentFlowForm closes so the list stays current
        /// without reloading the whole page.
        /// </summary>
        private void RefreshRow(int rowIndex, int purchaseId)
        {
            try
            {
                using (var db = new POSDbContext())
                {
                    var p = db.Purchases.Include("Supplier").AsNoTracking()
                                        .FirstOrDefault(x => x.Id == purchaseId);
                    if (p == null) return;

                    string supplierName = p.Supplier != null
                        ? $"{p.Supplier.SupplierName}  —  {p.Supplier.ShopName}"
                        : $"Supplier #{p.SupplierId}";

                    int itemCount = db.PurchaseItems.Count(i => i.PurchaseId == purchaseId && !i.IsDeleted);
                    decimal balance = p.NetAmount - p.TotalPaid;

                    string statusText = p.PaymentStatus == PurchasePaymentStatus.Paid ? "✔ Paid"
                                      : p.PaymentStatus == PurchasePaymentStatus.PartiallyPaid ? "◑ Partial"
                                      : "○ Pending";

                    var row = dgvPurchases.Rows[rowIndex];
                    row.Cells["colAPSupplier"].Value = supplierName;
                    row.Cells["colAPItems"].Value = itemCount;
                    row.Cells["colAPTotalBill"].Value = p.TotalAmount;
                    row.Cells["colAPDiscount"].Value = p.Discount;
                    row.Cells["colAPNetAmt"].Value = p.NetAmount;
                    row.Cells["colAPPaid"].Value = p.TotalPaid;
                    row.Cells["colAPBalance"].Value = balance;
                    row.Cells["colAPStatus"].Value = statusText;
                    StyleFlowCell((DataGridViewButtonCell)row.Cells["colAPFlow"], p.PaymentStatus);
                }
            }
            catch { /* non-critical — full refresh on next Search press */ }
        }

        private void DgvPurchases_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string col = dgvPurchases.Columns[e.ColumnIndex].Name;

            if (col == "colAPInvNo")
            {
                e.CellStyle.ForeColor = Color.FromArgb(21, 101, 192);
                e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Underline);
                e.CellStyle.SelectionForeColor = Color.FromArgb(13, 71, 161);
                return;
            }

            if (col == "colAPStatus" && e.Value != null)
            {
                string v = e.Value.ToString();
                if (v.StartsWith("✔"))
                { e.CellStyle.ForeColor = Color.FromArgb(46, 125, 50); e.CellStyle.BackColor = Color.FromArgb(232, 245, 233); e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); e.CellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201); }
                else if (v.StartsWith("◑"))
                { e.CellStyle.ForeColor = Color.FromArgb(230, 81, 0); e.CellStyle.BackColor = Color.FromArgb(255, 243, 224); e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); e.CellStyle.SelectionBackColor = Color.FromArgb(255, 224, 178); }
                else
                { e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40); e.CellStyle.BackColor = Color.FromArgb(255, 235, 238); e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); e.CellStyle.SelectionBackColor = Color.FromArgb(255, 205, 210); }
                return;
            }

            if (col == "colAPBalance" && e.Value is decimal bal && bal > 0)
            { e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40); e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); return; }

            if (col == "colAPDiscount" && e.Value is decimal disc && disc > 0)
                e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40);
        }

        private void DgvPurchases_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string col = dgvPurchases.Columns[e.ColumnIndex].Name;
            dgvPurchases.Cursor = (col == "colAPInvNo" || col == "colAPFlow")
                ? Cursors.Hand : Cursors.Default;
        }

        private void DgvPurchases_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
            => dgvPurchases.Cursor = Cursors.Default;

        // ══════════════════════════════════════════════════════════════════════
        //  CLOSE
        // ══════════════════════════════════════════════════════════════════════
        private void BtnClose_Click(object sender, EventArgs e) => Close();

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _supTimer.Dispose();
        }

        private static void HoverBtn(Button b, Color hover, Color normal)
        {
            b.MouseEnter += (s, e) => b.BackColor = hover;
            b.MouseLeave += (s, e) => b.BackColor = normal;
        }
    }
}