//using POS_Shop.Interfaces;
//using POS_Shop.Models;
//using POS_Shop.Repositories;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Product
//{
//    public partial class ProductFromControl : UserControl
//    {
//        private int PageSize = 100;
//        private int PageIndex = 1;
//        private int RecordCount = 0;
//        private string SearchTerm = "";


//        public ProductFromControl()
//        {
//            InitializeComponent();
//            this.Load += ProductFromControl_Load;
//        }

//        private async void ProductFromControl_Load(object sender, EventArgs e)
//        {

//            loadCategiryForDropdown(); // If this is sync, keep it here
//           await LoadProductsForDataGridView(); // Now you can await it safely
//            productTypeDropdown.SelectedItem = "عدد";
//        }


//        private void loadCategiryForDropdown()
//        {
//            // unsubscribe first to avoid multiple subscriptions
//            CategoryDropDownLst.SelectedIndexChanged -= CategoryDropdown_SelectedIndexChanged;

//            using (var context = new POSDbContext())
//            {
//                var categoriesList = context.Categories.Select(s=> new
//                {
//                    Id = s.id,
//                    Name = s.name
//                }).ToList();

//                // Add default option
//                var allItems = new List<object>();
//                allItems.Add(new { Id = 0, Name = "Select Category" });
//                allItems.AddRange(categoriesList);
//                CategoryDropDownLst.DataSource = allItems;
//                CategoryDropDownLst.DisplayMember = "Name";
//                CategoryDropDownLst.ValueMember = "Id";
//            }

//            // Subscribe AFTER data is loaded
//            CategoryDropDownLst.SelectedIndexChanged += CategoryDropdown_SelectedIndexChanged;
//        }

//        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            // Check if SelectedValue is null or the default option
//            if (CategoryDropDownLst.SelectedValue == null ||
//                Convert.ToInt32(CategoryDropDownLst.SelectedValue) == 0)
//            {
//                // Clear subcategory dropdown if no category selected
//                SubCategoryCategoryDropDownLst.DataSource = null;
//                SubCategoryCategoryDropDownLst.Items.Clear();
//                return;
//            }

//            // Get the selected ID as integer
//            int selectedId = Convert.ToInt32(CategoryDropDownLst.SelectedValue);

//            // Load subcategories based on the selected category ID
//            using (var context = new POSDbContext())
//            {
//                var subCategoriesList = context.SubCategories
//                    .Where(s => s.categoryId == selectedId).Select(s => new
//                    {
//                        Id = s.id,
//                        Name = s.name
//                    })
//                    .ToList();

//                // Add default option for subcategories
//                var allSubItems = new List<object>();
//                allSubItems.Add(new { Id = 0, Name = "Select SubCategory" });
//                allSubItems.AddRange(subCategoriesList);
//                SubCategoryCategoryDropDownLst.DataSource = allSubItems;
//                SubCategoryCategoryDropDownLst.DisplayMember = "Name";
//                SubCategoryCategoryDropDownLst.ValueMember = "Id";
//                SubCategoryCategoryDropDownLst.SelectedIndex = 0;

//            }
//        }

//        private void ProductSaveBtn_Click(object sender, EventArgs e)
//        {
//            var model = new Models.Product()
//            {
//                ProductEnglishName = ProductEngNameTxt.Text,
//                ProductUrduName = ProductUrduNameTxt.Text,
//                SubcategoryId = Convert.ToInt32(SubCategoryCategoryDropDownLst.SelectedValue),
//                PurchasePrice = Convert.ToDecimal(PurchasePriceTxt.Text),
//                ProductType = Convert.ToString(productTypeDropdown.SelectedItem),
//                SalePrice= Convert.ToDecimal(P_SalePriceTxt.Text),
//                Cost= Convert.ToInt32(p_costTxt.Text)
//                //isActive = ProductIsActiveChk.Checked
//            };

//            if (!model.IsValid(out var results))
//            {
//                var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
//                MessageBox.Show($"{errors}","Validation Errors",MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            using (var context = new POSDbContext())
//            {
//                var productRepository = new ProductRepository(context);
//                productRepository.Insert(model);
//                productRepository.Save();

