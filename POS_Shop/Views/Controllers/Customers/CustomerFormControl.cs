//using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Wordprocessing;
//using POS_Shop.Helpers;
//using POS_Shop.Interfaces;
//using POS_Shop.Models;
//using POS_Shop.Repositories;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//namespace POS_Shop.Views.Controllers.Customers
//{
//    public partial class CustomerFormControl : UserControl
//    {

//        private int PageSize = 100;
//        private int PageIndex = 1;
//        private int RecordCount = 0;
//        private string SearchTerm = "";

//        private HashSet<int> selectedProductIds = new HashSet<int>();
//        public CustomerFormControl()
//        {
//            InitializeComponent();

//            CustomerListDataGrid.RowTemplate.Height = 32;
//            this.Load += CustomerFormControl_Load;
//        }

//        private async void CustomerFormControl_Load(object sender, EventArgs e)
//        {
//            LoadCountriesForDropdown();
//            await LoadCustomersForDataGridView();
//        }

//        private void LoadCountriesForDropdown()
//        {

//            CountryDropDownLst.SelectedIndexChanged -= CountryDropDownLst_SelectedIndexChanged;
//            using (var context = new POSDbContext())
//            {
//                var countriesList = context.Countries.Select(s => new
//                {
//                    Id = s.Id,
//                    Name = s.CountryName
//                }).ToList();
//                CountryDropDownLst.Items.Clear();


//                // Add default option
//                var allItems = new List<object>();
//                allItems.Add(new { Id = 0, Name = "Select Country" });
//                allItems.AddRange(countriesList);
//                CountryDropDownLst.DataSource = allItems;
//                CountryDropDownLst.DisplayMember = "Name";
//                CountryDropDownLst.ValueMember = "Id";
//            }

//            // Subscribe AFTER data is loaded
//            CountryDropDownLst.SelectedIndexChanged += CountryDropDownLst_SelectedIndexChanged;
//        }

//        private void CountryDropDownLst_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            // Check if SelectedValue is null or the default option
//            if (CountryDropDownLst.SelectedValue == null ||
//                Convert.ToInt32(CountryDropDownLst.SelectedValue) == 0)
//            {
//                // Clear subcategory dropdown if no category selected
//                CityDropDownLst.DataSource = null;
//                CityDropDownLst.Items.Clear();
//                return;
//            }

//            // Get the selected ID as integer
//            int selectedId = Convert.ToInt32(CountryDropDownLst.SelectedValue);

//            // Load subcategories based on the selected category ID
//            using (var context = new POSDbContext())
//            {
//                var CitiesList = context.Cities
//                    .Where(s => s.CountryId == selectedId).Select(s => new
//                    {
//                        Id = s.Id,
//                        Name = s.Name
//                    })
//                    .ToList();

//                // Add default option for subcategories
//                var allSubItems = new List<object>();
//                allSubItems.Add(new { Id = 0, Name = "Select City" });
//                allSubItems.AddRange(CitiesList);
//                CityDropDownLst.DataSource = allSubItems;
//                CityDropDownLst.DisplayMember = "Name";
//                CityDropDownLst.ValueMember = "Id";
//                CityDropDownLst.SelectedIndex = 0;

//            }
//        }

//        private async void SaveCustomerBtn_Click(object sender, EventArgs e)
//        {


//            var model = new Models.Customer()
//                {
//                CustomerName = CustomerNameTxt.Text,
//                ContactNo = CustomerPhoneTxt.Text,
//                CustomerAddress = CustomerAddressTxt.Text,
//                CityId = Convert.ToInt32(CityDropDownLst.SelectedValue),
//                IsDeleted = false
//            };
//            var errors =new  StringBuilder();
//            if (!model.IsValid(out var results))
//            {
//                 errors.AppendLine(string.Join("\n", results.Select(r => r.ErrorMessage)));
//                MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }


//            using (var context = new POSDbContext())
//            {

//                ICustomerRepository customerRepository = new CustomerRepository(context);
//                if (await customerRepository.CheckRecoradAlreadyExistByName(model.CustomerName, model.CustomerAddress))
//                {
//                    MessageBox.Show($"Customer name '{model.CustomerName}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//                context.Customers.Add(model);
//                context.SaveChanges();

//                ClearFormFunction();
//                await LoadCustomersForDataGridView();
//                MessageBox.Show("Customer saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }

//        private async void UpdateCustomerBtn_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(CustomerIdTxt.Text) || !int.TryParse(CustomerIdTxt.Text, out int customerId) || customerId <= 0)
//            {
//                MessageBox.Show("Please select Record first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            if (ValidateChildren(ValidationConstraints.Enabled) == false)
//            {
//                // There are invalid controls
//                MessageBox.Show("Please correct the errors before Submitting", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }
//            var model = new Models.Customer()
//            {
//                Id = customerId,
//                CustomerName = CustomerNameTxt.Text,
//                ContactNo = CustomerPhoneTxt.Text,
//                CustomerAddress = CustomerAddressTxt.Text,
//                CityId = Convert.ToInt32(CityDropDownLst.SelectedValue),
//                IsDeleted = !CustomerActiveChkBox.Checked
//            };
//            if (!model.IsValid(out var results))
//            {
//                var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
//                MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }
//            using (var context = new POSDbContext())
//            {
//                ICustomerRepository customerRepository = new CustomerRepository(context);
//                var existingRecord = customerRepository.GetById(customerId);
//                if (existingRecord == null)
//                {
//                    MessageBox.Show("Customer not found for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    return;
//                }
//                // Check for duplicate name excluding the current record
//                if (context.Customers.Any(c => c.CustomerName.Equals(model.CustomerName, StringComparison.OrdinalIgnoreCase) && c.Id != customerId && c.CustomerAddress.Equals(model.CustomerAddress, StringComparison.OrdinalIgnoreCase)))
//                {
//                    MessageBox.Show($"Customer name '{model.CustomerName}' already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//                // Update fields
//                existingRecord.CustomerName = model.CustomerName;
//                existingRecord.ContactNo = model.ContactNo;
//                existingRecord.CustomerAddress = model.CustomerAddress;
//                existingRecord.CityId = model.CityId;
//                existingRecord.IsDeleted = model.IsDeleted;
//                customerRepository.Update(existingRecord);
//                customerRepository.Save();
//                ClearFormFunction();
//               await LoadCustomersForDataGridView();
//                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }


//        // Clear form fields
//        private void ClearFormFunction()
//        {
//            CustomerNameTxt.Clear();
//            CustomerPhoneTxt.Clear();
//            CustomerAddressTxt.Clear();
//            CountryDropDownLst.SelectedIndex = 0;
//            CityDropDownLst.DataSource = null;
//            CityDropDownLst.Items.Clear();
//        }

//        private async Task LoadCustomersForDataGridView()
//        {
//            using (var context = new POSDbContext())
//            {
//                ICustomerRepository customerRepository = new CustomerRepository(context);
//                var result = await customerRepository.GetCustomerPagingListAsync(PageIndex, PageSize, SearchTerm);
//                RecordCount = result.totalCount;

//                DataTable dt = new DataTable();
//                dt.Columns.Add("IsSelected", typeof(bool)); // Add selection column
//                dt.Columns.Add("ID", typeof(int));
//                dt.Columns.Add("Name", typeof(string));
//                dt.Columns.Add("Address", typeof(string));
//                dt.Columns.Add("Phone", typeof(string));
//                dt.Columns.Add("CityId", typeof(int));
//                dt.Columns.Add("City Name", typeof(string));
//                dt.Columns.Add("Active", typeof(bool));

//                foreach (var item in result.data)
//                {
//                    bool isSelected = selectedProductIds.Contains(item.Id);
//                    dt.Rows.Add(isSelected,item.Id, item.CustomerName, item.CustomerAddress,
//                                item.ContactNo, item.CityId, item.CityName, !item.IsDeleted);
//                }

//                //CustomerListDataGrid.ReadOnly = true;
//                CustomerListDataGrid.AllowUserToAddRows = false;


//                CustomerListDataGrid.AllowUserToResizeColumns = true;
//            CustomerListDataGrid.AllowUserToResizeRows = false;
//            CustomerListDataGrid.RowHeadersVisible = false;
//            CustomerListDataGrid.BackgroundColor = SystemColors.Window;
//            CustomerListDataGrid.BorderStyle = BorderStyle.None;
//            CustomerListDataGrid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
//            CustomerListDataGrid.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9);
//            CustomerListDataGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
//            CustomerListDataGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
//            CustomerListDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
//                CustomerListDataGrid.EnableHeadersVisualStyles = false;
//                //ProductListGrid.AutoGenerateColumns = false;

//                CustomerListDataGrid.DataSource = dt;
//                CustomerListDataGrid.Columns[1].Visible = false;
//                CustomerListDataGrid.Columns[5].Visible = false;

//                CustomerListDataGrid.Columns["IsSelected"].Width = 50;

//                //// Checkbox column bound to DataTable field
//                //DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
//                //{
//                //    Name = "IsSelected",
//                //    DataPropertyName = "IsSelected",
//                //    HeaderText = "",
//                //    Width = 30,
//                //    ReadOnly = false,
//                //    FlatStyle = FlatStyle.Standard
//                //};
//                //CustomerListDataGrid.Columns.Add(chk);
//                UpdatePager();
//            }
//        }


//        private void UpdatePager()
//        {
//            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
//            lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

//            PreviousPageBtn.Enabled = PageIndex > 1;
//            NextPageBtn.Enabled = PageIndex < totalPages;
//        }


//        private async void NextPageBtn_Click(object sender, EventArgs e)
//        {
//            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
//            if (PageIndex < totalPages)
//            {
//                PageIndex++;
//                await LoadCustomersForDataGridView();
//            }
//        }

//        private async void PreviousPageBtn_Click(object sender, EventArgs e)
//        {
//            if (PageIndex > 1)
//            {
//                PageIndex--;
//                await LoadCustomersForDataGridView();
//            }
//        }

