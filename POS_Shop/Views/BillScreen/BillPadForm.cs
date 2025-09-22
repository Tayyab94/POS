using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            // Apply highlighting to all textboxes and comboboxes
            foreach (Control control in this.Controls)
            {
                if (control is TextBox || control is ComboBox)
                {
                    control.Enter += Control_Enter;
                    control.Leave += Control_Leave;
                }
            }
            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Don't override Enter for ProductEnglishName
                if (this.ActiveControl == ProductEngNameTxt)
                    return; // let your ProductEngNameTxt_KeyPress logic run

                if (this.ActiveControl == CustomerNameTxt)
                    return; // let your CustomerNameTxt_KeyPress logic run

                if (this.ActiveControl == TopBarSearchProductTxt)
                    return;  // let your TopBarSearchProductTxt_KeyPress logic run

                e.SuppressKeyPress = true; // prevent ding

                // Move to next control
                this.SelectNextControl(
                    this.ActiveControl,
                    true,   // forward
                    true,   // tabStop only
                    true,   // include nested
                    true    // wrap around
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

        private void Control_Enter(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.LightYellow;
        }

        private void Control_Leave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
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
                if (row.Cells[1].Value != null &&
                    row.Cells[1].Value.ToString() == finalName)
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoadingManager.ShowLoading();

                e.Handled = true; // Prevents the default beep sound
                //MessageBox.Show($"Enter Pressed :{Keys.Enter}");
                Task.Delay(5000);

                OtherProductChk.Checked = false;
                var SearchProductUI = new SearchProductUI();
                SearchProductUI.ShowDialog();

                if (Convert.ToBoolean(SearchProductUI.FormCloseLbl.Text) == false)
                {
                    ProductEngNameTxt.Text = SearchProductUI.PNameLbl.Text;
                    prod_U_Name = SearchProductUI.PUNameLbl.Text;
                    PId = SearchProductUI.ProdIdLbl.Text;
                    ProductSalePrice.Text = SearchProductUI.ProdSalePriceLbl.Text;
                    P_StockQtyTxt.Text = "1";
                    var amt = decimal.Parse(ProductSalePrice.Text) * int.Parse(P_StockQtyTxt.Text);
                    ProductAmount.Text = Convert.ToString(amt);
                    productTypeDropdown.SelectedItem = !string.IsNullOrEmpty(SearchProductUI.PTypeLbl.Text) ? SearchProductUI.PTypeLbl.Text : productTypeDropdown.SelectedItem = "عدد";


                    ProductDetailTxt.Focus();
                }

            }
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
                InvoiceNumber = InvoiceNoLbl.Text,
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
            int leftMargin = 5;
            int currentY = 5;
            int lineHeight = 12;
            int sectionSpacing = 3;

            // Fonts for thermal printing
            Font titleFont = new Font("Arial", 11, FontStyle.Bold);
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
                    
                    e.Graphics.DrawString($"{row.Cells["Qty"].Value?.ToString()}", regularFont, Brushes.Black, qtyCol, currentY);
                    e.Graphics.DrawString(row.Cells["SalePrice"].Value?.ToString(), regularFont, Brushes.Black, priceCol, currentY);
                    e.Graphics.DrawString(row.Cells["Amount"].Value.ToString(), regularFont, Brushes.Black, totalCol, currentY);
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
            e.Graphics.DrawString(subtotal.ToString("0.0"), regularFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Tax (0%):", regularFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(taxAmount.ToString("0.0"), regularFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("TOTAL:", headerFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(total.ToString("0.0"), headerFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            currentY += lineHeight;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 6. PAYMENT INFORMATION
            e.Graphics.DrawString("Payment Method: CASH", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            decimal tendered = !string.IsNullOrEmpty(ReceivedAmountTxt.Text) ? decimal.Parse(ReceivedAmountTxt.Text) : decimal.Parse(TotalAmountLbl.Text);
            decimal change = tendered - total;

            e.Graphics.DrawString("Paid: " + tendered.ToString("0.00"), regularFont, Brushes.Black, leftMargin, currentY);
            e.Graphics.DrawString("Change: " + change.ToString("0.00"), regularFont, Brushes.Black, (totalsValueCol - 35), currentY);
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


        private void OrderPrintDocument_PrintPage_2(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Thermal printer settings (80mm paper)
            int paperWidth = 280;
            int leftMargin = 5;
            int currentY = 5;
            int lineHeight = 16;

            // Fonts
            Font titleFont = new Font("Nafees Web Naskh", 12, FontStyle.Bold);
            Font headerFont = new Font("Nafees Web Naskh", 10, FontStyle.Bold);
            Font regularFont = new Font("Nafees Web Naskh", 9, FontStyle.Regular);
            Font smallFont = new Font("Nafees Web Naskh", 8, FontStyle.Regular);

            // Alignment
            StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center };
            StringFormat rightFormat = new StringFormat() { Alignment = StringAlignment.Far };

            string dashLine = new string('-', 40);

            // 1. Header (Shop Name + Contact)
            if (!InvoiceShopName.Checked)
            {
                e.Graphics.DrawString("الیکٹرک شاپ", titleFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
                currentY += lineHeight * 2;

                e.Graphics.DrawString("رابطہ: 1234567", smallFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
                currentY += lineHeight * 2;
            }

            // 2. Invoice info
            e.Graphics.DrawString("انوائس", headerFont, Brushes.Black,
                                  new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight + 2;

            string cName = !string.IsNullOrEmpty(CustomerNameTxt.Text) ? CustomerNameTxt.Text : "";
            e.Graphics.DrawString("گاہک: " + cName, regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("تاریخ: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm"), regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("انوائس نمبر: " + InvoiceNoLbl.Text, regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            // 3. Table Layout
            int productCol = leftMargin;         // پروڈکٹ
            int productColWidth = 110;

            int qtyCol = productCol + productColWidth + 5; // مقدار
            int typeCol = qtyCol + 35;                     // قسم
            int priceCol = typeCol + 35;                   // قیمت
            int totalCol = priceCol + 45;                  // کل

            // Right-to-left Urdu layout
            StringFormat urduRightAlign = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };



            e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.Black, productCol, currentY);
            e.Graphics.DrawString("مقدار", headerFont, Brushes.Black, qtyCol, currentY);
            //e.Graphics.DrawString("قسم", headerFont, Brushes.Black, typeCol, currentY);
            e.Graphics.DrawString("قیمت", headerFont, Brushes.Black, priceCol, currentY);
            e.Graphics.DrawString("کل", headerFont, Brushes.Black, totalCol, currentY);

            currentY += lineHeight;
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + 40, currentY);
            currentY += 4;

            // ===== Table Rows =====
            decimal subtotal = 0m;
            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string product = row.Cells["Urdu Name"].Value?.ToString();
                    string qty = row.Cells["Qty"].Value?.ToString();
                    string type = row.Cells["ProductType"].Value?.ToString();
                    string price = row.Cells["SalePrice"].Value?.ToString();
                    string amount = row.Cells["Amount"].Value?.ToString();

                    // ✅ Product with word-wrap
                    RectangleF productRect = new RectangleF(productCol, currentY, productColWidth, lineHeight * 3);
                    e.Graphics.DrawString(product, regularFont, Brushes.Black, productRect);

                    SizeF productSize = e.Graphics.MeasureString(product, regularFont, productColWidth);
                    int productLines = (int)Math.Ceiling(productSize.Height / lineHeight);
                    int rowHeight = productLines * lineHeight;

                    // Other columns (single-line)
                    e.Graphics.DrawString($"{qty}-{type}", regularFont, Brushes.Black, qtyCol, currentY);
                    //e.Graphics.DrawString(qty, regularFont, Brushes.Black, qtyCol, currentY);
                    //e.Graphics.DrawString(type, regularFont, Brushes.Black, typeCol, currentY);
                    e.Graphics.DrawString(price, regularFont, Brushes.Black, priceCol, currentY);
                    e.Graphics.DrawString(amount, regularFont, Brushes.Black, totalCol, currentY);

                    if (decimal.TryParse(amount, out decimal amt))
                        subtotal += amt;

                    currentY += rowHeight;
                }
                e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + 40, currentY);
                currentY += lineHeight;
            }

            //// Final line
            //e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + 40, currentY);
            //currentY += lineHeight;

            // 4. Totals
            decimal taxAmount = 0m;
            decimal total = subtotal + taxAmount;

            e.Graphics.DrawString("ذیلی کل:", regularFont, Brushes.Black, priceCol, currentY);
            e.Graphics.DrawString(subtotal.ToString("0.0"), regularFont, Brushes.Black, totalCol + 30, currentY, rightFormat);
            currentY += lineHeight;

            e.Graphics.DrawString("ٹیکس 0:", regularFont, Brushes.Black, priceCol, currentY);
            e.Graphics.DrawString(taxAmount.ToString("0.0"), regularFont, Brushes.Black, totalCol + 30, currentY, rightFormat);
            currentY += lineHeight;

            e.Graphics.DrawString("کل:", headerFont, Brushes.Black, priceCol, currentY);
            e.Graphics.DrawString(total.ToString("0.0"), headerFont, Brushes.Black, totalCol + 30, currentY, rightFormat);
            currentY += lineHeight + 2;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            // 5. Payment
            e.Graphics.DrawString("ادائیگی کا طریقہ: نقد", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            decimal tendered = !string.IsNullOrEmpty(ReceivedAmountTxt.Text) ? decimal.Parse(ReceivedAmountTxt.Text) : subtotal;
            decimal change = tendered - total;

            e.Graphics.DrawString("ادا کیا گیا: " + tendered.ToString("0.0"), regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("بقایا رقم: " + change.ToString("0.0"), regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 6. Footer
            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("خریدا ہوا سامان واپس یا تبدیل نہیں ہوگا", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight;
        }


        private void OrderPrintDocument_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Thermal printer settings (80mm paper)
            int paperWidth = 280; // pixels for 80mm paper
            int leftMargin = 5;
            int currentY = 5;
            int lineHeight = 12;
            int sectionSpacing = 3;

            // Fonts for thermal printing
            Font titleFont = new Font("Arial", 11, FontStyle.Bold);
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
            int totalColWidth = 40;

            // Draw table headers
            e.Graphics.DrawString("Product", headerFont, Brushes.Black, productCol, currentY);
            e.Graphics.DrawString("Type", headerFont, Brushes.Black, typeCol, currentY);
            e.Graphics.DrawString("Qty", headerFont, Brushes.Black, qtyCol, currentY);
            e.Graphics.DrawString("Price", headerFont, Brushes.Black, priceCol, currentY);
            e.Graphics.DrawString("Total", headerFont, Brushes.Black, totalCol, currentY);

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
                    e.Graphics.DrawString(row.Cells["Qty"].Value?.ToString(), regularFont, Brushes.Black, qtyCol, currentY);
                    e.Graphics.DrawString(row.Cells["SalePrice"].Value?.ToString(), regularFont, Brushes.Black, priceCol, currentY);
                    e.Graphics.DrawString(row.Cells["Amount"].Value.ToString(), regularFont, Brushes.Black, totalCol, currentY);
                    currentY += lineHeight;


                    // Light separator line between items
                    e.Graphics.DrawLine(Pens.LightGray, leftMargin, currentY, totalCol + totalColWidth, currentY);
                    currentY += 2;
                }

            }

            // Bottom line of table
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + totalColWidth, currentY);
            currentY += lineHeight;

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
            e.Graphics.DrawString(subtotal.ToString("0.00"), regularFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Tax (0%):", regularFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(taxAmount.ToString("0.00"), regularFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("TOTAL:", headerFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(total.ToString("0.00"), headerFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            currentY += lineHeight;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 6. PAYMENT INFORMATION
            e.Graphics.DrawString("Payment Method: CASH", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            decimal tendered = !string.IsNullOrEmpty(ReceivedAmountTxt.Text) ? decimal.Parse(ReceivedAmountTxt.Text) : decimal.Parse(TotalAmountLbl.Text);
            decimal change = tendered - total;

            e.Graphics.DrawString("Paid: " + tendered.ToString("0.00"), regularFont, Brushes.Black, leftMargin, currentY);
            e.Graphics.DrawString("Change: " + change.ToString("0.00"), regularFont, Brushes.Black, (totalsValueCol - 35), currentY);
            currentY += lineHeight + 2;

            // 7. FOOTER
            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("خریدا ہوا سامان واپس یا تبدیل نہیں ہوگا", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight;

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




        //private void InvoiceShopName_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (InvoiceShopName.Checked)
        //    {
        //        InvoiceShopName.Checked = false;
        //        InvoiceShopName.Text = "Hide Shop Name is Invoice";
        //    }
        //    else
        //    {
        //        InvoiceShopName.Checked = true;
        //        InvoiceShopName.Text = "Show Shop Name is Invoice";
        //    }
        //}
    }
}
