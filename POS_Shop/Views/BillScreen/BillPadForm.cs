using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using ExcelDataReader;
using POS_Shop.Constants;
using POS_Shop.DTOs.Order;
using POS_Shop.DTOs.Product;
using POS_Shop.Helpers;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Models.AuthModel;
using POS_Shop.Models.LoanModelsV1;
using POS_Shop.Repositories;
using POS_Shop.Views.BankingQR;
using POS_Shop.Views.Controllers.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Order = POS_Shop.Models.Order;

namespace POS_Shop.Views.BillScreen
{
    public partial class BillPadForm : Form
    {
        // ── Selection / cart state ─────────────────────────────────────────────────
        private string PId { get; set; } = string.Empty;
        private string customerId { get; set; } = string.Empty;
        private string prod_U_Name { get; set; } = string.Empty;
        public bool isTempSaved { get; set; } = false;
        public bool isPaid { get; set; } = false;


        private bool _isAdvanceBalance;


        private decimal _customerAdvanceBalance = 0; // < 0 means advance credit available
        private decimal _advanceApplied = 0;

        // ── Debounce timers ────────────────────────────────────────────────────────
        private System.Windows.Forms.Timer _productDebounceTimer;
        private System.Windows.Forms.Timer _customerDebounceTimer;

        // ── Cancellation tokens (prevent stale search results) ─────────────────────
        private CancellationTokenSource _productSearchCts;
        private CancellationTokenSource _customerSearchCts;

        // ── UI update guard flags ──────────────────────────────────────────────────
        private bool _isUpdatingText = false;       // prevents TextChange re-triggering search
        private bool _isLoadingPrices = false;      // prevents dropdown firing extra DB call
        private string _lastSelectedProductText = string.Empty;

        // ── Column name constants (avoid magic strings everywhere) ─────────────────
        private static class Col
        {
            public const string Delete = "Delete";
            public const string Amount = "Amount";
            public const string SalePrice = "SalePrice";
            public const string UrduName = "Urdu Name";
            public const string ProductType = "ProductType";
            public const string Qty = "Qty";
            public const string ProductId = "ProductId";
            public const string Detail = "ProductDetail";
        }

        // ══════════════════════════════════════════════════════════════════════════
        // CONSTRUCTOR
        // ══════════════════════════════════════════════════════════════════════════
        public BillPadForm()
        {
            InitializeComponent();

            CustomerIdLbl.Text = string.Empty;
            CustomerNameTxt.Text = string.Empty;
            PreviousOrderIdLbl.Text = string.Empty;

            string invRef = TextFormatHelper.GetPrefix(Properties.Settings.Default.UserName);
            InvoiceNoLbl.Text = invRef + DateTime.Now.ToString("ddMMyy-HHmmss");

            this.Shown += (s, e) => ProductEngNameTxt.Focus();

            CustomerListDataGrid.BringToFront();
            SetItemGridView();

            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;

            // Role-based tab visibility
            string savedRole = Properties.Settings.Default.UserRole?.ToString() ?? string.Empty;
            if (!savedRole.Equals(AuthUserRole.SuperAdmin.ToString(), StringComparison.OrdinalIgnoreCase))
                InvoicePageTabControl.TabPages.Remove(TruncateTableTab);

            InitializeProductUnitsDropdown();
            InitializeDebounceTimers();
        }

        // ══════════════════════════════════════════════════════════════════════════
        // INITIALIZATION
        // ══════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Sets up debounce timers for product and customer search.
        /// Prevents a DB call on every single keystroke.
        /// </summary>
        private void InitializeDebounceTimers()
        {
            // Product search — 250ms delay
            _productDebounceTimer = new System.Windows.Forms.Timer { Interval = 250 };
            _productDebounceTimer.Tick += async (s, e) =>
            {
                _productDebounceTimer.Stop();
                await TriggerProductSearchAsync(ProductEngNameTxt.Text);
            };

            // Customer search — 300ms delay
            _customerDebounceTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _customerDebounceTimer.Tick += async (s, e) =>
            {
                _customerDebounceTimer.Stop();
                await ShowCustomerSuggestionsAsync(CustomerNameTxt.Text);
            };
        }

        private void InitializeProductUnitsDropdown()
        {
            using (var context = new POSDbContext())
            {
                var productUnitRepo = new ProductUnitRepository(context);
                var productUnits = productUnitRepo.GetAll()
                    .Select(s => new ProductUnit { Id = s.Id, Name = s.Name })
                    .ToList();

                productTypeDropdown.Items.Clear();
                productTypeDropdown.DataSource = productUnits;
                productTypeDropdown.DisplayMember = "Name";
                productTypeDropdown.ValueMember = "Name";
            }
        }

        private void SetItemGridView()
        {
            //CartProductList.ColumnCount = 7;

            //CartProductList.Columns[0].Name = Col.Amount;
            //CartProductList.Columns[1].Name = Col.SalePrice;
            //CartProductList.Columns[2].Name = Col.UrduName;
            //CartProductList.Columns[3].Name = Col.ProductType;
            //CartProductList.Columns[4].Name = Col.Qty;
            //CartProductList.Columns[5].Name = Col.ProductId;
            //CartProductList.Columns[6].Name = Col.Detail;

            //CartProductList.Columns[Col.Amount].Width = 100;
            //CartProductList.Columns[Col.SalePrice].Width = 60;
            //CartProductList.Columns[Col.UrduName].Width = 190;
            //CartProductList.Columns[Col.ProductType].Width = 30;
            //CartProductList.Columns[Col.Qty].Width = 50;
            //CartProductList.Columns[Col.ProductId].Width = 50;

            //CartProductList.Columns[Col.ProductId].Visible = false;
            //CartProductList.Columns[Col.Detail].Visible = false;

            //CartProductList.Columns[Col.Amount].ReadOnly = true;
            //CartProductList.Columns[Col.UrduName].ReadOnly = true;
            //CartProductList.Columns[Col.ProductType].ReadOnly = true;

            //// Delete button column — inserted at position 0
            //var btnCol = new DataGridViewButtonColumn
            //{
            //    Name = Col.Delete,
            //    HeaderText = "Action",
            //    Text = "Delete",
            //    UseColumnTextForButtonValue = true,
            //    Width = 50
            //};
            //CartProductList.Columns.Insert(0, btnCol);


            CartProductList.SuspendLayout();
            CartProductList.ColumnCount = 7;

            var cols = CartProductList.Columns;
            cols[0].Name = "Amount"; cols[0].Width = 90;
            cols[1].Name = "SalePrice"; cols[1].Width = 60;
            cols[2].Name = "Urdu Name"; cols[2].Width = 200;
            cols[3].Name = "ProductType"; cols[3].Width = 30;
            cols[4].Name = "Qty"; cols[4].Width = 30;
            cols[5].Name = "ProductId"; cols[5].Width = 50; cols[5].Visible = false;
            cols[6].Name = "ProductDetail"; cols[6].Visible = false;

            cols["Amount"].ReadOnly = true;
            cols["Urdu Name"].ReadOnly = true;
            cols["ProductType"].ReadOnly = true;

            var btnCol = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Action",
                Text = "Delete",
                UseColumnTextForButtonValue = true,

            };
            CartProductList.Columns.Insert(0, btnCol);
            cols[0].Width = 45;
            CartProductList.ResumeLayout();
        }

        // ══════════════════════════════════════════════════════════════════════════
        // PRODUCT SEARCH  (debounced + cancellable)
        // ══════════════════════════════════════════════════════════════════════════

        private void ProductEngNameTxt_TextChange(object sender, EventArgs e)
        {
            if (_isUpdatingText) return;

            if (string.IsNullOrEmpty(ProductEngNameTxt.Text) || ProductEngNameTxt.Text.Length < 2)
            {
                _productDebounceTimer?.Stop();
                SuggestionGrid.Visible = false;
                return;
            }

            if (ProductEngNameTxt.Text == _lastSelectedProductText) return;
            if (OtherProductChk.Checked) return;

            // Restart debounce window on every keystroke
            _productDebounceTimer.Stop();
            _productDebounceTimer.Start();
        }

        /// <summary>
        /// Cancels any in-flight product search and starts a new one.
        /// </summary>
        private async Task TriggerProductSearchAsync(string searchText)
        {
            _productSearchCts?.Cancel();
            _productSearchCts?.Dispose();
            _productSearchCts = new CancellationTokenSource();
            var token = _productSearchCts.Token;

            try
            {
                var suggestions = await GetProductSuggestionsAsync(searchText, token);
                if (token.IsCancellationRequested) return;

                if (!suggestions.Any())
                {
                    SuggestionGrid.Visible = false;
                    return;
                }

                BindSuggestionGrid(suggestions);
            }
            catch (OperationCanceledException) { /* superseded — ignore */ }
            catch (Exception ex)
            {
                SuggestionGrid.Visible = false;
                LogError("TriggerProductSearchAsync", ex);
            }
        }

        private async Task<List<ProductSuggestion>> GetProductSuggestionsAsync(
            string searchText, CancellationToken ct = default)
        {
            var words = string.IsNullOrWhiteSpace(searchText)
                ? Array.Empty<string>()
                : searchText.Trim().ToLowerInvariant()
                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            using (var ctx = new POSDbContext())
            {
                // No filter
                if (words.Length == 0)
                {
                    const string sql = @"
                        SELECT TOP 100
                            Id                 AS ProductId,
                            ProductEnglishName AS ProductName,
                            ProductUrduName,
       p.ProdQtyStockUnit AS ProductType,
                            Qty,
                            PurchasePrice
                        FROM Products WITH (NOLOCK)
                        ORDER BY Id";

                    return await ctx.Database.SqlQuery<ProductSuggestion>(sql)
                                    .ToListAsync(ct);
                }

                // Single word — simple LIKE
                if (words.Length == 1)
                {
                    const string sql = @"
                        SELECT TOP 100
                            p.Id                 AS ProductId,
                            p.ProductEnglishName AS ProductName,
                            p.ProductUrduName,
       p.ProdQtyStockUnit AS ProductType,
                            p.Qty,
                            p.PurchasePrice
                        FROM Products p WITH (NOLOCK)
                        WHERE p.ProductEnglishName     LIKE @p0
                           OR p.SearchByProductCode    LIKE @p0
                           OR CAST(p.Id AS VARCHAR(20)) LIKE @p0
                        ORDER BY p.Id";

                    var param = new SqlParameter("@p0", $"%{words[0]}%");
                    return await ctx.Database.SqlQuery<ProductSuggestion>(sql, param)
                                    .ToListAsync(ct);
                }

                // Multiple words — all must match (AND logic)
                return await ExecuteMultiWordSearchAsync(words, ctx, ct);
            }
        }