//        private async void CustomerSearchTxt_TextChanged(object sender, EventArgs e)
//        {
//            PageIndex = 1;
//            SearchTerm = CustomerSearchTxt.Text.Trim();
//            await LoadCustomersForDataGridView();
//        }   

//        private void CustomerListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            // Ensure the click is not on the header row (-1)
//            if (e.RowIndex != -1)
//            {
//                if (e.RowIndex >= 0)
//                {

//                    // Get the index of the clicked column
//                    int columnIndex = e.ColumnIndex;

//                    // Get the name of the clicked column
//                    string columnName = CustomerListDataGrid.Columns[columnIndex].Name;
//                    // You can add specific logic based on the column name or index
//                    if (columnName == "IsSelected")
//                    {

//                        var grid = (DataGridView)sender;
//                        var checkboxCell = grid.Rows[e.RowIndex].Cells["IsSelected"] as DataGridViewCheckBoxCell;
//                        var idCell = grid.Rows[e.RowIndex].Cells["ID"];

//                        if (checkboxCell != null && idCell != null && idCell.Value != null)
//                        {
//                            bool shoulRemove = Convert.ToBoolean(checkboxCell.Value);
//                            int productId = Convert.ToInt32(idCell.Value);

//                            if (shoulRemove)
//                            {
//                                selectedProductIds.Remove(productId);

//                            }
//                            else
//                            {
//                                selectedProductIds.Add(productId);
//                            }

//                            // Update status to show selected count
//                            UpdateSelectionStatus();
//                        }
//                    }
//                    else
//                    {
//                        RemoveCustomerBtn.Visible = true;
//                        UpdateCustomerBtn.Visible = true;
//                        DataGridViewRow row = CustomerListDataGrid.Rows[e.RowIndex];
//                        CustomerNameTxt.Text = row.Cells["Name"].Value.ToString();
//                        CustomerIdTxt.Text = row.Cells["ID"].Value.ToString();

//                        CustomerAddressTxt.Text = row.Cells["Address"].Value.ToString();
//                        CustomerPhoneTxt.Text = row.Cells["Phone"].Value.ToString();
//                        if (row.Cells["CityId"].Value != null)
//                        {
//                            using (var context = new POSDbContext())
//                            {
//                                var countryId = context.Cities.Find((int)row.Cells["CityId"].Value).CountryId;

//                                CountryDropDownLst.SelectedValue = countryId;
//                                CityDropDownLst.SelectedValue = (int)row.Cells["CityId"].Value;
//                            }
//                        }
//                        CustomerActiveChkBox.Checked = (bool)row.Cells["Active"].Value;
//                    }
//                }
//            }
//        }

//        private async void RemoveCustomerBtn_Click(object sender, EventArgs e)
//        {
//            var confirmResult = MessageBox.Show("Are you sure to delete this Customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
//            if (confirmResult == DialogResult.Yes)
//            {

//                var customerId=Convert.ToInt32(CustomerIdTxt.Text);
//                using (var context = new POSDbContext())
//                {
//                    var productRepo = new CustomerRepository(context);
//                    var data = productRepo.GetById(customerId);
//                    if (data != null)
//                    {
//                        try
//                        {

//                            productRepo.Delete(customerId);
//                            productRepo.Save();
//                            MessageBox.Show("Customer deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                            await LoadCustomersForDataGridView();
//                        }

//                        catch (DbUpdateException dbEx) when (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 547)
//                        {
//                            MessageBox.Show("This CUSTOMER is being used by other records and cannot be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            // Don't navigate away, stay on current page
//                            context.ChangeTracker.DetectChanges();
//                        }

//                    }
//                    else
//                    {
//                        MessageBox.Show("Customer not found for deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//            }
//        }

//        private async void ResetFormBtn_Click(object sender, EventArgs e)
//        {
//            ClearFormFunction();
//            await LoadCustomersForDataGridView();
//        }

//        private async void SelectAllBtn_Click(object sender, EventArgs e)
//        {
//            // Get all IDs on current page and add to selection
//            var dataTable = (DataTable)CustomerListDataGrid.DataSource;
//            foreach (DataRow row in dataTable.Rows)
//            {
//                int productId = Convert.ToInt32(row["ID"]);
//                selectedProductIds.Add(productId);
//            }

//            // Reload to update checkboxes
//            await LoadCustomersForDataGridView();

//            selectedProdLbl.Text = $"Selected: {selectedProductIds.Count} Customer(s)";
//        }

//        private void ClearAllSelectionBtn_Click(object sender, EventArgs e)
//        {
//            ClearSelection();
//        }

//        // Method to clear selection
//        public async void ClearSelection()
//        {
//            selectedProductIds.Clear();
//            // Reload current page to update checkboxes
//            selectedProdLbl.Text = $"Selected: {selectedProductIds.Count} Customer(s)";
//            await LoadCustomersForDataGridView();
//        }

//        //private void CustomerListDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
//        //{
//        //    if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Only handle checkbox column

//        //    var grid = (DataGridView)sender;
//        //    var checkboxCell = grid.Rows[e.RowIndex].Cells[7] as DataGridViewCheckBoxCell;
//        //    var idCell = grid.Rows[e.RowIndex].Cells["ID"];

