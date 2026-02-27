//using ClosedXML.Excel;
//using ExcelDataReader;
//using POS_Shop.Constants;
//using POS_Shop.DTOs.Order;
//using POS_Shop.DTOs.Product;
//using POS_Shop.Helpers;
//using POS_Shop.Interfaces;
//using POS_Shop.Models;
//using POS_Shop.Models.AuthModel;
//using POS_Shop.Repositories;
//using POS_Shop.Views.Controllers.Order;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Data.Entity;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.Drawing.Printing;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using static Org.BouncyCastle.Math.EC.ECCurve;
//using Color = System.Drawing.Color;
//using Font = System.Drawing.Font;
//using Order = POS_Shop.Models.Order;
//using Rectangle = System.Drawing.Rectangle;

//namespace POS_Shop.Views.BillScreen
//{
//    public partial class BillPadForm : Form
//    {
//        string PId { get; set; }
//        string customerId { get; set; } = string.Empty;
//        public string prod_U_Name { get; set; }
//        public bool isTempSaved { get; set; } = false;
//        public bool isPaid { get; set; } = false;


//        // ─── Add these fields to BillPadForm ───────────────────────────────────────
//        private CancellationTokenSource _searchCts;
//        private System.Windows.Forms.Timer _debounceTimer;

//        private System.Windows.Forms.Timer _customerDebounceTimer;

//        // ─── Replace InitializeComponent() or add to constructor ───────────────────
//        private void InitializeDebounceTimer()
//        {
//            _debounceTimer = new System.Windows.Forms.Timer { Interval = 250 };
//            _debounceTimer.Tick += async (s, e) =>
//            {
//                _debounceTimer.Stop();
//                await TriggerProductSearchAsync(ProductEngNameTxt.Text);
//            };

//            // Customer timer (NEW)
//            _customerDebounceTimer = new System.Windows.Forms.Timer { Interval = 300 };
//            _customerDebounceTimer.Tick += async (s, e) =>
//            {
//                _customerDebounceTimer.Stop();
//                await ShowCustomerSuggestionsAsync(CustomerNameTxt.Text);
//            };
//        }
//        public BillPadForm()
//        {
//            InitializeComponent();

//            // Initialize form state with default values
//            CustomerIdLbl.Text = string.Empty;
//            CustomerNameTxt.Text = string.Empty;
//            PreviousOrderIdLbl.Text = string.Empty;
//            string invRef = TextFormatHelper.GetPrefix(Properties.Settings.Default.UserName);
//            InvoiceNoLbl.Text = invRef + DateTime.Now.ToString("ddMMyy-HHmmss");
//            this.Shown += (s, e) => { ProductEngNameTxt.Focus(); };

//            CustomerListDataGrid.BringToFront();
//            SetItemGridView();

//            this.KeyPreview = true;
//            this.KeyDown += Form_KeyDown;

//            // Use .ToString() or check for null
//            string savedRole = Properties.Settings.Default.UserRole?.ToString() ?? string.Empty;

//            if (!savedRole.Equals(AuthUserRole.SuperAdmin.ToString(), StringComparison.OrdinalIgnoreCase))
//            {
//                InvoicePageTabControl.TabPages.Remove(TruncateTableTab);
//                // InvoicePageTabControl.TabPages.Remove(ImpoertOrderFileTab);
//            }
//            InitializeProductUnitsDropdown();

//            InitializeDebounceTimer();
//        }


//        // ─── New: centralized search trigger with cancellation ─────────────────────
//        private async Task TriggerProductSearchAsync(string searchText)
//        {
//            // Cancel any previous in-flight search
//            _searchCts?.Cancel();
//            _searchCts?.Dispose();
//            _searchCts = new CancellationTokenSource();
//            var token = _searchCts.Token;

//            try
//            {
//                var suggestions = await GetProductSuggestionsAsync(searchText, token);
//                if (token.IsCancellationRequested) return;

//                if (!suggestions.Any())
//                {
//                    SuggestionGrid.Visible = false;
//                    return;
//                }

//                BindSuggestionGrid(suggestions);
//            }
//            catch (OperationCanceledException) { /* Search was superseded — ignore */ }
//            catch (Exception ex)
//            {
//                SuggestionGrid.Visible = false;
//                // Log ex here
//            }
//        }


//        // ─── Replace GetProductSuggestions with cancellation-aware version ──────────
//        private async Task<List<ProductSuggestion>> GetProductSuggestionsAsync(
//            string searchText, CancellationToken ct = default)
//        {
//            var searchWords = string.IsNullOrWhiteSpace(searchText)
//                ? Array.Empty<string>()
//                : searchText.Trim().ToLowerInvariant()
//                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

//            using (var ctx = new POSDbContext())
//            {
//                // No filter — return top 100 by Id
//                if (searchWords.Length == 0)
//                {
//                    const string sql = @"
//                SELECT TOP 100
//                    Id             AS ProductId,
//                    ProductEnglishName AS ProductName,
//                    ProductUrduName,
//                    Qty,
//                    PurchasePrice
//                FROM Products WITH (NOLOCK)
//                ORDER BY Id";

//                    return await ctx.Database.SqlQuery<ProductSuggestion>(sql)
//                                    .ToListAsync(ct);
//                }

//                // Single word — simple LIKE
//                if (searchWords.Length == 1)
//                {
//                    const string sql = @"
//                SELECT TOP 100
//                    p.Id             AS ProductId,
//                    p.ProductEnglishName AS ProductName,
//                    p.ProductUrduName,
//                    p.Qty,
//                    p.PurchasePrice
//                FROM Products p WITH (NOLOCK)
//                WHERE p.ProductEnglishName    LIKE @p0
//                   OR p.SearchByProductCode   LIKE @p0
//                   OR CAST(p.Id AS VARCHAR(20)) LIKE @p0
//                ORDER BY p.Id";

//                    var param = new SqlParameter("@p0", $"%{searchWords[0]}%");
//                    return await ctx.Database.SqlQuery<ProductSuggestion>(sql, param)
//                                    .ToListAsync(ct);
//                }

//                // Multiple words — all must match (AND logic)
//                return await ExecuteMultiWordSearchAsync(searchWords, ctx, ct);
//            }
//        }

//        private async Task<List<ProductSuggestion>> ExecuteMultiWordSearchAsync(
//            string[] words, POSDbContext context, CancellationToken ct)
//        {
//            var parameters = new SqlParameter[words.Length];
//            var whereConditions = new string[words.Length];

//            for (int i = 0; i < words.Length; i++)
//            {
//                string pName = $"@w{i}";
//                parameters[i] = new SqlParameter(pName, $"%{words[i]}%");
//                whereConditions[i] = $@"(p.ProductEnglishName    LIKE {pName}
//                              OR p.SearchByProductCode   LIKE {pName}
//                              OR CAST(p.Id AS VARCHAR(20)) LIKE {pName})";
//            }

//            string sql = $@"
//        SELECT TOP 100
//            p.Id             AS ProductId,
//            p.ProductEnglishName AS ProductName,
//            p.ProductUrduName,
//            p.Qty,
//            p.PurchasePrice
//        FROM Products p WITH (NOLOCK)
//        WHERE {string.Join(" AND ", whereConditions)}
//        ORDER BY p.Id";

//            return await context.Database.SqlQuery<ProductSuggestion>(sql, parameters)
//                                .ToListAsync(ct);
//        }

//        private void BindSuggestionGrid(List<ProductSuggestion> suggestions)
//        {
//            var dt = new System.Data.DataTable();
//            dt.Columns.Add("ID", typeof(int));
//            dt.Columns.Add("Code", typeof(string));
//            dt.Columns.Add("Name", typeof(string));
//            dt.Columns.Add("U-Name", typeof(string));
//            dt.Columns.Add("Qty", typeof(int));

//            foreach (var item in suggestions)
//                dt.Rows.Add(item.ProductId, item.purchasePrice,
//                            item.ProductName,
//                            TextFormatHelper.FormatMixedText(item.ProductUrduName),
//                            item.Qty);

//            SuggestionGrid.ReadOnly = true;
//            SuggestionGrid.AllowUserToAddRows = false;
//            SuggestionGrid.DataSource = dt;

//            SuggestionGrid.Columns[0].Width = 40;
//            SuggestionGrid.Columns[1].Width = 50;
//            SuggestionGrid.Columns[2].Width = 200;
//            SuggestionGrid.Columns[3].Width = 200;

//            SuggestionGrid.Visible = true;
//            SuggestionGrid.BringToFront();
//        }

//        private void InitializeProductUnitsDropdown()
//        {
//            using (var context = new POSDbContext())
//            {
//                var productUnitRepo = new ProductUnitRepository(context);
//                var productUnit = productUnitRepo.GetAll().Select(s => new ProductUnit()
//                {
//                    Id = s.Id,
//                    Name = s.Name,

//                }).ToList();
//                productTypeDropdown.Items.Clear();

//                // Add default option
//                var allItems = new List<ProductUnit>();
//                //allItems.Add(new ProductUnit { Id = 0, Name = "" });
//                allItems.AddRange(productUnit);
//                productTypeDropdown.DataSource = allItems;
//                productTypeDropdown.DisplayMember = "Name";
//                productTypeDropdown.ValueMember = "Name";
//            }
//        }

//        private void Form_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
//            {
//                if (SuggestionGrid.Visible && this.ActiveControl == SuggestionGrid)
//                    return;

//                // Don't override Enter for these controls
//                if (this.ActiveControl == ProductEngNameTxt ||
//                    this.ActiveControl == CustomerNameTxt ||
//                    this.ActiveControl == TopBarSearchProductTxt ||
//                     this.ActiveControl == CustomerListDataGrid)
//                    return;

//                e.SuppressKeyPress = true;

//                this.SelectNextControl(
//                    this.ActiveControl,
//                    true, true, true, true
//                );
//            }
//            else if (e.KeyCode == Keys.Left)
//            {
//                if (SuggestionGrid.Visible && this.ActiveControl == SuggestionGrid)
//                    return;

//                // Don't override Enter for these controls
//                if (this.ActiveControl == ProductEngNameTxt ||
//                    this.ActiveControl == CustomerNameTxt ||
//                    this.ActiveControl == TopBarSearchProductTxt ||
//                     this.ActiveControl == CustomerListDataGrid)
//                    return;

//                e.SuppressKeyPress = true;

//                this.SelectNextControl(
//                    this.ActiveControl,
//                    false, true, true, true
//                );
//            }
//        }

//        private void SetItemGridView()
//        {
//            CartProductList.ColumnCount = 7;

//            CartProductList.Columns[0].Name = "Amount";
//            CartProductList.Columns[1].Name = "SalePrice";
//            CartProductList.Columns[2].Name = "Urdu Name";
//            CartProductList.Columns[3].Name = "ProductType";
//            CartProductList.Columns[4].Name = "Qty";
//            CartProductList.Columns[5].Name = "ProductId";
//            CartProductList.Columns[6].Name = "ProductDetail";

//            // Set column widths here
//            CartProductList.Columns[0].Width = 100;
//            CartProductList.Columns[1].Width = 60;
//            CartProductList.Columns[2].Width = 190;
//            CartProductList.Columns[3].Width = 30;
//            CartProductList.Columns[4].Width = 50;
//            CartProductList.Columns[5].Width = 50;

//            CartProductList.Columns[5].Visible = false;
//            CartProductList.Columns[6].Visible = false;

//            CartProductList.Columns["Amount"].ReadOnly = true; // Amount
//            CartProductList.Columns["Urdu Name"].ReadOnly = true; // Urdu Name
//            CartProductList.Columns["ProductType"].ReadOnly = true; // ProductType

//            // Add delete button column
//            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
//            btnCol.Name = "Delete";
//            btnCol.HeaderText = "Action";
//            btnCol.Text = "Delete";
//            btnCol.UseColumnTextForButtonValue = true;  // Always show "Delete"
//            // Insert at position 0 (first column)
//            CartProductList.Columns.Insert(0, btnCol);

//            // Set the width of the button column
//            CartProductList.Columns["Delete"].Width = 50;
//        }

//        protected override void OnFormClosing(FormClosingEventArgs e)
//        {
//            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
//            {
//                MessageBox.Show("Please Clear the Cart First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                e.Cancel = true;
//                return;
//            }
//            else
//            {
//                base.OnFormClosing(e);
//            }

//        }

//        private void BackScreenBtn_Click(object sender, EventArgs e)
//        {
//            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
//                MessageBox.Show("Please Clear the Cart First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            else
//            {
//                this.DialogResult = DialogResult.OK;
//                this.Close();
//            }
//        }

//        private bool ValidateInputs()
//        {
//            // Product ID
//            if (!OtherProductChk.Checked)
//            {
//                if (string.IsNullOrWhiteSpace(PId))
//                {
//                    MessageBox.Show("Product ID is required.", "Validation Error");
//                    return false;
//                }
//            }

//            // Product Name
//            if (string.IsNullOrWhiteSpace(ProductEngNameTxt.Text))
//            {
//                MessageBox.Show("Product name is required.", "Validation Error");
//                return false;
//            }

//            if (!OtherProductChk.Checked)
//            {
//                // Unit
//                if (string.IsNullOrWhiteSpace(prod_U_Name))
//                {
//                    MessageBox.Show("Unit name is required.", "Validation Error");
//                    return false;
//                }
//            }

