using ExcelDataReader;
using Org.BouncyCastle.Asn1.Cmp;
using POS_Shop.Helpers;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS_Shop.Views.DB_Screens
{
    public partial class ImportExcelFile : Form
    {
        public ImportExcelFile()
        {
            InitializeComponent();
            this.Load += ImportExcelFile_Load;
            bindingSource = new BindingSource();

            InitializeBackgroundWorker();
        }

        private async void ImportExcelFile_Load(object sender, EventArgs e)
        {
           CheckProductRecordsAndDisableTabs();
        }
        private async void CheckProductRecordsAndDisableTabs()
        {
            try
            {
                using (var context = new POSDbContext())
                {
                    if(await context.Products.AnyAsync())
                    {
                        tabPage1.Enabled=false;
                        tabPage1.Text="Products (Already Imported)";
                        ImportFileTabComtrol.SelectedTab = tabPage2;
                        tabPage2.Enabled = true;
                    }
                    else
                    {
                        tabPage1.Enabled = true;
                        tabPage1.Text = "Products (Not Imported Yet)";
                        ImportFileTabComtrol.SelectedTab = tabPage1;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BrowsFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Set the filter to show only .bak files
            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|All files|*.*";
            ofd.Title = "Select an Excel File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImportFilePathTxt.Text = ofd.FileName;
                loadDataBtn.Enabled = true;
            }
        }

        private BindingSource bindingSource;
        private BackgroundWorker backgroundWorker;

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        //private BindingSource bindingSource;
        //private void loadDataBtn_Click(object sender, EventArgs e)
        //{
        //    using (var stream = File.Open(ImportFilePathTxt.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        //    {
        //        //// Register encoding provider (needed for older Excel files, e.g., .xls)
        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        //        using (var reader = ExcelReaderFactory.CreateReader(stream))
        //        {
        //            var conf = new ExcelDataSetConfiguration
        //            {
        //                ConfigureDataTable = _ => new ExcelDataTableConfiguration
        //                {
        //                    UseHeaderRow = true
        //                }
        //            };
        //            var dataSet = reader.AsDataSet(conf);

        //            if (dataSet.Tables.Count == 0)
        //            {
        //                MessageBox.Show("No worksheets found in the file.", "No data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }


        //           var currentTable = dataSet.Tables[2];

        //            DataTable filtered = new DataTable();
        //            // Add only required columns
        //            filtered.Columns.Add("Item Name");
        //            filtered.Columns.Add("Urdu");
        //            filtered.Columns.Add("Unit");
        //            filtered.Columns.Add("Company Rate");
        //            filtered.Columns.Add("Cost");
        //            filtered.Columns.Add("Price (R)");
        //            filtered.Columns.Add("Category");
        //            //var selectedColumns = new[] { "Item Name", "Urdu", "Company Rate", "Cost", "Price (R)" };
        //            //DataTable filteredTable = currentTable.DefaultView.ToTable(false, selectedColumns);
        //            //ProductDataGrid.DataSource = filteredTable;

        //            // Copy rows
        //            foreach (DataRow row in currentTable.Rows)
        //            {
        //                // Skip rows that are empty or header duplicates
        //                if (row[2] == DBNull.Value || row[2].ToString() == "Item Name")
        //                    continue;
        //                filtered.Rows.Add(
        //                    row[2],  // Item Name (3rd col in Excel)
        //                    row[3],  // Urdu
        //                      null,    // Unit (empty for now, user selects)
        //                    row[4],  // Company Rate
        //                    row[5],  // Cost
        //                    row[8]   // Price (R)
        //                    ,row[6]  // Category (2nd col in Excel
        //                );
        //            }

        //            ProductDataGrid.DataSource = filtered;

        //            DataGridViewComboBoxColumn unitColumn = new DataGridViewComboBoxColumn();
        //            unitColumn.HeaderText = "Unit";
        //            unitColumn.Name = "Unit";
        //            unitColumn.DataPropertyName = "Unit"; // (optional if binding to DataTable)
        //            unitColumn.Items.AddRange(new object[]
        //                 {
        //                    "عدد",
        //                    "ڈبہ",
        //                    "درجن",
        //                    "کارٹن",
        //                    "پیکٹ",
        //                    "رول",
        //                    "گز",
        //                    "بنڈل",
        //                    "ڈبی",
        //                    "کلو",
        //                    "جوڑی",
        //                    "سابقہ"
        //                 });
        //            // Find index of Urdu column and insert Unit right after it
        //            int urduIndex = ProductDataGrid.Columns["Urdu"].Index;
        //            ProductDataGrid.Columns.Remove("Unit");
        //            ProductDataGrid.Columns.Insert(urduIndex + 1, unitColumn);               
        //        }

        //    }

        //    ImportToDbBtn.Enabled = true;
        //    //LoadDataFromExcel(ImportFilePathTxt.Text, ".xlsx", "YES");
        //}




        private async void loadDataBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ImportFilePathTxt.Text) || !File.Exists(ImportFilePathTxt.Text))
            {
                MessageBox.Show("Please select a valid file first.", "File Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Disable UI during processing
            SetUIState(false);

            // Show progress
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
         //   lblStatus.Text = "Reading Excel file...";

            try
            {
                // Use BackgroundWorker for better user experience
                backgroundWorker.RunWorkerAsync(ImportFilePathTxt.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting file processing: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetUIState(true);
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string filePath = e.Argument as string;
            var result = ReadExcelWithTrueStreaming(filePath, sender as BackgroundWorker);
            e.Result = result;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
           //     lblStatus.Text = e.UserState.ToString();
            }

            if (e.ProgressPercentage >= 0)
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = Math.Min(e.ProgressPercentage, 100);
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"Error reading file: {e.Error.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
             //   lblStatus.Text = "Error occurred";
            }
            else if (e.Result != null)
            {
                var result = (StreamingResult)e.Result;
                if (result.Success && result.DataTable != null && result.DataTable.Rows.Count > 0)
                {
                    ProductDataGrid.DataSource = result.DataTable;
                    AddUnitComboBoxColumn();
                    ImportToDbBtn.Enabled = true;
                  //  lblStatus.Text = $"Successfully loaded {result.DataTable.Rows.Count} products";

                    MessageBox.Show($"File imported successfully! Loaded {result.DataTable.Rows.Count} products.",
                                  "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(result.ErrorMessage ?? "No valid data found in the file.", "Warning",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  //  lblStatus.Text = "No data found";
                }
            }

            SetUIState(true);
            progressBar1.Visible = false;
        }

        private StreamingResult ReadExcelWithTrueStreaming(string filePath, BackgroundWorker worker)
        {
            var result = new StreamingResult();

            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // First, let's see what worksheets we have
                    var worksheetNames = new List<string>();
                    int totalSheets = 0;

                    do
                    {
                        totalSheets++;
                        worksheetNames.Add(reader.Name ?? $"Sheet{totalSheets}");
                    } while (reader.NextResult());

                    // Reset to read data
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var dataReader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataSetConfig = new ExcelDataSetConfiguration()
                        {
                            UseColumnDataType = false,
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true,
                                FilterRow = (rowReader) =>
                                {
                                    if (rowReader.Depth == 0) return true;
                                    return rowReader[2] != null &&
                                           !string.IsNullOrWhiteSpace(rowReader[2]?.ToString()) &&
                                           rowReader[2].ToString() != "Item Name";
                                }
                            }
                        };

                        var dataSet = dataReader.AsDataSet(dataSetConfig);

                        if (dataSet.Tables.Count < 1)
                        {
                            result.ErrorMessage = "No worksheets found in the file.";
                            result.Success = false;
                            return result;
                        }

                        // Use the available worksheets
                        DataTable targetTable = null;

                        if (dataSet.Tables.Count >= 3)
                        {
                            targetTable = dataSet.Tables[2]; // Third worksheet
                        }
                        else
                        {
                            // Use the last available worksheet
                            targetTable = dataSet.Tables[dataSet.Tables.Count - 1];

                            // Show info message about using different sheet
                            result.ErrorMessage = $"Third worksheet not found. Using '{worksheetNames[dataSet.Tables.Count - 1]}' instead. Available worksheets: {string.Join(", ", worksheetNames)}";
                        }

                        DataTable filteredTable = CreateFilteredDataTableStructure();
                        int processedRows = 0;

                        foreach (DataRow row in targetTable.Rows)
                        {
                            if (ProcessDataRow(row, filteredTable))
                            {
                                processedRows++;
                            }

                            if (processedRows % 50 == 0 && worker != null)
                            {
                                worker.ReportProgress(-1, $"Processed {processedRows} products...");
                            }
                        }

                        result.Success = processedRows > 0;
                        result.DataTable = filteredTable;
                        result.RowCount = processedRows;

                        if (!result.Success)
                        {
                            result.ErrorMessage = "No valid data found in the worksheet.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Error reading Excel file: {ex.Message}";
            }

            return result;
        }
        private DataTable CreateFilteredDataTableStructure()
        {
            DataTable filtered = new DataTable();
            // Use proper data types for better performance and validation
            filtered.Columns.Add("Item Name", typeof(string));
            filtered.Columns.Add("Urdu", typeof(string));
            filtered.Columns.Add("Unit", typeof(string));
            filtered.Columns.Add("Company Rate", typeof(decimal));
            filtered.Columns.Add("Cost", typeof(decimal));
            filtered.Columns.Add("Price (R)", typeof(decimal));
            filtered.Columns.Add("Category", typeof(string));
            return filtered;
        }

        private bool ProcessDataRow(DataRow row, DataTable filteredTable)
        {
            try
            {
                // Validate required field (Item Name)
                if (row[2] == null || string.IsNullOrWhiteSpace(row[2].ToString()))
                    return false;

                DataRow newRow = filteredTable.NewRow();

                // Safely assign values with proper validation
                // Note: Column indices are 0-based from the filtered columns
                newRow["Item Name"] = GetSafeStringFromObject(row[2]);
                newRow["Urdu"] = GetSafeStringFromObject(row[3]);
                newRow["Unit"] = string.Empty; // Empty for user selection
                newRow["Company Rate"] = GetSafeDecimalFromObject(row[4]);
                newRow["Cost"] = GetSafeDecimalFromObject(row[5]);
                newRow["Price (R)"] = GetSafeDecimalFromObject(row[8]);
                newRow["Category"] = GetSafeStringFromObject(row[6]);

                filteredTable.Rows.Add(newRow);
                return true;
            }
            catch (Exception ex)
            {
                // Log error but continue processing other rows
                System.Diagnostics.Debug.WriteLine($"Error processing row: {ex.Message}");
                return false;
            }
        }

        private string GetSafeStringFromObject(object value)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                    return string.Empty;

                return value.ToString()?.Trim() ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private decimal GetSafeDecimalFromObject(object value)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                    return 0m;

                // Handle different numeric types
                if (value is double d)
                    return (decimal)d;
                if (value is int i)
                    return (decimal)i;
                if (value is decimal dec)
                    return dec;
                if (value is float f)
                    return (decimal)f;

                // Try parsing from string
                if (decimal.TryParse(value.ToString(), out decimal result))
                    return result;

                return 0m;
            }
            catch
            {
                return 0m;
            }
        }

        private void AddUnitComboBoxColumn()
        {
            // Remove existing Unit column if it exists
            if (ProductDataGrid.Columns.Contains("Unit"))
                ProductDataGrid.Columns.Remove("Unit");

            if (ProductDataGrid.Columns.Contains("UnitColumn"))
                return;

            DataGridViewComboBoxColumn unitColumn = new DataGridViewComboBoxColumn();
            unitColumn.HeaderText = "Unit";
            unitColumn.Name = "UnitColumn";
            unitColumn.DataPropertyName = "Unit";
            unitColumn.Items.AddRange(new object[]
            {
        "عدد", "ڈبہ", "درجن", "کارٹن", "پیکٹ", "رول",
        "گز", "بنڈل", "ڈبی", "کلو", "جوڑی", "سابقہ"
            });

            // Find Urdu column index and insert Unit column after it
            if (ProductDataGrid.Columns.Contains("Urdu"))
            {
                int urduIndex = ProductDataGrid.Columns["Urdu"].Index;
                ProductDataGrid.Columns.Insert(urduIndex + 1, unitColumn);
            }
            else
            {
                // Fallback: add at the end
                ProductDataGrid.Columns.Add(unitColumn);
            }
        }

        private void SetUIState(bool enabled)
        {
            loadDataBtn.Enabled = enabled;
            // Only enable Import button if we have data
            ImportToDbBtn.Enabled = enabled && (ProductDataGrid.DataSource != null);

            if (!enabled)
            {
                progressBar1.Visible = true;
                progressBar1.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                progressBar1.Visible = false;
            }
        }

        // Helper class for streaming results
        private class StreamingResult
        {
            public bool Success { get; set; }
            public DataTable DataTable { get; set; }
            public int RowCount { get; set; }
            public string ErrorMessage { get; set; }
        }

        // Optional: Add this method to handle very large files with manual garbage collection
        private void OptimizeMemoryUsage()
        {
            // Force garbage collection to free up memory
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        // Optional: Add this method for quick testing with small files
        private void ReadExcelTraditional(string filePath)
        {
            // Only use this for small files (< 10MB)
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
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

                    if (dataSet.Tables.Count < 3)
                    {
                        MessageBox.Show("Third worksheet not found in the file.", "No data",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var currentTable = dataSet.Tables[2];
                    DataTable filtered = CreateFilteredDataTableStructure();

                    foreach (DataRow row in currentTable.Rows)
                    {
                        if (row[2] == DBNull.Value || row[2].ToString() == "Item Name")
                            continue;

                        filtered.Rows.Add(
                            row[2],  // Item Name
                            row[3],  // Urdu
                            null,    // Unit
                            row[4],  // Company Rate
                            row[5],  // Cost
                            row[8],  // Price (R)
                            row[6]   // Category
                        );
                    }

                    ProductDataGrid.DataSource = filtered;
                    AddUnitComboBoxColumn();
                    ImportToDbBtn.Enabled = true;
                }
            }
        }
        private void ImportToDbBtn_Click(object sender, EventArgs e)
        {
            if (ProductDataGrid.Rows.Count != 0 && ProductDataGrid.Rows != null)
            {
                try
                {
                    LoadingManager.ShowLoading();
                    DataTable dataTable = (DataTable)ProductDataGrid.DataSource;
                    if (dataTable == null || dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No data to import. Please load data from an Excel file first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (var context = new POSDbContext())
                    {
                        var ProductToAddList = new List<Models.Product>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row.IsNull("Item Name") || string.IsNullOrEmpty(row["Item Name"].ToString()))
                                continue;

                            ProductToAddList.Add(new Models.Product()
                            {
                                Cost = GetIntOrDefault(row["Cost"]),
                                ProductEnglishName = GetStringOrNull(row["Item Name"]),

                                ProductUrduName = GetStringOrNull(row["Urdu"]),
                                ProductType = GetStringOrNull(row["Unit"]),
                                //PurchasePrice = GetIntOrDefaultFromDecimalValue(row["Company Rate"]),
                                PurchasePrice = GetStringOrNull(row["Company Rate"]),
                                // Changed to int?
                                SalePrice = GetIntOrDefault(row["Price (R)"]),
                                SearchByProductCode=GetStringOrNull(row["Category"]),
                                SubcategoryId = 1
                            });
                        }

                        context.Products.AddRange(ProductToAddList);
                        int savedRecords = context.SaveChanges();
                        LoadingManager.HideLoading();

                        MessageBox.Show($"Successfully imported {savedRecords} records to database!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    LoadingManager.HideLoading();

                 
                }
            }else
                MessageBox.Show("Please Upload the Products first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private int GetIntOrDefault(object value, int defaultValue = 0)
        {
            if (value == DBNull.Value || value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return defaultValue;

            if (int.TryParse(value.ToString(), out int result))
                return result;
            return defaultValue;
        }

        private int GetIntOrDefaultFromDecimalValue(object value, int defaultValue = 0)
        {
            if (value == DBNull.Value || value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return defaultValue;

            if (decimal.TryParse(value.ToString(), out decimal result))
                return (int)result;
            return defaultValue;
        }

        // Helper methods for handling null values
        private string GetStringOrNull(object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            return value.ToString();
        }

        private decimal? GetNullableDecimal(object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            if (decimal.TryParse(value.ToString(), out decimal result))
                return result;

            return null;
        }

        private void BrowsUpdatedExcelFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Set the filter to show only .bak files
            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|All files|*.*";
            ofd.Title = "Select an Excel File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImportUpdatedFilePathTxt.Text = ofd.FileName;
                LoadUpdatedDataBtn.Enabled = true;
            }
        }

        private void LoadUpdatedDataBtn_Click(object sender, EventArgs e)
        {
            using (var stream = File.Open(ImportUpdatedFilePathTxt.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                SaveUpdatedPriceBtn.Visible = true;
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

                    DataTable filtered = new DataTable();
                    // Add only required columns
                    filtered.Columns.Add("Product ID");
                    filtered.Columns.Add("Product Name");
                    filtered.Columns.Add("Urdu Name");
                    filtered.Columns.Add("SearchByProductName");
                    filtered.Columns.Add("Type");
                    filtered.Columns.Add("Purchase Price");
                    filtered.Columns.Add("Sale Price");
                    filtered.Columns.Add("Cost");
                    filtered.Columns.Add("SubCategory");
                    

                    // Copy rows
                    foreach (DataRow row in currentTable.Rows)
                    {
                        //// Skip rows that are empty or header duplicates
                        if (row[0] == DBNull.Value || row[0].ToString() == "ProductID")
                            continue;
                        filtered.Rows.Add(
                            row[0],  
                            row[1],  
                            row[2],  
                            row[3],  
                            row[4],   
                            row[5],
                            row[6],
                            row[7],
                            row[8]
                        );
                    }

                    updatedProductLIstGrid.DataSource = filtered;
                    updatedProductLIstGrid.AllowUserToAddRows = false;
                    DataGridViewComboBoxColumn typeColumn = new DataGridViewComboBoxColumn();
                    typeColumn.HeaderText = "Type";
                    typeColumn.Name = "Type";
                    typeColumn.DataPropertyName = "Type"; // This is the key - it binds to the DataTable column
                    typeColumn.Items.AddRange(new object[]
                    {
                            "عدد",
                            "ڈبہ",
                            "درجن",
                            "کارٹن",
                            "پیکٹ",
                            "رول",
                            "گز",
                            "بنڈل",
                            "ڈبی",
                            "کلو",
                            "جوڑی",
                            "سابقہ"
                    });

                    // Remove original and add ComboBox
                    int typeIndex = updatedProductLIstGrid.Columns["Type"].Index;
                    updatedProductLIstGrid.Columns.Remove("Type");
                    updatedProductLIstGrid.Columns.Insert(typeIndex, typeColumn);

                    // Add Delete button column
                    DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                    deleteButtonColumn.HeaderText = "Action";
                    deleteButtonColumn.Name = "Delete";
                    deleteButtonColumn.Text = "Delete";
                    deleteButtonColumn.UseColumnTextForButtonValue = true;
                    updatedProductLIstGrid.Columns.Add(deleteButtonColumn);

                    // Handle the button click event
                    updatedProductLIstGrid.CellClick += (sender1, e1) =>
                    {
                        if (e1.ColumnIndex == updatedProductLIstGrid.Columns["Delete"].Index && e1.RowIndex >= 0)
                        {
                            // Confirm deletion
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this row?",
                                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                // Remove the row from the DataTable
                                DataRowView rowView = (DataRowView)updatedProductLIstGrid.Rows[e1.RowIndex].DataBoundItem;
                                DataRow rowToDelete = rowView.Row;

                                // Remove from DataTable
                                ((DataTable)updatedProductLIstGrid.DataSource).Rows.Remove(rowToDelete);

                                // Optional: Refresh the grid
                                updatedProductLIstGrid.Refresh();
                            }
                        }
                    };
                }

            }
        }

        private void SaveUpdatedPriceBtn_Click(object sender, EventArgs e)
        {
            if (updatedProductLIstGrid.Rows.Count != 0 && updatedProductLIstGrid.Rows != null)
            {
                try
                {
                    LoadingManager.ShowLoading();
                    DataTable dataTable = (DataTable)updatedProductLIstGrid.DataSource;
                    if (dataTable == null || dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No data to import. Please load data from an Excel file first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (var context = new POSDbContext())
                    {
                        int updatedCount = 0;
                        int addedCount = 0;
                        var ProductToAddList = new List<Models.Product>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row.IsNull("Product ID") || string.IsNullOrEmpty(row["Product Name"].ToString()))
                                continue;

                            int productId = Convert.ToInt32(row["Product ID"]);
                            //var existingProduct = context.Products.Find(productId);
                            var pName = row[1].ToString();
                            var existingProduct = context.Products.Where(s => s.ProductEnglishName == pName).FirstOrDefault();

                            if (existingProduct != null)
                            {
                                // Update existing product
                                existingProduct.ProductEnglishName = GetStringOrNull(row["Product Name"]);
                                existingProduct.ProductUrduName = GetStringOrNull(row["Urdu Name"]);
                                existingProduct.ProductType = GetStringOrNull(row["Type"]);
                                existingProduct.SearchByProductCode = GetStringOrNull(row["SearchByProductName"]);
                                //existingProduct.PurchasePrice = GetIntOrDefault(row["Purchase Price"]);
                                existingProduct.PurchasePrice = GetStringOrNull(row["Purchase Price"]);
                                existingProduct.SalePrice = GetIntOrDefault(row["Sale Price"]);
                                existingProduct.Cost = Convert.ToInt32(row["Cost"]);
                                existingProduct.SubcategoryId = Convert.ToInt32(row["SubCategory"]);

                                context.Entry(existingProduct).State = EntityState.Modified;
                                updatedCount++;
                            }
                            else
                            {
                                // Add new product
                                var newProduct = new Models.Product
                                {
                                    Id = productId,
                                    ProductEnglishName = GetStringOrNull(row["Product Name"]),
                                    ProductUrduName = GetStringOrNull(row["Urdu Name"]),
                                    ProductType = GetStringOrNull(row["Type"]),
                                    //PurchasePrice = GetIntOrDefault(row["Purchase Price"]),
                                    PurchasePrice = GetStringOrNull(row["Purchase Price"]),
                                    SalePrice = GetIntOrDefault(row["Sale Price"]),
                                    Cost = Convert.ToInt32(row["Cost"]),
                                    SubcategoryId = Convert.ToInt32(row["SubCategory"]),
                                    SearchByProductCode = GetStringOrNull(row["SearchByProductName"])
                                };

                                ProductToAddList.Add(newProduct);
                                addedCount++;
                            }
                        }

                        if (ProductToAddList.Count > 0)
                            context.Products.AddRange(ProductToAddList);
                        int savedRecords = context.SaveChanges();

                        LoadingManager.HideLoading();
                        MessageBox.Show($"Successfully imported {(addedCount+updatedCount)} records to database!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
                catch (Exception)
                {
                    LoadingManager.HideLoading();
                    throw;
                }
            }
            else
            {
                MessageBox.Show($"Please Upload the Products first",
                          "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
               
            
        }


        //public void LoadDataFromExcel(string filePath, string ext, string hdr)
        //{

        //    //string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR="+hdr+";'";

        //    string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR={1}'";
        //    con= string.Format(con, filePath, hdr);

        //    OleDbConnection excelcon = new OleDbConnection(con);
        //    excelcon.Open();
        //    DataTable dataTable= excelcon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //    string excelSheetName = dataTable.Rows[0]["TABLE_NAME"].ToString();
        //    OleDbCommand com = new OleDbCommand("Select * from [" + excelSheetName + "]", excelcon);
        //    OleDbDataAdapter da = new OleDbDataAdapter(com);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    excelcon.Close();
        //   ProductDataGrid.DataSource= dt;
        //}
        //public void LoadDataFromExcel(string filePath)
        //{

        //    string con ="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";


        //    // Implement the logic to load data from the Excel file
        //    // You can use libraries like EPPlus, ClosedXML, or Interop to read Excel files
        //    // Example using EPPlus (make sure to install the EPPlus NuGet package):
        //    /*
        //    using (var package = new ExcelPackage(new FileInfo(filePath)))
        //    {
        //        var worksheet = package.Workbook.Worksheets[0]; // Get the first worksheet
        //        var rowCount = worksheet.Dimension.Rows;
        //        var colCount = worksheet.Dimension.Columns;
        //        for (int row = 1; row <= rowCount; row++)
        //        {
        //            for (int col = 1; col <= colCount; col++)
        //            {
        //                var cellValue = worksheet.Cells[row, col].Text;
        //                // Process the cell value as needed
        //            }
        //        }
        //    }
        //    */
        //}
    }
}
