
//using POS_Shop.Models;
//using POS_Shop.Models.Suppliers;
//using POS_Shop.Repositories;
//using System;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Controllers.Supplier
//{
//    /// <summary>
//    /// Records a purchase invoice from a supplier.
//    ///
//    /// ── ONE PURCHASE PER SUPPLIER PER DAY ────────────────────────────────────
//    /// When a supplier is selected the DB is checked for an existing Purchase
//    /// on the selected date. If found the invoice is loaded into the grid for
//    /// editing / appending; saving will UPDATE that record. If not found a new
//    /// invoice is created as normal.
//    ///
//    /// ── PAYMENT ──────────────────────────────────────────────────────────────
//    /// Payment is NOT recorded here. Use SupplierPaymentForm.
//    /// Every saved invoice starts with PaymentStatus = Pending, Balance = NetAmount.
//    /// </summary>
//    public partial class PurchaseForm : Form
//    {
//        // ── State ──────────────────────────────────────────────────────────────
//        private readonly POSDbContext _db;

//        private int? _selectedSupplierId;
//        private string _selectedSupplierName;

//        private int? _selectedProductId;
//        private string _selectedProductName;
//        private string _selectedProductCode;

//        /// <summary>
//        /// null  = new invoice mode.
//        /// set   = editing an existing invoice loaded from DB.
//        /// </summary>
//        private int? _existingPurchaseId = null;

//        private readonly Timer _supplierTimer = new Timer { Interval = 300 };
//        private readonly Timer _productTimer = new Timer { Interval = 300 };

//        private bool _suppressSupplierEvent;
//        private bool _suppressProductEvent;
//        private bool _suppressGridEvent;

//        // ── Constructor ────────────────────────────────────────────────────────
//        public PurchaseForm()
//        {
//            InitializeComponent();
//            _db = new POSDbContext();
//            WireEvents();
//            SetupForm();
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  SETUP
//        // ══════════════════════════════════════════════════════════════════════

//        private void SetupForm()
//        {
//            lblHeaderDate.Text = "Date: " + DateTime.Now.ToString("dd MMM yyyy");
//            lblInvoiceNo.Text = GenerateInvoiceNumber();
//            dtpPurchaseDate.Value = DateTime.Now;
//        }

//        private void WireEvents()
//        {
//            // ── Things the designer CANNOT handle ─────────────────────────────

//            // Leave events (needed for suggestion-list focus management)
//            lstSupplierSugg.Leave += LstSupplierSugg_Leave;
//            lstProductSugg.Leave += LstProductSugg_Leave;

//            // Debounce timers (lambdas must stay out of designer)
//            _supplierTimer.Tick += (s, e) => { _supplierTimer.Stop(); SearchSuppliers(txtSupplierSearch.Text.Trim()); };
//            _productTimer.Tick += (s, e) => { _productTimer.Stop(); SearchProducts(txtProductSearch.Text.Trim()); };

//            // Hover colour effects
//            HoverBtn(btnAddItem, Color.FromArgb(13, 71, 161), Color.FromArgb(21, 101, 192));
//            HoverBtn(btnSave, Color.FromArgb(27, 94, 32), Color.FromArgb(46, 125, 50));
//            HoverBtn(btnClearAll, Color.FromArgb(183, 28, 28), Color.FromArgb(198, 40, 40));

//            // Resize — reposition floating suggestion dropdowns
//            this.Resize += (s, e) => RepositionDropdowns();

//            // Panel border paint (cannot be wired in designer)
//            pnlAddProduct.Paint += pnlAddProduct_Paint;

//            // Date change → re-check for existing purchase on new date
//            dtpPurchaseDate.ValueChanged += DtpPurchaseDate_ValueChanged;



//            WireKeyboardNavigation();   // ← ADD THIS LINE
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  ONE PURCHASE PER SUPPLIER PER DAY
//        // ══════════════════════════════════════════════════════════════════════

//        /// <summary>
//        /// Called after supplier is confirmed OR the purchase date changes.
//        /// Queries the DB for an existing Purchase for this supplier on the
//        /// selected date. Loads it if found; stays in new-mode if not.
//        /// </summary>
//        private void CheckForExistingPurchase()
//        {
//            if (!_selectedSupplierId.HasValue) return;

//            DateTime date = dtpPurchaseDate.Value.Date;

//            try
//            {
//                var existing = _db.Purchases
//                    .Where(p => p.SupplierId == _selectedSupplierId.Value
//                             && !p.IsDeleted
//                             && System.Data.Entity.DbFunctions.TruncateTime(p.PurchaseDate) == date)
//                    .FirstOrDefault();

//                if (existing != null)
//                    LoadExistingPurchase(existing);
//                else if (_existingPurchaseId.HasValue)
//                    SwitchToNewMode();      // date changed and no purchase on new date
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error checking existing purchase:\n" + ex.Message);
//            }
//        }

//        ///// <summary>Populates the form with an existing invoice for editing.</summary>
//        private void LoadExistingPurchase(Purchase purchase)
//        {
//            _existingPurchaseId = purchase.Id;

//            lblInvoiceNo.Text = purchase.InvoiceNumber;
//            txtReferenceNo.Text = purchase.SupplierReferenceNo ?? string.Empty;
//            dtpPurchaseDate.Value = purchase.PurchaseDate;
//            txtDiscount.Text = purchase.Discount.ToString("N2");
//            txtNotes.Text = purchase.Notes ?? string.Empty;

//            // Load items
//            dgvItems.Rows.Clear();

//            var full = _db.Purchases
//                .Include("PurchaseItems")
//                .FirstOrDefault(p => p.Id == purchase.Id);

//            if (full?.PurchaseItems != null)
//            {
//                foreach (var item in full.PurchaseItems.Where(i => !i.IsDeleted))
//                {
//                    var product = _db.Products.FirstOrDefault(p => p.Id == item.ProductId);
//                    string pName = product?.ProductEnglishName ?? $"Product #{item.ProductId}";
//                    string pCode = product?.SearchByProductCode ?? "";

//                    string unitName = "-";
//                    //if (item.ProductUnitId.HasValue && item.ProductUnitId > 0)
//                    //{
//                    //    var unit = _db.ProductUnits.FirstOrDefault(u => u.Id == item.ProductUnitId);
//                    //    unitName = unit?.Name ?? "-";
//                    //}

//                    int idx = dgvItems.Rows.Add();
//                    var row = dgvItems.Rows[idx];
//                    row.Tag = item.ProductId;                        // product id on row

//                    row.Cells["colSrNo"].Tag = item.Id;    // existing PurchaseItem.Id
//                    row.Cells["colSrNo"].Value = idx + 1;
//                    row.Cells["colProductCode"].Value = pCode;
//                    row.Cells["colProductName"].Value = pName;
//                    row.Cells["colUnit"].Tag = item.ProductUnitId;
//                    row.Cells["colQty"].Value = item.Quantity;
//                    row.Cells["colPrice"].Value = item.PurchasePrice;
//                    row.Cells["colTotal"].Value = item.TotalPrice;
//                }
//            }

//            UpdateItemCount();
//            RefreshTotals();
//            SetEditModeBanner(true);
//        }


//        ///// <summary>Populates the form with an existing invoice for editing.</summary>
//        //private void LoadExistingPurchase(Purchase purchase)
//        //{
//        //    _existingPurchaseId = purchase.Id;

//        //    lblInvoiceNo.Text = purchase.InvoiceNumber;
//        //    txtReferenceNo.Text = purchase.SupplierReferenceNo ?? string.Empty;
//        //    dtpPurchaseDate.Value = purchase.PurchaseDate;
//        //    txtDiscount.Text = purchase.Discount.ToString("N2");
//        //    txtNotes.Text = purchase.Notes ?? string.Empty;

//        //    // Load items
//        //    dgvItems.Rows.Clear();

//        //    var full = _db.Purchases
//        //        .Include("PurchaseItems")
//        //        .FirstOrDefault(p => p.Id == purchase.Id);

//        //    if (full?.PurchaseItems != null)
//        //    {
//        //        foreach (var item in full.PurchaseItems.Where(i => !i.IsDeleted))
//        //        {
//        //            var product = _db.Products.FirstOrDefault(p => p.Id == item.ProductId);
//        //            string pName = product?.ProductEnglishName ?? $"Product #{item.ProductId}";
//        //            string pCode = product?.SearchByProductCode ?? "";

//        //            string unitName = "-";
//        //            if (item.ProductUnitId.HasValue && item.ProductUnitId > 0)
//        //            {
//        //                var unit = _db.ProductUnits.FirstOrDefault(u => u.Id == item.ProductUnitId);
//        //                unitName = unit?.Name ?? "-";
//        //            }

//        //            int idx = dgvItems.Rows.Add();
//        //            var row = dgvItems.Rows[idx];
//        //            row.Tag = item.ProductId;                        // product id on row

//        //            row.Cells["colSrNo"].Tag = item.Id;    // existing PurchaseItem.Id
//        //            row.Cells["colSrNo"].Value = idx + 1;
//        //            row.Cells["colProductCode"].Value = pCode;
//        //            row.Cells["colProductName"].Value = pName;
//        //            row.Cells["colUnit"].Value = unitName;
//        //            row.Cells["colUnit"].Tag = item.ProductUnitId ?? 0;
//        //            row.Cells["colQty"].Value = item.Quantity;
//        //            row.Cells["colPrice"].Value = item.PurchasePrice;
//        //            row.Cells["colTotal"].Value = item.TotalPrice;
//        //        }
//        //    }

//        //    UpdateItemCount();
//        //    RefreshTotals();
//        //    SetEditModeBanner(true);
//        //}


//        private void SwitchToNewMode()
//        {
//            _existingPurchaseId = null;
//            lblInvoiceNo.Text = GenerateInvoiceNumber();
//            dgvItems.Rows.Clear();
//            UpdateItemCount();
//            RefreshTotals();
//            SetEditModeBanner(false);
//        }

//        /// <summary>Changes the grid title bar and Save button to signal edit vs new mode.</summary>
//        private void SetEditModeBanner(bool isEdit)
//        {
//            if (isEdit)
//            {
//                lblGridTitle.Text = $"  Purchase Items  ✏  EDITING EXISTING — {lblInvoiceNo.Text}";
//                lblGridTitle.BackColor = Color.FromArgb(230, 81, 0);   // orange
//                btnSave.Text = "Update Purchase";
//                btnSave.BackColor = Color.FromArgb(230, 81, 0);
//            }
//            else
//            {
//                lblGridTitle.Text = "  Purchase Items";
//                lblGridTitle.BackColor = Color.FromArgb(21, 101, 192); // blue
//                btnSave.Text = "Save Purchase";
//                btnSave.BackColor = Color.FromArgb(46, 125, 50);  // green
//            }
//        }