//            // Quantity
//            if (!int.TryParse(P_StockQtyTxt.Text, out int qty) || qty <= 0)
//            {
//                MessageBox.Show("Enter a valid quantity.", "Validation Error");
//                return false;
//            }
//            // Product Type
//            if (productTypeDropdown.SelectedItem == null)
//            {
//                MessageBox.Show("Please select a product type.", "Validation Error");
//                return false;
//            }
//            // Price
//            if (!decimal.TryParse(ProductSalePrice.Text, out decimal salePrice))
//            {
//                MessageBox.Show("Enter a valid sale price.", "Validation Error");
//                return false;
//            }
//            return true; // ✅ Passed all checks
//        }

//        private void AddToCardBtn_Click(object sender, EventArgs e)
//        {
//            if (!ValidateInputs())
//                return; // stop if validation fails

//            // Get values from the TextBoxes
//            string productId = PId;
//            string productName = ProductEngNameTxt.Text;
//            string ProductUrduName = prod_U_Name;
//            string productType = productTypeDropdown.SelectedValue?.ToString();
//            decimal salePrice = Math.Round(decimal.Parse(ProductSalePrice.Text), 1);
//            int qty = int.Parse(P_StockQtyTxt.Text);
//            decimal amount = salePrice * qty;
//            string productDetail = ProductDetailTxt.Text;

//            bool productExists = false;
//            var finalName = OtherProductChk.Checked == false ? $"{ProductUrduName} {productDetail}" : $"{productName} {productDetail}";

//            string formattedText = TextFormatHelper.FormatMixedText(finalName);
//            var finalPId = OtherProductChk.Checked == false ? productId : "";


//           // checking the available stock
//            var config = ConfigurationManager.Configuration.Features.EnableUpdateQty;
//            if (config ==true && !string.IsNullOrEmpty(productId))
//            {
//                int availableQty = int.Parse(Prod_Qty.Text);
//                if (availableQty <= 0 || int.Parse(prod_ItemCountTxt.Text)<=0 || (int.Parse(prod_ItemCountTxt.Text) * qty) > availableQty)
//                {
//                    if(int.Parse(prod_ItemCountTxt.Text)<=0)
//                    {

//                        MessageBox.Show($"Product type '{productType}' is not properly configured against this Product. Item count must be greater than zero.",
//                                "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }
//                    MessageBox.Show($"Available stock is {availableQty} Pieces.Please enter a valid quantity.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//            }


//            // IMPROVED DUPLICATE CHECK - Compare multiple properties
//            foreach (DataGridViewRow row in CartProductList.Rows)
//            {
//                if (row.Cells["ProductId"].Value == null) continue;

//                string existingProductId = row.Cells["ProductId"].Value?.ToString();
//                string existingName = row.Cells["Urdu Name"].Value?.ToString();
//                string existingDetail = row.Cells["ProductDetail"].Value?.ToString();
//                decimal existingPrice = row.Cells["SalePrice"].Value != null ? Convert.ToDecimal(row.Cells["SalePrice"].Value) : 0;
//                string existingType = row.Cells["ProductType"].Value?.ToString();


//                string cleanExisting = TextFormatHelper.RemoveDirectionalCharacters(existingName);
//                string cleanNew = TextFormatHelper.RemoveDirectionalCharacters(formattedText);

//                if (string.Equals(
//                    cleanExisting.Trim(),
//                    cleanNew.Trim(),
//                    StringComparison.OrdinalIgnoreCase))
//                {
//                    // Product already exists → increase Qty & update Amount
//                    int existingQty = row.Cells["Qty"].Value != null ?
//                        int.Parse(row.Cells["Qty"].Value.ToString()) : 0;
//                    existingQty += qty;
//                    row.Cells["Qty"].Value = existingQty;

//                    decimal newAmount = existingQty * salePrice;
//                    row.Cells["Amount"].Value = Math.Round(newAmount, 1);
//                    productExists = true;
//                    break;
//                }

//            }

//            // If product doesn't exist, add a new row
//            if (!productExists)
//            {
//                CartProductList.Rows.Add(null, amount, salePrice, formattedText,
//                                       productType, qty, finalPId, productDetail);
//            }

//            CalculateTotals();
//            CalculateReturnAmount();

//            // Clear input fields after adding
//            ClearInputs();
//            ProductEngNameTxt.Focus();
//        }

//        private void CartProductList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
//        {
//            // Ensure the row index is valid
//            if (e.RowIndex >= 0)
//            {
//                var row = CartProductList.Rows[e.RowIndex];

//                try
//                {
//                    // Only recalc if Qty or SalePrice column changed
//                    if (CartProductList.Columns[e.ColumnIndex].Name == "Qty" ||
//                        CartProductList.Columns[e.ColumnIndex].Name == "SalePrice")
//                    {

//                        if (CartProductList.Columns[e.ColumnIndex].Name=="Qty")
//                        {
//                            var config = ConfigurationManager.Configuration.Features.EnableUpdateQty;
//                            if (config == true)
//                            {

//                                int productId = int.Parse(row.Cells["ProductId"].Value?.ToString());
//                                string existingType = row.Cells["ProductType"].Value?.ToString();
//                                int availableQty = int.Parse(row.Cells["Qty"].Value?.ToString());

//                                if (config == true && !string.IsNullOrEmpty(row.Cells["ProductId"].Value?.ToString()))
//                                {
//                                    using (var context = new POSDbContext())
//                                    {
//                                        var productQty = context.Products.FirstOrDefault(s => s.Id == productId).Qty;
//                                        var price = context.ProductPrices.Where(s => s.ProductId == productId && s.TypeName == existingType).Select(s => new ProdPricesdto()
//                                        {
//                                            price = s.Price,
//                                            ItemCount = s.ItemsCount
//                                        }).FirstOrDefault();

//                                        if (price != null)
//                                        {
//                                            if (availableQty <= 0 || (price.ItemCount * availableQty) > productQty)
//                                            {
//                                                MessageBox.Show($"Available stock is {productQty} Pieces. Please enter a valid quantity.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                                                return;
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                        decimal salePrice = Convert.ToDecimal(row.Cells["SalePrice"].Value);
//                        int qty = Convert.ToInt32(row.Cells["Qty"].Value);
//                        decimal newAmount = salePrice * qty;
//                        row.Cells["Amount"].Value = Math.Round(newAmount, 1);
//                        CalculateTotals();
//                        CalculateReturnAmount();
//                    }
//                }
//                catch
//                {
//                    MessageBox.Show("Invalid input. Please enter correct numeric values.");
//                    row.Cells[e.ColumnIndex].Value = 0; // reset wrong cell
//                }
//            }
//        }

//        private void CalculateTotals()
//        {
//            int totalItems = 0;
//            decimal subTotal = 0;

//            foreach (DataGridViewRow row in CartProductList.Rows)
//            {
//                // Count each row as 1 item (skip empty rows)
//                if (row.Cells[1].Value != null) // Check if product name exists
//                {
//                    totalItems++;
//                }
//                // Skip empty rows
//                if (row.Cells["Amount"].Value != null)
//                {
//                    subTotal += Convert.ToDecimal(row.Cells["Amount"].Value);
//                }
//            }

//            // Update your UI elements with the calculated totals
//            TotalItemLbl.Text = totalItems.ToString();
//            TotalAmountLbl.Text = subTotal.ToString();
//        }

//        private void ClearInputs()
//        {
//            PId = string.Empty;
//            ProductEngNameTxt.Clear();
//            prod_U_Name = string.Empty;
//            ProductSalePrice.Clear();
//            P_StockQtyTxt.Clear();
//            ProductAmount.Clear();
//            productTypeDropdown.SelectedIndex = -1;
//            ProductDetailTxt.Clear();
//            OtherProductChk.Checked = false;
//            Prod_Qty.Clear();
//            prod_ItemCountTxt.Clear();
//            ProductOrderHistoryDataGrid.DataSource = null;
//            ProductPriceDataGridView.DataSource = null;
//        }

//        private void ProductSalePrice_Leave(object sender, EventArgs e)
//        {
//            if (!string.IsNullOrEmpty(ProductSalePrice.Text) && !string.IsNullOrEmpty(P_StockQtyTxt.Text))
//            {
//                var amt = decimal.Parse(ProductSalePrice.Text) * int.Parse(P_StockQtyTxt.Text);
//                ProductAmount.Text = Convert.ToString(amt);
//            }
//        }

//        private void P_StockQtyTxt_Leave(object sender, EventArgs e)
//        {
//            if (!string.IsNullOrEmpty(ProductSalePrice.Text) && !string.IsNullOrEmpty(P_StockQtyTxt.Text))
//            {
//                var amt = decimal.Parse(ProductSalePrice.Text) * int.Parse(P_StockQtyTxt.Text);
//                ProductAmount.Text = Convert.ToString(amt);
//            }
//        }

//        private void P_StockQtyTxt_Enter(object sender, EventArgs e)
//        {
//            P_StockQtyTxt.SelectAll();
//        }

//        private void ProductEngNameTxt_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)Keys.Enter)
//            {
//                if (!OtherProductChk.Checked)
//                {
//                    if (SuggestionGrid.Visible == false)
//                    {
//                        ShowSuggestions(ProductEngNameTxt.Text);
//                    }
//                    else
//                    {
//                        SuggestionGrid.Visible = false;
//                    }
//                    e.Handled = true;
//                }
//            }
//        }

//        //private void ProductEngNameTxt_TextChange(object sender, EventArgs e)
//        //{
//        //    // Skip if we're programmatically updating
//        //    if (_isUpdatingText) return;


//        //    if ((string.IsNullOrEmpty(ProductEngNameTxt.Text) || ProductEngNameTxt.Text.Length < 2))
//        //    {
//        //        SuggestionGrid.Visible = false;
//        //        return;
//        //    }

//        //    if (ProductEngNameTxt.Text == _lastSelectedProductText) return;

//        //    if (OtherProductChk.Checked == false)
//        //    {
//        //        ShowSuggestions(ProductEngNameTxt.Text);
//        //    }
//        //}

//        // ─── Replace ProductEngNameTxt_TextChange ──────────────────────────────────
//        private void ProductEngNameTxt_TextChange(object sender, EventArgs e)
//        {
//            if (_isUpdatingText) return;
//            if (string.IsNullOrEmpty(ProductEngNameTxt.Text) || ProductEngNameTxt.Text.Length < 2)
//            {
//                _debounceTimer?.Stop();
//                SuggestionGrid.Visible = false;
//                return;
//            }
//            if (ProductEngNameTxt.Text == _lastSelectedProductText) return;
//            if (OtherProductChk.Checked) return;

//            // Restart debounce timer on every keystroke
//            _debounceTimer.Stop();
//            _debounceTimer.Start();
//        }



//        private void ProductEngNameTxt_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Down && SuggestionGrid.Visible)
//            {
//                if (SuggestionGrid.Rows.Count > 0)
//                {
//                    SuggestionGrid.Focus();
//                    SuggestionGrid.Rows[0].Selected = true;
//                    e.Handled = true;
//                }
//            }
//            else if (e.KeyCode == Keys.Escape && SuggestionGrid.Visible)
//            {
//                SuggestionGrid.Visible = false;
//                ProductEngNameTxt.Focus();
//                e.Handled = true;
//            }
//        }

//        //private async void ShowSuggestions(string searchText, bool isForCustomer = false)

//        //{
//        //    try
//        //    {
//        //        if (isForCustomer)
//        //        {
//        //            using (var context = new POSDbContext())
//        //            {
//        //                ICustomerRepository customerRepository = new CustomerRepository(context);
//        //                var result = await customerRepository.GetCustomerPagingListAsync(pageIndex: 1, pageSize: 100, searchText);

//        //                System.Data.DataTable dt1 = new System.Data.DataTable();
//        //                dt1.Columns.Add("ID", typeof(int));
//        //                dt1.Columns.Add("Name", typeof(string));
//        //                dt1.Columns.Add("Address", typeof(string));

//        //                foreach (var item in result.data)
//        //                {
//        //                    dt1.Rows.Add(item.Id, item.CustomerName, item.CustomerAddress);
//        //                }

//        //                CustomerListDataGrid.ReadOnly = true;
//        //                CustomerListDataGrid.AllowUserToAddRows = false;
//        //                CustomerListDataGrid.DataSource = dt1;
//        //                CustomerListDataGrid.Columns[0].Visible = false;

//        //                CustomerListDataGrid.BringToFront();
//        //            }
//        //            return;
//        //        }

//        //        // Get suggestions from your data source
//        //        var suggestions = await GetProductSuggestions(searchText);

//        //        if (suggestions.Any())
//        //        {
//        //            System.Data.DataTable dt = new System.Data.DataTable();
//        //            dt.Columns.Add("ID", typeof(int));
//        //            dt.Columns.Add("Code", typeof(string));
//        //            dt.Columns.Add("Name", typeof(string));
//        //            dt.Columns.Add("U-Name", typeof(string));
//        //            dt.Columns.Add("Qty", typeof(int));
//        //            //dt.Columns.Add("Sale-P", typeof(string));

//        //            foreach (var item in suggestions)
//        //            {
//        //                dt.Rows.Add(item.ProductId, item.purchasePrice, item.ProductName, TextFormatHelper.FormatMixedText(item.ProductUrduName), item.Qty);
//        //            }

//        //            SuggestionGrid.ReadOnly = true;
//        //            SuggestionGrid.AllowUserToAddRows = false;
//        //            SuggestionGrid.DataSource = dt;