//        //    if (checkboxCell != null && idCell != null && idCell.Value != null)
//        //    {
//        //        bool isChecked = Convert.ToBoolean(checkboxCell.Value);
//        //        int productId = Convert.ToInt32(idCell.Value);

//        //        if (isChecked)
//        //        {
//        //            selectedProductIds.Add(productId);
//        //        }
//        //        else
//        //        {
//        //            selectedProductIds.Remove(productId);
//        //        }

//        //        // Update status to show selected count
//        //        UpdateSelectionStatus();
//        //    }
//        //}


//        private void UpdateSelectionStatus()
//        {
//            if (selectedProductIds.Count <= 0)
//            {
//                ClearAllSelectionBtn.Visible = false;
//            }
//            else
//            {
//                ClearAllSelectionBtn.Visible = true;
//            }

//            selectedProdLbl.Text = $"Selected: {selectedProductIds.Count} Customer(s)";
//        }

//        private void CustomerListDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
//        {  // Commit changes immediately when checkbox is toggled
//            if (CustomerListDataGrid.CurrentCell is DataGridViewCheckBoxCell)
//            {
//                CustomerListDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
//            }

//        }

//        private async void ExportAllBtn_Click(object sender, EventArgs e)
//        {
//            if (selectedProductIds.Count == 0)
//            {
//                MessageBox.Show("No Customer selected for export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }


//            using (var context = new POSDbContext())
//            {
//                var customerRepo = new CustomerRepository(context);
//                //var selectedProducts = productRepo.GetAll(selectedProductIds.ToList()).Result;
//                var selectedProducts = await customerRepo.GetAll(selectedProductIds.ToList());
//                if (selectedProducts.Count() > 0)
//                {
//                    DataTable exportTable = new DataTable();
//                    exportTable.TableName = "Customer";

//                    // Add columns

//                    exportTable.Columns.Add("ID", typeof(int));
//                    exportTable.Columns.Add("Name", typeof(string));
//                    exportTable.Columns.Add("Address", typeof(string));
//                    exportTable.Columns.Add("Phone", typeof(string));
//                    exportTable.Columns.Add("CityId", typeof(int));
//                    exportTable.Columns.Add("Active", typeof(bool));
//                    // Add rows
//                    foreach (var product in selectedProducts)
//                    {
//                        exportTable.Rows.Add(
//                            product.Id,
//                            product.CustomerName,
//                            product.CustomerAddress,
//                            product.ContactNo,
//                            product.CityId,
//                            !product.IsDeleted
//                        );
//                    }

