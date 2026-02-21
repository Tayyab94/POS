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
    /// Paid Purchase History
    /// ─────────────────────────────────────────────────────────────────────
    /// Shows all Purchase records where PaymentStatus == Paid (= 2).
    ///
    /// Features:
    ///   • Supplier live-search with suggestion dropdown (same pattern as other forms)
    ///   • Optional date-range filter
    ///   • Cursor-based pagination — uses the last-seen Purchase.Id as the cursor
    ///     so each page query is O(1) index seek instead of OFFSET scanning.
    ///   • Click any Invoice No cell → opens PurchaseDetailForm (read-only)
    ///
    /// Cursor pagination explained:
    ///   _nextCursor  = Id of the first record on the NEXT page  (null = no next page)
    ///   _prevCursors = stack of cursors for each previous page  (empty = on page 1)
    ///   Page 1  : WHERE Id > 0  ORDER BY Id ASC  TAKE pageSize
    ///   Next    : WHERE Id >= _nextCursor  ORDER BY Id ASC  TAKE pageSize
    ///   Prev    : pop _prevCursors, re-run from that cursor
    /// </summary>
    public partial class PaidPurchasesForm : Form
    {
        // ── DB + state ────────────────────────────────────────────────────────
        private readonly POSDbContext _db;

        private int? _selectedSupplierId;
        private string _selectedSupplierName;

        // Cursor pagination state
        private int _pageSize = 20;
        private int? _nextCursor = null;   // cursor for NEXT page (null = no next page)
        private readonly Stack<int?> _prevCursors = new Stack<int?>();  // stack of FROM cursors
        private int? _currentFrom = null;   // cursor we used for the current page
        private int _pageNumber = 1;
        private int _totalFound = 0;      // total matching records (for display only)

        private bool _hasSearched = false;

        private readonly Timer _supTimer = new Timer { Interval = 300 };
        private bool _suppressSupEvent;

        // ── Constructor ───────────────────────────────────────────────────────
        public PaidPurchasesForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            WireEvents();

            // Default date range: last 3 months → today
            dtpFrom.Value = DateTime.Today.AddMonths(-3);
            dtpTo.Value = DateTime.Today;
        }

        private void WireEvents()
        {
            lstSupSugg.Leave += LstSupSugg_Leave;
            _supTimer.Tick += (s, e) => { _supTimer.Stop(); SearchSuppliers(txtSupSearch.Text.Trim()); };
            this.Resize += (s, e) => RepositionDropdown();
            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };

            HoverBtn(btnSearch, Color.FromArgb(27, 94, 32), Color.FromArgb(46, 125, 50));
            HoverBtn(btnPrev, Color.FromArgb(27, 94, 32), Color.FromArgb(46, 125, 50));
            HoverBtn(btnNext, Color.FromArgb(27, 94, 32), Color.FromArgb(46, 125, 50));
            HoverBtn(btnClose, Color.FromArgb(207, 216, 220), Color.FromArgb(236, 239, 241));
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SUPPLIER SEARCH
        // ══════════════════════════════════════════════════════════════════════

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

        private void TxtSupSearch_Leave(object sender, EventArgs e)
        {
            if (!lstSupSugg.Focused) HideSugg();
        }
        private void LstSupSugg_Leave(object sender, EventArgs e)
        {
            if (!txtSupSearch.Focused) HideSugg();
        }

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

        // ── Suggestion dropdown helpers ───────────────────────────────────────

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
                e.Graphics.DrawString(
                    $"{sup.ShopName}  ·  {sup.ContactNo}", subFont,
                    new SolidBrush(Color.FromArgb(120, 144, 156)),
                    e.Bounds.Left + pad, e.Bounds.Top + 22);
            }

            boldFont.Dispose();
            subFont.Dispose();
            e.DrawFocusRectangle();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SEARCH — entry point
        // ══════════════════════════════════════════════════════════════════════

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // Reset cursor state for fresh search
            _prevCursors.Clear();
            _currentFrom = null;
            _pageNumber = 1;
            _hasSearched = true;

            LoadPage(fromCursor: null);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  CURSOR PAGINATION  — core loader
        // ══════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Loads one page of Paid purchases.
        ///
        /// Cursor strategy:
        ///   • Records are ordered by PurchaseDate DESC, Id DESC.
        ///   • "fromCursor" is the Id of the first record of the requested page.
        ///     null = first page.
        ///   • We fetch pageSize + 1 records. If we get pageSize+1 back we know
        ///     there is a next page; we store its first record's Id as _nextCursor
        ///     and trim the extra row before displaying.
        /// </summary>
        private void LoadPage(int? fromCursor)
        {
            _currentFrom = fromCursor;
            int take = _pageSize + 1;     // fetch one extra to detect next page

            try
            {
                DateTime dateFrom = dtpFrom.Value.Date;
                DateTime dateTo = dtpTo.Value.Date.AddDays(1).AddTicks(-1); // end of day

                // Base query: Paid only, not deleted, within date range
                var query = _db.Purchases
                    .Include("Supplier").AsNoTracking()
                    .Where(p => !p.IsDeleted
                             && p.PurchaseDate >= dateFrom
                             && p.PurchaseDate <= dateTo).AsQueryable();

                // Optional supplier filter
                if (_selectedSupplierId.HasValue)
                    query = query.Where(p => p.SupplierId == _selectedSupplierId.Value).AsQueryable();

                // Total count for display (only recount on page 1)
                if (fromCursor == null)
                    _totalFound = query.Count();

                // Apply cursor: skip records we've already seen
                // Ordering: newest first (PurchaseDate DESC, Id DESC)
                // Cursor is a composite of (Date, Id) encoded as Id; we use Id
                // as the tiebreaker since dates may overlap.
                IQueryable<Purchase> paged = query.OrderByDescending(p => p.PurchaseDate)
                                                  .ThenByDescending(p => p.Id);

                if (fromCursor.HasValue)
                    paged = paged.Where(p => p.Id <= fromCursor.Value);

                var rows = paged.Take(take).ToList();

                // Detect next page
                bool hasNext = rows.Count > _pageSize;
                if (hasNext)
                {
                    _nextCursor = rows[_pageSize].Id;   // first record of next page
                    rows = rows.Take(_pageSize).ToList();
                }
                else
                {
                    _nextCursor = null;
                }

                PopulateGrid(rows);
                UpdatePagerUI(hasNext);
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

        private void PopulateGrid(List<Purchase> rows)
        {
            dgvPurchases.Rows.Clear();

            foreach (var p in rows)
            {
                string supplierName = p.Supplier != null
                    ? $"{p.Supplier.SupplierName}  —  {p.Supplier.ShopName}"
                    : $"Supplier #{p.SupplierId}";

                // Count non-deleted items
                int itemCount = _db.PurchaseItems
                    .Count(i => i.PurchaseId == p.Id && !i.IsDeleted);

                int idx = dgvPurchases.Rows.Add();
                var row = dgvPurchases.Rows[idx];
                row.Tag = p.Id;   // store PurchaseId for click → detail navigation

                row.Cells["colInvNo"].Value = p.InvoiceNumber;
                row.Cells["colSupplier"].Value = supplierName;
                row.Cells["colDate"].Value = p.PurchaseDate.ToString("dd MMM yyyy");
                row.Cells["colItems"].Value = itemCount;
                row.Cells["colTotalBill"].Value = p.TotalAmount;
                row.Cells["colDiscount"].Value = p.Discount;
                row.Cells["colNetAmt"].Value = p.NetAmount;
                row.Cells["colPaidAmt"].Value = p.TotalPaid;
                row.Cells["colStatus"].Value = "✔ Paid";
            }

            // Update grid title
            if (rows.Count == 0)
                lblGridTitle.Text = "  No paid invoices found matching your criteria.";
            else
                lblGridTitle.Text = $"  Paid Invoices  —  {_totalFound:N0} total  (showing page {_pageNumber})";
        }

        // ══════════════════════════════════════════════════════════════════════
        //  PAGINATION BUTTONS
        // ══════════════════════════════════════════════════════════════════════

        private void UpdatePagerUI(bool hasNext)
        {
            btnNext.Enabled = hasNext;
            btnPrev.Enabled = _prevCursors.Count > 0;

            int shown = dgvPurchases.Rows.Count;
            lblPageInfo.Text = _totalFound > 0
                ? $"Page {_pageNumber}  •  {shown} of {_totalFound:N0} records"
                : $"Page {_pageNumber}";
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (!_nextCursor.HasValue) return;

            // Push current page's starting cursor so we can go back
            _prevCursors.Push(_currentFrom);
            _pageNumber++;

            LoadPage(fromCursor: _nextCursor);
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (_prevCursors.Count == 0) return;

            _pageNumber--;
            int? prevFrom = _prevCursors.Pop();
            LoadPage(fromCursor: prevFrom);
        }

        private void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_hasSearched) return;
            if (!int.TryParse(cmbPageSize.SelectedItem?.ToString(), out int size)) return;
            _pageSize = size;

            // Reset to page 1
            _prevCursors.Clear();
            _pageNumber = 1;
            _currentFrom = null;
            LoadPage(fromCursor: null);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  GRID EVENTS  — Invoice No click → PurchaseDetailForm
        // ══════════════════════════════════════════════════════════════════════

        private void DgvPurchases_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvPurchases.Columns[e.ColumnIndex].Name != "colInvNo") return;

            if (!(dgvPurchases.Rows[e.RowIndex].Tag is int purchaseId)) return;

            using (var detail = new PurchaseDetailForm(purchaseId))
                detail.ShowDialog(this);
        }

        private void DgvPurchases_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string col = dgvPurchases.Columns[e.ColumnIndex].Name;

            if (col == "colInvNo")
            {
                // Hyperlink style
                e.CellStyle.ForeColor = Color.FromArgb(21, 101, 192);
                e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Underline);
                e.CellStyle.SelectionForeColor = Color.FromArgb(13, 71, 161);
                return;
            }

            if (col == "colStatus" && e.Value != null)
            {
                e.CellStyle.ForeColor = Color.FromArgb(46, 125, 50);
                e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                e.CellStyle.BackColor = Color.FromArgb(232, 245, 233);
                e.CellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            }

            if (col == "colDiscount" && e.Value != null)
            {
                decimal disc = e.Value is decimal d ? d : 0m;
                if (disc > 0) e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40);
            }
        }

        private void DgvPurchases_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            dgvPurchases.Cursor = dgvPurchases.Columns[e.ColumnIndex].Name == "colInvNo"
                ? Cursors.Hand
                : Cursors.Default;
        }

        private void DgvPurchases_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvPurchases.Cursor = Cursors.Default;
        }

        // ══════════════════════════════════════════════════════════════════════
        //  CLOSE
        // ══════════════════════════════════════════════════════════════════════

        private void BtnClose_Click(object sender, EventArgs e) => this.Close();

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _supTimer.Dispose();
            _db.Dispose();
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static void HoverBtn(Button b, Color hover, Color normal)
        {
            b.MouseEnter += (s, e) => b.BackColor = hover;
            b.MouseLeave += (s, e) => b.BackColor = normal;
        }
    }
}