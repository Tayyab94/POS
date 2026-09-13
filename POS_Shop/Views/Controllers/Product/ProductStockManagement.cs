using Org.BouncyCastle.Asn1.Cmp;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Product
{
    public partial class ProductStockManagement : Form
    {
        private readonly POSDbContext dbContext;
        private POS_Shop.Models.Product currentProduct;
        private readonly int productId;
        private readonly string productName;

        // Constructor that receives Product ID and Name
        public ProductStockManagement(int productId, string productName)
        {
            InitializeComponent();
            this.productId = productId;
            this.productName = productName;
            dbContext = new POSDbContext();

            InitializeForm();
            LoadProductDetails();
        }

        private void InitializeForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Display product name in the form title
            this.Text = $"Product Stock Management - {productName}";

            // Set default values
            txtNewQty.Text = "0";
            lblStatus.Text = "Ready";
            lblStatus.ForeColor = SystemColors.ControlText;
            lblProductName.Text = productName;
        }

        #region Event Handlers

        private void txtNewQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers and control keys
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNewQty_TextChanged(object sender, EventArgs e)
        {
            if (currentProduct != null && decimal.TryParse(txtNewQty.Text, out decimal newQty))
            {
                UpdateCalculatedQuantity(newQty);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (currentProduct == null)
            {
                ShowMessage("Product not loaded", MessageType.Warning);
                return;
            }

            if (!ValidateQuantityInput())
                return;

            if (ShowConfirmationDialog($"Are you sure you want to add {txtNewQty.Text} {currentProduct.ProdQtyStockUnit}(s) to {currentProduct.ProductEnglishName}?") == DialogResult.Yes)
            {
                UpdateProductQuantity();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProductDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Core Functionality with Entity Framework

        private void LoadProductDetails()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblStatus.Text = "Loading product details...";
                lblStatus.ForeColor = SystemColors.ControlText;

                // Using Entity Framework to load product
                currentProduct = dbContext.Products.AsNoTracking()
                    .FirstOrDefault(p => p.Id == productId);

                if (currentProduct != null)
                {
                    DisplayProductDetails();
                   // ShowMessage($"Product '{currentProduct.ProductEnglishName}' loaded successfully", MessageType.Success);
                    lblStatus.Text = "Product loaded";
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    ShowMessage($"Product with ID {productId} not found", MessageType.Error);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading product: {ex.Message}", MessageType.Error);
                LogError(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (currentProduct == null)
                {
                    lblStatus.Text = "Failed to load";
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }

        private void DisplayProductDetails()
        {
            if (currentProduct == null) return;

            // Display in the table format as shown in your image
            lblIdValue.Text = currentProduct.Id.ToString();
            lblStockUnitValue.Text = currentProduct.ProdQtyStockUnit ?? "Piece";
            lblPreQtyValue.Text = currentProduct.Qty.ToString("N0");

            // NewQty row - editable textbox
            txtNewQty.Text = "0";

            // Label showing calculation
            UpdateCalculatedQuantity(0);
        }

        private void UpdateCalculatedQuantity(decimal newQty)
        {
            if (currentProduct != null)
            {
                decimal preQty = currentProduct.Qty;
                decimal totalQty = preQty + newQty;
                // Format: "10 + 7 = 17" as shown in your image
                lblNewQty.Text = $"{preQty:N0} + {newQty:N0} = {totalQty:N0}";
                lblCalculatedTotal.Text = totalQty.ToString("N0");
            }
        }

        private void UpdateProductQuantity()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                decimal newQty = decimal.Parse(txtNewQty.Text);
                int totalNewQty = (int)(currentProduct.Qty + newQty);

                // Using Entity Framework with transaction
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Reload the product to ensure we have the latest data
                        var productToUpdate = dbContext.Products.Find(currentProduct.Id);

                        if (productToUpdate == null)
                        {
                            ShowMessage("Product not found in database", MessageType.Error);
                            return;
                        }

                        // Store old quantity for logging
                        int oldQty = productToUpdate.Qty;

                        // Update the quantity
                        productToUpdate.Qty = totalNewQty;

                        // Save changes
                        dbContext.SaveChanges();

                        
                        // Commit transaction
                        transaction.Commit();

                        // Update local object
                        currentProduct.Qty = totalNewQty;

                        ShowMessage($"Quantity updated successfully! New stock: {totalNewQty} {currentProduct.ProdQtyStockUnit}(s)", MessageType.Success);
                        lblStatus.Text = "Update successful";
                        lblStatus.ForeColor = Color.Green;

                        // Refresh display
                        DisplayProductDetails();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error updating quantity: {ex.Message}", MessageType.Error);
                LogError(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region Validation

        private bool ValidateQuantityInput()
        {
            if (string.IsNullOrWhiteSpace(txtNewQty.Text))
            {
                ShowMessage("Please enter a quantity", MessageType.Warning);
                txtNewQty.Focus();
                return false;
            }

            if (!decimal.TryParse(txtNewQty.Text, out decimal newQty))
            {
                ShowMessage("Please enter a valid number", MessageType.Warning);
                txtNewQty.SelectAll();
                txtNewQty.Focus();
                return false;
            }

            if (newQty < 0)
            {
                ShowMessage("Quantity cannot be negative", MessageType.Warning);
                txtNewQty.SelectAll();
                txtNewQty.Focus();
                return false;
            }

            if (newQty == 0)
            {
                if (ShowConfirmationDialog("You are adding zero quantity. Are you sure?") != DialogResult.Yes)
                {
                    txtNewQty.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region UI Helpers

        private void ClearForm()
        {
            txtNewQty.Text = "0";
            lblIdValue.Text = "-";
            lblStockUnitValue.Text = "-";
            lblPreQtyValue.Text = "0";
            lblNewQty.Text = "0 + 0 = 0";
            lblCalculatedTotal.Text = "0";
            currentProduct = null;
            lblStatus.Text = "Ready";
            lblStatus.ForeColor = SystemColors.ControlText;
        }

        private void ShowMessage(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.Success:
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case MessageType.Warning:
                    MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case MessageType.Error:
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private DialogResult ShowConfirmationDialog(string message)
        {
            return MessageBox.Show(message, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void LogError(Exception ex)
        {
            // Implement your logging mechanism here
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
        }

        #endregion

        #region Cleanup

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Dispose DbContext to free resources
            dbContext?.Dispose();
        }

        #endregion

        #region Enums

        private enum MessageType
        {
            Information,
            Success,
            Warning,
            Error
        }

        #endregion
    }

}