//        //            SuggestionGrid.Columns[0].Width = 40;
//        //            SuggestionGrid.Columns[1].Width = 50;
//        //            SuggestionGrid.Columns[2].Width = 200;
//        //            SuggestionGrid.Columns[3].Width = 200;
//        //           // SuggestionGrid.Columns[5].Width = 75;
//        //            SuggestionGrid.Visible = true;
//        //            SuggestionGrid.BringToFront();
//        //        }
//        //        else
//        //        {
//        //            SuggestionGrid.Visible = false;
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        SuggestionGrid.Visible = false;
//        //        // Log error
//        //    }
//        //}

//        // ─── Update ShowSuggestions to just delegate (keeps customer search working) ─
//        private async void ShowSuggestions(string searchText, bool isForCustomer = false)
//        {
//            if (isForCustomer)
//            {
//                await ShowCustomerSuggestionsAsync(searchText);
//                // Customer search unchanged — kept here
//                //using (var context = new POSDbContext())
//                //{
//                //    ICustomerRepository customerRepository = new CustomerRepository(context);
//                //    var result = await customerRepository.GetCustomerPagingListAsync(1, 100, searchText);
//                //    // ... (your existing customer binding code, no change needed)

//                //    System.Data.DataTable dt1 = new System.Data.DataTable();
//                //    dt1.Columns.Add("ID", typeof(int));
//                //    dt1.Columns.Add("Name", typeof(string));
//                //    dt1.Columns.Add("Address", typeof(string));

//                //    foreach (var item in result.data)
//                //    {
//                //        dt1.Rows.Add(item.Id, item.CustomerName, item.CustomerAddress);
//                //    }

//                //    CustomerListDataGrid.ReadOnly = true;
//                //    CustomerListDataGrid.AllowUserToAddRows = false;
//                //    CustomerListDataGrid.DataSource = dt1;
//                //    CustomerListDataGrid.Columns[0].Visible = false;

//                //    CustomerListDataGrid.BringToFront();
//                //}
//                return;
//            }

//            // Product search now goes through the debounced path;
//            // this fallback is only used when called directly (e.g., Enter key press)
//            await TriggerProductSearchAsync(searchText);
//        }

//        // Add alongside _searchCts
//        private CancellationTokenSource _customerSearchCts;

//        private async Task ShowCustomerSuggestionsAsync(string searchText)
//        {
//            _customerSearchCts?.Cancel();
//            _customerSearchCts?.Dispose();
//            _customerSearchCts = new CancellationTokenSource();
//            var token = _customerSearchCts.Token;

//            try
//            {
//                using (var context = new POSDbContext())
//                {
//                    ICustomerRepository repo = new CustomerRepository(context);
//                    var result = await repo.GetCustomerPagingListAsync(1, 100, searchText);

//                    if (token.IsCancellationRequested) return; // ← stale result, discard

//                    var dt = new System.Data.DataTable();
//                    dt.Columns.Add("ID", typeof(int));
//                    dt.Columns.Add("Name", typeof(string));
//                    dt.Columns.Add("Address", typeof(string));

//                    foreach (var item in result.data)
//                        dt.Rows.Add(item.Id, item.CustomerName, item.CustomerAddress);

//                    CustomerListDataGrid.ReadOnly = true;
//                    CustomerListDataGrid.AllowUserToAddRows = false;
//                    CustomerListDataGrid.DataSource = dt;
//                    CustomerListDataGrid.Columns[0].Visible = false;
//                    CustomerListDataGrid.BringToFront();
//                    CustomerListDataGrid.Visible = true;
//                }
//            }
//            catch (OperationCanceledException) { /* superseded — ignore */ }
//            catch (Exception ex)
//            {
//                CustomerListDataGrid.Visible = false;
//                // log ex
//            }
//        }
//        // ─── Dispose timer and CTS properly ────────────────────────────────────────
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                _debounceTimer?.Dispose();
//                _customerDebounceTimer?.Dispose();   // ← ADD
//                _searchCts?.Dispose();
//                _customerSearchCts?.Dispose();       // ← ADD
//                components?.Dispose();
//            }
//            base.Dispose(disposing);
//        }


//        #region Old GetProductSuggestions use to get the Products for suggestion grid
//        //private List<ProductSuggestion> GetProductSuggestions(string searchText)
//        //{

//        //    //var suggestions = new List<ProductSuggestion>();

//        //    //using (var _context = new POSDbContext())
//        //    //{
//        //    //    var data = _context.Products.AsQueryable();

//        //    //    // apply search

//        //    //    if (!string.IsNullOrEmpty(searchText))
//        //    //    {
//        //    //        var searchWords = searchText.ToLower().Split(' ');
//        //    //        // apply search

//        //    //        foreach (var word in searchWords)
//        //    //        {
//        //    //            data = data.Where(s => s.ProductEnglishName.Contains(word) || s.Id.ToString().Contains(word) || s.SearchByProductCode.Contains(word));
//        //    //            //data = data.Where(s => s.CustomerName.Contains(word) || s.City.Name.Contains(word));
//        //    //        }
//        //    //    }

//        //    //    var result = data.OrderBy(s => s.Id).Select(s => new ProductSuggestion()
//        //    //    {
//        //    //        ProductId = s.Id,
//        //    //        ProductName = s.ProductEnglishName,
//        //    //        ProductUrduName = s.ProductUrduName,
//        //    //        Qty = s.Qty,
//        //    //        purchasePrice = s.PurchasePrice,
//        //    //    }).Take(100).ToList();

//        //    //    return result;
//        //    //}

//        //    var suggestions = new List<ProductSuggestion>();

//        //    using (var _context = new POSDbContext())
//        //    {
//        //        var data = _context.Products.AsNoTracking();

//        //        if (!string.IsNullOrEmpty(searchText))
//        //        {
//        //            var searchWords = searchText.ToLower()
//        //                                        .Trim()
//        //                                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

//        //            data = (System.Data.Entity.Infrastructure.DbQuery<Models.Product>)data.Where(s =>
//        //                searchWords.All(word =>
//        //                    s.ProductEnglishName.ToLower().Contains(word) ||
//        //                    s.SearchByProductCode.ToLower().Contains(word) ||
//        //                    s.Id.ToString().Contains(word)
//        //                )
//        //            );
//        //        }

//        //        var result = data
//        //            .OrderBy(s => s.Id)
//        //             .Take(100)
//        //            .Select(s => new ProductSuggestion
//        //            {
//        //                ProductId = s.Id,
//        //                ProductName = s.ProductEnglishName,
//        //                ProductUrduName = s.ProductUrduName,
//        //                purchasePrice = s.PurchasePrice,
//        //                Qty = s.Qty
//        //            }).AsNoTracking()
//        //            .ToList();

//        //        return result;
//        //    }
//        //}

//        #endregion
//        private async Task<List<ProductSuggestion>> GetProductSuggestions(string searchText)
//        {
//            using (var _context = new POSDbContext())
//            {
//                // Clean and prepare search words
//                var searchWords = string.IsNullOrWhiteSpace(searchText)
//                    ? Array.Empty<string>()
//                    : searchText.ToLower()
//                                .Trim()
//                                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

//                // CASE 1: No search text - Fastest path
//                if (searchWords.Length == 0)
//                {
//                    string sql = @"
//                SELECT TOP 100 
//                    Id AS ProductId,
//                    ProductEnglishName AS ProductName,
//                    ProductUrduName,
//                    Qty,
//                    PurchasePrice
//                FROM Products WITH (NOLOCK)
//                ORDER BY Id";

//                    return _context.Database.SqlQuery<ProductSuggestion>(sql).ToList();
//                }

//                // CASE 2: Single word search - Optimized
//                if (searchWords.Length == 1)
//                {
//                    string sql = @"
//                                    SELECT TOP 100 
//                                        p.Id AS ProductId,
//                                        p.ProductEnglishName AS ProductName,
//                                        p.ProductUrduName,
//                                        p.Qty,
//                                        p.PurchasePrice
//                                    FROM Products p WITH (NOLOCK)
//                                    WHERE 
//                                        p.ProductEnglishName LIKE @pattern 
//                                        OR p.SearchByProductCode LIKE @pattern 
//                                        OR CAST(p.Id AS VARCHAR(50)) LIKE @pattern
//                                    ORDER BY p.Id";

//                    var param = new SqlParameter("@pattern", $"%{searchWords[0]}%");
//                    return _context.Database.SqlQuery<ProductSuggestion>(sql, param).ToList();
//                }

//                // CASE 3: Multiple words - Efficient parameterized query
//                return await ExecuteMultiWordSearch(searchWords, _context);
//            }
//        }

//        private async Task<List<ProductSuggestion>> ExecuteMultiWordSearch(string[] words, POSDbContext context)
//        {
//            // Build parameterized query for multiple words
//            var parameters = new List<SqlParameter>();
//            var whereConditions = new List<string>();

//            for (int i = 0; i < words.Length; i++)
//            {
//                string paramName = $"@word{i}";
//                parameters.Add(new SqlParameter(paramName, $"%{words[i]}%"));

//                whereConditions.Add($@"
//                                    (p.ProductEnglishName LIKE {paramName}
//                                     OR p.SearchByProductCode LIKE {paramName}
//                                     OR CAST(p.Id AS VARCHAR(50)) LIKE {paramName})");
//            }

//            string whereClause = string.Join(" AND ", whereConditions);

//            string sql = $@"
//                            SELECT TOP 100 
//                                p.Id AS ProductId,
//                                p.ProductEnglishName AS ProductName,
//                                p.ProductUrduName,
//                                p.Qty,
//                                p.PurchasePrice
//                            FROM Products p WITH (NOLOCK)
//                            WHERE {whereClause}
//                            ORDER BY p.Id";

//            return await context.Database.SqlQuery<ProductSuggestion>(sql, parameters.ToArray()).ToListAsync();
//        }

//        private void P_StockQtyTxt_TextChange(object sender, EventArgs e)
//        {
//            string currentText = P_StockQtyTxt.Text;
//            string validText = RegexValidator.ValidateCommonPattern(currentText, ValidationPattern.NumbersOnly, "quantityField");
//            if (currentText != validText)
//            {
//                P_StockQtyTxt.Text = validText;
//                P_StockQtyTxt.SelectionStart = validText.Length;
//            }
//        }

//        private void ProductSalePrice_TextChange(object sender, EventArgs e)
//        {
//            string currentText = ProductSalePrice.Text;
//            string validText = RegexValidator.ValidateCommonPattern(currentText, ValidationPattern.NumbersWithDecimal, "saleAmontField");
//            if (currentText != validText)
//            {
//                ProductSalePrice.Text = validText;
//                ProductSalePrice.SelectionStart = validText.Length;
//            }
//        }

//        private void CartProductList_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            // Check if Delete column clicked
//            if (e.RowIndex >= 0 && CartProductList.Columns[e.ColumnIndex].Name == "Delete")
//            {
//                // Ask for confirmation (optional)
//                var confirm = MessageBox.Show("Do you want to delete this product?",
//                                              "Confirm Delete",
//                                              MessageBoxButtons.YesNo,
//                                              MessageBoxIcon.Question);

//                if (confirm == DialogResult.Yes)
//                {
//                    CartProductList.Rows.RemoveAt(e.RowIndex);
//                    CalculateTotals();
//                    CalculateReturnAmount();
//                    ProductEngNameTxt.Focus();
//                    ProductEngNameTxt.SelectAll();
//                }
//            }
//        }

//        private void CustomerNameTxt_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)Keys.Enter)
//            {
//                if (CustomerListDataGrid.Visible == false)
//                {
//                    ShowSuggestions(CustomerNameTxt.Text, isForCustomer: true);
//                }
//                else
//                {
//                    CustomerListDataGrid.Visible = false;
//                }
//                e.Handled = true;
//            }
//        }

//        private void ResetCustomerBtn_Click(object sender, EventArgs e)
//        {
//            CustomerNameTxt.Text = string.Empty;
//            customerId = string.Empty;
//            CustomerIdLbl.Text = string.Empty;
//            this.ResetCustomerBtn.Enabled = true;
//            this.ResetCustomerBtn.Visible = false;
//            ClearCustomerPreviousTransactionGroup();

//        }

//        private void ClearCustomerPreviousTransactionGroup()
//        {
//            previousBillAmountLbl.Text = "0";
//            PreviousReceivedAmountLbl.Text = "0";

//            PreviousOrderSummaryLbl.Visible = false;
//        }

//        private void ReceivedAmountTxt_TextChange(object sender, EventArgs e) => CalculateReturnAmount();

//        private void CalculateReturnAmount()
//        {
//            if (!string.IsNullOrEmpty(ReceivedAmountTxt.Text))
//            {
//                string currentText = ReceivedAmountTxt.Text;
//                string validText = RegexValidator.ValidateCommonPattern(currentText, ValidationPattern.NumbersOnly, "receivedAmountField");
//                if (currentText != validText)
//                {
//                    ReceivedAmountTxt.Text = validText;
//                    ReceivedAmountTxt.SelectionStart = validText.Length;
//                }
//            }

//            if (!string.IsNullOrEmpty(TotalAmountLbl.Text) && TotalAmountLbl.Text != "0")
//            {
//                // Calculate remaining amount
//                decimal totalAmount = Convert.ToDecimal(TotalAmountLbl.Text); // Your total amount

//                if (string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
//                {
//                    lblRemainingAmount.Text = "Remaining: Rs. 0";
//                    return;
//                }