//                    // 3. Ask where to save the file
//                    using (var sfd = new SaveFileDialog
//                    {
//                        Filter = "Excel Workbook (*.xlsx)|*.xlsx",
//                        FileName = "CustomerList.xlsx"
//                    })
//                    {
//                        if (sfd.ShowDialog() == DialogResult.OK)
//                        {
//                            // 4. Write to Excel using ClosedXML
//                            using (var workbook = new XLWorkbook())
//                            {
//                                workbook.Worksheets.Add(exportTable, "Customers");
//                                workbook.SaveAs(sfd.FileName);
//                            }
//                            MessageBox.Show("Export successful!");
//                        }
//                    }
//                    // Export logic here - for demo, we'll just show count
//                    MessageBox.Show($"{selectedProducts.Count()} Records ready for export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    ClearSelection();
//                }
//                else
//                {
//                    MessageBox.Show("No Customer found for the selected IDs.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
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
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Customers
{
    public partial class CustomerFormControl : UserControl
    {
        // ═══════════════════════════════════════════════════════════════════════════
        //  COLUMN NAME CONSTANTS
        // ═══════════════════════════════════════════════════════════════════════════
        private static class Col
        {
            public const string IsSelected = "IsSelected";
            public const string ID = "ID";
            public const string Name = "Name";
            public const string Address = "Address";
            public const string Phone = "Phone";
            public const string CityId = "CityId";
            public const string CityName = "CityName";
            public const string Active = "Active";
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  PAGINATION STATE
        // ═══════════════════════════════════════════════════════════════════════════
        private const int PAGE_SIZE = 100;
        private int _pageIndex = 1;
        private int _recordCount = 0;

        // ═══════════════════════════════════════════════════════════════════════════
        //  SEARCH DEBOUNCE
        // ═══════════════════════════════════════════════════════════════════════════
        private readonly System.Windows.Forms.Timer _searchDebounceTimer;
        private const int SEARCH_DEBOUNCE_MS = 350;
        private string _searchTerm = "";

        // ═══════════════════════════════════════════════════════════════════════════
        //  CANCELLATION + LOADING GUARD
        // ═══════════════════════════════════════════════════════════════════════════
        private CancellationTokenSource _loadCts = new CancellationTokenSource();
        private bool _isLoading = false;

        // ═══════════════════════════════════════════════════════════════════════════
        //  SELECTION STATE
        // ═══════════════════════════════════════════════════════════════════════════
        private readonly HashSet<int> _selectedCustomerIds = new HashSet<int>();

        // ═══════════════════════════════════════════════════════════════════════════
        //  ONE-TIME GRID CONFIG FLAG
        // ═══════════════════════════════════════════════════════════════════════════
        private bool _gridConfigured = false;

        // ═══════════════════════════════════════════════════════════════════════════
        //  CONSTRUCTOR
        // ═══════════════════════════════════════════════════════════════════════════
        public CustomerFormControl()
        {
            InitializeComponent();

            CustomerListDataGrid.RowTemplate.Height = 32;

            _searchDebounceTimer = new System.Windows.Forms.Timer();
            _searchDebounceTimer.Interval = SEARCH_DEBOUNCE_MS;
            _searchDebounceTimer.Tick += SearchDebounceTimer_Tick;

            this.Load += CustomerFormControl_Load;
            this.Disposed += CustomerFormControl_Disposed;
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  LOAD & DISPOSE
        // ═══════════════════════════════════════════════════════════════════════════
        private async void CustomerFormControl_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadCountriesForDropdown();
            await LoadCustomersAsync();
        }

        private void CustomerFormControl_Disposed(object sender, EventArgs e)
        {
            _loadCts?.Cancel();
            _loadCts?.Dispose();
            _searchDebounceTimer?.Stop();
            _searchDebounceTimer?.Dispose();
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  DROPDOWN LOADERS
        // ═══════════════════════════════════════════════════════════════════════════
        private void LoadCountriesForDropdown()
        {
            CountryDropDownLst.SelectedIndexChanged -= CountryDropDownLst_SelectedIndexChanged;

            using (var ctx = new POSDbContext())
            {
                BindDropdown(CountryDropDownLst,
                    BuildDropdownItems(
                        ctx.Countries.Select(c => new { Id = c.Id, Name = c.CountryName }).ToList(),
                        "Select Country"));
            }

            CountryDropDownLst.SelectedIndexChanged += CountryDropDownLst_SelectedIndexChanged;
        }

        private void LoadCitiesForDropdown(int countryId)
        {
            using (var ctx = new POSDbContext())
            {
                BindDropdown(CityDropDownLst,
                    BuildDropdownItems(
                        ctx.Cities
                           .Where(c => c.CountryId == countryId)
                           .Select(c => new { Id = c.Id, Name = c.Name })
                           .ToList(),
                        "Select City"));

                CityDropDownLst.SelectedIndex = 0;
            }
        }

        private void CountryDropDownLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountryDropDownLst.SelectedValue == null ||
                Convert.ToInt32(CountryDropDownLst.SelectedValue) == 0)
            {
                CityDropDownLst.DataSource = null;
                CityDropDownLst.Items.Clear();
                return;
            }

            LoadCitiesForDropdown(Convert.ToInt32(CountryDropDownLst.SelectedValue));
        }

        private static List<object> BuildDropdownItems<T>(List<T> source, string defaultLabel) where T : class
        {
            var list = new List<object> { new { Id = 0, Name = defaultLabel } };
            list.AddRange(source.Cast<object>());
            return list;
        }

        private static void BindDropdown(ComboBox combo, List<object> items)
        {
            combo.DataSource = items;
            combo.DisplayMember = "Name";
            combo.ValueMember = "Id";
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  CORE DATA LOAD
        // ═══════════════════════════════════════════════════════════════════════════
        private async Task LoadCustomersAsync()
        {
            if (_isLoading) return;

            _loadCts.Cancel();
            _loadCts.Dispose();
            _loadCts = new CancellationTokenSource();
            var token = _loadCts.Token;

            _isLoading = true;
            SetNavigationEnabled(false);

            try
            {
                DataTable dt = null;

                await Task.Run(async () =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        ICustomerRepository repo = new CustomerRepository(ctx);
                        var result = await repo.GetCustomerPagingListAsync(
                            _pageIndex, PAGE_SIZE, _searchTerm);

                        token.ThrowIfCancellationRequested();

                        _recordCount = result.totalCount;
                        dt = BuildGridDataTable(result.data);
                    }
                }, token);

                CustomerListDataGrid.DataSource = dt;
                UpdatePager();
                UpdateSelectionStatus();
            }
            catch (OperationCanceledException)
            {
                // Superseded by newer request — ignore
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load customers:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
                SetNavigationEnabled(true);
            }
        }

        /// <summary>
        /// Builds DataTable on background thread.
        /// Replace 'dynamic' with your actual DTO type from GetCustomerPagingListAsync.
        /// </summary>
        private DataTable BuildGridDataTable(IEnumerable<dynamic> data)
        {
            var dt = new DataTable();
            dt.Columns.Add(Col.IsSelected, typeof(bool));
            dt.Columns.Add(Col.ID, typeof(int));
            dt.Columns.Add(Col.Name, typeof(string));
            dt.Columns.Add(Col.Address, typeof(string));
            dt.Columns.Add(Col.Phone, typeof(string));
            dt.Columns.Add(Col.CityId, typeof(int));
            dt.Columns.Add(Col.CityName, typeof(string));
            dt.Columns.Add(Col.Active, typeof(bool));

            foreach (var item in data)
            {
                dt.Rows.Add(
                    _selectedCustomerIds.Contains((int)item.Id),
                    item.Id,
                    item.CustomerName,
                    item.CustomerAddress,
                    item.ContactNo,
                    item.CityId,
                    item.CityName,
                    !item.IsDeleted
                );
            }

            return dt;
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  GRID CONFIGURATION (called ONCE)
        // ═══════════════════════════════════════════════════════════════════════════
        private void ConfigureDataGridView()
        {
            if (_gridConfigured) return;
            _gridConfigured = true;

            var grid = CustomerListDataGrid;
            grid.Columns.Clear();
            grid.AutoGenerateColumns = false;
            grid.AllowUserToAddRows = false;
            grid.ReadOnly = false;

            // Selection checkbox
            grid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = Col.IsSelected,
                DataPropertyName = Col.IsSelected,
                HeaderText = "",
                Width = 40,
                ReadOnly = false,
                FlatStyle = FlatStyle.Standard,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            // Data columns
            AddTextColumn(grid, Col.ID, Col.ID, "ID", 60, visible: false);
            AddTextColumn(grid, Col.Name, Col.Name, "Name", 200);
            AddTextColumn(grid, Col.Address, Col.Address, "Address", 200);
            AddTextColumn(grid, Col.Phone, Col.Phone, "Phone", 120);
            AddTextColumn(grid, Col.CityId, Col.CityId, "CityId", 60, visible: false);
            AddTextColumn(grid, Col.CityName, Col.CityName, "City", 120);

            // Active — read-only checkbox display
            grid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = Col.Active,
                DataPropertyName = Col.Active,
                HeaderText = "Active",
                Width = 60,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            // Layout
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AllowUserToResizeColumns = true;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
            grid.BackgroundColor = SystemColors.Window;
            grid.BorderStyle = BorderStyle.None;

            // Styles
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            grid.EnableHeadersVisualStyles = false;

            // Events — subscribed ONCE
            grid.CellClick += CustomerListDataGrid_CellClick;
            grid.CurrentCellDirtyStateChanged += CustomerListDataGrid_CurrentCellDirtyStateChanged;
            grid.CellValueChanged += CustomerListDataGrid_CellValueChanged;
        }

        private static void AddTextColumn(DataGridView grid, string name, string dataProperty,
                                          string header, int width, bool visible = true)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                DataPropertyName = dataProperty,
                HeaderText = header,
                Width = width,
                ReadOnly = true,
                Visible = visible
            });
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  GRID EVENTS
        // ═══════════════════════════════════════════════════════════════════════════
        private void CustomerListDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (CustomerListDataGrid.CurrentCell is DataGridViewCheckBoxCell)
                CustomerListDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void CustomerListDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.ColumnIndex != CustomerListDataGrid.Columns[Col.IsSelected].Index) return;

            var row = CustomerListDataGrid.Rows[e.RowIndex];
            var chk = row.Cells[Col.IsSelected] as DataGridViewCheckBoxCell;
            var idCell = row.Cells[Col.ID];

            if (chk == null || idCell?.Value == null) return;

            int id = Convert.ToInt32(idCell.Value);
            bool isChecked = Convert.ToBoolean(chk.Value);

            if (isChecked) _selectedCustomerIds.Add(id);
            else _selectedCustomerIds.Remove(id);

            UpdateSelectionStatus();
        }

        private void CustomerListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _isLoading) return;

            var grid = (DataGridView)sender;

            // Let CellValueChanged handle the checkbox — skip here
            if (e.ColumnIndex == grid.Columns[Col.IsSelected].Index) return;

            _ = BindCustomerToFormAsync(grid.Rows[e.RowIndex]);
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  BIND RECORD TO FORM
        // ═══════════════════════════════════════════════════════════════════════════
        private async Task BindCustomerToFormAsync(DataGridViewRow row)
        {
            // Cheap — read directly from grid, no DB needed
            CustomerIdTxt.Text = row.Cells[Col.ID].Value.ToString();
            CustomerNameTxt.Text = row.Cells[Col.Name].Value.ToString();
            CustomerAddressTxt.Text = row.Cells[Col.Address].Value.ToString();
            CustomerPhoneTxt.Text = row.Cells[Col.Phone].Value.ToString();
            CustomerActiveChkBox.Checked = Convert.ToBoolean(row.Cells[Col.Active].Value);
            RemoveCustomerBtn.Visible = true;
            UpdateCustomerBtn.Visible = true;
            UpdateCustomerBtn.Enabled = true;
            RemoveCustomerBtn.Enabled = true;
            if (row.Cells[Col.CityId].Value == null) return;

            int cityId = Convert.ToInt32(row.Cells[Col.CityId].Value);
            int countryId = 0;

            try
            {
                // Only the CountryId lookup needs a DB call — fetch on background thread
                await Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        var city = ctx.Cities.Find(cityId);
                        if (city != null) countryId = city.CountryId;
                    }
                });

                if (countryId > 0)
                {
                    CountryDropDownLst.SelectedValue = countryId;
                    CityDropDownLst.SelectedValue = cityId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load city/country:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  SAVE  — normalize FIRST, then duplicate-check, then save
        // ═══════════════════════════════════════════════════════════════════════════
        private async void SaveCustomerBtn_Click(object sender, EventArgs e)
        {
            var model = BuildCustomerModelFromForm();

            // FIX: Normalize before validation and duplicate check so casing is consistent
            model.CustomerName = TextFormatHelper.ConvertStringToTileCaseOrNull(model.CustomerName);
            model.CustomerAddress = TextFormatHelper.ConvertStringToTileCaseOrNull(model.CustomerAddress);

            if (!model.IsValid(out var results)) { ShowValidationErrors(results); return; }

            SaveCustomerBtn.Enabled = false;
            try
            {
                using (var ctx = new POSDbContext())
                {
                    ICustomerRepository repo = new CustomerRepository(ctx);

                    if (await repo.CheckRecoradAlreadyExistByName(model.CustomerName, model.CustomerAddress))
                    {
                        MessageBox.Show($"Customer '{model.CustomerName}' already exists.",
                            "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    await Task.Run(() => { ctx.Customers.Add(model); ctx.SaveChanges(); });
                }

                MessageBox.Show("Customer saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                _pageIndex = 1;
                await LoadCustomersAsync();
            }
            finally
            {
                SaveCustomerBtn.Enabled = true;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  UPDATE  — normalize FIRST, then duplicate-check, then save
        // ═══════════════════════════════════════════════════════════════════════════
        private async void UpdateCustomerBtn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(CustomerIdTxt.Text, out int customerId) || customerId <= 0)
            {
                MessageBox.Show("Please select a record first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var model = BuildCustomerModelFromForm();
            model.Id = customerId;
            model.IsDeleted = !CustomerActiveChkBox.Checked;

            // FIX: Normalize before duplicate check — consistent with Save
            model.CustomerName = TextFormatHelper.ConvertStringToTileCaseOrNull(model.CustomerName);
            model.CustomerAddress = TextFormatHelper.ConvertStringToTileCaseOrNull(model.CustomerAddress);

            if (!model.IsValid(out var results)) { ShowValidationErrors(results); return; }

            UpdateCustomerBtn.Enabled = false;
            try
            {
                using (var ctx = new POSDbContext())
                {
                    ICustomerRepository repo = new CustomerRepository(ctx);
                    var existing = repo.GetById(customerId);

                    if (existing == null)
                    {
                        MessageBox.Show("Customer not found.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Duplicate check — exclude current record
                    bool isDuplicate = ctx.Customers.Any(c =>
                        c.CustomerName.Equals(model.CustomerName, StringComparison.OrdinalIgnoreCase) &&
                        c.CustomerAddress.Equals(model.CustomerAddress, StringComparison.OrdinalIgnoreCase) &&
                        c.Id != customerId);

                    if (isDuplicate)
                    {
                        MessageBox.Show($"Customer '{model.CustomerName}' already exists.",
                            "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    existing.CustomerName = model.CustomerName;
                    existing.CustomerAddress = model.CustomerAddress;
                    existing.ContactNo = model.ContactNo;
                    existing.CityId = model.CityId;
                    existing.IsDeleted = model.IsDeleted;

                    await Task.Run(() => { repo.Update(existing); repo.Save(); });
                }

                MessageBox.Show("Customer updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                _pageIndex = 1;
                await LoadCustomersAsync();
            }
            finally
            {
                UpdateCustomerBtn.Enabled = true;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  DELETE
        // ═══════════════════════════════════════════════════════════════════════════
        private async void RemoveCustomerBtn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(CustomerIdTxt.Text, out int customerId) || customerId <= 0)
            {
                MessageBox.Show("Please select a record first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this customer?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var ctx = new POSDbContext())
                {
                    var repo = new CustomerRepository(ctx);
                    var data = repo.GetById(customerId);

                    if (data == null)
                    {
                        MessageBox.Show("Customer not found.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    await Task.Run(() => { repo.Delete(customerId); repo.Save(); });
                }

                MessageBox.Show("Customer deleted successfully.", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _selectedCustomerIds.Remove(customerId);
                ClearForm();
                _pageIndex = 1;
                await LoadCustomersAsync();
            }
            catch (DbUpdateException dbEx) when (
                dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                MessageBox.Show(
                    "This customer is linked to other records and cannot be deleted.",
                    "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  PAGINATION
        // ═══════════════════════════════════════════════════════════════════════════
        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)_recordCount / PAGE_SIZE);
            lblStatus.Text = $"Page {_pageIndex} of {totalPages} | Total Records: {_recordCount}";
            PreviousPageBtn.Enabled = _pageIndex > 1;
            NextPageBtn.Enabled = _pageIndex < totalPages;
        }

        private void SetNavigationEnabled(bool enabled)
        {
            int totalPages = (int)Math.Ceiling((double)_recordCount / PAGE_SIZE);
            NextPageBtn.Enabled = enabled && _pageIndex < totalPages;
            PreviousPageBtn.Enabled = enabled && _pageIndex > 1;
        }

        private async void NextPageBtn_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)_recordCount / PAGE_SIZE);
            if (_pageIndex >= totalPages || _isLoading) return;
            _pageIndex++;
            await LoadCustomersAsync();
        }

        private async void PreviousPageBtn_Click(object sender, EventArgs e)
        {
            if (_pageIndex <= 1 || _isLoading) return;
            _pageIndex--;
            await LoadCustomersAsync();
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  SEARCH (debounced)
        // ═══════════════════════════════════════════════════════════════════════════
        private void CustomerSearchTxt_TextChanged(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private async void SearchDebounceTimer_Tick(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchTerm = CustomerSearchTxt.Text.Trim();
            _pageIndex = 1;
            await LoadCustomersAsync();
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  SELECTION HELPERS
        // ═══════════════════════════════════════════════════════════════════════════
        private void UpdateSelectionStatus()
        {
            ClearAllSelectionBtn.Visible = _selectedCustomerIds.Count > 0;
            selectedProdLbl.Text = $"Selected: {_selectedCustomerIds.Count} Customer(s)";
        }

        private void ToggleAllCheckboxesOnPage(bool isSelected)
        {
            foreach (DataGridViewRow row in CustomerListDataGrid.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells[Col.IsSelected] is DataGridViewCheckBoxCell chk)
                    chk.Value = isSelected;

                int id = Convert.ToInt32(row.Cells[Col.ID].Value);
                if (isSelected) _selectedCustomerIds.Add(id);
                else _selectedCustomerIds.Remove(id);
            }
        }

        public List<int> GetSelectedCustomerIds() => _selectedCustomerIds.ToList();

        public void ClearSelection()
        {
            _selectedCustomerIds.Clear();
            UpdateSelectionStatus();
        }

        private void SelectAllBtn_Click(object sender, EventArgs e)
        {
            ToggleAllCheckboxesOnPage(true);
            UpdateSelectionStatus();
        }

        // FIX: No longer reloads from DB — just unchecks visible rows directly
        private void ClearAllSelectionBtn_Click(object sender, EventArgs e)
        {
            _selectedCustomerIds.Clear();
            ToggleAllCheckboxesOnPage(false);
            UpdateSelectionStatus();
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  EXPORT
        // ═══════════════════════════════════════════════════════════════════════════
        private async void ExportAllBtn_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerIds.Count == 0)
            {
                MessageBox.Show("No customers selected for export.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ask where to save BEFORE doing any work
            string savePath = GetSaveFilePath("CustomerList.xlsx");
            if (savePath == null) return;

            try
            {
                List<Customer> customers;

                using (var ctx = new POSDbContext())
                {
                    // Materialize once — single DB call, .Count is O(1) on List
                    customers = (await new CustomerRepository(ctx)
                        .GetAll(_selectedCustomerIds.ToList())).ToList();
                }

                if (customers.Count == 0)
                {
                    MessageBox.Show("No customers found for the selected IDs.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Write Excel on background thread — large exports won't freeze UI
                await Task.Run(() => ExportToExcel(customers, savePath));

                MessageBox.Show($"{customers.Count} customer(s) exported to:\n{savePath}",
                    "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearSelection();
                _pageIndex = 1;
                await LoadCustomersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetSaveFilePath(string defaultFileName)
        {
            using (var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = defaultFileName
            })
            {
                return sfd.ShowDialog() == DialogResult.OK ? sfd.FileName : null;
            }
        }

        private static void ExportToExcel(IEnumerable<Customer> customers, string path)
        {
            var dt = new DataTable { TableName = "Customer" };
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("CityId", typeof(int));
            dt.Columns.Add("Active", typeof(bool));

            foreach (var c in customers)
                dt.Rows.Add(c.Id, c.CustomerName, c.CustomerAddress,
                            c.ContactNo, c.CityId, !c.IsDeleted);

            using (var wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Customers");
                wb.SaveAs(path);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  FORM HELPERS
        // ═══════════════════════════════════════════════════════════════════════════
        private Customer BuildCustomerModelFromForm()
        {
            return new Customer
            {
                CustomerName = CustomerNameTxt.Text.Trim(),
                ContactNo = CustomerPhoneTxt.Text.Trim(),
                CustomerAddress = CustomerAddressTxt.Text.Trim(),
                CityId = Convert.ToInt32(CityDropDownLst.SelectedValue),
                IsDeleted = false
            };
        }

        private static void ShowValidationErrors(
            IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> results)
        {
            MessageBox.Show(string.Join("\n", results.Select(r => r.ErrorMessage)),
                "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearForm()
        {
            CustomerIdTxt.Clear();
            CustomerNameTxt.Clear();
            CustomerPhoneTxt.Clear();
            CustomerAddressTxt.Clear();
            CountryDropDownLst.SelectedIndex = 0;
            CityDropDownLst.DataSource = null;
            CityDropDownLst.Items.Clear();
            CustomerActiveChkBox.Checked = false;
            RemoveCustomerBtn.Visible = false;
            UpdateCustomerBtn.Visible = false;
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  MISC UI EVENTS
        // ═══════════════════════════════════════════════════════════════════════════

        // FIX: No DB call — form reset doesn't change the data, so no reload needed
        private void ResetFormBtn_Click(object sender, EventArgs e) => ClearForm();
    }
}