//                MessageBox.Show("Product saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }


//            // var selectedId =Convert.ToString(productTypeDropdown.SelectedItem);
//            //MessageBox.Show(selectedId.ToString());
//        }

//        private async Task LoadProductsForDataGridView()
//        {
//            using (var context = new POSDbContext())
//            {
//                IProductRepository productRepository = new ProductRepository(context);
//                //var cities = await cityRepository.GetCitiesListAsync();

//                var result = await productRepository.GetProductPagingListAsync(PageIndex, PageSize, SearchTerm);
//                RecordCount = result.totalCount;
//                DataTable dt = new DataTable();
//                dt.Columns.Add("ID", typeof(int));
//                dt.Columns.Add("Name", typeof(string));
//                dt.Columns.Add("Type", typeof(string));
//                dt.Columns.Add("Cost", typeof(int));
//                dt.Columns.Add("Sale-Price", typeof(string));
//                dt.Columns.Add("Purchase-Price", typeof(string));

//                foreach (var item in result.data)
//                {
//                    dt.Rows.Add(item.Id, item.ProductEnglishName, item.ProductType, item.Cost, item.SalePrice, item.PurchasePrice);
//                }

//                //CountryDatagridView.AutoGenerateColumns = true;
//                ProductListGrid.ReadOnly = false;    
//                ProductListGrid.AllowUserToAddRows = false;
//                ProductListGrid.AutoGenerateColumns = false; // Important for manual column setup

//                ProductListGrid.DataSource = dt;



//                //ProductListGrid.Columns[2].Visible = false;
//                // Configure grid appearance
//                ConfigureDataGridView();
//                UpdatePager();
//            }
//        }
//        private void ConfigureDataGridView()
//        {
//            // Clear existing columns if any
//            ProductListGrid.Columns.Clear();


//            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
//            {
//                Width=10,
//                ReadOnly= false
//            };
//            ProductListGrid.Columns.Add(chk);
//            // Add columns with specific settings
//            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
//            {
//                Name = "ID",
//                DataPropertyName = "ID",
//                HeaderText = "ID",
//                Width = 50,
//                ReadOnly = true
//            });

//            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
//            {
//                Name = "Name",
//                DataPropertyName = "Name",
//                HeaderText = "Product Name",
//                Width = 150,
//                ReadOnly = true
//            });

//            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
//            {
//                Name = "Type",
//                DataPropertyName = "Type",
//                HeaderText = "Type",
//                Width = 100,
//                ReadOnly = true
//            });

//            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
//            {
//                Name = "Cost",
//                DataPropertyName = "Cost",
//                HeaderText = "Cost",
//                Width = 80,
//                ReadOnly = true,
//                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N0" }
//            });

//            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
//            {
//                Name = "SalePrice",
//                DataPropertyName = "Sale-Price",
//                HeaderText = "Sale Price",
//                Width = 100,
//                ReadOnly = true,
//                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2" }
//            });

//            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
//            {
//                Name = "PurchasePrice",
//                DataPropertyName = "Purchase-Price",
//                HeaderText = "Purchase Price",
//                Width = 100,
//                ReadOnly = true,
//                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2" }
//            });



//            // Configure grid properties for better appearance
//            ProductListGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

//            // Override the fill behavior for the checkbox column to maintain its small width
//            chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Prevent auto-sizing
//            chk.Width = 30; // Set fixed width again after adding to grid

//            ProductListGrid.AllowUserToResizeColumns = true;
//            ProductListGrid.AllowUserToResizeRows = false;
//            ProductListGrid.RowHeadersVisible = false;
//            ProductListGrid.BackgroundColor = SystemColors.Window;
//            ProductListGrid.BorderStyle = BorderStyle.None;
//            ProductListGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
//            ProductListGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9);
//            ProductListGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
//            ProductListGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
//            ProductListGrid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
//            ProductListGrid.EnableHeadersVisualStyles = false;
//        }

//        private void UpdatePager()
//        {
//            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
//            lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