        private async Task<List<ProductSuggestion>> ExecuteMultiWordSearchAsync(
            string[] words, POSDbContext context, CancellationToken ct)
        {
            var parameters = new SqlParameter[words.Length];
            var whereConditions = new string[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                string pName = $"@w{i}";
                parameters[i] = new SqlParameter(pName, $"%{words[i]}%");
                whereConditions[i] = $@"(p.ProductEnglishName     LIKE {pName}
                                      OR p.SearchByProductCode    LIKE {pName}
                                      OR CAST(p.Id AS VARCHAR(20)) LIKE {pName})";
            }

            string sql = $@"
                SELECT TOP 100
                    p.Id                 AS ProductId,
                    p.ProductEnglishName AS ProductName,
                    p.ProductUrduName,
                    p.ProdQtyStockUnit AS ProductType,
                    p.Qty,
                    p.PurchasePrice
                FROM Products p WITH (NOLOCK)
                WHERE {string.Join(" AND ", whereConditions)}
                ORDER BY p.Id";

            return await context.Database.SqlQuery<ProductSuggestion>(sql, parameters)
                                .ToListAsync(ct);
        }

        private void BindSuggestionGrid(List<ProductSuggestion> suggestions)
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("U-Name", typeof(string));
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("Type", typeof(string));

            foreach (var item in suggestions)
                dt.Rows.Add(
                    item.ProductId,
                    item.purchasePrice,
                    item.ProductName,
                    TextFormatHelper.FormatMixedText(item.ProductUrduName),
                    
                    item.Qty, item.ProductType);

            SuggestionGrid.SuspendLayout();
            SuggestionGrid.ReadOnly = true;
            SuggestionGrid.AllowUserToAddRows = false;
            SuggestionGrid.DataSource = dt;
            SuggestionGrid.Columns[0].Width = 40;
            SuggestionGrid.Columns[1].Width = 50;
            SuggestionGrid.Columns[2].Width = 200;
            SuggestionGrid.Columns[3].Width = 200;
            SuggestionGrid.Columns[4].Width = 100;
            SuggestionGrid.ResumeLayout();

            SuggestionGrid.Visible = true;
            SuggestionGrid.BringToFront();
        }

        // ══════════════════════════════════════════════════════════════════════════
        // CUSTOMER SEARCH  (debounced + cancellable)
        // ══════════════════════════════════════════════════════════════════════════

        private void CustomerNameTxt_TextChange(object sender, EventArgs e)
        {
            if (_isUpdatingText) return;
            if (string.IsNullOrEmpty(CustomerNameTxt.Text) || CustomerNameTxt.Text.Length < 2)
            {
                _customerDebounceTimer?.Stop();
                CustomerListDataGrid.Visible = false;
                return;
            }

            _customerDebounceTimer.Stop();
            _customerDebounceTimer.Start();
        }

        /// <summary>
        /// Cancels any in-flight customer search and starts a new one.
        /// </summary>
        private async Task ShowCustomerSuggestionsAsync(string searchText)
        {
            _customerSearchCts?.Cancel();
            _customerSearchCts?.Dispose();
            _customerSearchCts = new CancellationTokenSource();
            var token = _customerSearchCts.Token;

            try
            {
                using (var context = new POSDbContext())
                {
                    ICustomerRepository repo = new CustomerRepository(context);
                    var result = await repo.GetCustomerPagingListAsync(1, 100, searchText);

                    if (token.IsCancellationRequested) return; // stale — discard

                    var dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Address", typeof(string));

                    foreach (var item in result.data)
                        dt.Rows.Add(item.Id, item.CustomerName, item.CustomerAddress);

                    CustomerListDataGrid.ReadOnly = true;
                    CustomerListDataGrid.AllowUserToAddRows = false;
                    CustomerListDataGrid.DataSource = dt;
                    CustomerListDataGrid.Columns[0].Visible = false;
                    CustomerListDataGrid.BringToFront();
                    CustomerListDataGrid.Visible = true;
                }
            }
            catch (OperationCanceledException) { /* superseded — ignore */ }
            catch (Exception ex)
            {
                CustomerListDataGrid.Visible = false;
                LogError("ShowCustomerSuggestionsAsync", ex);
            }
        }

        // ══════════════════════════════════════════════════════════════════════════
        // SUGGESTION GRID — keyboard & mouse selection
        // ══════════════════════════════════════════════════════════════════════════

        private async void SuggestionGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (SuggestionGrid.CurrentRow?.Index == 0)
                {
                    ProductEngNameTxt.Focus();
                    ProductEngNameTxt.SelectAll();
                    SuggestionGrid.Visible = false;
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                e.Handled = e.SuppressKeyPress = true;
                ProductEngNameTxt.Focus();
                ProductEngNameTxt.SelectAll();
                SuggestionGrid.Visible = false;
            }
            else if (e.KeyCode == Keys.Enter && !e.Handled)
            {
                e.Handled = e.SuppressKeyPress = true;

                if (SuggestionGrid.CurrentRow != null && SuggestionGrid.CurrentRow.Index >= 0)
                    await SelectProductFromSuggestionRow(SuggestionGrid.CurrentRow);
            }
        }

        private async void SuggestionGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (SuggestionGrid.Rows.Count > 0 && SuggestionGrid.CurrentRow != null)
                await SelectProductFromSuggestionRow(SuggestionGrid.CurrentRow);
        }

        /// <summary>
        /// Shared logic for selecting a product from the suggestion grid,
        /// whether triggered by keyboard or mouse.
        /// </summary>
        private async Task SelectProductFromSuggestionRow(DataGridViewRow row)
        {
            _isUpdatingText = true;
            try
            {
                int pId = Convert.ToInt32(row.Cells[0].Value);
                string selectedText = row.Cells[2].Value?.ToString() ?? string.Empty;
                string urduName = row.Cells[3].Value?.ToString() ?? string.Empty;
                int stockQty = Convert.ToInt32(row.Cells[4].Value);

                _lastSelectedProductText = selectedText;
                ProductEngNameTxt.Text = selectedText;
                prod_U_Name = urduName;
                Prod_Qty.Text = stockQty.ToString();
                PId = pId.ToString();
                P_StockQtyTxt.Text = "1";
                SuggestionGrid.Visible = false;
                prodStockUnit.Text = row.Cells[5].Value?.ToString() ?? string.Empty;
                ProductDetailTxt.Focus();

                await ShowProductPricesAsync(pId);

                if (!string.IsNullOrEmpty(CustomerIdLbl.Text) &&
                    int.TryParse(CustomerIdLbl.Text, out int cId))
                    SetProductPreviousSalePrice(cId, pId);
            }
            finally
            {
                _isUpdatingText = false;
                _lastSelectedProductText = string.Empty;
            }
        }

        // ══════════════════════════════════════════════════════════════════════════
        // PRODUCT PRICES  (guarded against double DB calls)
        // ══════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Loads all price tiers for a product and sets the default price.
        /// Uses _isLoadingPrices flag to prevent the dropdown SelectedIndexChanged
        /// from firing a second redundant DB call during programmatic binding.
        /// </summary>
        private async Task ShowProductPricesAsync(int productId)
        {
            _isLoadingPrices = true;
            try
            {
                using (var context = new POSDbContext())
                {
                    var data = await context.ProductPrices
                        .Where(s => s.ProductId == productId)
                        .Select(s => new ProdDTO
                        {
                            Type = s.TypeName,
                            Price = s.Price,
                            Items = s.ItemsCount,
                            P_Per_Item = s.PricePerItem
                        }).ToListAsync();

                    ProductPriceDataGridView.DataSource = null;
                    ProductPriceDataGridView.DataSource = data;
                    ProductPriceDataGridView.RowHeadersVisible = false;
                    ProductPriceDataGridView.ClearSelection();

                    if (data.Count > 0)
                    {
                        var first = data.First();
                        productTypeDropdown.SelectedValue = first.Type;
                        ProductSalePrice.Text = $"{(int)first.Price}";
                        prod_ItemCountTxt.Text = first.Items.ToString();
                        ProductAmount.Text = (1 * (int)first.Price).ToString();
                    }
                    else
                    {
                        ProductSalePrice.Text = $"0";
                        ProductAmount.Text = (1 * (int)0).ToString();
                    }
                }
            }
            finally
            {
                _isLoadingPrices = false;
            }
        }

        /// <summary>
        /// Only fires when the user MANUALLY changes the product type dropdown.
        /// Guard flags prevent this from running during programmatic price loading.
        /// </summary>
        private async void productTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skip if programmatically setting value during product load
            if (_isLoadingPrices || _isUpdatingText) return;

            if (productTypeDropdown.SelectedItem == null ||
                string.IsNullOrEmpty(productTypeDropdown.SelectedValue?.ToString())) return;

            if (!int.TryParse(PId, out int pid))
            {
                ProductSalePrice.Text = "0";
                return;
            }

            string selectedValue = productTypeDropdown.SelectedValue.ToString();

            using (var context = new POSDbContext())
            {
                var price = await context.ProductPrices
                    .Where(s => s.ProductId == pid && s.TypeName == selectedValue)
                    .Select(s => new ProdPricesdto { price = s.Price, ItemCount = s.ItemsCount })
                    .FirstOrDefaultAsync();

                ProductSalePrice.Text = price != null ? $"{price.price:0}" : "0";
                prod_ItemCountTxt.Text = price != null ? price.ItemCount.ToString() : "0";
            }

            if (int.TryParse(P_StockQtyTxt.Text, out int qty) &&
                int.TryParse(ProductSalePrice.Text, out int sp))
                ProductAmount.Text = (qty * sp).ToString();
        }

        // ══════════════════════════════════════════════════════════════════════════
        // CART OPERATIONS
        // ══════════════════════════════════════════════════════════════════════════

        private void AddToCardBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string productId = PId;
            string productName = ProductEngNameTxt.Text;
            string productType = productTypeDropdown.SelectedValue?.ToString();
            decimal salePrice = Math.Round(decimal.Parse(ProductSalePrice.Text), 1);
            int qty = int.Parse(P_StockQtyTxt.Text);
            decimal amount = salePrice * qty;
            string detail = ProductDetailTxt.Text;

            var finalName = OtherProductChk.Checked
                ? $"{productName} {detail}"
                : $"{prod_U_Name} {detail}";

            string formattedText = TextFormatHelper.FormatMixedText(finalName);
            string finalPId = OtherProductChk.Checked ? string.Empty : productId;

            // Stock check
            var config = ConfigurationManager.Configuration.Features.EnableUpdateQty;
            if (config && !string.IsNullOrEmpty(productId))
            {
                if (!int.TryParse(Prod_Qty.Text, out int availableQty) ||
                    !int.TryParse(prod_ItemCountTxt.Text, out int itemCount) ||
                    itemCount <= 0)
                {
                    MessageBox.Show(
                        $"Product type '{productType}' is not properly configured. Item count must be > 0.",
                        "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (availableQty <= 0 || (itemCount * qty) > availableQty)
                {
                    MessageBox.Show(
                        $"Available stock is {availableQty} {prodStockUnit.Text}. Please enter a valid quantity.",
                        "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Duplicate check — if same product name already in cart, increase qty
            bool productExists = false;
            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells[Col.ProductId].Value == null) continue;

                string cleanExisting = TextFormatHelper.RemoveDirectionalCharacters(
                    row.Cells[Col.UrduName].Value?.ToString() ?? string.Empty);
                string cleanNew = TextFormatHelper.RemoveDirectionalCharacters(formattedText);

                if (string.Equals(cleanExisting.Trim(), cleanNew.Trim(),
                                  StringComparison.OrdinalIgnoreCase))
                {
                    int existingQty = int.TryParse(row.Cells[Col.Qty].Value?.ToString(), out int eq) ? eq : 0;

                    if (config && !string.IsNullOrEmpty(productId))
                    {
                        if ((existingQty * Convert.ToInt32(prod_ItemCountTxt.Text)) + (qty * Convert.ToInt32(prod_ItemCountTxt.Text)) <= Convert.ToInt32(Prod_Qty.Text))
                        {
                            existingQty += qty;
                            row.Cells[Col.Qty].Value = existingQty;
                            row.Cells[Col.Amount].Value = Math.Round(existingQty * salePrice, 1);
                            productExists = true;
                            break;
                        }
                        else
                        {
                            productExists = false;

                            MessageBox.Show($"Remaining stock is {Convert.ToInt32(Prod_Qty.Text) - (existingQty * Convert.ToInt32(prod_ItemCountTxt.Text)) + (qty * Convert.ToInt32(prod_ItemCountTxt.Text))} - {prodStockUnit.Text}. Please enter a valid quantity.");

                            return;
                        }
                    }
                    else
                    {
                      
                        existingQty += qty;
                        row.Cells["Qty"].Value = existingQty;
                        row.Cells["Amount"].Value = Math.Round(existingQty * salePrice, 1);
                        productExists = true;
                        break;
                    }
                   
                }
            }

            if (!productExists)
                CartProductList.Rows.Add(null, amount, salePrice, formattedText,
                                         productType, qty, finalPId, detail);

            CalculateTotals();
            CalculateReturnAmount();
            ClearInputs();
            ProductEngNameTxt.Focus();
        }

        private void CartProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
              e.ColumnIndex >= CartProductList.Columns.Count ||
              CartProductList.Columns[e.ColumnIndex].Name != "Delete") return;

            // Get the row that is being clicked
            DataGridViewRow row = CartProductList.Rows[e.RowIndex];
            // Check if the "Urdu Name" cell contains any of the restricted values
            string urduName = row.Cells["Urdu Name"]?.Value?.ToString();

            if (urduName == "سابقہ" || urduName == "سابقہ ایڈوانس جمع" || urduName == "سابقہ ادھار جمع")
            {
                MessageBox.Show($"Press (Ctrl+W) to delete {urduName} ",
                    "Delete Restricted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Do you want to delete this product?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                CartProductList.Rows.RemoveAt(e.RowIndex);

                CalculateTotals();
                CalculateReturnAmount();
                ProductEngNameTxt.Focus();
                ProductEngNameTxt.SelectAll();
            }
        }

        private void CartProductList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = CartProductList.Rows[e.RowIndex];
            string colName = CartProductList.Columns[e.ColumnIndex].Name;

            if (colName != Col.Qty && colName != Col.SalePrice) return;

            try
            {
                // Stock validation when qty edited directly in grid
                if (colName == Col.Qty &&
                    ConfigurationManager.Configuration.Features.EnableUpdateQty)
                {
                    string pidVal = row.Cells[Col.ProductId].Value?.ToString();
                    if (!string.IsNullOrEmpty(pidVal) && int.TryParse(pidVal, out int productId))
                    {
                        string typeName = row.Cells[Col.ProductType].Value?.ToString();
                        int editedQty = int.Parse(row.Cells[Col.Qty].Value?.ToString() ?? "0");

                        using (var context = new POSDbContext())
                        {
                            var product = context.Products.FirstOrDefault(s => s.Id == productId);
                            var price = context.ProductPrices
                                .Where(s => s.ProductId == productId && s.TypeName == typeName)
                                .Select(s => new ProdPricesdto
                                {
                                    price = s.Price,
                                    ItemCount = s.ItemsCount
                                }).FirstOrDefault();

                            if (product != null && price != null)
                            {
                                if (editedQty <= 0 || (price.ItemCount * editedQty) > product.Qty)
                                {
                                    MessageBox.Show(
                                        $"Available stock is {product.Qty} {product.ProdQtyStockUnit}.",
                                        "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    row.Cells[e.ColumnIndex].Value = 0;
                                    return;
                                }
                            }
                        }
                    }
                }

                decimal sp = Convert.ToDecimal(row.Cells[Col.SalePrice].Value);
                int q = Convert.ToInt32(row.Cells[Col.Qty].Value);
                row.Cells[Col.Amount].Value = Math.Round(sp * q, 1);
                CalculateTotals();
                CalculateReturnAmount();
            }
            catch
            {
                MessageBox.Show("Invalid input. Please enter correct numeric values.");
                row.Cells[e.ColumnIndex].Value = 0;
            }
        }

        private void CartProductList_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode != Keys.Back || CartProductList.CurrentRow == null) return;

            // Get the current row
            DataGridViewRow currentRow = CartProductList.CurrentRow;

            // Check if the "Urdu Name" cell contains any of the restricted values
            string urduName = currentRow.Cells["Urdu Name"]?.Value?.ToString();
            if (urduName == "سابقہ" ||
                  urduName == "سابقہ ایڈوانس جمع" ||
                  urduName == "سابقہ ادھار جمع")
            {
                MessageBox.Show($"Press (Ctrl+W) to delete {urduName} ",
                    "Delete Restricted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;

            }

            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                CartProductList.Rows.RemoveAt(CartProductList.CurrentRow.Index);

                CalculateTotals();
                CalculateReturnAmount();
                ProductEngNameTxt.Focus();
                ProductEngNameTxt.SelectAll();
            }
            e.Handled = true;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // TOTALS & CALCULATIONS
        // ══════════════════════════════════════════════════════════════════════════

        private void CalculateTotals()
        {
            int totalItems = 0;
            decimal subTotal = 0;

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells[Col.Amount].Value != null)
                {
                    totalItems++;
                    subTotal += Convert.ToDecimal(row.Cells[Col.Amount].Value);
                }
            }

            TotalItemLbl.Text = totalItems.ToString();
            TotalAmountLbl.Text = subTotal.ToString();
        }

        private void CalculateReturnAmount()
        {
            if (!string.IsNullOrEmpty(ReceivedAmountTxt.Text))
            {
                string validated = RegexValidator.ValidateCommonPattern(
                    ReceivedAmountTxt.Text, ValidationPattern.NumbersOnly, "receivedAmountField");
                if (ReceivedAmountTxt.Text != validated)
                {
                    ReceivedAmountTxt.Text = validated;
                    ReceivedAmountTxt.SelectionStart = validated.Length;
                }
            }

            if (string.IsNullOrEmpty(TotalAmountLbl.Text) || TotalAmountLbl.Text == "0")
            {
                lblRemainingAmount.Text = "Remaining: Rs. 0";
                return;
            }

            if (string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
            {
                lblRemainingAmount.Text = "Remaining: Rs. 0";
                return;
            }

            if (!decimal.TryParse(TotalAmountLbl.Text, out decimal total) ||
                !decimal.TryParse(ReceivedAmountTxt.Text, out decimal received)) return;

            decimal remaining = total - received;
            lblRemainingAmount.Text = remaining >= 0
                ? $"Remaining Amt:  Rs. {remaining}"
                : $"Return Amt:  Rs. {Math.Abs(remaining)}";
            lblRemainingAmount.ForeColor = remaining >= 0 ? Color.Red : Color.Blue;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // SAVE ORDER
        // ══════════════════════════════════════════════════════════════════════════

        private async Task<bool> SaveOrder(bool isUpdate = false)
        {
            using (var context = new POSDbContext())
            using (var tx = context.Database.BeginTransaction())
            {
                try
                {
                    var repo = new OrderRepository(context);
                    var orderData = await GetOrderData();

                    if (isUpdate)
                        orderData.Id = int.Parse(PreviousOrderIdLbl.Text);

                    var orderId = isUpdate
                        ? await UpdateOrderAsync(repo, orderData, context)
                        : await repo.AddOrder(orderData);

                    await SaveOrderDetailsAsync(context, orderId);

                    // ─── 4. INSIDE SaveOrder(): Post to ledger after order is saved ───────────────

                    #region Code for POST to ledger after order save
                    // Add this INSIDE your existing SaveOrder() method, after tx.Commit()

                    // Example — add after your existing order save, inside the transaction:
                    /*
                        // Get received and total
                        decimal received = decimal.TryParse(ReceivedAmountTxt.Text, out decimal r) ? r : 0;
                        decimal total = decimal.TryParse(TotalBillTxt.Text, out decimal t) ? t : 0;

                        if (_selectedCustomerId > 0)
                        {
                            var ledgerRepo = new CustomerLedgerRepository(context);

                            if (_advanceApplied > 0)
                            {
                                // Post: advance applied to this order
                                await ledgerRepo.PostAdvanceDepositAsync(... use PostAdjustment with positive amount);
                                // Actually: use a SALE entry that shows debit (order total) and credit (advance applied)
                                // Simpler approach: post sale entry for the truly outstanding amount
                            }

                            // Outstanding = what customer still owes after received + advance
                            decimal outstanding = total - received; // received already includes advance if checkbox ticked
                            if (outstanding > 0)
                            {
                                await ledgerRepo.PostSaleEntryAsync(
                                    _selectedCustomerId, total, received, orderId, "User");
                            }
                            else if (outstanding < 0)
                            {
                                // Overpayment → becomes advance
                                await ledgerRepo.PostAdvanceDepositAsync(
                                    _selectedCustomerId, Math.Abs(outstanding), "Cash", "", 
                                    $"Overpayment from Invoice #{orderId}", "User");
                            }
                        }
                    */

                    #endregion

                    await SettleAdvanceLoan(context, orderData.ReceiveAmount, orderData.TotalBill);

                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    LogError("SaveOrder", ex);
                    return false;
                }
            }
        }

        private async Task SettleAdvanceLoan(POSDbContext context, float receivedAmt, float totalBill)
        {
            if (_shouldAddWithBill && int.TryParse(CustomerIdLbl.Text, out int custId) && custId > 0)
            {
                decimal amount = Math.Abs(_customerAdvanceBalance);
                var repo = new CustomerLedgerRepository(context);

                if (_isAdvanceBalance)
                    await repo.PostAdjustmentAsync(custId, amount, $"Payment settle INV-{InvoiceNoLbl.Text}", "User");
                else
                    await repo.PostAdvanceDepositAsync(custId, amount, "Cash", InvoiceNoLbl.Text.Trim(),
                        $"Payment settle INV-{InvoiceNoLbl.Text}", "User");

                _shouldAddWithBill = false;
            }

            if (CustomerLagerRecordChk.Checked)
            {
                if (int.TryParse(CustomerIdLbl.Text, out int customerId) && customerId > 0)
                {
                    decimal difference = Convert.ToDecimal(receivedAmt - totalBill);
                    if (difference == 0) return;

                    var repo = new CustomerLedgerRepository(context);

                    if (difference < 0)
                        await repo.PostAdjustmentAsync(customerId, Math.Abs(difference),
                            $"loan added INV-{InvoiceNoLbl.Text}", "User");
                    else
                        await repo.PostAdvanceDepositAsync(customerId, difference, "Cash",
                            InvoiceNoLbl.Text, $"Advance deposit {InvoiceNoLbl.Text}", "User");
                }


                CustomerLagerRecordChk.Checked = false;
            }
        }


        /// <summary>
        /// For an update: restores stock for old quantities (if enabled),
        /// removes old detail rows, all within the SAME transaction/context.
        /// </summary>
        private async Task<int> UpdateOrderAsync(
            OrderRepository repo, Order order, POSDbContext context)
        {
            var orderId = await repo.AddOrder(order);

            var existingDetails = await context.OrderDetails
                .Where(s => s.OrderId == orderId).ToListAsync();

            // Restore stock for old order quantities (within same transaction)
            if (ConfigurationManager.Configuration.Features.EnableUpdateQty)
            {
                foreach (var item in existingDetails)
                {
                    if (!item.ProductId.HasValue) continue;

                    var product = await context.Products.FindAsync(item.ProductId);
                    var prices = await context.ProductPrices
                        .Where(p => p.ProductId == item.ProductId &&
                                    p.TypeName == item.QuantityType)
                        .FirstOrDefaultAsync();

                    if (product != null && prices != null)
                    {
                        product.Qty += item.Quantity * prices.ItemsCount;
                        context.Entry(product).State = EntityState.Modified;
                    }
                }
            }

            // Remove old detail rows
            context.OrderDetails.RemoveRange(existingDetails);
            await context.SaveChangesAsync(); // async — stays in the same transaction

            return orderId;
        }

        private async Task SaveOrderDetailsAsync(POSDbContext context, int orderId)
        {
            var detailList = new List<OrderDetail>();
            bool stockCheck = ConfigurationManager.Configuration.Features.EnableUpdateQty;

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells[Col.ProductId].Value == null) continue;

                string pidVal = row.Cells[Col.ProductId].Value?.ToString();
                if (!int.TryParse(row.Cells[Col.Qty].Value?.ToString(), out int qty)) continue;
                if (!float.TryParse(row.Cells[Col.SalePrice].Value?.ToString(), out float price)) continue;

                ProductPrice prices = null;

                if (stockCheck && !string.IsNullOrEmpty(pidVal))
                {
                    int pid = int.Parse(pidVal);
                    string typeName = row.Cells[Col.ProductType].Value?.ToString();

                    prices = await context.ProductPrices
                        .Where(p => p.ProductId == pid && p.TypeName == typeName)
                        .FirstOrDefaultAsync();

                    var product = await context.Products.FindAsync(pid);

                    if (product != null && prices != null &&
                        (qty <= 0 || (qty * prices.ItemsCount) > product.Qty))
                    {
                        LoadingManager.HideLoading();
                        MessageBox.Show($"Insufficient stock for '{product.ProductEnglishName}'.",
                            "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw new InvalidOperationException(
                            $"Insufficient stock for product ID {pidVal}");
                    }
                }

                var detail = new OrderDetail
                {
                    ProductId = string.IsNullOrEmpty(pidVal) ? (int?)null : int.Parse(pidVal),
                    OtherProductName = string.IsNullOrEmpty(pidVal)
                        ? row.Cells[Col.UrduName].Value?.ToString() : null,
                    Quantity = qty,
                    QuantityType = row.Cells[Col.ProductType].Value?.ToString(),
                    Price = price,
                    CreatedDate = DateTime.Now,
                    OrderId = orderId,
                    ProductDetail = row.Cells[Col.Detail].Value?.ToString()
                };
                detailList.Add(detail);

                // Deduct stock — same context, same transaction
                if (stockCheck && !string.IsNullOrEmpty(pidVal) && prices != null)
                {
                    int pid = int.Parse(pidVal);
                    var product = await context.Products.FindAsync(pid);
                    if (product != null)
                    {
                        product.Qty -= detail.Quantity * prices.ItemsCount;
                        context.Entry(product).State = EntityState.Modified;
                    }
                }
            }

            context.OrderDetails.AddRange(detailList);
            await context.SaveChangesAsync();
        }

        private async Task<Order> GetOrderData()
        {
            int? cId = null;
            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) &&
                !string.IsNullOrEmpty(CustomerIdLbl.Text) &&
                int.TryParse(CustomerIdLbl.Text, out int parsedId))
                cId = parsedId;

            float.TryParse(TotalAmountLbl.Text, out float totalBill);
            float receiveAmount = totalBill;
            if (!string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
                float.TryParse(ReceivedAmountTxt.Text, out receiveAmount);

            return new Order
            {
                TotalBill = totalBill,
                ReceiveAmount = receiveAmount,
                CreatedDate = DateTime.Now,
                InvoiceNumber = !string.IsNullOrEmpty(InvoiceNoLbl.Text)
                    ? InvoiceNoLbl.Text : DateTime.Now.ToString("MMddyyy-HHmmss"),
                paymentType = CashRadioBtn.Checked ? "Cash" : "Bank",
                customerId = cId
            };
        }

        // ══════════════════════════════════════════════════════════════════════════
        // TEMP ORDER SAVE
        // ══════════════════════════════════════════════════════════════════════════

        private async Task<TempOrder> GetTempOrderData()
        {
            int? cId = null;
            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) && !string.IsNullOrEmpty(CustomerIdLbl.Text) && int.TryParse(CustomerIdLbl.Text, out int parsedId))
                cId = parsedId;

            float.TryParse(TotalAmountLbl.Text, out float totalBill);

            return new TempOrder
            {
                TotalBill = totalBill,
                CreatedDate = DateTime.Now,
                InvoiceNumber = !string.IsNullOrEmpty(InvoiceNoLbl.Text)
                    ? InvoiceNoLbl.Text : DateTime.Now.ToString("MMddyyy-HHmmss"),
                customerId = cId,
                CustomerName = CustomerNameTxt.Text
            };
        }

        private async Task SaveTempOrderDetailsAsync(POSDbContext context, string invoiceNo)
        {
            // Delete existing temp details for this invoice first
            var existing = context.TempOrderDetails
                .Where(s => s.TempInvoiceNumber == invoiceNo).ToList();
            if (existing.Count > 0)
            {
                context.TempOrderDetails.RemoveRange(existing);
                await context.SaveChangesAsync();
            }

            var detailList = new List<TempOrderDetail>();

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells[Col.ProductId].Value == null) continue;

                string pidVal = row.Cells[Col.ProductId].Value?.ToString();
                if (!int.TryParse(row.Cells[Col.Qty].Value?.ToString(), out int qty)) continue;
                if (!float.TryParse(row.Cells[Col.SalePrice].Value?.ToString(), out float price)) continue;

                detailList.Add(new TempOrderDetail
                {
                    ProductId = string.IsNullOrEmpty(pidVal) ? (int?)null : int.Parse(pidVal),
                    ProductName = row.Cells[Col.UrduName].Value?.ToString(),
                    Quantity = qty,
                    QuantityType = row.Cells[Col.ProductType].Value?.ToString(),
                    Price = price,
                    TempInvoiceNumber = invoiceNo,
                    ProductDetail = row.Cells[Col.Detail].Value?.ToString()
                });
            }

            context.TempOrderDetails.AddRange(detailList);
            await context.SaveChangesAsync();
        }

        private async void SaveOrderTransactionAsync(string customerName)
        {
            using (var context = new POSDbContext())
            using (var tx = context.Database.BeginTransaction())
            {
                try
                {

                    // Delete the Record from Customer Ledger 
                    context.Database.ExecuteSqlCommand($"delete from CustomerLedger where Note like '%{InvoiceNoLbl.Text}%'");


                    var repo = new OrderRepository(context);
                    var data = await GetTempOrderData();

                    // FIX: check TempOrders, not Orders
                    var existingTemp = await context.TempOrders
                        .FirstOrDefaultAsync(o => o.InvoiceNumber == data.InvoiceNumber);

                    if (existingTemp != null)
                    {
                        var existingTempDetails = await context.TempOrderDetails
                            .Where(d => d.TempInvoiceNumber == data.InvoiceNumber).ToListAsync();
                        context.TempOrderDetails.RemoveRange(existingTempDetails);
                        context.TempOrders.Remove(existingTemp);
                        await context.SaveChangesAsync();
                    }

                    var invoiceNo = await repo.AddTempOrder(data);
                    await SaveTempOrderDetailsAsync(context, invoiceNo);

                    tx.Commit();
                    ResetUIAfterSave();
                    MessageBox.Show("Order Saved Successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    LogError("SaveOrderTransactionAsync", ex);
                    MessageBox.Show("Order save failed. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ══════════════════════════════════════════════════════════════════════════
        // PRINT & PDF
        // ══════════════════════════════════════════════════════════════════════════

        private async void SaveAndPrintOrderBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count == 0)
            {
                MessageBox.Show("Please add a product first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadingManager.ShowLoading();
            try
            {
                bool isUpdate = !string.IsNullOrEmpty(PreviousOrderIdLbl.Text) &&
                                PreviousOrderIdLbl.Text != "Prev Order Id";
                bool done = await SaveOrder(isUpdate);

                LoadingManager.HideLoading();

                if (!done)
                {
                    MessageBox.Show("Order creation failed!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                OrderPrintPreviewDialog.Document = OrderPrintDocument;
                OrderPrintDocument.DefaultPageSettings.PaperSize =
                    new PaperSize("FullInvoice", 280, 32767);
                OrderPrintDocument.Print();

                await DeleteTempOrderIfNeededAsync();
                ResetUIAfterSave();
                SendKeys.SendWait("^{F11}");
                MessageBox.Show("Order created successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LoadingManager.HideLoading();
                LogError("SaveAndPrintOrderBtn_Click", ex);
            }
        }

        private async void SaveOrderWithoutPrintBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count == 0)
            {
                MessageBox.Show("Please add a product first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadingManager.ShowLoading();
            try
            {
                bool isUpdate = !string.IsNullOrEmpty(PreviousOrderIdLbl.Text) &&
                                PreviousOrderIdLbl.Text != "Prev Order Id";
                bool done = await SaveOrder(isUpdate);

                LoadingManager.HideLoading();

                if (!done)
                {
                    MessageBox.Show("Order creation failed!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await DeleteTempOrderIfNeededAsync();
                ResetUIAfterSave();
                MessageBox.Show("Order saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LoadingManager.HideLoading();
                LogError("SaveOrderWithoutPrintBtn_Click", ex);
            }
        }

        /// <summary>
        /// Removes the temp order record if this order was originally a temp save.
        /// </summary>
        private async Task DeleteTempOrderIfNeededAsync()
        {
            if (!isTempSaved) return;

            using (var context = new POSDbContext())
            {
                var p1 = new SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text);
                await context.Database.ExecuteSqlCommandAsync(
                    "DELETE FROM TempOrderDetails WHERE TempInvoiceNumber = @InvoiceNumber", p1);

                var p2 = new SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text);
                await context.Database.ExecuteSqlCommandAsync(
                    "DELETE FROM TempOrders WHERE InvoiceNumber = @InvoiceNumber", p2);
            }
        }

        private void OrderPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (EnglishInvoiceChk.Checked)
                InvoicePrintHelper.PrintEnglishInvoice(
                    e, CartProductList, CustomerNameTxt.Text,
                    InvoiceNoLbl.Text, TotalAmountLbl.Text,
                    CashRadioBtn.Checked, ReceivedAmountTxt.Text, isPaid);
            else
                InvoicePrintHelper.PrintInvoice(
                    e, CartProductList, CustomerNameTxt.Text,
                    InvoiceNoLbl.Text, TotalAmountLbl.Text,
                    CashRadioBtn.Checked, ReceivedAmountTxt.Text, isPaid);
        }

        private void PrintPreviewBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count == 0)
            {
                MessageBox.Show("Please add a product first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SendKeys.SendWait("^{F11}");
            OrderPrintPreviewDialog.Document = OrderPrintDocument;
            OrderPrintDocument.DefaultPageSettings.PaperSize =
                new PaperSize("FullInvoice", 280, 32767);
            OrderPrintPreviewDialog.PrintPreviewControl.Zoom = 1.0;
            OrderPrintPreviewDialog.ShowDialog();
        }

        public async Task GeneratePdfInvoice()
        {
            var confirm = MessageBox.Show("Generate PDF invoice?", "Confirm Action",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var dlg = new SaveFileDialog())
                {
                    string pdfName = !string.IsNullOrEmpty(CustomerNameTxt.Text)
                        ? $"{CustomerNameTxt.Text}-{InvoiceNoLbl.Text}"
                        : InvoiceNoLbl.Text;

                    dlg.FileName = $"Invoice_{pdfName}.pdf";
                    dlg.Filter = "PDF Files (*.pdf)|*.pdf";
                    dlg.DefaultExt = "pdf";
                    dlg.InitialDirectory = Environment.GetFolderPath(
                        Environment.SpecialFolder.Desktop);

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        var generator = new PrintToPdfGenerator();
                        generator.GenerateInvoice(CartProductList, dlg.FileName,
                            CustomerNameTxt.Text, InvoiceNoLbl.Text,
                            TotalAmountLbl.Text, ReceivedAmountTxt.Text);

                        ToastHelper.ShowSuccess($"Invoice saved to:\n{dlg.FileName}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateInvoicePdfBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count == 0)
            {
                MessageBox.Show("Please add a product first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _ = GeneratePdfInvoice();
        }

        // ══════════════════════════════════════════════════════════════════════════
        // LOAD EXISTING ORDER
        // ══════════════════════════════════════════════════════════════════════════

        private async void PreviousOrderIdLbl_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PreviousOrderIdLbl.Text) ||
                PreviousOrderIdLbl.Text == "OrderID" ||
                string.IsNullOrEmpty(InvoiceNoLbl.Text) ||
                InvoiceNoLbl.Text == "InvoiceNo") return;

            if (!int.TryParse(PreviousOrderIdLbl.Text, out int orderId)) return;

            using (var context = new POSDbContext())
            {
                var repo = new OrderRepository(context);
                var result = await repo.GetOrderByIdAsync(orderId, InvoiceNoLbl.Text);
                if (result == null) return;

                CartProductList.Rows.Clear();

                CustomerIdLbl.Text = result.CustomerId?.ToString() ?? string.Empty;
                CustomerNameTxt.Text = string.IsNullOrEmpty(CustomerIdLbl.Text)
                    ? string.Empty : result.CustomerName;
                TotalAmountLbl.Text = result.TotalBill.ToString();

                CashRadioBtn.Checked = result.paymentType == "Cash";
                BankTransferRaadioBtn.Checked = result.paymentType != "Cash";

                foreach (var item in result.OrderDetailsList)
                {
                    string finalName = !string.IsNullOrEmpty(item.ProductDetail)
                        ? $"{item.ProductName} {item.ProductDetail}" : item.ProductName;
                    decimal sp = Math.Round((decimal)item.Price, 1);
                    decimal amount = sp * item.Quantity;

                    CartProductList.Rows.Add(null, amount, sp, finalName,
                        item.QuantityType, item.Quantity,
                        item.ProductId.ToString(), item.ProductDetail);
                }

                CalculateTotals();
            }
        }

        // ══════════════════════════════════════════════════════════════════════════
        // CUSTOMER SELECTION
        // ══════════════════════════════════════════════════════════════════════════

        private async void CustomerListDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && CustomerListDataGrid.CurrentRow?.Index == 0)
            {
                CustomerNameTxt.Focus();
                CustomerNameTxt.SelectAll();
                CustomerListDataGrid.Visible = false;
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;

                if (CustomerListDataGrid.IsCurrentCellInEditMode)
                {
                    CustomerListDataGrid.EndEdit();
                    return;
                }

                if (CustomerListDataGrid.CurrentRow != null && !CustomerListDataGrid.CurrentRow.IsNewRow)
                    await SelectCustomerFromRow(CustomerListDataGrid.CurrentRow);
            }
        }

        private async void CustomerListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && CustomerListDataGrid.CurrentRow != null)
                await SelectCustomerFromRow(CustomerListDataGrid.CurrentRow);
        }

        private async Task SelectCustomerFromRow(DataGridViewRow row)
        {
            if (!int.TryParse(row.Cells[0].Value?.ToString(), out int cId)) return;

            _isUpdatingText = true;

            try
            {
                CustomerIdLbl.Text = cId.ToString();
                CustomerNameTxt.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
                ResetCustomerBtn.Visible = true;
                CustomerListDataGrid.Visible = false;

                ProductEngNameTxt.Focus();
                ProductEngNameTxt.SelectAll();

                using (var context = new POSDbContext())
                {
                    IOrderRepository orderRepo = new OrderRepository(context);
                    var summary = orderRepo.GetLatestOrderAmountSummaryByCustomerId(cId);
                    UpdatePreviousOrderSummary(summary);
                }

                await LoadCustomerLedgerBalanceAsync(cId);
            }
            finally
            {
                _isUpdatingText = false;
                _lastSelectedProductText = string.Empty;
            }

        }

        private void UpdatePreviousOrderSummary(OrderAmountSummaryDto summary)
        {
            PreviousOrderSummaryLbl.Text = string.Empty;
            if (summary == null) return;

            previousBillAmountLbl.Text = summary.TotalAmount.ToString();
            PreviousReceivedAmountLbl.Text = summary.ReceivedAmount.ToString();

            float remaining = summary.TotalAmount - summary.ReceivedAmount;
            if (remaining == 0) return;

            bool isPositive = remaining >= 0;
            PreviousOrderSummaryLbl.Text = isPositive
                ? $"Remaining Amt:  Rs. {remaining}"
                : $"Return Amt:  Rs. {Math.Abs(remaining)}";
            PreviousOrderSummaryLbl.ForeColor = isPositive ? Color.Red : Color.Blue;
            PreviousOrderSummaryLbl.Visible = true;
        }

        private void SetProductPreviousSalePrice(int cId, int productId)
        {
            using (var context = new POSDbContext())
            {
                IProductRepository repo = new ProductRepository(context);
                var history = repo.ProductPreviousPriceInRecentOrderByCustomerId(cId, productId);
                if(history.Count()>0)
                {

                    ProductOrderHistoryDataGrid.DataSource = null;
                    ProductOrderHistoryDataGrid.DataSource = history.ToList();
                    ProductOrderHistoryDataGrid.RowHeadersVisible = false;
                    ProductOrderHistoryDataGrid.ClearSelection();
                }else
                {
                    ProductOrderHistoryDataGrid.DataSource = null;
                    ProductOrderHistoryDataGrid.ClearSelection();
                }
            }
        }

        // ══════════════════════════════════════════════════════════════════════════
        // TEMP ORDERS
        // ══════════════════════════════════════════════════════════════════════════

        private async void SaveBillBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows?.Count == 0)
            {
                MessageBox.Show("Please add a product first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Store as temporary record?", "Save Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            string customerName = await GetCustomerNameAsync();
            if (customerName == null) return;

            SaveOrderTransactionAsync(customerName);
        }

        private async Task<string> GetCustomerNameAsync()
        {
            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) ||
                !string.IsNullOrEmpty(CustomerIdLbl.Text))
                return CustomerNameTxt.Text;

            using (var dialog = new InputDialog("Enter customer name:", "Customer Info"))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return null;
                CustomerNameTxt.Text = dialog.InputValue;
                customerId = string.Empty;
                CustomerIdLbl.Text = string.Empty;
                return dialog.InputValue;
            }
        }

        private async void TemOrderBtn_Click(object sender, EventArgs e)
        {
            var form = new Form { Text = "Temp Order Form", StartPosition = FormStartPosition.CenterScreen };
            var ctrl = new TempOrderControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Width = 1050; form.Height = 525;
            form.ShowDialog();

            if (!ctrl.IsRecordSelected) return;
            if (!string.IsNullOrEmpty(ctrl.InvoiceNoLbl.Text))
                InvoiceNoLbl.Text = ctrl.InvoiceNoLbl.Text;

            if (InvoiceNoLbl.Text == "InvoiceNo" || string.IsNullOrEmpty(InvoiceNoLbl.Text)) return;

            if (CartProductList.Rows.Count > 0)
            {
                if (MessageBox.Show("Loading this order will clear current cart. Continue?",
                        "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            }

            if (ctrl.CustomerId != 0)
            {

                _isUpdatingText = true;  // ← ADD THIS
                CustomerIdLbl.Text = ctrl.CustomerId.ToString();
                CustomerNameTxt.Text = ctrl.CustomerName;
                ResetCustomerBtn.Visible = ResetCustomerBtn.Enabled = true;
                CustomerListDataGrid.Visible = false;

                using (var context = new POSDbContext())
                {
                    IOrderRepository orderRepo = new OrderRepository(context);
                    UpdatePreviousOrderSummary(
                        orderRepo.GetLatestOrderAmountSummaryByCustomerId(ctrl.CustomerId));
                }
                await LoadCustomerLedgerBalanceAsync(ctrl.CustomerId);

                _isUpdatingText = false;
            }
            else
            {
                ClearCustomerPreviousTransactionGroup();
                CustomerIdLbl.Text = CustomerNameTxt.Text = string.Empty;
                ResetCustomerBtn.Visible = ResetCustomerBtn.Enabled = false;
                ApplyAdvanceChk.Visible = false;
            }

            using (var context = new POSDbContext())
            {
                var orderRepo = new OrderRepository(context);
                var result = orderRepo.GetTempOrderDetailByInvoice(InvoiceNoLbl.Text);

                if (result == null || result.Count == 0) return;

                isTempSaved = true;
                CartProductList.Rows.Clear();

                foreach (var item in result)
                {
                    string finalName = !string.IsNullOrEmpty(item.ProductDetail)
                        ? $"{item.ProductName} {item.ProductDetail}" : item.ProductName;
                    decimal sp = Math.Round((decimal)item.Price, 1);
                    decimal amount = sp * item.Quantity;

                    CartProductList.Rows.Add(null, amount, sp, finalName,
                        item.QuantityType, item.Quantity, item.ProductId.ToString(), item.ProductDetail);
                }
                CalculateTotals();
            }

            //if (HasMatchingValue(CartProductList))
            //{
            //    ApplyAdvanceChk.CheckedChanged -= ApplyAdvanceChk_CheckedChanged;
            //    AllowApplyAdvanceCheck();
            //    ApplyAdvanceChk.CheckedChanged += ApplyAdvanceChk_CheckedChanged;
            //}

            //ProductEngNameTxt.Focus();

            ReceivedAmountTxt.Text = ctrl.ReceivedAmount.ToString();

            if (HasMatchingValue(CartProductList))
            {
                //ApplyAdvanceChk.CheckedChanged -= ApplyAdvanceChk_CheckedChanged;
                //AllowApplyAdvanceCheck();
                //_shouldAddWithBill = true;
                //ApplyAdvanceChk.CheckedChanged += ApplyAdvanceChk_CheckedChanged;

                //_isInternalChange = true;

                //RemoveLoamAdvance();
                PreviousOrderIdLbl.Text = string.Empty;
                await LoadCustomerLedgerBalanceAsync(ctrl.CustomerId);
                AllowApplyAdvanceCheck();
            }

            ProductEngNameTxt.Focus();
            ProductEngNameTxt.SelectAll();
        }
        //private bool HasMatchingValue(DataGridView dataGridView)
        //{
        //    foreach (DataGridViewRow row in dataGridView.Rows)
        //    {
        //        if (row.Cells["Urdu Name"].Value != null)
        //        {
        //            string cellValue = row.Cells["Urdu Name"].Value.ToString();
        //            if (cellValue == "سابقہ ایڈوانس جمع" || cellValue == "سابقہ ادھار جمع")
        //            {
        //                return true;
        //            } 
        //        }
        //    }
        //    return false;
        //}

        private bool HasMatchingValue(DataGridView dataGridView)
        {
            // Find the row to remove
            DataGridViewRow rowToRemove = null;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["Urdu Name"].Value != null)
                {
                    string cellValue = row.Cells["Urdu Name"].Value.ToString();
                    if (cellValue == "سابقہ ایڈوانس جمع" || cellValue == "سابقہ ادھار جمع")
                    {
                        rowToRemove = row;
                        break;
                    }
                }
            }

            // Remove it after iteration
            if (rowToRemove != null)
            {
                dataGridView.Rows.Remove(rowToRemove);
                return true;
            }

            return false;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // EXPORT / IMPORT EXCEL
        // ══════════════════════════════════════════════════════════════════════════

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count == 0)
            {
                MessageBox.Show("Please add a product first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var exportTable = new DataTable { TableName = "Products" };
            exportTable.Columns.Add("ProductID", typeof(int));
            exportTable.Columns.Add("ProductName", typeof(string));
            exportTable.Columns.Add("Qty", typeof(int));
            exportTable.Columns.Add("ProductType", typeof(string));
            exportTable.Columns.Add("SalePrice", typeof(string));

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells[Col.ProductId].Value == null) continue;

                string pidVal = row.Cells[Col.ProductId].Value?.ToString();
                if (!int.TryParse(row.Cells[Col.Qty].Value?.ToString(), out int qty)) continue;
                if (!float.TryParse(row.Cells[Col.SalePrice].Value?.ToString(), out float price)) continue;

                exportTable.Rows.Add(
                    string.IsNullOrEmpty(pidVal) ? (object)DBNull.Value : int.Parse(pidVal),
                    row.Cells[Col.UrduName].Value?.ToString(),
                    qty,
                    row.Cells[Col.ProductType].Value?.ToString(),
                    price);
            }

            using (var sfd = new SaveFileDialog
            { Filter = "Excel Workbook (*.xlsx)|*.xlsx", FileName = "CustomerOrder.xlsx" })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                using (var wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(exportTable, "CustomerOrderSheet");
                    wb.SaveAs(sfd.FileName);
                }
                MessageBox.Show("Export successful!");
            }
        }

        private void BrowsOrderExcelFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|All files|*.*",
                Title = "Select an Excel File"
            })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;
                ImportUpdatedFilePathTxt.Text = ofd.FileName;
                LoadOrderExcelFileBtn.Enabled = true;
            }
        }

        private void LoadOrderExcelFileBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var stream = File.Open(ImportUpdatedFilePathTxt.Text,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    System.Text.Encoding.RegisterProvider(
                        System.Text.CodePagesEncodingProvider.Instance);

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var conf = new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ =>
                                new ExcelDataTableConfiguration { UseHeaderRow = true }
                        };

                        var ds = reader.AsDataSet(conf);
                        if (ds.Tables.Count == 0)
                        {
                            MessageBox.Show("No worksheets found.", "No data",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        CartProductList.Rows.Clear();

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (!decimal.TryParse(row[4].ToString(), out decimal sp)) continue;
                            if (!int.TryParse(row[2].ToString(), out int qty)) continue;

                            sp = Math.Round(sp, 1);
                            CartProductList.Rows.Add(null, sp * qty, sp, row[1].ToString(),
                                row[3].ToString(), qty, row[0].ToString(), null);
                        }
                        CalculateTotals();
                    }
                }

                ImportUpdatedFilePathTxt.Text = string.Empty;
                InvoicePageTabControl.SelectedTab = BilPad;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════════════════════════════════
        // CLEAR / RESET
        // ══════════════════════════════════════════════════════════════════════════

        private void ClearInputs()
        {
            PId = string.Empty;
            prod_U_Name = string.Empty;
            ProductEngNameTxt.Clear();
            ProductSalePrice.Clear();
            P_StockQtyTxt.Clear();
            ProductAmount.Clear();
            Prod_Qty.Clear();
            prod_ItemCountTxt.Clear();
            ProductDetailTxt.Clear();
            prodStockUnit.Clear();
            productTypeDropdown.SelectedIndex = -1;
            OtherProductChk.Checked = false;
            ProductOrderHistoryDataGrid.DataSource = null;
            ProductPriceDataGridView.DataSource = null;

        }

        private void ClearCartFunction()
        {
            PId = customerId = string.Empty;
            CustomerNameTxt.Text = CustomerIdLbl.Text = string.Empty;
            CartProductList.Rows.Clear();
            ResetCustomerBtn.Visible = false;
            PreviousOrderIdLbl.Text = string.Empty;
            isTempSaved = false;
            isPaid = false;
            TotalItemLbl.Text = "0";
            TotalAmountLbl.Text = "0";
            ReceivedAmountTxt.Clear();

            string invRef = TextFormatHelper.GetPrefix(Properties.Settings.Default.UserName);
            InvoiceNoLbl.Text = invRef + DateTime.Now.ToString("ddMMyy-HHmmss");
            ApplyAdvanceChk.Visible = false;
        }

        private void ClearCustomerPreviousTransactionGroup()
        {
            previousBillAmountLbl.Text = "0";
            PreviousReceivedAmountLbl.Text = "0";
            PreviousOrderSummaryLbl.Visible = false;
        }

        private void ResetUIAfterSave()
        {
            ClearInputs();
            ClearCartFunction();
            ClearCustomerPreviousTransactionGroup();
            ResetCustomerBtn.Visible = false;
            string invRef = TextFormatHelper.GetPrefix(Properties.Settings.Default.UserName);
            InvoiceNoLbl.Text = invRef + DateTime.Now.ToString("ddMMyy-HHmmss");
        }

        private void ClearCartBtn_Click(object sender, EventArgs e)
        {
            ClearCartFunction();
            ClearInputs();
            ClearCustomerPreviousTransactionGroup();
            ProductEngNameTxt.Focus();
            MessageBox.Show("Cart cleared successfully!", "Clear Cart",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetCustomerBtn_Click(object sender, EventArgs e)
        {
            CustomerNameTxt.Text = string.Empty;
            customerId = string.Empty;
            CustomerIdLbl.Text = string.Empty;
            ResetCustomerBtn.Enabled = true;
            ResetCustomerBtn.Visible = false;

            lblAdvanceInfo.Visible = false;
            ApplyAdvanceChk.Visible = false;
            ProductOrderHistoryDataGrid.DataSource = null;
            ProductOrderHistoryDataGrid.ClearSelection();
            ClearCustomerPreviousTransactionGroup();
        }

        // ══════════════════════════════════════════════════════════════════════════
        // VALIDATION
        // ══════════════════════════════════════════════════════════════════════════

        private bool ValidateInputs()
        {
            if (!OtherProductChk.Checked && string.IsNullOrWhiteSpace(PId))
            {
                MessageBox.Show("Product ID is required.", "Validation Error");
                return false;
            }
            if (string.IsNullOrWhiteSpace(ProductEngNameTxt.Text))
            {
                MessageBox.Show("Product name is required.", "Validation Error");
                return false;
            }
            if (!OtherProductChk.Checked && string.IsNullOrWhiteSpace(prod_U_Name))
            {
                MessageBox.Show("Unit name is required.", "Validation Error");
                return false;
            }
            if (!int.TryParse(P_StockQtyTxt.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Enter a valid quantity.", "Validation Error");
                return false;
            }
            if (productTypeDropdown.SelectedItem == null)
            {
                MessageBox.Show("Please select a product type.", "Validation Error");
                return false;
            }
            if (!decimal.TryParse(ProductSalePrice.Text, out _))
            {
                MessageBox.Show("Enter a valid sale price.", "Validation Error");
                return false;
            }
            return true;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // KEYBOARD SHORTCUTS
        // ══════════════════════════════════════════════════════════════════════════

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S:
                    SaveAndPrintOrderBtn.PerformClick(); return true;
                case Keys.Control | Keys.T:
                    SaveBillBtn.PerformClick(); return true;
                case Keys.Control | Keys.P:
                    PrintPreviewBtn.PerformClick(); return true;
                case Keys.Control | Keys.N:
                    ClearCartBtn.PerformClick(); return true;
                case Keys.Control | Keys.D:
                    SaveOrderWithoutPrintBtn.PerformClick(); return true;
                case Keys.Control | Keys.R:
                    GenerateInvoicePdfBtn.PerformClick(); return true;
                case Keys.Control | Keys.E:
                    ExportBtn.PerformClick(); return true;
                case Keys.Control | Keys.G:
                    TemOrderBtn.PerformClick(); return true;
                case Keys.Control | Keys.O:
                    SearchInvoiceLink_LinkClicked(null, null); return true;
                case Keys.Control | Keys.Q:
                    GotoFirstRow(); return true;
                case Keys.Control | Keys.D1:
                    ProductEngNameTxt.Focus();
                    ProductEngNameTxt.SelectAll(); return true;
                case Keys.Control | Keys.D2:
                    CustomerNameTxt.Focus();
                    CustomerNameTxt.SelectAll(); return true;
                case Keys.Alt | Keys.R:
                    ReceivedAmountTxt.Focus();
                    ReceivedAmountTxt.SelectAll(); return true;
                case Keys.Escape:
                    ProductEngNameTxt.Focus();
                    ProductEngNameTxt.SelectAll();
                    SuggestionGrid.Visible = false; return true;
                case Keys.F1:
                    CustomerNameTxt.Focus();
                    CustomerNameTxt.SelectAll(); return true;
                case Keys.Control | Keys.W: AllowApplyAdvanceCheck(); return true;
                case Keys.Control | Keys.Space: CustomerLagerRecordChk.Checked = !CustomerLagerRecordChk.Checked; return true;

                case Keys.Alt | Keys.F4:
                    this.Close(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AllowApplyAdvanceCheck()
        {
            ApplyAdvanceChk.Checked = !ApplyAdvanceChk.Checked;
            //   _shouldAddWithBill = !_shouldAddWithBill;

            Console.WriteLine($"The Value of ShouldAllow Bill :{_shouldAddWithBill}");
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                if (SuggestionGrid.Visible && ActiveControl == SuggestionGrid) return;
                if (ActiveControl == ProductEngNameTxt || ActiveControl == CustomerNameTxt ||
                    ActiveControl == TopBarSearchProductTxt || ActiveControl == CustomerListDataGrid)
                    return;

                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (SuggestionGrid.Visible && ActiveControl == SuggestionGrid) return;
                if (ActiveControl == ProductEngNameTxt || ActiveControl == CustomerNameTxt ||
                    ActiveControl == TopBarSearchProductTxt || ActiveControl == CustomerListDataGrid)
                    return;

                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void GotoFirstRow()
        {
            if (CartProductList.Rows.Count == 0) return;
            CartProductList.ClearSelection();
            CartProductList.Rows[0].Selected = true;
            CartProductList.CurrentCell = CartProductList.Rows[0].Cells[1];
            CartProductList.Focus();
        }

        // ══════════════════════════════════════════════════════════════════════════
        // MISC EVENT HANDLERS
        // ══════════════════════════════════════════════════════════════════════════

        private void ProductEngNameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter || OtherProductChk.Checked) return;
            e.Handled = true;

            if (!SuggestionGrid.Visible)
                _ = TriggerProductSearchAsync(ProductEngNameTxt.Text);
            else
                SuggestionGrid.Visible = false;
        }

        private void ProductEngNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && SuggestionGrid.Visible &&
                SuggestionGrid.Rows.Count > 0)
            {
                SuggestionGrid.Focus();
                SuggestionGrid.Rows[0].Selected = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape && SuggestionGrid.Visible)
            {
                SuggestionGrid.Visible = false;
                ProductEngNameTxt.Focus();
                e.Handled = true;
            }
        }

        private void CustomerNameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            e.Handled = true;

            if (!CustomerListDataGrid.Visible)
                _ = ShowCustomerSuggestionsAsync(CustomerNameTxt.Text);
            else
                CustomerListDataGrid.Visible = false;
        }

        private void CustomerNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && CustomerListDataGrid.Visible &&
                CustomerListDataGrid.Rows.Count > 0)
            {
                CustomerListDataGrid.Focus();
                CustomerListDataGrid.Rows[0].Selected = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape && CustomerListDataGrid.Visible)
            {
                CustomerListDataGrid.Visible = false;
                ProductEngNameTxt.Focus();
                e.Handled = true;
            }
        }

        private void ProductEngNameTxt_Enter(object sender, EventArgs e)
            => CustomerListDataGrid.Visible = false;

        private void CustomerNameTxt_Enter(object sender, EventArgs e)
            => SuggestionGrid.Visible = false;

        private void ProductSalePrice_Enter(object sender, EventArgs e)
            => ProductSalePrice.SelectAll();

        private void P_StockQtyTxt_Enter(object sender, EventArgs e)
            => P_StockQtyTxt.SelectAll();

        private void ReceivedAmountTxt_TextChange(object sender, EventArgs e)
            => CalculateReturnAmount();

        private void ProductSalePrice_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProductSalePrice.Text) &&
                !string.IsNullOrEmpty(P_StockQtyTxt.Text) &&
                decimal.TryParse(ProductSalePrice.Text, out decimal sp) &&
                int.TryParse(P_StockQtyTxt.Text, out int qty))
                ProductAmount.Text = (sp * qty).ToString();
        }

        private void P_StockQtyTxt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProductSalePrice.Text) &&
                !string.IsNullOrEmpty(P_StockQtyTxt.Text) &&
                decimal.TryParse(ProductSalePrice.Text, out decimal sp) &&
                int.TryParse(P_StockQtyTxt.Text, out int qty))
                ProductAmount.Text = (sp * qty).ToString();
        }

        private void P_StockQtyTxt_TextChange(object sender, EventArgs e)
        {
            string valid = RegexValidator.ValidateCommonPattern(
                P_StockQtyTxt.Text, ValidationPattern.NumbersOnly, "quantityField");
            if (P_StockQtyTxt.Text == valid) return;
            P_StockQtyTxt.Text = valid;
            P_StockQtyTxt.SelectionStart = valid.Length;
        }

        private void ProductSalePrice_TextChange(object sender, EventArgs e)
        {
            string valid = RegexValidator.ValidateCommonPattern(
                ProductSalePrice.Text, ValidationPattern.NumbersWithDecimal, "salePriceField");
            if (ProductSalePrice.Text == valid) return;
            ProductSalePrice.Text = valid;
            ProductSalePrice.SelectionStart = valid.Length;
        }

        private void productTypeDropdown_Enter(object sender, EventArgs e)
            => productTypeDropdown.BorderColor = Color.BlueViolet;

        private void productTypeDropdown_Leave(object sender, EventArgs e)
            => productTypeDropdown.BorderColor = Color.Silver;

        
        private void BackScreenBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count != 0)
                MessageBox.Show("Please clear the cart first.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void TopBarSearchProductTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            e.Handled = true;

            var form = new Form
            {
                Text = "Product Form",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 1050,
                Height = 625
            };
            var ctrl = new POS_Shop.Views.Controllers.Product.ProductListControl
            { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.ShowDialog();
        }

        private async void SearchInvoiceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PreviousOrderIdLbl.Text = string.Empty;

            var form = new Form { Text = "Order List", StartPosition = FormStartPosition.CenterScreen };
            var screen = Screen.PrimaryScreen;
            if (screen.Bounds.Width * screen.Bounds.Height < 1327104)
                form.WindowState = FormWindowState.Maximized;
            else
            {
                form.Width = 1390;
                form.Height = 730;
            }

            var ctrl = new Views.Controllers.Order.OrdersControlUI { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.ShowDialog();

            if (!ctrl.isRecordSelected) return;

            if (MessageBox.Show("Clear current cart before loading order?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            ClearCustomerPreviousTransactionGroup();
            InvoiceNoLbl.Text = ctrl.InvoiceNoLbl.Text;
            PreviousOrderIdLbl.Text = ctrl.OrderIDLbl.Text;
            TotalAmountLbl.Text = ctrl.TotalBill.ToString();
            ReceivedAmountTxt.Text = ctrl.ReceiveAmount.ToString();

            if (ctrl.CustomerId != 0)
            {
                _isUpdatingText = true;  // ← ADD THIS
                CustomerIdLbl.Text = ctrl.CustomerId.ToString();
                CustomerNameTxt.Text = ctrl.CustomerName;
                ResetCustomerBtn.Visible = ResetCustomerBtn.Enabled = true;
                CustomerListDataGrid.Visible = false;

                _isUpdatingText = false;
                await LoadCustomerLedgerBalanceAsync(ctrl.CustomerId);
            }
            else
            {
                _customerAdvanceBalance = 0;
                ApplyAdvanceChk.Visible = false;
                ResetCustomerBtn.Visible = false;
            }
        }

        private void AddNewCustomerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new Form
            {
                Text = "Add New Customer",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 1050,
                Height = 625
            };
            var ctrl = new Views.Controllers.Customers.CustomerFormControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.ShowDialog();
        }

        // ══════════════════════════════════════════════════════════════════════════
        // ADMIN — TRUNCATE TABLES
        // ══════════════════════════════════════════════════════════════════════════

        private void TruncateOrder_OrderDetailBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete ALL orders? This cannot be undone.", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            using (var ctx = new POSDbContext())
            {
                ctx.Database.ExecuteSqlCommand(
                    "ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]");
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[OrderDetails]");
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[Orders]");
                ctx.Database.ExecuteSqlCommand(@"ALTER TABLE [dbo].[OrderDetails]
                    ADD CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]
                    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id])");
            }

            MessageBox.Show("Records deleted.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            InvoicePageTabControl.SelectedTab = BilPad;
        }

        private void ClearProductTblBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete ALL products and orders? This cannot be undone.",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            using (var ctx = new POSDbContext())
            {
                ctx.Database.ExecuteSqlCommand(
                    "ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]");
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[OrderDetails]");
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[Orders]");
                ctx.Database.ExecuteSqlCommand(@"ALTER TABLE [dbo].[OrderDetails]
                    ADD CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]
                    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id])");
                ctx.Database.ExecuteSqlCommand("DELETE FROM Products");
                ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Products', RESEED, 0)");
            }

            MessageBox.Show("Records deleted.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            InvoicePageTabControl.SelectedTab = BilPad;
        }

        private void ClearTempOrderTabls_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all temporary orders?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            using (var ctx = new POSDbContext())
            {
                ctx.Database.ExecuteSqlCommand("DELETE FROM TempOrderDetails");
                ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('TempOrderDetails', RESEED, 0)");
                ctx.Database.ExecuteSqlCommand("DELETE FROM TempOrders");
            }

            MessageBox.Show("Records deleted.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            InvoicePageTabControl.SelectedTab = BilPad;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // FORM LIFECYCLE
        // ══════════════════════════════════════════════════════════════════════════

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (CartProductList.Rows.Count != 0)
            {
                MessageBox.Show("Please clear the cart first.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productDebounceTimer?.Dispose();
                _customerDebounceTimer?.Dispose();
                _productSearchCts?.Dispose();
                _customerSearchCts?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        // ══════════════════════════════════════════════════════════════════════════
        // ERROR LOGGING  (replace with your real logger if available)
        // ══════════════════════════════════════════════════════════════════════════

        private static void LogError(string context, Exception ex)
        {
            try
            {
                string logPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "pos_errors.log");
                File.AppendAllText(logPath,
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{context}] " +
                    $"{ex.Message}\n{ex.StackTrace}\n\n");
            }
            catch { /* logging must never crash the app */ }
        }




        // ─── 2. METHOD: Call this right after a customer is selected ─────────────────

        /// <summary>
        /// After customer is selected in BillPadForm, check for any advance credit.
        /// Call this from your existing customer selection handler.
        /// </summary>
        private async Task LoadCustomerLedgerBalanceAsync(int customerId)
        {
            using (var context = new POSDbContext())
            {
                var repo = new CustomerLedgerRepository(context);
                decimal balance = await repo.GetCurrentBalanceAsync(customerId);
                _customerAdvanceBalance = Convert.ToInt32(balance);
                _advanceApplied = 0;

                if (balance < 0) // Has advance credit
                {
                    decimal advCredit = Math.Abs(balance);
                    lblAdvanceInfo.Visible = false;
                    lblAdvanceInfo.Text = $"🔵 Advance Credit: PKR {advCredit:N2}";
                    lblAdvanceInfo.ForeColor = Color.FromArgb(0, 102, 204);
                    ApplyAdvanceChk.Visible = true;
                    ApplyAdvanceChk.Text = $"🔴 Apply Advance (PKR {advCredit:N2})";
                    ApplyAdvanceChk.ForeColor = Color.FromArgb(0, 102, 204);
                    ApplyAdvanceChk.Checked = false;
                    _isAdvanceBalance = true;
                }
                else if (balance > 0) // Has outstanding loan
                {
                    lblAdvanceInfo.Visible = false;
                    lblAdvanceInfo.Text = $"🔴 Outstanding Loan: PKR {balance:N2}";
                    lblAdvanceInfo.ForeColor = Color.FromArgb(192, 0, 0);
                    ApplyAdvanceChk.Visible = true;
                    ApplyAdvanceChk.Text = $"🔴 Apply Loan (PKR {balance:N2})";
                    ApplyAdvanceChk.ForeColor = Color.FromArgb(192, 0, 0);
                    ApplyAdvanceChk.Checked = false;
                    _isAdvanceBalance = false;
                }
                else
                {
                    lblAdvanceInfo.Visible = false;
                    ApplyAdvanceChk.Visible = false;
                }
            }
        }

        // ─── 3. EVENT: When "Apply advance" checkbox is toggled ──────────────────────

        //private void ApplyAdvanceChk_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ApplyAdvanceChk.Checked && _customerAdvanceBalance < 0)
        //    {
        //        decimal advCredit = Math.Abs(_customerAdvanceBalance);
        //        // decimal total = GetCurrentCartTotal(); // your existing method

        //        decimal total = Convert.ToDecimal(TotalAmountLbl.Text);
        //        _advanceApplied = Math.Min(advCredit, total);
        //        ReceivedAmountTxt.Text =Convert.ToInt32(_advanceApplied).ToString();
        //        // This will auto-trigger your existing CalculateReturnAmount
        //    }
        //    else
        //    {
        //        _advanceApplied = 0;
        //        ReceivedAmountTxt.Text = "0";
        //    }
        //    CalculateTotals(); // your existing method
        //}

        private bool _isProcessingCheck = false;
        private bool _shouldAddWithBill = false;
        //private void ApplyAdvanceChk_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text))
        //    {
        //        MessageBox.Show("While Updating you can't Perform this action", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        ApplyAdvanceChk.Checked = false;
        //        return;
        //    }
        //    if (ApplyAdvanceChk.Checked)
        //    {
        //        _shouldAddWithBill = true;
        //        // Checkbox is checked - Add the row
        //        if (_customerAdvanceBalance < 0)
        //        {
        //            ProductEngNameTxt.Text = "سابقہ ایڈوانس جمع";
        //        }
        //        else
        //        {
        //            ProductEngNameTxt.Text = "سابقہ ادھر جمع";
        //        }

        //        CartProductList.Rows.Add(null, _customerAdvanceBalance, _customerAdvanceBalance, ProductEngNameTxt.Text,
        //                               "سابقہ", 1, "", "");
        //    }
        //    else
        //    {
        //        // Checkbox is unchecked - Remove the "سابقہ" row
        //        for (int i = CartProductList.Rows.Count - 1; i >= 0; i--)
        //        {
        //            DataGridViewRow row = CartProductList.Rows[i];
        //            if (row.Cells["Urdu Name"]?.Value?.ToString() == "سابقہ" ||
        //                row.Cells["Urdu Name"]?.Value?.ToString() == "سابقہ ایڈوانس جمع" ||
        //                row.Cells["Urdu Name"]?.Value?.ToString() == "سابقہ ادھر جمع")
        //            {
        //                CartProductList.Rows.RemoveAt(i);
        //            }
        //        }
        //        _shouldAddWithBill = false;
        //    }

        //    // Recalculate totals after removal
        //    CalculateTotals();
        //    CalculateReturnAmount();
        //    ClearInputs();
        //    ProductEngNameTxt.Focus();
        //}


        private bool _isInternalChange = false;
        private async void ApplyAdvanceChk_CheckedChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text))
            {
                // Only show message and prevent if it was being checked
                if (ApplyAdvanceChk.Checked)
                {
                    ApplyAdvanceChk.Checked = false;
                    MessageBox.Show("Now you are Updating this Order, So you can't Perform this action", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                return;
            }
            if (!string.IsNullOrEmpty(CustomerIdLbl.Text) && _customerAdvanceBalance != 0)
            {
                if (_isInternalChange)
                {
                    _shouldAddWithBill = true;
                    _isInternalChange = false;
                    return;
                }

                if (ApplyAdvanceChk.Checked)
                {
                    _shouldAddWithBill = true;
                    // Checkbox is checked - Add the row
                    if (_customerAdvanceBalance < 0)
                    {
                        ProductEngNameTxt.Text = "سابقہ ایڈوانس جمع";
                    }
                    else
                    {
                        ProductEngNameTxt.Text = "سابقہ ادھار جمع";
                    }

                    CartProductList.Rows.Add(null, _customerAdvanceBalance, _customerAdvanceBalance, ProductEngNameTxt.Text, "سابقہ", 1, "", "");
                }
                else
                {
                    // Checkbox is unchecked - Remove the "سابقہ" row
                    for (int i = CartProductList.Rows.Count - 1; i >= 0; i--)
                    {
                        DataGridViewRow row = CartProductList.Rows[i];
                        if (row.Cells["Urdu Name"]?.Value?.ToString() == "سابقہ" ||
                            row.Cells["Urdu Name"]?.Value?.ToString() == "سابقہ ایڈوانس جمع" ||
                            row.Cells["Urdu Name"]?.Value?.ToString() == "سابقہ ادھار جمع")
                        {
                            CartProductList.Rows.RemoveAt(i);
                        }
                    }
                    _shouldAddWithBill = false;
                }

                // Recalculate totals after removal
                CalculateTotals();
                CalculateReturnAmount();
                ClearInputs();
                ProductEngNameTxt.Focus();
                Console.WriteLine($"The Value of ShouldAllow Bill :{_shouldAddWithBill}");
            }
        }

        private void LedgerEntryFromBtn_Click(object sender, EventArgs e)
        {
            var purchaseOrderForm = new POS_Shop.Views.CustomerLoanScreensV1.ManualLedgerEntryForm();
            purchaseOrderForm.ShowDialog();
        }



        private bool _isUpdating = false;
        private void CustomerLagerRecordChk_CheckedChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;

            _isUpdating = true;

            try
            {
                if (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text))
                {
                    MessageBox.Show("While Updating you can't Perform this action", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    CustomerLagerRecordChk.Checked = false;
                    return;
                }

                // Validate Customer Name
                if (string.IsNullOrEmpty(CustomerNameTxt.Text))
                {
                    CustomerLagerRecordChk.Checked = false;
                    return;
                }

                // Parse Total Amount
                if (!decimal.TryParse(TotalAmountLbl.Text, out decimal totalAmount) || totalAmount == 0)
                {
                    CustomerLagerRecordChk.Checked = false;
                    return;
                }

                // Parse Received Amount (only if not empty)
                if (string.IsNullOrEmpty(ReceivedAmountTxt.Text))
                {
                    CustomerLagerRecordChk.Checked = false;
                    return;
                }

                if (!decimal.TryParse(ReceivedAmountTxt.Text, out decimal receivedAmount))
                {
                    CustomerLagerRecordChk.Checked = false;
                    return;
                }

                // Keep checked only if NOT full payment
                if (receivedAmount == totalAmount)
                {
                    CustomerLagerRecordChk.Checked = false;
                }
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private void LedgerListBtn_Click(object sender, EventArgs e)
        {
            var ledgerListForm = new POS_Shop.Views.CustomerLoanScreensV1.AllCustomerBalancesForm();
            ledgerListForm.ShowDialog();
        }

        private void QRCodeBtn_Click(object sender, EventArgs e)
        {
            using (var f = new ImageManagementForm())
            {
                //f.Icon = new Icon(Application.StartupPath + "/pos_icon.ico");
                f.ShowDialog(this);
            }
        }
    }
}