//                if (decimal.TryParse(ReceivedAmountTxt.Text, out decimal receivedAmount))
//                {
//                    decimal remainingAmount = totalAmount - receivedAmount;
//                    lblRemainingAmount.Text = remainingAmount >= 0
//                        ? $"Remaining Amt:  Rs. {remainingAmount}"
//                        : $"Return Amt:  Rs. {Math.Abs(remainingAmount)}";
//                    lblRemainingAmount.ForeColor = remainingAmount >= 0 ? Color.Red : Color.Blue;
//                }
//            }
//            else
//            {
//                lblRemainingAmount.Text = "Remaining: Rs. 0";
//            }
//        }

//        private void ClearCartBtn_Click(object sender, EventArgs e)
//        {
//            ClearCartFunction();
//            ClearInputs();
//            ClearCustomerPreviousTransactionGroup();
//            // Optional: Show confirmation message
//            ProductEngNameTxt.Focus();
//            MessageBox.Show("Cart cleared successfully!", "Clear Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//        private void ClearCartFunction()
//        {
//            PId = string.Empty;
//            customerId = string.Empty;
//            CustomerNameTxt.Text = string.Empty;
//            CartProductList.Rows.Clear();
//            ResetCustomerBtn.Visible = false;

//            customerId = string.Empty;
//            CustomerIdLbl.Text = string.Empty;
//            CustomerNameTxt.Text = string.Empty;

//            PreviousOrderIdLbl.Text = string.Empty;
//            string invRef = TextFormatHelper.GetPrefix(Properties.Settings.Default.UserName);
//            InvoiceNoLbl.Text = invRef + DateTime.Now.ToString("ddMMyy-HHmmss");
//            isTempSaved = false;

//            // Also update the totals to zero
//            TotalItemLbl.Text = "0";
//            TotalAmountLbl.Text = "0";
//            ReceivedAmountTxt.Clear();

//            isPaid = false;
//        }

//        private void TopBarSearchProductTxt_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)Keys.Enter)
//            {
//                e.Handled = true; // Prevents the default beep sound
//                // Create a new instance of your Form
//                Form ProductForm = new Form();
//                ProductForm.Text = "Product Form";
//                ProductForm.StartPosition = FormStartPosition.CenterScreen;

//                // Create an instance of your User Control
//                var FormCtrl = new POS_Shop.Views.Controllers.Product.ProductListControl();
//                FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

//                // Add the User Control to the new Form's controls collection
//                ProductForm.Controls.Add(FormCtrl);
//                ProductForm.Width = 1050; ProductForm.Height = 625;
//                // Show the new form
//                ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
//            }
//        }

//        private void SearchInvoiceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
//        {
//            PreviousOrderIdLbl.Text = string.Empty;
//            // Create a new instance of your Form
//            Form OrderListForm = new Form();
//            OrderListForm.Text = "Order List";
//            OrderListForm.StartPosition = FormStartPosition.CenterScreen;

//            // Get screen area
//            Screen currentScreen = Screen.PrimaryScreen;
//            int screenArea = currentScreen.Bounds.Width * currentScreen.Bounds.Height;

//            // If screen has less than 1.5M pixels (typical for smaller/lower-res screens)
//            if (screenArea < 1327104)
//            {
//                OrderListForm.WindowState = FormWindowState.Maximized;
//            }
//            else
//            {
//                OrderListForm.Width = 1390;
//                OrderListForm.Height = 730;
//            }

//            // Create an instance of your User Control
//            var FormCtrl = new Views.Controllers.Order.OrdersControlUI();
//            FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

//            // Add the User Control to the new Form's controls collection
//            OrderListForm.Controls.Add(FormCtrl);
//            //  OrderListForm.Width = 1390; OrderListForm.Height = 730;

//            // Show the new form
//            OrderListForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
//            if (FormCtrl.isRecordSelected == true)
//            {
//                // PREVENT DUPLICATE LOADING - Check if cart has items

//                var result = MessageBox.Show("Clear current cart before loading order?", "Confirm",
//                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                if (result != DialogResult.Yes)
//                {
//                    return;
//                }

//                ClearCustomerPreviousTransactionGroup();
//                InvoiceNoLbl.Text = FormCtrl.InvoiceNoLbl.Text;
//                PreviousOrderIdLbl.Text = FormCtrl.OrderIDLbl.Text;
//                TotalAmountLbl.Text = FormCtrl.TotalBill.ToString();
//                ReceivedAmountTxt.Text = FormCtrl.ReceiveAmount.ToString();
//                if (FormCtrl.CustomerId != 0)
//                {
//                    CustomerIdLbl.Text = FormCtrl.CustomerId.ToString();
//                    CustomerNameTxt.Text = FormCtrl.CustomerName;
//                    this.ResetCustomerBtn.Visible = true;
//                    this.ResetCustomerBtn.Enabled = true;
//                    CustomerListDataGrid.Visible = false;
//                }
//                else
//                {
//                    ResetCustomerBtn.Visible = false;
//                }
//            }
//        }

//        private async void PreviousOrderIdLbl_TextChanged(object sender, EventArgs e)
//        {
//            if ((PreviousOrderIdLbl.Text != "OrderID" && InvoiceNoLbl.Text != "InvoiceNo") &&
//                (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text) && !string.IsNullOrEmpty(InvoiceNoLbl.Text)))
//            {
//                //// PREVENT DUPLICATE LOADING - Check if cart has items
//                //if (CartProductList.Rows.Count > 0)
//                //{
//                //    var result = MessageBox.Show("Clear current cart before loading order?", "Confirm",
//                //                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                //    if (result != DialogResult.Yes)
//                //    {
//                //        return;
//                //    }
//                //}

//                using (var context = new POSDbContext())
//                {
//                    var orderRepo = new OrderRepository(context);
//                    var result = await orderRepo.GetOrderByIdAsync(Convert.ToInt32(PreviousOrderIdLbl.Text), InvoiceNoLbl.Text);
//                    if (result != null)
//                    {
//                        // CLEAR EXISTING ITEMS FIRST to prevent duplicates
//                        CartProductList.Rows.Clear();

//                        CustomerIdLbl.Text = result.CustomerId.HasValue ? result.CustomerId.Value.ToString() : string.Empty;
//                        CustomerNameTxt.Text = string.IsNullOrEmpty(CustomerIdLbl.Text) ? "" : result.CustomerName;
//                        TotalAmountLbl.Text = result.TotalBill.ToString();
//                        if (result.paymentType == "Cash")
//                        {
//                            CashRadioBtn.Checked = true;
//                            BankTransferRaadioBtn.Checked = false;
//                        }
//                        else
//                        {
//                            CashRadioBtn.Checked = false;
//                            BankTransferRaadioBtn.Checked = true;
//                        }

//                        // Safely add order details
//                        foreach (var order in result.OrderDetailsList)
//                        {
//                            string productId = order.ProductId.ToString() ?? "0";
//                            string finalName = !string.IsNullOrEmpty(order.ProductDetail) ?
//                                $"{order.ProductName} {order.ProductDetail}" : order.ProductName;

//                            string productType = order.QuantityType;
//                            decimal salePrice = Math.Round(decimal.Parse(order.Price.ToString()), 1);
//                            int qty = order.Quantity;
//                            decimal amount = salePrice * qty;

//                            CartProductList.Rows.Add(null, amount, salePrice, finalName,
//                                                   productType, qty, productId, order.ProductDetail);
//                        }

//                        CalculateTotals();
//                    }
//                }
//            }
//        }

//        // Usage method
//        public async Task GeneratePdfInvoice()
//        {
//            var confirm = MessageBox.Show("Do you want to Generate PDF?",
//                                          "Confirm Action",
//                                          MessageBoxButtons.YesNo,
//                                          MessageBoxIcon.Question);

//            if (confirm == DialogResult.Yes)
//            {
//                try
//                {
//                    using (var saveFileDialog = new SaveFileDialog())
//                    {
//                        string pdfName = !string.IsNullOrEmpty(CustomerNameTxt.Text)
//                            ? $"{CustomerNameTxt.Text}-{InvoiceNoLbl.Text}"
//                            : InvoiceNoLbl.Text;

//                        saveFileDialog.FileName = $"Invoice_{pdfName}.pdf";
//                        saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
//                        saveFileDialog.DefaultExt = "pdf";
//                        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

//                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                        {
//                            PrintToPdfGenerator generator = new PrintToPdfGenerator();
//                            generator.GenerateInvoice(CartProductList,
//                                saveFileDialog.FileName,
//                                customerName: CustomerNameTxt.Text,
//                                invoiceNo: InvoiceNoLbl.Text,
//                                totalAmount: TotalAmountLbl.Text,
//                                receivedAmount: ReceivedAmountTxt.Text);

//                            ToastHelper.ShowSuccess($"Invoice saved to:\n{saveFileDialog.FileName}");
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private async void SaveAndPrintOrderBtn_Click(object sender, EventArgs e)
//        {
//            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
//            {
//                LoadingManager.ShowLoading();
//                try
//                {

//                    #region code of showing Confirmation for Paid Stamp
//                    //DialogResult result = MessageBox.Show(
//                    //"Mark this bill as paid?",  // Simple, direct question
//                    //"Payment Status",
//                    //MessageBoxButtons.YesNo,
//                    //MessageBoxIcon.Question);

//                    //if (result == DialogResult.Yes)
//                    //{
//                    //    // Perform delete operation
//                    //    isPaid = true;
//                    //}
//                    #endregion

//                    bool IsDone = false;
//                    if (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text) && PreviousOrderIdLbl.Text != "Prev Order Id")
//                        IsDone = await SaveOrder(true);  //await UpdateOrderSaved();
//                    else
//                        IsDone = await SaveOrder(false);  // await NewOrderSaved();

//                    if (IsDone)
//                    {

//                        LoadingManager.HideLoading();

//                        //// First clear any previous handlers
//                        //OrderPrintDocument.PrintPage -= OrderPrintDocument_PrintPage;
//                        //OrderPrintDocument.PrintPage -= OrderPrintDocument_PrintPage_English;

//                        //if (EnglishInvoiceChk.Checked)
//                        //    OrderPrintDocument.PrintPage += OrderPrintDocument_PrintPage_English;
//                        //else
//                        //    OrderPrintDocument.PrintPage += OrderPrintDocument_PrintPage;


//                        OrderPrintPreviewDialog.Document = OrderPrintDocument;
//                        OrderPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("FullInvoice", 280, 32767);
//                        OrderPrintDocument.Print();

//                        if (isTempSaved)
//                        {
//                            string sql = "DELETE FROM TempOrders WHERE InvoiceNumber = @InvoiceNumber";
//                            string sql1 = "DELETE FROM TempOrderDetails WHERE TempInvoiceNumber = @InvoiceNumber";

//                            using (var context = new POSDbContext())
//                            {
//                                var parameters1 = new[]
//                                {
//                                new System.Data.SqlClient.SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text)
//                            };

//                                context.Database.ExecuteSqlCommand(sql1, parameters1);

//                                var parameters = new[]
//                                {
//                                new System.Data.SqlClient.SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text),
//                            };
//                                context.Database.ExecuteSqlCommand(sql, parameters);
//                            }
//                        }

//                        ResetUIAfterSave();
//                        SendKeys.SendWait("^{F11}");
//                        MessageBox.Show("Order Created Successfully!", "Order Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                    else
//                    {
//                        MessageBox.Show("Order Creation Failed!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//                catch (Exception)
//                {

//                    LoadingManager.HideLoading();

//                }
//            }
//            else
//            {
//                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//        }

//        private void GenerateInvoicePdf()
//        {
//            var confirm = MessageBox.Show("Do you want to Generate PDF?",
//                                          "Confirm Action",
//                                          MessageBoxButtons.YesNo,
//                                          MessageBoxIcon.Question);

//            if (confirm == DialogResult.Yes)
//            {
//                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
//                {
//                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
//                    saveFileDialog.Title = "Save Invoice as PDF";
//                    saveFileDialog.FileName = $"Invoice_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

//                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                    {
//                        try
//                        {
//                            MessageBox.Show("Invoice saved as PDF successfully!", "Success",
//                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        }
//                        catch (Exception ex)
//                        {
//                            MessageBox.Show($"Error saving PDF: {ex.Message}", "Error",
//                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                    }
//                }
//            }
//        }

//        private async Task<bool> SaveOrder(bool isUpdate = false)
//        {
//            using (var context = new POSDbContext())
//            using (var dbTransaction = context.Database.BeginTransaction())
//            {
//                try
//                {
//                    var orderRepository = new OrderRepository(context);

//                    // Get order data
//                    var orderData = await GetOrderData();

//                    if (isUpdate)
//                    {
//                        orderData.Id = int.Parse(PreviousOrderIdLbl.Text);
//                    }

//                    // Save order
//                    var orderId = isUpdate
//                        ? await UpdateOrder(orderRepository, orderData, context)
//                        : await orderRepository.AddOrder(orderData);

//                    // Save order details
//                    await SaveOrderDetails(context, orderId);

//                    dbTransaction.Commit();
//                    return true;
//                }
//                catch (DbException ex)
//                {
//                    dbTransaction.Rollback();
//                    return false;
//                }
//            }
//        }

//        private async Task<Order> GetOrderData()
//        {
//            int? customerId = null;
//            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) && !string.IsNullOrEmpty(CustomerIdLbl.Text))
//            {
//                int.TryParse(CustomerIdLbl.Text, out int parsedId);
//                customerId = parsedId;
//            }

//            float.TryParse(TotalAmountLbl.Text, out float totalBill);

