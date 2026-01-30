using ClosedXML.Excel;
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
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using Order = POS_Shop.Models.Order;
using Rectangle = System.Drawing.Rectangle;

namespace POS_Shop.Views.BillScreen
{
    public partial class BillPadForm : Form
    {
        string PId { get; set; }
        string customerId { get; set; } = string.Empty;
        public string prod_U_Name { get; set; }
        public bool isTempSaved { get; set; } = false;

        public BillPadForm()
        {
            InitializeComponent();

            // Initialize form state with default values
            CustomerIdLbl.Text = string.Empty;
            CustomerNameTxt.Text = string.Empty;
            PreviousOrderIdLbl.Text = string.Empty;
            string invRef = TextFormatHelper.GetPrefix(Properties.Settings.Default.UserName);
            InvoiceNoLbl.Text = invRef + DateTime.Now.ToString("ddMMyy-HHmmss");
            this.Shown += (s, e) => { ProductEngNameTxt.Focus(); };

            CustomerListDataGrid.BringToFront();
            SetItemGridView();

            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;

            // Use .ToString() or check for null
            string savedRole = Properties.Settings.Default.UserRole?.ToString() ?? string.Empty;

            if (!savedRole.Equals(AuthUserRole.SuperAdmin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                InvoicePageTabControl.TabPages.Remove(TruncateTableTab);
                // InvoicePageTabControl.TabPages.Remove(ImpoertOrderFileTab);
            }
            InitializeProductUnitsDropdown();
        }

        private void InitializeProductUnitsDropdown()
        {
            using (var context = new POSDbContext())
            {
                var productUnitRepo = new ProductUnitRepository(context);
                var productUnit = productUnitRepo.GetAll().Select(s => new ProductUnit()
                {
                    Id = s.Id,
                    Name = s.Name,

                }).ToList();
                productTypeDropdown.Items.Clear();

                // Add default option
                var allItems = new List<ProductUnit>();
                //allItems.Add(new ProductUnit { Id = 0, Name = "" });
                allItems.AddRange(productUnit);
                productTypeDropdown.DataSource = allItems;
                productTypeDropdown.DisplayMember = "Name";
                productTypeDropdown.ValueMember = "Name";
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                if (SuggestionGrid.Visible && this.ActiveControl == SuggestionGrid)
                    return;

                // Don't override Enter for these controls
                if (this.ActiveControl == ProductEngNameTxt ||
                    this.ActiveControl == CustomerNameTxt ||
                    this.ActiveControl == TopBarSearchProductTxt ||
                     this.ActiveControl == CustomerListDataGrid)
                    return;

                e.SuppressKeyPress = true;

                this.SelectNextControl(
                    this.ActiveControl,
                    true, true, true, true
                );
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (SuggestionGrid.Visible && this.ActiveControl == SuggestionGrid)
                    return;

                // Don't override Enter for these controls
                if (this.ActiveControl == ProductEngNameTxt ||
                    this.ActiveControl == CustomerNameTxt ||
                    this.ActiveControl == TopBarSearchProductTxt ||
                     this.ActiveControl == CustomerListDataGrid)
                    return;

                e.SuppressKeyPress = true;

                this.SelectNextControl(
                    this.ActiveControl,
                    false, true, true, true
                );
            }
        }

        private void SetItemGridView()
        {
            CartProductList.ColumnCount = 7;

            CartProductList.Columns[0].Name = "Amount";
            CartProductList.Columns[1].Name = "SalePrice";
            CartProductList.Columns[2].Name = "Urdu Name";
            CartProductList.Columns[3].Name = "ProductType";
            CartProductList.Columns[4].Name = "Qty";
            CartProductList.Columns[5].Name = "ProductId";
            CartProductList.Columns[6].Name = "ProductDetail";

            // Set column widths here
            CartProductList.Columns[0].Width = 100;
            CartProductList.Columns[1].Width = 60;
            CartProductList.Columns[2].Width = 190;
            CartProductList.Columns[3].Width = 30;
            CartProductList.Columns[4].Width = 50;
            CartProductList.Columns[5].Width = 50;

            CartProductList.Columns[5].Visible = false;
            CartProductList.Columns[6].Visible = false;

            CartProductList.Columns["Amount"].ReadOnly = true; // Amount
            CartProductList.Columns["Urdu Name"].ReadOnly = true; // Urdu Name
            CartProductList.Columns["ProductType"].ReadOnly = true; // ProductType

            // Add delete button column
            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.Name = "Delete";
            btnCol.HeaderText = "Action";
            btnCol.Text = "Delete";
            btnCol.UseColumnTextForButtonValue = true;  // Always show "Delete"
            // Insert at position 0 (first column)
            CartProductList.Columns.Insert(0, btnCol);

            // Set the width of the button column
            CartProductList.Columns["Delete"].Width = 50;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
            {
                MessageBox.Show("Please Clear the Cart First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            else
            {
                base.OnFormClosing(e);
            }

        }

        private void BackScreenBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
                MessageBox.Show("Please Clear the Cart First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidateInputs()
        {
            // Product ID
            if (!OtherProductChk.Checked)
            {
                if (string.IsNullOrWhiteSpace(PId))
                {
                    MessageBox.Show("Product ID is required.", "Validation Error");
                    return false;
                }
            }

            // Product Name
            if (string.IsNullOrWhiteSpace(ProductEngNameTxt.Text))
            {
                MessageBox.Show("Product name is required.", "Validation Error");
                return false;
            }

            if (!OtherProductChk.Checked)
            {
                // Unit
                if (string.IsNullOrWhiteSpace(prod_U_Name))
                {
                    MessageBox.Show("Unit name is required.", "Validation Error");
                    return false;
                }
            }

            // Quantity
            if (!int.TryParse(P_StockQtyTxt.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Enter a valid quantity.", "Validation Error");
                return false;
            }
            // Product Type
            if (productTypeDropdown.SelectedItem == null)
            {
                MessageBox.Show("Please select a product type.", "Validation Error");
                return false;
            }
            // Price
            if (!decimal.TryParse(ProductSalePrice.Text, out decimal salePrice))
            {
                MessageBox.Show("Enter a valid sale price.", "Validation Error");
                return false;
            }
            return true; // ✅ Passed all checks
        }


        //private void AddToCardBtn_Click(object sender, EventArgs e)
        //{

        //    if (!ValidateInputs())
        //        return; // stop if validation fails

        //    // Get values from the TextBoxes
        //    string productId = PId; // (or use the label SearchProductUI.ProdIdLbl.Text)
        //    string productName = ProductEngNameTxt.Text;
        //    string ProductUrduName = prod_U_Name;
        //    string productType = productTypeDropdown.SelectedItem?.ToString();
        //    decimal salePrice = Math.Round(decimal.Parse(ProductSalePrice.Text), 1);
        //    int qty = int.Parse(P_StockQtyTxt.Text);
        //    decimal amount = salePrice * qty;

        //    bool productExists = false;
        //    var finalName = OtherProductChk.Checked == false ? $"{ProductUrduName} {ProductDetailTxt.Text}" : $"{productName} {ProductDetailTxt.Text}";


        //    //string formattedText = FixCommonPatterns(finalName);

        //    string formattedText = TextFormatHelper.FormatMixedText(finalName);
        //    var finalPId = OtherProductChk.Checked == false ? productId : "";
        //    //if (!OtherProductChk.Checked)
        //    //{
        //    // Loop through DataGridView rows to check if product already exists
        //    foreach (DataGridViewRow row in CartProductList.Rows)
        //    {

        //        string existingName = row.Cells["Urdu Name"].Value.ToString();
        //        // Remove directional characters for comparison
        //        string cleanExisting = TextFormatHelper.RemoveDirectionalCharacters(existingName);
        //        string cleanNew = TextFormatHelper.RemoveDirectionalCharacters(formattedText);

        //        if (string.Equals(
        //            cleanExisting.Trim(),
        //            cleanNew.Trim(),
        //            StringComparison.OrdinalIgnoreCase))
        //        {
        //            // Product already exists → increase Qty & update Amount
        //            int existingQty = int.Parse(row.Cells["Qty"].Value.ToString());
        //            existingQty += qty;
        //            row.Cells["Qty"].Value = existingQty;

        //            decimal newAmount = existingQty * salePrice;
        //            row.Cells["Amount"].Value = Math.Round(newAmount, 1);
        //            productExists = true;
        //            break;
        //        }

        //    }
        //    //}

        //    // If product doesn’t exist, add a new row
        //    if (!productExists)
        //    {
        //        //CartProductList.Rows.Add(finalPId, finalName, productType, qty,salePrice, amount);
        //        CartProductList.Rows.Add(null, amount, salePrice, formattedText, productType, qty, finalPId, ProductDetailTxt.Text);
        //    }

        //    CalculateTotals();
        //    CalculateReturnAmount();

        //    // Clear input fields after adding
        //    ClearInputs();
        //    ProductEngNameTxt.Focus();
        //}


        private void AddToCardBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return; // stop if validation fails

            // Get values from the TextBoxes
            string productId = PId;
            string productName = ProductEngNameTxt.Text;
            string ProductUrduName = prod_U_Name;
            string productType = productTypeDropdown.SelectedValue?.ToString();
            decimal salePrice = Math.Round(decimal.Parse(ProductSalePrice.Text), 1);
            int qty = int.Parse(P_StockQtyTxt.Text);
            decimal amount = salePrice * qty;
            string productDetail = ProductDetailTxt.Text;

            bool productExists = false;
            var finalName = OtherProductChk.Checked == false ? $"{ProductUrduName} {productDetail}" : $"{productName} {productDetail}";

            string formattedText = TextFormatHelper.FormatMixedText(finalName);
            var finalPId = OtherProductChk.Checked == false ? productId : "";


            //// checking the available stock
            //int availableQty =int.Parse(Prod_Qty.Text);
            //if(qty > availableQty)
            //{
            //    MessageBox.Show($"Available stock is {availableQty}. Please enter a valid quantity.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            // IMPROVED DUPLICATE CHECK - Compare multiple properties
            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells["ProductId"].Value == null) continue;

                string existingProductId = row.Cells["ProductId"].Value?.ToString();
                string existingName = row.Cells["Urdu Name"].Value?.ToString();
                string existingDetail = row.Cells["ProductDetail"].Value?.ToString();
                decimal existingPrice = row.Cells["SalePrice"].Value != null ? Convert.ToDecimal(row.Cells["SalePrice"].Value) : 0;
                string existingType = row.Cells["ProductType"].Value?.ToString();


                string cleanExisting = TextFormatHelper.RemoveDirectionalCharacters(existingName);
                string cleanNew = TextFormatHelper.RemoveDirectionalCharacters(formattedText);

                if (string.Equals(
                    cleanExisting.Trim(),
                    cleanNew.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    // Product already exists → increase Qty & update Amount
                    int existingQty = row.Cells["Qty"].Value != null ?
                        int.Parse(row.Cells["Qty"].Value.ToString()) : 0;
                    existingQty += qty;
                    row.Cells["Qty"].Value = existingQty;

                    decimal newAmount = existingQty * salePrice;
                    row.Cells["Amount"].Value = Math.Round(newAmount, 1);
                    productExists = true;
                    break;
                }

            }

            // If product doesn't exist, add a new row
            if (!productExists)
            {
                CartProductList.Rows.Add(null, amount, salePrice, formattedText,
                                       productType, qty, finalPId, productDetail);
            }

            CalculateTotals();
            CalculateReturnAmount();

            // Clear input fields after adding
            ClearInputs();
            ProductEngNameTxt.Focus();
        }

        private void CartProductList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the row index is valid
            if (e.RowIndex >= 0)
            {
                var row = CartProductList.Rows[e.RowIndex];

                try
                {
                    // Only recalc if Qty or SalePrice column changed
                    if (CartProductList.Columns[e.ColumnIndex].Name == "Qty" ||
                        CartProductList.Columns[e.ColumnIndex].Name == "SalePrice")
                    {
                        decimal salePrice = Convert.ToDecimal(row.Cells["SalePrice"].Value);
                        int qty = Convert.ToInt32(row.Cells["Qty"].Value);
                        decimal newAmount = salePrice * qty;
                        row.Cells["Amount"].Value = Math.Round(newAmount, 1);
                        CalculateTotals();
                        CalculateReturnAmount();
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid input. Please enter correct numeric values.");
                    row.Cells[e.ColumnIndex].Value = 0; // reset wrong cell
                }
            }
        }

        private void CalculateTotals()
        {
            int totalItems = 0;
            decimal subTotal = 0;

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                // Count each row as 1 item (skip empty rows)
                if (row.Cells[1].Value != null) // Check if product name exists
                {
                    totalItems++;
                }
                // Skip empty rows
                if (row.Cells["Amount"].Value != null)
                {
                    subTotal += Convert.ToDecimal(row.Cells["Amount"].Value);
                }
            }

            // Update your UI elements with the calculated totals
            TotalItemLbl.Text = totalItems.ToString();
            TotalAmountLbl.Text = subTotal.ToString();
        }

        private void ClearInputs()
        {
            PId = string.Empty;
            ProductEngNameTxt.Clear();
            prod_U_Name = string.Empty;
            ProductSalePrice.Clear();
            P_StockQtyTxt.Clear();
            ProductAmount.Clear();
            productTypeDropdown.SelectedIndex = -1;
            ProductDetailTxt.Clear();
            OtherProductChk.Checked = false;
            Prod_Qty.Clear();

            ProductOrderHistoryDataGrid.DataSource = null;
            ProductPriceDataGridView.DataSource = null;
        }

        private void ProductSalePrice_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProductSalePrice.Text) && !string.IsNullOrEmpty(P_StockQtyTxt.Text))
            {
                var amt = decimal.Parse(ProductSalePrice.Text) * int.Parse(P_StockQtyTxt.Text);
                ProductAmount.Text = Convert.ToString(amt);
            }
        }

