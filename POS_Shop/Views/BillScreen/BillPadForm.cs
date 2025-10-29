using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using POS_Shop.DTOs.Product;
using POS_Shop.Helpers;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using POS_Shop.Views.Controllers.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
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
            InvoiceNoLbl.Text = DateTime.Now.ToString("ddMMyy-HHmmss");
            this.Shown += (s, e) => { ProductEngNameTxt.Focus(); };

            SetItemGridView();

            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;

        }

        //private void Form_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        // Don't override Enter for ProductEnglishName
        //        if (this.ActiveControl == ProductEngNameTxt)
        //            return; // let your ProductEngNameTxt_KeyPress logic run

        //        if (this.ActiveControl == CustomerNameTxt)
        //            return; // let your CustomerNameTxt_KeyPress logic run

        //        if (this.ActiveControl == TopBarSearchProductTxt)
        //            return;  // let your TopBarSearchProductTxt_KeyPress logic run

        //        e.SuppressKeyPress = true; // prevent ding

        //        //    // Move to next control
        //        this.SelectNextControl(
        //            this.ActiveControl,
        //            true,   // forward
        //            true,   // tabStop only
        //            true,   // include nested
        //            true    // wrap around
        //        );

        //    }
        //}

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
            //CartProductList.Columns.Add(btnCol);

            // Set the width of the button column
            CartProductList.Columns["Delete"].Width = 50;

        }


        private void BackScreenBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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
            if (!decimal.TryParse(ProductSalePrice.Text, out decimal salePrice))  //if (!decimal.TryParse(ProductSalePrice.Text, out decimal salePrice) || salePrice <= 0)
            {
                MessageBox.Show("Enter a valid sale price.", "Validation Error");
                return false;
            }
            return true; // ✅ Passed all checks
        }

        

        private void AddToCardBtn_Click(object sender, EventArgs e)
        {

            if (!ValidateInputs())
                return; // stop if validation fails

            // Get values from the TextBoxes
            string productId = PId; // (or use the label SearchProductUI.ProdIdLbl.Text)
            string productName = ProductEngNameTxt.Text;
            string ProductUrduName = prod_U_Name;
            string productType = productTypeDropdown.SelectedItem?.ToString();
            decimal salePrice = Math.Round(decimal.Parse(ProductSalePrice.Text), 1);
            int qty = int.Parse(P_StockQtyTxt.Text);
            decimal amount = salePrice * qty;

            bool productExists = false;
            var finalName = OtherProductChk.Checked == false ? $"{ProductUrduName} {ProductDetailTxt.Text}" : $"{productName} {ProductDetailTxt.Text}";


            //string formattedText = FixCommonPatterns(finalName);

            string formattedText = TextFormatHelper.FormatMixedText(finalName);
            var finalPId = OtherProductChk.Checked == false ? productId : "";
            //if (!OtherProductChk.Checked)
            //{
            // Loop through DataGridView rows to check if product already exists
            foreach (DataGridViewRow row in CartProductList.Rows)
            {  

                string existingName = row.Cells["Urdu Name"].Value.ToString();
                // Remove directional characters for comparison
                string cleanExisting = TextFormatHelper.RemoveDirectionalCharacters(existingName);
                string cleanNew = TextFormatHelper.RemoveDirectionalCharacters(formattedText);

                if (string.Equals(
                    cleanExisting.Trim(),
                    cleanNew.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    // Product already exists → increase Qty & update Amount
                    int existingQty = int.Parse(row.Cells["Qty"].Value.ToString());
                    existingQty += qty;
                    row.Cells["Qty"].Value = existingQty;

                    decimal newAmount = existingQty * salePrice;
                    row.Cells["Amount"].Value = Math.Round(newAmount, 1);
                    productExists = true;
                    break;
                }

            }
            //}

            // If product doesn’t exist, add a new row
            if (!productExists)
            {
                //CartProductList.Rows.Add(finalPId, finalName, productType, qty,salePrice, amount);
                CartProductList.Rows.Add(null, amount, salePrice, formattedText, productType, qty, finalPId, ProductDetailTxt.Text);
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
            // Example: assuming you have labels for these
            TotalItemLbl.Text = totalItems.ToString();
            //TotalAmountLbl.Text = subTotal.ToString("C2", CultureInfo.GetCultureInfo("en-PK")); // Format as currency
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

            #region Show the products list as Modal.. Don't Remove this code

            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    LoadingManager.ShowLoading();

            //    e.Handled = true; // Prevents the default beep sound
            //    //MessageBox.Show($"Enter Pressed :{Keys.Enter}");
            //    Task.Delay(5000);

            //    OtherProductChk.Checked = false;
            //    var SearchProductUI = new SearchProductUI();
            //    SearchProductUI.ShowDialog();

            //    if (Convert.ToBoolean(SearchProductUI.FormCloseLbl.Text) == false)
            //    {
            //        ProductEngNameTxt.Text = SearchProductUI.PNameLbl.Text;
            //        prod_U_Name = SearchProductUI.PUNameLbl.Text;
            //        PId = SearchProductUI.ProdIdLbl.Text;
            //        ProductSalePrice.Text = SearchProductUI.ProdSalePriceLbl.Text;
            //        P_StockQtyTxt.Text = "1";
            //        var amt = decimal.Parse(ProductSalePrice.Text) * int.Parse(P_StockQtyTxt.Text);
            //        ProductAmount.Text = Convert.ToString(amt);
            //        productTypeDropdown.SelectedItem = !string.IsNullOrEmpty(SearchProductUI.PTypeLbl.Text) ? SearchProductUI.PTypeLbl.Text : productTypeDropdown.SelectedItem = "ڈبہ";


            //        ProductDetailTxt.Focus();
            //    }

            //}

            #endregion


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


        private async void ShowSuggestions(string searchText, bool isForCustomer=false)
        {
            try
            {
                if(isForCustomer)
                {
                    using (var context = new POSDbContext())
                    {
                        ICustomerRepository customerRepository = new CustomerRepository(context);
                        var result = await customerRepository.GetCustomerPagingListAsync(pageIndex:1, pageSize:100, searchText);
                      

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
                    dt.Columns.Add("Type", typeof(string));
                    dt.Columns.Add("Sale-P", typeof(string));


                    foreach (var item in suggestions)
                    {
                        dt.Rows.Add(item.ProductId, item.purchasePrice, item.ProductName, TextFormatHelper.FormatMixedText(item.ProductUrduName), item.ProductType, item.Price);
                        //dt.Rows.Add(item.ProductId, item.ProductName, item.ProductUrduName, item.purchasePrice, item.ProductType, item.Price);
                    }

                    SuggestionGrid.ReadOnly = true;
                    SuggestionGrid.AllowUserToAddRows = false;
                    SuggestionGrid.DataSource = dt;

                    SuggestionGrid.Columns[0].Width = 40;
                    SuggestionGrid.Columns[1].Width = 50;
                    SuggestionGrid.Columns[2].Width = 190;
                    SuggestionGrid.Columns[3].Width = 190;
                    SuggestionGrid.Columns[4].Width = 40;
                    SuggestionGrid.Columns[5].Width = 65;
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

        private List<ProductSuggestion> GetProductSuggestions(string searchText)
        {
            // Replace this with your actual data access logic
            var suggestions = new List<ProductSuggestion>();

            using (var _context = new POSDbContext())
            {
                var data = _context.Products.AsQueryable();

                // apply search

                if (!string.IsNullOrEmpty(searchText))
                {
                    var searchWords = searchText.ToLower().Split(' ');
                    // apply search

                    foreach (var word in searchWords)
                    {
                        data = data.Where(s => s.ProductEnglishName.Contains(word) || s.Id.ToString().Contains(word) || s.SearchByProductCode.Contains(word));
                        //data = data.Where(s => s.CustomerName.Contains(word) || s.City.Name.Contains(word));
                    }
                }

                var result = data.OrderBy(s => s.Id).Select(s => new ProductSuggestion()
                {
                    ProductId = s.Id,
                    ProductName = s.ProductEnglishName,
                    ProductUrduName = s.ProductUrduName,
                    ProductType = s.ProductType,
                    Price = s.SalePrice.HasValue ? s.SalePrice.Value : 0,
                    purchasePrice = s.PurchasePrice,
                }).Take(100).ToList();

                return result;
            }
            ;
        }


        private void P_StockQtyTxt_TextChange(object sender, EventArgs e)
        {
            //var IsFalid= RegexValidator.ValidateAndRevert(P_StockQtyTxt.Text, ValidationPattern.NumbersOnly.ToString());

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

            #region Code to show the Dialog form for Customer Search.. Don't Remove this code

            //if (e.KeyChar == (char)Keys.Enter)
            //{

            //    e.Handled = true; // Prevents the default beep sound

            //    OtherProductChk.Checked = false;
            //    var SearchCustomerUI = new SearchCustomerUI();
            //    SearchCustomerUI.ShowDialog();
            //    if (Convert.ToBoolean(SearchCustomerUI.FormCloseLbl.Text) == false)
            //    {
            //        CustomerNameTxt.Text = SearchCustomerUI.CustomerName.Text;
            //        customerId = SearchCustomerUI.CustomerIdLbl.Text;
            //        CustomerIdLbl.Text = customerId;
            //        this.ResetCustomerBtn.Visible = true;
            //        ProductEngNameTxt.Focus();
            //    }
            //}

            #endregion

            if (e.KeyChar == (char)Keys.Enter)
            {
                
                    if (CustomerNameTxt.Visible == false)
                    {
                        ShowSuggestions(CustomerNameTxt.Text, isForCustomer:true);

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
            InvoiceNoLbl.Text = DateTime.Now.ToString("ddMMyy-HHmmss");
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
                // Replace 'YourUserControl' with the actual name of your User Control
                var FormCtrl = new Views.Product.ProductFromControl();
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

            // Create an instance of your User Control
            // Replace 'YourUserControl' with the actual name of your User Control
            var FormCtrl = new Views.Controllers.Order.OrdersControlUI();
            FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

            // Add the User Control to the new Form's controls collection
            OrderListForm.Controls.Add(FormCtrl);
            OrderListForm.Width = 830; OrderListForm.Height = 550;
            // Show the new form
            OrderListForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
           if (FormCtrl.isRecordSelected==true)
            {

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
            }

        }


        private async void PreviousOrderIdLbl_TextChanged(object sender, EventArgs e)
        {
            if ((PreviousOrderIdLbl.Text != "OrderID" && InvoiceNoLbl.Text != "InvoiceNo") && (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text) && !string.IsNullOrEmpty(InvoiceNoLbl.Text)))
            {
                using (var context = new POSDbContext())
                {
                    var orderRepo = new OrderRepository(context);
                    var result = await orderRepo.GetOrderByIdAsync(Convert.ToInt32(PreviousOrderIdLbl.Text), InvoiceNoLbl.Text);
                    if (result != null)
                    {

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

                        if (result.OrderDetailsList.Count > 0)
                            CartProductList.Rows.Clear();

                        foreach (var order in result.OrderDetailsList)
                        {
                            // Get values from the TextBoxes
                            string productId = order.ProductId.ToString() ?? "0"; // (or use the label SearchProductUI.ProdIdLbl.Text)
                            string finalName = !string.IsNullOrEmpty(order.ProductDetail)==true ?$"{order.ProductName} {order.ProductDetail}":order.ProductName;

                            string productType = order.QuantityType;
                            decimal salePrice = Math.Round(decimal.Parse(order.Price.ToString()), 1);
                            int qty = order.Quantity;
                            decimal amount = salePrice * qty;
                            //CartProductList.Rows.Add(productId, finalName, productType, qty, salePrice, amount);
                            CartProductList.Rows.Add(null, amount, salePrice, finalName, productType, qty, productId, order.ProductDetail);

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
                    #region Whatsapp Feature done
                    //string pdfName = !string.IsNullOrEmpty(CustomerNameTxt.Text) == true ? $"{CustomerNameTxt.Text}-{InvoiceNoLbl.Text}" : InvoiceNoLbl.Text;
                    //string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Invoice_{pdfName}.pdf");

                    //PrintToPdfGenerator generator = new PrintToPdfGenerator();
                    //generator.GenerateInvoice(CartProductList,
                    //            filePath, customerName: CustomerNameTxt.Text, invoiceNo: InvoiceNoLbl.Text, totalAmount: TotalAmountLbl.Text, receivedAmount: ReceivedAmountTxt.Text);
                    //ToastHelper.ShowSuccess($"Invoice saved to:\n{pdfName}");
                    //await Task.Delay(2000);
                    //var send = new SimpleWhatsAppSender();
                    //send.SendInvoice("+92__Phone Number", pdfName, decimal.Parse(TotalAmountLbl.Text), filePath);

                    //// Open the PDF automatically
                    ////System.Diagnostics.Process.Start(filePath);

                    #endregion

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
                    // First clear any previous handlers
                    OrderPrintDocument.PrintPage -= OrderPrintDocument_PrintPage;
                    OrderPrintDocument.PrintPage -= OrderPrintDocument_PrintPage_English;

                    if (EnglishInvoiceChk.Checked)
                        OrderPrintDocument.PrintPage += OrderPrintDocument_PrintPage_English;
                    else
                        OrderPrintDocument.PrintPage += OrderPrintDocument_PrintPage;

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

                    //GenerateInvoicePdf();
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
            // Ask for confirmation (optional)
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
                            //InvoicePDFGenerator.SaveInvoiceAsPdf(
                            //    saveFileDialog.FileName,
                            //    CartProductList, // your DataGridView
                            //    CustomerNameTxt.Text,
                            //    InvoiceNoLbl.Text,
                            //    TotalAmountLbl.Text,
                            //    CashRadioBtn.Checked,
                            //    ReceivedAmountTxt.Text,
                            //    InvoiceShopName.Checked
                            //);

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

                    //if (context.Orders.Any(s => s.InvoiceNumber ==orderData.InvoiceNumber ))
                    //{
                    //    isUpdate= true;
                    //}
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
                paymentType = CashRadioBtn.Checked ? "Cash" : "Bank Transfer",
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

            // Remove existing order details
            var existingDetails = context.OrderDetails.Where(s => s.OrderId == orderId).ToList();
            context.OrderDetails.RemoveRange(existingDetails);
            await context.SaveChangesAsync();

            return orderId;
        }

        private async Task SaveOrderDetails(POSDbContext context, int orderId)
        {
            var orderDetailList = new List<OrderDetail>();

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells["ProductId"].Value == null) continue;

                var productIdValue = row.Cells["ProductId"].Value?.ToString();
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


        #region Old code

        //private async Task<bool> NewOrderSaved()
        //{
        //    using (var context = new POSDbContext())
        //    {

        //        using (var dbTransaction = context.Database.BeginTransaction())
        //        {

        //            try
        //            {
        //                var orderRepository = new OrderRepository(context);
        //                int? customerId = null;

        //                if (!string.IsNullOrEmpty(CustomerNameTxt.Text) && !string.IsNullOrEmpty(CustomerIdLbl.Text))
        //                {
        //                    if (int.TryParse(CustomerIdLbl.Text, out int parsedId))
        //                    {
        //                        customerId = parsedId;
        //                    }
        //                    else
        //                    {
        //                        // Handle parsing error
        //                        customerId = null;
        //                    }
        //                }
        //                else
        //                {
        //                    customerId = null;
        //                }

        //                float totalBill;
        //                if (!float.TryParse(TotalAmountLbl.Text, out totalBill))
        //                {
        //                    totalBill = 0; // or handle error
        //                }

        //                float receiveAmount;
        //                if (!string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
        //                {
        //                    if (!float.TryParse(ReceivedAmountTxt.Text, out receiveAmount))
        //                        receiveAmount = totalBill; // fallback
        //                }
        //                else
        //                {
        //                    receiveAmount = totalBill;
        //                }
        //                // Create new order
        //                var order = new Order
        //                {

        //                    TotalBill = totalBill,
        //                    ReceiveAmount = receiveAmount,
        //                    CreatedDate = DateTime.Now,
        //                    InvoiceNumber = InvoiceNoLbl.Text,
        //                    paymentType = CashRadioBtn.Checked ? "Cash" : "Bank Transfer",
        //                    customerId = customerId
        //                };

        //                var orderId = await orderRepository.AddOrder(order);

        //                var orderDetailList = new List<OrderDetail>();

        //                foreach (DataGridViewRow row in CartProductList.Rows)
        //                {


        //                    if (row.Cells[0].Value != null) // Check if row has data
        //                    {
        //                        var odrDetail = new OrderDetail
        //                        {
        //                            ProductId = string.IsNullOrEmpty(row.Cells[0].Value?.ToString()) ?
        //                                       (int?)null : int.Parse(row.Cells[0].Value.ToString()),
        //                            OtherProductName = string.IsNullOrEmpty(row.Cells[0].Value?.ToString()) ?
        //                                             row.Cells[1].Value?.ToString() : null,
        //                            Quantity = int.Parse(row.Cells[4].Value?.ToString()),
        //                            QuantityType = row.Cells[2].Value?.ToString(),
        //                            Price = float.Parse(row.Cells[3].Value?.ToString()),
        //                            CreatedDate = DateTime.Now,
        //                            OrderId = orderId,
        //                        };
        //                        orderDetailList.Add(odrDetail);
        //                    }
        //                }

        //                context.OrderDetails.AddRange(orderDetailList);
        //                await context.SaveChangesAsync();

        //                dbTransaction.Commit();
        //                return true;

        //            }
        //            catch (Exception ex)
        //            {
        //                dbTransaction.Rollback();
        //                return false;

        //            }
        //        }
        //    }
        //}


        //private async Task<bool> UpdateOrderSaved()
        //{
        //    using (var context = new POSDbContext())
        //    {

        //        using (var dbTransaction = context.Database.BeginTransaction())
        //        {

        //            try
        //            {
        //                var orderRepository = new OrderRepository(context);
        //                int? customerId = null;

        //                if (!string.IsNullOrEmpty(CustomerNameTxt.Text) && !string.IsNullOrEmpty(CustomerIdLbl.Text))
        //                {
        //                    if (int.TryParse(CustomerIdLbl.Text, out int parsedId))
        //                    {
        //                        customerId = parsedId;
        //                    }
        //                    else
        //                    {
        //                        // Handle parsing error
        //                        customerId = null;
        //                    }
        //                }
        //                else
        //                {
        //                    customerId = null;
        //                }

        //                float totalBill;
        //                if (!float.TryParse(TotalAmountLbl.Text, out totalBill))
        //                {
        //                    totalBill = 0; // or handle error
        //                }

        //                float receiveAmount;
        //                if (!string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
        //                {
        //                    if (!float.TryParse(ReceivedAmountTxt.Text, out receiveAmount))
        //                        receiveAmount = totalBill; // fallback
        //                }
        //                else
        //                {
        //                    receiveAmount = totalBill;
        //                }
        //                // Create new order
        //                var order = new Order
        //                {
        //                    Id=int.Parse(PreviousOrderIdLbl.Text),
        //                    TotalBill = totalBill,
        //                    ReceiveAmount = receiveAmount,
        //                    CreatedDate = DateTime.Now,
        //                    InvoiceNumber = InvoiceNoLbl.Text,
        //                    paymentType = CashRadioBtn.Checked ? "Cash" : "Bank Transfer",
        //                    customerId = customerId
        //                };

        //                var orderId = await orderRepository.AddOrder(order);

        //                var orderDetailList = new List<OrderDetail>();
        //                var Details = context.OrderDetails.Where(s => s.OrderId.Equals(orderId)).ToList();
        //                context.OrderDetails.RemoveRange(Details);
        //                context.SaveChanges();
        //                foreach (DataGridViewRow row in CartProductList.Rows)
        //                {


        //                    if (row.Cells[0].Value != null) // Check if row has data
        //                    {
        //                        var odrDetail = new OrderDetail
        //                        {
        //                            ProductId = string.IsNullOrEmpty(row.Cells[0].Value?.ToString()) ?
        //                                       (int?)null : int.Parse(row.Cells[0].Value.ToString()),
        //                            OtherProductName = string.IsNullOrEmpty(row.Cells[0].Value?.ToString()) ?
        //                                             row.Cells[1].Value?.ToString() : null,
        //                            Quantity = int.Parse(row.Cells[4].Value?.ToString()),
        //                            QuantityType = row.Cells[2].Value?.ToString(),
        //                            Price = float.Parse(row.Cells[3].Value?.ToString()),
        //                            CreatedDate = DateTime.Now,
        //                            OrderId = orderId,
        //                        };
        //                        orderDetailList.Add(odrDetail);
        //                    }
        //                }

        //                context.OrderDetails.AddRange(orderDetailList);
        //                await context.SaveChangesAsync();

        //                dbTransaction.Commit();
        //                return true;

        //            }
        //            catch (Exception ex)
        //            {
        //                dbTransaction.Rollback();
        //                return false;

        //            }
        //        }
        //    }
        //}


        #endregion


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

            InvoicePrintHelper.PrintInvoice(
                  e: e,
                  cartProductList: CartProductList,
                  customerName: CustomerNameTxt.Text,
                  invoiceNo: InvoiceNoLbl.Text,
                  totalAmount: TotalAmountLbl.Text,
                  isCashPayment: CashRadioBtn.Checked,
                  receivedAmount: ReceivedAmountTxt.Text,
                  hideShopName: InvoiceShopName.Checked
              );


            #region this code is using in static helper class. Don't remove it

            //// Thermal printer settings (80mm paper)
            //int paperWidth = 280; // pixels for 80mm paper
            //int leftMargin = 0;
            //int currentY = 5;
            //int lineHeight = 12;
            //int sectionSpacing = 3;

            //// Fonts for thermal printing
            //System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
            //Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            //Font regularFont = new Font("Arial", 8, FontStyle.Regular);
            //Font smallFont = new Font("Arial", 7, FontStyle.Regular);

            //// Urdu font
            //Font urduFont = new Font("Arial", 9, FontStyle.Regular);
            //if (urduFont.Name != "Nafees Web Naskh")
            //    urduFont = new Font("Arial", 8, FontStyle.Regular);

            //// Center alignment
            //StringFormat centerFormat = new StringFormat();
            //centerFormat.Alignment = StringAlignment.Center;

            //// Right alignment for Urdu (right-to-left)
            //StringFormat rightFormat = new StringFormat();
            //rightFormat.Alignment = StringAlignment.Near;
            //rightFormat.LineAlignment = StringAlignment.Near;

            //// Left alignment for English text
            //StringFormat leftFormat = new StringFormat();
            //leftFormat.Alignment = StringAlignment.Near;

            //string dashLine = new string('-', 82);

            //// 1. COMPANY HEADER
            //if (!InvoiceShopName.Checked)
            //{
            //    e.Graphics.DrawString("Electric Shop", titleFont, Brushes.Black,
            //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
            //    currentY += lineHeight * 2;
            //    e.Graphics.DrawString("Contact: 1234567", smallFont, Brushes.Black,
            //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            //    currentY += lineHeight;
            //    currentY += lineHeight + 2;
            //}

            //// 2. INVOICE INFO - Mixed Urdu and English
            //e.Graphics.DrawString("انوائس", headerFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight;

            //string cName = !string.IsNullOrEmpty(CustomerNameTxt.Text) ? CustomerNameTxt.Text : "";
            //e.Graphics.DrawString($"کسٹمر: {cName}", urduFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight;

            //e.Graphics.DrawString("تاریخ: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), urduFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight;

            //e.Graphics.DrawString("انوائس نمبر:" + InvoiceNoLbl.Text, urduFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight + 2;

            //e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            //currentY += lineHeight + 2;

            //// Define columns - ADJUSTED WIDTHS
            //int col1 = leftMargin;                    // کل (Total)
            //int col1Width = 60;  // INCREASED from 40

            //int col2 = col1 + col1Width + 5;          // قیمت (Price)
            //int col2Width = 50;  // INCREASED from 40

            //int col3 = col2 + col2Width + 5;          // تعداد + قسم (Quantity + Type)
            //int col3Width = 100; // INCREASED from 40 to accommodate both fields

            //// REMOVED separate قسم column since it's now combined with تعداد
            //int productCol = col3 + col3Width + 5;    // پروڈکٹ (Product name)
            //int productColWidth = paperWidth - productCol - 5;

            //// Draw Urdu table headers - UPDATED
            //e.Graphics.DrawString("قیمت", headerFont, Brushes.Black,
            //                     new Rectangle(col1, currentY, col1Width, lineHeight), rightFormat);
            //e.Graphics.DrawString("ریٹ ", headerFont, Brushes.Black,
            //                     new Rectangle(col2, currentY, col2Width, lineHeight), rightFormat);
            //e.Graphics.DrawString("تعداد", headerFont, Brushes.Black,  // COMBINED HEADER
            //                     new Rectangle(col3, currentY, col3Width, lineHeight), rightFormat);
            //e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.Black,
            //                     new Rectangle(productCol, currentY, productColWidth, lineHeight), rightFormat);

            //currentY += lineHeight;
            //e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
            //currentY += 5;

            //// TABLE ROWS - 2 ROWS PER PRODUCT
            //foreach (DataGridViewRow row in CartProductList.Rows)
            //{
            //    if (row.Cells[0].Value != null)
            //    {
            //        // Extract values
            //        decimal amount = row.Cells["Amount"]?.Value != null ? Convert.ToDecimal(row.Cells["Amount"].Value) : 0;
            //        decimal salePrice = row.Cells["SalePrice"]?.Value != null ? Convert.ToDecimal(row.Cells["SalePrice"].Value) : 0;
            //        decimal qty = row.Cells["Qty"]?.Value != null ? Convert.ToDecimal(row.Cells["Qty"].Value) : 0;
            //        string productType = row.Cells["ProductType"]?.Value?.ToString() ?? "";
            //        string productName = row.Cells["Urdu Name"]?.Value?.ToString() ?? "";

            //        // ROW 1: PRODUCT NAME ONLY - SPANS ALL COLUMNS
            //        StringFormat productFormat = new StringFormat();
            //        productFormat.Alignment = StringAlignment.Far; // Left align for product names
            //        productFormat.LineAlignment = StringAlignment.Center;
            //        productFormat.FormatFlags = StringFormatFlags.NoWrap;
            //        productFormat.Trimming = StringTrimming.None;

            //        // Product name uses FULL WIDTH from leftMargin to right edge
            //        int fullWidth = paperWidth - leftMargin - 5;
            //        e.Graphics.DrawString(productName, regularFont, Brushes.Black,
            //                             new Rectangle(leftMargin, currentY, fullWidth, lineHeight), productFormat);

            //        // ROW 2: DETAILS IN SEPARATE COLUMNS
            //        int detailsY = currentY + lineHeight;

            //        // Draw details in their respective columns
            //        e.Graphics.DrawString($"{amount:0}", regularFont, Brushes.Black,
            //                             new Rectangle(col1, detailsY, col1Width, lineHeight), rightFormat);
            //        e.Graphics.DrawString($"{salePrice:0}", regularFont, Brushes.Black,
            //                             new Rectangle(col2, detailsY, col2Width, lineHeight), rightFormat);

            //        // COMBINED: تعداد + قسم in same column
            //        string combinedQtyType = $"{qty:0} / {productType}";
            //        e.Graphics.DrawString(combinedQtyType, regularFont, Brushes.Black,
            //                             new Rectangle(col3, detailsY, col3Width, lineHeight), rightFormat); // تعداد / قسم
            //                                                                                                 //e.Graphics.DrawString(productType, regularFont, Brushes.Black,
            //                                                                                                 //                     new Rectangle(col4, detailsY, col4Width, lineHeight), rightFormat);

            //        // Product column is EMPTY on details row since name was in row 1
            //        e.Graphics.DrawString("", regularFont, Brushes.Black,
            //                             new Rectangle(productCol, detailsY, productColWidth, lineHeight), rightFormat);

            //        currentY = detailsY + lineHeight;
            //        e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY); // Bottom line
            //        currentY += 5; // Extra spacing between products
            //    }
            //}

            //currentY += sectionSpacing;

            //// 5. TOTALS SECTION - Urdu labels
            //decimal subtotal = decimal.Parse(TotalAmountLbl.Text);
            //decimal taxAmount = 0m; // 0% tax
            //decimal total = subtotal + taxAmount;

            //// Totals section with Urdu labels
            //e.Graphics.DrawString($"سب ٹوٹل: {subtotal:0}", urduFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight;

            ////e.Graphics.DrawString($"ٹیکس (0%): {taxAmount:0}", urduFont, Brushes.Black,
            ////                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            ////currentY += lineHeight;

            //e.Graphics.DrawString($"کل رقم: {total:0}", headerFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight;

            //currentY += lineHeight;

            //e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            //currentY += lineHeight + 2;

            //var method = CashRadioBtn.Checked == true ? "نقد" : "بینک ٹرانسفر";
            //// 6. PAYMENT INFORMATION - Urdu
            //e.Graphics.DrawString($"ادائیگی کا طریقہ: {method}", urduFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight;

            //decimal tendered = !string.IsNullOrEmpty(ReceivedAmountTxt.Text) ? decimal.Parse(ReceivedAmountTxt.Text) : decimal.Parse(TotalAmountLbl.Text);
            //decimal change = tendered - total;

            //e.Graphics.DrawString($"وصول رقم: {tendered:0}", urduFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight;

            //e.Graphics.DrawString($"بقایا: {change:0}", urduFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight + 2;

            //// 7. URDU FOOTER TEXT
            //e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            //currentY += lineHeight;

            ////string footerText1 = "خریدا ہوا سامان واپس یا تبدیل نہیں ہوگا۔";
            //string footerText2 = "چائنہ مال کی وارنٹی نہیں۔";

            ////e.Graphics.DrawString(footerText1, headerFont, Brushes.Black,
            ////                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            ////currentY += lineHeight;

            //e.Graphics.DrawString(footerText2, headerFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            //currentY += lineHeight;

            #endregion
        }
        // This is default
        private void OrderPrintDocument_PrintPage_English(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Thermal printer settings (80mm paper)
            int paperWidth = 280; // pixels for 80mm paper
            int leftMargin = 5;
            int currentY = 5;
            int lineHeight = 12;
            int sectionSpacing = 3;

            // Fonts for thermal printing
            System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
            Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            Font regularFont = new Font("Arial", 8, FontStyle.Regular);
            Font smallFont = new Font("Arial", 7, FontStyle.Regular);

            // Urdu font
            Font urduFont = new Font("Nafees Web Naskh", 8, FontStyle.Regular);
            if (urduFont.Name != "Nafees Web Naskh")
                urduFont = new Font("Arial", 8, FontStyle.Regular);

            // Center alignment
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;

            // Right alignment for numbers
            StringFormat rightFormat = new StringFormat();
            rightFormat.Alignment = StringAlignment.Far;

            string dashLine = new string('-', 82);

            // 1. COMPANY HEADER
            if (!InvoiceShopName.Checked)
            {
                e.Graphics.DrawString("Electric Shop", titleFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
                currentY += lineHeight * 2;
                e.Graphics.DrawString("Contact: 1234567", smallFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
                currentY += lineHeight;
                currentY += lineHeight + 2;
            }


            // 2. INVOICE INFO
            e.Graphics.DrawString("INVOICE", headerFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            string cName = !string.IsNullOrEmpty(CustomerNameTxt.Text) ? CustomerNameTxt.Text : "";
            e.Graphics.DrawString($"Customer: {cName}", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Invoice #:" + InvoiceNoLbl.Text, regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 3. TABLE LAYOUT - FIXED COLUMN POSITIONS TO PREVENT OVERLAP
            int productCol = leftMargin;                    // Product name column
            int productColWidth = 120;                      // Width for product names

            int typeCol = productCol + productColWidth + 5; // Type column
            int typeColWidth = 30;

            int qtyCol = typeCol + typeColWidth + 5;        // Qty column
            int qtyColWidth = 25;

            int priceCol = qtyCol + qtyColWidth + 5;        // Price column
            int priceColWidth = 40;

            int totalCol = priceCol + priceColWidth + 5;    // Total column
            int totalColWidth = 90;

            e.Graphics.DrawString("Product", headerFont, Brushes.Black, productCol, currentY);
            e.Graphics.DrawString(" ", headerFont, Brushes.Black, typeCol, currentY);
            e.Graphics.DrawString("Qty", headerFont, Brushes.Black, qtyCol, currentY);
            e.Graphics.DrawString("Price", headerFont, Brushes.Black, priceCol, currentY);
            e.Graphics.DrawString("Amount", headerFont, Brushes.Black, totalCol, currentY);

            currentY += lineHeight;
            currentY += 3;
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + totalColWidth, currentY);
            currentY += 5;

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells[0].Value != null) // Check if row has data
                {

                    string urduName = row.Cells["Urdu Name"]?.Value?.ToString() ?? "";
                    string formattedText = TextFormatHelper.FormatMixedText(urduName);
                    // First line: Product name only (left aligned)
                    e.Graphics.DrawString(formattedText, regularFont, Brushes.Black, productCol, currentY);
                    currentY += lineHeight;

                    // Second line: Type, Qty, Price, Total (in columns)
                    e.Graphics.DrawString(row.Cells["ProductType"].Value?.ToString(), urduFont, Brushes.Black, typeCol, currentY);
                    e.Graphics.DrawString($"{Convert.ToDecimal(row.Cells["Qty"].Value):0}", regularFont, Brushes.Black, qtyCol, currentY);
                    e.Graphics.DrawString($"{Convert.ToDecimal(row.Cells["SalePrice"].Value):0}", regularFont, Brushes.Black, priceCol, currentY);
                    e.Graphics.DrawString($"{Convert.ToDecimal(row.Cells["Amount"].Value):0}", regularFont, Brushes.Black, totalCol, currentY);

                    currentY += lineHeight;
                }
                e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + totalColWidth, currentY);
                currentY += lineHeight;
            }


            // 5. TOTALS SECTION - MOVED LEFT FOR BETTER ALIGNMENT
            decimal subtotal = decimal.Parse(TotalAmountLbl.Text);
            decimal taxRate = 0.05m;
            //decimal taxAmount = Math.Round(subtotal * taxRate, 2);
            decimal taxAmount = Math.Round(0m, 2);
            decimal total = subtotal + taxAmount;

            // Move totals left by using priceCol-20 instead of priceCol
            int totalsLabelCol = priceCol - 20; // Move labels 20 pixels left
            int totalsValueCol = totalCol - 15; // Move values 15 pixels left


            e.Graphics.DrawString("Subtotal:", regularFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(subtotal.ToString("0"), regularFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Tax (0%):", regularFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(taxAmount.ToString("0"), regularFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("TOTAL:", headerFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(total.ToString("0"), headerFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            currentY += lineHeight;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 6. PAYMENT INFORMATION
            e.Graphics.DrawString("Payment Method: CASH", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            decimal tendered = !string.IsNullOrEmpty(ReceivedAmountTxt.Text) ? decimal.Parse(ReceivedAmountTxt.Text) : decimal.Parse(TotalAmountLbl.Text);
            decimal change = tendered - total;

            e.Graphics.DrawString("Paid: " + $"{Convert.ToDecimal(tendered):0}", regularFont, Brushes.Black, leftMargin, currentY);
            e.Graphics.DrawString("Change: " + $"{Convert.ToDecimal(change):0}", regularFont, Brushes.Black, (totalsValueCol - 35), currentY);
            currentY += lineHeight + 2;

            // 7. FOOTER
            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("No returns or exchanges accepted.", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight;

            e.Graphics.DrawString("Chinese goods have no warranty.", headerFont, Brushes.Black,
                               new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight + 2;

            //e.Graphics.DrawString("7-day return with receipt", smallFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
        }


        private void DrawLine(Graphics graphics, int paperWidth, ref int yPos)
        {
            graphics.DrawLine(Pens.Black, 10, yPos, paperWidth - 10, yPos);
            yPos += 5;
        }


        private void DrawCenteredString(Graphics graphics, string text, Font font, int paperWidth, ref int yPos)
        {
            SizeF textSize = graphics.MeasureString(text, font);
            int xPos = (paperWidth - (int)textSize.Width) / 2;
            graphics.DrawString(text, font, Brushes.Black, xPos, yPos);
            yPos += (int)textSize.Height + 2;
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
            else if (e.KeyCode == Keys.F1) // F1 to Focus on Product TextBox
            {
                ProductEngNameTxt.Focus();
                ProductEngNameTxt.SelectAll();
            }
            else if (e.KeyCode == Keys.R && e.Control)
            {
                GenerateInvoicePdfBtn.PerformClick();
            }
            else if(e.KeyCode== Keys.Q && e.Control)
            {
                e.Handled = true;
                GotoFirstRow();
            }
        }

        private void GotoFirstRow()
        {
            if(CartProductList.Rows.Count > 0)
            {
                CartProductList.ClearSelection();
                CartProductList.Rows[0].Selected = true;
                CartProductList.CurrentCell = CartProductList.Rows[0].Cells[1];
                CartProductList.Focus();
            }
        }

        private void SuggestionGrid_KeyDown(object sender, KeyEventArgs e)
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
                    ////PTypeLbl.Text = (string)ProductListGrid.CurrentRow.Cells[3].Value;
                    //productTypeDropdown.SelectedItem = SuggestionGrid.CurrentRow.Cells[3].Value == null
                    //            || SuggestionGrid.CurrentRow.Cells[3].Value == DBNull.Value
                    //            ? "ڈبہ"
                    //            : SuggestionGrid.CurrentRow.Cells[3].Value.ToString();

                    //ProductSalePrice.Text = SuggestionGrid.CurrentRow.Cells[4].Value == null
                    //    || SuggestionGrid.CurrentRow.Cells[4].Value == DBNull.Value
                    //    ? string.Empty
                    //    : SuggestionGrid.CurrentRow.Cells[4].Value.ToString();
                    //PId = pId.ToString();

                    //P_StockQtyTxt.Text = "1";
                    //SuggestionGrid.Visible = false;

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
                        productTypeDropdown.SelectedItem = foundRow.Cells[4].Value == null
                                    || foundRow.Cells[4].Value == DBNull.Value
                                    ? "ڈبہ"
                                    : foundRow.Cells[4].Value.ToString();

                        ProductSalePrice.Text = foundRow.Cells[5].Value == null
                            || foundRow.Cells[5].Value == DBNull.Value
                            ? string.Empty
                            : foundRow.Cells[5].Value.ToString();
                        PId = pId.ToString();
                        P_StockQtyTxt.Text = "1";
                        SuggestionGrid.Visible = false;

                        ProductAmount.Text = Convert.ToString(Convert.ToInt32(P_StockQtyTxt.Text) * Convert.ToInt32(ProductSalePrice.Text));
                    }
                    ProductDetailTxt.Focus();

                }
            }
        }

        private void SuggestionGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            if (SuggestionGrid.Rows.Count > 0)
            {

                int pId = Convert.ToInt32(SuggestionGrid.CurrentRow.Cells[0].Value);
                ProductEngNameTxt.Text = (string)SuggestionGrid.CurrentRow.Cells[2].Value;
                prod_U_Name = (string)SuggestionGrid.CurrentRow.Cells[3].Value;
                productTypeDropdown.SelectedItem = SuggestionGrid.CurrentRow.Cells[4].Value == null
                            || SuggestionGrid.CurrentRow.Cells[4].Value == DBNull.Value
                            ? "ڈبہ"
                            : SuggestionGrid.CurrentRow.Cells[4].Value.ToString();
                ProductSalePrice.Text = SuggestionGrid.CurrentRow.Cells[5].Value == null
                    || SuggestionGrid.CurrentRow.Cells[5].Value == DBNull.Value
                    ? string.Empty
                    : SuggestionGrid.CurrentRow.Cells[5].Value.ToString();
                PId = pId.ToString();

                P_StockQtyTxt.Text = "1";
                ProductAmount.Text = Convert.ToString(Convert.ToInt32(P_StockQtyTxt.Text) * Convert.ToInt32(ProductSalePrice.Text));
                SuggestionGrid.Visible = false;
                ProductDetailTxt.Focus();
            }
        }

        //private async void SaveBillBtn_Click(object sender, EventArgs e)
        //{
        //    string customerName = string.Empty;

        //    if (CartProductList.Rows.Count != 0 && CartProductList.Rows != null)
        //    {

        //        var confirmResult = MessageBox.Show("Are you sure you want to store Temporary Record?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //        if (confirmResult == DialogResult.Yes)
        //        {
        //            if (string.IsNullOrEmpty(CustomerNameTxt.Text) && string.IsNullOrEmpty(CustomerIdLbl.Text))
        //            {
        //                using (var dialog = new InputDialog("Enter customer name:", "Customer Info"))
        //                {
        //                    if (dialog.ShowDialog() == DialogResult.OK)
        //                    {
        //                        customerName = dialog.InputValue;
        //                        CustomerNameTxt.Text = customerName;
        //                        customerId= string.Empty;
        //                        CustomerIdLbl.Text = string.Empty;
        //                        MessageBox.Show("Customer entered: " + customerName);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                customerName = CustomerNameTxt.Text;
        //            }
        //            using (var context = new POSDbContext())
        //            using (var dbTransaction = context.Database.BeginTransaction())
        //            {
        //                try
        //                {
        //                    var orderRepository = new OrderRepository(context);
        //                    var data =await GetTempOrderData();

        //                     var invoiceNo= await orderRepository.AddTempOrder(data);

        //                    await SaveTempOrderDetails(context, invoiceNo);

        //                    dbTransaction.Commit();


        //                    ClearInputs();
        //                    ClearCartFunction();
        //                    ResetCustomerBtn.Visible = false;
        //                    InvoiceNoLbl.Text = DateTime.Now.ToString("ddMMyy-HHmmss");

        //                    MessageBox.Show("Order Saved Successfully!", "Order Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //                }
        //                catch (DbException ex)
        //                {
        //                    dbTransaction.Rollback();
        //                    MessageBox.Show("Order Creation Failed!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //                }
        //            }


        //        }

        //    }
        //    else
        //    {
        //        MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

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
                        var orderDetail=await context.OrderDetails.Where(od => od.Order.InvoiceNumber == data.InvoiceNumber).ToListAsync();
                    
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
            ResetCustomerBtn.Visible = false;
            InvoiceNoLbl.Text = DateTime.Now.ToString("ddMMyy-HHmmss");
        }

        private void TemOrderBtn_Click(object sender, EventArgs e)
        { // Create a new instance of your Form
            Form ProductForm = new Form();
            ProductForm.Text = "Temp Order Form";
            ProductForm.StartPosition = FormStartPosition.CenterScreen;

            // Create an instance of your User Control
            // Replace 'YourUserControl' with the actual name of your User Control
            var FormCtrl = new TempOrderControl();
            FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

            // Add the User Control to the new Form's controls collection
            ProductForm.Controls.Add(FormCtrl);
            ProductForm.Width = 1050; ProductForm.Height = 525;
            // Show the new form
            ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog

            //if(!string.IsNullOrEmpty(FormCtrl.InvoiceNoLbl.Text))
            //{
            //    InvoiceNoLbl.Text = FormCtrl.InvoiceNoLbl.Text;
            //}

            if (!string.IsNullOrEmpty(FormCtrl.InvoiceNoLbl.Text)) InvoiceNoLbl.Text = FormCtrl.InvoiceNoLbl.Text;

            if (FormCtrl.CustomerId != 0)
            {
                CustomerIdLbl.Text = FormCtrl.CustomerId.ToString();
                CustomerNameTxt.Text = FormCtrl.CustomerName;
                this.ResetCustomerBtn.Visible = true;
                this.ResetCustomerBtn.Enabled = true;
            }

            if (InvoiceNoLbl.Text != "InvoiceNo" && !string.IsNullOrEmpty(InvoiceNoLbl.Text))
            {
                using (var context = new POSDbContext())
                {
                    var orderRepo = new OrderRepository(context);

                    var result = orderRepo.GetTempOrderDetailByInvoice(InvoiceNoLbl.Text);
                    //  var resut = await orderRepo.GetOrderByIdAsync(Convert.ToInt32(PreviousOrderIdLbl.Text), InvoiceNoLbl.Text);
                    if (result != null)
                    {
                        isTempSaved = true;

                        if (result.Count > 0)
                            CartProductList.Rows.Clear();

                        foreach (var order in result)
                        {
                            // Get values from the TextBoxes
                            string productId = order.ProductId.ToString() ?? "0"; // (or use the label SearchProductUI.ProdIdLbl.Text)
                            string finalName = !string.IsNullOrEmpty(order.ProductDetail)==true ?$"{order.ProductName} {order.ProductDetail}":order.ProductName;
                            string productType = order.QuantityType;
                            decimal salePrice = Math.Round(decimal.Parse(order.Price.ToString()), 1);
                            int qty = order.Quantity;
                            decimal amount = salePrice * qty;
                            //CartProductList.Rows.Add(productId, finalName, productType, qty, salePrice, amount);
                            CartProductList.Rows.Add(null, amount, salePrice, finalName, productType, qty, productId);

                        }

                        CalculateTotals();
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

            ShowSuggestions(CustomerNameTxt.Text, isForCustomer:true);
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
                    CustomerNameTxt.Text = (string)CustomerListDataGrid.CurrentRow.Cells[1].Value;

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
                        CustomerNameTxt.Text = (string)foundRow.Cells[1].Value;

                        ProductEngNameTxt.Focus();
                        ProductEngNameTxt.SelectAll();
                        this.ResetCustomerBtn.Visible = true;

                        // Hide the DataGridView after selection
                        CustomerListDataGrid.Visible = false;
                    }
                }
            }
        }

        private void AddNewCustomerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Create a new instance of your Form
            Form customerForm = new Form();
            customerForm.Text = "Add New Customer";
            customerForm.StartPosition = FormStartPosition.CenterScreen;

            // Create an instance of your User Control
            // Replace 'YourUserControl' with the actual name of your User Control
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
    }
}