//            PrevBtn.Enabled = PageIndex > 1;
//            NextBtn.Enabled = PageIndex < totalPages;
//        }

//        private async void ProdSearchTxt_TextChanged(object sender, EventArgs e)
//        {
//            PageIndex = 1;
//            SearchTerm = ProdSearchTxt.Text.Trim();
//            await LoadProductsForDataGridView();
//        }

//        private async void PrevBtn_Click(object sender, EventArgs e)
//        {
//            if (PageIndex > 1)
//            {
//                PageIndex--;
//                await LoadProductsForDataGridView();
//            }
//        }

//        private async void NextBtn_Click(object sender, EventArgs e)
//        {
//            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
//            if (PageIndex < totalPages)
//            {
//                PageIndex++;
//                await LoadProductsForDataGridView();
//            }
//        }
//    }
//}



using ClosedXML.Excel;
using POS_Shop.Helpers;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Product
{
    public partial class ProductFromControl : UserControl
    {
        private int PageSize = 100;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";

        // Store selected IDs across all pages
        private HashSet<int> selectedProductIds = new HashSet<int>();

        public ProductFromControl()
        {
            InitializeComponent();
            this.Load += ProductFromControl_Load;
            ProductEngNameTxt.Focus();
        }

        private async void ProductFromControl_Load(object sender, EventArgs e)
        {
            LoadingManager.ShowLoading();
            loadCategiryForDropdown();
            await LoadProductsForDataGridView();
            productTypeDropdown.SelectedItem = "عدد";
            LoadingManager.HideLoading();
        }


        private void loadCategiryForDropdown()
        {
            // unsubscribe first to avoid multiple subscriptions
            CategoryDropDownLst.SelectedIndexChanged -= CategoryDropdown_SelectedIndexChanged;

            using (var context = new POSDbContext())
            {
                var categoriesList = context.Categories.Select(s => new
                {
                    Id = s.id,
                    Name = s.name
                }).ToList();

                // Add default option
                var allItems = new List<object>();
                allItems.Add(new { Id = 0, Name = "Select Category" });
                allItems.AddRange(categoriesList);
                CategoryDropDownLst.DataSource = allItems;
                CategoryDropDownLst.DisplayMember = "Name";
                CategoryDropDownLst.ValueMember = "Id";
            }

            // Subscribe AFTER data is loaded
            CategoryDropDownLst.SelectedIndexChanged += CategoryDropdown_SelectedIndexChanged;
        }


        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if SelectedValue is null or the default option
            if (CategoryDropDownLst.SelectedValue == null ||
                Convert.ToInt32(CategoryDropDownLst.SelectedValue) == 0)
            {
                // Clear subcategory dropdown if no category selected
                SubCategoryCategoryDropDownLst.DataSource = null;
                SubCategoryCategoryDropDownLst.Items.Clear();
                return;
            }

            // Get the selected ID as integer
            int selectedId = Convert.ToInt32(CategoryDropDownLst.SelectedValue);

            // Load subcategories based on the selected category ID
            using (var context = new POSDbContext())
            {
                var subCategoriesList = context.SubCategories
                    .Where(s => s.categoryId == selectedId).Select(s => new
                    {
                        Id = s.id,
                        Name = s.name
                    })
                    .ToList();

                // Add default option for subcategories
                var allSubItems = new List<object>();
                allSubItems.Add(new { Id = 0, Name = "Select SubCategory" });
                allSubItems.AddRange(subCategoriesList);
                SubCategoryCategoryDropDownLst.DataSource = allSubItems;
                SubCategoryCategoryDropDownLst.DisplayMember = "Name";
                SubCategoryCategoryDropDownLst.ValueMember = "Id";
                SubCategoryCategoryDropDownLst.SelectedIndex = 0;

            }
        }
        private async Task LoadProductsForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                IProductRepository productRepository = new ProductRepository(context);
                var result = await productRepository.GetProductPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;

                DataTable dt = new DataTable();
                dt.Columns.Add("IsSelected", typeof(bool)); // Add selection column
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Cost", typeof(int));
                dt.Columns.Add("Purchase-Price", typeof(string));
                dt.Columns.Add("SalePrice", typeof(string));

                foreach (var item in result.data)
                {
                    // Check if this product is in our selected list
                    bool isSelected = selectedProductIds.Contains(item.Id);
                    dt.Rows.Add(isSelected, item.Id, item.ProductEnglishName, item.ProductType,
                                item.Cost, item.PurchasePrice,item.SalePrice);
                }

                ProductListGrid.ReadOnly = false;
                ProductListGrid.AllowUserToAddRows = false;
                ProductListGrid.AutoGenerateColumns = false;
                ProductListGrid.DataSource = dt;
                ConfigureDataGridView();
                UpdatePager();
            }
        }


        private async void ProductSaveBtn_Click(object sender, EventArgs e)
        {
            var model = new Models.Product()
            {
                ProductEnglishName = ProductEngNameTxt.Text,
                ProductUrduName = ProductUrduNameTxt.Text,
                SubcategoryId = Convert.ToInt32(SubCategoryCategoryDropDownLst.SelectedValue),
                PurchasePrice = ConversionHelper.ParseInt(PurchasePriceTxt.Text),
                ProductType = Convert.ToString(productTypeDropdown.SelectedItem),
                SalePrice = ConversionHelper.ParseInt(P_SalePriceTxt.Text),
                Cost = ConversionHelper.ParseInt(p_costTxt.Text)
                //isActive = ProductIsActiveChk.Checked
            };

            if (!model.IsValid(out var results))
            {
                var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            using (var context = new POSDbContext())
            {
                IProductRepository productRepository = new ProductRepository(context);
                if (await productRepository.CheckRecoradlreadyExistByName(model.ProductEnglishName))
                {
                    MessageBox.Show($"Product with name '{model.ProductEnglishName}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                productRepository.Insert(model);
                productRepository.Save();
                ClearFormFunction();
                MessageBox.Show("Product saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            // var selectedId =Convert.ToString(productTypeDropdown.SelectedItem);
            //MessageBox.Show(selectedId.ToString());
        }

        private async void updateProductBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(productIdTxt.Text) || !int.TryParse(productIdTxt.Text, out int prodId))
            {
                MessageBox.Show("Invalid Product ID for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var model = new Models.Product()
            {
                Id = prodId,
                ProductEnglishName = ProductEngNameTxt.Text,
                ProductUrduName = ProductUrduNameTxt.Text,
                SubcategoryId = Convert.ToInt32(SubCategoryCategoryDropDownLst.SelectedValue),
                PurchasePrice = ConversionHelper.ParseInt(PurchasePriceTxt.Text),
                ProductType = Convert.ToString(productTypeDropdown.SelectedItem),
                SalePrice = ConversionHelper.ParseInt(P_SalePriceTxt.Text),
                Cost = ConversionHelper.ParseInt(p_costTxt.Text)
            };
            if (!model.IsValid(out var results))
            {
                var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var context = new POSDbContext())
            {
                IProductRepository productRepository = new ProductRepository(context);
                var existingProduct = productRepository.GetById(model.Id);

                if (existingProduct == null)
                {
                    MessageBox.Show("Product not found for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                existingProduct.ProductEnglishName = model.ProductEnglishName;
                existingProduct.ProductUrduName = model.ProductUrduName;
                existingProduct.SubcategoryId = model.SubcategoryId;
                existingProduct.PurchasePrice = model.PurchasePrice;
                existingProduct.ProductType = model.ProductType;
                existingProduct.SalePrice = model.SalePrice;
                existingProduct.Cost = model.Cost;
                productRepository.Update(existingProduct);
                productRepository.Save();
                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updateProductBtn.Visible = false;
                await LoadProductsForDataGridView();
                ClearFormFunction();
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

                updateProductBtn.Visible = true;
                ProductSaveBtn.Enabled = false;

                ProductFormLbl.Text = "Edit Product";
                GetAndBindProductForEdit(productId);

                //MessageBox.Show($"Prod ID :{productId} and NAme :{productName} for Edit");
            }
            else if (e.ColumnIndex == grid.Columns["Delete"].Index)
            {

                DeleteProductById(productId);

            }
        }


        private void GetAndBindProductForEdit(int productId)
        {

            using (var context = new POSDbContext())
            {
                var productRepo = new ProductRepository(context);
                var product = productRepo.GetById(productId);
                if (product != null)
                {
                    productIdTxt.Text = product.Id.ToString();
                    // Populate form fields for editing
                    ProductEngNameTxt.Text = product.ProductEnglishName;
                    ProductUrduNameTxt.Text = product.ProductUrduName;
                    PurchasePriceTxt.Text = product.PurchasePrice.ToString();
                    p_costTxt.Text = product.Cost.ToString();
                    P_SalePriceTxt.Text = product.SalePrice.ToString();
                    productTypeDropdown.SelectedItem = product.ProductType;
                    // Set category and subcategory dropdowns
                    var subCategory = context.SubCategories.Find(product.SubcategoryId);
                    if (subCategory != null)
                    {
                        var category = context.Categories.Find(subCategory.categoryId);
                        if (category != null)
                        {
                            CategoryDropDownLst.SelectedValue = category.id;
                            // This will trigger loading of subcategories
                            SubCategoryCategoryDropDownLst.SelectedValue = subCategory.id;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Product not found for editing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

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

        private async void ClearFormFunction()
        {
            // Clear all input fields
            productIdTxt.Clear();
            ProductEngNameTxt.Clear();
            ProductUrduNameTxt.Clear();
            PurchasePriceTxt.Clear();
            p_costTxt.Clear();
            P_SalePriceTxt.Clear();
            productTypeDropdown.SelectedIndex = 0;
            CategoryDropDownLst.SelectedIndex = 0;
            SubCategoryCategoryDropDownLst.DataSource = null;
            SubCategoryCategoryDropDownLst.Items.Clear();
            updateProductBtn.Visible = false;
            ProductSaveBtn.Enabled = true;
            ProductEngNameTxt.Focus();
            await LoadProductsForDataGridView();
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
            if(selectedProductIds.Count <= 0)
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
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

            PreviousPageBtn.Enabled = PageIndex > 1;
            NextPageBtn.Enabled = PageIndex < totalPages;

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
            PageIndex = 1;
            SearchTerm = ProdSearchTxt.Text.Trim();
            await LoadProductsForDataGridView();
        }


        private async void NextPageBtn_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            if (PageIndex < totalPages)
            {
                PageIndex++;
                await LoadProductsForDataGridView();
            }
        }

        private async void PreviousPageBtn_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadProductsForDataGridView();
            }
        }

        private async void SelectAllBtn_Click(object sender, EventArgs e)
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

        private void ClearAllSelectionBtn_Click(object sender, EventArgs e)
        {
            ClearSelection();
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
                if(selectedProducts.Count()>0)
                {
                    DataTable exportTable = new DataTable();
                    exportTable.TableName = "Products";

                    // Add columns
                    exportTable.Columns.Add("ProductID", typeof(int));
                    exportTable.Columns.Add("ProductName", typeof(string));
                    exportTable.Columns.Add("UrduName", typeof(string));
                    exportTable.Columns.Add("Type", typeof(string));
                    exportTable.Columns.Add("PurchasePrice", typeof(decimal));
                    exportTable.Columns.Add("SalePrice", typeof(decimal));
                    exportTable.Columns.Add("Cost", typeof(int));
                    exportTable.Columns.Add("SubCategory", typeof(int));

                    // Add rows
                    foreach (var product in selectedProducts)
                    {
                        exportTable.Rows.Add(
                            product.Id,
                            product.ProductEnglishName,
                            product.ProductUrduName,
                            product.ProductType,
                            product.PurchasePrice,
                            product.SalePrice,
                            product.Cost,
                            product.SubcategoryId
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
                }
                else
                {
                   MessageBox.Show("No products found for the selected IDs.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ProductResetFormBtn_Click(object sender, EventArgs e)
        {
            ClearFormFunction();
            ProductFormLbl.Text = "Add Product";
        }
    }
}