//        private void DtpPurchaseDate_ValueChanged(object sender, EventArgs e)
//        {
//            CheckForExistingPurchase();
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  SUPPLIER SEARCH
//        // ══════════════════════════════════════════════════════════════════════

//        private void TxtSupplierSearch_TextChanged(object sender, EventArgs e)
//        {
//            if (_suppressSupplierEvent) return;
//            if (_selectedSupplierId.HasValue) ClearSupplierSelection(false);
//            _supplierTimer.Stop();
//            _supplierTimer.Start();
//        }

//        private void SearchSuppliers(string q)
//        {
//            if (q.Length < 1) { Hide(lstSupplierSugg); return; }
//            try
//            {
//                var list = _db.Suppliers
//                    .Where(s => !s.IsDeleted &&
//                               (s.SupplierName.Contains(q) ||
//                                s.ShopName.Contains(q) ||
//                                s.ContactNo.Contains(q)))
//                    .OrderBy(s => s.SupplierName)
//                    .Take(8).ToList();

//                lstSupplierSugg.DataSource = list.Count > 0 ? (object)list : null;
//                lstSupplierSugg.DisplayMember = "SupplierName";
//                lstSupplierSugg.ValueMember = "Id";
//                Show(lstSupplierSugg, txtSupplierSearch, list.Count);
//            }
//            catch (Exception ex) { MessageBox.Show("Supplier search: " + ex.Message); }
//        }

//        private void TxtSupplierSearch_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (!lstSupplierSugg.Visible) return;
//            if (e.KeyCode == Keys.Down)
//            {
//                lstSupplierSugg.Focus();
//                if (lstSupplierSugg.Items.Count > 0) lstSupplierSugg.SelectedIndex = 0;
//                e.Handled = true;
//            }
//            else if (e.KeyCode == Keys.Escape) { Hide(lstSupplierSugg); e.Handled = true; }
//        }

//        private void LstSupplierSugg_MouseClick(object sender, MouseEventArgs e) => SelectSupplier();
//        private void LstSupplierSugg_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter) { SelectSupplier(); e.Handled = true; }
//            else if (e.KeyCode == Keys.Escape) { Hide(lstSupplierSugg); txtSupplierSearch.Focus(); e.Handled = true; }
//        }

//        private void TxtSupplierSearch_Leave(object sender, EventArgs e) { if (!lstSupplierSugg.Focused) Hide(lstSupplierSugg); }
//        private void LstSupplierSugg_Leave(object sender, EventArgs e) { if (!txtSupplierSearch.Focused) Hide(lstSupplierSugg); }

//        private void SelectSupplier()
//        {
//            if (!(lstSupplierSugg.SelectedItem is Models.Supplier s)) return;

//            _selectedSupplierId = s.Id;
//            _selectedSupplierName = $"{s.SupplierName}  —  {s.ShopName}";

//            _suppressSupplierEvent = true;
//            txtSupplierSearch.Text = string.Empty;
//            _suppressSupplierEvent = false;

//            lblSelectedSupplier.Text = _selectedSupplierName;
//            pnlSupplierBadge.Visible = true;
//            Hide(lstSupplierSugg);

//            // KEY: check for an existing purchase on the selected date
//            CheckForExistingPurchase();

//            txtReferenceNo.Focus();
//        }

//        private void BtnClearSupplier_Click(object sender, EventArgs e) => ClearSupplierSelection(true);

//        private void ClearSupplierSelection(bool focusSearch)
//        {
//            _selectedSupplierId = null;
//            _selectedSupplierName = null;
//            pnlSupplierBadge.Visible = false;

//            if (_existingPurchaseId.HasValue) SwitchToNewMode();

//            if (focusSearch) txtSupplierSearch.Focus();
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  PRODUCT SEARCH
//        // ══════════════════════════════════════════════════════════════════════

//        private void TxtProductSearch_TextChanged(object sender, EventArgs e)
//        {
//            if (_suppressProductEvent) return;
//            if (_selectedProductId.HasValue) ClearProductSelection();
//            _productTimer.Stop();
//            _productTimer.Start();
//        }

//        private void SearchProducts(string q)
//        {
//            if (q.Length < 1) { Hide(lstProductSugg); return; }
//            try
//            {
//                var data = _db.Products.AsQueryable();
//                var words = q.ToLower().Split(' ');
//                foreach (var word in words)
//                    data = data.Where(p => p.ProductEnglishName.Contains(word)
//                                        || p.Id.ToString().Contains(word)
//                                        || p.SearchByProductCode.Contains(word));

//                var list = data.OrderBy(p => p.ProductEnglishName).Take(20).ToList();

//                lstProductSugg.DataSource = list.Count > 0 ? (object)list : null;
//                lstProductSugg.DisplayMember = "ProductEnglishName";
//                lstProductSugg.ValueMember = "Id";
//                Show(lstProductSugg, txtProductSearch, list.Count);
//            }
//            catch (Exception ex) { MessageBox.Show("Product search: " + ex.Message); }
//        }

//        private void TxtProductSearch_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (!lstProductSugg.Visible) return;
//            if (e.KeyCode == Keys.Down)
//            {
//                lstProductSugg.Focus();
//                if (lstProductSugg.Items.Count > 0) lstProductSugg.SelectedIndex = 0;
//                e.Handled = true;
//            }
//            else if (e.KeyCode == Keys.Escape) { Hide(lstProductSugg); e.Handled = true; }
//        }

//        private void LstProductSugg_MouseClick(object sender, MouseEventArgs e) => SelectProduct();
//        private void LstProductSugg_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter) { SelectProduct(); e.Handled = true; }
//            else if (e.KeyCode == Keys.Escape) { Hide(lstProductSugg); txtProductSearch.Focus(); e.Handled = true; }
//        }

//        private void TxtProductSearch_Leave(object sender, EventArgs e) { if (!lstProductSugg.Focused) Hide(lstProductSugg); }
//        private void LstProductSugg_Leave(object sender, EventArgs e) { if (!txtProductSearch.Focused) Hide(lstProductSugg); }

//        private void SelectProduct()
//        {
//            if (!(lstProductSugg.SelectedItem is Models.Product p)) return;

//            _selectedProductId = p.Id;
//            _selectedProductName = p.ProductEnglishName;
//            _selectedProductCode = p.SearchByProductCode;

//            _suppressProductEvent = true;
//            txtProductSearch.Text = p.ProductEnglishName;
//            _suppressProductEvent = false;

//            Hide(lstProductSugg);
//            LoadUnitsForProduct(p.Id);

//            if (!string.IsNullOrEmpty(p.PurchasePrice) &&
//                decimal.TryParse(p.PurchasePrice, out decimal price))
//                txtItemPrice.Text = price.ToString("N2");

//            cmbUnit.Focus();
//            //txtQty.Focus();
//            //txtQty.SelectAll();
//        }

//        private void ClearProductSelection()
//        {
//            _selectedProductId = null;
//            _selectedProductName = null;
//            _selectedProductCode = null;
//            cmbUnit.DataSource = null;
//            txtItemPrice.Text = "0.00";
//            txtItemTotal.Text = "0.00";
//        }

//        private void LoadUnitsForProduct(int productId)
//        {
//            using (var ctx = new POSDbContext())
//            {
//                var repo = new ProductUnitRepository(ctx);
//                var units = repo.GetAll()
//                    .Select(u => new ProductUnit { Id = u.Id, Name = u.Name })
//                    .ToList();

//                cmbUnit.Items.Clear();
//                cmbUnit.DataSource = units;
//                cmbUnit.DisplayMember = "Name";
//                cmbUnit.ValueMember = "Name";
//                if (cmbUnit.Items.Count > 0) cmbUnit.SelectedIndex = 0;
//            }
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  SUGGESTION LIST — SHARED HELPERS
//        // ══════════════════════════════════════════════════════════════════════

//        private void Show(ListBox lst, System.Windows.Forms.Control anchor, int count)
//        {
//            if (count == 0) { Hide(lst); return; }
//            System.Drawing.Point pt = PointToClient(anchor.Parent.PointToScreen(anchor.Location));
//            lst.Location = new System.Drawing.Point(pt.X, pt.Y + anchor.Height);
//            lst.Width = anchor.Width;
//            lst.Height = Math.Min(count, 6) * lst.ItemHeight + 2;
//            lst.BringToFront();
//            lst.Visible = true;
//        }

//        private void Hide(ListBox lst)
//        {
//            lst.Visible = false;
//            lst.DataSource = null;
//        }

//        private void RepositionDropdowns()
//        {
//            if (lstSupplierSugg.Visible) Show(lstSupplierSugg, txtSupplierSearch, lstSupplierSugg.Items.Count);
//            if (lstProductSugg.Visible) Show(lstProductSugg, txtProductSearch, lstProductSugg.Items.Count);
//        }

//        private void LstSugg_DrawItem(object sender, DrawItemEventArgs e)
//        {
//            if (e.Index < 0) return;
//            var lst = (ListBox)sender;
//            var item = lst.Items[e.Index];
//            bool sel = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

//            e.Graphics.FillRectangle(
//                sel ? new SolidBrush(Color.FromArgb(227, 242, 253)) : Brushes.White,
//                e.Bounds);

//            if (e.Index > 0)
//                e.Graphics.DrawLine(new Pen(Color.FromArgb(236, 239, 241)),
//                    e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);

//            var boldFont = new Font("Segoe UI", 10F, FontStyle.Bold);
//            var subFont = new Font("Segoe UI", 8.5F);
//            int pad = 10;

//            if (item is Models.Supplier sup)
//            {
//                e.Graphics.DrawString(sup.SupplierName, boldFont,
//                    new SolidBrush(Color.FromArgb(26, 35, 126)), e.Bounds.Left + pad, e.Bounds.Top + 4);
//                e.Graphics.DrawString($"{sup.ShopName}  ·  {sup.ContactNo}", subFont,
//                    new SolidBrush(Color.FromArgb(120, 144, 156)), e.Bounds.Left + pad, e.Bounds.Top + 22);
//            }
//            else if (item is Models.Product prod)
//            {
//                e.Graphics.DrawString(prod.ProductEnglishName, boldFont,
//                    new SolidBrush(Color.FromArgb(26, 35, 126)), e.Bounds.Left + pad, e.Bounds.Top + 4);
//                string sub = prod.ProductUrduName ?? "";
//                if (!string.IsNullOrEmpty(prod.SearchByProductCode)) sub += $"  [{prod.SearchByProductCode}]";
//                e.Graphics.DrawString(sub, subFont,
//                    new SolidBrush(Color.FromArgb(120, 144, 156)), e.Bounds.Left + pad, e.Bounds.Top + 22);
//            }

//            boldFont.Dispose();
//            subFont.Dispose();
//            e.DrawFocusRectangle();
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  ITEM CALCULATION
//        // ══════════════════════════════════════════════════════════════════════

