using Org.BouncyCastle.Tls;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Supplier
{
    public partial class SupplierControl : UserControl
    {

        private int PageSize = 100;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";

        private HashSet<int> selectedSupplierIds = new HashSet<int>();


        public SupplierControl()
        {
            InitializeComponent();

            SupplierNameTxt.Focus();
            SupplierNameTxt.SelectAll();
            this.Load += SupplierControl_Load;
        }
        private async void SupplierControl_Load(object sender, EventArgs e)
        {
            LoadCountriesForDropdown();
            await LoadSuppliersForDataGridView();
        }
        private void LoadCountriesForDropdown()
        {

            CountryDropDownLst.SelectedIndexChanged -= CountryDropDownLst_SelectedIndexChanged;
            using (var context = new POSDbContext())
            {
                var countriesList = context.Countries.Select(s => new
                {
                    Id = s.Id,
                    Name = s.CountryName
                }).ToList();
                CountryDropDownLst.Items.Clear();


                // Add default option
                var allItems = new List<object>();
                allItems.Add(new { Id = 0, Name = "Select Country" });
                allItems.AddRange(countriesList);
                CountryDropDownLst.DataSource = allItems;
                CountryDropDownLst.DisplayMember = "Name";
                CountryDropDownLst.ValueMember = "Id";
            }

            // Subscribe AFTER data is loaded
            CountryDropDownLst.SelectedIndexChanged += CountryDropDownLst_SelectedIndexChanged;
        }

        private void CountryDropDownLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if SelectedValue is null or the default option
            if (CountryDropDownLst.SelectedValue == null ||
                Convert.ToInt32(CountryDropDownLst.SelectedValue) == 0)
            {
                // Clear subcategory dropdown if no category selected
                CityDropDownLst.DataSource = null;
                CityDropDownLst.Items.Clear();
                return;
            }

            // Get the selected ID as integer
            int selectedId = Convert.ToInt32(CountryDropDownLst.SelectedValue);

            // Load subcategories based on the selected category ID
            using (var context = new POSDbContext())
            {
                var CitiesList = context.Cities
                    .Where(s => s.CountryId == selectedId).Select(s => new
                    {
                        Id = s.Id,
                        Name = s.Name
                    })
                    .ToList();

                // Add default option for subcategories
                var allSubItems = new List<object>();
                allSubItems.Add(new { Id = 0, Name = "Select City" });
                allSubItems.AddRange(CitiesList);
                CityDropDownLst.DataSource = allSubItems;
                CityDropDownLst.DisplayMember = "Name";
                CityDropDownLst.ValueMember = "Id";
                CityDropDownLst.SelectedIndex = 0;

            }
        }




        private async Task LoadSuppliersForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                ISupplierRepository supplierRepo = new SupplierRepository(context);
                var result = await supplierRepo.GetSupplierPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;

                DataTable dt = new DataTable();
                dt.Columns.Add("IsSelected", typeof(bool)); // Add selection column
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Shop", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("CityId", typeof(int));
                dt.Columns.Add("City Name", typeof(string));
                dt.Columns.Add("Active", typeof(bool));

                foreach (var item in result.data)
                {
                    bool isSelected = selectedSupplierIds.Contains(item.Id);
                    dt.Rows.Add(isSelected, item.Id, item.SupplierName,item.ShopName ,item.Address,
                                item.ContactNo, item.CityId, item.CityName, !item.IsDeleted);
                }

                //SupplierListDataGrid.ReadOnly = true;
                SupplierListDataGrid.AllowUserToAddRows = false;


                SupplierListDataGrid.AllowUserToResizeColumns = true;
                SupplierListDataGrid.AllowUserToResizeRows = false;
                SupplierListDataGrid.RowHeadersVisible = false;
                SupplierListDataGrid.BackgroundColor = SystemColors.Window;
                SupplierListDataGrid.BorderStyle = BorderStyle.None;
                SupplierListDataGrid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                SupplierListDataGrid.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9);
                SupplierListDataGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                SupplierListDataGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
                SupplierListDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                SupplierListDataGrid.EnableHeadersVisualStyles = false;
                //ProductListGrid.AutoGenerateColumns = false;

                SupplierListDataGrid.DataSource = dt;
                SupplierListDataGrid.Columns[1].Visible = false;
                SupplierListDataGrid.Columns[6].Visible = false;

                SupplierListDataGrid.Columns["IsSelected"].Width = 50;

                UpdatePager();
            }
        }


        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

            PreviousPageBtn.Enabled = PageIndex > 1;
            NextPageBtn.Enabled = PageIndex < totalPages;
        }


        private async void NextPageBtn_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            if (PageIndex < totalPages)
            {
                PageIndex++;
                await LoadSuppliersForDataGridView();
            }
        }

        private async void PreviousPageBtn_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadSuppliersForDataGridView();
            }
        }

        private async void SupplierSearchTxt_TextChanged(object sender, EventArgs e)
        {
            PageIndex = 1;
            SearchTerm = SupplierSearchTxt .Text.Trim();
            await LoadSuppliersForDataGridView();
        }

        private void ClearAllSelectionBtn_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        // Method to clear selection
        public async void ClearSelection()
        {
            selectedSupplierIds.Clear();
            // Reload current page to update checkboxes
            selectedProdLbl.Text = $"Selected: {selectedSupplierIds.Count} Supplier(s)";
            await LoadSuppliersForDataGridView();
        }

        private async void SelectAllBtn_Click(object sender, EventArgs e)
        {
            // Get all IDs on current page and add to selection
            var dataTable = (DataTable)SupplierListDataGrid.DataSource;
            foreach (DataRow row in dataTable.Rows)
            {
                int productId = Convert.ToInt32(row["ID"]);
                selectedSupplierIds.Add(productId);
            }

            // Reload to update checkboxes
            await LoadSuppliersForDataGridView();

            selectedProdLbl.Text = $"Selected: {selectedSupplierIds.Count} Supplier(s)";
        }

        private async void ResetFormBtn_Click(object sender, EventArgs e)
        {
            ClearFormFunction();
            await LoadSuppliersForDataGridView();
        }

        // Clear form fields
        private void ClearFormFunction()
        {
            SupplierIdTxt.Clear();
            SupplierNameTxt.Clear();
            SupplierPhoneTxt.Clear();
            SupplierAddressTxt.Clear();
            SupplierShopNameTxt.Clear();
            CountryDropDownLst.SelectedIndex = 0;
            CityDropDownLst.DataSource = null;
            CityDropDownLst.Items.Clear();
        }

        private  async void SaveSupplierBtn_Click(object sender, EventArgs e)
        {

            var model = new Models.Supplier()
            {
                SupplierName = SupplierNameTxt.Text,
                ShopName= SupplierShopNameTxt.Text,
                ContactNo = SupplierPhoneTxt.Text,
                Address = SupplierAddressTxt.Text,
                CityId = Convert.ToInt32(CityDropDownLst.SelectedValue),
                IsDeleted = false,
                CreatedAt= DateTime.UtcNow,
            };
            var errors = new StringBuilder();
            if (!model.IsValid(out var results))
            {
                errors.AppendLine(string.Join("\n", results.Select(r => r.ErrorMessage)));
                MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            using (var context = new POSDbContext())
            {

                ISupplierRepository supplierRepo = new SupplierRepository(context);
                if (await supplierRepo.CheckRecoradAlreadyExistByName(model.SupplierName, model.Address))
                {
                    MessageBox.Show($"Supplier name '{model.SupplierName}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                context.Suppliers.Add(model);
                context.SaveChanges();

                ClearFormFunction();
                await LoadSuppliersForDataGridView();
                MessageBox.Show("Supplier saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void UpdateSupplierBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SupplierIdTxt.Text) || !int.TryParse(SupplierIdTxt.Text, out int supplierId) || supplierId <= 0)
            {
                MessageBox.Show("Please select Record first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ValidateChildren(ValidationConstraints.Enabled) == false)
            {
                // There are invalid controls
                MessageBox.Show("Please correct the errors before Submitting", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var model = new Models.Supplier()
            {
                Id= supplierId,
                SupplierName = SupplierNameTxt.Text,
                ShopName = SupplierShopNameTxt.Text,
                ContactNo = SupplierPhoneTxt.Text,
                Address = SupplierAddressTxt.Text,
                CityId = Convert.ToInt32(CityDropDownLst.SelectedValue),
                IsDeleted = false
            };
            if (!model.IsValid(out var results))
            {
                var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var context = new POSDbContext())
            {
                ISupplierRepository supplierRepo = new SupplierRepository(context);
                var existingRecord = supplierRepo.GetById(supplierId);
                if (existingRecord == null)
                {
                    MessageBox.Show("Supplier not found for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Check for duplicate name excluding the current record
                if (context.Suppliers.Any(c => c.SupplierName.Equals(model.SupplierName, StringComparison.OrdinalIgnoreCase) && c.Id != supplierId && c.Address.Equals(model.Address, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show($"Supplier name '{model.SupplierName}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Update fields
                existingRecord.SupplierName = model.SupplierName;
                existingRecord.ContactNo = model.ContactNo;
                existingRecord.Address = model.Address;
                existingRecord.CityId = model.CityId;
                existingRecord.IsDeleted = model.IsDeleted;
                supplierRepo.Update(existingRecord);
                supplierRepo.Save();
                ClearFormFunction();
                await LoadSuppliersForDataGridView();
                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void RemoveSupplierBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this Supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {

                var customerId = Convert.ToInt32(SupplierIdTxt.Text);
                using (var context = new POSDbContext())
                {
                    var supplierRepo = new SupplierRepository(context);
                    var data = supplierRepo.GetById(customerId);
                    if (data != null)
                    {
                        try
                        {

                            supplierRepo.Delete(customerId);
                            supplierRepo.Save();
                            MessageBox.Show("Supplier deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            await LoadSuppliersForDataGridView();
                        }

                        catch (DbUpdateException dbEx) when (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                        {
                            MessageBox.Show("This CUSTOMER is being used by other records and cannot be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // Don't navigate away, stay on current page
                            context.ChangeTracker.DetectChanges();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Customer not found for deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SupplierListDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Commit changes immediately when checkbox is toggled
            if (SupplierListDataGrid.CurrentCell is DataGridViewCheckBoxCell)
            {
                SupplierListDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void SupplierListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // Ensure the click is not on the header row (-1)
            if (e.RowIndex != -1)
            {
                if (e.RowIndex >= 0)
                {

                    // Get the index of the clicked column
                    int columnIndex = e.ColumnIndex;

                    // Get the name of the clicked column
                    string columnName = SupplierListDataGrid.Columns[columnIndex].Name;
                    // You can add specific logic based on the column name or index
                    if (columnName == "IsSelected")
                    {

                        var grid = (DataGridView)sender;
                        var checkboxCell = grid.Rows[e.RowIndex].Cells["IsSelected"] as DataGridViewCheckBoxCell;
                        var idCell = grid.Rows[e.RowIndex].Cells["ID"];

                        if (checkboxCell != null && idCell != null && idCell.Value != null)
                        {
                            bool shoulRemove = Convert.ToBoolean(checkboxCell.Value);
                            int productId = Convert.ToInt32(idCell.Value);

                            if (shoulRemove)
                            {
                                selectedSupplierIds.Remove(productId);

                            }
                            else
                            {
                                selectedSupplierIds.Add(productId);
                            }

                            // Update status to show selected count
                            UpdateSelectionStatus();
                        }
                    }
                    else
                    {
                        RemoveSupplierBtn.Visible = true;
                        UpdateSupplierBtn.Visible = true;
                        DataGridViewRow row = SupplierListDataGrid.Rows[e.RowIndex];
                        SupplierNameTxt.Text = row.Cells["Name"].Value.ToString();
                        SupplierIdTxt.Text = row.Cells["ID"].Value.ToString();
                        SupplierShopNameTxt.Text = row.Cells["Shop"].Value.ToString();
                        SupplierAddressTxt.Text = row.Cells["Address"].Value.ToString();
                        SupplierPhoneTxt.Text = row.Cells["Phone"].Value.ToString();
                        if (row.Cells["CityId"].Value != null)
                        {
                            using (var context = new POSDbContext())
                            {
                                var countryId = context.Cities.Find((int)row.Cells["CityId"].Value).CountryId;

                                CountryDropDownLst.SelectedValue = countryId;
                                CityDropDownLst.SelectedValue = (int)row.Cells["CityId"].Value;
                            }
                        }
                        CustomerActiveChkBox.Checked = (bool)row.Cells["Active"].Value;
                    }
                }
            }
        }


        private void UpdateSelectionStatus()
        {
            if (selectedSupplierIds.Count <= 0)
            {
                ClearAllSelectionBtn.Visible = false;
            }
            else
            {
                ClearAllSelectionBtn.Visible = true;
            }

            selectedProdLbl.Text = $"Selected: {selectedSupplierIds.Count} Supplier(s)";
        }
    }
}
