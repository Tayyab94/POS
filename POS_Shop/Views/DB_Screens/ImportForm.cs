using ClosedXML.Excel;
using Org.BouncyCastle.Asn1.Cmp;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.DB_Screens
{
    public partial class ImportForm : Form
    {
        private string selectedFilePath = string.Empty;
        private ImportResult importResult;

        public ImportForm()
        {
            InitializeComponent();
            InitializeDataGridViews();
        }

        private void InitializeDataGridViews()
        {
            // Initialize Products DataGridView
            dgvProductsPreview.Columns.Clear();
            dgvProductsPreview.Columns.Add("ProductID", "Product ID");
            dgvProductsPreview.Columns.Add("ProductName", "Product Name");
            dgvProductsPreview.Columns.Add("UrduName", "Urdu Name");
            dgvProductsPreview.Columns.Add("SearchCode", "Search Code");
            dgvProductsPreview.Columns.Add("PurchasePrice", "Purchase Price");
            dgvProductsPreview.Columns.Add("Cost", "Cost");
            dgvProductsPreview.Columns.Add("SubCategory", "SubCategory ID");
            dgvProductsPreview.Columns.Add("Qty", "Quantity");
            dgvProductsPreview.Columns.Add("ProductOldName", "Old Name");

            // Initialize Prices DataGridView
            dgvPricesPreview.Columns.Clear();
            dgvPricesPreview.Columns.Add("ProductID", "Product ID");
            dgvPricesPreview.Columns.Add("ProductName", "Product Name");
            dgvPricesPreview.Columns.Add("PriceID", "Price ID");
            dgvPricesPreview.Columns.Add("UnitTypeID", "Unit Type ID");
            dgvPricesPreview.Columns.Add("TypeName", "Type Name");
            dgvPricesPreview.Columns.Add("Unit", "Unit");
            dgvPricesPreview.Columns.Add("ItemsCount", "Items Count");
            dgvPricesPreview.Columns.Add("Price", "Price");
            dgvPricesPreview.Columns.Add("PricePerItem", "Price Per Item");
            dgvPricesPreview.Columns.Add("CreatedDate", "Created Date");

            // Set readonly
            dgvProductsPreview.ReadOnly = true;
            dgvPricesPreview.ReadOnly = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Excel File to Import";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    txtFilePath.Text = selectedFilePath;

                    // Preview the file
                    PreviewExcelFile(selectedFilePath);

                    btnImport.Enabled = true;
                    lblStatus.Text = $"File loaded: {Path.GetFileName(selectedFilePath)}";
                    lblStatus.ForeColor = Color.Green;
                }
            }
        }

        private void PreviewExcelFile(string filePath)
        {
            try
            {
                // Clear previous previews
                dgvProductsPreview.Rows.Clear();
                dgvPricesPreview.Rows.Clear();

                using (var workbook = new XLWorkbook(filePath))
                {
                    // Preview Products sheet
                    if (workbook.Worksheets.TryGetWorksheet("Products", out var productsWorksheet))
                    {
                        var productRows = productsWorksheet.RowsUsed().Skip(1).Take(50); // Preview first 50 rows
                        foreach (var row in productRows)
                        {
                            dgvProductsPreview.Rows.Add(
                                GetCellValue(row.Cell(1)),
                                GetCellValue(row.Cell(2)),
                                GetCellValue(row.Cell(3)),
                                GetCellValue(row.Cell(4)),
                                GetCellValue(row.Cell(5)),
                                GetCellValue(row.Cell(6)),
                                GetCellValue(row.Cell(7)),
                                GetCellValue(row.Cell(8)),
                                GetCellValue(row.Cell(9))
                            );
                        }

                        lblProductsCount.Text = $"Products found: {productsWorksheet.RowsUsed().Count() - 1}";
                    }
                    else
                    {
                        MessageBox.Show("'Products' worksheet not found in the Excel file.",
                                      "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // Preview ProductPrices sheet
                    if (workbook.Worksheets.TryGetWorksheet("ProductPrices", out var pricesWorksheet))
                    {
                        var priceRows = pricesWorksheet.RowsUsed().Skip(1).Take(50); // Preview first 50 rows
                        foreach (var row in priceRows)
                        {
                            // Skip the "no data" row
                            if (row.Cell(1).Value.ToString().Contains("No product prices"))
                                continue;

                            dgvPricesPreview.Rows.Add(
                                GetCellValue(row.Cell(1)),
                                GetCellValue(row.Cell(2)),
                                GetCellValue(row.Cell(3)),
                                GetCellValue(row.Cell(4)),
                                GetCellValue(row.Cell(5)),
                                GetCellValue(row.Cell(6)),
                                GetCellValue(row.Cell(7)),
                                GetCellValue(row.Cell(8)),
                                GetCellValue(row.Cell(9)),
                                GetCellValue(row.Cell(10))
                            );
                        }

                        lblPricesCount.Text = $"Prices found: {pricesWorksheet.RowsUsed().Count() - 1}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error previewing file: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCellValue(IXLCell cell)
        {
            
            if (cell.DataType == XLDataType.DateTime)
                return cell.GetDateTime().ToString("yyyy-MM-dd HH:mm");

            return cell.Value.ToString();
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath) || !File.Exists(selectedFilePath))
            {
                MessageBox.Show("Please select a valid Excel file first.",
                              "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Show confirmation dialog
            var confirmResult = MessageBox.Show(
                "Are you sure you want to import this data?\n\n" +
                "Existing records will be updated based on ID matching.\n" +
                "New records will be created if they don't exist.",
                "Confirm Import",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            // Disable UI during import
            SetImportUIState(false);

            try
            {
                // Show progress form
                using (var progressForm = new ImportProgressForm())
                {
                    progressForm.Show();
                    Application.DoEvents();

                    // Perform the import
                    importResult = await ImportExcelFileAsync(selectedFilePath, progressForm);

                    progressForm.Close();

                    // Show results
                    ShowImportResults(importResult);

                    if (importResult.Errors.Count > 0)
                    {
                        var viewErrors = MessageBox.Show(
                            $"Import completed with {importResult.Errors.Count} errors.\n\n" +
                            "Do you want to view the errors?",
                            "Import Complete",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (viewErrors == DialogResult.Yes)
                        {
                            ShowImportErrors(importResult.Errors);
                        }
                    }else
                    {
                        MessageBox.Show($"Product File Imported Successfully",
              "Import Successs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during import: {ex.Message}",
                              "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable UI
                SetImportUIState(true);
            }
        }

        private async Task<ImportResult> ImportExcelFileAsync(string filePath, ImportProgressForm progressForm)
        {
            var result = new ImportResult();

            using (var context = new POSDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        using (var workbook = new XLWorkbook(filePath))
                        {
                            // 1. Import Products from "Products" sheet
                            if (workbook.Worksheets.TryGetWorksheet("Products", out var productsWorksheet))
                            {
                                progressForm.UpdateMessage("Importing products...");
                                await ImportProductsSheetAsync(productsWorksheet, context, result, progressForm);
                            }
                            else
                            {
                                result.Errors.Add("'Products' worksheet not found in the Excel file.");
                            }

                            // 2. Import ProductPrices from "ProductPrices" sheet
                            if (workbook.Worksheets.TryGetWorksheet("ProductPrices", out var pricesWorksheet))
                            {
                                progressForm.UpdateMessage("Importing product prices...");
                                await ImportProductPricesSheetAsync(pricesWorksheet, context, result, progressForm);
                            }

                            // Save all changes to database
                            progressForm.UpdateMessage("Saving to database...");
                            await context.SaveChangesAsync();
                            transaction.Commit();

                            progressForm.UpdateMessage("Import completed successfully!");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        result.Errors.Add($"Transaction rolled back due to error: {ex.Message}");
                        throw;
                    }
                }
            }

            return result;
        }

        private async Task ImportProductsSheetAsync(IXLWorksheet worksheet, POSDbContext context,
                                                   ImportResult result, ImportProgressForm progressForm)
        {
            var rows = worksheet.RowsUsed().Skip(1).ToList();
            result.ProductsProcessed = rows.Count;

            for (int i = 0; i < rows.Count; i++)
            {
                if (progressForm.IsCancelled)
                {
                    result.Errors.Add("Import cancelled by user.");
                    throw new OperationCanceledException("Import cancelled by user.");
                }

                progressForm.UpdateProgress("Importing Products", i + 1, rows.Count);

                try
                {
                    var row = rows[i];

                    // Read values from Excel row
                    int productId = row.Cell(1).GetValue<int>();
                    string productEnglishName = row.Cell(2).GetString();
                    string productUrduName = row.Cell(3).GetString();
                    string searchByProductCode = row.Cell(4).GetString();
                    string purchasePrice = row.Cell(5).GetString();
                    int cost = row.Cell(6).GetValue<int>();
                    int subcategoryId = row.Cell(7).GetValue<int>();
                    int qty = row.Cell(8).GetValue<int>();
                    string productOldName = row.Cell(9).GetString();
                    // Validate required fields
                    if (string.IsNullOrWhiteSpace(productEnglishName))
                    {
                        result.Errors.Add($"Row {i + 2}: Product English Name is required");
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(productUrduName))
                    {
                        result.Errors.Add($"Row {i + 2}: Product Urdu Name is required");
                        continue;
                    }

                    // Check if product exists
                    Product existingProduct = null;

                    // Try by ID
                    //if (productId > 0)
                    //{
                    //    existingProduct = await context.Products
                    //        .FirstOrDefaultAsync(p => p.Id == productId);
                    //}

                    //// If not found by ID, try by English Name
                    //if (existingProduct == null)
                    //{
                    //    existingProduct = await context.Products
                    //        .FirstOrDefaultAsync(p => p.ProductEnglishName == productEnglishName);
                    //}

                    //// If not found by English Name, try by Urdu Name
                    //if (existingProduct == null)
                    //{
                    //    existingProduct = await context.Products
                    //        .FirstOrDefaultAsync(p => p.ProductUrduName == productUrduName);
                    //}

                    existingProduct = await context.Products
                        .FirstOrDefaultAsync(S => S.ProductEnglishName == productOldName);

                    if (existingProduct != null)
                    {
                        // Update existing product
                        existingProduct.ProductEnglishName = productEnglishName;
                        existingProduct.ProductUrduName = productUrduName;
                        existingProduct.SearchByProductCode = searchByProductCode;
                        existingProduct.PurchasePrice = purchasePrice;
                        existingProduct.Cost = cost;
                        existingProduct.SubcategoryId = subcategoryId;
                        existingProduct.Qty = qty;

                        context.Products.AddOrUpdate(existingProduct);
                        result.ProductsUpdated++;
                    }
                    else
                    {
                        // Create new product
                        var newProduct = new Product
                        {
                            ProductEnglishName = productEnglishName,
                            ProductUrduName = productUrduName,
                            SearchByProductCode = searchByProductCode,
                            PurchasePrice = purchasePrice,
                            Cost = cost,
                            SubcategoryId = subcategoryId,
                            Qty = qty
                        };

                        // If Excel has an ID and it's not in use, preserve it
                        if (productId > 0 && !await context.Products.AnyAsync(p => p.Id == productId))
                        {
                            // For SQL Server, we might need to handle identity insert
                            // This is a simplified approach
                            try
                            {
                                // Temporarily set ID, but let DB handle it if there's a conflict
                                newProduct.Id = productId;
                                 context.Products.Add(newProduct);
                            }
                            catch
                            {
                                // If setting ID fails, let DB generate it
                                newProduct.Id = 0;
                                 context.Products.Add(newProduct);
                            }
                        }
                        else
                        {
                             context.Products.Add(newProduct);
                        }

                        result.ProductsCreated++;
                    }

                    // Save periodically (every 50 rows)
                    if ((i + 1) % 50 == 0)
                    {
                        await context.SaveChangesAsync();
                        progressForm.UpdateMessage($"Saved {i + 1} of {rows.Count} products...");
                    }
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Row {i + 2}: Error processing product - {ex.Message}");
                }
            }

            // Final save for remaining products
            await context.SaveChangesAsync();
        }

        private async Task ImportProductPricesSheetAsync(IXLWorksheet worksheet, POSDbContext context,
                                                        ImportResult result, ImportProgressForm progressForm)
        {
            var rows = worksheet.RowsUsed().Skip(1).ToList();

            // Skip if the sheet only contains the "no data" message
            if (rows.Count == 1 && rows[0].Cell(1).Value.ToString()?.Contains("No product prices") == true)
            {
                result.PricesProcessed = 0;
                return;
            }

            result.PricesProcessed = rows.Count;

            for (int i = 0; i < rows.Count; i++)
            {
                if (progressForm.IsCancelled)
                {
                    result.Errors.Add("Import cancelled by user.");
                    throw new OperationCanceledException("Import cancelled by user.");
                }

                progressForm.UpdateProgress("Importing Prices", i + 1, rows.Count);

                try
                {
                    var row = rows[i];

                    // Read values from Excel row
                    int productId = row.Cell(1).GetValue<int>();
                    string productName = row.Cell(2).GetString();
                    int priceId = row.Cell(3).GetValue<int>();
                    int unitTypeId = row.Cell(4).GetValue<int>();
                    string typeName = row.Cell(5).GetString();
                    string unit = row.Cell(6).GetString();
                    int itemsCount = row.Cell(7).GetValue<int>();
                    decimal price = row.Cell(8).GetValue<decimal>();
                    decimal pricePerItem = row.Cell(9).GetValue<decimal>();
                    DateTime createdDate = row.Cell(10).GetValue<DateTime>();

                    // Find the product
                    Product product = await context.Products
                        .FirstOrDefaultAsync(p => p.Id == productId || p.ProductEnglishName == productName);

                    if (product == null)
                    {
                        result.Errors.Add($"Row {i + 2}: Product not found (ID: {productId}, Name: {productName})");
                        continue;
                    }

                    // Verify ProductUnit exists
                    var productUnit = await context.ProductUnits
                        .FirstOrDefaultAsync(pu => pu.Id == unitTypeId);

                    if (productUnit == null)
                    {
                        result.Errors.Add($"Row {i + 2}: Product Unit Type with ID {unitTypeId} not found");
                        continue;
                    }

                    // Check if price exists
                    ProductPrice existingPrice = null;

                    // Try by Price ID
                    if (priceId > 0)
                    {
                        existingPrice = await context.ProductPrices
                            .FirstOrDefaultAsync(pp => pp.Id == priceId);
                    }

                    // If not found by ID, check if similar price exists for this product and unit
                    if (existingPrice == null)
                    {
                        existingPrice = await context.ProductPrices
                            .FirstOrDefaultAsync(pp => pp.ProductId == product.Id && pp.Prod_Unit_TypeId == unitTypeId);
                    }

                    if (existingPrice != null)
                    {
                        // Update existing price
                        existingPrice.ProductId = product.Id;
                        existingPrice.Prod_Unit_TypeId = unitTypeId;
                        existingPrice.TypeName = typeName;
                        existingPrice.Unit = unit;
                        existingPrice.ItemsCount = itemsCount;
                        existingPrice.Price = price;
                        existingPrice.PricePerItem = pricePerItem;
                        existingPrice.CreatedDate = createdDate;

                        context.ProductPrices.AddOrUpdate(existingPrice);
                        result.PricesUpdated++;
                    }
                    else
                    {
                        // Create new price
                        var newPrice = new ProductPrice
                        {
                            ProductId = product.Id,
                            Prod_Unit_TypeId = unitTypeId,
                            TypeName = typeName,
                            Unit = unit,
                            ItemsCount = itemsCount,
                            Price = price,
                            PricePerItem = pricePerItem,
                            CreatedDate = createdDate
                        };

                        // Try to preserve Price ID if possible
                        if (priceId > 0 && !await context.ProductPrices.AnyAsync(pp => pp.Id == priceId))
                        {
                            try
                            {
                                newPrice.Id = priceId;
                                 context.ProductPrices.Add(newPrice);
                            }
                            catch
                            {
                                newPrice.Id = 0;
                                 context.ProductPrices.Add(newPrice);
                            }
                        }
                        else
                        {
                             context.ProductPrices.Add(newPrice);
                        }

                        result.PricesCreated++;
                    }

                    // Save periodically
                    if ((i + 1) % 50 == 0)
                    {
                        await context.SaveChangesAsync();
                        progressForm.UpdateMessage($"Saved {i + 1} of {rows.Count} prices...");
                    }
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Row {i + 2}: Error processing price - {ex.Message}");
                }
            }

            // Final save for remaining prices
            await context.SaveChangesAsync();
        }

       

        private void ShowImportResults(ImportResult result)
        {
            string summary = $"IMPORT SUMMARY\n" +
                           $"===============\n\n" +
                           $"PRODUCTS:\n" +
                           $"  • Total processed: {result.ProductsProcessed}\n" +
                           $"  • Created: {result.ProductsCreated}\n" +
                           $"  • Updated: {result.ProductsUpdated}\n\n" +
                           $"PRICES:\n" +
                           $"  • Total processed: {result.PricesProcessed}\n" +
                           $"  • Created: {result.PricesCreated}\n" +
                           $"  • Updated: {result.PricesUpdated}\n\n" +
                           $"ERRORS: {result.Errors.Count}";

            txtImportSummary.Text = summary;

            // Update status
            lblStatus.Text = $"Import completed. {result.ProductsCreated + result.ProductsUpdated} products processed.";
            lblStatus.ForeColor = Color.Green;
        }

        private void ShowImportErrors(List<string> errors)
        {
            ErrorLogForm errorForm = new ErrorLogForm(errors);
            errorForm.ShowDialog();
        }

        private void SetImportUIState(bool enabled)
        {
            btnBrowse.Enabled = enabled;
            btnImport.Enabled = enabled && !string.IsNullOrEmpty(selectedFilePath);
            btnClose.Enabled = enabled;

            if (!enabled)
            {
                lblStatus.Text = "Importing... Please wait.";
                lblStatus.ForeColor = Color.Blue;
                Cursor = Cursors.WaitCursor;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            btnImport.Enabled = false;
        }
    }

    // Import Result class
    public class ImportResult
    {
        public int ProductsProcessed { get; set; }
        public int ProductsCreated { get; set; }
        public int ProductsUpdated { get; set; }
        public int PricesProcessed { get; set; }
        public int PricesCreated { get; set; }
        public int PricesUpdated { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