//            float receiveAmount = totalBill;
//            if (!string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
//            {
//                float.TryParse(ReceivedAmountTxt.Text, out receiveAmount);
//            }

//            return new Order
//            {
//                TotalBill = totalBill,
//                ReceiveAmount = receiveAmount,
//                CreatedDate = DateTime.Now,
//                InvoiceNumber = !string.IsNullOrEmpty(InvoiceNoLbl.Text) ? InvoiceNoLbl.Text : DateTime.Now.ToString("MMddyyy-HHmmss"),
//                paymentType = CashRadioBtn.Checked ? "Cash" : "Bank",
//                customerId = customerId
//            };
//        }

//        private async Task<TempOrder> GetTempOrderData()
//        {
//            int? customerId = null;
//            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) && !string.IsNullOrEmpty(CustomerIdLbl.Text))
//            {
//                int.TryParse(CustomerIdLbl.Text, out int parsedId);
//                customerId = parsedId;
//            }

//            float.TryParse(TotalAmountLbl.Text, out float totalBill);
//            float receiveAmount = totalBill;

//            return new TempOrder
//            {
//                TotalBill = totalBill,
//                CreatedDate = DateTime.Now,
//                InvoiceNumber = !string.IsNullOrEmpty(InvoiceNoLbl.Text) ? InvoiceNoLbl.Text : DateTime.Now.ToString("MMddyyy-HHmmss"),
//                customerId = customerId,
//                CustomerName = CustomerNameTxt.Text
//            };
//        }

//        private async Task<int> UpdateOrder(OrderRepository orderRepository, Order order, POSDbContext context)
//        {
//            var orderId = await orderRepository.AddOrder(order);

//            // Checking User has Enabled the Stock Qty Update Feature
//            var config = ConfigurationManager.Configuration.Features.EnableUpdateQty;
//            if (config)
//                UpdateStockQuantity(orderId);

//            // Remove existing order details
//            var existingDetails = context.OrderDetails.Where(s => s.OrderId == orderId).ToList();
//            context.OrderDetails.RemoveRange(existingDetails);
//            context.SaveChanges();
//            return orderId;
//        }

//        private void UpdateStockQuantity(int orderId)
//        {
//            using (var context = new POSDbContext())
//            {
//                var existingDetails = context.OrderDetails.Where(s => s.OrderId == orderId).ToList();
//                foreach (var item in existingDetails)
//                {
//                    if (item.ProductId.HasValue)
//                    {
//                        var product = context.Products.Find(item.ProductId);
//                        if (product != null)
//                        {
//                            var prices = context.ProductPrices
//                            .Where(p => p.ProductId == item.ProductId && p.TypeName == item.QuantityType)
//                            .FirstOrDefault();
//                            product.Qty += (item.Quantity * prices.ItemsCount);
//                            context.Entry(product).State = EntityState.Modified;
//                        }
//                    }
//                }
//                context.SaveChanges();
//            }
//        }

//        private async Task SaveOrderDetails(POSDbContext context, int orderId)
//        {
//            var orderDetailList = new List<OrderDetail>();
//            var prices = new ProductPrice();
//            foreach (DataGridViewRow row in CartProductList.Rows)
//            {
//                if (row.Cells["ProductId"].Value == null) continue;

//                var productIdValue = row.Cells["ProductId"].Value?.ToString();


//                var q = int.Parse(row.Cells["Qty"].Value?.ToString());

//                // Checking User has Enabled the Stock Qty Update Feature
//                var config = ConfigurationManager.Configuration.Features.EnableUpdateQty;
//                if (config)
//                {
//                    if (!string.IsNullOrEmpty(productIdValue))
//                    {

//                        int prodId = int.Parse(productIdValue);
//                        string typeName = row.Cells["ProductType"].Value?.ToString();
//                        prices = context.ProductPrices.Where(p => p.ProductId == prodId && p.TypeName == typeName).FirstOrDefault();

//                        var productCheck = context.Products.Find(int.Parse(productIdValue));

//                        if ((int.Parse(row.Cells["Qty"].Value?.ToString()) < 0 || (int.Parse(row.Cells["Qty"].Value?.ToString()) * prices.ItemsCount) < q))
//                        {
//                            LoadingManager.HideLoading();
//                            MessageBox.Show($"Insufficient stock for product {productCheck.ProductEnglishName}",
//                               "Error",
//                               MessageBoxButtons.OK,
//                               MessageBoxIcon.Error);
//                            throw new Exception($"Insufficient stock for product ID {productIdValue}");
//                        }


//                    }
//                }

//                var odrDetail = new OrderDetail
//                {
//                    ProductId = string.IsNullOrEmpty(productIdValue) ? (int?)null : int.Parse(productIdValue),
//                    OtherProductName = string.IsNullOrEmpty(productIdValue) ? row.Cells["Urdu Name"].Value?.ToString() : null,
//                    Quantity = int.Parse(row.Cells["Qty"].Value?.ToString()),
//                    QuantityType = row.Cells["ProductType"].Value?.ToString(),
//                    Price = float.Parse(row.Cells["SalePrice"].Value?.ToString()),
//                    CreatedDate = DateTime.Now,
//                    OrderId = orderId,
//                    ProductDetail = row.Cells["ProductDetail"].Value?.ToString()
//                };
//                orderDetailList.Add(odrDetail);

//                // Checking User has Enabled the Stock Qty Update Feature
//                if (config)
//                {
//                    if (!string.IsNullOrEmpty(productIdValue))
//                    {
//                        var pid = int.Parse(productIdValue);
//                        var product = context.Products.Find(pid);
//                        product.Qty -= (odrDetail.Quantity * prices.ItemsCount);
//                        context.Entry(product).State = EntityState.Modified;
//                    }
//                }
//            }

//            context.OrderDetails.AddRange(orderDetailList);
//            await context.SaveChangesAsync();
//        }

//        private async Task SaveTempOrderDetails(POSDbContext context, string invoiceNo)
//        {
//            //First we will check if the TempOrderDetail has already record or not? if yes then we will delete all first.. 
//            var tempOrderDetailList = context.TempOrderDetails.Where(s => s.TempInvoiceNumber.Equals(invoiceNo)).ToList();
//            if (tempOrderDetailList.Count > 0)
//            {
//                context.TempOrderDetails.RemoveRange(tempOrderDetailList);
//                context.SaveChanges();
//            }

//            var orderDetailList = new List<TempOrderDetail>();

//            foreach (DataGridViewRow row in CartProductList.Rows)
//            {
//                if (row.Cells["ProductId"].Value == null) continue;

//                var productIdValue = row.Cells["ProductId"].Value?.ToString();
//                var odrDetail = new TempOrderDetail
//                {
//                    ProductId = string.IsNullOrEmpty(productIdValue) ? (int?)null : int.Parse(productIdValue),
//                    ProductName = row.Cells["Urdu Name"].Value?.ToString(),
//                    Quantity = int.Parse(row.Cells["Qty"].Value?.ToString()),
//                    QuantityType = row.Cells["ProductType"].Value?.ToString(),
//                    Price = float.Parse(row.Cells["SalePrice"].Value?.ToString()),
//                    TempInvoiceNumber = invoiceNo,
//                    ProductDetail = row.Cells["ProductDetail"].Value?.ToString()
//                };
//                orderDetailList.Add(odrDetail);
//            }

//            context.TempOrderDetails.AddRange(orderDetailList);
//            await context.SaveChangesAsync();
//        }

//        private void TruncateOrder_OrderDetailBtn_Click(object sender, EventArgs e)
//        {
//            // Ask for confirmation (optional)
//            var confirm = MessageBox.Show("Do you want to delete this Orders?",
//                                          "Confirm Delete",
//                                          MessageBoxButtons.YesNo,
//                                          MessageBoxIcon.Question);

//            if (confirm == DialogResult.Yes)
//            {
//                using (var ctx = new POSDbContext())
//                {
//                    ctx.Database.ExecuteSqlCommand("ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]");
//                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[OrderDetails]");
//                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[Orders]");
//                    ctx.Database.ExecuteSqlCommand(@"ALTER TABLE [dbo].[OrderDetails] 
//                                     ADD CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId] 
//                                     FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id])");

//                    MessageBox.Show("Records has been Delete",
//                                             "Information",
//                                             MessageBoxButtons.OK,
//                                             MessageBoxIcon.Information);
//                }

//                InvoicePageTabControl.SelectedTab = BilPad;
//            }
//        }

//        private void PrintPreviewBtn_Click(object sender, EventArgs e)
//        {
//            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
//            {
//                //// Simulate Ctrl + F11 key press, to shift the control automatically because we are using Auto sharing printer usb
//                SendKeys.SendWait("^{F11}");
//                OrderPrintPreviewDialog.Document = OrderPrintDocument;
//                OrderPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("FullInvoice", 280, 32767);
//                OrderPrintPreviewDialog.PrintPreviewControl.Zoom = 1.0;
//                OrderPrintPreviewDialog.ShowDialog();
//            }
//            else
//            {
//                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//        }

//        // This is default
//        private void OrderPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//        {
//            if (EnglishInvoiceChk.Checked)
//                InvoicePrintHelper.PrintEnglishInvoice(
//                      e: e,
//                      cartProductList: CartProductList,
//                      customerName: CustomerNameTxt.Text,
//                      invoiceNo: InvoiceNoLbl.Text,
//                      totalAmount: TotalAmountLbl.Text,
//                      isCashPayment: CashRadioBtn.Checked,
//                      receivedAmount: ReceivedAmountTxt.Text,
//                      isPaid
//                  );
//            else
//                InvoicePrintHelper.PrintInvoice(
//                      e: e,
//                      cartProductList: CartProductList,
//                      customerName: CustomerNameTxt.Text,
//                      invoiceNo: InvoiceNoLbl.Text,
//                      totalAmount: TotalAmountLbl.Text,
//                      isCashPayment: CashRadioBtn.Checked,
//                      receivedAmount: ReceivedAmountTxt.Text,
//                      isPaid
//                  );
//        }



//        private void productTypeDropdown_Enter(object sender, EventArgs e)
//        {
//            productTypeDropdown.BorderColor = Color.BlueViolet;
//        }

//        private void productTypeDropdown_Leave(object sender, EventArgs e)
//        {
//            productTypeDropdown.BorderColor = Color.Silver;
//        }

//        private void InvoiceShopName_CheckedChanged(object sender, EventArgs e)
//        {
//            InvoiceShopName.Text = InvoiceShopName.Checked ? "Hide Shop Name is Invoice" : "Show Shop Name is Invoice";
//        }

//        private void BillPadForm_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.S && e.Control) // Ctrl + S to Save and Print
//            {
//                SaveAndPrintOrderBtn.PerformClick();
//            }
//            else if (e.KeyCode == Keys.T && e.Control) // Ctrl + T to Save and Print (Thermal)
//            {
//                SaveBillBtn.PerformClick();
//            }
//            else if (e.KeyCode == Keys.P && e.Control) // Ctrl + P to Print Preview
//            {
//                PrintPreviewBtn.PerformClick();
//            }
//            else if (e.KeyCode == Keys.N && e.Control) // Ctrl + N to New Invoice
//            {
//                ClearCartBtn.PerformClick();
//            }
//            else if (e.KeyCode == Keys.Escape) // Esc to Clear Cart
//            {
//                e.Handled = true;
//                ProductEngNameTxt.Focus();
//                ProductEngNameTxt.SelectAll(); // Optional: select all text

//                SuggestionGrid.Visible = false;
//            }
//            else if (e.KeyCode == Keys.D1 && e.Control) // 1 to Focus on Product TextBox
//            {
//                ProductEngNameTxt.Focus();
//                ProductEngNameTxt.SelectAll(); 
//            }
//            else if (e.KeyCode == Keys.D2 && e.Control) // 2 to Focus on Product TextBox
//            {
//                CustomerNameTxt.Focus();  
//                CustomerNameTxt.SelectAll();
//            }
//            else if (e.KeyCode == Keys.R && e.Control)
//            {
//                GenerateInvoicePdfBtn.PerformClick();
//            }
//            else if (e.KeyCode == Keys.Q && e.Control)
//            {
//                e.Handled = true;
//                GotoFirstRow();
//            }
//            else if (e.KeyCode == Keys.D && e.Control)
//            {
//                e.Handled = true;
//                SaveOrderWithoutPrintBtn.PerformClick();
//            }
//            else if (e.KeyCode == Keys.E && e.Control)
//            {
//                e.Handled = true;
//                ExportBtn.PerformClick();
//            }
//            else if (e.KeyCode == Keys.R && e.Alt)
//            {
//                 e.Handled = true;
//                ReceivedAmountTxt.Focus();
//            }
//        }

//        private void GotoFirstRow()
//        {
//            if (CartProductList.Rows.Count > 0)
//            {
//                CartProductList.ClearSelection();
//                CartProductList.Rows[0].Selected = true;
//                CartProductList.CurrentCell = CartProductList.Rows[0].Cells[1];
//                CartProductList.Focus();
//            }
//        }

//        private bool _isUpdatingText = false;
//        private string _lastSelectedProductText = "";



//        //private async void SuggestionGrid_KeyDown(object sender, KeyEventArgs e)
//        //{
//        //    if (e.KeyCode == Keys.Up)
//        //    {
//        //        // If we're on the first row, move focus back to TextBox
//        //        if (SuggestionGrid.CurrentRow != null &&
//        //            SuggestionGrid.CurrentRow.Index == 0)
//        //        {
//        //            ProductEngNameTxt.Focus();
//        //            ProductEngNameTxt.SelectAll(); // Optional: select all text