//        private void TxtCalc_TextChanged(object sender, EventArgs e)
//        {
//            decimal qty = D(txtQty.Text);
//            decimal price = D(txtItemPrice.Text);
//            txtItemTotal.Text = (qty * price).ToString("N2");
//        }

//        private void TxtDiscount_TextChanged(object sender, EventArgs e) => RefreshTotals();

//        // ══════════════════════════════════════════════════════════════════════
//        //  ADD ITEM TO GRID
//        // ══════════════════════════════════════════════════════════════════════

//        private void BtnAddItem_Click(object sender, EventArgs e)
//        {
//            if (!_selectedProductId.HasValue)
//            { Error("Please select a product from the suggestions."); txtProductSearch.Focus(); return; }

//            decimal qty = D(txtQty.Text);
//            decimal price = D(txtItemPrice.Text);

//            if (qty <= 0) { Error("Quantity must be greater than 0."); txtQty.Focus(); return; }
//            if (price <= 0) { Error("Price must be greater than 0."); txtItemPrice.Focus(); return; }

//            // Duplicate product → merge qty into existing row
//            foreach (DataGridViewRow row in dgvItems.Rows)
//            {
//                if (row.Tag is int pid && pid == _selectedProductId.Value)
//                {
//                    decimal existing = D(row.Cells["colQty"].Value?.ToString());
//                    decimal newQty = existing + qty;
//                    row.Cells["colQty"].Value = newQty;
//                    row.Cells["colTotal"].Value = newQty * D(row.Cells["colPrice"].Value?.ToString());
//                    RefreshTotals();
//                    ResetProductRow();
//                    return;
//                }
//            }

//         //   var unit = cmbUnit.SelectedItem as ProductUnit;
//            int idx = dgvItems.Rows.Add();
//            var r = dgvItems.Rows[idx];
//            r.Tag = _selectedProductId.Value;


//            // colSrNo.Tag = 0 means this is a brand-new item (no DB PurchaseItem.Id yet)
//            r.Cells["colSrNo"].Tag = 0;
//            r.Cells["colSrNo"].Value = dgvItems.Rows.Count;
//            r.Cells["colProductCode"].Value = _selectedProductCode;
//            r.Cells["colProductName"].Value = _selectedProductName;
//            r.Cells["colUnit"].Value = cmbUnit.SelectedItem?.ToString();
//            r.Cells["colUnit"].Tag = cmbUnit.SelectedItem?.ToString();
//            r.Cells["colQty"].Value = qty;
//            r.Cells["colPrice"].Value = price;
//            r.Cells["colTotal"].Value = qty * price;

//            UpdateItemCount();
//            RefreshTotals();
//            ResetProductRow();
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  DATAGRID EVENTS
//        // ══════════════════════════════════════════════════════════════════════

//        private void DgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex < 0) return;
//            if (dgvItems.Columns[e.ColumnIndex].Name != "colDelete") return;

//            if (MessageBox.Show("Remove this item?", "Confirm",
//                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
//                return;

//            var row = dgvItems.Rows[e.RowIndex];

//            // If this row came from the DB, soft-delete it immediately so the
//            // change is persisted even if the user cancels without clicking Save.
//            int purchaseItemId = row.Cells["colSrNo"].Tag is int t ? t : 0;
//            if (purchaseItemId > 0 && _existingPurchaseId.HasValue)
//            {
//                try
//                {
//                    var dbItem = _db.PurchaseItems.FirstOrDefault(i => i.Id == purchaseItemId);
//                    if (dbItem != null)
//                    {
//                        dbItem.IsDeleted = true;
//                        _db.SaveChanges();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Could not remove item from DB:\n" + ex.Message);
//                    return;
//                }
//            }

//            dgvItems.Rows.RemoveAt(e.RowIndex);
//            Renumber();
//            UpdateItemCount();
//            RefreshTotals();
//        }

//        private void DgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex < 0 || _suppressGridEvent) return;
//            string col = dgvItems.Columns[e.ColumnIndex].Name;
//            if (col != "colQty" && col != "colPrice") return;

//            _suppressGridEvent = true;
//            decimal qty = D(dgvItems.Rows[e.RowIndex].Cells["colQty"].Value?.ToString());
//            decimal price = D(dgvItems.Rows[e.RowIndex].Cells["colPrice"].Value?.ToString());
//            dgvItems.Rows[e.RowIndex].Cells["colTotal"].Value = qty * price;
//            _suppressGridEvent = false;
//            RefreshTotals();
//        }

//        private void DgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
//        {
//            e.Control.KeyPress -= CellNum_KeyPress;
//            e.Control.KeyPress -= CellDec_KeyPress;
//            string col = dgvItems.Columns[dgvItems.CurrentCell.ColumnIndex].Name;
//            if (col == "colQty") e.Control.KeyPress += CellNum_KeyPress;
//            if (col == "colPrice") e.Control.KeyPress += CellDec_KeyPress;
//        }

//        private void CellNum_KeyPress(object sender, KeyPressEventArgs e)
//        { if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b') e.Handled = true; }

//        private void CellDec_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            var tb = sender as TextBox;
//            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b') { e.Handled = true; return; }
//            if (e.KeyChar == '.' && tb?.Text.Contains('.') == true) e.Handled = true;
//        }

//        private void Renumber()
//        {
//            for (int i = 0; i < dgvItems.Rows.Count; i++)
//                dgvItems.Rows[i].Cells["colSrNo"].Value = i + 1;
//        }

//        private void UpdateItemCount()
//        {
//            int n = dgvItems.Rows.Count;
//            lblItemCount.Text = $"{n} item{(n != 1 ? "s" : "")}";
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  TOTALS
//        // ══════════════════════════════════════════════════════════════════════

//        private void RefreshTotals()
//        {
//            decimal subtotal = 0;
//            foreach (DataGridViewRow row in dgvItems.Rows)
//                subtotal += D(row.Cells["colTotal"].Value?.ToString());

//            decimal discount = D(txtDiscount.Text);
//            decimal net = Math.Max(0, subtotal - discount);

//            lblSubtotalVal.Text = subtotal.ToString("N2");
//            lblNetVal.Text = net.ToString("N2");

//            lblStatusInfo.Text = net <= 0 ? "—" : "⏳ PENDING  (Pay later via Supplier Payment)";
//            lblStatusInfo.ForeColor = Color.FromArgb(245, 124, 0);
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  SAVE / UPDATE PURCHASE
//        // ══════════════════════════════════════════════════════════════════════

//        private void BtnSave_Click(object sender, EventArgs e)
//        {
//            if (!_selectedSupplierId.HasValue)
//            { Error("Please select a supplier."); txtSupplierSearch.Focus(); return; }

//            if (dgvItems.Rows.Count == 0)
//            { Error("Please add at least one product."); txtProductSearch.Focus(); return; }

//            decimal subtotal = 0;
//            foreach (DataGridViewRow row in dgvItems.Rows)
//                subtotal += D(row.Cells["colTotal"].Value?.ToString());

//            decimal discount = D(txtDiscount.Text);
//            decimal net = Math.Max(0, subtotal - discount);

//            if (_existingPurchaseId.HasValue)
//                UpdateExistingPurchase(net, subtotal, discount);
//            else
//                SaveNewPurchase(net, subtotal, discount);
//        }

//        // ── NEW PURCHASE ───────────────────────────────────────────────────────

//        private void SaveNewPurchase(decimal net, decimal subtotal, decimal discount)
//        {
//            var purchase = new Purchase
//            {
//                InvoiceNumber = lblInvoiceNo.Text,
//                SupplierReferenceNo = txtReferenceNo.Text.Trim(),
//                PurchaseDate = dtpPurchaseDate.Value,
//                SupplierId = _selectedSupplierId.Value,
//                TotalAmount = subtotal,
//                Discount = discount,
//                NetAmount = net,

//                // Payment starts at zero / Pending — SupplierPaymentForm handles payment
//                TotalPaid = 0,
//                Balance = net,
//                PaymentStatus = PurchasePaymentStatus.Pending,

//                Notes = txtNotes.Text.Trim(),
//                IsDeleted = false,
//                CreatedAt = DateTime.UtcNow
//            };

//            foreach (DataGridViewRow row in dgvItems.Rows)
//            {
//                purchase.PurchaseItems.Add(new PurchaseItem
//                {
//                    ProductId = (int)row.Tag,
//                    ProductUnitId = (int)(row.Cells["colUnit"].Tag ?? 0),
//                    Quantity = D(row.Cells["colQty"].Value?.ToString()),
//                    PurchasePrice = D(row.Cells["colPrice"].Value?.ToString()),
//                    TotalPrice = D(row.Cells["colTotal"].Value?.ToString())
//                });
//            }

//            try
//            {
//                _db.Purchases.Add(purchase);
//                _db.SaveChanges();

//                MessageBox.Show(
//                    $"✔  Purchase saved — PENDING payment\n\n" +
//                    $"Invoice   :  {purchase.InvoiceNumber}\n" +
//                    $"Supplier  :  {_selectedSupplierName}\n" +
//                    $"Items     :  {purchase.PurchaseItems.Count}\n" +
//                    $"Net Amount:  Rs. {net:N2}\n\n" +
//                    "Open Supplier Payment to record payment for this invoice.",
//                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                ResetForm();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Save failed:\n" + ex.Message, "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }


//        // ── NEW PURCHASE ───────────────────────────────────────────────────────

//        //private void SaveNewPurchase(decimal net, decimal subtotal, decimal discount)
//        //{
//        //    var purchase = new Purchase
//        //    {
//        //        InvoiceNumber = lblInvoiceNo.Text,
//        //        SupplierReferenceNo = txtReferenceNo.Text.Trim(),
//        //        PurchaseDate = dtpPurchaseDate.Value,
//        //        SupplierId = _selectedSupplierId.Value,
//        //        TotalAmount = subtotal,
//        //        Discount = discount,
//        //        NetAmount = net,

//        //        // Payment starts at zero / Pending — SupplierPaymentForm handles payment
//        //        TotalPaid = 0,
//        //        Balance = net,
//        //        PaymentStatus = PurchasePaymentStatus.Pending,

//        //        Notes = txtNotes.Text.Trim(),
//        //        IsDeleted = false,
//        //        CreatedAt = DateTime.UtcNow
//        //    };


//        //    try
//        //    {
//        //        foreach (DataGridViewRow row in dgvItems.Rows)
//        //        {

//        //            var data = new PurchaseItem();

//        //                data.ProductId = (int)row.Tag;
//        //                data.ProductUnitId = (int)(row.Cells["colUnit"].Tag ?? 0);
//        //                data.Quantity = D(row.Cells["colQty"].Value?.ToString());
//        //                data.PurchasePrice = D(row.Cells["colPrice"].Value?.ToString());
//        //                data.TotalPrice = D(row.Cells["colTotal"].Value?.ToString());


