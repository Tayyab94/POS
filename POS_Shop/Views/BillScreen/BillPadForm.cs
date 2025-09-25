using POS_Shop.DTOs.Product;
using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using Order = POS_Shop.Models.Order;

namespace POS_Shop.Views.BillScreen
{
    public partial class BillPadForm : Form
    {

        string PId { get; set; }
        string customerId { get; set; }
        public string prod_U_Name { get; set; }

        public BillPadForm()
        {
            InitializeComponent();
            InvoiceNoLbl.Text = DateTime.Now.ToString("MMddyyy-HHmmss");
            this.Shown += (s, e) => { ProductEngNameTxt.Focus(); };

            SetItemGrdiView();
           
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
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right )
            {

                if (SuggestionGrid.Visible && this.ActiveControl == SuggestionGrid)
                    return;

                // Don't override Enter for these controls
                if (this.ActiveControl == ProductEngNameTxt ||
                    this.ActiveControl == CustomerNameTxt ||
                    this.ActiveControl == TopBarSearchProductTxt)
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
                    this.ActiveControl == TopBarSearchProductTxt)
                    return;

                e.SuppressKeyPress = true;

                this.SelectNextControl(
                    this.ActiveControl,
                    false, true, true, true
                );
            }
        }

        private void SetItemGrdiView()
        {

            CartProductList.ColumnCount = 6;

            CartProductList.Columns[0].Name = "Amount";
            CartProductList.Columns[1].Name = "SalePrice";
            CartProductList.Columns[2].Name = "Urdu Name";
            CartProductList.Columns[3].Name = "ProductType";
            CartProductList.Columns[4].Name = "Qty";
            CartProductList.Columns[5].Name = "ProductId";

            // Set column widths here

            CartProductList.Columns[0].Width = 100;
            CartProductList.Columns[1].Width = 60;
            CartProductList.Columns[2].Width = 190;
            CartProductList.Columns[3].Width = 30;
            CartProductList.Columns[4].Width = 50;
            CartProductList.Columns[5].Width = 50;

            CartProductList.Columns[5].Visible = false;


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
            if (!decimal.TryParse(ProductSalePrice.Text, out decimal salePrice) || salePrice <= 0)
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

                var finalPId = OtherProductChk.Checked == false ? productId : "";
                //if (!OtherProductChk.Checked)
                //{
                // Loop through DataGridView rows to check if product already exists
                foreach (DataGridViewRow row in CartProductList.Rows)
                {
                    if (row.Cells["Urdu Name"].Value != null &&
                        row.Cells["Urdu Name"].Value.ToString() == finalName)
                    {
                        // Product already exists → increase Qty & update Amount
                        int existingQty = Convert.ToInt32(row.Cells["Qty"].Value);
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
                    CartProductList.Rows.Add(null, amount, salePrice, finalName, productType, qty, finalPId);
                }

                CalculateTotals();

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
            ResetCustomerBtn.Visible = false;

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
            //        productTypeDropdown.SelectedItem = !string.IsNullOrEmpty(SearchProductUI.PTypeLbl.Text) ? SearchProductUI.PTypeLbl.Text : productTypeDropdown.SelectedItem = "عدد";


            //        ProductDetailTxt.Focus();
            //    }

            //}

            #endregion


            if (e.KeyChar == (char)Keys.Enter)
            {
                ShowSuggestions(ProductEngNameTxt.Text);

                e.Handled = true;

            }
        }

        private void ProductEngNameTxt_TextChange(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProductEngNameTxt.Text) || ProductEngNameTxt.Text.Length < 2)
            {
                SuggestionGrid.Visible = false;
                return;
            }

            ShowSuggestions(ProductEngNameTxt.Text);
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

        private void ShowSuggestions(string searchText)
        {
            try
            {
                // Get suggestions from your data source
                var suggestions = GetProductSuggestions(searchText);

                if (suggestions.Any())
                {
                   
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("U-Name", typeof(string));
                    //dt.Columns.Add("P-Price", typeof(string));

                    //dt.Columns.Add("C-p", typeof(int));
                    dt.Columns.Add("Type", typeof(string));
                    dt.Columns.Add("S-P", typeof(string));


                    foreach (var item in suggestions)
                    {
                        dt.Rows.Add(item.ProductId, item.ProductName, item.ProductUrduName, item.ProductType, item.Price);
                    }

                    SuggestionGrid.ReadOnly = true;
                    SuggestionGrid.AllowUserToAddRows = false;
                    SuggestionGrid.DataSource = dt;

                    SuggestionGrid.Columns[0].Width = 40;
                    SuggestionGrid.Columns[1].Width = 190;
                    SuggestionGrid.Columns[2].Width = 190;
                    SuggestionGrid.Columns[3].Width = 40;
                    SuggestionGrid.Columns[4].Width = 50;
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
                        data = data.Where(s => s.ProductEnglishName.Contains(word) || s.Id.ToString().Contains(word));
                        //data = data.Where(s => s.CustomerName.Contains(word) || s.City.Name.Contains(word));
                    }
                }

                var result = data.OrderBy(s => s.Id).Select(s=>new ProductSuggestion()
                {
                    ProductId = s.Id,
                    ProductName = s.ProductEnglishName,
                    ProductUrduName= s.ProductUrduName,
                    ProductType= s.ProductType,
                    Price = s.SalePrice.HasValue ? s.SalePrice.Value : 0
                }).Take(40).ToList();

                return result;
            };
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
                }
            }
        }

        private void CustomerNameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                e.Handled = true; // Prevents the default beep sound

                OtherProductChk.Checked = false;
                var SearchCustomerUI = new SearchCustomerUI();
                SearchCustomerUI.ShowDialog();

                if (Convert.ToBoolean(SearchCustomerUI.FormCloseLbl.Text) == false)
                {
                    CustomerNameTxt.Text = SearchCustomerUI.CustomerName.Text;
                    customerId = SearchCustomerUI.CustomerIdLbl.Text;
                    CustomerIdLbl.Text = customerId;
                    this.ResetCustomerBtn.Visible = true;

                    ProductEngNameTxt.Focus();
                }

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

        private void ReceivedAmountTxt_TextChange(object sender, EventArgs e)
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


            if(!string.IsNullOrEmpty(TotalAmountLbl.Text) && TotalAmountLbl.Text != "0")
            {
                // Calculate remaining amount
                decimal totalAmount = Convert.ToDecimal(TotalAmountLbl.Text); // Your total amount

                if (string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
                {
                    lblRemainingAmount.Text = $"Remaining: Rs. {totalAmount}";
                    return;
                }

                if (decimal.TryParse(ReceivedAmountTxt.Text, out decimal receivedAmount))
                {
                    decimal remainingAmount = totalAmount - receivedAmount;
                    lblRemainingAmount.Text = remainingAmount >= 0
                        ? $"Remaining:  Rs. {remainingAmount}"
                        : $"Exceeded by:  Rs. {Math.Abs(remainingAmount)}";
                    lblRemainingAmount.ForeColor = remainingAmount >= 0 ? Color.Black : Color.Red;
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
            InvoiceNoLbl.Text = DateTime.Now.ToString("MMddyyy-HHmmss");
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

            // Also update the totals to zero
            TotalItemLbl.Text = "0";
            TotalAmountLbl.Text = "0.00";
            ReceivedAmountTxt.Clear();


        }

        private void TopBarSearchProductTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

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

            InvoiceNoLbl.Text = FormCtrl.InvoiceNoLbl.Text;
            PreviousOrderIdLbl.Text = FormCtrl.OrderIDLbl.Text;

        }


        private async void PreviousOrderIdLbl_TextChanged(object sender, EventArgs e)
        {
            if ((PreviousOrderIdLbl.Text != "OrderID" && InvoiceNoLbl.Text != "InvoiceNo") && (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text) && !string.IsNullOrEmpty(InvoiceNoLbl.Text)))
            {
                using (var context = new POSDbContext())
                {
                    var orderRepo = new OrderRepository(context);
                    var resut = await orderRepo.GetOrderByIdAsync(Convert.ToInt32(PreviousOrderIdLbl.Text), InvoiceNoLbl.Text);
                    if (resut != null)
                    {

                        CustomerIdLbl.Text = resut.CustomerId.HasValue ? resut.CustomerId.Value.ToString() : string.Empty;
                        CustomerNameTxt.Text = string.IsNullOrEmpty(CustomerIdLbl.Text) ? "" : resut.CustomerName;
                        TotalAmountLbl.Text = resut.TotalBill.ToString();
                        if (resut.paymentType == "Cash")
                        {
                            CashRadioBtn.Checked = true;
                            BankTransferRaadioBtn.Checked = false;
                        }
                        else
                        {
                            CashRadioBtn.Checked = false;
                            BankTransferRaadioBtn.Checked = true;
                        }

                        if(resut.OrderDetailsList.Count > 0)
                            CartProductList.Rows.Clear();

                        foreach (var order in resut.OrderDetailsList)
                        {
                            // Get values from the TextBoxes
                            string productId = order.ProductId.ToString() ?? "0"; // (or use the label SearchProductUI.ProdIdLbl.Text)
                            string finalName = order.ProductName;
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
                    OrderPrintPreviewDialog.Document = OrderPrintDocument;

                    OrderPrintDocument.Print();
                    ClearInputs();
                    ClearCartFunction();
                    ResetCustomerBtn.Visible = false;
                    InvoiceNoLbl.Text = DateTime.Now.ToString("MMddyyy-HHmmss");
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
                InvoiceNumber = !string.IsNullOrEmpty(InvoiceNoLbl.Text)? InvoiceNoLbl.Text: DateTime.Now.ToString("MMddyyy-HHmmss"),
                paymentType = CashRadioBtn.Checked ? "Cash" : "Bank Transfer",
                customerId = customerId
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
                };
                orderDetailList.Add(odrDetail);
            }

            context.OrderDetails.AddRange(orderDetailList);
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


                    MessageBox.Show("Uou Records has been Delete",
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
                //SendKeys.SendWait("^{F11}");
                OrderPrintPreviewDialog.Document = OrderPrintDocument;
                OrderPrintPreviewDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Add the Product first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OrderPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Thermal printer settings (80mm paper)
            int paperWidth = 280; // pixels for 80mm paper
            int leftMargin = 0;
            int currentY = 5;
            int lineHeight = 12;
            int sectionSpacing = 3;

            // Fonts for thermal printing
            System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
            Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            Font regularFont = new Font("Arial", 8, FontStyle.Regular);
            Font smallFont = new Font("Arial", 7, FontStyle.Regular);

            // Urdu font
            Font urduFont = new Font("Nafees Web Naskh", 9, FontStyle.Regular);
            if (urduFont.Name != "Nafees Web Naskh")
                urduFont = new Font("Arial", 8, FontStyle.Regular);

            // Center alignment
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;

            // Right alignment for Urdu (right-to-left)
            StringFormat rightFormat = new StringFormat();
            rightFormat.Alignment = StringAlignment.Far;
            rightFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Left alignment for English text
            StringFormat leftFormat = new StringFormat();
            leftFormat.Alignment = StringAlignment.Near;

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

            // 2. INVOICE INFO - Mixed Urdu and English
            e.Graphics.DrawString("انوائس", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;

            string cName = !string.IsNullOrEmpty(CustomerNameTxt.Text) ? CustomerNameTxt.Text : "";
            e.Graphics.DrawString($"کسٹمر: {cName}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;

            e.Graphics.DrawString("تاریخ: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;

            e.Graphics.DrawString("انوائس نمبر:" + InvoiceNoLbl.Text, urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight + 2;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 3. TABLE LAYOUT - URDU COLUMN HEADERS (RIGHT-TO-LEFT ORDER)
            // پروڈکٹ on RIGHT side, then قسم, مقدار, قیمت, کل on LEFT side
            int col1 = leftMargin;                    // کل (Total) - FAR RIGHT in RTL
            int col1Width = 40;

            int col2 = col1 + col1Width + 5;          // قیمت (Price)
            int col2Width = 40;

            int col3 = col2 + col2Width + 5;          // مقدار (Quantity)
            int col3Width = 40;

            int col4 = col3 + col3Width + 5;          // قسم (Type)
            int col4Width = 40;

            int productCol = col4 + col4Width + 5;    // پروڈکٹ (Product name) - FAR LEFT in RTL
            int productColWidth = paperWidth - productCol - 5;

            // Draw Urdu table headers - Right aligned (RTL order)
            e.Graphics.DrawString("کل", headerFont, Brushes.Black,
                                 new Rectangle(col1, currentY, col1Width, lineHeight), rightFormat);
            e.Graphics.DrawString("قیمت", headerFont, Brushes.Black,
                                 new Rectangle(col2, currentY, col2Width, lineHeight), rightFormat);
            e.Graphics.DrawString("مقدار", headerFont, Brushes.Black,
                                 new Rectangle(col3, currentY, col3Width, lineHeight), rightFormat);
            e.Graphics.DrawString("قسم", headerFont, Brushes.Black,
                                 new Rectangle(col4, currentY, col4Width, lineHeight), rightFormat);
            e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.Black,
                                 new Rectangle(productCol, currentY, productColWidth, lineHeight), rightFormat);

            currentY += lineHeight;
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
            currentY += 5;

            // 4. TABLE ROWS - PRODUCT NAME ON FIRST ROW, DETAILS ON SECOND ROW (RTL ORDER)
            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells[0].Value != null) // Check if row has data
                {
                    // Extract values with null checking
                    decimal amount = row.Cells["Amount"]?.Value != null ? Convert.ToDecimal(row.Cells["Amount"].Value) : 0;
                    decimal salePrice = row.Cells["SalePrice"]?.Value != null ? Convert.ToDecimal(row.Cells["SalePrice"].Value) : 0;
                    decimal qty = row.Cells["Qty"]?.Value != null ? Convert.ToDecimal(row.Cells["Qty"].Value) : 0;
                    string productType = row.Cells["ProductType"]?.Value?.ToString() ?? "";
                    string urduName = row.Cells["Urdu Name"]?.Value?.ToString() ?? "";

                    // FIRST ROW: Only Product Name on the RIGHT side (پروڈکٹ column)
                    //e.Graphics.DrawString(urduName, urduFont, Brushes.Black,
                    //                     new Rectangle(productCol, currentY, productColWidth, lineHeight), rightFormat);

                    RectangleF productRect = new RectangleF(productCol, currentY, productColWidth, lineHeight * 3);
                    e.Graphics.DrawString(urduName, urduFont, Brushes.Black, productRect);

                    SizeF productSize = e.Graphics.MeasureString(urduName, urduFont, productColWidth);
                    int productLines = (int)Math.Ceiling(productSize.Height / lineHeight);
                    int rowHeight = productLines * lineHeight;
                    currentY += lineHeight;

                    // SECOND ROW: Details (Type, Quantity, Price, Total) - RTL order
                    e.Graphics.DrawString($"{amount:0}", regularFont, Brushes.Black,
                                         new Rectangle(col1, currentY, col1Width, lineHeight), leftFormat);
                    e.Graphics.DrawString($"{salePrice:0}", regularFont, Brushes.Black,
                                         new Rectangle(col2, currentY, col2Width, lineHeight), leftFormat);
                    e.Graphics.DrawString($"{qty:0}", regularFont, Brushes.Black,
                                         new Rectangle(col3, currentY, col3Width, lineHeight), leftFormat);
                    e.Graphics.DrawString(productType, urduFont, Brushes.Black,
                                         new Rectangle(col4, currentY, col4Width, lineHeight), rightFormat);

                    // Leave product column empty on second row since we used it in first row
                    e.Graphics.DrawString("", regularFont, Brushes.Black,
                                         new Rectangle(productCol, currentY, productColWidth, lineHeight), rightFormat);

                    currentY += lineHeight;
                    e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
                    currentY += 5; // Extra spacing between products
                }
            }

            currentY += sectionSpacing;

            // 5. TOTALS SECTION - Urdu labels
            decimal subtotal = decimal.Parse(TotalAmountLbl.Text);
            decimal taxAmount = 0m; // 0% tax
            decimal total = subtotal + taxAmount;

            // Totals section with Urdu labels
            e.Graphics.DrawString($"سب ٹوٹل: {subtotal:0}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;

            e.Graphics.DrawString($"ٹیکس (0%): {taxAmount:0}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;

            e.Graphics.DrawString($"کل رقم: {total:0}", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;

            currentY += lineHeight;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 6. PAYMENT INFORMATION - Urdu
            e.Graphics.DrawString("ادائیگی کا طریقہ: نقد", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;

            decimal tendered = !string.IsNullOrEmpty(ReceivedAmountTxt.Text) ? decimal.Parse(ReceivedAmountTxt.Text) : decimal.Parse(TotalAmountLbl.Text);
            decimal change = tendered - total;

            e.Graphics.DrawString($"ادا کی گئی رقم: {tendered:0}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;

            e.Graphics.DrawString($"واپسی: {change:0}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight + 2;

            // 7. URDU FOOTER TEXT
            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            string footerText1 = "خریدا ہوا سامان واپس یا تبدیل نہیں ہوگا۔";
            string footerText2 = "چائنہ مال کی وارنٹی نہیں۔";

            e.Graphics.DrawString(footerText1, headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;

            e.Graphics.DrawString(footerText2, headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
            currentY += lineHeight;
        }

        private void OrderPrintDocument_PrintPage_12_PreviousUsingCode(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

            // Draw table headers
            e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.Black, productCol, currentY);
            e.Graphics.DrawString(" ", headerFont, Brushes.Black, typeCol, currentY);
            e.Graphics.DrawString("مقدار", headerFont, Brushes.Black, qtyCol, currentY);
            e.Graphics.DrawString("قیمت", headerFont, Brushes.Black, priceCol, currentY);
            e.Graphics.DrawString("کل", headerFont, Brushes.Black, totalCol, currentY);

            currentY += lineHeight;
            currentY += 3;
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + totalColWidth, currentY);
            currentY += 5;

            foreach (DataGridViewRow row in CartProductList.Rows)
            {


                if (row.Cells[0].Value != null) // Check if row has data
                {
                    // First line: Product name only (left aligned)
                    e.Graphics.DrawString(row.Cells["Urdu Name"].Value?.ToString(), regularFont, Brushes.Black, productCol, currentY);
                    currentY += lineHeight;

                    // Second line: Type, Qty, Price, Total (in columns)
                   e.Graphics.DrawString(row.Cells["ProductType"].Value?.ToString(), urduFont, Brushes.Black, typeCol, currentY);
                    
                    //e.Graphics.DrawString($"{row.Cells["Qty"].Value?.ToString()}", regularFont, Brushes.Black, qtyCol, currentY);
                    //e.Graphics.DrawString(row.Cells["SalePrice"].Value?.ToString(), regularFont, Brushes.Black, priceCol, currentY);
                    //e.Graphics.DrawString(row.Cells["Amount"].Value.ToString(), regularFont, Brushes.Black, totalCol, currentY);

                    e.Graphics.DrawString($"{Convert.ToDecimal(row.Cells["Qty"].Value):0}", regularFont, Brushes.Black, qtyCol, currentY);
                    e.Graphics.DrawString($"{Convert.ToDecimal(row.Cells["SalePrice"].Value):0}", regularFont, Brushes.Black, priceCol, currentY);
                    e.Graphics.DrawString($"{Convert.ToDecimal(row.Cells["Amount"].Value):0}", regularFont, Brushes.Black, totalCol, currentY);

                    currentY += lineHeight;

                    //e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
                    //currentY += lineHeight + 2;
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

            e.Graphics.DrawString("Paid: " +$"{Convert.ToDecimal(tendered):0}", regularFont, Brushes.Black, leftMargin, currentY);
            e.Graphics.DrawString("Change: " + $"{Convert.ToDecimal(change):0}", regularFont, Brushes.Black, (totalsValueCol - 35), currentY);
            currentY += lineHeight + 2;

            // 7. FOOTER
            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("خریدا ہوا سامان واپس یا تبدیل نہیں ہوگا۔", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight;

            e.Graphics.DrawString("چائنہ مال کی وارنٹی نہیں۔", headerFont, Brushes.Black,
                               new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight +2;
          
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

            if(e.KeyCode == Keys.S && e.Control) // Ctrl + S to Save and Print
            {
                SaveAndPrintOrderBtn.PerformClick();
            }
            else if(e.KeyCode == Keys.P && e.Control) // Ctrl + P to Print Preview
            {
                PrintPreviewBtn.PerformClick();
            }
            else if(e.KeyCode == Keys.N && e.Control) // Ctrl + N to New Invoice
            {
                ClearCartBtn.PerformClick();
            }
            else if(e.KeyCode == Keys.Escape) // Esc to Clear Cart
            {
                ClearCartBtn.PerformClick();
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

                    SuggestionGrid.Visible= false;
                    e.Handled = true;
                }
            }
            else if(e.KeyCode == Keys.Left)
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
                    ProductEngNameTxt.Text = (string)SuggestionGrid.CurrentRow.Cells[1].Value;
                    prod_U_Name = (string)SuggestionGrid.CurrentRow.Cells[2].Value;
                    ////PTypeLbl.Text = (string)ProductListGrid.CurrentRow.Cells[3].Value;
                    //productTypeDropdown.SelectedItem = SuggestionGrid.CurrentRow.Cells[3].Value == null
                    //            || SuggestionGrid.CurrentRow.Cells[3].Value == DBNull.Value
                    //            ? "عدد"
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
                        ProductEngNameTxt.Text = (string)foundRow.Cells[1].Value;
                        prod_U_Name = (string)foundRow.Cells[2].Value;
                        productTypeDropdown.SelectedItem = foundRow.Cells[3].Value == null
                                    || foundRow.Cells[3].Value == DBNull.Value
                                    ? "عدد"
                                    : foundRow.Cells[3].Value.ToString();

                        ProductSalePrice.Text = foundRow.Cells[4].Value == null
                            || foundRow.Cells[4].Value == DBNull.Value
                            ? string.Empty
                            : foundRow.Cells[4].Value.ToString();
                        PId = pId.ToString();
                        P_StockQtyTxt.Text = "1";
                        SuggestionGrid.Visible = false;

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
                ProductEngNameTxt.Text = (string)SuggestionGrid.CurrentRow.Cells[1].Value;
                prod_U_Name = (string)SuggestionGrid.CurrentRow.Cells[2].Value;
                productTypeDropdown.SelectedItem = SuggestionGrid.CurrentRow.Cells[3].Value == null
                            || SuggestionGrid.CurrentRow.Cells[3].Value == DBNull.Value
                            ? "عدد"
                            : SuggestionGrid.CurrentRow.Cells[3].Value.ToString();
                ProductSalePrice.Text = SuggestionGrid.CurrentRow.Cells[4].Value == null
                    || SuggestionGrid.CurrentRow.Cells[4].Value == DBNull.Value
                    ? string.Empty
                    : SuggestionGrid.CurrentRow.Cells[4].Value.ToString();
                PId = pId.ToString();

                P_StockQtyTxt.Text = "1";
                SuggestionGrid.Visible = false;
                ProductDetailTxt.Focus();
            }
        }

    }
}