//        //            SuggestionGrid.Visible = false;
//        //            e.Handled = true;
//        //        }
//        //    }
//        //    else if (e.KeyCode == Keys.Left)
//        //    {
//        //        e.Handled = true;
//        //        e.SuppressKeyPress = true;
//        //        ProductEngNameTxt.Focus();
//        //        ProductEngNameTxt.SelectAll(); // Optional: select all text

//        //        SuggestionGrid.Visible = false;
//        //    }
//        //    else if (e.KeyCode == Keys.Enter && !e.Handled)
//        //    {
//        //        e.Handled = true;
//        //        e.SuppressKeyPress = true; // This prevents the beep sound and default behavior

//        //        if (SuggestionGrid.CurrentRow != null && SuggestionGrid.CurrentRow.Index >= 0)
//        //        {
//        //            int pId = Convert.ToInt32(SuggestionGrid.CurrentRow.Cells[0].Value);
//        //            ProductEngNameTxt.Text = (string)SuggestionGrid.CurrentRow.Cells[2].Value;
//        //            prod_U_Name = (string)SuggestionGrid.CurrentRow.Cells[3].Value;

//        //            DataGridViewRow foundRow = null;

//        //            foreach (DataGridViewRow row in SuggestionGrid.Rows)
//        //            {
//        //                if (row.Cells[0].Value != null &&
//        //                    Convert.ToInt32(row.Cells[0].Value) == pId)
//        //                {
//        //                    foundRow = row;
//        //                    break;
//        //                }
//        //            }

//        //            if (foundRow != null)
//        //            {
//        //                _isUpdatingText = true;

//        //                try
//        //                {
//        //                    SuggestionGrid.Visible = false;
//        //                    string selectedText = (string)foundRow.Cells[2].Value;
//        //                    _lastSelectedProductText = selectedText;

//        //                    pId = Convert.ToInt32(foundRow.Cells[0].Value);
//        //                    ProductEngNameTxt.Text = selectedText;
//        //                    prod_U_Name = (string)foundRow.Cells[3].Value;
//        //                    Prod_Qty.Text = SuggestionGrid.CurrentRow.Cells[4].Value.ToString();
//        //                    PId = pId.ToString();
//        //                    P_StockQtyTxt.Text = "1";

//        //                    ProductDetailTxt.Focus();


//        //                    // Now do async operations while flag is still true
//        //                    await ShowProductPrices(pId);

//        //                    if (!string.IsNullOrEmpty(CustomerIdLbl.Text))
//        //                    {
//        //                        SetProductPreviousSalePrice(int.Parse(CustomerIdLbl.Text), productId: pId);
//        //                    }
//        //                    SuggestionGrid.Visible = false;
//        //                }
//        //                finally
//        //                {
//        //                    // This ensures flag is ALWAYS reset, even if an exception occurs
//        //                    _isUpdatingText = false;
//        //                    _lastSelectedProductText = "";
//        //                }

//        //            }

//        //            //SetProductPreviousSalePrice(customerId: string.IsNullOrEmpty(CustomerIdLbl.Text) ? 0 : int.Parse(CustomerIdLbl.Text),
//        //            //                      productId: pId);
//        //        }
//        //    }
//        //}


//        private async void SuggestionGrid_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Up)
//            {
//                // If we're on the first row, move focus back to TextBox
//                if (SuggestionGrid.CurrentRow != null &&
//                    SuggestionGrid.CurrentRow.Index == 0)
//                {
//                    ProductEngNameTxt.Focus();
//                    ProductEngNameTxt.SelectAll();
//                    SuggestionGrid.Visible = false;
//                    e.Handled = true;
//                }
//            }
//            else if (e.KeyCode == Keys.Left)
//            {
//                e.Handled = true;
//                e.SuppressKeyPress = true;
//                ProductEngNameTxt.Focus();
//                ProductEngNameTxt.SelectAll();
//                SuggestionGrid.Visible = false;
//            }
//            else if (e.KeyCode == Keys.Enter && !e.Handled)
//            {
//                e.Handled = true;
//                e.SuppressKeyPress = true;

//                if (SuggestionGrid.CurrentRow != null && SuggestionGrid.CurrentRow.Index >= 0)
//                {
//                    // Get the selected product ID
//                    int pId = Convert.ToInt32(SuggestionGrid.CurrentRow.Cells[0].Value);

//                    // Find the row in the grid
//                    DataGridViewRow foundRow = null;
//                    foreach (DataGridViewRow row in SuggestionGrid.Rows)
//                    {
//                        if (row.Cells[0].Value != null &&
//                            Convert.ToInt32(row.Cells[0].Value) == pId)
//                        {
//                            foundRow = row;
//                            break;
//                        }
//                    }

//                    if (foundRow != null)
//                    {
//                        // SET FLAG BEFORE ANY TEXT CHANGES!
//                        _isUpdatingText = true;

//                        try
//                        {
//                            string selectedText = (string)foundRow.Cells[2].Value;
//                            string selectedUrduName = (string)foundRow.Cells[3].Value;
//                            int selectedQty = Convert.ToInt32(foundRow.Cells[4].Value);
//                            pId = Convert.ToInt32(foundRow.Cells[0].Value);

//                            // NOW update all UI controls
//                            _lastSelectedProductText = selectedText;
//                            ProductEngNameTxt.Text = selectedText;  // ← ONLY ONE PLACE!
//                            prod_U_Name = selectedUrduName;
//                            Prod_Qty.Text = selectedQty.ToString();
//                            PId = pId.ToString();
//                            P_StockQtyTxt.Text = "1";
//                            SuggestionGrid.Visible = false;

//                            ProductDetailTxt.Focus();

//                            // Do async operations while flag is still true
//                            await ShowProductPrices(pId);

//                            if (!string.IsNullOrEmpty(CustomerIdLbl.Text))
//                            {
//                                SetProductPreviousSalePrice(int.Parse(CustomerIdLbl.Text), productId: pId);
//                            }
//                        }
//                        finally
//                        {
//                            // Reset flag after ALL operations complete
//                            _isUpdatingText = false;
//                            _lastSelectedProductText = "";
//                        }
//                    }
//                }
//            }
//        }

//        //private async void SuggestionGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
//        //{
//        //    if (SuggestionGrid.Rows.Count > 0)
//        //    {
//        //        int pId = Convert.ToInt32(SuggestionGrid.CurrentRow.Cells[0].Value);

//        //        ProductEngNameTxt.Text = (string)SuggestionGrid.CurrentRow.Cells[2].Value;

//        //        prod_U_Name = (string)SuggestionGrid.CurrentRow.Cells[3].Value;

//        //        Prod_Qty.Text = SuggestionGrid.CurrentRow.Cells[4].Value.ToString();
//        //        PId = pId.ToString();
//        //        P_StockQtyTxt.Text = "1";

//        //        SuggestionGrid.Visible = false;
//        //        ProductDetailTxt.Focus();

//        //       await ShowProductPrices(pId);

//        //        if (!string.IsNullOrEmpty(CustomerIdLbl.Text))
//        //        {
//        //            SetProductPreviousSalePrice(int.Parse(CustomerIdLbl.Text), productId: pId);
//        //        }

//        //        SuggestionGrid.Visible = false;
//        //    }
//        //}

//        private async void SuggestionGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
//        {
//            if (SuggestionGrid.Rows.Count > 0 && SuggestionGrid.CurrentRow != null)
//            {
//                // SET FLAG BEFORE ANY TEXT CHANGES!
//                _isUpdatingText = true;

//                try
//                {
//                    int pId = Convert.ToInt32(SuggestionGrid.CurrentRow.Cells[0].Value);
//                    string selectedText = (string)SuggestionGrid.CurrentRow.Cells[2].Value;
//                    string selectedUrduName = (string)SuggestionGrid.CurrentRow.Cells[3].Value;
//                    int selectedQty = Convert.ToInt32(SuggestionGrid.CurrentRow.Cells[4].Value);

//                    // Update all UI controls
//                    _lastSelectedProductText = selectedText;
//                    ProductEngNameTxt.Text = selectedText;
//                    prod_U_Name = selectedUrduName;
//                    Prod_Qty.Text = selectedQty.ToString();
//                    PId = pId.ToString();
//                    P_StockQtyTxt.Text = "1";
//                    SuggestionGrid.Visible = false;

//                    ProductDetailTxt.Focus();

//                    await ShowProductPrices(pId);

//                    if (!string.IsNullOrEmpty(CustomerIdLbl.Text))
//                    {
//                        SetProductPreviousSalePrice(int.Parse(CustomerIdLbl.Text), productId: pId);
//                    }
//                }
//                finally
//                {
//                    _isUpdatingText = false;
//                    _lastSelectedProductText = "";
//                }
//            }
//        }

//        private void SetProductPreviousSalePrice(int customerId, int productId)
//        {
//            using (var context = new POSDbContext())
//            {
//                IProductRepository productRepo = new ProductRepository(context);
//                var previousPricesTask = productRepo.ProductPreviousPriceInRecentOrderByCustomerId(customerId, productId);
//                ProductOrderHistoryDataGrid.DataSource = null;
//                ProductOrderHistoryDataGrid.DataSource = previousPricesTask.ToList();
//                ProductOrderHistoryDataGrid.RowHeadersVisible = false;
//                //ProductOrderHistoryDataGrid.CurrentCell = null;
//                ProductOrderHistoryDataGrid.ClearSelection();
//            }
//        }


//        private async Task ShowProductPrices(int productId)
//        {
//            using(var context =new  POSDbContext())
//            {
//                var data = await context.ProductPrices.Where(s => s.ProductId == productId).Select(s => new ProdDTO()
//                {
//                    Type = s.TypeName,
//                    Price = s.Price,
//                    Items = s.ItemsCount,
//                    P_Per_Item = s.PricePerItem
//                }).ToListAsync();

//                ProductPriceDataGridView.DataSource = null;
//                ProductPriceDataGridView.DataSource = data;
//                ProductPriceDataGridView.RowHeadersVisible = false;
//                ProductPriceDataGridView.ClearSelection();


//                if (data.Count() > 0)
//                {
//                    var d = data.FirstOrDefault();
//                    productTypeDropdown.SelectedValue = d.Type;
//                    ProductSalePrice.Text = ((int)d.Price).ToString();
//                    ProductAmount.Text = Convert.ToString(Convert.ToInt32(P_StockQtyTxt.Text) * Convert.ToInt32(ProductSalePrice.Text));
//                }
//            }
//        }

//        private async void SaveBillBtn_Click(object sender, EventArgs e)
//        {
//            if (CartProductList.Rows?.Count == 0)
//            {
//                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (MessageBox.Show("Are you sure you want to store Temporary Record?", "Save Confirmation",
//                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

//            string customerName = await GetCustomerNameAsync();
//            if (customerName == null) return; // User cancelled the dialog

//            await SaveOrderTransactionAsync(customerName);
//        }

//        private async Task<string> GetCustomerNameAsync()
//        {
//            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) || !string.IsNullOrEmpty(CustomerIdLbl.Text))
//                return CustomerNameTxt.Text;

//            using (var dialog = new InputDialog("Enter customer name:", "Customer Info"))
//            {
//                if (dialog.ShowDialog() != DialogResult.OK) return null;

//                string customerName = dialog.InputValue;
//                CustomerNameTxt.Text = customerName;
//                customerId = string.Empty;
//                CustomerIdLbl.Text = string.Empty;
//                return customerName;
//            }
//        }

//        private async Task SaveOrderTransactionAsync(string customerName)
//        {
//            using (var context = new POSDbContext())
//            using (var dbTransaction = context.Database.BeginTransaction())
//                try
//                {
//                    var orderRepository = new OrderRepository(context);
//                    var data = await GetTempOrderData();
//                    if (context.Orders.Any(o => o.InvoiceNumber == data.InvoiceNumber))
//                    {
//                        var orderDetail = await context.OrderDetails.Where(od => od.Order.InvoiceNumber == data.InvoiceNumber).ToListAsync();

//                        context.OrderDetails.RemoveRange(orderDetail);
//                        var existingOrder = await context.Orders.FirstOrDefaultAsync(o => o.InvoiceNumber == data.InvoiceNumber);
//                        if (existingOrder != null)
//                        {
//                            context.Orders.Remove(existingOrder);
//                        }
//                        await context.SaveChangesAsync();
//                    }

//                    var invoiceNo = await orderRepository.AddTempOrder(data);
//                    await SaveTempOrderDetails(context, invoiceNo);

//                    dbTransaction.Commit();
//                    ResetUIAfterSave();
//                    MessageBox.Show("Order Saved Successfully!", "Order Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                catch (DbException ex)
//                {
//                    dbTransaction.Rollback();
//                    MessageBox.Show("Order Creation Failed!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//        }

//        private void ResetUIAfterSave()
//        {
//            ClearInputs();
//            ClearCartFunction();
//            ClearCustomerPreviousTransactionGroup();
//            ResetCustomerBtn.Visible = false;
//            string invRef = TextFormatHelper.GetPrefix(Properties.Settings.Default.UserName);
//            InvoiceNoLbl.Text = invRef + DateTime.Now.ToString("ddMMyy-HHmmss");
//        }

//        private void TemOrderBtn_Click(object sender, EventArgs e)
//        {
//            // Create a new instance of your Form
//            Form ProductForm = new Form();
//            ProductForm.Text = "Temp Order Form";
//            ProductForm.StartPosition = FormStartPosition.CenterScreen;

