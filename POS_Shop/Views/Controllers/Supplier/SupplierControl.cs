

using Org.BouncyCastle.Asn1.Cmp;
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

namespace POS_Shop.Views.Controllers.Supplier
{
    public partial class SupplierControl : UserControl
    {
        // ═══════════════════════════════════════════════════════════════════════════
        //  COLUMN NAME CONSTANTS  — compile-time safety, no magic strings
        // ═══════════════════════════════════════════════════════════════════════════
        private static class Col
        {
            public const string IsSelected = "IsSelected";
            public const string ID = "ID";
            public const string Name = "Name";
            public const string Shop = "Shop";
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
        //  SEARCH DEBOUNCE  — one DB call after user stops typing, not every keystroke
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
        private readonly HashSet<int> _selectedSupplierIds = new HashSet<int>();

        // ═══════════════════════════════════════════════════════════════════════════
        //  ONE-TIME GRID CONFIG FLAG
        // ═══════════════════════════════════════════════════════════════════════════
        private bool _gridConfigured = false;

        // ═══════════════════════════════════════════════════════════════════════════
        //  CONSTRUCTOR
        // ═══════════════════════════════════════════════════════════════════════════
        public SupplierControl()
        {
            InitializeComponent();

            _searchDebounceTimer = new System.Windows.Forms.Timer();
            _searchDebounceTimer.Interval = SEARCH_DEBOUNCE_MS;
            _searchDebounceTimer.Tick += SearchDebounceTimer_Tick;

            this.Load += SupplierControl_Load;
            this.Disposed += SupplierControl_Disposed;

            SupplierNameTxt.Focus();
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  LOAD & DISPOSE
        // ═══════════════════════════════════════════════════════════════════════════
        private async void SupplierControl_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();   // once only
            LoadCountriesForDropdown();
            await LoadSuppliersAsync();
        }

        private void SupplierControl_Disposed(object sender, EventArgs e)
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
        //  CORE DATA LOAD  (cancellation + loading guard)
        // ═══════════════════════════════════════════════════════════════════════════
        private async Task LoadSuppliersAsync()
        {
            if (_isLoading) return;

            // Cancel any previous in-flight request
            _loadCts.Cancel();
            _loadCts.Dispose();
            _loadCts = new CancellationTokenSource();
            var token = _loadCts.Token;

            _isLoading = true;
            SetNavigationEnabled(false);

            try
            {
                DataTable dt = null;

                // DB fetch on background thread — keeps UI responsive
                await Task.Run(async () =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        ISupplierRepository repo = new SupplierRepository(ctx);
                        var result = await repo.GetSupplierPagingListAsync(
                            _pageIndex, PAGE_SIZE, _searchTerm);

                        token.ThrowIfCancellationRequested();

                        _recordCount = result.totalCount;
                        dt = BuildGridDataTable(result.data);
                    }
                }, token);

                // Back on UI thread — bind
                SupplierListDataGrid.DataSource = dt;
                UpdatePager();
                UpdateSelectionStatus();
            }
            catch (OperationCanceledException)
            {
                // Superseded by a newer request — silently ignore
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load suppliers:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
                SetNavigationEnabled(true);
            }
        }

