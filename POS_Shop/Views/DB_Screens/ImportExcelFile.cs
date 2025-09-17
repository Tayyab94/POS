using ExcelDataReader;
using POS_Shop.Helpers;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.DB_Screens
{
    public partial class ImportExcelFile : Form
    {
        public ImportExcelFile()
        {
            InitializeComponent();
            this.Load += ImportExcelFile_Load;
            bindingSource = new BindingSource();
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
        private void loadDataBtn_Click(object sender, EventArgs e)
        {
            using (var stream = File.Open(ImportFilePathTxt.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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


                   var currentTable = dataSet.Tables[2];

                    DataTable filtered = new DataTable();
                    // Add only required columns
                    filtered.Columns.Add("Item Name");
                    filtered.Columns.Add("Urdu");
                    filtered.Columns.Add("Unit");
                    filtered.Columns.Add("Company Rate");
                    filtered.Columns.Add("Cost");
                    filtered.Columns.Add("Price (R)");
                    //var selectedColumns = new[] { "Item Name", "Urdu", "Company Rate", "Cost", "Price (R)" };
                    //DataTable filteredTable = currentTable.DefaultView.ToTable(false, selectedColumns);
                    //ProductDataGrid.DataSource = filteredTable;

                    // Copy rows
                    foreach (DataRow row in currentTable.Rows)
                    {
                        // Skip rows that are empty or header duplicates
                        if (row[2] == DBNull.Value || row[2].ToString() == "Item Name")
                            continue;
                        filtered.Rows.Add(
                            row[2],  // Item Name (3rd col in Excel)
                            row[3],  // Urdu
                              null,    // Unit (empty for now, user selects)
                            row[4],  // Company Rate
                            row[5],  // Cost
                            row[8]   // Price (R)
                        );
                    }

                    ProductDataGrid.DataSource = filtered;

                    DataGridViewComboBoxColumn unitColumn = new DataGridViewComboBoxColumn();
                    unitColumn.HeaderText = "Unit";
                    unitColumn.Name = "Unit";
                    unitColumn.DataPropertyName = "Unit"; // (optional if binding to DataTable)
                    unitColumn.Items.AddRange(new object[]
                    {
                            "عدد",
                            "ڈبہ",
                            "درجن",
                            "کارٹن",
                            "پیکٹ",
                            "رول",
                            "گز"
                    });

                    // Find index of Urdu column and insert Unit right after it
                    int urduIndex = ProductDataGrid.Columns["Urdu"].Index;
                    ProductDataGrid.Columns.Remove("Unit");
                    ProductDataGrid.Columns.Insert(urduIndex + 1, unitColumn);
                
                 
                }

            }

            ImportToDbBtn.Enabled = true;
            //LoadDataFromExcel(ImportFilePathTxt.Text, ".xlsx", "YES");
        }

        private void ImportToDbBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingManager.ShowLoading();
                DataTable dataTable = (DataTable)ProductDataGrid.DataSource;
                if(dataTable == null || dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No data to import. Please load data from an Excel file first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using(var context = new POSDbContext())
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
                            PurchasePrice = GetNullableDecimal(row["Company Rate"]),
                            // Changed to int?
                            SalePrice = GetNullableDecimal(row["Price (R)"])
                        });
                    }

                    context.Products.AddRange(ProductToAddList);
                    int savedRecords = context.SaveChanges();

                    MessageBox.Show($"Successfully imported {savedRecords} records to database!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private int GetIntOrDefault(object value, int defaultValue = 0)
        {
            if (value == DBNull.Value || value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return defaultValue;

            if (int.TryParse(value.ToString(), out int result))
                return result;
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
                            row[7]
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
                                "گز"
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
                        var existingProduct = context.Products.Where(s=>s.ProductEnglishName== pName).FirstOrDefault();

                        if (existingProduct != null)
                        {
                            // Update existing product
                            existingProduct.ProductEnglishName = GetStringOrNull(row["Product Name"]);
                            existingProduct.ProductUrduName = GetStringOrNull(row["Urdu Name"]);
                            existingProduct.ProductType = GetStringOrNull(row["Type"]);
                            existingProduct.PurchasePrice = GetNullableDecimal(row["Purchase Price"]);
                            existingProduct.SalePrice = GetNullableDecimal(row["Sale Price"]);
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
                                PurchasePrice = GetNullableDecimal(row["Purchase Price"]),
                                SalePrice = GetNullableDecimal(row["Sale Price"]),
                                Cost = Convert.ToInt32(row["Cost"]),
                                SubcategoryId = Convert.ToInt32(row["SubCategory"])
                            };

                            ProductToAddList.Add(newProduct);
                            addedCount++;
                        }
                    }
                  
                    if(ProductToAddList.Count > 0)
                        context.Products.AddRange(ProductToAddList);
                    int savedRecords = context.SaveChanges();

                    LoadingManager.HideLoading();
                    MessageBox.Show($"Successfully imported {savedRecords} records to database!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                LoadingManager.HideLoading();
                throw;
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