        private void P_StockQtyTxt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProductSalePrice.Text) && !string.IsNullOrEmpty(P_StockQtyTxt.Text))
            {
                var amt = decimal.Parse(ProductSalePrice.Text) * int.Parse(P_StockQtyTxt.Text);
                ProductAmount.Text = Convert.ToString(amt);
            }
        }

        private void P_StockQtyTxt_Enter(object sender, EventArgs e)
        {
            P_StockQtyTxt.SelectAll();
        }

        private void ProductEngNameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!OtherProductChk.Checked)
                {
                    if (SuggestionGrid.Visible == false)
                    {
                        ShowSuggestions(ProductEngNameTxt.Text);
                    }
                    else
                    {
                        SuggestionGrid.Visible = false;
                    }
                    e.Handled = true;
                }
            }
        }

        private void ProductEngNameTxt_TextChange(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(ProductEngNameTxt.Text) || ProductEngNameTxt.Text.Length < 2))
            {
                SuggestionGrid.Visible = false;
                return;
            }
            if (OtherProductChk.Checked == false)
            {
                ShowSuggestions(ProductEngNameTxt.Text);
            }
        }

        private void ProductEngNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && SuggestionGrid.Visible)
            {
                if (SuggestionGrid.Rows.Count > 0)
                {
                    SuggestionGrid.Focus();
                    SuggestionGrid.Rows[0].Selected = true;
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Escape && SuggestionGrid.Visible)
            {
                SuggestionGrid.Visible = false;
                ProductEngNameTxt.Focus();
                e.Handled = true;
            }
        }

        private async void ShowSuggestions(string searchText, bool isForCustomer = false)
        
        {
            try
            {
                if (isForCustomer)
                {
                    using (var context = new POSDbContext())
                    {
                        ICustomerRepository customerRepository = new CustomerRepository(context);
                        var result = await customerRepository.GetCustomerPagingListAsync(pageIndex: 1, pageSize: 100, searchText);

                        System.Data.DataTable dt1 = new System.Data.DataTable();
                        dt1.Columns.Add("ID", typeof(int));
                        dt1.Columns.Add("Name", typeof(string));
                        dt1.Columns.Add("Address", typeof(string));

                        foreach (var item in result.data)
                        {
                            dt1.Rows.Add(item.Id, item.CustomerName, item.CustomerAddress);
                        }

                        CustomerListDataGrid.ReadOnly = true;
                        CustomerListDataGrid.AllowUserToAddRows = false;
                        CustomerListDataGrid.DataSource = dt1;
                        CustomerListDataGrid.Columns[0].Visible = false;

                        CustomerListDataGrid.BringToFront();
                    }
                    return;
                }

                // Get suggestions from your data source
                var suggestions = GetProductSuggestions(searchText);

                if (suggestions.Any())
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Code", typeof(string));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("U-Name", typeof(string));
                    dt.Columns.Add("Qty", typeof(int));
                    //dt.Columns.Add("Sale-P", typeof(string));

                    foreach (var item in suggestions)
                    {
                        dt.Rows.Add(item.ProductId, item.purchasePrice, item.ProductName, TextFormatHelper.FormatMixedText(item.ProductUrduName), item.Qty);
                    }

                    SuggestionGrid.ReadOnly = true;
                    SuggestionGrid.AllowUserToAddRows = false;
                    SuggestionGrid.DataSource = dt;

                    SuggestionGrid.Columns[0].Width = 40;
                    SuggestionGrid.Columns[1].Width = 50;
                    SuggestionGrid.Columns[2].Width = 200;
                    SuggestionGrid.Columns[3].Width = 200;
                   // SuggestionGrid.Columns[5].Width = 75;
                    SuggestionGrid.Visible = true;
                    SuggestionGrid.BringToFront();
                }
                else
                {
                    SuggestionGrid.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SuggestionGrid.Visible = false;
                // Log error
            }
        }

        #region Old GetProductSuggestions use to get the Products for suggestion grid
        //private List<ProductSuggestion> GetProductSuggestions(string searchText)
        //{

        //    //var suggestions = new List<ProductSuggestion>();

        //    //using (var _context = new POSDbContext())
        //    //{
        //    //    var data = _context.Products.AsQueryable();

        //    //    // apply search

        //    //    if (!string.IsNullOrEmpty(searchText))
        //    //    {
        //    //        var searchWords = searchText.ToLower().Split(' ');
        //    //        // apply search

        //    //        foreach (var word in searchWords)
        //    //        {
        //    //            data = data.Where(s => s.ProductEnglishName.Contains(word) || s.Id.ToString().Contains(word) || s.SearchByProductCode.Contains(word));
        //    //            //data = data.Where(s => s.CustomerName.Contains(word) || s.City.Name.Contains(word));
        //    //        }
        //    //    }

        //    //    var result = data.OrderBy(s => s.Id).Select(s => new ProductSuggestion()
        //    //    {
        //    //        ProductId = s.Id,
        //    //        ProductName = s.ProductEnglishName,
        //    //        ProductUrduName = s.ProductUrduName,
        //    //        Qty = s.Qty,
        //    //        purchasePrice = s.PurchasePrice,
        //    //    }).Take(100).ToList();

        //    //    return result;
        //    //}

        //    var suggestions = new List<ProductSuggestion>();

        //    using (var _context = new POSDbContext())
        //    {
        //        var data = _context.Products.AsNoTracking();

        //        if (!string.IsNullOrEmpty(searchText))
        //        {
        //            var searchWords = searchText.ToLower()
        //                                        .Trim()
        //                                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        //            data = (System.Data.Entity.Infrastructure.DbQuery<Models.Product>)data.Where(s =>
        //                searchWords.All(word =>
        //                    s.ProductEnglishName.ToLower().Contains(word) ||
        //                    s.SearchByProductCode.ToLower().Contains(word) ||
        //                    s.Id.ToString().Contains(word)
        //                )
        //            );
        //        }

        //        var result = data
        //            .OrderBy(s => s.Id)
        //             .Take(100)
        //            .Select(s => new ProductSuggestion
        //            {
        //                ProductId = s.Id,
        //                ProductName = s.ProductEnglishName,
        //                ProductUrduName = s.ProductUrduName,
        //                purchasePrice = s.PurchasePrice,
        //                Qty = s.Qty
        //            }).AsNoTracking()
        //            .ToList();

        //        return result;
        //    }
        //}

        #endregion
        private List<ProductSuggestion> GetProductSuggestions(string searchText)
        {
            using (var _context = new POSDbContext())
            {
                // Clean and prepare search words
                var searchWords = string.IsNullOrWhiteSpace(searchText)
                    ? Array.Empty<string>()
                    : searchText.ToLower()
                                .Trim()
                                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // CASE 1: No search text - Fastest path
                if (searchWords.Length == 0)
                {
                    string sql = @"
                SELECT TOP 100 
                    Id AS ProductId,
                    ProductEnglishName AS ProductName,
                    ProductUrduName,
                    Qty,
                    PurchasePrice
                FROM Products WITH (NOLOCK)
                ORDER BY Id";

                    return _context.Database.SqlQuery<ProductSuggestion>(sql).ToList();
                }

                // CASE 2: Single word search - Optimized
                if (searchWords.Length == 1)
                {
                    string sql = @"
                                    SELECT TOP 100 
                                        p.Id AS ProductId,
                                        p.ProductEnglishName AS ProductName,
                                        p.ProductUrduName,
                                        p.Qty,
                                        p.PurchasePrice
                                    FROM Products p WITH (NOLOCK)
                                    WHERE 
                                        p.ProductEnglishName LIKE @pattern 
                                        OR p.SearchByProductCode LIKE @pattern 
                                        OR CAST(p.Id AS VARCHAR(50)) LIKE @pattern
                                    ORDER BY p.Id";

                    var param = new SqlParameter("@pattern", $"%{searchWords[0]}%");
                    return _context.Database.SqlQuery<ProductSuggestion>(sql, param).ToList();
                }

                // CASE 3: Multiple words - Efficient parameterized query
                return ExecuteMultiWordSearch(searchWords, _context);
            }
        }

        private List<ProductSuggestion> ExecuteMultiWordSearch(string[] words, POSDbContext context)
        {
            // Build parameterized query for multiple words
            var parameters = new List<SqlParameter>();
            var whereConditions = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                string paramName = $"@word{i}";
                parameters.Add(new SqlParameter(paramName, $"%{words[i]}%"));

                whereConditions.Add($@"
                                    (p.ProductEnglishName LIKE {paramName}
                                     OR p.SearchByProductCode LIKE {paramName}
                                     OR CAST(p.Id AS VARCHAR(50)) LIKE {paramName})");
            }

            string whereClause = string.Join(" AND ", whereConditions);

            string sql = $@"
                            SELECT TOP 100 
                                p.Id AS ProductId,
                                p.ProductEnglishName AS ProductName,
                                p.ProductUrduName,
                                p.Qty,
                                p.PurchasePrice
                            FROM Products p WITH (NOLOCK)
                            WHERE {whereClause}
                            ORDER BY p.Id";

            return context.Database.SqlQuery<ProductSuggestion>(sql, parameters.ToArray()).ToList();
        }

        private void P_StockQtyTxt_TextChange(object sender, EventArgs e)
        {
            string currentText = P_StockQtyTxt.Text;
            string validText = RegexValidator.ValidateCommonPattern(currentText, ValidationPattern.NumbersOnly, "quantityField");
            if (currentText != validText)
            {
                P_StockQtyTxt.Text = validText;
                P_StockQtyTxt.SelectionStart = validText.Length;
            }
        }

        private void ProductSalePrice_TextChange(object sender, EventArgs e)
        {
            string currentText = ProductSalePrice.Text;
            string validText = RegexValidator.ValidateCommonPattern(currentText, ValidationPattern.NumbersWithDecimal, "saleAmontField");
            if (currentText != validText)
            {
                ProductSalePrice.Text = validText;
                ProductSalePrice.SelectionStart = validText.Length;
            }
        }

        private void CartProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if Delete column clicked
            if (e.RowIndex >= 0 && CartProductList.Columns[e.ColumnIndex].Name == "Delete")
            {
                // Ask for confirmation (optional)
                var confirm = MessageBox.Show("Do you want to delete this product?",
                                              "Confirm Delete",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

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

        private void CustomerNameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (CustomerListDataGrid.Visible == false)
                {
                    ShowSuggestions(CustomerNameTxt.Text, isForCustomer: true);
                }
                else
                {
                    CustomerListDataGrid.Visible = false;
                }
                e.Handled = true;
            }
        }

        private void ResetCustomerBtn_Click(object sender, EventArgs e)
        {
            CustomerNameTxt.Text = string.Empty;
            customerId = string.Empty;
            CustomerIdLbl.Text = string.Empty;
            this.ResetCustomerBtn.Enabled = true;
            this.ResetCustomerBtn.Visible = false;
            ClearCustomerPreviousTransactionGroup();

        }

        private void ClearCustomerPreviousTransactionGroup()
        {
            previousBillAmountLbl.Text = "0";
            PreviousReceivedAmountLbl.Text = "0";

            PreviousOrderSummaryLbl.Visible = false;
        }

        private void ReceivedAmountTxt_TextChange(object sender, EventArgs e) => CalculateReturnAmount();

        private void CalculateReturnAmount()
        {
            if (!string.IsNullOrEmpty(ReceivedAmountTxt.Text))
            {
                string currentText = ReceivedAmountTxt.Text;
                string validText = RegexValidator.ValidateCommonPattern(currentText, ValidationPattern.NumbersOnly, "receivedAmountField");
                if (currentText != validText)
                {
                    ReceivedAmountTxt.Text = validText;
                    ReceivedAmountTxt.SelectionStart = validText.Length;
                }
            }

            if (!string.IsNullOrEmpty(TotalAmountLbl.Text) && TotalAmountLbl.Text != "0")
            {
                // Calculate remaining amount
                decimal totalAmount = Convert.ToDecimal(TotalAmountLbl.Text); // Your total amount

                if (string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
                {
                    lblRemainingAmount.Text = "Remaining: Rs. 0";
                    return;
                }

                if (decimal.TryParse(ReceivedAmountTxt.Text, out decimal receivedAmount))
                {
                    decimal remainingAmount = totalAmount - receivedAmount;
                    lblRemainingAmount.Text = remainingAmount >= 0
                        ? $"Remaining Amt:  Rs. {remainingAmount}"
                        : $"Return Amt:  Rs. {Math.Abs(remainingAmount)}";
                    lblRemainingAmount.ForeColor = remainingAmount >= 0 ? Color.Red : Color.Blue;
                }
            }
            else
            {
                lblRemainingAmount.Text = "Remaining: Rs. 0";
            }
        }

        private void ClearCartBtn_Click(object sender, EventArgs e)
        {
            ClearCartFunction();
            ClearInputs();
            ClearCustomerPreviousTransactionGroup();
            // Optional: Show confirmation message
            ProductEngNameTxt.Focus();
            MessageBox.Show("Cart cleared successfully!", "Clear Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearCartFunction()
        {
            PId = string.Empty;
            customerId = string.Empty;
            CustomerNameTxt.Text = string.Empty;
            CartProductList.Rows.Clear();
            ResetCustomerBtn.Visible = false;

            customerId = string.Empty;
            CustomerIdLbl.Text = string.Empty;
            CustomerNameTxt.Text = string.Empty;

            PreviousOrderIdLbl.Text = string.Empty;
            string invRef = TextFormatHelper.GetPrefix(Properties.Settings.Default.UserName);
            InvoiceNoLbl.Text = invRef + DateTime.Now.ToString("ddMMyy-HHmmss");
            isTempSaved = false;

            // Also update the totals to zero
            TotalItemLbl.Text = "0";
            TotalAmountLbl.Text = "0";
            ReceivedAmountTxt.Clear();
        }

        private void TopBarSearchProductTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevents the default beep sound
                // Create a new instance of your Form
                Form ProductForm = new Form();
                ProductForm.Text = "Product Form";
                ProductForm.StartPosition = FormStartPosition.CenterScreen;

                // Create an instance of your User Control
                var FormCtrl = new POS_Shop.Views.Controllers.Product.ProductListControl();
                FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

                // Add the User Control to the new Form's controls collection
                ProductForm.Controls.Add(FormCtrl);
                ProductForm.Width = 1050; ProductForm.Height = 625;
                // Show the new form
                ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
            }
        }

        private void SearchInvoiceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PreviousOrderIdLbl.Text = string.Empty;
            // Create a new instance of your Form
            Form OrderListForm = new Form();
            OrderListForm.Text = "Order List";
            OrderListForm.StartPosition = FormStartPosition.CenterScreen;

            // Get screen area
            Screen currentScreen = Screen.PrimaryScreen;
            int screenArea = currentScreen.Bounds.Width * currentScreen.Bounds.Height;

            // If screen has less than 1.5M pixels (typical for smaller/lower-res screens)
            if (screenArea < 1327104)
            {
                OrderListForm.WindowState = FormWindowState.Maximized;
            }
            else
            {
                OrderListForm.Width = 1390;
                OrderListForm.Height = 730;
            }

            // Create an instance of your User Control
            var FormCtrl = new Views.Controllers.Order.OrdersControlUI();
            FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

            // Add the User Control to the new Form's controls collection
            OrderListForm.Controls.Add(FormCtrl);
            //  OrderListForm.Width = 1390; OrderListForm.Height = 730;

            // Show the new form
            OrderListForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
            if (FormCtrl.isRecordSelected == true)
            {
                // PREVENT DUPLICATE LOADING - Check if cart has items

                var result = MessageBox.Show("Clear current cart before loading order?", "Confirm",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                ClearCustomerPreviousTransactionGroup();
                InvoiceNoLbl.Text = FormCtrl.InvoiceNoLbl.Text;
                PreviousOrderIdLbl.Text = FormCtrl.OrderIDLbl.Text;
                TotalAmountLbl.Text = FormCtrl.TotalBill.ToString();
                ReceivedAmountTxt.Text = FormCtrl.ReceiveAmount.ToString();
                if (FormCtrl.CustomerId != 0)
                {
                    CustomerIdLbl.Text = FormCtrl.CustomerId.ToString();
                    CustomerNameTxt.Text = FormCtrl.CustomerName;
                    this.ResetCustomerBtn.Visible = true;
                    this.ResetCustomerBtn.Enabled = true;
                    CustomerListDataGrid.Visible = false;
                }
                else
                {
                    ResetCustomerBtn.Visible = false;
                }
            }
        }

        private async void PreviousOrderIdLbl_TextChanged(object sender, EventArgs e)
        {
            if ((PreviousOrderIdLbl.Text != "OrderID" && InvoiceNoLbl.Text != "InvoiceNo") &&
                (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text) && !string.IsNullOrEmpty(InvoiceNoLbl.Text)))
            {
                //// PREVENT DUPLICATE LOADING - Check if cart has items
                //if (CartProductList.Rows.Count > 0)
                //{
                //    var result = MessageBox.Show("Clear current cart before loading order?", "Confirm",
                //                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (result != DialogResult.Yes)
                //    {
                //        return;
                //    }
                //}

                using (var context = new POSDbContext())
                {
                    var orderRepo = new OrderRepository(context);
                    var result = await orderRepo.GetOrderByIdAsync(Convert.ToInt32(PreviousOrderIdLbl.Text), InvoiceNoLbl.Text);
                    if (result != null)
                    {
                        // CLEAR EXISTING ITEMS FIRST to prevent duplicates
                        CartProductList.Rows.Clear();

                        CustomerIdLbl.Text = result.CustomerId.HasValue ? result.CustomerId.Value.ToString() : string.Empty;
                        CustomerNameTxt.Text = string.IsNullOrEmpty(CustomerIdLbl.Text) ? "" : result.CustomerName;
                        TotalAmountLbl.Text = result.TotalBill.ToString();
                        if (result.paymentType == "Cash")
                        {
                            CashRadioBtn.Checked = true;
                            BankTransferRaadioBtn.Checked = false;
                        }
                        else
                        {
                            CashRadioBtn.Checked = false;
                            BankTransferRaadioBtn.Checked = true;
                        }

                        // Safely add order details
                        foreach (var order in result.OrderDetailsList)
                        {
                            string productId = order.ProductId.ToString() ?? "0";
                            string finalName = !string.IsNullOrEmpty(order.ProductDetail) ?
                                $"{order.ProductName} {order.ProductDetail}" : order.ProductName;

                            string productType = order.QuantityType;
                            decimal salePrice = Math.Round(decimal.Parse(order.Price.ToString()), 1);
                            int qty = order.Quantity;
                            decimal amount = salePrice * qty;

                            CartProductList.Rows.Add(null, amount, salePrice, finalName,
                                                   productType, qty, productId, order.ProductDetail);
                        }

                        CalculateTotals();
                    }
                }
            }
        }

        // Usage method
        public async Task GeneratePdfInvoice()
        {
            var confirm = MessageBox.Show("Do you want to Generate PDF?",
                                          "Confirm Action",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (var saveFileDialog = new SaveFileDialog())
                    {
                        string pdfName = !string.IsNullOrEmpty(CustomerNameTxt.Text)
                            ? $"{CustomerNameTxt.Text}-{InvoiceNoLbl.Text}"
                            : InvoiceNoLbl.Text;

                        saveFileDialog.FileName = $"Invoice_{pdfName}.pdf";
                        saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                        saveFileDialog.DefaultExt = "pdf";
                        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            PrintToPdfGenerator generator = new PrintToPdfGenerator();
                            generator.GenerateInvoice(CartProductList,
                                saveFileDialog.FileName,
                                customerName: CustomerNameTxt.Text,
                                invoiceNo: InvoiceNoLbl.Text,
                                totalAmount: TotalAmountLbl.Text,
                                receivedAmount: ReceivedAmountTxt.Text);

                            ToastHelper.ShowSuccess($"Invoice saved to:\n{saveFileDialog.FileName}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void SaveAndPrintOrderBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
            {
                bool IsDone = false;
                if (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text) && PreviousOrderIdLbl.Text != "Prev Order Id")
                    IsDone = await SaveOrder(true);  //await UpdateOrderSaved();
                else
                    IsDone = await SaveOrder(false);  // await NewOrderSaved();

                if (IsDone)
                {
                    //// First clear any previous handlers
                    //OrderPrintDocument.PrintPage -= OrderPrintDocument_PrintPage;
                    //OrderPrintDocument.PrintPage -= OrderPrintDocument_PrintPage_English;

                    //if (EnglishInvoiceChk.Checked)
                    //    OrderPrintDocument.PrintPage += OrderPrintDocument_PrintPage_English;
                    //else
                    //    OrderPrintDocument.PrintPage += OrderPrintDocument_PrintPage;


                    OrderPrintPreviewDialog.Document = OrderPrintDocument;
                    OrderPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("FullInvoice", 280, 32767);
                    OrderPrintDocument.Print();

                    if (isTempSaved)
                    {
                        string sql = "DELETE FROM TempOrders WHERE InvoiceNumber = @InvoiceNumber";
                        string sql1 = "DELETE FROM TempOrderDetails WHERE TempInvoiceNumber = @InvoiceNumber";

                        using (var context = new POSDbContext())
                        {
                            var parameters1 = new[]
                            {
                                new System.Data.SqlClient.SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text)
                            };

                            context.Database.ExecuteSqlCommand(sql1, parameters1);

                            var parameters = new[]
                            {
                                new System.Data.SqlClient.SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text),
                            };
                            context.Database.ExecuteSqlCommand(sql, parameters);
                        }
                    }

                    ResetUIAfterSave();
                    SendKeys.SendWait("^{F11}");
                    MessageBox.Show("Order Created Successfully!", "Order Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Order Creation Failed!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GenerateInvoicePdf()
        {
            var confirm = MessageBox.Show("Do you want to Generate PDF?",
                                          "Confirm Action",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save Invoice as PDF";
                    saveFileDialog.FileName = $"Invoice_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            MessageBox.Show("Invoice saved as PDF successfully!", "Success",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error saving PDF: {ex.Message}", "Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private async Task<bool> SaveOrder(bool isUpdate = false)
        {
            using (var context = new POSDbContext())
            using (var dbTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var orderRepository = new OrderRepository(context);

                    // Get order data
                    var orderData = await GetOrderData();

                    if (isUpdate)
                    {
                        orderData.Id = int.Parse(PreviousOrderIdLbl.Text);
                    }

                    // Save order
                    var orderId = isUpdate
                        ? await UpdateOrder(orderRepository, orderData, context)
                        : await orderRepository.AddOrder(orderData);

                    // Save order details
                    await SaveOrderDetails(context, orderId);

                    dbTransaction.Commit();
                    return true;
                }
                catch (DbException ex)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        private async Task<Order> GetOrderData()
        {
            int? customerId = null;
            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) && !string.IsNullOrEmpty(CustomerIdLbl.Text))
            {
                int.TryParse(CustomerIdLbl.Text, out int parsedId);
                customerId = parsedId;
            }

            float.TryParse(TotalAmountLbl.Text, out float totalBill);

            float receiveAmount = totalBill;
            if (!string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
            {
                float.TryParse(ReceivedAmountTxt.Text, out receiveAmount);
            }

            return new Order
            {
                TotalBill = totalBill,
                ReceiveAmount = receiveAmount,
                CreatedDate = DateTime.Now,
                InvoiceNumber = !string.IsNullOrEmpty(InvoiceNoLbl.Text) ? InvoiceNoLbl.Text : DateTime.Now.ToString("MMddyyy-HHmmss"),
                paymentType = CashRadioBtn.Checked ? "Cash" : "Bank",
                customerId = customerId
            };
        }

        private async Task<TempOrder> GetTempOrderData()
        {
            int? customerId = null;
            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) && !string.IsNullOrEmpty(CustomerIdLbl.Text))
            {
                int.TryParse(CustomerIdLbl.Text, out int parsedId);
                customerId = parsedId;
            }

            float.TryParse(TotalAmountLbl.Text, out float totalBill);
            float receiveAmount = totalBill;

            return new TempOrder
            {
                TotalBill = totalBill,
                CreatedDate = DateTime.Now,
                InvoiceNumber = !string.IsNullOrEmpty(InvoiceNoLbl.Text) ? InvoiceNoLbl.Text : DateTime.Now.ToString("MMddyyy-HHmmss"),
                customerId = customerId,
                CustomerName = CustomerNameTxt.Text
            };
        }

        private async Task<int> UpdateOrder(OrderRepository orderRepository, Order order, POSDbContext context)
        {
            var orderId = await orderRepository.AddOrder(order);

            // Checking User has Enabled the Stock Qty Update Feature
            var config = ConfigurationManager.Configuration.Features.EnableUpdateQty;
            if (config)
                UpdateStockQuantity(orderId);

            // Remove existing order details
            var existingDetails = context.OrderDetails.Where(s => s.OrderId == orderId).ToList();
            context.OrderDetails.RemoveRange(existingDetails);
            context.SaveChanges();
            return orderId;
        }

        private void UpdateStockQuantity(int orderId)
        {
            using (var context = new POSDbContext())
            {
                var existingDetails = context.OrderDetails.Where(s => s.OrderId == orderId).ToList();
                foreach (var item in existingDetails)
                {
                    if (item.ProductId.HasValue)
                    {
                        var product = context.Products.Find(item.ProductId);
                        if (product != null)
                        {
                            product.Qty += item.Quantity;
                            context.Entry(product).State = EntityState.Modified;
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        private async Task SaveOrderDetails(POSDbContext context, int orderId)
        {
            var orderDetailList = new List<OrderDetail>();

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells["ProductId"].Value == null) continue;

                var productIdValue = row.Cells["ProductId"].Value?.ToString();


                var q = int.Parse(row.Cells["Qty"].Value?.ToString());

                // Checking User has Enabled the Stock Qty Update Feature
                var config = ConfigurationManager.Configuration.Features.EnableUpdateQty;
                if (config)
                {
                    if (!string.IsNullOrEmpty(productIdValue))
                    {
                        var productCheck = context.Products.Find(int.Parse(productIdValue));
                        if (productCheck.Qty < 0 || productCheck.Qty < q)
                        {
                            MessageBox.Show($"Insufficient stock for product {productCheck.ProductEnglishName}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                            throw new Exception($"Insufficient stock for product ID {productIdValue}");
                        }

                    }
                }

                var odrDetail = new OrderDetail
                {
                    ProductId = string.IsNullOrEmpty(productIdValue) ? (int?)null : int.Parse(productIdValue),
                    OtherProductName = string.IsNullOrEmpty(productIdValue) ? row.Cells["Urdu Name"].Value?.ToString() : null,
                    Quantity = int.Parse(row.Cells["Qty"].Value?.ToString()),
                    QuantityType = row.Cells["ProductType"].Value?.ToString(),
                    Price = float.Parse(row.Cells["SalePrice"].Value?.ToString()),
                    CreatedDate = DateTime.Now,
                    OrderId = orderId,
                    ProductDetail = row.Cells["ProductDetail"].Value?.ToString()
                };
                orderDetailList.Add(odrDetail);

                // Checking User has Enabled the Stock Qty Update Feature

                if (config)
                {
                    if (!string.IsNullOrEmpty(productIdValue))
                    {
                        var pid = int.Parse(productIdValue);
                        var product = context.Products.Find(pid);
                        product.Qty -= odrDetail.Quantity;
                        context.Entry(product).State = EntityState.Modified;
                    }
                }
            }

            context.OrderDetails.AddRange(orderDetailList);
            await context.SaveChangesAsync();
        }

        private async Task SaveTempOrderDetails(POSDbContext context, string invoiceNo)
        {
            //First we will check if the TempOrderDetail has already record or not? if yes then we will delete all first.. 
            var tempOrderDetailList = context.TempOrderDetails.Where(s => s.TempInvoiceNumber.Equals(invoiceNo)).ToList();
            if (tempOrderDetailList.Count > 0)
            {
                context.TempOrderDetails.RemoveRange(tempOrderDetailList);
                context.SaveChanges();
            }

            var orderDetailList = new List<TempOrderDetail>();

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells["ProductId"].Value == null) continue;

                var productIdValue = row.Cells["ProductId"].Value?.ToString();
                var odrDetail = new TempOrderDetail
                {
                    ProductId = string.IsNullOrEmpty(productIdValue) ? (int?)null : int.Parse(productIdValue),
                    ProductName = row.Cells["Urdu Name"].Value?.ToString(),
                    Quantity = int.Parse(row.Cells["Qty"].Value?.ToString()),
                    QuantityType = row.Cells["ProductType"].Value?.ToString(),
                    Price = float.Parse(row.Cells["SalePrice"].Value?.ToString()),
                    TempInvoiceNumber = invoiceNo,
                    ProductDetail = row.Cells["ProductDetail"].Value?.ToString()
                };
                orderDetailList.Add(odrDetail);
            }

            context.TempOrderDetails.AddRange(orderDetailList);
            await context.SaveChangesAsync();
        }

        private void TruncateOrder_OrderDetailBtn_Click(object sender, EventArgs e)
        {
            // Ask for confirmation (optional)
            var confirm = MessageBox.Show("Do you want to delete this Orders?",
                                          "Confirm Delete",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                using (var ctx = new POSDbContext())
                {
                    ctx.Database.ExecuteSqlCommand("ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]");
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[OrderDetails]");
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[Orders]");
                    ctx.Database.ExecuteSqlCommand(@"ALTER TABLE [dbo].[OrderDetails] 
                                     ADD CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId] 
                                     FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id])");

                    MessageBox.Show("Records has been Delete",
                                             "Information",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
                }

                InvoicePageTabControl.SelectedTab = BilPad;
            }
        }

        private void PrintPreviewBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
            {
                //// Simulate Ctrl + F11 key press, to shift the control automatically because we are using Auto sharing printer usb
                SendKeys.SendWait("^{F11}");
                OrderPrintPreviewDialog.Document = OrderPrintDocument;
                OrderPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("FullInvoice", 280, 32767);
                OrderPrintPreviewDialog.PrintPreviewControl.Zoom = 1.0;
                OrderPrintPreviewDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string FixCommonPatterns(string input)
        {
            // Common battery patterns
            var patterns = new Dictionary<string, string>
            {
                { @"(\d+)V-(\d+)AH", "{$1V-$2AH}" },  // 12V-7AH pattern
                { @"(\d+)V", "{$1V}" },               // Simple voltage
                { @"(\d+)AH", "{$1AH}" }              // Amp-hour
            };

            string result = input;

            // First, protect common battery patterns
            foreach (var pattern in patterns)
            {
                var regex = new System.Text.RegularExpressions.Regex(pattern.Key);
                result = regex.Replace(result, pattern.Value);
            }

            // Now apply directional marks
            const char LRM = '\u200E'; // Left-to-Right
            const char RLM = '\u200F'; // Right-to-Left

            // Start with RTL context
            var sb = new System.Text.StringBuilder().Append(RLM);

            bool inProtected = false;
            foreach (char c in result)
            {
                if (c == '{')
                {
                    sb.Append(LRM); // Start LTR for protected content
                    inProtected = true;
                }
                else if (c == '}')
                {
                    sb.Append(RLM); // Resume RTL
                    inProtected = false;
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Trim();
        }

        // This is default
        private void OrderPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (EnglishInvoiceChk.Checked)
                InvoicePrintHelper.PrintEnglishInvoice(
                      e: e,
                      cartProductList: CartProductList,
                      customerName: CustomerNameTxt.Text,
                      invoiceNo: InvoiceNoLbl.Text,
                      totalAmount: TotalAmountLbl.Text,
                      isCashPayment: CashRadioBtn.Checked,
                      receivedAmount: ReceivedAmountTxt.Text
                  //hideShopName: InvoiceShopName.Checked
                  );
            else
                InvoicePrintHelper.PrintInvoice(
                      e: e,
                      cartProductList: CartProductList,
                      customerName: CustomerNameTxt.Text,
                      invoiceNo: InvoiceNoLbl.Text,
                      totalAmount: TotalAmountLbl.Text,
                      isCashPayment: CashRadioBtn.Checked,
                      receivedAmount: ReceivedAmountTxt.Text
                  // hideShopName: InvoiceShopName.Checked
                  );
        }



        private void productTypeDropdown_Enter(object sender, EventArgs e)
        {
            productTypeDropdown.BorderColor = Color.BlueViolet;
        }

        private void productTypeDropdown_Leave(object sender, EventArgs e)
        {
            productTypeDropdown.BorderColor = Color.Silver;
        }

        private void InvoiceShopName_CheckedChanged(object sender, EventArgs e)
        {
            InvoiceShopName.Text = InvoiceShopName.Checked ? "Hide Shop Name is Invoice" : "Show Shop Name is Invoice";
        }

        private void BillPadForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control) // Ctrl + S to Save and Print
            {
                SaveAndPrintOrderBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.T && e.Control) // Ctrl + T to Save and Print (Thermal)
            {
                SaveBillBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.P && e.Control) // Ctrl + P to Print Preview
            {
                PrintPreviewBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.N && e.Control) // Ctrl + N to New Invoice
            {
                ClearCartBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape) // Esc to Clear Cart
            {
                e.Handled = true;
                ProductEngNameTxt.Focus();
                ProductEngNameTxt.SelectAll(); // Optional: select all text

                SuggestionGrid.Visible = false;
            }
            else if (e.KeyCode == Keys.D1 && e.Control) // 1 to Focus on Product TextBox
            {
                ProductEngNameTxt.Focus();
                ProductEngNameTxt.SelectAll();
            }
            else if (e.KeyCode == Keys.D2 && e.Control) // 2 to Focus on Product TextBox
            {
                CustomerNameTxt.Focus();  
                CustomerNameTxt.SelectAll();
            }
            else if (e.KeyCode == Keys.R && e.Control)
            {
                GenerateInvoicePdfBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.Q && e.Control)
            {
                e.Handled = true;
                GotoFirstRow();
            }
            else if (e.KeyCode == Keys.D && e.Control)
            {
                e.Handled = true;
                SaveOrderWithoutPrintBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.E && e.Control)
            {
                e.Handled = true;
                ExportBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.R && e.Alt)
            {
                e.Handled = true;
                ReceivedAmountTxt.Focus();
            }
        }

        private void GotoFirstRow()
        {
            if (CartProductList.Rows.Count > 0)
            {
                CartProductList.ClearSelection();
                CartProductList.Rows[0].Selected = true;
                CartProductList.CurrentCell = CartProductList.Rows[0].Cells[1];
                CartProductList.Focus();
            }
        }

        private async void SuggestionGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                // If we're on the first row, move focus back to TextBox
                if (SuggestionGrid.CurrentRow != null &&
                    SuggestionGrid.CurrentRow.Index == 0)
                {
                    ProductEngNameTxt.Focus();
                    ProductEngNameTxt.SelectAll(); // Optional: select all text

                    SuggestionGrid.Visible = false;
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ProductEngNameTxt.Focus();
                ProductEngNameTxt.SelectAll(); // Optional: select all text

                SuggestionGrid.Visible = false;
            }
            else if (e.KeyCode == Keys.Enter && !e.Handled)
            {
                e.Handled = true;
                e.SuppressKeyPress = true; // This prevents the beep sound and default behavior

                if (SuggestionGrid.CurrentRow != null && SuggestionGrid.CurrentRow.Index >= 0)
                {
                    int pId = Convert.ToInt32(SuggestionGrid.CurrentRow.Cells[0].Value);
                    ProductEngNameTxt.Text = (string)SuggestionGrid.CurrentRow.Cells[2].Value;
                    prod_U_Name = (string)SuggestionGrid.CurrentRow.Cells[3].Value;

                    DataGridViewRow foundRow = null;

                    foreach (DataGridViewRow row in SuggestionGrid.Rows)
                    {
                        if (row.Cells[0].Value != null &&
                            Convert.ToInt32(row.Cells[0].Value) == pId)
                        {
                            foundRow = row;
                            break;
                        }
                    }

                    if (foundRow != null)
                    {
                        pId = Convert.ToInt32(foundRow.Cells[0].Value);
                        ProductEngNameTxt.Text = (string)foundRow.Cells[2].Value;
                        prod_U_Name = (string)foundRow.Cells[3].Value;
                        Prod_Qty.Text = SuggestionGrid.CurrentRow.Cells[4].Value.ToString();
                        PId = pId.ToString();
                        P_StockQtyTxt.Text = "1";
                        SuggestionGrid.Visible = false;
                        ProductDetailTxt.Focus();
                    }
                    ProductDetailTxt.Focus();
                   await ShowProductPrices(pId);
                    if (!string.IsNullOrEmpty(CustomerIdLbl.Text))
                    {
                        SetProductPreviousSalePrice(int.Parse(CustomerIdLbl.Text), productId: pId);
                    }
                    //SetProductPreviousSalePrice(customerId: string.IsNullOrEmpty(CustomerIdLbl.Text) ? 0 : int.Parse(CustomerIdLbl.Text),
                    //                      productId: pId);
                }
            }
        }

        private async void SuggestionGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (SuggestionGrid.Rows.Count > 0)
            {
                int pId = Convert.ToInt32(SuggestionGrid.CurrentRow.Cells[0].Value);

                ProductEngNameTxt.Text = (string)SuggestionGrid.CurrentRow.Cells[2].Value;

                prod_U_Name = (string)SuggestionGrid.CurrentRow.Cells[3].Value;

                Prod_Qty.Text = SuggestionGrid.CurrentRow.Cells[4].Value.ToString();
                PId = pId.ToString();
                P_StockQtyTxt.Text = "1";
                SuggestionGrid.Visible = false;
                ProductDetailTxt.Focus();

               await ShowProductPrices(pId);

                if (!string.IsNullOrEmpty(CustomerIdLbl.Text))
                {
                    SetProductPreviousSalePrice(int.Parse(CustomerIdLbl.Text), productId: pId);
                }
            }
        }

        private void SetProductPreviousSalePrice(int customerId, int productId)
        {
            using (var context = new POSDbContext())
            {
                IProductRepository productRepo = new ProductRepository(context);
                var previousPricesTask = productRepo.ProductPreviousPriceInRecentOrderByCustomerId(customerId, productId);
                ProductOrderHistoryDataGrid.DataSource = null;
                ProductOrderHistoryDataGrid.DataSource = previousPricesTask.ToList();
                ProductOrderHistoryDataGrid.RowHeadersVisible = false;
                //ProductOrderHistoryDataGrid.CurrentCell = null;
                ProductOrderHistoryDataGrid.ClearSelection();
            }
        }


        private async Task ShowProductPrices(int productId)
        {
            using(var context =new  POSDbContext())
            {
                var data = await context.ProductPrices.Where(s => s.ProductId == productId).Select(s => new ProdDTO()
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


                if (data.Count() > 0)
                {
                    var d = data.FirstOrDefault();
                    productTypeDropdown.SelectedValue = d.Type;
                    ProductSalePrice.Text = ((int)d.Price).ToString();
                    ProductAmount.Text = Convert.ToString(Convert.ToInt32(P_StockQtyTxt.Text) * Convert.ToInt32(ProductSalePrice.Text));
                }
            }
        }

        private async void SaveBillBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows?.Count == 0)
            {
                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to store Temporary Record?", "Save Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            string customerName = await GetCustomerNameAsync();
            if (customerName == null) return; // User cancelled the dialog

            await SaveOrderTransactionAsync(customerName);
        }

        private async Task<string> GetCustomerNameAsync()
        {
            if (!string.IsNullOrEmpty(CustomerNameTxt.Text) || !string.IsNullOrEmpty(CustomerIdLbl.Text))
                return CustomerNameTxt.Text;

            using (var dialog = new InputDialog("Enter customer name:", "Customer Info"))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return null;

                string customerName = dialog.InputValue;
                CustomerNameTxt.Text = customerName;
                customerId = string.Empty;
                CustomerIdLbl.Text = string.Empty;
                return customerName;
            }
        }

        private async Task SaveOrderTransactionAsync(string customerName)
        {
            using (var context = new POSDbContext())
            using (var dbTransaction = context.Database.BeginTransaction())
                try
                {
                    var orderRepository = new OrderRepository(context);
                    var data = await GetTempOrderData();
                    if (context.Orders.Any(o => o.InvoiceNumber == data.InvoiceNumber))
                    {
                        var orderDetail = await context.OrderDetails.Where(od => od.Order.InvoiceNumber == data.InvoiceNumber).ToListAsync();

                        context.OrderDetails.RemoveRange(orderDetail);
                        var existingOrder = await context.Orders.FirstOrDefaultAsync(o => o.InvoiceNumber == data.InvoiceNumber);
                        if (existingOrder != null)
                        {
                            context.Orders.Remove(existingOrder);
                        }
                        await context.SaveChangesAsync();
                    }

                    var invoiceNo = await orderRepository.AddTempOrder(data);
                    await SaveTempOrderDetails(context, invoiceNo);

                    dbTransaction.Commit();
                    ResetUIAfterSave();
                    MessageBox.Show("Order Saved Successfully!", "Order Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (DbException ex)
                {
                    dbTransaction.Rollback();
                    MessageBox.Show("Order Creation Failed!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void TemOrderBtn_Click(object sender, EventArgs e)
        {
            // Create a new instance of your Form
            Form ProductForm = new Form();
            ProductForm.Text = "Temp Order Form";
            ProductForm.StartPosition = FormStartPosition.CenterScreen;

            // Create an instance of your User Control
            var FormCtrl = new TempOrderControl();
            FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

            // Add the User Control to the new Form's controls collection
            ProductForm.Controls.Add(FormCtrl);
            ProductForm.Width = 1050; ProductForm.Height = 525;
            // Show the new form
            ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog

            if (FormCtrl.isRecordSelected == true)
            {
                if (!string.IsNullOrEmpty(FormCtrl.InvoiceNoLbl.Text)) InvoiceNoLbl.Text = FormCtrl.InvoiceNoLbl.Text;


                if (InvoiceNoLbl.Text != "InvoiceNo" && !string.IsNullOrEmpty(InvoiceNoLbl.Text))
                {
                    // CHECK FOR EXISTING ITEMS BEFORE LOADING
                    if (CartProductList.Rows.Count > 0)
                    {
                        var result = MessageBox.Show("Loading this order will clear current cart. Continue?",
                                                   "Confirm", MessageBoxButtons.YesNo);
                        if (result != DialogResult.Yes) return;
                    }


                    if (FormCtrl.CustomerId != 0)
                    {
                        CustomerIdLbl.Text = FormCtrl.CustomerId.ToString();
                        CustomerNameTxt.Text = FormCtrl.CustomerName;
                        this.ResetCustomerBtn.Visible = true;
                        this.ResetCustomerBtn.Enabled = true;

                        ProductEngNameTxt.Focus();
                        ProductEngNameTxt.SelectAll();

                        // Hide the DataGridView after selection
                        CustomerListDataGrid.Visible = false;
                        using (var context = new POSDbContext())
                        {
                            IOrderRepository orderRepo = new OrderRepository(context);
                            var customerPreviousDue = orderRepo.GetLatestOrderAmountSummaryByCustomerId(FormCtrl.CustomerId);
                            UpdatePreviousOrderSummary(customerPreviousDue);
                        }
                    }
                    else
                    {

                        ClearCustomerPreviousTransactionGroup();
                        CustomerIdLbl.Text = string.Empty;
                        CustomerNameTxt.Text = string.Empty;
                        this.ResetCustomerBtn.Visible = false;
                        this.ResetCustomerBtn.Enabled = false;
                    }

                    using (var context = new POSDbContext())
                    {
                        var orderRepo = new OrderRepository(context);
                        var result = orderRepo.GetTempOrderDetailByInvoice(InvoiceNoLbl.Text);

                        if (result != null && result.Count > 0)
                        {
                            isTempSaved = true;
                            // CLEAR BEFORE ADDING to prevent duplicates
                            CartProductList.Rows.Clear();

                            foreach (var order in result)
                            {
                                string productId = order.ProductId.ToString() ?? "0";
                                string finalName = !string.IsNullOrEmpty(order.ProductDetail) ?
                                    $"{order.ProductName} {order.ProductDetail}" : order.ProductName;
                                string productType = order.QuantityType;
                                decimal salePrice = Math.Round(decimal.Parse(order.Price.ToString()), 1);
                                int qty = order.Quantity;
                                decimal amount = salePrice * qty;

                                CartProductList.Rows.Add(null, amount, salePrice, finalName,
                                                       productType, qty, productId, order.ProductDetail);
                            }
                            CalculateTotals();
                        }
                    }
                }
            }
        }

        private void ClearProductTblBtn_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Do you want to delete this product?",
                                          "Confirm Delete",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                using (var ctx = new POSDbContext())
                {
                    ctx.Database.ExecuteSqlCommand("ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]");
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[OrderDetails]");
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[Orders]");
                    ctx.Database.ExecuteSqlCommand(@"ALTER TABLE [dbo].[OrderDetails] 
                                     ADD CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId] 
                                     FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id])");

                    // Now safely delete all products
                    ctx.Database.ExecuteSqlCommand("DELETE FROM Products");

                    // Optional: Reset identity seed if needed
                    ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Products', RESEED, 0)");
                    MessageBox.Show("Records has been Delete",
                                             "Information",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
                }

                InvoicePageTabControl.SelectedTab = BilPad;
            }
        }

        private void GenerateInvoicePdfBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
            {
                // This is for PDF Invoice
                GeneratePdfInvoice();
            }
            else
            {
                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearTempOrderTabls_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Do you want to Clear the Temporary orders?",
                                         "Confirm Delete",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                using (var ctx = new POSDbContext())
                {
                    // Now safely delete all products
                    ctx.Database.ExecuteSqlCommand("DELETE FROM TempOrderDetails");

                    // Optional: Reset identity seed if needed
                    ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('TempOrderDetails', RESEED, 0)");
                    ctx.Database.ExecuteSqlCommand("DELETE FROM TempOrders");

                    MessageBox.Show("Records has been Delete",
                                             "Information",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
                }
                InvoicePageTabControl.SelectedTab = BilPad;
            }
        }

        private void ProductSalePrice_Enter(object sender, EventArgs e)
        {
            ProductSalePrice.SelectAll();
        }

        private void CartProductList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && CartProductList.CurrentRow != null)
            {
                // Confirm deletion (optional)
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                    "Confirm Delete",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    CartProductList.Rows.RemoveAt(CartProductList.CurrentRow.Index);
                    CalculateTotals();
                    CalculateReturnAmount();
                    ProductEngNameTxt.Focus();
                    ProductEngNameTxt.SelectAll();
                }

                e.Handled = true; // Mark event as handled
            }
        }

        private void CustomerNameTxt_TextChange(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(CustomerNameTxt.Text) || CustomerNameTxt.Text.Length < 2))
            {
                CustomerListDataGrid.Visible = false;
                return;
            }

            ShowSuggestions(CustomerNameTxt.Text, isForCustomer: true);
            CustomerListDataGrid.Visible = true;
        }

        private void CustomerNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && CustomerListDataGrid.Visible)
            {
                if (CustomerListDataGrid.Rows.Count > 0)
                {
                    CustomerListDataGrid.Focus();
                    CustomerListDataGrid.Rows[0].Selected = true;
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Escape && CustomerListDataGrid.Visible)
            {
                CustomerListDataGrid.Visible = false;
                ProductEngNameTxt.Focus();
                e.Handled = true;
            }
        }

        private void CustomerListDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (CustomerListDataGrid.CurrentRow != null &&
                    CustomerListDataGrid.CurrentRow.Index == 0)
                {
                    CustomerNameTxt.Focus();
                    CustomerNameTxt.SelectAll();
                    CustomerListDataGrid.Visible = false;
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Check if the DataGridView is in edit mode
                if (CustomerListDataGrid.IsCurrentCellInEditMode)
                {
                    // If editing, end the edit first
                    CustomerListDataGrid.EndEdit();
                    return;
                }

                if (CustomerListDataGrid.CurrentRow != null &&
                    CustomerListDataGrid.CurrentRow.Index >= 0 &&
                    !CustomerListDataGrid.CurrentRow.IsNewRow)
                {
                    int pId = Convert.ToInt32(CustomerListDataGrid.CurrentRow.Cells[0].Value);

                    DataGridViewRow foundRow = null;
                    foreach (DataGridViewRow row in CustomerListDataGrid.Rows)
                    {
                        if (row.Cells[0].Value != null &&
                            Convert.ToInt32(row.Cells[0].Value) == pId)
                        {
                            foundRow = row;
                            break;
                        }
                    }

                    if (foundRow != null)
                    {
                        pId = Convert.ToInt32(foundRow.Cells[0].Value);
                        CustomerIdLbl.Text = pId.ToString();
                        CustomerNameTxt.Text = $"{(string)foundRow.Cells[1].Value}";

                        ProductEngNameTxt.Focus();
                        ProductEngNameTxt.SelectAll();
                        this.ResetCustomerBtn.Visible = true;

                        // Hide the DataGridView after selection
                        CustomerListDataGrid.Visible = false;

                        using (var context = new POSDbContext())
                        {
                            IOrderRepository orderRepo = new OrderRepository(context);
                            var customerPreviousDue = orderRepo.GetLatestOrderAmountSummaryByCustomerId(pId);
                            UpdatePreviousOrderSummary(customerPreviousDue);
                        }
                    }
                }
            }
        }

        private void UpdatePreviousOrderSummary(OrderAmountSummaryDto customerPreviousDue)
        {
            PreviousOrderSummaryLbl.Text = string.Empty;
            if (customerPreviousDue == null) return;

            previousBillAmountLbl.Text = customerPreviousDue.TotalAmount.ToString();
            PreviousReceivedAmountLbl.Text = customerPreviousDue.ReceivedAmount.ToString();

            float remainingAmount = customerPreviousDue.TotalAmount - customerPreviousDue.ReceivedAmount;

            if (remainingAmount == 0) return;

            var isPositive = remainingAmount >= 0;
            PreviousOrderSummaryLbl.Text = isPositive
                ? $"Remaining Amt:  Rs. {remainingAmount}"
                : $"Return Amt:  Rs. {Math.Abs(remainingAmount)}";

            PreviousOrderSummaryLbl.ForeColor = isPositive ? Color.Red : Color.Blue;
            PreviousOrderSummaryLbl.Visible = true;
        }


        private void AddNewCustomerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create a new instance of your Form
            Form customerForm = new Form();
            customerForm.Text = "Add New Customer";
            customerForm.StartPosition = FormStartPosition.CenterScreen;

            // Create an instance of your User Control
            var CustomerFormCtrl = new Views.Controllers.Customers.CustomerFormControl();
            CustomerFormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

            // Add the User Control to the new Form's controls collection
            customerForm.Controls.Add(CustomerFormCtrl);
            customerForm.Width = 1050; customerForm.Height = 625;
            // Show the new form
            customerForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
        }

        private void ProductEngNameTxt_Enter(object sender, EventArgs e)
        {
            CustomerListDataGrid.Visible = false;
        }

        private void CustomerNameTxt_Enter(object sender, EventArgs e)
        {
            SuggestionGrid.Visible = false;
        }

        private async void SaveOrderWithoutPrintBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
            {
                bool IsDone = false;
                if (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text) && PreviousOrderIdLbl.Text != "Prev Order Id")
                    IsDone = await SaveOrder(true);  //await UpdateOrderSaved();
                else
                    IsDone = await SaveOrder(false);  // await NewOrderSaved();

                if (IsDone)
                {

                    if (isTempSaved)
                    {
                        string sql = "DELETE FROM TempOrders WHERE InvoiceNumber = @InvoiceNumber";
                        string sql1 = "DELETE FROM TempOrderDetails WHERE TempInvoiceNumber = @InvoiceNumber";

                        using (var context = new POSDbContext())
                        {
                            var parameters1 = new[]
                            {
                                new System.Data.SqlClient.SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text)
                            };

                            context.Database.ExecuteSqlCommand(sql1, parameters1);

                            var parameters = new[]
                            {
                                new System.Data.SqlClient.SqlParameter("@InvoiceNumber", InvoiceNoLbl.Text),
                            };
                            context.Database.ExecuteSqlCommand(sql, parameters);
                        }
                    }

                    ResetUIAfterSave();
                    MessageBox.Show("Order Saved Successfully!", "Order Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Order Creation Failed!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
            {
                var orderDetailList = new List<OrderDetail>();


                System.Data.DataTable exportTable = new System.Data.DataTable();
                exportTable.TableName = "Products";

                // Add columns
                exportTable.Columns.Add("ProductID", typeof(int));
                exportTable.Columns.Add("ProductName", typeof(string));
                exportTable.Columns.Add("Qty", typeof(int));
                exportTable.Columns.Add("ProductType", typeof(string));
                exportTable.Columns.Add("SalePrice", typeof(string));

                foreach (DataGridViewRow row in CartProductList.Rows)
                {
                    if (row.Cells["ProductId"].Value == null) continue;

                    var productIdValue = row.Cells["ProductId"].Value?.ToString();
                    var odrDetail = new OrderDetail
                    {
                        ProductId = string.IsNullOrEmpty(productIdValue) ? (int?)null : int.Parse(productIdValue),
                        OtherProductName = row.Cells["Urdu Name"].Value?.ToString(),
                        Quantity = int.Parse(row.Cells["Qty"].Value?.ToString()),
                        QuantityType = row.Cells["ProductType"].Value?.ToString(),
                        Price = float.Parse(row.Cells["SalePrice"].Value?.ToString()),
                        //CreatedDate = DateTime.Now,
                        //OrderId = orderId,
                        ProductDetail = row.Cells["ProductDetail"].Value?.ToString()
                    };

                    exportTable.Rows.Add(odrDetail.ProductId, odrDetail.OtherProductName, odrDetail.Quantity, odrDetail.QuantityType, odrDetail.Price);

                }

                // 3. Ask where to save the file
                using (var sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = "CustomerOrder.xlsx"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // 4. Write to Excel using ClosedXML
                        using (var workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(exportTable, "CustomerOrderSheet");
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("Export successful!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Add the Product first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void BrowsOrderExcelFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Set the filter to show only .bak files
            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|All files|*.*";
            ofd.Title = "Select an Excel File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImportUpdatedFilePathTxt.Text = ofd.FileName;
                LoadOrderExcelFileBtn.Enabled = true;
            }
        }

        private void LoadOrderExcelFileBtn_Click(object sender, EventArgs e)
        {
            using (var stream = File.Open(ImportUpdatedFilePathTxt.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //// Register encoding provider (needed for older Excel files, e.g., .xls)
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };


                    var dataSet = reader.AsDataSet(conf);

                    if (dataSet.Tables.Count == 0)
                    {
                        MessageBox.Show("No worksheets found in the file.", "No data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    var currentTable = dataSet.Tables[0];

                    System.Data.DataTable filtered = new System.Data.DataTable();
                    // Add only required columns



                    filtered.Columns.Add("ProductID", typeof(int));
                    filtered.Columns.Add("ProductName", typeof(string));
                    filtered.Columns.Add("Qty", typeof(string));
                    filtered.Columns.Add("ProductType", typeof(string));
                    filtered.Columns.Add("SalePrice", typeof(int));

                    // CLEAR EXISTING ITEMS FIRST to prevent duplicates
                    CartProductList.Rows.Clear();
                    // Copy rows
                    foreach (DataRow row in currentTable.Rows)
                    {
                        //// Skip rows that are empty or header duplicates
                        //if (row[0] == DBNull.Value || row[0].ToString() == "ID")
                        //    continue;



                        string productId = row[0].ToString() ?? "0";
                        string finalName = row[1].ToString();

                        string productType = row[3].ToString();
                        decimal salePrice = Math.Round(decimal.Parse(row[4].ToString()), 1);
                        int qty = Convert.ToInt32(row[2].ToString());
                        decimal amount = salePrice * qty;

                        CartProductList.Rows.Add(null, amount, salePrice, finalName,
                                               productType, qty, productId, null);
                    }
                    CalculateTotals();
                }
            }
            ImportUpdatedFilePathTxt.Text = string.Empty;
            InvoicePageTabControl.SelectedTab = BilPad;
        }


        // Updates product price when product type is selected from dropdown
        private void productTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var i = PId;
            // Check if SelectedValue is null or the default option
            if (productTypeDropdown.SelectedItem == null || string.IsNullOrEmpty(productTypeDropdown.SelectedValue?.ToString()))
                return;

            // Get the selected ID as integer
            string selectedValue = Convert.ToString(productTypeDropdown.SelectedValue);
            if (!string.IsNullOrEmpty(PId))
            {
                int pid = Convert.ToInt32(PId);
                using (var context = new POSDbContext())
                {
                    var price = context.ProductPrices
                  .FirstOrDefault(s => s.ProductId == pid && s.TypeName == selectedValue)
                  ?.Price ?? 0;

                    ProductSalePrice.Text = $"{price:0}";
                }
            }
            else
                ProductSalePrice.Text = "0";

            if (!string.IsNullOrEmpty(P_StockQtyTxt.Text))
                ProductAmount.Text = Convert.ToString(Convert.ToInt32(P_StockQtyTxt.Text) * Convert.ToInt32(ProductSalePrice.Text));
        }
    }
}
