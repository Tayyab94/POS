using ClosedXML.Excel;
using POS_Shop.Helpers;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using POS_Shop.Views.DB_Screens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Product
{
    public partial class ProductListControl : UserControl
    {

        private int PageSize = 100;
        private int? CurrentCursor = null; // Current page's last ID
        private int? PreviousCursor = null; // Previous page's last ID
        private Stack<int> CursorHistory = new Stack<int>(); // Track navigation history
        private int RecordCount = 0;
        private string SearchTerm = "";
        private bool IsFirstPage = true;
        private bool HasMoreRecords = false;

        // Store selected IDs across all pages
        private HashSet<int> selectedProductIds = new HashSet<int>();

        public ProductListControl()
        {
            InitializeComponent();
            this.Load += ProductFromControl_Load;

        }

        private async void ProductFromControl_Load(object sender, EventArgs e)
        {
            LoadingManager.ShowLoading();
            await LoadProductsForDataGridView();

            LoadingManager.HideLoading();
        }


        private async Task LoadProductsForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                IProductRepository productRepository = new ProductRepository(context);
                //var result = await productRepository.GetProductPagingListAsync(PageIndex, PageSize, SearchTerm);
                //RecordCount = result.totalCount;

                var result = await productRepository.GetProductCursorPagingListAsync(CurrentCursor, PageSize, SearchTerm);
                RecordCount = result.totalCount;
                HasMoreRecords = result.hasMore;

                // Update cursor for next page
                if (result.data.Any())
                {
                    CurrentCursor = result.data.Last().Id;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("IsSelected", typeof(bool)); // Add selection column
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("UName", typeof(string));
                dt.Columns.Add("SearchBy", typeof(string));
                dt.Columns.Add("Qty", typeof(int));
                dt.Columns.Add("Cost", typeof(int));
                dt.Columns.Add("Purchase-Price", typeof(string));
                dt.Columns.Add("SalePrice", typeof(string));

                foreach (var item in result.data)
                {
                    // Check if this product is in our selected list
                    bool isSelected = selectedProductIds.Contains(item.Id);
                    dt.Rows.Add(isSelected, item.Id, item.ProductEnglishName, TextFormatHelper.FormatMixedText(item.ProductUrduName), item.SearchByProductCode, item.Qty,
                           item.Cost, item.PurchasePrice, item.SalePrice);
                }

                ProductListGrid.ReadOnly = false;
                ProductListGrid.AllowUserToAddRows = false;
                ProductListGrid.AutoGenerateColumns = false;
                ProductListGrid.DataSource = dt;
                ConfigureDataGridView();
                UpdatePager();
            }
        }
        private void ConfigureDataGridView()
        {
            // Clear existing columns if any
            ProductListGrid.Columns.Clear();

            // Checkbox column bound to DataTable field
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
            {
                Name = "IsSelected",
                DataPropertyName = "IsSelected",
                HeaderText = "",
                Width = 30,
                ReadOnly = false,
                FlatStyle = FlatStyle.Standard
            };
            ProductListGrid.Columns.Add(chk);

            // Add other columns
            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ID",
                DataPropertyName = "ID",
                HeaderText = "ID",
                Width = 40,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Product Name",
                Width = 200,
                ReadOnly = true
            });
            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "UName",
                DataPropertyName = "UName",
                HeaderText = "Urdu Name",
                Width = 200,
                ReadOnly = true
            });
            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SearchBy",
                DataPropertyName = "SearchBy",
                HeaderText = "SearchByName",
                Width = 200,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Qty",
                DataPropertyName = "Qty",
                HeaderText = "Qty",
                Width = 200,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Type",
                DataPropertyName = "Type",
                HeaderText = "Type",
                Width = 70,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Cost",
                DataPropertyName = "Cost",
                HeaderText = "Cost",
                Width = 50,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N0" }
            });



            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PurchasePrice",
                DataPropertyName = "Purchase-Price",
                HeaderText = "Purchase Price",
                Width = 60,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2" }
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SalePrice",
                DataPropertyName = "SalePrice",
                HeaderText = "Sale Price",
                Width = 60,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2" }
            });


            // Edit button column
            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn()
            {
                Name = "Edit",
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 50
            };
            ProductListGrid.Columns.Add(editColumn);

            // Delete button column
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn()
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 50
            };
            ProductListGrid.Columns.Add(deleteColumn);



            // Configure grid properties
            ProductListGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            chk.Width = 30;


            editColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            editColumn.Width = 50;
            deleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            deleteColumn.Width = 50;


            ProductListGrid.AllowUserToResizeColumns = true;
            ProductListGrid.AllowUserToResizeRows = false;
            ProductListGrid.RowHeadersVisible = false;
            ProductListGrid.BackgroundColor = SystemColors.Window;
            ProductListGrid.BorderStyle = BorderStyle.None;
            ProductListGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            ProductListGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            ProductListGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            ProductListGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
            ProductListGrid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            ProductListGrid.EnableHeadersVisualStyles = false;

            // Subscribe to events
            ProductListGrid.CellValueChanged += ProductListGrid_CellValueChanged;
            ProductListGrid.CurrentCellDirtyStateChanged += ProductListGrid_CurrentCellDirtyStateChanged;
        }


        // Then modify the CellClick event handler:
        private void ProductListGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Header row clicked

            var grid = (DataGridView)sender;
            int productId = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value);
            string productName = grid.Rows[e.RowIndex].Cells["Name"].Value.ToString();

            if (e.ColumnIndex == grid.Columns["Edit"].Index)
            {

                //updateProductBtn.Visible = true;
                //ProductSaveBtn.Enabled = false;

                //ProductFormLbl.Text = "Edit Product";
                //GetAndBindProductForEdit(productId);

                //MessageBox.Show($"Prod ID :{productId} and NAme :{productName} for Edit");

                var form = new NewProductForm(productId);
                form.ShowDialog();
            }
            else if (e.ColumnIndex == grid.Columns["Delete"].Index)
            {

                DeleteProductById(productId);

            }else
            {
                // Select the entire row
                ProductListGrid.Rows[e.RowIndex].Selected = true;
                // Optional: Highlight the row
                ProductListGrid.CurrentCell = ProductListGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                btnManagePrices.Enabled = true;
                btnDeleteProduct.Enabled = true;
            }
        }


        //private void GetAndBindProductForEdit(int productId)
        //{

        //    using (var context = new POSDbContext())
        //    {
        //        var productRepo = new ProductRepository(context);
        //        var product = productRepo.GetById(productId);
        //        if (product != null)
        //        {
        //            productIdTxt.Text = product.Id.ToString();
        //            // Populate form fields for editing
        //            ProductEngNameTxt.Text = product.ProductEnglishName;
        //            ProductUrduNameTxt.Text = product.ProductUrduName;
        //            PurchasePriceTxt.Text = string.IsNullOrEmpty(product.PurchasePrice) ? "0" : product.PurchasePrice.ToString();
        //            p_costTxt.Text = product.Cost.ToString();
        //            P_SalePriceTxt.Text = product.SalePrice.ToString();
        //            productTypeDropdown.SelectedItem = product.ProductType;
        //            SearchBynameTxt.Text = product.SearchByProductCode;
        //            P_StockQtyTxt.Text = product.Qty.ToString();
        //            // Set category and subcategory dropdowns
        //            var subCategory = context.SubCategories.Find(product.SubcategoryId);
        //            if (subCategory != null)
        //            {
        //                var category = context.Categories.Find(subCategory.categoryId);
        //                if (category != null)
        //                {
        //                    CategoryDropDownLst.SelectedValue = category.id;
        //                    // This will trigger loading of subcategories
        //                    SubCategoryCategoryDropDownLst.SelectedValue = subCategory.id;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Product not found for editing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private async void DeleteProductById(int productId)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                using (var context = new POSDbContext())
                {
                    var productRepo = new ProductRepository(context);
                    var product = productRepo.GetById(productId);
                    if (product != null)
                    {
                        productRepo.Delete(productId);
                        productRepo.Save();
                        MessageBox.Show("Product deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        selectedProductIds.Remove(productId);
                        await LoadProductsForDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Product not found for deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void ProductListGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Commit changes immediately when checkbox is toggled
            if (ProductListGrid.CurrentCell is DataGridViewCheckBoxCell)
            {
                ProductListGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void ProductListGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Only handle checkbox column

            var grid = (DataGridView)sender;
            var checkboxCell = grid.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;
            var idCell = grid.Rows[e.RowIndex].Cells["ID"];

            if (checkboxCell != null && idCell != null && idCell.Value != null)
            {
                bool isChecked = Convert.ToBoolean(checkboxCell.Value);
                int productId = Convert.ToInt32(idCell.Value);

                if (isChecked)
                {
                    selectedProductIds.Add(productId);
                }
                else
                {
                    selectedProductIds.Remove(productId);
                }

                // Update status to show selected count
                UpdateSelectionStatus();
            }
        }




        private void UpdateSelectionStatus()
        {
            if (selectedProductIds.Count <= 0)
            {
                ClearAllSelectionBtn.Visible = false;
            }
            else
            {
                ClearAllSelectionBtn.Visible = true;
            }

            selectedProdLbl.Text = $"Selected: {selectedProductIds.Count} product(s)";
        }

        private void UpdatePager()
        {

            int currentPage = CursorHistory.Count + 1;
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);

            lblStatus.Text = $"Page {currentPage} of {totalPages} | Total Records: {RecordCount}";

            PreviousPageBtn.Enabled = !IsFirstPage && CursorHistory.Count > 0;
            NextPageBtn.Enabled = HasMoreRecords;

            // Update selection status
            UpdateSelectionStatus();
        }

        // Method to get all selected IDs
        public List<int> GetSelectedProductIds()
        {
            return selectedProductIds.ToList();
        }

        // Method to clear selection
        public async void ClearSelection()
        {
            selectedProductIds.Clear();
            // Reload current page to update checkboxes
            await LoadProductsForDataGridView();
        }

        // Add these button click handlers if you want select all/clear all functionality
        private async void btnSelectAll_Click(object sender, EventArgs e)
        {
            // Get all IDs on current page and add to selection
            var dataTable = (DataTable)ProductListGrid.DataSource;
            foreach (DataRow row in dataTable.Rows)
            {
                int productId = Convert.ToInt32(row["ID"]);
                selectedProductIds.Add(productId);
            }

            // Reload to update checkboxes
            await LoadProductsForDataGridView();
        }



        // Your existing pagination and search methods
        private async void ProdSearchTxt_TextChanged(object sender, EventArgs e)
        {

            SearchTerm = ProdSearchTxt.Text.Trim();
            ResetPagination();
            await LoadProductsForDataGridView();
        }

        // Reset pagination (call when search changes)
        private void ResetPagination()
        {
            CurrentCursor = null;
            PreviousCursor = null;
            CursorHistory.Clear();
            IsFirstPage = true;
            HasMoreRecords = false;
        }


        private async void NextPageBtn_Click(object sender, EventArgs e)
        {

            if (HasMoreRecords && CurrentCursor.HasValue)
            {
                // Save current cursor to history
                CursorHistory.Push(CurrentCursor.Value);
                IsFirstPage = false;

                await LoadProductsForDataGridView();
            }
        }

        private async void PreviousPageBtn_Click(object sender, EventArgs e)
        {
            if (CursorHistory.Count > 0)
            {
                // Pop the last cursor
                CursorHistory.Pop();

                if (CursorHistory.Count > 0)
                {
                    // Set cursor to previous page
                    CurrentCursor = CursorHistory.Peek();
                }
                else
                {
                    // Back to first page
                    CurrentCursor = null;
                    IsFirstPage = true;
                }

                await LoadProductsForDataGridView();
            }
        }

        private async void SelectAllBtn_Click(object sender, EventArgs e)
        {
            // Select all checkboxes
            UpdateAllCheckboxes(true);
            UpdateSelectionStatus();

        }
        // Helper method to update all checkboxes based on selectedProductIds
        private void UpdateAllCheckboxes(bool isSelected)
        {
            // Method 1: Update via DataTable (Recommended)
            //var dataTable = ProductListGrid.DataSource as DataTable;
            //if (dataTable != null)
            //{
            //    // Update all rows in the DataTable
            //    foreach (DataRow row in dataTable.Rows)
            //    {
            //        int productId = Convert.ToInt32(row["ID"]);
            //        row["IsSelected"] = isSelected;

            //        // Update the selection list
            //        if (isSelected && !selectedProductIds.Contains(productId))
            //        {
            //            selectedProductIds.Add(productId);
            //        }
            //        else if (!isSelected)
            //        {
            //            selectedProductIds.Remove(productId);
            //        }
            //    }

            //    // Force the DataGridView to refresh
            //    ProductListGrid.Invalidate();
            //    ProductListGrid.Refresh();

            //    // Alternatively, rebind the data source
            //    // ProductListGrid.DataSource = null;
            //    // ProductListGrid.DataSource = dataTable;
            //}

            // Method 2: Update via DataGridView directly (Backup)

            foreach (DataGridViewRow row in ProductListGrid.Rows)
            {
                if (row.IsNewRow) continue;

                var chkCell = row.Cells["IsSelected"] as DataGridViewCheckBoxCell;
                if (chkCell != null)
                {
                    chkCell.Value = isSelected;
                }

                int productId = Convert.ToInt32(row.Cells["ID"].Value);
                if (isSelected && !selectedProductIds.Contains(productId))
                {
                    selectedProductIds.Add(productId);
                }
                else if (!isSelected)
                {
                    selectedProductIds.Remove(productId);
                }
            }

        }
        private async void ClearAllSelectionBtn_Click(object sender, EventArgs e)
        {
            //ClearSelection();

            // Clear all checkboxes
            //  UpdateAllCheckboxes(false);

            // Update UI
            // UpdateSelectionStatus();
            selectedProductIds.RemoveWhere(id => true);
            selectedProdLbl.Text = $"Selected: {selectedProductIds.Count} Customer(s)";
            CurrentCursor = 0;
            await LoadProductsForDataGridView();
        }

        private async void ExportProdBtn_Click(object sender, EventArgs e)
        {
            if (selectedProductIds.Count == 0)
            {
                MessageBox.Show("No products selected for export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            using (var context = new POSDbContext())
            {
                var productRepo = new ProductRepository(context);
                //var selectedProducts = productRepo.GetAll(selectedProductIds.ToList()).Result;
                var selectedProducts = await productRepo.GetAll(selectedProductIds.ToList());
                if (selectedProducts.Count() > 0)
                {
                    DataTable exportTable = new DataTable();
                    exportTable.TableName = "Products";

                    // Add columns
                    exportTable.Columns.Add("ProductID", typeof(int));
                    exportTable.Columns.Add("ProductName", typeof(string));
                    exportTable.Columns.Add("UrduName", typeof(string));
                    exportTable.Columns.Add("SearchByProductName", typeof(string));
                
                    exportTable.Columns.Add("PurchasePrice", typeof(string));
                    exportTable.Columns.Add("SalePrice", typeof(decimal));
                    exportTable.Columns.Add("Cost", typeof(int));
                    exportTable.Columns.Add("SubCategory", typeof(int));
                    exportTable.Columns.Add("ProductOldName", typeof(string));
                    exportTable.Columns.Add("Qty", typeof(int));

                    // Add rows
                    foreach (var product in selectedProducts)
                    {
                        exportTable.Rows.Add(
                            product.Id,
                            product.ProductEnglishName,
                            product.ProductUrduName,
                            product.SearchByProductCode,
                            product.PurchasePrice,
                            product.SalePrice,
                            product.Cost,
                            product.SubcategoryId,
                            product.ProductEnglishName,
                            product.Qty
                        );
                    }

                    // 3. Ask where to save the file
                    using (var sfd = new SaveFileDialog
                    {
                        Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                        FileName = "SelectedProducts.xlsx"
                    })
                    {
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            // 4. Write to Excel using ClosedXML
                            using (var workbook = new XLWorkbook())
                            {
                                workbook.Worksheets.Add(exportTable, "Products");
                                workbook.SaveAs(sfd.FileName);
                            }
                            MessageBox.Show("Export successful!");
                        }
                    }
                    // Export logic here - for demo, we'll just show count
                    MessageBox.Show($"{selectedProducts.Count()} products ready for export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearSelection();
                }
                else
                {
                    MessageBox.Show("No products found for the selected IDs.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void ImportFilBtn_Click(object sender, EventArgs e)
        {
            LoadingManager.ShowLoading();
            ImportExcelFile importExcelForm = new ImportExcelFile();
            importExcelForm.Show();

            LoadingManager.HideLoading();
        }

        private async void AddNewProductFormBtn_Click(object sender, EventArgs e)
        {
            var form = new NewProductForm();
            form.ShowDialog();

        }

        private void btnManagePrices_Click(object sender, EventArgs e)
        {

            // Simple check for any selection
            if (ProductListGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product first.", "No Selection",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = ProductListGrid.SelectedRows[0];

            // Quick validation
            if (row.Cells["ID"].Value == null ||
                !int.TryParse(row.Cells["ID"].Value.ToString(), out int productId))
            {
                MessageBox.Show("Invalid product selection.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get product name (adjust column name as needed)
            string productName = row.Cells["Name"].Value?.ToString();

            //// Open form
            //using (var priceForm = new NewProductForm(productId))
            //{
            //    if (priceForm.ShowDialog() == DialogResult.OK)
            //    {
            //        MessageBox.Show("Prices updated successfully!", "Success",
            //                      MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}

            // Open price management form
            using (var priceForm = new EditProdPricesForm(productId, productName))
            {
                if (priceForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Product prices updated successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {

            // Simple check for any selection
            if (ProductListGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product first.", "No Selection",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = ProductListGrid.SelectedRows[0];

            // Quick validation
            if (row.Cells["ID"].Value == null ||
                !int.TryParse(row.Cells["ID"].Value.ToString(), out int productId))
            {
                MessageBox.Show("Invalid product selection.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DeleteProductById(productId);

        }
    }
}
