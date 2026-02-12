using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using POS_Shop.Helpers;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS_Shop.Views.Controllers.Customers
{
    public partial class CustomerFormControl : UserControl
    {

        private int PageSize = 100;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";

        private HashSet<int> selectedProductIds = new HashSet<int>();
        public CustomerFormControl()
        {
            InitializeComponent();
           
            CustomerListDataGrid.RowTemplate.Height = 32;
            this.Load += CustomerFormControl_Load;
        }

        private async void CustomerFormControl_Load(object sender, EventArgs e)
        {
            LoadCountriesForDropdown();
            await LoadCustomersForDataGridView();
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

        private async void SaveCustomerBtn_Click(object sender, EventArgs e)
        {
            

            var model = new Models.Customer()
                {
                CustomerName = CustomerNameTxt.Text,
                ContactNo = CustomerPhoneTxt.Text,
                CustomerAddress = CustomerAddressTxt.Text,
                CityId = Convert.ToInt32(CityDropDownLst.SelectedValue),
                IsDeleted = false
            };
            var errors =new  StringBuilder();
            if (!model.IsValid(out var results))
            {
                 errors.AppendLine(string.Join("\n", results.Select(r => r.ErrorMessage)));
                MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           

            using (var context = new POSDbContext())
            {

                ICustomerRepository customerRepository = new CustomerRepository(context);
                if (await customerRepository.CheckRecoradAlreadyExistByName(model.CustomerName, model.CustomerAddress))
                {
                    MessageBox.Show($"Customer name '{model.CustomerName}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                context.Customers.Add(model);
                context.SaveChanges();

                ClearFormFunction();
                await LoadCustomersForDataGridView();
                MessageBox.Show("Customer saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void UpdateCustomerBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerIdTxt.Text) || !int.TryParse(CustomerIdTxt.Text, out int customerId) || customerId <= 0)
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
            var model = new Models.Customer()
            {
                Id = customerId,
                CustomerName = CustomerNameTxt.Text,
                ContactNo = CustomerPhoneTxt.Text,
                CustomerAddress = CustomerAddressTxt.Text,
                CityId = Convert.ToInt32(CityDropDownLst.SelectedValue),
                IsDeleted = !CustomerActiveChkBox.Checked
            };
            if (!model.IsValid(out var results))
            {
                var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var context = new POSDbContext())
            {
                ICustomerRepository customerRepository = new CustomerRepository(context);
                var existingRecord = customerRepository.GetById(customerId);
                if (existingRecord == null)
                {
                    MessageBox.Show("Customer not found for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Check for duplicate name excluding the current record
                if (context.Customers.Any(c => c.CustomerName.Equals(model.CustomerName, StringComparison.OrdinalIgnoreCase) && c.Id != customerId && c.CustomerAddress.Equals(model.CustomerAddress, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show($"Customer name '{model.CustomerName}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Update fields
                existingRecord.CustomerName = model.CustomerName;
                existingRecord.ContactNo = model.ContactNo;
                existingRecord.CustomerAddress = model.CustomerAddress;
                existingRecord.CityId = model.CityId;
                existingRecord.IsDeleted = model.IsDeleted;
                customerRepository.Update(existingRecord);
                customerRepository.Save();
                ClearFormFunction();
               await LoadCustomersForDataGridView();
                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // Clear form fields
        private void ClearFormFunction()
        {
            CustomerNameTxt.Clear();
            CustomerPhoneTxt.Clear();
            CustomerAddressTxt.Clear();
            CountryDropDownLst.SelectedIndex = 0;
            CityDropDownLst.DataSource = null;
            CityDropDownLst.Items.Clear();
        }

        private async Task LoadCustomersForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                ICustomerRepository customerRepository = new CustomerRepository(context);
                var result = await customerRepository.GetCustomerPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;

                DataTable dt = new DataTable();
                dt.Columns.Add("IsSelected", typeof(bool)); // Add selection column
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("CityId", typeof(int));
                dt.Columns.Add("City Name", typeof(string));
                dt.Columns.Add("Active", typeof(bool));
         
                foreach (var item in result.data)
                {
                    bool isSelected = selectedProductIds.Contains(item.Id);
                    dt.Rows.Add(isSelected,item.Id, item.CustomerName, item.CustomerAddress,
                                item.ContactNo, item.CityId, item.CityName, !item.IsDeleted);
                }

                //CustomerListDataGrid.ReadOnly = true;
                CustomerListDataGrid.AllowUserToAddRows = false;


                CustomerListDataGrid.AllowUserToResizeColumns = true;
            CustomerListDataGrid.AllowUserToResizeRows = false;
            CustomerListDataGrid.RowHeadersVisible = false;
            CustomerListDataGrid.BackgroundColor = SystemColors.Window;
            CustomerListDataGrid.BorderStyle = BorderStyle.None;
            CustomerListDataGrid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            CustomerListDataGrid.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9);
            CustomerListDataGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            CustomerListDataGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
            CustomerListDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                CustomerListDataGrid.EnableHeadersVisualStyles = false;
                //ProductListGrid.AutoGenerateColumns = false;

                CustomerListDataGrid.DataSource = dt;
                CustomerListDataGrid.Columns[1].Visible = false;
                CustomerListDataGrid.Columns[5].Visible = false;
                
                CustomerListDataGrid.Columns["IsSelected"].Width = 50;

                //// Checkbox column bound to DataTable field
                //DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
                //{
                //    Name = "IsSelected",
                //    DataPropertyName = "IsSelected",
                //    HeaderText = "",
                //    Width = 30,
                //    ReadOnly = false,
                //    FlatStyle = FlatStyle.Standard
                //};
                //CustomerListDataGrid.Columns.Add(chk);
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
                await LoadCustomersForDataGridView();
            }
        }

        private async void PreviousPageBtn_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadCustomersForDataGridView();
            }
        }

        private async void CustomerSearchTxt_TextChanged(object sender, EventArgs e)
        {
            PageIndex = 1;
            SearchTerm = CustomerSearchTxt.Text.Trim();
            await LoadCustomersForDataGridView();
        }   

        private void CustomerListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is not on the header row (-1)
            if (e.RowIndex != -1)
            {
                if (e.RowIndex >= 0)
                {

                    // Get the index of the clicked column
                    int columnIndex = e.ColumnIndex;

                    // Get the name of the clicked column
                    string columnName = CustomerListDataGrid.Columns[columnIndex].Name;
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
                                selectedProductIds.Remove(productId);
     
                            }
                            else
                            {
                                selectedProductIds.Add(productId);
                            }

                            // Update status to show selected count
                            UpdateSelectionStatus();
                        }
                    }
                    else
                    {
                        RemoveCustomerBtn.Visible = true;
                        UpdateCustomerBtn.Visible = true;
                        DataGridViewRow row = CustomerListDataGrid.Rows[e.RowIndex];
                        CustomerNameTxt.Text = row.Cells["Name"].Value.ToString();
                        CustomerIdTxt.Text = row.Cells["ID"].Value.ToString();

                        CustomerAddressTxt.Text = row.Cells["Address"].Value.ToString();
                        CustomerPhoneTxt.Text = row.Cells["Phone"].Value.ToString();
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

        private async void RemoveCustomerBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this Customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
            
                var customerId=Convert.ToInt32(CustomerIdTxt.Text);
                using (var context = new POSDbContext())
                {
                    var productRepo = new CustomerRepository(context);
                    var data = productRepo.GetById(customerId);
                    if (data != null)
                    {
                        try
                        {

                            productRepo.Delete(customerId);
                            productRepo.Save();
                            MessageBox.Show("Customer deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            await LoadCustomersForDataGridView();
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

        private async void ResetFormBtn_Click(object sender, EventArgs e)
        {
            ClearFormFunction();
            await LoadCustomersForDataGridView();
        }

        private async void SelectAllBtn_Click(object sender, EventArgs e)
        {
            // Get all IDs on current page and add to selection
            var dataTable = (DataTable)CustomerListDataGrid.DataSource;
            foreach (DataRow row in dataTable.Rows)
            {
                int productId = Convert.ToInt32(row["ID"]);
                selectedProductIds.Add(productId);
            }

            // Reload to update checkboxes
            await LoadCustomersForDataGridView();

            selectedProdLbl.Text = $"Selected: {selectedProductIds.Count} Customer(s)";
        }

        private void ClearAllSelectionBtn_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        // Method to clear selection
        public async void ClearSelection()
        {
            selectedProductIds.Clear();
            // Reload current page to update checkboxes
            selectedProdLbl.Text = $"Selected: {selectedProductIds.Count} Customer(s)";
            await LoadCustomersForDataGridView();
        }

        //private void CustomerListDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Only handle checkbox column

        //    var grid = (DataGridView)sender;
        //    var checkboxCell = grid.Rows[e.RowIndex].Cells[7] as DataGridViewCheckBoxCell;
        //    var idCell = grid.Rows[e.RowIndex].Cells["ID"];

        //    if (checkboxCell != null && idCell != null && idCell.Value != null)
        //    {
        //        bool isChecked = Convert.ToBoolean(checkboxCell.Value);
        //        int productId = Convert.ToInt32(idCell.Value);

        //        if (isChecked)
        //        {
        //            selectedProductIds.Add(productId);
        //        }
        //        else
        //        {
        //            selectedProductIds.Remove(productId);
        //        }

        //        // Update status to show selected count
        //        UpdateSelectionStatus();
        //    }
        //}


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

            selectedProdLbl.Text = $"Selected: {selectedProductIds.Count} Customer(s)";
        }

        private void CustomerListDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {  // Commit changes immediately when checkbox is toggled
            if (CustomerListDataGrid.CurrentCell is DataGridViewCheckBoxCell)
            {
                CustomerListDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }

        private async void ExportAllBtn_Click(object sender, EventArgs e)
        {
            if (selectedProductIds.Count == 0)
            {
                MessageBox.Show("No Customer selected for export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            using (var context = new POSDbContext())
            {
                var customerRepo = new CustomerRepository(context);
                //var selectedProducts = productRepo.GetAll(selectedProductIds.ToList()).Result;
                var selectedProducts = await customerRepo.GetAll(selectedProductIds.ToList());
                if (selectedProducts.Count() > 0)
                {
                    DataTable exportTable = new DataTable();
                    exportTable.TableName = "Customer";

                    // Add columns
                    
                    exportTable.Columns.Add("ID", typeof(int));
                    exportTable.Columns.Add("Name", typeof(string));
                    exportTable.Columns.Add("Address", typeof(string));
                    exportTable.Columns.Add("Phone", typeof(string));
                    exportTable.Columns.Add("CityId", typeof(int));
                    exportTable.Columns.Add("Active", typeof(bool));
                    // Add rows
                    foreach (var product in selectedProducts)
                    {
                        exportTable.Rows.Add(
                            product.Id,
                            product.CustomerName,
                            product.CustomerAddress,
                            product.ContactNo,
                            product.CityId,
                            !product.IsDeleted
                        );
                    }

                    // 3. Ask where to save the file
                    using (var sfd = new SaveFileDialog
                    {
                        Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                        FileName = "CustomerList.xlsx"
                    })
                    {
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            // 4. Write to Excel using ClosedXML
                            using (var workbook = new XLWorkbook())
                            {
                                workbook.Worksheets.Add(exportTable, "Customers");
                                workbook.SaveAs(sfd.FileName);
                            }
                            MessageBox.Show("Export successful!");
                        }
                    }
                    // Export logic here - for demo, we'll just show count
                    MessageBox.Show($"{selectedProducts.Count()} Records ready for export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearSelection();
                }
                else
                {
                    MessageBox.Show("No Customer found for the selected IDs.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