        /// <summary>
        /// Builds DataTable on background thread — no UI access here.
        /// Replace 'dynamic' with your actual DTO type from GetSupplierPagingListAsync.
        /// </summary>
        private DataTable BuildGridDataTable(IEnumerable<dynamic> data)
        {
            var dt = new DataTable();
            dt.Columns.Add(Col.IsSelected, typeof(bool));
            dt.Columns.Add(Col.ID, typeof(int));
            dt.Columns.Add(Col.Name, typeof(string));
            dt.Columns.Add(Col.Shop, typeof(string));
            dt.Columns.Add(Col.Address, typeof(string));
            dt.Columns.Add(Col.Phone, typeof(string));
            dt.Columns.Add(Col.CityId, typeof(int));
            dt.Columns.Add(Col.CityName, typeof(string));
            dt.Columns.Add(Col.Active, typeof(bool));

            foreach (var item in data)
            {
                dt.Rows.Add(
                    _selectedSupplierIds.Contains((int)item.Id),
                    item.Id,
                    item.SupplierName,
                    item.ShopName,
                    item.Address,
                    item.ContactNo,
                    item.CityId,
                    item.CityName,
                    !item.IsDeleted
                );
            }

            return dt;
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  GRID CONFIGURATION  (called ONCE)
        // ═══════════════════════════════════════════════════════════════════════════
        private void ConfigureDataGridView()
        {
            if (_gridConfigured) return;
            _gridConfigured = true;

            var grid = SupplierListDataGrid;
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
            AddTextColumn(grid, Col.Name, Col.Name, "Supplier Name", 180);
            AddTextColumn(grid, Col.Shop, Col.Shop, "Shop Name", 180);
            AddTextColumn(grid, Col.Address, Col.Address, "Address", 180);
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
            grid.CellClick += SupplierListDataGrid_CellClick;
            grid.CurrentCellDirtyStateChanged += SupplierListDataGrid_CurrentCellDirtyStateChanged;
            grid.CellValueChanged += SupplierListDataGrid_CellValueChanged;
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
        private void SupplierListDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (SupplierListDataGrid.CurrentCell is DataGridViewCheckBoxCell)
                SupplierListDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void SupplierListDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Only handle the IsSelected checkbox column
            if (e.RowIndex < 0 ||
                e.ColumnIndex != SupplierListDataGrid.Columns[Col.IsSelected].Index) return;

            var row = SupplierListDataGrid.Rows[e.RowIndex];
            var chk = row.Cells[Col.IsSelected] as DataGridViewCheckBoxCell;
            var idCell = row.Cells[Col.ID];

            if (chk == null || idCell?.Value == null) return;

            int id = Convert.ToInt32(idCell.Value);
            bool isChecked = Convert.ToBoolean(chk.Value);

            if (isChecked) _selectedSupplierIds.Add(id);
            else _selectedSupplierIds.Remove(id);

            UpdateSelectionStatus();
        }

        private void SupplierListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _isLoading) return;

            var grid = (DataGridView)sender;

            // Let CellValueChanged handle the checkbox — skip here
            if (e.ColumnIndex == grid.Columns[Col.IsSelected].Index) return;

