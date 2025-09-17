using POS_Shop.Helpers;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            CartProductList.ColumnCount = 6;

            CartProductList.Columns[0].Name = "ProductId";
            //CartProductList.Columns[1].Name = "ProductName";
            CartProductList.Columns[1].Name = "Urdu Name";
            CartProductList.Columns[2].Name = "ProductType";
            CartProductList.Columns[3].Name = "SalePrice";
            CartProductList.Columns[4].Name = "Qty";
            CartProductList.Columns[5].Name = "Amount";

            // Set column widths here
            CartProductList.Columns[0].Width = 50;
            CartProductList.Columns[1].Width = 190;
            CartProductList.Columns[2].Width = 70;
            CartProductList.Columns[3].Width = 50;
            CartProductList.Columns[4].Width = 60;
            CartProductList.Columns[5].Width = 100;

            // Add delete button column
            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.Name = "Delete";
            btnCol.HeaderText = "Action";
            btnCol.Text = "Delete";
            btnCol.UseColumnTextForButtonValue = true;  // Always show "Delete"
            CartProductList.Columns.Add(btnCol);

            // Set the width of the button column
            CartProductList.Columns["Delete"].Width = 50;


            InvoiceNoLbl.Text = DateTime.Now.ToString("MMddyyy-HHmmss");

            //ProductEngNameTxt.Focus();

            this.Shown += (s, e) => { ProductEngNameTxt.Focus(); };

            // Apply highlighting to all textboxes and comboboxes
            foreach (Control control in this.Controls)
            {
                if (control is TextBox || control is ComboBox)
                {
                    control.Enter += Control_Enter;
                    control.Leave += Control_Leave;
                }
            }
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
            decimal salePrice = decimal.Parse(ProductSalePrice.Text);
            int qty = int.Parse(P_StockQtyTxt.Text);
            decimal amount = salePrice * qty;

            bool productExists = false;
            var finalName = OtherProductChk.Checked == false ? $"{ProductUrduName} {ProductDetailTxt.Text}" : productName;
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
                    row.Cells["Amount"].Value = newAmount;

                    productExists = true;
                    break;
                }
            }
            //}


            // If product doesn’t exist, add a new row
            if (!productExists)
            {
                CartProductList.Rows.Add(finalPId, finalName, productType, salePrice, qty, amount);
            }

            CalculateTotals();

            // Clear input fields after adding
            ClearInputs();
            ProductEngNameTxt.Focus();
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
                    productTypeDropdown.SelectedItem = SearchProductUI.PTypeLbl.Text;

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
        }

        private void ClearCartBtn_Click(object sender, EventArgs e)
        {
            //PId = string.Empty;
            //customerId = string.Empty;
            //CustomerNameTxt.Text = string.Empty;
            //CartProductList.Rows.Clear();

            //// Also update the totals to zero
            //TotalItemLbl.Text = "0";
            //TotalAmountLbl.Text = "Rs 0.00";

            ClearCartFunction();

            // Optional: Show confirmation message
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
        }

        private async void SaveAndPrintOrderBtn_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text) && PreviousOrderIdLbl.Text != "Prev Order Id")
            {
                // update the ordear
            }
            else
            {
                NewOrderSaved();
                ClearInputs();
                ClearCartFunction();
                ResetCustomerBtn.Visible = false;
                InvoiceNoLbl.Text = DateTime.Now.ToString("MMddyyy-HHmmss");
            }

        }

        private async void NewOrderSaved()
        {
            using (var context = new POSDbContext())
            {

                using (var dbTransaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        var orderRepository = new OrderRepository(context);

                        int? customerId = null;

                        if (!string.IsNullOrEmpty(CustomerNameTxt.Text) && !string.IsNullOrEmpty(CustomerIdLbl.Text))
                        {
                            if (int.TryParse(CustomerIdLbl.Text, out int parsedId))
                            {
                                customerId = parsedId;
                            }
                            else
                            {
                                // Handle parsing error
                                customerId = null;
                            }
                        }
                        else
                        {
                            customerId = null;
                        }

                        float totalBill;
                        if (!float.TryParse(TotalAmountLbl.Text, out totalBill))
                        {
                            totalBill = 0; // or handle error
                        }

                        float receiveAmount;
                        if (!string.IsNullOrWhiteSpace(ReceivedAmountTxt.Text))
                        {
                            if (!float.TryParse(ReceivedAmountTxt.Text, out receiveAmount))
                                receiveAmount = totalBill; // fallback
                        }
                        else
                        {
                            receiveAmount = totalBill;
                        }
                        // Create new order
                        var order = new Order
                        {
                            TotalBill = totalBill,
                            ReceiveAmount = receiveAmount,
                            CreatedDate = DateTime.Now,
                            InvoiceNumber = InvoiceNoLbl.Text,
                            paymentType = CashRadioBtn.Checked ? "Cash" : "Bank Transfer",
                            customerId = customerId
                        };

                        var orderId = await orderRepository.AddOrder(order);

                        var orderDetailList = new List<OrderDetail>();

                        foreach (DataGridViewRow row in CartProductList.Rows)
                        {


                            if (row.Cells[0].Value != null) // Check if row has data
                            {
                                var odrDetail = new OrderDetail
                                {
                                    ProductId = string.IsNullOrEmpty(row.Cells[0].Value?.ToString()) ?
                                               (int?)null : int.Parse(row.Cells[0].Value.ToString()),
                                    OtherProductName = string.IsNullOrEmpty(row.Cells[0].Value?.ToString()) ?
                                                     row.Cells[1].Value?.ToString() : null,
                                    Quantity = int.Parse(row.Cells[4].Value?.ToString()),
                                    QuantityType = row.Cells[2].Value?.ToString(),
                                    Price = float.Parse(row.Cells[3].Value?.ToString()),
                                    CreatedDate = DateTime.Now,
                                    OrderId = orderId,
                                };
                                orderDetailList.Add(odrDetail);
                            }
                        }

                        context.OrderDetails.AddRange(orderDetailList);
                        await context.SaveChangesAsync();

                        dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();

                    }
                }
            }
        }

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
            }
        }

        //private async void SaveAndPrintOrderBtn_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(PreviousOrderIdLbl.Text))
        //    {
        //        // update the ordear
        //    }else
        //    {
        //        await NewOrderSaved()l
        //    }
        //}


        //private void bunifuButton2_Click(object sender, EventArgs e)
        //{
        //    using(var context = new POSDbContext())
        //    {
        //       using(var dbTransaction = context.Database.BeginTransaction())
        //        {


        //        }
        //    }
        //}
    }
}