//        //            purchase.PurchaseItems.Add(data);
//        //            //purchase.PurchaseItems.Add(new PurchaseItem
//        //            //{
//        //            //    ProductId = (int)row.Tag,
//        //            //    ProductUnitId = (int)(row.Cells["colUnit"].Tag ?? 0),
//        //            //    Quantity = D(row.Cells["colQty"].Value?.ToString()),
//        //            //    PurchasePrice = D(row.Cells["colPrice"].Value?.ToString()),
//        //            //    TotalPrice = D(row.Cells["colTotal"].Value?.ToString())
//        //            //});
//        //        }
//        //    }
//        //    catch (Exception e)
//        //    {

//        //        throw;
//        //    }


//        //    try
//        //    {
//        //        _db.Purchases.Add(purchase);
//        //        _db.SaveChanges();

//        //        MessageBox.Show(
//        //            $"✔  Purchase saved — PENDING payment\n\n" +
//        //            $"Invoice   :  {purchase.InvoiceNumber}\n" +
//        //            $"Supplier  :  {_selectedSupplierName}\n" +
//        //            $"Items     :  {purchase.PurchaseItems.Count}\n" +
//        //            $"Net Amount:  Rs. {net:N2}\n\n" +
//        //            "Open Supplier Payment to record payment for this invoice.",
//        //            "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

//        //        ResetForm();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        MessageBox.Show("Save failed:\n" + ex.Message, "Error",
//        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
//        //    }
//        //}


//        // ── UPDATE EXISTING PURCHASE ───────────────────────────────────────────

//        private void UpdateExistingPurchase(decimal net, decimal subtotal, decimal discount)
//        {
//            try
//            {
//                var purchase = _db.Purchases
//                    .Include("PurchaseItems")
//                    .FirstOrDefault(p => p.Id == _existingPurchaseId.Value);

//                if (purchase == null)
//                {
//                    Error("The existing purchase record could not be found. Please refresh and try again.");
//                    return;
//                }

//                // Update header fields
//                purchase.SupplierReferenceNo = txtReferenceNo.Text.Trim();
//                purchase.TotalAmount = subtotal;
//                purchase.Discount = discount;
//                purchase.NetAmount = net;
//                purchase.Notes = txtNotes.Text.Trim();
//                purchase.UpdatedAt = DateTime.UtcNow;

//                // Keep Balance consistent with any existing payments
//                if (purchase.PaymentStatus == PurchasePaymentStatus.Pending)
//                    purchase.Balance = net;                         // nothing paid yet
//                else
//                    purchase.Balance = Math.Max(0, net - purchase.TotalPaid);

//                // Sync grid rows to DB
//                foreach (DataGridViewRow row in dgvItems.Rows)
//                {
//                    int productId = (int)row.Tag;
//                    int purchaseItemId = row.Cells["colSrNo"].Tag is int t ? t : 0;
//                    decimal qty = D(row.Cells["colQty"].Value?.ToString());
//                    decimal price = D(row.Cells["colPrice"].Value?.ToString());
//                    decimal total = D(row.Cells["colTotal"].Value?.ToString());
//                    string unitId = (string)(row.Cells["colUnit"].Tag ?? 0);

//                    if (purchaseItemId > 0)
//                    {
//                        // Row came from DB → UPDATE the existing PurchaseItem
//                        var existing = purchase.PurchaseItems
//                            .FirstOrDefault(i => i.Id == purchaseItemId && !i.IsDeleted);

//                        if (existing != null)
//                        {
//                            existing.Quantity = qty;
//                            existing.PurchasePrice = price;
//                            existing.TotalPrice = total;
//                            existing.ProductUnitId =Convert.ToInt32(unitId);
//                        }
//                    }
//                    else
//                    {
//                        // Row is new this session → INSERT a new PurchaseItem
//                        purchase.PurchaseItems.Add(new PurchaseItem
//                        {
//                            PurchaseId = purchase.Id,
//                            ProductId = productId,
//                            ProductUnitId = Convert.ToInt32(unitId),
//                            TotalPrice = total
//                        });
//                    }
//                }

//                _db.SaveChanges();

//                MessageBox.Show(
//                    $"✔  Purchase updated!\n\n" +
//                    $"Invoice   :  {purchase.InvoiceNumber}\n" +
//                    $"Supplier  :  {_selectedSupplierName}\n" +
//                    $"Items     :  {dgvItems.Rows.Count}\n" +
//                    $"Net Amount:  Rs. {net:N2}",
//                    "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                ResetForm();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Update failed:\n" + ex.Message, "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }


//        //private void UpdateExistingPurchase(decimal net, decimal subtotal, decimal discount)
//        //{
//        //    try
//        //    {
//        //        var purchase = _db.Purchases
//        //            .Include("PurchaseItems")
//        //            .FirstOrDefault(p => p.Id == _existingPurchaseId.Value);

//        //        if (purchase == null)
//        //        {
//        //            Error("The existing purchase record could not be found. Please refresh and try again.");
//        //            return;
//        //        }

//        //        // Update header fields
//        //        purchase.SupplierReferenceNo = txtReferenceNo.Text.Trim();
//        //        purchase.TotalAmount = subtotal;
//        //        purchase.Discount = discount;
//        //        purchase.NetAmount = net;
//        //        purchase.Notes = txtNotes.Text.Trim();
//        //        purchase.UpdatedAt = DateTime.UtcNow;

//        //        // Keep Balance consistent with any existing payments
//        //        if (purchase.PaymentStatus == PurchasePaymentStatus.Pending)
//        //            purchase.Balance = net;                         // nothing paid yet
//        //        else
//        //            purchase.Balance = Math.Max(0, net - purchase.TotalPaid);

//        //        // Sync grid rows to DB
//        //        foreach (DataGridViewRow row in dgvItems.Rows)
//        //        {
//        //            int productId = (int)row.Tag;
//        //            int purchaseItemId = row.Cells["colSrNo"].Tag is int t ? t : 0;
//        //            decimal qty = D(row.Cells["colQty"].Value?.ToString());
//        //            decimal price = D(row.Cells["colPrice"].Value?.ToString());
//        //            decimal total = D(row.Cells["colTotal"].Value?.ToString());
//        //            int unitId = (int)(row.Cells["colUnit"].Tag ?? 0);

//        //            if (purchaseItemId > 0)
//        //            {
//        //                // Row came from DB → UPDATE the existing PurchaseItem
//        //                var existing = purchase.PurchaseItems
//        //                    .FirstOrDefault(i => i.Id == purchaseItemId && !i.IsDeleted);

//        //                if (existing != null)
//        //                {
//        //                    existing.Quantity = qty;
//        //                    existing.PurchasePrice = price;
//        //                    existing.TotalPrice = total;
//        //                    existing.ProductUnitId = unitId;
//        //                }
//        //            }
//        //            else
//        //            {
//        //                // Row is new this session → INSERT a new PurchaseItem
//        //                purchase.PurchaseItems.Add(new PurchaseItem
//        //                {
//        //                    PurchaseId = purchase.Id,
//        //                    ProductId = productId,
//        //                    ProductUnitId = unitId,
//        //                    Quantity = qty,
//        //                    PurchasePrice = price,
//        //                    TotalPrice = total
//        //                });
//        //            }
//        //        }

//        //        _db.SaveChanges();

//        //        MessageBox.Show(
//        //            $"✔  Purchase updated!\n\n" +
//        //            $"Invoice   :  {purchase.InvoiceNumber}\n" +
//        //            $"Supplier  :  {_selectedSupplierName}\n" +
//        //            $"Items     :  {dgvItems.Rows.Count}\n" +
//        //            $"Net Amount:  Rs. {net:N2}",
//        //            "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

//        //        ResetForm();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        MessageBox.Show("Update failed:\n" + ex.Message, "Error",
//        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
//        //    }
//        //}

//        // ══════════════════════════════════════════════════════════════════════
//        //  CLEAR / RESET
//        // ══════════════════════════════════════════════════════════════════════

//        private void BtnClearAll_Click(object sender, EventArgs e)
//        {
//            if (MessageBox.Show("Clear all and reset the form?", "Confirm",
//                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//                ResetForm();
//        }

//        private void ResetForm()
//        {
//            _existingPurchaseId = null;
//            SetEditModeBanner(false);

//            ClearSupplierSelection(false);
//            _suppressSupplierEvent = true;
//            txtSupplierSearch.Text = string.Empty;
//            _suppressSupplierEvent = false;

//            ResetProductRow();
//            dgvItems.Rows.Clear();
//            UpdateItemCount();

//            txtReferenceNo.Text = string.Empty;
//            txtDiscount.Text = "0.00";
//            txtNotes.Text = string.Empty;
//            dtpPurchaseDate.Value = DateTime.Now;

//            RefreshTotals();
//            lblInvoiceNo.Text = GenerateInvoiceNumber();
//            txtSupplierSearch.Focus();
//        }

//        private void ResetProductRow()
//        {
//            _suppressProductEvent = true;
//            txtProductSearch.Text = string.Empty;
//            _suppressProductEvent = false;

//            ClearProductSelection();
//            txtQty.Text = "1";
//            txtItemPrice.Text = "0.00";
//            txtItemTotal.Text = "0.00";
//            txtProductSearch.Focus();
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  HELPERS
//        // ══════════════════════════════════════════════════════════════════════

//        private string GenerateInvoiceNumber()
//        {
//            try
//            {
//                int last = _db.Purchases.Any() ? _db.Purchases.Max(p => p.Id) : 0;
//                return $"INV-{(last + 1):D5}";
//            }
//            catch { return $"INV-{DateTime.Now:yyyyMMddHHmm}"; }
//        }

//        private static decimal D(string s) => decimal.TryParse(s, out decimal v) ? v : 0m;

//        private static void Error(string msg)
//            => MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

//        private static void HoverBtn(Button b, Color hover, Color normal)
//        {
//            b.MouseEnter += (s, e) => b.BackColor = hover;
//            b.MouseLeave += (s, e) => b.BackColor = normal;
//        }

//        private void NumericOnly(object sender, KeyPressEventArgs e)
//        { if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b') e.Handled = true; }

//        private void DecimalOnly(object sender, KeyPressEventArgs e)
//        {
//            var tb = sender as TextBox;
//            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b') { e.Handled = true; return; }
//            if (e.KeyChar == '.' && tb?.Text.Contains('.') == true) e.Handled = true;
//        }

//        private void pnlAddProduct_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
//        {
//            var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(207, 216, 220));
//            e.Graphics.DrawLine(pen, 0, 0, pnlAddProduct.Width, 0);
//            e.Graphics.DrawLine(pen, 0, pnlAddProduct.Height - 1, pnlAddProduct.Width, pnlAddProduct.Height - 1);
//            pen.Dispose();
//        }