            // Any other column — load record into form
            _ = BindSupplierToFormAsync(grid.Rows[e.RowIndex]);
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  BIND RECORD TO FORM  (async — fetches CountryId on background thread)
        // ═══════════════════════════════════════════════════════════════════════════
        private async Task BindSupplierToFormAsync(DataGridViewRow row)
        {
            // Cheap — read directly from grid row, no DB needed
            SupplierIdTxt.Text = row.Cells[Col.ID].Value.ToString();
            SupplierNameTxt.Text = row.Cells[Col.Name].Value.ToString();
            SupplierShopNameTxt.Text = row.Cells[Col.Shop].Value.ToString();
            SupplierAddressTxt.Text = row.Cells[Col.Address].Value.ToString();
            SupplierPhoneTxt.Text = row.Cells[Col.Phone].Value.ToString();
            CustomerActiveChkBox.Checked = Convert.ToBoolean(row.Cells[Col.Active].Value);
            RemoveSupplierBtn.Visible = true;
            UpdateSupplierBtn.Visible = true;

            if (row.Cells[Col.CityId].Value == null) return;

            int cityId = Convert.ToInt32(row.Cells[Col.CityId].Value);
            int countryId = 0;

            try
            {
                // Only CountryId lookup requires a DB call — run on background thread
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
        //  SAVE
        // ═══════════════════════════════════════════════════════════════════════════
        private async void SaveSupplierBtn_Click(object sender, EventArgs e)
        {
            var model = BuildSupplierModelFromForm();
            if (!model.IsValid(out var results)) { ShowValidationErrors(results); return; }

            SaveSupplierBtn.Enabled = false;
            try
            {
                using (var ctx = new POSDbContext())
                {
                    ISupplierRepository repo = new SupplierRepository(ctx);

                    if (await repo.CheckRecoradAlreadyExistByName(model.SupplierName, model.Address))
                    {
                        MessageBox.Show($"Supplier '{model.SupplierName}' already exists.",
                            "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    await Task.Run(() => { ctx.Suppliers.Add(model); ctx.SaveChanges(); });
                }

                MessageBox.Show("Supplier saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                _pageIndex = 1;
                await LoadSuppliersAsync();
            }
            finally
            {
                SaveSupplierBtn.Enabled = true;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  UPDATE
        //  FIX 1: ShopName was silently not being saved (missing field in original)
        //  FIX 2: "Customer updated" message corrected to "Supplier updated"
        //  FIX 3: Removed unreliable ValidateChildren() — model.IsValid() handles it
        //  FIX 4: Button disabled during operation to prevent double-submit
        // ═══════════════════════════════════════════════════════════════════════════
        private async void UpdateSupplierBtn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(SupplierIdTxt.Text, out int supplierId) || supplierId <= 0)
            {
                MessageBox.Show("Please select a record first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var model = BuildSupplierModelFromForm();
            model.Id = supplierId;
            model.IsDeleted = !CustomerActiveChkBox.Checked;

            if (!model.IsValid(out var results)) { ShowValidationErrors(results); return; }

            UpdateSupplierBtn.Enabled = false;
            try
            {
                using (var ctx = new POSDbContext())
                {
                    ISupplierRepository repo = new SupplierRepository(ctx);
                    var existing = repo.GetById(supplierId);

                    if (existing == null)
                    {
                        MessageBox.Show("Supplier not found.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Duplicate check — exclude current record
                    bool isDuplicate = ctx.Suppliers.Any(s =>
                        s.SupplierName.Equals(model.SupplierName, StringComparison.OrdinalIgnoreCase) &&
                        s.Address.Equals(model.Address, StringComparison.OrdinalIgnoreCase) &&
                        s.Id != supplierId);

                    if (isDuplicate)
                    {
                        MessageBox.Show($"Supplier '{model.SupplierName}' already exists.",
                            "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // FIX: ShopName was missing in the original update — now included
                    existing.SupplierName = model.SupplierName;
                    existing.ShopName = model.ShopName;
                    existing.ContactNo = model.ContactNo;
                    existing.Address = model.Address;
                    existing.CityId = model.CityId;
                    existing.IsDeleted = model.IsDeleted;

                    await Task.Run(() => { repo.Update(existing); repo.Save(); });
                }

                // FIX: Was showing "Customer updated successfully!" — now correct
                MessageBox.Show("Supplier updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                _pageIndex = 1;
                await LoadSuppliersAsync();
            }
            finally
            {
                UpdateSupplierBtn.Enabled = true;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  DELETE
        //  FIX 1: Added int.TryParse guard — original crashed if SupplierIdTxt empty
        //  FIX 2: Error messages said "CUSTOMER" — corrected to "Supplier"
        //  FIX 3: FK violation catch moved outside the using block (correct scope)
        // ═══════════════════════════════════════════════════════════════════════════
        private async void RemoveSupplierBtn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(SupplierIdTxt.Text, out int supplierId) || supplierId <= 0)
            {
                MessageBox.Show("Please select a record first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this supplier?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var ctx = new POSDbContext())
                {
                    var repo = new SupplierRepository(ctx);
                    var data = repo.GetById(supplierId);

                    if (data == null)
                    {
                        MessageBox.Show("Supplier not found.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    await Task.Run(() => { repo.Delete(supplierId); repo.Save(); });
                }

                MessageBox.Show("Supplier deleted successfully.", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _selectedSupplierIds.Remove(supplierId);
                ClearForm();
                _pageIndex = 1;
                await LoadSuppliersAsync();
            }
            catch (DbUpdateException dbEx) when (
                dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                // FIX: Original message incorrectly said "CUSTOMER" — corrected
                MessageBox.Show(
                    "This supplier is linked to other records and cannot be deleted.",
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
            await LoadSuppliersAsync();
        }

        private async void PreviousPageBtn_Click(object sender, EventArgs e)
        {
            if (_pageIndex <= 1 || _isLoading) return;
            _pageIndex--;
            await LoadSuppliersAsync();
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  SEARCH  (debounced — one DB call after user stops typing)
        // ═══════════════════════════════════════════════════════════════════════════
        private void SupplierSearchTxt_TextChanged(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private async void SearchDebounceTimer_Tick(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();   // one-shot
            _searchTerm = SupplierSearchTxt.Text.Trim();
            _pageIndex = 1;
            await LoadSuppliersAsync();
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  SELECTION HELPERS
        // ═══════════════════════════════════════════════════════════════════════════
        private void UpdateSelectionStatus()
        {
            ClearAllSelectionBtn.Visible = _selectedSupplierIds.Count > 0;
            selectedProdLbl.Text = $"Selected: {_selectedSupplierIds.Count} Supplier(s)";
        }

        private void ToggleAllCheckboxesOnPage(bool isSelected)
        {
            foreach (DataGridViewRow row in SupplierListDataGrid.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells[Col.IsSelected] is DataGridViewCheckBoxCell chk)
                    chk.Value = isSelected;

                int id = Convert.ToInt32(row.Cells[Col.ID].Value);
                if (isSelected) _selectedSupplierIds.Add(id);
                else _selectedSupplierIds.Remove(id);
            }
        }

        public List<int> GetSelectedSupplierIds() => _selectedSupplierIds.ToList();

        public void ClearSelection()
        {
            _selectedSupplierIds.Clear();
            UpdateSelectionStatus();
        }

        private void SelectAllBtn_Click(object sender, EventArgs e)
        {
            // FIX: Original reloaded entire grid just to check boxes — now updates UI directly
            ToggleAllCheckboxesOnPage(true);
            UpdateSelectionStatus();
        }

        private void ClearAllSelectionBtn_Click(object sender, EventArgs e)
        {
            // FIX: Original was async void with DB reload — now just unchecks visible rows
            _selectedSupplierIds.Clear();
            ToggleAllCheckboxesOnPage(false);
            UpdateSelectionStatus();
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  FORM HELPERS
        // ═══════════════════════════════════════════════════════════════════════════
        private Models.Supplier BuildSupplierModelFromForm()
        {
            return new Models.Supplier
            {
                SupplierName = SupplierNameTxt.Text.Trim(),
                ShopName = SupplierShopNameTxt.Text.Trim(),
                ContactNo = SupplierPhoneTxt.Text.Trim(),
                Address = SupplierAddressTxt.Text.Trim(),
                CityId = Convert.ToInt32(CityDropDownLst.SelectedValue),
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
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
            SupplierIdTxt.Clear();
            SupplierNameTxt.Clear();
            SupplierPhoneTxt.Clear();
            SupplierAddressTxt.Clear();
            SupplierShopNameTxt.Clear();
            CountryDropDownLst.SelectedIndex = 0;
            CityDropDownLst.DataSource = null;
            CityDropDownLst.Items.Clear();
            CustomerActiveChkBox.Checked = false;
            RemoveSupplierBtn.Visible = false;
            UpdateSupplierBtn.Visible = false;
        }

        // ═══════════════════════════════════════════════════════════════════════════
        //  MISC UI EVENTS
        // ═══════════════════════════════════════════════════════════════════════════

        // FIX: No DB reload — form reset doesn't change data
        private void ResetFormBtn_Click(object sender, EventArgs e) => ClearForm();
    }
}
