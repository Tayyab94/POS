using POS_Shop.Helpers.DAL;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Product
{
    public partial class NewProductForm : Form
    {
        private int productId;
        private DatabaseHelper dbHelper;
        private List<ProductUnitDto> allProductUnits = new List<ProductUnitDto>();
        private List<ProductPrice> productPrices = new List<ProductPrice>();
        private Dictionary<int, Panel> pricePanels = new Dictionary<int, Panel>();

        private const int CONTROL_SPACING = 45;
        private const int INITIAL_Y_POSITION = 5;

        //// Default items count for each type
        //private Dictionary<string, int> defaultItems = new Dictionary<string, int>
        //{
        //    { "عدد", 1 },
        //    { "درجن", 12 },
        //    { "ڈبہ", 24 },
        //    { "پیکٹ", 6 },
        //    { "کلو", 1 },
        //    { "کارٹن", 48 },
        //    { "بنڈل", 10 },
        //    { "جوڑی", 1 }
        //};

        public NewProductForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
    
            InitializeUI();
            //WireEvents();
            // Wire up events
            this.Load += NewProductForm_Load;
        }

        public NewProductForm(int existingProductId) : this()
        {
            // Set product ID
            productId = existingProductId;
            LoadData();
        }

        private void WireEvents()
        {
            btnAddPrice.Click += BtnAddPrice_Click;
            ProductSaveBtn.Click += BtnSaveProduct_Click;
            ProductResetFormBtn.Click += (s, e) => ResetForm();
            updateProductBtn.Click += BtnUpdateProduct_Click;
        }

        private void LoadData()
        {
            try
            {
                // Load product prices if productId exists
                if (productId > 0)
                {
                    productPrices = dbHelper.GetProductPrices(productId);

                    // Get all product units for reference
                    using (var context = new POSDbContext())
                    {
                        allProductUnits = context.ProductUnits
                            .Where(pu => pu.IsActive)
                            .Select(s => new ProductUnitDto()
                            {
                                Id = s.Id,
                                Name = s.Name,
                            }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductData()
        {
            if (productId <= 0) return;

            try
            {
                var product = dbHelper.GetProductById(productId);
                if (product != null)
                {
                    // Fill form fields
                    productIdTxt.Text = product.Id.ToString();
                    ProductEngNameTxt.Text = product.ProductEnglishName;
                    ProductUrduNameTxt.Text = product.ProductUrduName;
                    SearchBynameTxt.Text = product.SearchByProductCode;
                    PurchasePriceTxt.Text = product.PurchasePrice;
                    P_SalePriceTxt.Text = product.SalePrice?.ToString() ?? "";
                    p_costTxt.Text = product.Cost?.ToString() ?? "";
                    P_StockQtyTxt.Text = product.Qty.ToString();

                    //// Set product type dropdown
                    //if (!string.IsNullOrEmpty(product.ProductType) && int.TryParse(product.ProductType, out int productTypeId))
                    //{
                    //    foreach (ProductUnitDto item in productTypeDropdown.Items)
                    //    {
                    //        if (item.Id == productTypeId)
                    //        {
                    //            productTypeDropdown.SelectedItem = item;
                    //            break;
                    //        }
                    //    }
                    //}

                    // Load category and subcategory - FIXED
                    if (product.SubcategoryId.HasValue)
                    {
                        using (var context = new POSDbContext())
                        {
                            var subCategory = context.SubCategories
                                .Include(sc => sc.category)
                                .FirstOrDefault(sc => sc.id == product.SubcategoryId.Value);

                            if (subCategory != null)
                            {
                                // First ensure categories are loaded
                               // LoadCategoryForDropdown();

                                // Set category
                                bool categorySet = false;
                                foreach (var item in CategoryDropDownLst.Items)
                                {
                                    dynamic obj = item;
                                    if (obj.Id == subCategory.categoryId)
                                    {
                                        CategoryDropDownLst.SelectedItem = item;
                                        categorySet = true;
                                        break;
                                    }
                                }

                                if (categorySet)
                                {
                                    // Force category changed event to load subcategories
                                    CategoryDropdown_SelectedIndexChanged(null, EventArgs.Empty);

                                    // Set subcategory after a short delay
                                    this.BeginInvoke(new Action(() =>
                                    {
                                        System.Threading.Thread.Sleep(100); // Small delay to ensure loading

                                        bool subCategorySet = false;
                                        foreach (var item in SubCategoryCategoryDropDownLst.Items)
                                        {
                                            dynamic obj = item;
                                            if (obj.Id == product.SubcategoryId.Value)
                                            {
                                                SubCategoryCategoryDropDownLst.SelectedItem = item;
                                                subCategorySet = true;
                                                break;
                                            }
                                        }

                                        if (!subCategorySet)
                                        {
                                            // Add subcategory if not found
                                            SubCategoryCategoryDropDownLst.Items.Add(new
                                            {
                                                Id = product.SubcategoryId.Value,
                                                Name = subCategory.name
                                            });
                                            SubCategoryCategoryDropDownLst.SelectedIndex =
                                                SubCategoryCategoryDropDownLst.Items.Count - 1;
                                        }
                                    }));
                                }
                            }
                        }
                    }

                    // Create controls for existing prices
                    foreach (var price in productPrices)
                    {
                        AddPriceControl(price);
                    }

                    // Show update button, hide save button
                    updateProductBtn.Visible = true;
                    ProductSaveBtn.Visible = false;
                    label1.Text = "Edit Product";

                    // Update dropdown after loading prices
                    UpdateProductUnitDropdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeUI()
        {
            // Set default states
            if (productId == 0)
            {
                P_StockQtyTxt.Text = "0";
            }
            else
            {
                // Ensure controls are ready for editing mode
                productIdTxt.Enabled = false;
            }

            // Update summary
            UpdatePriceSummary();
        }

        private void NewProductForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Load product units for PRICE TYPE dropdown
                LoadProductUnitsForPriceTypeDropdown();

                // Load category dropdown
                LoadCategoryForDropdown();
                // Initialize UI
                InitializeUI();
                WireEvents();
                // Load product data if editing existing product
                if (productId > 0)
                {
                    LoadProductData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductUnitsForPriceTypeDropdown()
        {
            using (var context = new POSDbContext())
            {
                var productUnits = context.ProductUnits
                    .Where(pu => pu.IsActive)
                    .Select(s => new ProductUnitDto()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList();

                // Store in allProductUnits for later use
                allProductUnits = productUnits;

                // Load the price type dropdown
                UpdateProductUnitDropdown();
            }
        }

        private void UpdateProductUnitDropdown()
        {
            try
            {
                var existingTypeIds = new HashSet<int>(productPrices.Select(p => p.Prod_Unit_TypeId));
                var availableUnits = allProductUnits
                    .Where(u => u.Id > 0 && !existingTypeIds.Contains(u.Id))
                    .OrderBy(u => u.Name)  // Sort by name for better UX
                    .ToList();

                // Clear and reload the dropdown
                cmbProductType.Items.Clear();

                // Add default option
                cmbProductType.Items.Add(new ProductUnitDto  { Id = 0, Name = "Select Unit" });

                // Add available units
                foreach (var unit in availableUnits)
                {
                    cmbProductType.Items.Add(unit);
                }

                cmbProductType.DisplayMember = "Name";
                cmbProductType.ValueMember = "Id";

                // Only set selected index if there are items
                if (cmbProductType.Items.Count > 0)
                {
                    cmbProductType.SelectedIndex = 0;
                }

                btnAddPrice.Enabled = availableUnits.Count > 0;
                if (!btnAddPrice.Enabled)
                {
                    cmbProductType.Text = "All units added";
                    cmbProductType.Enabled = false;
                }
                else
                {
                    cmbProductType.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating price type dropdown: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategoryForDropdown()
        {
            // Unsubscribe first to avoid multiple subscriptions
            CategoryDropDownLst.SelectedIndexChanged -= CategoryDropdown_SelectedIndexChanged;

            using (var context = new POSDbContext())
            {
                var categoriesList = context.Categories
                    .Select(s => new
                    {
                        Id = s.id,
                        Name = s.name
                    }).ToList();

                // Add default option
                var allItems = new List<object>
                {
                    new { Id = 0, Name = "Select Category" }
                };
                allItems.AddRange(categoriesList);

                CategoryDropDownLst.DataSource = allItems;
                CategoryDropDownLst.DisplayMember = "Name";
                CategoryDropDownLst.ValueMember = "Id";
                CategoryDropDownLst.SelectedIndex = 0;
            }

            // Subscribe AFTER data is loaded
            CategoryDropDownLst.SelectedIndexChanged += CategoryDropdown_SelectedIndexChanged;
        }

        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoryDropDownLst.SelectedValue == null ||
                Convert.ToInt32(CategoryDropDownLst.SelectedValue) == 0)
            {
                SubCategoryCategoryDropDownLst.DataSource = null;
                SubCategoryCategoryDropDownLst.Items.Clear();
                return;
            }

            int selectedId = Convert.ToInt32(CategoryDropDownLst.SelectedValue);

            using (var context = new POSDbContext())
            {
                var subCategoriesList = context.SubCategories
                    .Where(s => s.categoryId == selectedId)
                    .Select(s => new
                    {
                        Id = s.id,
                        Name = s.name
                    }).ToList();

                // Add default option
                var allSubItems = new List<object>
                {
                    new { Id = 0, Name = "Select SubCategory" }
                };
                allSubItems.AddRange(subCategoriesList);

                SubCategoryCategoryDropDownLst.DataSource = allSubItems;
                SubCategoryCategoryDropDownLst.DisplayMember = "Name";
                SubCategoryCategoryDropDownLst.ValueMember = "Id";
                SubCategoryCategoryDropDownLst.SelectedIndex = 0;
            }
        }

        //private void UpdateProductUnitDropdown()
        //{
        //    var existingTypeIds = new HashSet<int>(productPrices.Select(p => p.Prod_Unit_TypeId));
        //    var availableUnits = allProductUnits
        //        .Where(u => u.Id > 0 && !existingTypeIds.Contains(u.Id))
        //        .OrderBy(u => u.Id)
        //        .ToList();

        //    if (cmbProductType.Items.Count != availableUnits.Count ||
        //        !cmbProductType.Items.Cast<ProductUnit>().SequenceEqual(availableUnits))
        //    {
        //        var currentSelection = cmbProductType.SelectedItem as ProductUnit;
        //        cmbProductType.DataSource = null;
        //        cmbProductType.DataSource = availableUnits;
        //        cmbProductType.DisplayMember = "Name";
        //        cmbProductType.ValueMember = "Id";

        //        if (currentSelection != null && availableUnits.Contains(currentSelection))
        //            cmbProductType.SelectedItem = currentSelection;
        //        else if (availableUnits.Count > 0)
        //            cmbProductType.SelectedIndex = 0;
        //    }

        //    btnAddPrice.Enabled = availableUnits.Count > 0;
        //    if (!btnAddPrice.Enabled)
        //        cmbProductType.Text = "All units added";
        //}

        private void BtnAddPrice_Click(object sender, EventArgs e)
        {
            if (cmbProductType.SelectedItem == null || cmbProductType.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a product unit.", "Warning",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedUnit = (ProductUnitDto)cmbProductType.SelectedItem;

            if (pricePanels.ContainsKey(selectedUnit.Id))
            {
                MessageBox.Show($"Price for {selectedUnit.Name} is already added.",
                              "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newPrice = new ProductPrice
            {
                Prod_Unit_TypeId = selectedUnit.Id,
                TypeName = selectedUnit.Name,
                Unit = selectedUnit.Name ?? selectedUnit.Name,
                //ItemsCount = GetDefaultItemsCount(selectedUnit.Name),
                ItemsCount= 1,
                Price = 0,
                PricePerItem = 0,
                CreatedDate = DateTime.Now
            };

            productPrices.Add(newPrice);
            AddPriceControl(newPrice);
            UpdateProductUnitDropdown();
            UpdatePriceSummary();
        }

        private void AddPriceControl(ProductPrice price)
        {
            var productUnit = allProductUnits.FirstOrDefault(t => t.Id == price.Prod_Unit_TypeId);
            if (productUnit == null) return;

            int yPosition = INITIAL_Y_POSITION + (pricePanels.Count * CONTROL_SPACING);
            Panel pricePanel = CreatePricePanel(price, productUnit, yPosition);

            priceControlsContainer.Controls.Add(pricePanel);
            pricePanels.Add(price.Prod_Unit_TypeId, pricePanel);
        }

        private Panel CreatePricePanel(ProductPrice price, ProductUnitDto productUnit, int yPosition)
        {
            Panel pricePanel = new Panel
            {
                Size = new Size(750, 40),
                Location = new Point(5, yPosition),
                Tag = price.Prod_Unit_TypeId,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Padding = new Padding(5)
            };

            int xPos = 5;

            // Remove button (on left side for RTL)
            Button btnRemove = new Button
            {
                Location = new Point(xPos, 8),
                Size = new Size(80, 24),
                Font = new Font("Segoe UI", 9),
                Tag = price.Prod_Unit_TypeId,
                FlatStyle = FlatStyle.Flat,
                Text = "Remove",
                BackColor = Color.IndianRed,
                ForeColor = Color.White
            };
            btnRemove.Click += BtnRemovePrice_Click;
            pricePanel.Controls.Add(btnRemove);
            xPos += 90;

            // Type label
            Label lblType = new Label
            {
                Text = $"{productUnit.Name}:",
                Location = new Point(xPos, 10),
                Size = new Size(70, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pricePanel.Controls.Add(lblType);
            xPos += 80;

            // Items Count
            Label lblItems = new Label
            {
                Text = "Qty:",
                Location = new Point(xPos, 10),
                Size = new Size(30, 20),
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleRight
            };
            pricePanel.Controls.Add(lblItems);
            xPos += 35;

            NumericUpDown numItems = new NumericUpDown
            {
                Location = new Point(xPos, 8),
                Size = new Size(60, 22),
                Font = new Font("Segoe UI", 9),
                Minimum = 1,
                Maximum = 10000,
                Value = price.ItemsCount,
                DecimalPlaces = 0,
                Tag = "items"
            };
            numItems.ValueChanged += (s, e) => UpdateSinglePriceCalculation(pricePanel);
            pricePanel.Controls.Add(numItems);
            xPos += 70;

            Label lblPieces = new Label
            {
                Text = "pieces",
                Location = new Point(xPos, 10),
                Size = new Size(45, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            pricePanel.Controls.Add(lblPieces);
            xPos += 55;

            // Price
            Label lblPrice = new Label
            {
                Text = "Price:",
                Location = new Point(xPos, 10),
                Size = new Size(40, 20),
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleRight
            };
            pricePanel.Controls.Add(lblPrice);
            xPos += 45;

            TextBox txtPrice = new TextBox
            {
                Location = new Point(xPos, 8),
                Size = new Size(80, 22),
                Font = new Font("Segoe UI", 9),
                Text = price.Price > 0 ? price.Price.ToString("F2") : "",
                TextAlign = HorizontalAlignment.Right,
                Tag = "price"
            };
            txtPrice.TextChanged += (s, e) => UpdateSinglePriceCalculation(pricePanel);
            txtPrice.KeyPress += TxtPrice_KeyPress;
            pricePanel.Controls.Add(txtPrice);
            xPos += 90;

            // Unit label
            Label lblUnit = new Label
            {
                Text = $"per {productUnit.Name ?? productUnit.Name}",
                Location = new Point(xPos, 10),
                Size = new Size(70, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkGreen
            };
            pricePanel.Controls.Add(lblUnit);
            xPos += 80;

            // Calculated price per piece
            Label lblPerPiece = new Label
            {
                Text = price.PricePerItem > 0 ?
                       $"Rs {price.PricePerItem:F2}/piece" :
                       "Rs 0.00/piece",
                Location = new Point(xPos, 10),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Blue,
                Tag = "perPiece"
            };
            pricePanel.Controls.Add(lblPerPiece);

            return pricePanel;
        }

        private void BtnRemovePrice_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int typeId = (int)btn.Tag;

            if (!pricePanels.TryGetValue(typeId, out Panel panelToRemove))
                return;

            var priceToRemove = productPrices.FirstOrDefault(p => p.Prod_Unit_TypeId == typeId);
            string typeName = priceToRemove?.TypeName ?? "this price type";

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to remove the price for {typeName}?",
                "Confirm Removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // Remove from collections
            pricePanels.Remove(typeId);
            productPrices.RemoveAll(p => p.Prod_Unit_TypeId == typeId);

            // Store the index of the removed panel
            int removedIndex = GetPanelIndex(panelToRemove);

            // Remove from UI
            priceControlsContainer.Controls.Remove(panelToRemove);
            panelToRemove.Dispose();

            // Only reposition panels below the removed one
            RepositionPanelsBelow(removedIndex);

            UpdateProductUnitDropdown();
            UpdatePriceSummary();
        }

        private void BtnSaveProduct_Click(object sender, EventArgs e)
        {
            SaveProduct(false);

        }

        private void BtnUpdateProduct_Click(object sender, EventArgs e)
        {
            SaveProduct(true);
        }

        private void SaveProduct(bool isUpdate)
        {
            // Validate product details
            if (!ValidateProductDetails())
                return;

            // Validate prices
            if (!ValidatePrices())
                return;

            try
            {
                // Create/Update product
                var product = new POS_Shop.Models.Product
                {
                    ProductEnglishName = ProductEngNameTxt.Text.Trim(),
                    ProductUrduName = ProductUrduNameTxt.Text.Trim(),
                    SearchByProductCode = SearchBynameTxt.Text.Trim(),
                    PurchasePrice = PurchasePriceTxt.Text.Trim(),
                    SalePrice = int.TryParse(P_SalePriceTxt.Text, out int salePrice) ? salePrice : (int?)null,
                    Cost = int.TryParse(p_costTxt.Text, out int cost) ? cost : (int?)null,
                    Qty = int.TryParse(P_StockQtyTxt.Text, out int qty) ? qty : 0,
                    SubcategoryId = SubCategoryCategoryDropDownLst.SelectedValue != null &&
                                   Convert.ToInt32(SubCategoryCategoryDropDownLst.SelectedValue) > 0 ?
                                   Convert.ToInt32(SubCategoryCategoryDropDownLst.SelectedValue) : (int?)null
                };

                if (isUpdate && !string.IsNullOrEmpty(productIdTxt.Text))
                {
                    product.Id = Convert.ToInt32(productIdTxt.Text);
                }

                // Save product to database
                int savedProductId = dbHelper.AddProductAndGetId(product);

                if (savedProductId > 0)
                {
                    // Save product prices
                    bool pricesSaved = dbHelper.SaveProductPrices(savedProductId, productPrices);

                    if (pricesSaved)
                    {
                        MessageBox.Show($"Product {(isUpdate ? "updated" : "created")} successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (!isUpdate)
                        {
                            ResetForm();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Product was {(isUpdate ? "updated" : "created")} but there was an error saving prices.", "Warning",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Error {(isUpdate ? "updating" : "creating")} product. Please try again.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving product: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateProductDetails()
        {
            if (string.IsNullOrWhiteSpace(ProductEngNameTxt.Text))
            {
                MessageBox.Show("Product English name is required.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProductEngNameTxt.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ProductUrduNameTxt.Text))
            {
                MessageBox.Show("Product Urdu name is required.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProductUrduNameTxt.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(SearchBynameTxt.Text))
            {
                MessageBox.Show("Product code is required.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                SearchBynameTxt.Focus();
                return false;
            }

            // Check if product code already exists (only for new products)
            if (productId == 0)
            {
                try
                {
                    bool codeExists = dbHelper.CheckProductCodeExists(SearchBynameTxt.Text.Trim());
                    if (codeExists)
                    {
                        MessageBox.Show("Product code already exists. Please use a unique code.",
                                      "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SearchBynameTxt.Focus();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking product code: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            // Validate numeric fields
            if (!decimal.TryParse(PurchasePriceTxt.Text, out decimal purchasePrice) || purchasePrice < 0)
            {
                MessageBox.Show("Please enter a valid purchase price.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                PurchasePriceTxt.Focus();
                return false;
            }

            return true;
        }

        private bool ValidatePrices()
        {
            if (productPrices.Count == 0)
            {
                MessageBox.Show("Please add at least one price unit for the product.",
                              "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var errors = new List<string>();

            foreach (var price in productPrices)
            {
                if (price.Price <= 0)
                {
                    errors.Add($"Please enter price for {price.TypeName}");
                }
                else if (price.ItemsCount <= 0)
                {
                    errors.Add($"Invalid items count for {price.TypeName}");
                }
            }

            if (errors.Any())
            {
                MessageBox.Show($"Please fix the following errors:\n\n{string.Join("\n", errors)}",
                              "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // Helper methods for price management
        private int GetPanelIndex(Panel panel)
        {
            return (panel.Location.Y - INITIAL_Y_POSITION) / CONTROL_SPACING;
        }

        private void RepositionPanelsBelow(int startIndex)
        {
            for (int i = 0; i < priceControlsContainer.Controls.Count; i++)
            {
                if (priceControlsContainer.Controls[i] is Panel panel)
                {
                    int currentIndex = GetPanelIndex(panel);
                    if (currentIndex > startIndex)
                    {
                        panel.Location = new Point(5, INITIAL_Y_POSITION + ((currentIndex - 1) * CONTROL_SPACING));
                    }
                }
            }
        }

        private void UpdateSinglePriceCalculation(Panel panel)
        {
            var numItems = panel.Controls.OfType<NumericUpDown>().FirstOrDefault();
            var txtPrice = panel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Tag?.ToString() == "price");
            var lblPerPiece = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Tag?.ToString() == "perPiece");
            var lblType = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Font.Bold);

            if (numItems != null && txtPrice != null && lblPerPiece != null && lblType != null)
            {
                int itemsCount = (int)numItems.Value;
                string typeName = lblType.Text.Trim(':');

                if (decimal.TryParse(txtPrice.Text, out decimal price) && itemsCount > 0)
                {
                    decimal pricePerPiece = price / itemsCount;
                    lblPerPiece.Text = $"Rs {pricePerPiece:F2}/piece";

                    var productPrice = productPrices.FirstOrDefault(p => p.TypeName == typeName);
                    if (productPrice != null)
                    {
                        productPrice.ItemsCount = itemsCount;
                        productPrice.Price = price;
                        productPrice.PricePerItem = pricePerPiece;
                    }
                }
                else
                {
                    lblPerPiece.Text = "Rs 0.00/piece";
                }
            }

            UpdatePriceSummary();
        }

        private void UpdatePriceSummary()
        {
            int activePrices = productPrices.Count(p => p.Price > 0);

            if (activePrices == 0)
            {
                // You might want to add a label for summary like in the previous form
                return;
            }

            // Update summary logic here if you add a summary label
        }

        //private int GetDefaultItemsCount(string typeName)
        //{
        //    return defaultItems.ContainsKey(typeName) ? defaultItems[typeName] : 1;
        //}

        private void TxtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void ResetForm()
        {
            // Clear all fields
            productIdTxt.Clear();
            ProductEngNameTxt.Clear();
            ProductUrduNameTxt.Clear();
            SearchBynameTxt.Clear();
            PurchasePriceTxt.Clear();
            P_SalePriceTxt.Clear();
            p_costTxt.Clear();
            P_StockQtyTxt.Text = "0";
            CategoryDropDownLst.SelectedIndex = 0;
            SubCategoryCategoryDropDownLst.DataSource = null;
            SubCategoryCategoryDropDownLst.Items.Clear();

            // Clear price panels
            foreach (var panel in pricePanels.Values)
            {
                priceControlsContainer.Controls.Remove(panel);
                panel.Dispose();
            }
            pricePanels.Clear();
            productPrices.Clear();

            // Reset buttons
            updateProductBtn.Visible = false;
            ProductSaveBtn.Visible = true;
            label1.Text = "New Product";

            // Reload dropdowns
            UpdateProductUnitDropdown();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            dbHelper?.Dispose();
        }
    }


    public class ProductUnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