//            // Create an instance of your User Control
//            var FormCtrl = new TempOrderControl();
//            FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

//            // Add the User Control to the new Form's controls collection
//            ProductForm.Controls.Add(FormCtrl);
//            ProductForm.Width = 1050; ProductForm.Height = 525;
//            // Show the new form
//            ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog

//            if (FormCtrl.isRecordSelected == true)
//            {
//                if (!string.IsNullOrEmpty(FormCtrl.InvoiceNoLbl.Text)) InvoiceNoLbl.Text = FormCtrl.InvoiceNoLbl.Text;


//                if (InvoiceNoLbl.Text != "InvoiceNo" && !string.IsNullOrEmpty(InvoiceNoLbl.Text))
//                {
//                    // CHECK FOR EXISTING ITEMS BEFORE LOADING
//                    if (CartProductList.Rows.Count > 0)
//                    {
//                        var result = MessageBox.Show("Loading this order will clear current cart. Continue?",
//                                                   "Confirm", MessageBoxButtons.YesNo);
//                        if (result != DialogResult.Yes) return;
//                    }


//                    if (FormCtrl.CustomerId != 0)
//                    {
//                        CustomerIdLbl.Text = FormCtrl.CustomerId.ToString();
//                        CustomerNameTxt.Text = FormCtrl.CustomerName;
//                        this.ResetCustomerBtn.Visible = true;
//                        this.ResetCustomerBtn.Enabled = true;

//                        ProductEngNameTxt.Focus();
//                        ProductEngNameTxt.SelectAll();

//                        // Hide the DataGridView after selection
//                        CustomerListDataGrid.Visible = false;
//                        using (var context = new POSDbContext())
//                        {
//                            IOrderRepository orderRepo = new OrderRepository(context);
//                            var customerPreviousDue = orderRepo.GetLatestOrderAmountSummaryByCustomerId(FormCtrl.CustomerId);
//                            UpdatePreviousOrderSummary(customerPreviousDue);
//                        }
//                    }
//                    else
//                    {

//                        ClearCustomerPreviousTransactionGroup();
//                        CustomerIdLbl.Text = string.Empty;
//                        CustomerNameTxt.Text = string.Empty;
//                        this.ResetCustomerBtn.Visible = false;
//                        this.ResetCustomerBtn.Enabled = false;
//                    }

//                    using (var context = new POSDbContext())
//                    {
//                        var orderRepo = new OrderRepository(context);
//                        var result = orderRepo.GetTempOrderDetailByInvoice(InvoiceNoLbl.Text);

//                        if (result != null && result.Count > 0)
//                        {
//                            isTempSaved = true;
//                            // CLEAR BEFORE ADDING to prevent duplicates
//                            CartProductList.Rows.Clear();

//                            foreach (var order in result)
//                            {
//                                string productId = order.ProductId.ToString() ?? "0";
//                                string finalName = !string.IsNullOrEmpty(order.ProductDetail) ?
//                                    $"{order.ProductName} {order.ProductDetail}" : order.ProductName;
//                                string productType = order.QuantityType;
//                                decimal salePrice = Math.Round(decimal.Parse(order.Price.ToString()), 1);
//                                int qty = order.Quantity;
//                                decimal amount = salePrice * qty;

//                                CartProductList.Rows.Add(null, amount, salePrice, finalName,
//                                                       productType, qty, productId, order.ProductDetail);
//                            }
//                            CalculateTotals();
//                        }
//                    }
//                }
//            }
//        }

//        private void ClearProductTblBtn_Click(object sender, EventArgs e)
//        {
//            var confirm = MessageBox.Show("Do you want to delete this product?",
//                                          "Confirm Delete",
//                                          MessageBoxButtons.YesNo,
//                                          MessageBoxIcon.Question);

//            if (confirm == DialogResult.Yes)
//            {
//                using (var ctx = new POSDbContext())
//                {
//                    ctx.Database.ExecuteSqlCommand("ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]");
//                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[OrderDetails]");
//                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[Orders]");
//                    ctx.Database.ExecuteSqlCommand(@"ALTER TABLE [dbo].[OrderDetails] 
//                                     ADD CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId] 
//                                     FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id])");

//                    // Now safely delete all products
//                    ctx.Database.ExecuteSqlCommand("DELETE FROM Products");

//                    // Optional: Reset identity seed if needed
//                    ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Products', RESEED, 0)");
//                    MessageBox.Show("Records has been Delete",
//                                             "Information",
//                                             MessageBoxButtons.OK,
//                                             MessageBoxIcon.Information);
//                }

//                InvoicePageTabControl.SelectedTab = BilPad;
//            }
//        }

//        private void GenerateInvoicePdfBtn_Click(object sender, EventArgs e)
//        {
//            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
//            {
//                // This is for PDF Invoice
//                GeneratePdfInvoice();
//            }
//            else
//            {
//                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//        }

//        private void ClearTempOrderTabls_Click(object sender, EventArgs e)
//        {
//            var confirm = MessageBox.Show("Do you want to Clear the Temporary orders?",
//                                         "Confirm Delete",
//                                         MessageBoxButtons.YesNo,
//                                         MessageBoxIcon.Question);

//            if (confirm == DialogResult.Yes)
//            {
//                using (var ctx = new POSDbContext())
//                {
//                    // Now safely delete all products
//                    ctx.Database.ExecuteSqlCommand("DELETE FROM TempOrderDetails");

//                    // Optional: Reset identity seed if needed
//                    ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('TempOrderDetails', RESEED, 0)");
//                    ctx.Database.ExecuteSqlCommand("DELETE FROM TempOrders");

//                    MessageBox.Show("Records has been Delete",
//                                             "Information",
//                                             MessageBoxButtons.OK,
//                                             MessageBoxIcon.Information);
//                }
//                InvoicePageTabControl.SelectedTab = BilPad;
//            }
//        }

//        private void ProductSalePrice_Enter(object sender, EventArgs e)
//        {
//            ProductSalePrice.SelectAll();
//        }

//        private void CartProductList_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Back && CartProductList.CurrentRow != null)
//            {
//                // Confirm deletion (optional)
//                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
//                                                    "Confirm Delete",
//                                                    MessageBoxButtons.YesNo,
//                                                    MessageBoxIcon.Question);

//                if (result == DialogResult.Yes)
//                {
//                    CartProductList.Rows.RemoveAt(CartProductList.CurrentRow.Index);
//                    CalculateTotals();
//                    CalculateReturnAmount();
//                    ProductEngNameTxt.Focus();
//                    ProductEngNameTxt.SelectAll();
//                }

//                e.Handled = true; // Mark event as handled
//            }
//        }

//        private void CustomerNameTxt_TextChange(object sender, EventArgs e)
//        {
//            if ((string.IsNullOrEmpty(CustomerNameTxt.Text) || CustomerNameTxt.Text.Length < 2))
//            {
//                _customerDebounceTimer?.Stop();
//                CustomerListDataGrid.Visible = false;
//                return;
//            }

//            _customerDebounceTimer.Stop();
//            _customerDebounceTimer.Start();
//            ShowSuggestions(CustomerNameTxt.Text, isForCustomer: true);
//            CustomerListDataGrid.Visible = true;
//        }

//        private void CustomerNameTxt_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Down && CustomerListDataGrid.Visible)
//            {
//                if (CustomerListDataGrid.Rows.Count > 0)
//                {
//                    CustomerListDataGrid.Focus();
//                    CustomerListDataGrid.Rows[0].Selected = true;
//                    e.Handled = true;
//                }
//            }
//            else if (e.KeyCode == Keys.Escape && CustomerListDataGrid.Visible)
//            {
//                CustomerListDataGrid.Visible = false;
//                ProductEngNameTxt.Focus();
//                e.Handled = true;
//            }
//        }

//        private void CustomerListDataGrid_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Up)
//            {
//                if (CustomerListDataGrid.CurrentRow != null &&
//                    CustomerListDataGrid.CurrentRow.Index == 0)
//                {
//                    CustomerNameTxt.Focus();
//                    CustomerNameTxt.SelectAll();
//                    CustomerListDataGrid.Visible = false;
//                    e.Handled = true;
//                    return;
//                }
//            }
//            else if (e.KeyCode == Keys.Enter)
//            {
//                e.Handled = true;
//                e.SuppressKeyPress = true;

//                // Check if the DataGridView is in edit mode
//                if (CustomerListDataGrid.IsCurrentCellInEditMode)
//                {
//                    // If editing, end the edit first
//                    CustomerListDataGrid.EndEdit();
//                    return;
//                }

//                if (CustomerListDataGrid.CurrentRow != null &&
//                    CustomerListDataGrid.CurrentRow.Index >= 0 &&
//                    !CustomerListDataGrid.CurrentRow.IsNewRow)
//                {
//                    int pId = Convert.ToInt32(CustomerListDataGrid.CurrentRow.Cells[0].Value);

//                    DataGridViewRow foundRow = null;
//                    foreach (DataGridViewRow row in CustomerListDataGrid.Rows)
//                    {
//                        if (row.Cells[0].Value != null &&
//                            Convert.ToInt32(row.Cells[0].Value) == pId)
//                        {
//                            foundRow = row;
//                            break;
//                        }
//                    }

//                    if (foundRow != null)
//                    {
//                        pId = Convert.ToInt32(foundRow.Cells[0].Value);
//                        CustomerIdLbl.Text = pId.ToString();
//                        CustomerNameTxt.Text = $"{(string)foundRow.Cells[1].Value}";

//                        ProductEngNameTxt.Focus();
//                        ProductEngNameTxt.SelectAll();
//                        this.ResetCustomerBtn.Visible = true;

//                        // Hide the DataGridView after selection
//                        CustomerListDataGrid.Visible = false;

//                        using (var context = new POSDbContext())
//                        {
//                            IOrderRepository orderRepo = new OrderRepository(context);
//                            var customerPreviousDue = orderRepo.GetLatestOrderAmountSummaryByCustomerId(pId);
//                            UpdatePreviousOrderSummary(customerPreviousDue);
//                        }
//                    }
//                }
//            }
//        }

//        private void UpdatePreviousOrderSummary(OrderAmountSummaryDto customerPreviousDue)
//        {
//            PreviousOrderSummaryLbl.Text = string.Empty;
//            if (customerPreviousDue == null) return;

//            previousBillAmountLbl.Text = customerPreviousDue.TotalAmount.ToString();
//            PreviousReceivedAmountLbl.Text = customerPreviousDue.ReceivedAmount.ToString();

//            float remainingAmount = customerPreviousDue.TotalAmount - customerPreviousDue.ReceivedAmount;

//            if (remainingAmount == 0) return;

//            var isPositive = remainingAmount >= 0;
//            PreviousOrderSummaryLbl.Text = isPositive
//                ? $"Remaining Amt:  Rs. {remainingAmount}"
//                : $"Return Amt:  Rs. {Math.Abs(remainingAmount)}";

//            PreviousOrderSummaryLbl.ForeColor = isPositive ? Color.Red : Color.Blue;
//            PreviousOrderSummaryLbl.Visible = true;
//        }


//        private void AddNewCustomerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
//        {
//            // Create a new instance of your Form
//            Form customerForm = new Form();
//            customerForm.Text = "Add New Customer";
//            customerForm.StartPosition = FormStartPosition.CenterScreen;

//            // Create an instance of your User Control
//            var CustomerFormCtrl = new Views.Controllers.Customers.CustomerFormControl();
//            CustomerFormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

//            // Add the User Control to the new Form's controls collection
//            customerForm.Controls.Add(CustomerFormCtrl);
//            customerForm.Width = 1050; customerForm.Height = 625;
//            // Show the new form
//            customerForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
//        }

//        private void ProductEngNameTxt_Enter(object sender, EventArgs e)
//        {
//            CustomerListDataGrid.Visible = false;
//        }

//        private void CustomerNameTxt_Enter(object sender, EventArgs e)
//        {
//            SuggestionGrid.Visible = false;
//        }

//        private async void SaveOrderWithoutPrintBtn_Click(object sender, EventArgs e)
//        {
//            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
//            {
//                LoadingManager.ShowLoading();
//                bool IsDone = false;
//                if (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text) && PreviousOrderIdLbl.Text != "Prev Order Id")
//                    IsDone = await SaveOrder(true);  //await UpdateOrderSaved();
//                else
//                    IsDone = await SaveOrder(false);  // await NewOrderSaved();

//                if (IsDone)
//                {
//                    LoadingManager.HideLoading();
//                    if (isTempSaved)
//                    {
//                        string sql = "DELETE FROM TempOrders WHERE InvoiceNumber = @InvoiceNumber";
//                        string sql1 = "DELETE FROM TempOrderDetails WHERE TempInvoiceNumber = @InvoiceNumber";

//                        using (var context = new POSDbContext())
//                        {
//                            var parameters1 = new[]
//                            {
//                                new System.Data.SqlClient.SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text)
//                            };

//                            context.Database.ExecuteSqlCommand(sql1, parameters1);

//                            var parameters = new[]
//                            {
//                                new System.Data.SqlClient.SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text),
//                            };
//                            context.Database.ExecuteSqlCommand(sql, parameters);
//                        }
//                    }

//                    ResetUIAfterSave();
//                    MessageBox.Show("Order Saved Successfully!", "Order Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                else
//                {
//                    MessageBox.Show("Order Creation Failed!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//        }

