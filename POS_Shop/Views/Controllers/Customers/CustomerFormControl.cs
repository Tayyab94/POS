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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Customers
{
    public partial class CustomerFormControl : UserControl
    {


        private int PageSize = 50;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";
        public CustomerFormControl()
        {
            InitializeComponent();
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
                allItems.Add(new { Id = 0, Name = "Select Category" });
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
        private void CustomerNameTxt_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerNameTxt.Text))
            {

                e.Cancel = true;
                CustomerFormErrorProvider.SetError(CustomerNameTxt, "Customer Name is required.");
            }
            else
            {
                e.Cancel = false;
                CustomerFormErrorProvider.SetError(CustomerNameTxt, null);
            }
        }
   

        private void CustomerPhoneTxt_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerPhoneTxt.Text))
            {

                e.Cancel = true;
                CustomerFormErrorProvider.SetError(CustomerPhoneTxt, "Customer Phone No. is required.");
            }
            else
            {
                e.Cancel = false;
                CustomerFormErrorProvider.SetError(CustomerPhoneTxt, null);
            }
        }



        private void CustomerAddressTxt_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerAddressTxt.Text))
            {

                e.Cancel = true;
                CustomerFormErrorProvider.SetError(CustomerAddressTxt, "Customer Address is required.");
            }
            else
            {
                e.Cancel = false;
                CustomerFormErrorProvider.SetError(CustomerAddressTxt, null);
            }
        }

        private void CityDropDownLst_Validating(object sender, CancelEventArgs e)
        {
            // Check if the dropdown has a null value or no selection (index -1)
            if (CityDropDownLst.SelectedItem == null || CityDropDownLst.SelectedIndex == -1 || (int)CityDropDownLst.SelectedValue==0)
            {
                // Set error and cancel the validation
                e.Cancel = true;
                CustomerFormErrorProvider.SetError(CityDropDownLst, "Please select a valid city");
                return;
            }

            // Check if the selected value is empty or whitespace only
            string selectedValue = CityDropDownLst.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(selectedValue))
            {
                e.Cancel = true;
                CustomerFormErrorProvider.SetError(CityDropDownLst, "Please select a valid city");
                return;
            }

            e.Cancel = false;
            // If validation passes, clear any previous error
            CustomerFormErrorProvider.SetError(CityDropDownLst, null);
        }

        private async void SaveCustomerBtn_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled) == false)
            {
                // There are invalid controls
                MessageBox.Show("Please correct the errors before Submitting", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var model = new Models.Customer()
                {
                CustomerName = CustomerNameTxt.Text,
                ContactNo = CustomerPhoneTxt.Text,
                CustomerAddress = CustomerAddressTxt.Text,
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

                ICustomerRepository customerRepository = new CustomerRepository(context);
                if (await customerRepository.CheckRecoradAlreadyExistByName(model.CustomerName))
                {
                    MessageBox.Show($"Product with name '{model.CustomerName}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (context.Customers.Any(c => c.CustomerName.Equals(model.CustomerName, StringComparison.OrdinalIgnoreCase) && c.Id != customerId))
                {
                    MessageBox.Show($"Customer with name '{model.CustomerName}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                //dt.Columns.Add("IsSelected", typeof(bool)); // Add selection column
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("CityId", typeof(int));
                dt.Columns.Add("City Name", typeof(string));
                dt.Columns.Add("Active", typeof(bool));

                foreach (var item in result.data)
                {
                    
                    dt.Rows.Add( item.Id, item.CustomerName, item.CustomerAddress,
                                item.ContactNo, item.CityId, item.CityName, !item.IsDeleted);
                }

                CustomerListDataGrid.ReadOnly = true;
                CustomerListDataGrid.AllowUserToAddRows = false;
                //ProductListGrid.AutoGenerateColumns = false;

                CustomerListDataGrid.DataSource = dt;
                CustomerListDataGrid.Columns[0].Visible = false;
                CustomerListDataGrid.Columns[4].Visible = false;

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
            if (e.RowIndex >= 0)
            {
                RemoveCustomerBtn.Visible = true;
                UpdateCustomerBtn.Visible = true;
                DataGridViewRow row = CustomerListDataGrid.Rows[e.RowIndex];
                CustomerNameTxt.Text = row.Cells["Name"].Value.ToString();
                CustomerIdTxt.Text = row.Cells["ID"].Value.ToString();

                CustomerAddressTxt.Text = row.Cells["Address"].Value.ToString();
                CustomerPhoneTxt.Text = row.Cells["Phone"].Value.ToString();
                if(row.Cells["CityId"].Value != null)
                {
                    using (var context = new POSDbContext())
                    {
                        var countryId = context.Cities.Find((int)row.Cells["CityId"].Value).CountryId;
                        
                        CountryDropDownLst.SelectedValue = countryId;
                        CityDropDownLst.SelectedValue = (int)row.Cells["CityId"].Value;
                    }
                }
              

                CustomerActiveChkBox.Checked =(bool)row.Cells["Active"].Value;
                
            }
        }

        private async void RemoveCustomerBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this Customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                var customerId=-Convert.ToInt32(CustomerIdTxt.Text);
                using (var context = new POSDbContext())
                {
                    var productRepo = new CustomerRepository(context);
                    var data = productRepo.GetById(customerId);
                    if (data != null)
                    {
                        productRepo.Delete(customerId);
                        productRepo.Save();
                        MessageBox.Show("Customer deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                        await LoadCustomersForDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Customer not found for deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
