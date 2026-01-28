using POS_Shop.Helpers.DAL;
using POS_Shop.Models;
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
    public partial class EditProdPricesForm : Form
    {
        private int productId;
        private string productName;
        private List<ProductUnit> allProductUnits;
        private List<ProductPrice> productPrices = new List<ProductPrice>();
        private Dictionary<int, Panel> pricePanels = new Dictionary<int, Panel>();
        private DatabaseHelper dbHelper;

        private const int CONTROL_SPACING = 55;
        private const int INITIAL_Y_POSITION = 5;

        public EditProdPricesForm(int productId, string productName)
        {
            InitializeComponent();

            this.productId = productId;
            this.productName = productName;
            this.dbHelper = new DatabaseHelper();

            LoadData();
            InitializeUI();
            WireEvents();
        }

        private void WireEvents()
        {
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void LoadData()
        {
            try
            {
                // Load all product units
                allProductUnits = dbHelper.GetAllProductUnits();

                // Load existing prices for this product
                productPrices = dbHelper.GetProductPrices(productId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeUI()
        {
            this.Text = $"Manage Product Prices - {productName}";
            lblProductName.Text = $"Product: {productName}";

            ClearAllPricePanels();


            UpdateUnitDropdown();

            // Create controls for existing prices
            foreach (var price in productPrices)
            {
                AddPriceControl(price);
            }

            UpdateSummary();
        }



        private void ClearAllPricePanels()
        {
            // Clear all panels from container
            priceControlsContainer.Controls.Clear();

            // Clear tracking dictionary
            pricePanels.Clear();
        }

        private void UpdateUnitDropdown()
        {
            try
            {
                // Get existing unit IDs from current prices
                var existingUnitIds = new HashSet<int>(productPrices.Select(p => p.Prod_Unit_TypeId));

                // Filter available units
                var availableUnits = allProductUnits
                    .Where(u => u.Id > 0 && !existingUnitIds.Contains(u.Id))
                    .OrderBy(u => u.Name)
                    .ToList();

                // Store current selection
                object currentSelection = cmbProductUnit.SelectedItem;

                // Clear and update dropdown
                cmbProductUnit.DataSource = null;
                cmbProductUnit.Items.Clear();

                if (availableUnits.Count > 0)
                {
                    cmbProductUnit.DataSource = availableUnits;
                    cmbProductUnit.DisplayMember = "Name";
                    cmbProductUnit.ValueMember = "Id";
                    cmbProductUnit.SelectedIndex = 0;
                    cmbProductUnit.Enabled = true;
                }
                else
                {
                    // No units available - add a placeholder
                    cmbProductUnit.Items.Add("All units have been added");
                    cmbProductUnit.SelectedIndex = 0;
                    cmbProductUnit.Enabled = false;
                }

                btnAddPrice.Enabled = availableUnits.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating unit dropdown: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPrice_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if dropdown is enabled and has valid items
                if (!cmbProductUnit.Enabled || cmbProductUnit.Items.Count == 0)
                {
                    MessageBox.Show("All product units have been added.", "Information",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Check if an item is selected
                if (cmbProductUnit.SelectedItem == null ||
                    !(cmbProductUnit.SelectedItem is ProductUnit))
                {
                    MessageBox.Show("Please select a valid product unit.", "Warning",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedUnit = (ProductUnit)cmbProductUnit.SelectedItem;

                // Check if this unit already has a price
                if (pricePanels.ContainsKey(selectedUnit.Id))
                {
                    MessageBox.Show($"Price for {selectedUnit.Name} is already added.",
                                  "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if this unit already exists in productPrices list
                if (productPrices.Any(p => p.Prod_Unit_TypeId == selectedUnit.Id))
                {
                    MessageBox.Show($"Price for {selectedUnit.Name} already exists in the list.",
                                  "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create new price entry
                var newPrice = new ProductPrice
                {
                    Prod_Unit_TypeId = selectedUnit.Id,
                    TypeName = selectedUnit.Name,
                    Unit = !string.IsNullOrEmpty(selectedUnit.Abbreviation) ?
                           selectedUnit.Abbreviation : selectedUnit.Name,
                    ItemsCount = 1, // Default to 1
                    Price = 0,
                    PricePerItem = 0,
                    CreatedDate = DateTime.Now
                };

                // Add to list and UI
                productPrices.Add(newPrice);
                AddPriceControl(newPrice);
                UpdateUnitDropdown();
                UpdateSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding price: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AddPriceControl(ProductPrice price)
        {
            // CRITICAL CHECK: Ensure we don't add duplicate unit IDs
            if (pricePanels.ContainsKey(price.Prod_Unit_TypeId))
            {
                // If already exists, update it instead of adding new
                UpdateExistingPricePanel(price);
                return;
            }

            var productUnit = allProductUnits.FirstOrDefault(t => t.Id == price.Prod_Unit_TypeId);
            if (productUnit == null)
            {
                MessageBox.Show($"Product unit with ID {price.Prod_Unit_TypeId} not found.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calculate position
            int yPosition = INITIAL_Y_POSITION + (pricePanels.Count * CONTROL_SPACING);

            Panel pricePanel = CreatePricePanel(price, productUnit, yPosition);

            // Add to container and tracking dictionary
            priceControlsContainer.Controls.Add(pricePanel);
            pricePanels.Add(price.Prod_Unit_TypeId, pricePanel);
        }

        private Panel CreatePricePanel(ProductPrice price, ProductUnit productUnit, int yPosition)
        {
            Panel pricePanel = new Panel
            {
                Size = new Size(750, 50),
                Location = new Point(5, yPosition),
                Tag = price.Prod_Unit_TypeId,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Padding = new Padding(5)
            };

            int xPos = 5;

            // Remove button (on left for better layout)
            Button btnRemove = new Button
            {
                Location = new Point(xPos, 13),
                Size = new Size(80, 24),
                Font = new Font("Segoe UI", 9),
                Tag = price.Prod_Unit_TypeId,
                FlatStyle = FlatStyle.Flat,
                Text = "Remove",
                BackColor = Color.IndianRed,
                ForeColor = Color.White
            };
            btnRemove.Click += BtnRemove_Click;
            pricePanel.Controls.Add(btnRemove);
            xPos += 90;

            // Unit label
            Label lblUnit = new Label
            {
                Text = $"{productUnit.Name}:",
                Location = new Point(xPos, 15),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pricePanel.Controls.Add(lblUnit);
            xPos += 90;

            // Items Count
            Label lblItems = new Label
            {
                Text = "Qty:",
                Location = new Point(xPos, 15),
                Size = new Size(35, 20),
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleRight
            };
            pricePanel.Controls.Add(lblItems);
            xPos += 40;

            NumericUpDown numItems = new NumericUpDown
            {
                Location = new Point(xPos, 13),
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
                Location = new Point(xPos, 15),
                Size = new Size(50, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            pricePanel.Controls.Add(lblPieces);
            xPos += 60;

            // Price
            Label lblPrice = new Label
            {
                Text = "Price:",
                Location = new Point(xPos, 15),
                Size = new Size(45, 20),
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleRight
            };
            pricePanel.Controls.Add(lblPrice);
            xPos += 50;

            TextBox txtPrice = new TextBox
            {
                Location = new Point(xPos, 13),
                Size = new Size(90, 22),
                Font = new Font("Segoe UI", 9),
                Text = price.Price > 0 ? price.Price.ToString("F2") : "",
                TextAlign = HorizontalAlignment.Right,
                Tag = "price"
            };
            txtPrice.TextChanged += (s, e) => UpdateSinglePriceCalculation(pricePanel);
            txtPrice.KeyPress += TxtPrice_KeyPress;
            pricePanel.Controls.Add(txtPrice);
            xPos += 100;

            // Unit abbreviation label
            Label lblUnitAbbr = new Label
            {
                Text = !string.IsNullOrEmpty(productUnit.Abbreviation) ?
                       $"per {productUnit.Abbreviation}" : $"per {productUnit.Name}",
                Location = new Point(xPos, 15),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkGreen
            };
            pricePanel.Controls.Add(lblUnitAbbr);
            xPos += 90;

            // Calculated price per piece
            Label lblPerPiece = new Label
            {
                Text = price.PricePerItem > 0 ?
                       $"Rs {price.PricePerItem:F2}/piece" :
                       "Rs 0.00/piece",
                Location = new Point(xPos, 15),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Blue,
                Tag = "perPiece"
            };
            pricePanel.Controls.Add(lblPerPiece);

            return pricePanel;
        }


        private void UpdateExistingPricePanel(ProductPrice price)
        {
            if (!pricePanels.TryGetValue(price.Prod_Unit_TypeId, out Panel existingPanel))
                return;

            // Update controls in existing panel
            var numItems = existingPanel.Controls.OfType<NumericUpDown>().FirstOrDefault();
            var txtPrice = existingPanel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Tag?.ToString() == "price");
            var lblPerPiece = existingPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Tag?.ToString() == "perPiece");

            if (numItems != null && txtPrice != null && lblPerPiece != null)
            {
                numItems.Value = price.ItemsCount;
                txtPrice.Text = price.Price > 0 ? price.Price.ToString("F2") : "";

                if (price.Price > 0 && price.ItemsCount > 0)
                {
                    decimal pricePerPiece = price.Price / price.ItemsCount;
                    lblPerPiece.Text = $"Rs {pricePerPiece:F2}/piece";
                }
                else
                {
                    lblPerPiece.Text = "Rs 0.00/piece";
                }
            }
        }
        private void BtnRemove_Click(object sender, EventArgs e)
        {

            try
            {
                Button btn = (Button)sender;
                int unitId = (int)btn.Tag;

                if (!pricePanels.TryGetValue(unitId, out Panel panelToRemove))
                    return;

                var priceToRemove = productPrices.FirstOrDefault(p => p.Prod_Unit_TypeId == unitId);
                string unitName = priceToRemove?.TypeName ?? "this price unit";

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to remove the price for {unitName}?",
                    "Confirm Removal",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                // Remove from collections
                pricePanels.Remove(unitId);
                productPrices.RemoveAll(p => p.Prod_Unit_TypeId == unitId);

                // Store the index of the removed panel
                int removedIndex = GetPanelIndex(panelToRemove);

                // Remove from UI
                priceControlsContainer.Controls.Remove(panelToRemove);
                panelToRemove.Dispose();

                // Reposition panels below the removed one
                RepositionPanelsBelow(removedIndex);

                UpdateUnitDropdown();
                UpdateSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing price: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            var lblUnit = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Font.Bold);

            if (numItems != null && txtPrice != null && lblPerPiece != null && lblUnit != null)
            {
                int itemsCount = (int)numItems.Value;
                string unitName = lblUnit.Text.Trim(':');

                if (decimal.TryParse(txtPrice.Text, out decimal price) && itemsCount > 0)
                {
                    decimal pricePerPiece = price / itemsCount;
                    lblPerPiece.Text = $"Rs {pricePerPiece:F2}/piece";

                    var productPrice = productPrices.FirstOrDefault(p => p.TypeName == unitName);
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

            UpdateSummary();
        }

        private void UpdateSummary()
        {
            int activePrices = productPrices.Count(p => p.Price > 0);

            if (activePrices == 0)
            {
                lblSummary.Text = "No prices added yet. Add at least one price unit.";
                lblSummary.ForeColor = Color.Red;
                return;
            }

            // Show summary of all prices
            var lowestPrice = productPrices
                .Where(p => p.Price > 0)
                .OrderBy(p => p.PricePerItem)
                .FirstOrDefault();

            if (lowestPrice != null)
            {
                lblSummary.Text = $"{activePrices} price unit(s) configured | " +
                                 $"Best Value: {lowestPrice.TypeName} @ Rs {lowestPrice.PricePerItem:F2} per piece";
                lblSummary.ForeColor = Color.DarkGreen;
            }
            else
            {
                lblSummary.Text = $"{activePrices} price unit(s) configured";
                lblSummary.ForeColor = Color.OrangeRed;
            }
        }

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (productPrices.Count == 0)
                {
                    MessageBox.Show("Please add at least one product price.",
                                  "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
                    return;
                }

                //// Check for duplicate unit IDs before saving
                //var duplicateUnits = productPrices
                //    .GroupBy(p => p.Prod_Unit_TypeId)
                //    .Where(g => g.Count() > 1)
                //    .Select(g => g.Key)
                //    .ToList();

                //if (duplicateUnits.Any())
                //{
                //    MessageBox.Show($"Found duplicate prices for units: {string.Join(", ", duplicateUnits)}",
                //                  "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                // Save to database
                bool success = dbHelper.SaveProductPrices(productId, productPrices);

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save product prices.",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving product prices: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            dbHelper?.Dispose();
        }
    }
}