//        private void ExportBtn_Click(object sender, EventArgs e)
//        {
//            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
//            {
//                var orderDetailList = new List<OrderDetail>();


//                System.Data.DataTable exportTable = new System.Data.DataTable();
//                exportTable.TableName = "Products";

//                // Add columns
//                exportTable.Columns.Add("ProductID", typeof(int));
//                exportTable.Columns.Add("ProductName", typeof(string));
//                exportTable.Columns.Add("Qty", typeof(int));
//                exportTable.Columns.Add("ProductType", typeof(string));
//                exportTable.Columns.Add("SalePrice", typeof(string));

//                foreach (DataGridViewRow row in CartProductList.Rows)
//                {
//                    if (row.Cells["ProductId"].Value == null) continue;

//                    var productIdValue = row.Cells["ProductId"].Value?.ToString();
//                    var odrDetail = new OrderDetail
//                    {
//                        ProductId = string.IsNullOrEmpty(productIdValue) ? (int?)null : int.Parse(productIdValue),
//                        OtherProductName = row.Cells["Urdu Name"].Value?.ToString(),
//                        Quantity = int.Parse(row.Cells["Qty"].Value?.ToString()),
//                        QuantityType = row.Cells["ProductType"].Value?.ToString(),
//                        Price = float.Parse(row.Cells["SalePrice"].Value?.ToString()),
//                        //CreatedDate = DateTime.Now,
//                        //OrderId = orderId,
//                        ProductDetail = row.Cells["ProductDetail"].Value?.ToString()
//                    };

//                    exportTable.Rows.Add(odrDetail.ProductId, odrDetail.OtherProductName, odrDetail.Quantity, odrDetail.QuantityType, odrDetail.Price);

//                }

//                // 3. Ask where to save the file
//                using (var sfd = new SaveFileDialog
//                {
//                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
//                    FileName = "CustomerOrder.xlsx"
//                })
//                {
//                    if (sfd.ShowDialog() == DialogResult.OK)
//                    {
//                        // 4. Write to Excel using ClosedXML
//                        using (var workbook = new XLWorkbook())
//                        {
//                            workbook.Worksheets.Add(exportTable, "CustomerOrderSheet");
//                            workbook.SaveAs(sfd.FileName);
//                        }
//                        MessageBox.Show("Export successful!");
//                    }
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please Add the Product first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//        }

//        private void BrowsOrderExcelFile_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog ofd = new OpenFileDialog();
//            // Set the filter to show only .bak files
//            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|All files|*.*";
//            ofd.Title = "Select an Excel File";

//            if (ofd.ShowDialog() == DialogResult.OK)
//            {
//                ImportUpdatedFilePathTxt.Text = ofd.FileName;
//                LoadOrderExcelFileBtn.Enabled = true;
//            }
//        }

//        private void LoadOrderExcelFileBtn_Click(object sender, EventArgs e)
//        {
//            using (var stream = File.Open(ImportUpdatedFilePathTxt.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
//            {
//                //// Register encoding provider (needed for older Excel files, e.g., .xls)
//                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
//                using (var reader = ExcelReaderFactory.CreateReader(stream))
//                {
//                    var conf = new ExcelDataSetConfiguration
//                    {
//                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
//                        {
//                            UseHeaderRow = true
//                        }
//                    };


//                    var dataSet = reader.AsDataSet(conf);

//                    if (dataSet.Tables.Count == 0)
//                    {
//                        MessageBox.Show("No worksheets found in the file.", "No data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }


//                    var currentTable = dataSet.Tables[0];

//                    System.Data.DataTable filtered = new System.Data.DataTable();
//                    // Add only required columns



//                    filtered.Columns.Add("ProductID", typeof(int));
//                    filtered.Columns.Add("ProductName", typeof(string));
//                    filtered.Columns.Add("Qty", typeof(string));
//                    filtered.Columns.Add("ProductType", typeof(string));
//                    filtered.Columns.Add("SalePrice", typeof(int));

//                    // CLEAR EXISTING ITEMS FIRST to prevent duplicates
//                    CartProductList.Rows.Clear();
//                    // Copy rows
//                    foreach (DataRow row in currentTable.Rows)
//                    {
//                        //// Skip rows that are empty or header duplicates
//                        //if (row[0] == DBNull.Value || row[0].ToString() == "ID")
//                        //    continue;



//                        string productId = row[0].ToString() ?? "0";
//                        string finalName = row[1].ToString();

//                        string productType = row[3].ToString();
//                        decimal salePrice = Math.Round(decimal.Parse(row[4].ToString()), 1);
//                        int qty = Convert.ToInt32(row[2].ToString());
//                        decimal amount = salePrice * qty;

//                        CartProductList.Rows.Add(null, amount, salePrice, finalName,
//                                               productType, qty, productId, null);
//                    }
//                    CalculateTotals();
//                }
//            }
//            ImportUpdatedFilePathTxt.Text = string.Empty;
//            InvoicePageTabControl.SelectedTab = BilPad;
//        }


//        // Updates product price when product type is selected from dropdown
//        private async void productTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            //var i = PId;
//            //// Check if SelectedValue is null or the default option
//            //if (productTypeDropdown.SelectedItem == null || string.IsNullOrEmpty(productTypeDropdown.SelectedValue?.ToString()))
//            //    return;


//            //// Get the selected ID as integer
//            //string selectedValue = Convert.ToString(productTypeDropdown.SelectedValue);
//            //if (!string.IsNullOrEmpty(PId))
//            //{
//            //    int pid = Convert.ToInt32(PId);
//            //    using (var context = new POSDbContext())
//            //    {
//            //        var price = context.ProductPrices.Where(s=>s.ProductId== pid && s.TypeName== selectedValue).Select(s => new ProdPricesdto()
//            //        {
//            //            price = s.Price,
//            //            ItemCount = s.ItemsCount
//            //        }).FirstOrDefault();

//            //        if(price != null)
//            //        {
//            //            ProductSalePrice.Text = $"{price.price:0}";
//            //            prod_ItemCountTxt.Text = price.ItemCount.ToString();
//            //        }
//            //        else
//            //        {
//            //            ProductSalePrice.Text = "0";
//            //            prod_ItemCountTxt.Text = "0";
//            //        }               
//            //    }
//            //}
//            //else
//            //    ProductSalePrice.Text = "0";

//            //if (!string.IsNullOrEmpty(P_StockQtyTxt.Text))
//            //    ProductAmount.Text = Convert.ToString(Convert.ToInt32(P_StockQtyTxt.Text) * Convert.ToInt32(ProductSalePrice.Text));


//            if (productTypeDropdown.SelectedItem == null ||
//       string.IsNullOrEmpty(productTypeDropdown.SelectedValue?.ToString())) return;

//            string selectedValue = productTypeDropdown.SelectedValue.ToString();

//            if (!string.IsNullOrEmpty(PId) && int.TryParse(PId, out int pid))
//            {
//                using (var context = new POSDbContext())
//                {
//                    var price = await context.ProductPrices
//                        .Where(s => s.ProductId == pid && s.TypeName == selectedValue)
//                        .Select(s => new ProdPricesdto { price = s.Price, ItemCount = s.ItemsCount })
//                        .FirstOrDefaultAsync(); // ← async, non-blocking

//                    ProductSalePrice.Text = price != null ? $"{price.price:0}" : "0";
//                    prod_ItemCountTxt.Text = price != null ? price.ItemCount.ToString() : "0";
//                }
//            }
//            else
//                ProductSalePrice.Text = "0";

//            if (!string.IsNullOrEmpty(P_StockQtyTxt.Text) &&
//                int.TryParse(P_StockQtyTxt.Text, out int qty) &&
//                int.TryParse(ProductSalePrice.Text, out int sp))
//                ProductAmount.Text = (qty * sp).ToString();


//        }
//    }


//}





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
using POS_Shop.Repositories;
using POS_Shop.Views.Controllers.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
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
            CartProductList.ColumnCount = 7;

            CartProductList.Columns[0].Name = Col.Amount;
            CartProductList.Columns[1].Name = Col.SalePrice;
            CartProductList.Columns[2].Name = Col.UrduName;
            CartProductList.Columns[3].Name = Col.ProductType;
            CartProductList.Columns[4].Name = Col.Qty;
            CartProductList.Columns[5].Name = Col.ProductId;
            CartProductList.Columns[6].Name = Col.Detail;

            CartProductList.Columns[Col.Amount].Width = 100;
            CartProductList.Columns[Col.SalePrice].Width = 60;
            CartProductList.Columns[Col.UrduName].Width = 190;
            CartProductList.Columns[Col.ProductType].Width = 30;
            CartProductList.Columns[Col.Qty].Width = 50;
            CartProductList.Columns[Col.ProductId].Width = 50;

            CartProductList.Columns[Col.ProductId].Visible = false;
            CartProductList.Columns[Col.Detail].Visible = false;

            CartProductList.Columns[Col.Amount].ReadOnly = true;
            CartProductList.Columns[Col.UrduName].ReadOnly = true;
            CartProductList.Columns[Col.ProductType].ReadOnly = true;

            // Delete button column — inserted at position 0
            var btnCol = new DataGridViewButtonColumn
            {
                Name = Col.Delete,
                HeaderText = "Action",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 50
            };
            CartProductList.Columns.Insert(0, btnCol);
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
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("U-Name", typeof(string));
            dt.Columns.Add("Qty", typeof(int));

            foreach (var item in suggestions)
                dt.Rows.Add(
                    item.ProductId,
                    item.purchasePrice,
                    item.ProductName,
                    TextFormatHelper.FormatMixedText(item.ProductUrduName),
                    item.Qty);

            SuggestionGrid.SuspendLayout();
            SuggestionGrid.ReadOnly = true;
            SuggestionGrid.AllowUserToAddRows = false;
            SuggestionGrid.DataSource = dt;
            SuggestionGrid.Columns[0].Width = 40;
            SuggestionGrid.Columns[1].Width = 50;
            SuggestionGrid.Columns[2].Width = 200;
            SuggestionGrid.Columns[3].Width = 200;
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
                        $"Available stock is {availableQty} pieces. Please enter a valid quantity.",
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
                    existingQty += qty;
                    row.Cells[Col.Qty].Value = existingQty;
                    row.Cells[Col.Amount].Value = Math.Round(existingQty * salePrice, 1);
                    productExists = true;
                    break;
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
            if (e.RowIndex >= 0 && CartProductList.Columns[e.ColumnIndex].Name == Col.Delete)
            {
                var confirm = MessageBox.Show("Delete this product from cart?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    CartProductList.Rows.RemoveAt(e.RowIndex);
                    CalculateTotals();
                    CalculateReturnAmount();
                    ProductEngNameTxt.Focus();
                    ProductEngNameTxt.SelectAll();
                }
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
                                        $"Available stock is {product.Qty} pieces.",
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
            if (e.KeyCode == Keys.Back && CartProductList.CurrentRow != null)
            {
                var result = MessageBox.Show("Delete this record?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CartProductList.Rows.RemoveAt(CartProductList.CurrentRow.Index);
                    CalculateTotals();
                    CalculateReturnAmount();
                    ProductEngNameTxt.Focus();
                    ProductEngNameTxt.SelectAll();
                }
                e.Handled = true;
            }
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
            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) &&!string.IsNullOrEmpty(CustomerIdLbl.Text) &&int.TryParse(CustomerIdLbl.Text, out int parsedId))
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

        private void CustomerListDataGrid_KeyDown(object sender, KeyEventArgs e)
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

                if (CustomerListDataGrid.CurrentRow != null &&!CustomerListDataGrid.CurrentRow.IsNewRow)
                    SelectCustomerFromRow(CustomerListDataGrid.CurrentRow);
            }
        }

        private void CustomerListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && CustomerListDataGrid.CurrentRow != null)
                SelectCustomerFromRow(CustomerListDataGrid.CurrentRow);
        }

        private void SelectCustomerFromRow(DataGridViewRow row)
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
                ProductOrderHistoryDataGrid.DataSource = null;
                ProductOrderHistoryDataGrid.DataSource = history.ToList();
                ProductOrderHistoryDataGrid.RowHeadersVisible = false;
                ProductOrderHistoryDataGrid.ClearSelection();
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

        private void TemOrderBtn_Click(object sender, EventArgs e)
        {
            var form = new Form { Text = "Temp Order Form", StartPosition = FormStartPosition.CenterScreen };
            var ctrl = new TempOrderControl { Dock = DockStyle.Fill };
            form.Controls.Add(ctrl);
            form.Width = 1050; form.Height = 525;
            form.ShowDialog();

            if (!ctrl.isRecordSelected) return;
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
            }
            else
            {
                ClearCustomerPreviousTransactionGroup();
                CustomerIdLbl.Text = CustomerNameTxt.Text = string.Empty;
                ResetCustomerBtn.Visible = ResetCustomerBtn.Enabled = false;
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
                case Keys.Alt | Keys.F4:
                    this.Close(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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

        private void InvoiceShopName_CheckedChanged(object sender, EventArgs e)
            => InvoiceShopName.Text = InvoiceShopName.Checked
                ? "Hide Shop Name in Invoice" : "Show Shop Name in Invoice";

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

        private void SearchInvoiceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                CustomerIdLbl.Text = ctrl.CustomerId.ToString();
                CustomerNameTxt.Text = ctrl.CustomerName;
                ResetCustomerBtn.Visible = ResetCustomerBtn.Enabled = true;
                CustomerListDataGrid.Visible = false;
            }
            else
            {
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
    }
}
