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


        public ProductFromControl()
        {
            InitializeComponent();
            this.Load += ProductFromControl_Load;
        }

        private async void ProductFromControl_Load(object sender, EventArgs e)
        {

            loadCategiryForDropdown(); // If this is sync, keep it here
           await LoadProductsForDataGridView(); // Now you can await it safely
            productTypeDropdown.SelectedItem = "عدد";
        }


        private void loadCategiryForDropdown()
        {
            // unsubscribe first to avoid multiple subscriptions
            CategoryDropDownLst.SelectedIndexChanged -= CategoryDropdown_SelectedIndexChanged;

            using (var context = new POSDbContext())
            {
                var categoriesList = context.Categories.Select(s=> new
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

        private void ProductSaveBtn_Click(object sender, EventArgs e)
        {
            var model = new Models.Product()
            {
                ProductEnglishName = ProductEngNameTxt.Text,
                ProductUrduName = ProductUrduNameTxt.Text,
                SubcategoryId = Convert.ToInt32(SubCategoryCategoryDropDownLst.SelectedValue),
                PurchasePrice = Convert.ToDecimal(PurchasePriceTxt.Text),
                ProductType = Convert.ToString(productTypeDropdown.SelectedItem),
                SalePrice= Convert.ToDecimal(P_SalePriceTxt.Text),
                Cost= Convert.ToInt32(p_costTxt.Text)
                //isActive = ProductIsActiveChk.Checked
            };

            if (!model.IsValid(out var results))
            {
                var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                MessageBox.Show($"{errors}","Validation Errors",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new POSDbContext())
            {
                var productRepository = new ProductRepository(context);
                productRepository.Insert(model);
                productRepository.Save();

                MessageBox.Show("Product saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            // var selectedId =Convert.ToString(productTypeDropdown.SelectedItem);
            //MessageBox.Show(selectedId.ToString());
        }

        private async Task LoadProductsForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                IProductRepository productRepository = new ProductRepository(context);
                //var cities = await cityRepository.GetCitiesListAsync();

                var result = await productRepository.GetProductPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Cost", typeof(int));
                dt.Columns.Add("Sale-Price", typeof(string));
                dt.Columns.Add("Purchase-Price", typeof(string));

                foreach (var item in result.data)
                {
                    dt.Rows.Add(item.Id, item.ProductEnglishName, item.ProductType, item.Cost, item.SalePrice, item.PurchasePrice);
                }

                //CountryDatagridView.AutoGenerateColumns = true;
                ProductListGrid.ReadOnly = true;    
                ProductListGrid.AllowUserToAddRows = false;
                ProductListGrid.AutoGenerateColumns = false; // Important for manual column setup

                ProductListGrid.DataSource = dt;
                //ProductListGrid.Columns[2].Visible = false;
                // Configure grid appearance
                ConfigureDataGridView();
                UpdatePager();
            }
        }
        private void ConfigureDataGridView()
        {
            // Clear existing columns if any
            ProductListGrid.Columns.Clear();

            // Add columns with specific settings
            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ID",
                DataPropertyName = "ID",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Product Name",
                Width = 150,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Type",
                DataPropertyName = "Type",
                HeaderText = "Type",
                Width = 100,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Cost",
                DataPropertyName = "Cost",
                HeaderText = "Cost",
                Width = 80,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N0" }
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SalePrice",
                DataPropertyName = "Sale-Price",
                HeaderText = "Sale Price",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2" }
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PurchasePrice",
                DataPropertyName = "Purchase-Price",
                HeaderText = "Purchase Price",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2" }
            });

            // Configure grid properties for better appearance
            ProductListGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
        }

        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

            PrevBtn.Enabled = PageIndex > 1;
            NextBtn.Enabled = PageIndex < totalPages;
        }

        private async void ProdSearchTxt_TextChanged(object sender, EventArgs e)
        {
            PageIndex = 1;
            SearchTerm = ProdSearchTxt.Text.Trim();
            await LoadProductsForDataGridView();
        }

        private async void PrevBtn_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadProductsForDataGridView();
            }
        }

        private async void NextBtn_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            if (PageIndex < totalPages)
            {
                PageIndex++;
                await LoadProductsForDataGridView();
            }
        }
    }
}