//        protected override void OnFormClosed(FormClosedEventArgs e)
//        {
//            base.OnFormClosed(e);
//            _supplierTimer.Dispose();
//            _productTimer.Dispose();
//            _db.Dispose();
//        }


//        // ══════════════════════════════════════════════════════════════════════
//        //  KEYBOARD NAVIGATION  (Unit → Qty → Price → Total → Add Item)
//        //
//        //  Right / Enter  = move to next control
//        //  Left           = move to previous control
//        // ══════════════════════════════════════════════════════════════════════

//        // Add this method to PurchaseForm.cs and call it from WireEvents():
//        //
//        //   WireKeyboardNavigation();

//        private void WireKeyboardNavigation()
//        {
//            // ── cmbUnit: Right/Enter → txtQty ────────────────────────────────
//            cmbUnit.KeyDown += (s, e) =>
//            {
//                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
//                {
//                    e.Handled = true;
//                    e.SuppressKeyPress = true;   // stop Enter from opening dropdown
//                    txtQty.Focus();
//                    txtQty.SelectAll();
//                }
//                // Left from Unit → back to product search
//                else if (e.KeyCode == Keys.Left)
//                {
//                    e.Handled = true;
//                    txtProductSearch.Focus();
//                    txtProductSearch.SelectAll();
//                }
//            };

//            // ── txtQty: Right/Enter → txtItemPrice,  Left → cmbUnit ──────────
//            txtQty.KeyDown += (s, e) =>
//            {
//                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
//                {
//                    e.Handled = true;
//                    e.SuppressKeyPress = true;
//                    txtItemPrice.Focus();
//                    txtItemPrice.SelectAll();
//                }
//                else if (e.KeyCode == Keys.Left)
//                {
//                    e.Handled = true;
//                    cmbUnit.Focus();
//                }
//            };

//            // ── txtItemPrice: Right/Enter → txtItemTotal,  Left → txtQty ─────
//            txtItemPrice.KeyDown += (s, e) =>
//            {
//                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
//                {
//                    e.Handled = true;
//                    e.SuppressKeyPress = true;
//                    txtItemTotal.Focus();
//                    txtItemTotal.SelectAll();
//                }
//                else if (e.KeyCode == Keys.Left)
//                {
//                    e.Handled = true;
//                    txtQty.Focus();
//                    txtQty.SelectAll();
//                }
//            };

//            // ── txtItemTotal: Right/Enter → btnAddItem,  Left → txtItemPrice ─
//            // txtItemTotal is read-only (calculated) so Enter here fires Add Item
//            txtItemTotal.KeyDown += (s, e) =>
//            {
//                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
//                {
//                    e.Handled = true;
//                    e.SuppressKeyPress = true;
//                    // Trigger Add Item directly — no need to tab to button
//                    BtnAddItem_Click(btnAddItem, EventArgs.Empty);
//                }
//                else if (e.KeyCode == Keys.Left)
//                {
//                    e.Handled = true;
//                    txtItemPrice.Focus();
//                    txtItemPrice.SelectAll();
//                }
//            };

//            // ── btnAddItem: Enter already fires Click by default ─────────────
//            // Left → go back to txtItemTotal
//            btnAddItem.KeyDown += (s, e) =>
//            {
//                if (e.KeyCode == Keys.Left)
//                {
//                    e.Handled = true;
//                    txtItemTotal.Focus();
//                    txtItemTotal.SelectAll();
//                }
//            };
//        }
//    }
//}


