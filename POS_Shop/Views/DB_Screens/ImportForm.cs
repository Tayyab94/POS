using ClosedXML.Excel;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
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

        // ═══════════════════════════════════════════════════════════════
        // UI INITIALIZATION
        // ═══════════════════════════════════════════════════════════════

        private void InitializeDataGridViews()
        {
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

            dgvProductsPreview.ReadOnly = true;
            dgvPricesPreview.ReadOnly = true;
        }

        // ═══════════════════════════════════════════════════════════════
        // FILE BROWSE & PREVIEW
        // ═══════════════════════════════════════════════════════════════

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Excel File to Import";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    txtFilePath.Text = selectedFilePath;
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
                dgvProductsPreview.Rows.Clear();
                dgvPricesPreview.Rows.Clear();

                using (var workbook = new XLWorkbook(filePath))
                {
                    if (workbook.Worksheets.TryGetWorksheet("Products", out var productsSheet))
                    {
                        foreach (var row in productsSheet.RowsUsed().Skip(1).Take(50))
                        {
                            dgvProductsPreview.Rows.Add(
                                GetCellValue(row.Cell(1)), GetCellValue(row.Cell(2)),
                                GetCellValue(row.Cell(3)), GetCellValue(row.Cell(4)),
                                GetCellValue(row.Cell(5)), GetCellValue(row.Cell(6)),
                                GetCellValue(row.Cell(7)), GetCellValue(row.Cell(8)),
                                GetCellValue(row.Cell(9)));
                        }
                        lblProductsCount.Text = $"Products found: {productsSheet.RowsUsed().Count() - 1}";
                    }
                    else
                    {
                        MessageBox.Show("'Products' worksheet not found.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    if (workbook.Worksheets.TryGetWorksheet("ProductPrices", out var pricesSheet))
                    {
                        foreach (var row in pricesSheet.RowsUsed().Skip(1).Take(50))
                        {
                            if (row.Cell(1).Value.ToString().Contains("No product prices"))
                                continue;

                            dgvPricesPreview.Rows.Add(
                                GetCellValue(row.Cell(1)), GetCellValue(row.Cell(2)),
                                GetCellValue(row.Cell(3)), GetCellValue(row.Cell(4)),
                                GetCellValue(row.Cell(5)), GetCellValue(row.Cell(6)),
                                GetCellValue(row.Cell(7)), GetCellValue(row.Cell(8)),
                                GetCellValue(row.Cell(9)), GetCellValue(row.Cell(10)));
                        }
                        lblPricesCount.Text = $"Prices found: {pricesSheet.RowsUsed().Count() - 1}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error previewing file: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCellValue(IXLCell cell)
        {
            if (cell.DataType == XLDataType.DateTime)
                return cell.GetDateTime().ToString("yyyy-MM-dd HH:mm");
            return cell.Value.ToString();
        }

        // ═══════════════════════════════════════════════════════════════
        // IMPORT BUTTON
        // ═══════════════════════════════════════════════════════════════

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath) || !File.Exists(selectedFilePath))
            {
                MessageBox.Show("Please select a valid Excel file first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to import this data?\n\n" +
                "Existing records will be updated based on ID matching.\n" +
                "New records will be created if they don't exist.",
                "Confirm Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            SetImportUIState(false);

            try
            {
                using (var progressForm = new ImportProgressForm())
                {
                    progressForm.Show();
                    Application.DoEvents();

                    importResult = await ImportExcelFileAsync(selectedFilePath, progressForm);

                    progressForm.Close();
                    ShowImportResults(importResult);

                    if (importResult.Errors.Count > 0)
                    {
                        var viewErrors = MessageBox.Show(
                            $"Import completed with {importResult.Errors.Count} errors.\n\nDo you want to view the errors?",
                            "Import Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (viewErrors == DialogResult.Yes)
                            ShowImportErrors(importResult.Errors);
                    }
                    else
                    {
                        MessageBox.Show("Product File Imported Successfully",
                            "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during import: {ex.Message}", "Import Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetImportUIState(true);
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // MAIN IMPORT ORCHESTRATOR
        // ═══════════════════════════════════════════════════════════════

        private async Task<ImportResult> ImportExcelFileAsync(string filePath, ImportProgressForm progressForm)
        {
            var result = new ImportResult();

            using (var context = new POSDbContext())
            {
                context.Database.CommandTimeout = 300;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        using (var workbook = new XLWorkbook(filePath))
                        {
                            // Step 1: Products must come FIRST — prices depend on product IDs
                            if (workbook.Worksheets.TryGetWorksheet("Products", out var productsSheet))
                            {
                                progressForm.UpdateMessage("Importing products...");
                                await ImportProductsSheetAsync(productsSheet, context, result, progressForm);
                            }
                            else
                            {
                                result.Errors.Add("'Products' worksheet not found in the Excel file.");
                            }

                            // Step 2: Prices — done after products so new product IDs are available
                            if (workbook.Worksheets.TryGetWorksheet("ProductPrices", out var pricesSheet))
                            {
                                progressForm.UpdateMessage("Importing product prices...");
                                await ImportProductPricesSheetAsync(pricesSheet, context, result, progressForm);
                            }

                            transaction.Commit();
                            progressForm.UpdateMessage("Import completed successfully!");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        result.Errors.Add($"Transaction rolled back: {ex.Message}");
                        throw;
                    }
                }
            }

            return result;
        }

        // ═══════════════════════════════════════════════════════════════
        // PRODUCTS — PARSE → BULK INSERT → BULK UPDATE
        // ═══════════════════════════════════════════════════════════════

        private async Task ImportProductsSheetAsync(IXLWorksheet worksheet, POSDbContext context,
                                                    ImportResult result, ImportProgressForm progressForm)
        {
            var rows = worksheet.RowsUsed().Skip(1).ToList();
            result.ProductsProcessed = rows.Count;

            // ── Load all existing products ONCE into memory ──────────────
            progressForm.UpdateMessage("Loading existing products into memory...");

            var existingProducts = await context.Products
                .Select(p => new { p.Id, p.ProductEnglishName })
                .ToListAsync();

            // Key = old English name  →  Value = DB id
            var byOldName = existingProducts
                .Where(p => !string.IsNullOrEmpty(p.ProductEnglishName))
                .GroupBy(p => p.ProductEnglishName)
                .ToDictionary(g => g.Key, g => g.First().Id, StringComparer.OrdinalIgnoreCase);

            var usedIds = new HashSet<int>(existingProducts.Select(p => p.Id));

            // ── Parse ALL rows — zero DB calls inside this loop ───────────
            var toInsert = new List<Product>();
            var toUpdate = new List<Product>();

            for (int i = 0; i < rows.Count; i++)
            {
                if (progressForm.IsCancelled)
                    throw new OperationCanceledException("Import cancelled by user.");

                // Update UI every 100 rows to avoid UI bottleneck
                if (i % 100 == 0)
                    progressForm.UpdateProgress("Parsing Products", i + 1, rows.Count);

                try
                {
                    var row = rows[i];

                    int productId = row.Cell(1).GetValue<int>();
                    string englishName = row.Cell(2).GetString().Trim();
                    string urduName = row.Cell(3).GetString().Trim();
                    string searchCode = row.Cell(4).GetString().Trim();
                    string purchasePrice = row.Cell(5).GetString().Trim();
                    int cost = row.Cell(6).GetValue<int>();
                    int subcategoryId = row.Cell(7).GetValue<int>();
                    int qty = row.Cell(8).GetValue<int>();
                    string oldName = row.Cell(9).GetString().Trim();

                    if (string.IsNullOrWhiteSpace(englishName))
                    { result.Errors.Add($"Row {i + 2}: English Name is required"); continue; }

                    if (string.IsNullOrWhiteSpace(urduName))
                    { result.Errors.Add($"Row {i + 2}: Urdu Name is required"); continue; }

                    if (byOldName.TryGetValue(oldName, out int existingId))
                    {
                        // ── UPDATE existing product ────────────────────────
                        toUpdate.Add(new Product
                        {
                            Id = existingId,
                            ProductEnglishName = englishName,
                            ProductUrduName = urduName,
                            SearchByProductCode = searchCode,
                            PurchasePrice = purchasePrice,
                            Cost = cost,
                            SubcategoryId = subcategoryId,
                            Qty = qty
                        });
                        result.ProductsUpdated++;
                    }
                    else
                    {
                        // ── INSERT new product ─────────────────────────────
                        var newProduct = new Product
                        {
                            ProductEnglishName = englishName,
                            ProductUrduName = urduName,
                            SearchByProductCode = searchCode,
                            PurchasePrice = purchasePrice,
                            Cost = cost,
                            SubcategoryId = subcategoryId,
                            Qty = qty
                        };

                        // Preserve ID from Excel only if it's genuinely free
                        if (productId > 0 && !usedIds.Contains(productId))
                        {
                            newProduct.Id = productId;
                            usedIds.Add(productId);
                        }

                        toInsert.Add(newProduct);
                        result.ProductsCreated++;

                        // Add to lookup so duplicate rows in same file don't create duplicates
                        if (!string.IsNullOrEmpty(oldName) && !byOldName.ContainsKey(oldName))
                            byOldName[oldName] = 0; // placeholder; real ID assigned by DB
                    }
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Row {i + 2}: {ex.Message}");
                }
            }

            // ── Bulk INSERT ───────────────────────────────────────────────
            if (toInsert.Any())
            {
                progressForm.UpdateMessage($"Bulk inserting {toInsert.Count} new products...");
                await BulkInsertProductsAsync(context, toInsert);
            }

            // ── Bulk UPDATE ───────────────────────────────────────────────
            if (toUpdate.Any())
            {
                progressForm.UpdateMessage($"Bulk updating {toUpdate.Count} products...");
                await BulkUpdateProductsAsync(context, toUpdate);
            }

            progressForm.UpdateMessage("Products done.");
        }

        // ═══════════════════════════════════════════════════════════════
        // PRODUCT PRICES — PARSE → BULK INSERT → BULK UPDATE
        // ═══════════════════════════════════════════════════════════════

        private async Task ImportProductPricesSheetAsync(IXLWorksheet worksheet, POSDbContext context,
                                                         ImportResult result, ImportProgressForm progressForm)
        {
            var rows = worksheet.RowsUsed().Skip(1).ToList();

            // Skip sheet if it only has the "no data" placeholder row
            if (rows.Count == 0 ||
               (rows.Count == 1 && rows[0].Cell(1).Value.ToString().Contains("No product prices")))
            {
                result.PricesProcessed = 0;
                return;
            }

            result.PricesProcessed = rows.Count;

            // ── Load all products ONCE (we need both Id→Name and Name→Id) ──
            progressForm.UpdateMessage("Loading products for price matching...");

            var allProducts = await context.Products
                .Select(p => new { p.Id, p.ProductEnglishName })
                .ToListAsync();

            var productsById = allProducts.ToDictionary(p => p.Id, p => p.ProductEnglishName);
            var productsByName = allProducts
                .GroupBy(p => p.ProductEnglishName, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First().Id, StringComparer.OrdinalIgnoreCase);

            // ── Load all existing prices ONCE ─────────────────────────────
            progressForm.UpdateMessage("Loading existing prices into memory...");

            var existingPrices = await context.ProductPrices
                .Select(p => new { p.Id, p.ProductId, p.Prod_Unit_TypeId })
                .ToListAsync();

            // Key = "productId|unitTypeId"  →  Value = price DB id
            var pricesByKey = existingPrices
                .GroupBy(p => $"{p.ProductId}|{p.Prod_Unit_TypeId}")
                .ToDictionary(g => g.Key, g => g.First().Id);

            var usedPriceIds = new HashSet<int>(existingPrices.Select(p => p.Id));

            // ── Parse ALL rows — zero DB calls inside this loop ───────────
            var toInsert = new List<ProductPrice>();
            var toUpdate = new List<ProductPrice>();

            for (int i = 0; i < rows.Count; i++)
            {
                if (progressForm.IsCancelled)
                    throw new OperationCanceledException("Import cancelled by user.");

                if (i % 100 == 0)
                    progressForm.UpdateProgress("Parsing Prices", i + 1, rows.Count);

                try
                {
                    var row = rows[i];

                    // Skip placeholder rows
                    if (row.Cell(1).Value.ToString().Contains("No product prices"))
                        continue;

                    int productId = row.Cell(1).GetValue<int>();
                    string productName = row.Cell(2).GetString().Trim();
                    int priceId = row.Cell(3).GetValue<int>();
                    int unitTypeId = row.Cell(4).GetValue<int>();
                    string typeName = row.Cell(5).GetString().Trim();
                    string unit = row.Cell(6).GetString().Trim();
                    int itemsCount = row.Cell(7).GetValue<int>();
                    decimal price = row.Cell(8).GetValue<decimal>();
                    decimal pricePerItem = row.Cell(9).GetValue<decimal>();
                    DateTime createdDate = row.Cell(10).GetValue<DateTime>();

                    // ── Resolve actual product ID (from DB) ────────────────
                    int actualProductId = 0;
                    if (productsById.ContainsKey(productId))
                        actualProductId = productId;
                    else if (productsByName.TryGetValue(productName, out int idByName))
                        actualProductId = idByName;

                    if (actualProductId == 0)
                    {
                        result.Errors.Add($"Row {i + 2}: Product not found — ID: {productId}, Name: '{productName}'");
                        continue;
                    }

                    string priceKey = $"{actualProductId}|{unitTypeId}";

                    if (pricesByKey.TryGetValue(priceKey, out int existingPriceId))
                    {
                        // ── UPDATE existing price ──────────────────────────
                        toUpdate.Add(new ProductPrice
                        {
                            Id = existingPriceId,
                            ProductId = actualProductId,
                            Prod_Unit_TypeId = unitTypeId,
                            TypeName = typeName,
                            Unit = unit,
                            ItemsCount = itemsCount,
                            Price = price,
                            PricePerItem = pricePerItem,
                            CreatedDate = createdDate
                        });
                        result.PricesUpdated++;
                    }
                    else
                    {
                        // ── INSERT new price ───────────────────────────────
                        var newPrice = new ProductPrice
                        {
                            ProductId = actualProductId,
                            Prod_Unit_TypeId = unitTypeId,
                            TypeName = typeName,
                            Unit = unit,
                            ItemsCount = itemsCount,
                            Price = price,
                            PricePerItem = pricePerItem,
                            CreatedDate = createdDate
                        };

                        // Preserve ID from Excel only if it's genuinely free
                        if (priceId > 0 && !usedPriceIds.Contains(priceId))
                        {
                            newPrice.Id = priceId;
                            usedPriceIds.Add(priceId);
                        }

                        toInsert.Add(newPrice);
                        result.PricesCreated++;

                        // Register so duplicate rows in same file don't double-insert
                        pricesByKey[priceKey] = 0; // placeholder
                    }
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Row {i + 2}: {ex.Message}");
                }
            }

            // ── Bulk INSERT ───────────────────────────────────────────────
            if (toInsert.Any())
            {
                progressForm.UpdateMessage($"Bulk inserting {toInsert.Count} new prices...");
                await BulkInsertPricesAsync(context, toInsert);
            }

            // ── Bulk UPDATE ───────────────────────────────────────────────
            if (toUpdate.Any())
            {
                progressForm.UpdateMessage($"Bulk updating {toUpdate.Count} prices...");
                await BulkUpdatePricesAsync(context, toUpdate);
            }

            progressForm.UpdateMessage("Prices done.");
        }

        // ═══════════════════════════════════════════════════════════════
        // BULK HELPERS — PRODUCTS
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// SqlBulkCopy INSERT — sends all rows in one network round-trip.
        /// ~100x faster than EF AddRange at scale.
        /// </summary>
        private async Task BulkInsertProductsAsync(POSDbContext context, List<Product> products)
        {
            var table = new DataTable();
            // NOTE: Do NOT include the identity column (Id) unless you need to preserve IDs.
            // If you DO need to preserve IDs, add Id column + use SqlBulkCopyOptions.KeepIdentity.
            table.Columns.Add("ProductEnglishName", typeof(string));
            table.Columns.Add("ProductUrduName", typeof(string));
            table.Columns.Add("SearchByProductCode", typeof(string));
            table.Columns.Add("PurchasePrice", typeof(string));
            table.Columns.Add("Cost", typeof(int));
            table.Columns.Add("SubcategoryId", typeof(int));
            table.Columns.Add("Qty", typeof(int));

            foreach (var p in products)
                table.Rows.Add(
                    p.ProductEnglishName,
                    p.ProductUrduName,
                    p.SearchByProductCode ?? string.Empty,
                    p.PurchasePrice ?? string.Empty,
                    p.Cost,
                    p.SubcategoryId,
                    p.Qty);

            await ExecuteBulkCopyAsync(context, table, "Products");
        }

        /// <summary>
        /// Temp-table + single UPDATE JOIN — updates all rows in one SQL statement.
        /// </summary>
        private async Task BulkUpdateProductsAsync(POSDbContext context, List<Product> products)
        {
            const string createTempTable = @"
                CREATE TABLE #TempProducts (
                    Id                  INT           NOT NULL,
                    ProductEnglishName  NVARCHAR(500) NOT NULL,
                    ProductUrduName     NVARCHAR(500) NOT NULL,
                    SearchByProductCode NVARCHAR(200),
                    PurchasePrice       NVARCHAR(100),
                    Cost                INT           NOT NULL,
                    SubcategoryId       INT           NOT NULL,
                    Qty                 INT           NOT NULL
                )";

            const string mergeUpdate = @"
                UPDATE p
                SET
                    p.ProductEnglishName   = t.ProductEnglishName,
                    p.ProductUrduName      = t.ProductUrduName,
                    p.SearchByProductCode  = t.SearchByProductCode,
                    p.PurchasePrice        = t.PurchasePrice,
                    p.Cost                 = t.Cost,
                    p.SubcategoryId        = t.SubcategoryId,
                    p.Qty                  = t.Qty
                FROM Products p
                INNER JOIN #TempProducts t ON p.Id = t.Id";

            await ExecuteTempTableUpdateAsync(context, createTempTable, mergeUpdate, () =>
            {
                var table = new DataTable();
                table.Columns.Add("Id", typeof(int));
                table.Columns.Add("ProductEnglishName", typeof(string));
                table.Columns.Add("ProductUrduName", typeof(string));
                table.Columns.Add("SearchByProductCode", typeof(string));
                table.Columns.Add("PurchasePrice", typeof(string));
                table.Columns.Add("Cost", typeof(int));
                table.Columns.Add("SubcategoryId", typeof(int));
                table.Columns.Add("Qty", typeof(int));

                foreach (var p in products)
                    table.Rows.Add(p.Id, p.ProductEnglishName, p.ProductUrduName,
                                   p.SearchByProductCode ?? string.Empty,
                                   p.PurchasePrice ?? string.Empty,
                                   p.Cost, p.SubcategoryId, p.Qty);
                return table;
            }, "#TempProducts");
        }

        // ═══════════════════════════════════════════════════════════════
        // BULK HELPERS — PRODUCT PRICES
        // ═══════════════════════════════════════════════════════════════

        private async Task BulkInsertPricesAsync(POSDbContext context, List<ProductPrice> prices)
        {
            var table = new DataTable();
            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("Prod_Unit_TypeId", typeof(int));
            table.Columns.Add("TypeName", typeof(string));
            table.Columns.Add("Unit", typeof(string));
            table.Columns.Add("ItemsCount", typeof(int));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("PricePerItem", typeof(decimal));
            table.Columns.Add("CreatedDate", typeof(DateTime));

            foreach (var p in prices)
                table.Rows.Add(
                    p.ProductId,
                    p.Prod_Unit_TypeId,
                    p.TypeName ?? string.Empty,
                    p.Unit ?? string.Empty,
                    p.ItemsCount,
                    p.Price,
                    p.PricePerItem,
                    p.CreatedDate);

            await ExecuteBulkCopyAsync(context, table, "ProductPrices");
        }

        private async Task BulkUpdatePricesAsync(POSDbContext context, List<ProductPrice> prices)
        {
            const string createTempTable = @"
                CREATE TABLE #TempPrices (
                    Id               INT             NOT NULL,
                    ProductId        INT             NOT NULL,
                    Prod_Unit_TypeId INT             NOT NULL,
                    TypeName         NVARCHAR(200),
                    Unit             NVARCHAR(100),
                    ItemsCount       INT             NOT NULL,
                    Price            DECIMAL(18, 2)  NOT NULL,
                    PricePerItem     DECIMAL(18, 2)  NOT NULL,
                    CreatedDate      DATETIME        NOT NULL
                )";

            const string mergeUpdate = @"
                UPDATE pp
                SET
                    pp.TypeName         = t.TypeName,
                    pp.Unit             = t.Unit,
                    pp.ItemsCount       = t.ItemsCount,
                    pp.Price            = t.Price,
                    pp.PricePerItem     = t.PricePerItem,
                    pp.CreatedDate      = t.CreatedDate
                FROM ProductPrices pp
                INNER JOIN #TempPrices t ON pp.Id = t.Id";

            await ExecuteTempTableUpdateAsync(context, createTempTable, mergeUpdate, () =>
            {
                var table = new DataTable();
                table.Columns.Add("Id", typeof(int));
                table.Columns.Add("ProductId", typeof(int));
                table.Columns.Add("Prod_Unit_TypeId", typeof(int));
                table.Columns.Add("TypeName", typeof(string));
                table.Columns.Add("Unit", typeof(string));
                table.Columns.Add("ItemsCount", typeof(int));
                table.Columns.Add("Price", typeof(decimal));
                table.Columns.Add("PricePerItem", typeof(decimal));
                table.Columns.Add("CreatedDate", typeof(DateTime));

                foreach (var p in prices)
                    table.Rows.Add(p.Id, p.ProductId, p.Prod_Unit_TypeId,
                                   p.TypeName ?? string.Empty,
                                   p.Unit ?? string.Empty,
                                   p.ItemsCount, p.Price, p.PricePerItem, p.CreatedDate);
                return table;
            }, "#TempPrices");
        }

        // ═══════════════════════════════════════════════════════════════
        // SHARED SQL INFRASTRUCTURE
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// Extracts both the open SqlConnection AND its active SqlTransaction from the EF6 context.
        /// Every SqlCommand and SqlBulkCopy MUST use both — SQL Server requires this when a
        /// transaction is already open on the connection.
        /// </summary>
        private (SqlConnection conn, SqlTransaction tx) GetConnectionAndTransaction(POSDbContext context)
        {
            var conn = (SqlConnection)context.Database.Connection;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            // EF6 exposes the underlying ADO.NET transaction via CurrentTransaction.UnderlyingTransaction
            var tx = (SqlTransaction)context.Database.CurrentTransaction?.UnderlyingTransaction;

            return (conn, tx);
        }

        /// <summary>
        /// Reusable SqlBulkCopy INSERT helper.
        /// Always enrolls in the active transaction — required when EF has an open transaction.
        /// </summary>
        private async Task ExecuteBulkCopyAsync(POSDbContext context, DataTable table,
                                                string destinationTable)
        {
            var (conn, tx) = GetConnectionAndTransaction(context);

            // SqlBulkCopyOptions.Default = no identity preservation
            // Pass tx explicitly — if tx is null (no active transaction) this still works fine
            using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tx))
            {
                bulkCopy.DestinationTableName = destinationTable;
                bulkCopy.BatchSize = 2000;
                bulkCopy.BulkCopyTimeout = 300;

                foreach (DataColumn col in table.Columns)
                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);

                await bulkCopy.WriteToServerAsync(table);
            }
        }

        /// <summary>
        /// Reusable temp-table UPDATE pattern.
        /// All commands (CREATE, BulkCopy, UPDATE, DROP) share the same connection + transaction.
        ///   1. Create temp table
        ///   2. BulkCopy data into it
        ///   3. Run single UPDATE JOIN against real table
        ///   4. Drop temp table
        /// </summary>
        private async Task ExecuteTempTableUpdateAsync(POSDbContext context,
                                                       string createTempTableSql,
                                                       string updateJoinSql,
                                                       Func<DataTable> buildDataTable,
                                                       string tempTableName)
        {
            var (conn, tx) = GetConnectionAndTransaction(context);

            // 1. Create temp table — must be on same connection+transaction
            await ExecuteNonQueryAsync(conn, tx, createTempTableSql);

            // 2. Bulk copy into temp table — must share same transaction
            using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tx))
            {
                bulkCopy.DestinationTableName = tempTableName;
                bulkCopy.BatchSize = 2000;
                bulkCopy.BulkCopyTimeout = 300;

                var table = buildDataTable();
                foreach (DataColumn col in table.Columns)
                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);

                await bulkCopy.WriteToServerAsync(table);
            }

            // 3. Single UPDATE JOIN — must share same transaction
            await ExecuteNonQueryAsync(conn, tx, updateJoinSql, timeout: 300);

            // 4. Drop temp table
            await ExecuteNonQueryAsync(conn, tx, $"DROP TABLE {tempTableName}");
        }

        /// <summary>
        /// Executes a non-query SQL command, always enrolling it in the provided transaction.
        /// tx can be null (if no transaction is active) and the command still works correctly.
        /// </summary>
        private async Task ExecuteNonQueryAsync(SqlConnection conn, SqlTransaction tx,
                                                string sql, int timeout = 30)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandTimeout = timeout;
                cmd.Transaction = tx; // ← THE FIX: assign transaction to every command
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Returns the EF context's underlying SqlConnection, opening it if needed.
        /// </summary>
        private SqlConnection GetOpenConnection(POSDbContext context)
        {
            var conn = (SqlConnection)context.Database.Connection;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            return conn;
        }

        // ═══════════════════════════════════════════════════════════════
        // UI HELPERS
        // ═══════════════════════════════════════════════════════════════

        private void ShowImportResults(ImportResult result)
        {
            txtImportSummary.Text =
                $"IMPORT SUMMARY\n" +
                $"===============\n\n" +
                $"PRODUCTS:\n" +
                $"  • Total processed : {result.ProductsProcessed}\n" +
                $"  • Created         : {result.ProductsCreated}\n" +
                $"  • Updated         : {result.ProductsUpdated}\n\n" +
                $"PRICES:\n" +
                $"  • Total processed : {result.PricesProcessed}\n" +
                $"  • Created         : {result.PricesCreated}\n" +
                $"  • Updated         : {result.PricesUpdated}\n\n" +
                $"ERRORS: {result.Errors.Count}";

            lblStatus.Text = $"Import completed. {result.ProductsCreated + result.ProductsUpdated} products processed.";
            lblStatus.ForeColor = Color.Green;
        }

        private void ShowImportErrors(List<string> errors)
        {
            new ErrorLogForm(errors).ShowDialog();
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

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void ImportForm_Load(object sender, EventArgs e) => btnImport.Enabled = false;
    }

    // ═══════════════════════════════════════════════════════════════════
    // IMPORT RESULT MODEL
    // ═══════════════════════════════════════════════════════════════════

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