using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Supplier
{
    /// <summary>
    /// Records a purchase invoice from a supplier.
    ///
    /// ── ONE PURCHASE PER SUPPLIER PER DAY ────────────────────────────────────
    /// When a supplier is selected the DB is checked for an existing Purchase
    /// on the selected date. If found the invoice is loaded into the grid for
    /// editing / appending; saving will UPDATE that record. If not found a new
    /// invoice is created as normal.
    ///
    /// ── PAYMENT ──────────────────────────────────────────────────────────────
    /// Payment is NOT recorded here. Use SupplierPaymentForm.
    /// Every saved invoice starts with PaymentStatus = Pending, Balance = NetAmount.
    /// </summary>
    public partial class PurchaseForm : Form
    {
        // ── State ──────────────────────────────────────────────────────────────
        private readonly POSDbContext _db;

        private int? _selectedSupplierId;
        private string _selectedSupplierName;

        private int? _selectedProductId;
        private string _selectedProductName;
        private string _selectedProductCode;

        /// <summary>
        /// null  = new invoice mode.
        /// set   = editing an existing invoice loaded from DB.
        /// </summary>
        private int? _existingPurchaseId = null;

        private readonly Timer _supplierTimer = new Timer { Interval = 300 };
        private readonly Timer _productTimer = new Timer { Interval = 300 };

        private bool _suppressSupplierEvent;
        private bool _suppressProductEvent;
        private bool _suppressGridEvent;

        // ── Constructor ────────────────────────────────────────────────────────
        public PurchaseForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            WireEvents();
            SetupForm();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SETUP
        // ══════════════════════════════════════════════════════════════════════

        private void SetupForm()
        {
            lblHeaderDate.Text = "Date: " + DateTime.Now.ToString("dd MMM yyyy");
            lblInvoiceNo.Text = GenerateInvoiceNumber();
            dtpPurchaseDate.Value = DateTime.Now;
            LoadUnitsForProduct();   // populate dropdown on startup
        }

        private void WireEvents()
        {
            // ── Things the designer CANNOT handle ─────────────────────────────

            lstSupplierSugg.Leave += LstSupplierSugg_Leave;
            lstProductSugg.Leave += LstProductSugg_Leave;

            _supplierTimer.Tick += (s, e) => { _supplierTimer.Stop(); SearchSuppliers(txtSupplierSearch.Text.Trim()); };
            _productTimer.Tick += (s, e) => { _productTimer.Stop(); SearchProducts(txtProductSearch.Text.Trim()); };

            HoverBtn(btnAddItem, Color.FromArgb(13, 71, 161), Color.FromArgb(21, 101, 192));
            HoverBtn(btnSave, Color.FromArgb(27, 94, 32), Color.FromArgb(46, 125, 50));
            HoverBtn(btnClearAll, Color.FromArgb(183, 28, 28), Color.FromArgb(198, 40, 40));

            this.Resize += (s, e) => RepositionDropdowns();

            pnlAddProduct.Paint += pnlAddProduct_Paint;

            dtpPurchaseDate.ValueChanged += DtpPurchaseDate_ValueChanged;

            WireKeyboardNavigation();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  ONE PURCHASE PER SUPPLIER PER DAY
        // ══════════════════════════════════════════════════════════════════════

        private void CheckForExistingPurchase()
        {
            if (!_selectedSupplierId.HasValue) return;

            DateTime date = dtpPurchaseDate.Value.Date;

            try
            {
                var existing = _db.Purchases
                    .Where(p => p.SupplierId == _selectedSupplierId.Value
                             && !p.IsDeleted
                             && System.Data.Entity.DbFunctions.TruncateTime(p.PurchaseDate) == date)
                    .FirstOrDefault();

                if (existing != null)
                {

                    LoadExistingPurchase(existing);
                    CheckExistingOrderPaidStatus();
                }
                else if (_existingPurchaseId.HasValue)
                    SwitchToNewMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking existing purchase:\n" + ex.Message);
            }
        }

        private void LoadExistingPurchase(Purchase purchase)
        {
            _existingPurchaseId = purchase.Id;

            lblInvoiceNo.Text = purchase.InvoiceNumber;
            txtReferenceNo.Text = purchase.SupplierReferenceNo ?? string.Empty;
            dtpPurchaseDate.Value = purchase.PurchaseDate;
            txtDiscount.Text = purchase.Discount.ToString("N2");
            txtNotes.Text = purchase.Notes ?? string.Empty;

            dgvItems.Rows.Clear();

            var full = _db.Purchases
                .Include("PurchaseItems")
                .FirstOrDefault(p => p.Id == purchase.Id);

            if (full?.PurchaseItems != null)
            {
                foreach (var item in full.PurchaseItems.Where(i => !i.IsDeleted))
                {
                    var product = _db.Products.FirstOrDefault(p => p.Id == item.ProductId);
                    string pName = product?.ProductEnglishName ?? $"Product #{item.ProductId}";
                    string pCode = product?.SearchByProductCode ?? "";

                    int idx = dgvItems.Rows.Add();
                    var row = dgvItems.Rows[idx];
                    row.Tag = item.ProductId;

                    row.Cells["colSrNo"].Tag = item.Id;
                    row.Cells["colSrNo"].Value = idx + 1;
                    row.Cells["colProductCode"].Value = pCode;
                    row.Cells["colProductName"].Value = pName;
                    row.Cells["colUnit"].Value = item.ProductUnitId?.ToString();
                    row.Cells["colUnit"].Tag = item.ProductUnitId;
                    row.Cells["colQty"].Value = item.Quantity;
                    row.Cells["colPrice"].Value = item.PurchasePrice;
                    row.Cells["colTotal"].Value = item.TotalPrice;
                }
            }

            UpdateItemCount();
            RefreshTotals();
            SetEditModeBanner(true);
        }

        private void CheckExistingOrderPaidStatus()
        {
            if (!_existingPurchaseId.HasValue)
            {
               // OrderStatusInfoLbl.Text = string.Empty;
                return;
            }
            using (var context = new POSDbContext())
            {
                decimal data = context.Purchases.Where(s => s.Id.Equals(_existingPurchaseId.Value))
                    .FirstOrDefault().TotalPaid;
                lblGridTitle.Text = string.Concat(lblGridTitle.Text, data >= 1 ? $"- You have already paid: Rs. {data.ToString("F0")} againt this order":string.Empty);
            }
        }

        private void SwitchToNewMode()
        {
            _existingPurchaseId = null;
            lblInvoiceNo.Text = GenerateInvoiceNumber();
            dgvItems.Rows.Clear();
            UpdateItemCount();
            RefreshTotals();
            SetEditModeBanner(false);
        }

        private void SetEditModeBanner(bool isEdit)
        {
            if (isEdit)
            {
                lblGridTitle.Text = $"  Purchase Items  ✏  EDITING EXISTING — {lblInvoiceNo.Text}";
                lblGridTitle.BackColor = Color.FromArgb(230, 81, 0);
                btnSave.Text = "Update Purchase";
                btnSave.BackColor = Color.FromArgb(230, 81, 0);
            }
            else
            {
                lblGridTitle.Text = "  Purchase Items";
                lblGridTitle.BackColor = Color.FromArgb(21, 101, 192);
                btnSave.Text = "Save Purchase";
                btnSave.BackColor = Color.FromArgb(46, 125, 50);
            }
        }

        private void DtpPurchaseDate_ValueChanged(object sender, EventArgs e)
        {
            CheckForExistingPurchase();
            CheckExistingOrderPaidStatus();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SUPPLIER SEARCH
        // ══════════════════════════════════════════════════════════════════════

        private void TxtSupplierSearch_TextChanged(object sender, EventArgs e)
        {
            if (_suppressSupplierEvent) return;
            if (_selectedSupplierId.HasValue) ClearSupplierSelection(false);
            _supplierTimer.Stop();
            _supplierTimer.Start();
        }

        private void SearchSuppliers(string q)
        {
            if (q.Length < 1) { Hide(lstSupplierSugg); return; }
            try
            {
                var list = _db.Suppliers
                    .Where(s => !s.IsDeleted &&
                               (s.SupplierName.Contains(q) ||
                                s.ShopName.Contains(q) ||
                                s.ContactNo.Contains(q)))
                    .OrderBy(s => s.SupplierName)
                    .Take(8).ToList();

                lstSupplierSugg.DataSource = list.Count > 0 ? (object)list : null;
                lstSupplierSugg.DisplayMember = "SupplierName";
                lstSupplierSugg.ValueMember = "Id";
                Show(lstSupplierSugg, txtSupplierSearch, list.Count);
            }
            catch (Exception ex) { MessageBox.Show("Supplier search: " + ex.Message); }
        }

        private void TxtSupplierSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (!lstSupplierSugg.Visible) return;
            if (e.KeyCode == Keys.Down)
            {
                lstSupplierSugg.Focus();
                if (lstSupplierSugg.Items.Count > 0) lstSupplierSugg.SelectedIndex = 0;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape) { Hide(lstSupplierSugg); e.Handled = true; }
        }

        private void LstSupplierSugg_MouseClick(object sender, MouseEventArgs e) => SelectSupplier();
        private void LstSupplierSugg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { SelectSupplier(); e.Handled = true; }
            else if (e.KeyCode == Keys.Escape) { Hide(lstSupplierSugg); txtSupplierSearch.Focus(); e.Handled = true; }
        }

        private void TxtSupplierSearch_Leave(object sender, EventArgs e) { if (!lstSupplierSugg.Focused) Hide(lstSupplierSugg); }
        private void LstSupplierSugg_Leave(object sender, EventArgs e) { if (!txtSupplierSearch.Focused) Hide(lstSupplierSugg); }

        private void SelectSupplier()
        {
            if (!(lstSupplierSugg.SelectedItem is Models.Supplier s)) return;

            _selectedSupplierId = s.Id;
            _selectedSupplierName = $"{s.SupplierName}  —  {s.ShopName}";

            _suppressSupplierEvent = true;
            txtSupplierSearch.Text = string.Empty;
            _suppressSupplierEvent = false;

            lblSelectedSupplier.Text = _selectedSupplierName;
            pnlSupplierBadge.Visible = true;
            Hide(lstSupplierSugg);

            CheckForExistingPurchase();

            txtReferenceNo.Focus();
        }

        private void BtnClearSupplier_Click(object sender, EventArgs e) => ClearSupplierSelection(true);

        private void ClearSupplierSelection(bool focusSearch)
        {
            _selectedSupplierId = null;
            _selectedSupplierName = null;
            pnlSupplierBadge.Visible = false;

            if (_existingPurchaseId.HasValue) SwitchToNewMode();

            if (focusSearch) txtSupplierSearch.Focus();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  PRODUCT SEARCH
        // ══════════════════════════════════════════════════════════════════════

        private void TxtProductSearch_TextChanged(object sender, EventArgs e)
        {
            if (_suppressProductEvent) return;
            if (_selectedProductId.HasValue) ClearProductSelection();
            _productTimer.Stop();
            _productTimer.Start();
        }

        private void SearchProducts(string q)
        {
            if (q.Length < 1) { Hide(lstProductSugg); return; }
            try
            {
                var data = _db.Products.AsQueryable();
                var words = q.ToLower().Split(' ');
                foreach (var word in words)
                    data = data.Where(p => p.ProductEnglishName.Contains(word)
                                        || p.Id.ToString().Contains(word)
                                        || p.SearchByProductCode.Contains(word));

                var list = data.OrderBy(p => p.ProductEnglishName).Take(20).ToList();

                lstProductSugg.DataSource = list.Count > 0 ? (object)list : null;
                lstProductSugg.DisplayMember = "ProductEnglishName";
                lstProductSugg.ValueMember = "Id";
                Show(lstProductSugg, txtProductSearch, list.Count);
            }
            catch (Exception ex) { MessageBox.Show("Product search: " + ex.Message); }
        }

        private void TxtProductSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (!lstProductSugg.Visible) return;
            if (e.KeyCode == Keys.Down)
            {
                lstProductSugg.Focus();
                if (lstProductSugg.Items.Count > 0) lstProductSugg.SelectedIndex = 0;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape) { Hide(lstProductSugg); e.Handled = true; }
        }

        private void LstProductSugg_MouseClick(object sender, MouseEventArgs e) => SelectProduct();
        private void LstProductSugg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { SelectProduct(); e.Handled = true; }
            else if (e.KeyCode == Keys.Escape) { Hide(lstProductSugg); txtProductSearch.Focus(); e.Handled = true; }
        }

        private void TxtProductSearch_Leave(object sender, EventArgs e) { if (!lstProductSugg.Focused) Hide(lstProductSugg); }
        private void LstProductSugg_Leave(object sender, EventArgs e) { if (!txtProductSearch.Focused) Hide(lstProductSugg); }

        private void SelectProduct()
        {
            if (!(lstProductSugg.SelectedItem is Models.Product p)) return;

            _selectedProductId = p.Id;
            _selectedProductName = p.ProductEnglishName;
            _selectedProductCode = p.SearchByProductCode;

            _suppressProductEvent = true;
            txtProductSearch.Text = p.ProductEnglishName;
            _suppressProductEvent = false;

            Hide(lstProductSugg);

            LoadUnitsForProduct();

            if (!string.IsNullOrEmpty(p.PurchasePrice) &&
                decimal.TryParse(p.PurchasePrice, out decimal price))
                txtItemPrice.Text = price.ToString("N2");

            cmbUnit.Focus();
            if (cmbUnit.Items.Count > 0) cmbUnit.SelectedIndex = 0;
        }

        private void LoadUnitsForProduct()
        {
            try
            {
                var units = _db.ProductUnits
                    .OrderBy(u => u.Name)
                    .ToList();

                cmbUnit.DataSource = null;
                cmbUnit.DataSource = units;
                cmbUnit.DisplayMember = "Name";
                cmbUnit.ValueMember = "Id";

                if (cmbUnit.Items.Count > 0) cmbUnit.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading units: " + ex.Message);
            }
        }

        private void ClearProductSelection()
        {
            _selectedProductId = null;
            _selectedProductName = null;
            _selectedProductCode = null;
            txtItemPrice.Text = "0.00";
            txtItemTotal.Text = "0.00";
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SUGGESTION LIST — SHARED HELPERS
        // ══════════════════════════════════════════════════════════════════════

        private void Show(ListBox lst, System.Windows.Forms.Control anchor, int count)
        {
            if (count == 0) { Hide(lst); return; }
            System.Drawing.Point pt = PointToClient(anchor.Parent.PointToScreen(anchor.Location));
            lst.Location = new System.Drawing.Point(pt.X, pt.Y + anchor.Height);
            lst.Width = anchor.Width;
            lst.Height = Math.Min(count, 6) * lst.ItemHeight + 2;
            lst.BringToFront();
            lst.Visible = true;
        }

        private void Hide(ListBox lst)
        {
            lst.Visible = false;
            lst.DataSource = null;
        }

        private void RepositionDropdowns()
        {
            if (lstSupplierSugg.Visible) Show(lstSupplierSugg, txtSupplierSearch, lstSupplierSugg.Items.Count);
            if (lstProductSugg.Visible) Show(lstProductSugg, txtProductSearch, lstProductSugg.Items.Count);
        }

        private void LstSugg_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var lst = (ListBox)sender;
            var item = lst.Items[e.Index];
            bool sel = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            e.Graphics.FillRectangle(
                sel ? new SolidBrush(Color.FromArgb(227, 242, 253)) : Brushes.White,
                e.Bounds);

            if (e.Index > 0)
                e.Graphics.DrawLine(new Pen(Color.FromArgb(236, 239, 241)),
                    e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);

            var boldFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            var subFont = new Font("Segoe UI", 8.5F);
            int pad = 10;

            if (item is Models.Supplier sup)
            {
                e.Graphics.DrawString(sup.SupplierName, boldFont,
                    new SolidBrush(Color.FromArgb(26, 35, 126)), e.Bounds.Left + pad, e.Bounds.Top + 4);
                e.Graphics.DrawString($"{sup.ShopName}  ·  {sup.ContactNo}", subFont,
                    new SolidBrush(Color.FromArgb(120, 144, 156)), e.Bounds.Left + pad, e.Bounds.Top + 22);
            }
            else if (item is Models.Product prod)
            {
                e.Graphics.DrawString(prod.ProductEnglishName, boldFont,
                    new SolidBrush(Color.FromArgb(26, 35, 126)), e.Bounds.Left + pad, e.Bounds.Top + 4);
                string sub = prod.ProductUrduName ?? "";
                if (!string.IsNullOrEmpty(prod.SearchByProductCode)) sub += $"  [{prod.SearchByProductCode}]";
                e.Graphics.DrawString(sub, subFont,
                    new SolidBrush(Color.FromArgb(120, 144, 156)), e.Bounds.Left + pad, e.Bounds.Top + 22);
            }

            boldFont.Dispose();
            subFont.Dispose();
            e.DrawFocusRectangle();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  ITEM CALCULATION
        // ══════════════════════════════════════════════════════════════════════

        private void TxtCalc_TextChanged(object sender, EventArgs e)
        {
            decimal qty = D(txtQty.Text);
            decimal price = D(txtItemPrice.Text);
            txtItemTotal.Text = (qty * price).ToString("N2");
        }

        private void TxtDiscount_TextChanged(object sender, EventArgs e) => RefreshTotals();

        // ══════════════════════════════════════════════════════════════════════
        //  ADD ITEM TO GRID
        // ══════════════════════════════════════════════════════════════════════

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            if (!_selectedProductId.HasValue)
            { Error("Please select a product from the suggestions."); txtProductSearch.Focus(); return; }

            decimal qty = D(txtQty.Text);
            decimal price = D(txtItemPrice.Text);

            if (qty <= 0) { Error("Quantity must be greater than 0."); txtQty.Focus(); return; }
            if (price <= 0) { Error("Price must be greater than 0."); txtItemPrice.Focus(); return; }

            // Duplicate product → merge qty into existing row
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.Tag is int pid && pid == _selectedProductId.Value)
                {
                    decimal existing = D(row.Cells["colQty"].Value?.ToString());
                    decimal newQty = existing + qty;
                    row.Cells["colQty"].Value = newQty;
                    row.Cells["colTotal"].Value = newQty * D(row.Cells["colPrice"].Value?.ToString());
                    RefreshTotals();
                    ResetProductRow();
                    return;
                }
            }

            int idx = dgvItems.Rows.Add();
            var r = dgvItems.Rows[idx];
            r.Tag = _selectedProductId.Value;

            r.Cells["colSrNo"].Tag = 0;
            r.Cells["colSrNo"].Value = dgvItems.Rows.Count;
            r.Cells["colProductCode"].Value = _selectedProductCode;
            r.Cells["colProductName"].Value = _selectedProductName;
            // Store unit Id in Tag (for DB save) and unit Name as display value
            int? selUnitId = cmbUnit.SelectedValue is int id ? id : (int?)null;
            string selUnitName = (cmbUnit.SelectedItem as ProductUnit)?.Name ?? cmbUnit.SelectedItem?.ToString() ?? "";
            r.Cells["colUnit"].Value = selUnitName;
            r.Cells["colUnit"].Tag = selUnitId;
            r.Cells["colQty"].Value = qty;
            r.Cells["colPrice"].Value = price;
            r.Cells["colTotal"].Value = qty * price;

            UpdateItemCount();
            RefreshTotals();
            ResetProductRow();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  DATAGRID EVENTS
        // ══════════════════════════════════════════════════════════════════════

        //private void DgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0) return;
        //    if (dgvItems.Columns[e.ColumnIndex].Name != "colDelete") return;

        //    if (MessageBox.Show("Remove this item?", "Confirm",
        //        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        //        return;

        //    var row = dgvItems.Rows[e.RowIndex];

        //    int purchaseItemId = row.Cells["colSrNo"].Tag is int t ? t : 0;
        //    if (purchaseItemId > 0 && _existingPurchaseId.HasValue)
        //    {
        //        try
        //        {
        //            var dbItem = _db.PurchaseItems.FirstOrDefault(i => i.Id == purchaseItemId);
        //            if (dbItem != null)
        //            {
        //                dbItem.IsDeleted = true;
        //                _db.SaveChanges();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Could not remove item from DB:\n" + ex.Message);
        //            return;
        //        }
        //    }

        //    dgvItems.Rows.RemoveAt(e.RowIndex);
        //    Renumber();
        //    UpdateItemCount();
        //    RefreshTotals();
        //}


        private void DgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvItems.Columns[e.ColumnIndex].Name != "colDelete") return;

            if (MessageBox.Show("Remove this item?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var row = dgvItems.Rows[e.RowIndex];

            string prod_code = row.Cells["colProductCode"].Value.ToString();

            string prod_name = row.Cells["colProductName"].Value.ToString();

            // If this row came from the DB, soft-delete it immediately so the
            // change is persisted even if the user cancels without clicking Save.
            int purchaseItemId = row.Cells["colSrNo"].Value is int t ? t : 0;
            if (purchaseItemId > 0 && _existingPurchaseId.HasValue)
            {
                if (dgvItems.Rows.Count == 1 & (_db.SupplierPaymentDetails
                     .Any(spd => spd.PurchaseId == _existingPurchaseId.Value)))
                {
                    MessageBox.Show(
                    "Cannot delete this purchase!\n\n" +
                    "Reason: Payments have been made against this order.\n\n" +
                    "Action Required:\n" +
                    "1. First reverse/refund the payments, OR\n" +
                    "2. Create a Purchase Return/Credit Note instead\n" +
                    "3. Contact accounts department for assistance",
                    "Deletion Restricted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var pr = _db.Products.FirstOrDefault(s => s.SearchByProductCode == prod_code && s.ProductEnglishName == prod_name).Id;

                    if (pr > 0)
                    {
                        var dbItem = _db.PurchaseItems.FirstOrDefault(i => i.ProductId == pr && i.IsDeleted == false);
                        if (dbItem != null)
                        {
                            dbItem.IsDeleted = true;
                            _db.SaveChanges();
                            if (dgvItems.Rows.Count == 1)
                            {
                                using (var transaction = _db.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        var purchaseId = _existingPurchaseId.Value;

                                        // 1. Delete SupplierPaymentDetails
                                        _db.Database.ExecuteSqlCommand(
                                            "DELETE FROM SupplierPaymentDetails WHERE PurchaseId = @purchaseId",
                                            new SqlParameter("@purchaseId", purchaseId));

                                        // 2. Delete PurchaseItems
                                        _db.Database.ExecuteSqlCommand(
                                            "DELETE FROM PurchaseItems WHERE PurchaseId = @purchaseId",
                                            new SqlParameter("@purchaseId", purchaseId));

                                        // 3. Delete Purchase
                                        _db.Database.ExecuteSqlCommand(
                                            "DELETE FROM Purchases WHERE Id = @purchaseId",
                                            new SqlParameter("@purchaseId", purchaseId));

                                        // 4. Delete orphaned SupplierPayments
                                        _db.Database.ExecuteSqlCommand(@"
                                            DELETE FROM SupplierPayments 
                                            WHERE Id IN (
                                                SELECT DISTINCT pd.SupplierPaymentId
                                                FROM SupplierPaymentDetails pd
                                                LEFT JOIN SupplierPaymentDetails pd2 ON pd.SupplierPaymentId = pd2.SupplierPaymentId 
                                                    AND pd2.PurchaseId IS NOT NULL
                                                WHERE pd.PurchaseId = @purchaseId
                                                AND pd2.SupplierPaymentId IS NULL
                                            )",
                                            new SqlParameter("@purchaseId", purchaseId));

                                        transaction.Commit();
                                        MessageBox.Show("Purchase and associated records deleted successfully.");
                                        this.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show("Error deleting purchase: " + ex.Message);
                                    }
                                }
                            }
                            dgvItems.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not remove item from DB:\n" + ex.Message);
                    return;
                }
            }
            else
            {
                dgvItems.Rows.RemoveAt(e.RowIndex);
            }


            Renumber();
            UpdateItemCount();
            RefreshTotals();
        }


        private void DgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _suppressGridEvent) return;
            string col = dgvItems.Columns[e.ColumnIndex].Name;
            if (col != "colQty" && col != "colPrice") return;

            _suppressGridEvent = true;
            decimal qty = D(dgvItems.Rows[e.RowIndex].Cells["colQty"].Value?.ToString());
            decimal price = D(dgvItems.Rows[e.RowIndex].Cells["colPrice"].Value?.ToString());
            dgvItems.Rows[e.RowIndex].Cells["colTotal"].Value = qty * price;
            _suppressGridEvent = false;
            RefreshTotals();
        }

        private void DgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= CellNum_KeyPress;
            e.Control.KeyPress -= CellDec_KeyPress;
            string col = dgvItems.Columns[dgvItems.CurrentCell.ColumnIndex].Name;
            if (col == "colQty") e.Control.KeyPress += CellNum_KeyPress;
            if (col == "colPrice") e.Control.KeyPress += CellDec_KeyPress;
        }

        private void CellNum_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b') e.Handled = true; }

        private void CellDec_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b') { e.Handled = true; return; }
            if (e.KeyChar == '.' && tb?.Text.Contains('.') == true) e.Handled = true;
        }

        private void Renumber()
        {
            for (int i = 0; i < dgvItems.Rows.Count; i++)
                dgvItems.Rows[i].Cells["colSrNo"].Value = i + 1;
        }

        private void UpdateItemCount()
        {
            int n = dgvItems.Rows.Count;
            lblItemCount.Text = $"{n} item{(n != 1 ? "s" : "")}";
        }

        // ══════════════════════════════════════════════════════════════════════
        //  TOTALS
        // ══════════════════════════════════════════════════════════════════════

        private void RefreshTotals()
        {
            //decimal subtotal = 0;
            //foreach (DataGridViewRow row in dgvItems.Rows)
            //    subtotal += D(row.Cells["colTotal"].Value?.ToString());

            //decimal discount = D(txtDiscount.Text);
            //decimal net = Math.Max(0, subtotal - discount);

            //lblSubtotalVal.Text = subtotal.ToString("N2");
            //lblNetVal.Text = net.ToString("N2");

            //lblStatusInfo.Text = net <= 0 ? "—" : "⏳ PENDING  (Pay later via Supplier Payment)";
            //lblStatusInfo.ForeColor = Color.FromArgb(245, 124, 0);

            int totalItems = 0;
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.Cells[1].Value == null) continue;
                totalItems++;
                if (row.Cells["colTotal"].Value != null)
                    subtotal += Convert.ToDecimal(row.Cells["colTotal"].Value);
            }
            //foreach (DataGridViewRow row in dgvItems.Rows)
            //    subtotal += D(row.Cells["colTotal"].Value?.ToString());

            decimal discount = D(txtDiscount.Text);
            decimal net = Math.Max(0, subtotal - discount);

            lblSubtotalVal.Text = subtotal.ToString("N2");
            lblNetVal.Text = net.ToString("N2");
            TotalAmountLbl.Text = subtotal.ToString("N0");
            lblStatusInfo.Text = net <= 0 ? "—" : "⏳ PENDING  (Pay later via Supplier Payment)";
            lblStatusInfo.ForeColor = Color.FromArgb(245, 124, 0);
            TotalItemLbl.Text = totalItems.ToString();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SAVE / UPDATE PURCHASE
        // ══════════════════════════════════════════════════════════════════════

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!_selectedSupplierId.HasValue)
            { Error("Please select a supplier."); txtSupplierSearch.Focus(); return; }

            if (dgvItems.Rows.Count == 0)
            { Error("Please add at least one product."); txtProductSearch.Focus(); return; }

            decimal subtotal = 0;
            foreach (DataGridViewRow row in dgvItems.Rows)
                subtotal += D(row.Cells["colTotal"].Value?.ToString());

            decimal discount = D(txtDiscount.Text);
            decimal net = Math.Max(0, subtotal - discount);

            if (_existingPurchaseId.HasValue)
                UpdateExistingPurchase(net, subtotal, discount);
            else
                SaveNewPurchase(net, subtotal, discount);
        }

        // ── NEW PURCHASE ───────────────────────────────────────────────────────

        private void SaveNewPurchase(decimal net, decimal subtotal, decimal discount)
        {
            var purchase = new Purchase
            {
                InvoiceNumber = lblInvoiceNo.Text,
                SupplierReferenceNo = txtReferenceNo.Text.Trim(),
                PurchaseDate = dtpPurchaseDate.Value,
                SupplierId = _selectedSupplierId.Value,
                TotalAmount = subtotal,
                Discount = discount,
                NetAmount = net,
                TotalPaid = 0,
                Balance = net,
                PaymentStatus = PurchasePaymentStatus.Pending,
                Notes = txtNotes.Text.Trim(),
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                purchase.PurchaseItems.Add(new PurchaseItem
                {
                    ProductId = (int)row.Tag,
                    ProductUnitId = row.Cells["colUnit"].Tag is int uid ? uid : (int?)null,
                    Quantity = D(row.Cells["colQty"].Value?.ToString()),
                    PurchasePrice = D(row.Cells["colPrice"].Value?.ToString()),
                    TotalPrice = D(row.Cells["colTotal"].Value?.ToString())
                });
            }

            try
            {
                _db.Purchases.Add(purchase);
                _db.SaveChanges();

                MessageBox.Show(
                    $"✔  Purchase saved — PENDING payment\n\n" +
                    $"Invoice   :  {purchase.InvoiceNumber}\n" +
                    $"Supplier  :  {_selectedSupplierName}\n" +
                    $"Items     :  {purchase.PurchaseItems.Count}\n" +
                    $"Net Amount:  Rs. {net:N2}\n\n" +
                    "Open Supplier Payment to record payment for this invoice.",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── UPDATE EXISTING PURCHASE ───────────────────────────────────────────

        private void UpdateExistingPurchase(decimal net, decimal subtotal, decimal discount)
        {
            try
            {
                var purchase = _db.Purchases
                    .Include("PurchaseItems")
                    .FirstOrDefault(p => p.Id == _existingPurchaseId.Value);

                if (purchase == null)
                {
                    Error("The existing purchase record could not be found.");
                    return;
                }

                purchase.SupplierReferenceNo = txtReferenceNo.Text.Trim();
                purchase.TotalAmount = subtotal;
                purchase.Discount = discount;
                purchase.NetAmount = net;
                purchase.Notes = txtNotes.Text.Trim();
                purchase.UpdatedAt = DateTime.UtcNow;

                if (purchase.PaymentStatus == PurchasePaymentStatus.Pending)
                    purchase.Balance = net;
                else
                    purchase.Balance = Math.Max(0, net - purchase.TotalPaid);

                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    int productId = (int)row.Tag;
                    //int purchaseItemId = row.Cells["colSrNo"].Tag is int t ? t : 0;
                    int purchaseItemId = row.Cells["colSrNo"].Value is int t ? t : 0;
                    decimal qty = D(row.Cells["colQty"].Value?.ToString());
                    decimal price = D(row.Cells["colPrice"].Value?.ToString());
                    decimal total = D(row.Cells["colTotal"].Value?.ToString());
                    int? unitId = row.Cells["colUnit"].Tag is int uid ? uid : (int?)null;


                    string prod_code = row.Cells["colProductCode"].Value.ToString();

                    string prod_name = row.Cells["colProductName"].Value.ToString();

                    var pr = _db.Products.FirstOrDefault(s => s.SearchByProductCode == prod_code && s.ProductEnglishName == prod_name).Id;

                    //if (purchaseItemId > 0)
                    //{
                    //    var existing = purchase.PurchaseItems
                    //        .FirstOrDefault(i => i.Id == purchaseItemId && !i.IsDeleted);

                    //    if (existing != null)
                    //    {
                    //        existing.Quantity = qty;
                    //        existing.PurchasePrice = price;
                    //        existing.TotalPrice = total;
                    //        existing.ProductUnitId = unitId;
                    //    }
                    //}
                    //else
                    //{
                    //    purchase.PurchaseItems.Add(new PurchaseItem
                    //    {
                    //        PurchaseId = purchase.Id,
                    //        ProductId = productId,
                    //        ProductUnitId = unitId,
                    //        Quantity = qty,
                    //        PurchasePrice = price,
                    //        TotalPrice = total
                    //    });
                    //}

                    if (pr > 0)
                    {

                        // Row came from DB → UPDATE the existing PurchaseItem
                        var existing = purchase.PurchaseItems
                            .FirstOrDefault(i => i.ProductId == pr && !i.IsDeleted);

                        if (existing != null)
                        {
                            existing.Quantity = qty;
                            existing.PurchasePrice = price;
                            existing.TotalPrice = total;
                            existing.ProductUnitId = unitId;
                        }
                        else
                        {
                            // Row is new this session → INSERT a new PurchaseItem
                            purchase.PurchaseItems.Add(new PurchaseItem
                            {
                                PurchaseId = purchase.Id,
                                ProductId = productId,
                                ProductUnitId = unitId,
                                Quantity = qty,
                                PurchasePrice = price,
                                TotalPrice = total
                            });
                        }
                    }
                    else
                    {
                        // Row is new this session → INSERT a new PurchaseItem
                        purchase.PurchaseItems.Add(new PurchaseItem
                        {
                            PurchaseId = purchase.Id,
                            ProductId = productId,
                            ProductUnitId = unitId,
                            Quantity = qty,
                            PurchasePrice = price,
                            TotalPrice = total
                        });
                    }
                }

                _db.SaveChanges();

                MessageBox.Show(
                    $"✔  Purchase updated!\n\n" +
                    $"Invoice   :  {purchase.InvoiceNumber}\n" +
                    $"Supplier  :  {_selectedSupplierName}\n" +
                    $"Items     :  {dgvItems.Rows.Count}\n" +
                    $"Net Amount:  Rs. {net:N2}",
                    "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  CLEAR / RESET
        // ══════════════════════════════════════════════════════════════════════

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all and reset the form?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                ResetForm();
        }

        private void ResetForm()
        {
            _existingPurchaseId = null;
            SetEditModeBanner(false);

            ClearSupplierSelection(false);
            _suppressSupplierEvent = true;
            txtSupplierSearch.Text = string.Empty;
            _suppressSupplierEvent = false;

            ResetProductRow();
            dgvItems.Rows.Clear();
            UpdateItemCount();

            txtReferenceNo.Text = string.Empty;
            txtDiscount.Text = "0.00";
            txtNotes.Text = string.Empty;
            dtpPurchaseDate.Value = DateTime.Now;

            RefreshTotals();
            lblInvoiceNo.Text = GenerateInvoiceNumber();
            txtSupplierSearch.Focus();
        }

        private void ResetProductRow()
        {
            _suppressProductEvent = true;
            txtProductSearch.Text = string.Empty;
            _suppressProductEvent = false;

            ClearProductSelection();
            txtQty.Text = "1";
            txtItemPrice.Text = "0.00";
            txtItemTotal.Text = "0.00";
            txtProductSearch.Focus();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  KEYBOARD NAVIGATION  (Unit → Qty → Price → Total → Add Item)
        //  Right / Enter = forward,  Left = back
        // ══════════════════════════════════════════════════════════════════════

        private void WireKeyboardNavigation()
        {
            // cmbUnit: Right/Enter → txtQty,  Left → txtProductSearch
            cmbUnit.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
                {
                    e.Handled = true; e.SuppressKeyPress = true;
                    txtQty.Focus(); txtQty.SelectAll();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    e.Handled = true;
                    txtProductSearch.Focus(); txtProductSearch.SelectAll();
                }
            };

            // txtQty: Right/Enter → txtItemPrice,  Left → cmbUnit
            txtQty.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
                {
                    e.Handled = true; e.SuppressKeyPress = true;
                    txtItemPrice.Focus(); txtItemPrice.SelectAll();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    e.Handled = true; cmbUnit.Focus();
                }
            };

            // txtItemPrice: Right/Enter → txtItemTotal,  Left → txtQty
            txtItemPrice.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
                {
                    e.Handled = true; e.SuppressKeyPress = true;
                    txtItemTotal.Focus(); txtItemTotal.SelectAll();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    e.Handled = true; txtQty.Focus(); txtQty.SelectAll();
                }
            };

            // txtItemTotal: Right/Enter → fires Add Item,  Left → txtItemPrice
            txtItemTotal.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
                {
                    e.Handled = true; e.SuppressKeyPress = true;
                    BtnAddItem_Click(btnAddItem, EventArgs.Empty);
                }
                else if (e.KeyCode == Keys.Left)
                {
                    e.Handled = true; txtItemPrice.Focus(); txtItemPrice.SelectAll();
                }
            };

            // btnAddItem: Left → txtItemTotal
            btnAddItem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Left)
                {
                    e.Handled = true; txtItemTotal.Focus(); txtItemTotal.SelectAll();
                }
            };
        }

        // ══════════════════════════════════════════════════════════════════════
        //  HELPERS
        // ══════════════════════════════════════════════════════════════════════

        private string GenerateInvoiceNumber()
        {
            try
            {
                int last = _db.Purchases.Any() ? _db.Purchases.Max(p => p.Id) : 0;
                return $"INV-{(last + 1):D5}";
            }
            catch { return $"INV-{DateTime.Now:yyyyMMddHHmm}"; }
        }

        private static decimal D(string s) => decimal.TryParse(s, out decimal v) ? v : 0m;

        private static void Error(string msg)
            => MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private static void HoverBtn(Button b, Color hover, Color normal)
        {
            b.MouseEnter += (s, e) => b.BackColor = hover;
            b.MouseLeave += (s, e) => b.BackColor = normal;
        }

        private void NumericOnly(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b') e.Handled = true; }

        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b') { e.Handled = true; return; }
            if (e.KeyChar == '.' && tb?.Text.Contains('.') == true) e.Handled = true;
        }

        private void pnlAddProduct_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(207, 216, 220));
            e.Graphics.DrawLine(pen, 0, 0, pnlAddProduct.Width, 0);
            e.Graphics.DrawLine(pen, 0, pnlAddProduct.Height - 1, pnlAddProduct.Width, pnlAddProduct.Height - 1);
            pen.Dispose();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _supplierTimer.Dispose();
            _productTimer.Dispose();
            _db.Dispose();
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(_selectedProductId.HasValue)
            //{
            //    int? selUnitId = cmbUnit.SelectedValue is int id ? id : (int?)null;
            //    string selUnitName = (cmbUnit.SelectedItem as ProductUnit)?.Name ?? cmbUnit.SelectedItem?.ToString() ?? "";

            //    var data = _db.ProductPrices.Where(s => s.ProductId == _selectedProductId.Value && s.Prod_Unit_TypeId == selUnitId).Select(s => s.Price).FirstOrDefault();

            //    txtItemPrice.Text = data.ToString();
            //}
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S: btnSave.PerformClick(); return true;
                case Keys.Control | Keys.N: btnClearAll.PerformClick(); return true;
                case Keys.Escape:
                    txtProductSearch.Focus(); txtProductSearch.SelectAll(); return true;
                case Keys.F1:
                    txtSupplierSearch.Focus(); txtSupplierSearch.SelectAll(); return true;
                case Keys.Alt | Keys.F4: this.Close(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender is TextBox textBox)
                {
                    txtDiscount.SelectAll();
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
                return;
            }
        }
    }
}