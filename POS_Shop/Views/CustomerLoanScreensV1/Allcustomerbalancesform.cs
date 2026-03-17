using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using POS_Shop.Models;
using POS_Shop.Models.LoanModelsV1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.CustomerLoanScreensV1
{

    /// <summary>
    /// Dashboard: all customers with outstanding loan or advance balances.
    ///
    /// Two separate queries:
    ///   1. GetDashboardKpiAsync()         — fast SQL aggregate, always shows
    ///                                       accurate business totals in KPI strip.
    ///   2. GetCustomerBalancePageAsync()  — cursor-based, 100 rows per page,
    ///                                       never loads the full table at once.
    /// </summary>
    //public partial class AllCustomerBalancesForm : Form
    //{
    //    // ─── Pagination state ─────────────────────────────────────────────────
    //    private const int PageSize = 100;

    //    private List<CustomerBalanceSummary> _currentPage = new List<CustomerBalanceSummary>();

    //    // Cursors: CustomerId of first/last row on current page.
    //    // Passed to next/prev query so DB skips already-seen rows
    //    // without expensive OFFSET counting.
    //    private int _firstIdOnPage = 0;
    //    private int _lastIdOnPage = 0;
    //    private bool _hasNextPage = false;
    //    private bool _hasPrevPage = false;

    //    // Current filter/search — preserved when turning pages
    //    private BalanceType? _activeFilter = null;  // null = All
    //    private string _activeSearch = "";

    //    public AllCustomerBalancesForm()
    //    {
    //        InitializeComponent();
    //    }

    //    // ─── Load ─────────────────────────────────────────────────────────────
    //    private async void AllCustomerBalancesForm_Load(object sender, EventArgs e)
    //    {
    //        SetupGrid();
    //        SetupFilterButtons();
    //        await LoadFirstPageAsync();
    //    }

    //    // ─── Grid setup ───────────────────────────────────────────────────────
    //    private void SetupGrid()
    //    {
    //        BalanceGrid.AutoGenerateColumns = false;
    //        BalanceGrid.AllowUserToAddRows = false;
    //        BalanceGrid.AllowUserToDeleteRows = false;
    //        BalanceGrid.ReadOnly = true;
    //        BalanceGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    //        BalanceGrid.RowHeadersVisible = false;
    //        BalanceGrid.BackgroundColor = Color.White;
    //        BalanceGrid.BorderStyle = BorderStyle.None;
    //        BalanceGrid.GridColor = Color.FromArgb(230, 230, 230);
    //        BalanceGrid.Font = new Font("Segoe UI", 9);
    //        BalanceGrid.RowTemplate.Height = 34;
    //        BalanceGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
    //        BalanceGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
    //        BalanceGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
    //        BalanceGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
    //        BalanceGrid.ColumnHeadersHeight = 38;
    //        BalanceGrid.EnableHeadersVisualStyles = false;
    //        BalanceGrid.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
    //        BalanceGrid.CellFormatting += BalanceGrid_CellFormatting;
    //        BalanceGrid.CellDoubleClick += BalanceGrid_CellDoubleClick;

    //        AddCol("CustomerName", "Customer Name", 200);
    //        AddCol("ContactNo", "Contact", 120);
    //        AddCol("BalanceDisplay", "Balance (PKR)", 140, DataGridViewContentAlignment.MiddleRight);
    //        AddCol("BalanceType", "Status", 100, DataGridViewContentAlignment.MiddleCenter);
    //        AddCol("LastTransactionDate", "Last Transaction", 130, DataGridViewContentAlignment.MiddleCenter, "dd-MMM-yyyy");

    //        var btnCol = new DataGridViewButtonColumn
    //        {
    //            Name = "colAction",
    //            HeaderText = "Action",
    //            Text = "💰 Receive",
    //            UseColumnTextForButtonValue = true,
    //            Width = 110,
    //            FlatStyle = FlatStyle.Flat
    //        };
    //        BalanceGrid.Columns.Add(btnCol);
    //        BalanceGrid.CellClick += BalanceGrid_CellClick;
    //    }

    //    private void AddCol(string prop, string header, int width,
    //        DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleLeft,
    //        string format = null)
    //    {
    //        BalanceGrid.Columns.Add(new DataGridViewTextBoxColumn
    //        {
    //            DataPropertyName = prop,
    //            HeaderText = header,
    //            Width = width,
    //            DefaultCellStyle = new DataGridViewCellStyle { Alignment = align, Format = format ?? "" }
    //        });
    //    }

    //    // ─── Filter + search wiring ───────────────────────────────────────────
    //    private void SetupFilterButtons()
    //    {
    //        rbAll.CheckedChanged += async (s, e) => { if (rbAll.Checked) { _activeFilter = null; await LoadFirstPageAsync(); } };
    //        rbLoan.CheckedChanged += async (s, e) => { if (rbLoan.Checked) { _activeFilter = BalanceType.Loan; await LoadFirstPageAsync(); } };
    //        rbAdvance.CheckedChanged += async (s, e) => { if (rbAdvance.Checked) { _activeFilter = BalanceType.Advance; await LoadFirstPageAsync(); } };
    //        rbClear.CheckedChanged += async (s, e) => { if (rbClear.Checked) { _activeFilter = BalanceType.Clear; await LoadFirstPageAsync(); } };

    //        // Debounce — only query after user stops typing for 350ms
    //        var searchTimer = new System.Windows.Forms.Timer { Interval = 350 };
    //        searchTimer.Tick += async (s, e) =>
    //        {
    //            searchTimer.Stop();
    //            _activeSearch = txtSearch.Text.Trim();
    //            await LoadFirstPageAsync();
    //        };
    //        txtSearch.TextChanged += (s, e) => { searchTimer.Stop(); searchTimer.Start(); };
    //    }

    //    // ─── Data loading ─────────────────────────────────────────────────────

    //    /// <summary>
    //    /// Jump to page 1 and refresh KPIs.
    //    /// Called on: first load, filter/search change, after any save.
    //    /// </summary>
    //    private async Task LoadFirstPageAsync()
    //    {
    //        SetLoading(true);
    //        try
    //        {
    //            using (var context = new POSDbContext())
    //            {
    //                var repo = new CustomerLedgerRepository(context);

    //                // KPI: single SQL aggregate — always full-business accurate
    //                var kpi = await repo.GetDashboardKpiAsync();
    //                UpdateKpis(kpi);

    //                // Grid page 1: cursor=0 means start from beginning
    //                var page = await repo.GetCustomerBalancePageAsync(
    //                    lastCustomerId: 0,
    //                    goingForward: true,
    //                    filter: _activeFilter,
    //                    search: _activeSearch,
    //                    pageSize: PageSize);

    //                BindPage(page);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Error loading dashboard:\n{ex.Message}", "Error",
    //                MessageBoxButtons.OK, MessageBoxIcon.Error);
    //        }
    //        finally { SetLoading(false); }
    //    }

    //    /// <summary>Next page — pass last CustomerId on current page as cursor.</summary>
    //    private async Task LoadNextPageAsync()
    //    {
    //        if (!_hasNextPage || _lastIdOnPage == 0) return;
    //        SetLoading(true);
    //        try
    //        {
    //            using (var context = new POSDbContext())
    //            {
    //                var page = await new CustomerLedgerRepository(context)
    //                    .GetCustomerBalancePageAsync(
    //                        lastCustomerId: _lastIdOnPage,
    //                        goingForward: true,
    //                        filter: _activeFilter,
    //                        search: _activeSearch,
    //                        pageSize: PageSize);
    //                BindPage(page);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //        }
    //        finally { SetLoading(false); }
    //    }

    //    /// <summary>Previous page — pass first CustomerId on current page as cursor.</summary>
    //    private async Task LoadPrevPageAsync()
    //    {
    //        if (!_hasPrevPage || _firstIdOnPage == 0) return;
    //        SetLoading(true);
    //        try
    //        {
    //            using (var context = new POSDbContext())
    //            {
    //                var page = await new CustomerLedgerRepository(context)
    //                    .GetCustomerBalancePageAsync(
    //                        lastCustomerId: _firstIdOnPage,
    //                        goingForward: false,
    //                        filter: _activeFilter,
    //                        search: _activeSearch,
    //                        pageSize: PageSize);
    //                BindPage(page);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show($"Error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //        }
    //        finally { SetLoading(false); }
    //    }

    //    // ─── Bind helpers ─────────────────────────────────────────────────────
    //    private void BindPage(CustomerBalancePage page)
    //    {
    //        _currentPage = page.Rows;
    //        _hasNextPage = page.HasNextPage;
    //        _hasPrevPage = page.HasPrevPage;
    //        _firstIdOnPage = _currentPage.Any() ? _currentPage.First().CustomerId : 0;
    //        _lastIdOnPage = _currentPage.Any() ? _currentPage.Last().CustomerId : 0;

    //        BalanceGrid.DataSource = null;
    //        BalanceGrid.DataSource = _currentPage;
    //        UpdatePaginationBar();
    //    }

    //    private void UpdateKpis(DashboardKpi kpi)
    //    {
    //        lblTotalLoanAmount.Text = $"PKR {kpi.TotalLoanAmount:N2}";
    //        lblLoanCount.Text = $"{kpi.LoanCustomerCount} customers";
    //        lblTotalAdvanceAmount.Text = $"PKR {kpi.TotalAdvanceAmount:N2}";
    //        lblAdvanceCount.Text = $"{kpi.AdvanceCustomerCount} customers";
    //    }

    //    private void UpdatePaginationBar()
    //    {
    //        PrevBtn.Enabled = _hasPrevPage;
    //        NextBtn.Enabled = _hasNextPage;

    //        int showing = _currentPage.Count;
    //        lblCount.Text = showing == 0
    //            ? "No customers found"
    //            : $"Showing {showing} customer{(showing == 1 ? "" : "s")}";

    //        lblPageInfo.Text =
    //            (_hasPrevPage ? "◀ Previous    " : "              ") +
    //            (_hasNextPage ? "    Next ▶" : "");
    //    }

    //    private void SetLoading(bool loading)
    //    {
    //        BalanceGrid.Visible = !loading;
    //        lblLoading.Visible = loading;
    //        PrevBtn.Enabled = !loading && _hasPrevPage;
    //        NextBtn.Enabled = !loading && _hasNextPage;
    //    }

    //    // ─── Grid events ──────────────────────────────────────────────────────
    //    private void BalanceGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    //    {
    //        if (e.RowIndex < 0 || e.RowIndex >= _currentPage.Count) return;
    //        var row = _currentPage[e.RowIndex];
    //        var colName = BalanceGrid.Columns[e.ColumnIndex].DataPropertyName;

    //        if (colName == "BalanceDisplay")
    //        {
    //            e.CellStyle.ForeColor = row.BalanceType == BalanceType.Loan
    //                ? Color.FromArgb(192, 0, 0)
    //                : Color.FromArgb(0, 102, 204);
    //            e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
    //        }

    //        if (colName == "BalanceType")
    //        {
    //            switch (row.BalanceType)
    //            {
    //                case BalanceType.Loan:
    //                    e.CellStyle.BackColor = Color.FromArgb(192, 0, 0);
    //                    e.CellStyle.ForeColor = Color.White;
    //                    e.Value = "🔴 LOAN"; break;
    //                case BalanceType.Advance:
    //                    e.CellStyle.BackColor = Color.FromArgb(0, 102, 204);
    //                    e.CellStyle.ForeColor = Color.White;
    //                    e.Value = "🔵 ADVANCE"; break;
    //                case BalanceType.Clear:
    //                    e.CellStyle.BackColor = Color.FromArgb(39, 174, 96);
    //                    e.CellStyle.ForeColor = Color.White;
    //                    e.Value = "✅ CLEAR"; break;
    //            }
    //        }
    //    }

    //    private void BalanceGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    //    {
    //        if (e.RowIndex < 0 || e.RowIndex >= _currentPage.Count) return;
    //        OpenLedger(_currentPage[e.RowIndex]);
    //    }

    //    private async void BalanceGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    //    {
    //        if (e.RowIndex < 0 || e.RowIndex >= _currentPage.Count) return;
    //        if (BalanceGrid.Columns[e.ColumnIndex].Name != "colAction") return;

    //        var cust = _currentPage[e.RowIndex];
    //        if (cust.BalanceType != BalanceType.Loan)
    //        {
    //            MessageBox.Show(
    //                "This customer has no outstanding loan.\n" +
    //                "Use the ledger to add advance or adjustment.",
    //                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //            return;
    //        }

    //        var frm = new Customerpaymentform(cust.CustomerId, cust.CustomerName, cust.Balance, false);
    //        if (frm.ShowDialog(this) == DialogResult.OK && frm.PaymentPosted)
    //            await LoadFirstPageAsync();
    //    }

    //    private void OpenLedger(CustomerBalanceSummary cust)
    //    {
    //        new Customerledgerform(cust.CustomerId, cust.CustomerName).ShowDialog(this);
    //        _ = LoadFirstPageAsync(); // Refresh KPIs + grid after any changes inside
    //    }

    //    // ─── Toolbar handlers ─────────────────────────────────────────────────
    //    private async void ManualEntryBtn_Click(object sender, EventArgs e)
    //    {
    //        var frm = new ManualLedgerEntryForm();
    //        if (frm.ShowDialog(this) == DialogResult.OK && frm.EntrySaved)
    //            await LoadFirstPageAsync();
    //    }

    //    private async void RefreshBtn_Click(object sender, EventArgs e) => await LoadFirstPageAsync();

    //    private void OpenLedgerBtn_Click(object sender, EventArgs e)
    //    {
    //        if (BalanceGrid.SelectedRows.Count == 0) return;
    //        int idx = BalanceGrid.SelectedRows[0].Index;
    //        if (idx >= 0 && idx < _currentPage.Count)
    //            OpenLedger(_currentPage[idx]);
    //    }

    //    // ─── Pagination handlers ──────────────────────────────────────────────
    //    private async void PrevBtn_Click(object sender, EventArgs e) => await LoadPrevPageAsync();
    //    private async void NextBtn_Click(object sender, EventArgs e) => await LoadNextPageAsync();
    //}


    public partial class AllCustomerBalancesForm : Form
    {
        // ─── Pagination state ─────────────────────────────────────────────────
        private const int PageSize = 100;

        private List<CustomerBalanceSummary> _currentPage = new List<CustomerBalanceSummary>();

        // Cursors: CustomerId of first/last row on current page.
        // Passed to next/prev query so DB skips already-seen rows
        // without expensive OFFSET counting.
        private int _firstIdOnPage = 0;
        private int _lastIdOnPage = 0;
        private bool _hasNextPage = false;
        private bool _hasPrevPage = false;

        // Current filter/search — preserved when turning pages
        private BalanceType? _activeFilter = null;  // null = All
        private string _activeSearch = "";

        public AllCustomerBalancesForm()
        {
            InitializeComponent();
        }

        // ─── Load ─────────────────────────────────────────────────────────────
        private async void AllCustomerBalancesForm_Load(object sender, EventArgs e)
        {
            SetupGrid();
            SetupFilterButtons();
            await LoadFirstPageAsync();
        }

        // ─── Grid setup ───────────────────────────────────────────────────────
        private void SetupGrid()
        {
            BalanceGrid.AutoGenerateColumns = false;
            BalanceGrid.AllowUserToAddRows = false;
            BalanceGrid.AllowUserToDeleteRows = false;
            BalanceGrid.ReadOnly = true;
            BalanceGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BalanceGrid.RowHeadersVisible = false;
            BalanceGrid.BackgroundColor = Color.White;
            BalanceGrid.BorderStyle = BorderStyle.None;
            BalanceGrid.GridColor = Color.FromArgb(230, 230, 230);
            BalanceGrid.Font = new Font("Segoe UI", 9);
            BalanceGrid.RowTemplate.Height = 34;
            BalanceGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            BalanceGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            BalanceGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            BalanceGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            BalanceGrid.ColumnHeadersHeight = 38;
            BalanceGrid.EnableHeadersVisualStyles = false;
            BalanceGrid.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            BalanceGrid.CellFormatting += BalanceGrid_CellFormatting;
            BalanceGrid.CellDoubleClick += BalanceGrid_CellDoubleClick;

            AddCol("CustomerName", "Customer Name", 200);
            AddCol("ContactNo", "Contact", 120);
            AddCol("BalanceDisplay", "Balance (PKR)", 140, DataGridViewContentAlignment.MiddleRight);
            AddCol("BalanceType", "Status", 100, DataGridViewContentAlignment.MiddleCenter);
            AddCol("LastTransactionDate", "Last Transaction", 130, DataGridViewContentAlignment.MiddleCenter, "dd-MMM-yyyy");

            var btnCol = new DataGridViewButtonColumn
            {
                Name = "colAction",
                HeaderText = "Action",
                Text = "💰 Receive",
                UseColumnTextForButtonValue = true,
                Width = 110,
                FlatStyle = FlatStyle.Flat
            };
            BalanceGrid.Columns.Add(btnCol);

            var btnDelete = new DataGridViewButtonColumn
            {
                Name = "colDelete",
                HeaderText = "",
                Text = "🗑 Delete History",
                UseColumnTextForButtonValue = true,
                Width = 130,
                FlatStyle = FlatStyle.Flat,
                Visible = false  // shown only when Clear filter is active
            };
            BalanceGrid.Columns.Add(btnDelete);

            BalanceGrid.CellClick += BalanceGrid_CellClick;
        }

        private void AddCol(string prop, string header, int width,
            DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleLeft,
            string format = null)
        {
            BalanceGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = prop,
                HeaderText = header,
                Width = width,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = align, Format = format ?? "" }
            });
        }

        // ─── Filter + search wiring ───────────────────────────────────────────
        private void SetupFilterButtons()
        {
            rbAll.CheckedChanged += async (s, e) => { if (rbAll.Checked) { _activeFilter = null; ToggleDeleteColumn(false); await LoadFirstPageAsync(); } };
            rbLoan.CheckedChanged += async (s, e) => { if (rbLoan.Checked) { _activeFilter = BalanceType.Loan; ToggleDeleteColumn(false); await LoadFirstPageAsync(); } };
            rbAdvance.CheckedChanged += async (s, e) => { if (rbAdvance.Checked) { _activeFilter = BalanceType.Advance; ToggleDeleteColumn(false); await LoadFirstPageAsync(); } };
            rbClear.CheckedChanged += async (s, e) => { if (rbClear.Checked) { _activeFilter = BalanceType.Clear; ToggleDeleteColumn(true); await LoadFirstPageAsync(); } };

            // Debounce — only query after user stops typing for 350ms
            var searchTimer = new System.Windows.Forms.Timer { Interval = 350 };
            searchTimer.Tick += async (s, e) =>
            {
                searchTimer.Stop();
                _activeSearch = txtSearch.Text.Trim();
                await LoadFirstPageAsync();
            };
            txtSearch.TextChanged += (s, e) => { searchTimer.Stop(); searchTimer.Start(); };
        }

        // ─── Data loading ─────────────────────────────────────────────────────

        /// <summary>
        /// Jump to page 1 and refresh KPIs.
        /// Called on: first load, filter/search change, after any save.
        /// </summary>
        private async Task LoadFirstPageAsync()
        {
            SetLoading(true);
            try
            {
                using (var context = new POSDbContext())
                {
                    var repo = new CustomerLedgerRepository(context);

                    // KPI: single SQL aggregate — always full-business accurate
                    var kpi = await repo.GetDashboardKpiAsync();
                    UpdateKpis(kpi);

                    // Grid page 1: cursor=0 means start from beginning
                    var page = await repo.GetCustomerBalancePageAsync(
                        lastCustomerId: 0,
                        goingForward: true,
                        filter: _activeFilter,
                        search: _activeSearch,
                        pageSize: PageSize);

                    BindPage(page);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { SetLoading(false); }
        }

        /// <summary>Next page — pass last CustomerId on current page as cursor.</summary>
        private async Task LoadNextPageAsync()
        {
            if (!_hasNextPage || _lastIdOnPage == 0) return;
            SetLoading(true);
            try
            {
                using (var context = new POSDbContext())
                {
                    var page = await new CustomerLedgerRepository(context)
                        .GetCustomerBalancePageAsync(
                            lastCustomerId: _lastIdOnPage,
                            goingForward: true,
                            filter: _activeFilter,
                            search: _activeSearch,
                            pageSize: PageSize);
                    BindPage(page);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { SetLoading(false); }
        }

        /// <summary>Previous page — pass first CustomerId on current page as cursor.</summary>
        private async Task LoadPrevPageAsync()
        {
            if (!_hasPrevPage || _firstIdOnPage == 0) return;
            SetLoading(true);
            try
            {
                using (var context = new POSDbContext())
                {
                    var page = await new CustomerLedgerRepository(context)
                        .GetCustomerBalancePageAsync(
                            lastCustomerId: _firstIdOnPage,
                            goingForward: false,
                            filter: _activeFilter,
                            search: _activeSearch,
                            pageSize: PageSize);
                    BindPage(page);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { SetLoading(false); }
        }

        // ─── Bind helpers ─────────────────────────────────────────────────────
        private void BindPage(CustomerBalancePage page)
        {
            _currentPage = page.Rows;
            _hasNextPage = page.HasNextPage;
            _hasPrevPage = page.HasPrevPage;
            _firstIdOnPage = _currentPage.Any() ? _currentPage.First().CustomerId : 0;
            _lastIdOnPage = _currentPage.Any() ? _currentPage.Last().CustomerId : 0;

            BalanceGrid.DataSource = null;
            BalanceGrid.DataSource = _currentPage;
            UpdatePaginationBar();
        }

        private void UpdateKpis(DashboardKpi kpi)
        {
            lblTotalLoanAmount.Text = $"PKR {kpi.TotalLoanAmount:N2}";
            lblLoanCount.Text = $"{kpi.LoanCustomerCount} customers";
            lblTotalAdvanceAmount.Text = $"PKR {kpi.TotalAdvanceAmount:N2}";
            lblAdvanceCount.Text = $"{kpi.AdvanceCustomerCount} customers";
        }

        private void UpdatePaginationBar()
        {
            PrevBtn.Enabled = _hasPrevPage;
            NextBtn.Enabled = _hasNextPage;

            int showing = _currentPage.Count;
            lblCount.Text = showing == 0
                ? "No customers found"
                : $"Showing {showing} customer{(showing == 1 ? "" : "s")}";

            lblPageInfo.Text =
                (_hasPrevPage ? "◀ Previous    " : "              ") +
                (_hasNextPage ? "    Next ▶" : "");
        }

        private void SetLoading(bool loading)
        {
            BalanceGrid.Visible = !loading;
            lblLoading.Visible = loading;
            PrevBtn.Enabled = !loading && _hasPrevPage;
            NextBtn.Enabled = !loading && _hasNextPage;
        }

        // ─── Grid events ──────────────────────────────────────────────────────
        private void BalanceGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentPage.Count) return;
            var row = _currentPage[e.RowIndex];
            var colName = BalanceGrid.Columns[e.ColumnIndex].Name;

            // Style Delete button red so it looks dangerous
            if (colName == "colDelete")
            {
                e.CellStyle.BackColor = Color.FromArgb(192, 0, 0);
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                return;
            }

            colName = BalanceGrid.Columns[e.ColumnIndex].DataPropertyName;

            if (colName == "BalanceDisplay")
            {
                e.CellStyle.ForeColor = row.BalanceType == BalanceType.Loan
                    ? Color.FromArgb(192, 0, 0)
                    : Color.FromArgb(0, 102, 204);
                e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }

            if (colName == "BalanceType")
            {
                switch (row.BalanceType)
                {
                    case BalanceType.Loan:
                        e.CellStyle.BackColor = Color.FromArgb(192, 0, 0);
                        e.CellStyle.ForeColor = Color.White;
                        e.Value = "🔴 LOAN"; break;
                    case BalanceType.Advance:
                        e.CellStyle.BackColor = Color.FromArgb(0, 102, 204);
                        e.CellStyle.ForeColor = Color.White;
                        e.Value = "🔵 ADVANCE"; break;
                    case BalanceType.Clear:
                        e.CellStyle.BackColor = Color.FromArgb(39, 174, 96);
                        e.CellStyle.ForeColor = Color.White;
                        e.Value = "✅ CLEAR"; break;
                }
            }
        }

        private void BalanceGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentPage.Count) return;
            OpenLedger(_currentPage[e.RowIndex]);
        }

        private async void BalanceGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentPage.Count) return;

            string colName = BalanceGrid.Columns[e.ColumnIndex].Name;

            // ── Receive Payment button ────────────────────────────────────────
            if (colName == "colAction")
            {
                var cust = _currentPage[e.RowIndex];
                if (cust.BalanceType != BalanceType.Loan)
                {
                    MessageBox.Show(
                        "This customer has no outstanding loan.\n" +
                        "Use the ledger to add advance or adjustment.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var frm = new Customerpaymentform(cust.CustomerId, cust.CustomerName, cust.Balance, false);
                if (frm.ShowDialog(this) == DialogResult.OK && frm.PaymentPosted)
                    await LoadFirstPageAsync();
            }

            // ── Delete History button (only visible on Clear filter) ──────────
            else if (colName == "colDelete")
            {
                var cust = _currentPage[e.RowIndex];

                // Safety check — never delete a customer who still has a balance
                if (cust.BalanceType != BalanceType.Clear)
                {
                    MessageBox.Show(
                        "You can only delete history for customers with a zero balance (Clear status).",
                        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show(
                    $"This will permanently delete ALL ledger history for:\n\n" +
                    $"  {cust.CustomerName}\n\n" +
                    $"This cannot be undone. Are you sure?",
                    "⚠️ Delete History",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);  // No is default — safer

                if (confirm != DialogResult.Yes) return;

                try
                {
                    using (var context = new POSDbContext())
                    {
                        await new CustomerLedgerRepository(context)
                            .DeleteAllLedgerEntriesAsync(cust.CustomerId);
                    }

                    MessageBox.Show(
                        $"Ledger history deleted for {cust.CustomerName}.",
                        "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadFirstPageAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting history:\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>Shows or hides the Delete History column.</summary>
        private void ToggleDeleteColumn(bool visible)
        {
            if (BalanceGrid.Columns.Contains("colDelete"))
                BalanceGrid.Columns["colDelete"].Visible = visible;
        }

        private void OpenLedger(CustomerBalanceSummary cust)
        {
            new Customerledgerform(cust.CustomerId, cust.CustomerName).ShowDialog(this);
            _ = LoadFirstPageAsync(); // Refresh KPIs + grid after any changes inside
        }

        // ─── Toolbar handlers ─────────────────────────────────────────────────
        private async void ManualEntryBtn_Click(object sender, EventArgs e)
        {
            var frm = new ManualLedgerEntryForm();
            if (frm.ShowDialog(this) == DialogResult.OK && frm.EntrySaved)
                await LoadFirstPageAsync();
        }

        private async void RefreshBtn_Click(object sender, EventArgs e) => await LoadFirstPageAsync();

        private void OpenLedgerBtn_Click(object sender, EventArgs e)
        {
            if (BalanceGrid.SelectedRows.Count == 0) return;
            int idx = BalanceGrid.SelectedRows[0].Index;
            if (idx >= 0 && idx < _currentPage.Count)
                OpenLedger(_currentPage[idx]);
        }

        // ─── Pagination handlers ──────────────────────────────────────────────
        private async void PrevBtn_Click(object sender, EventArgs e) => await LoadPrevPageAsync();
        private async void NextBtn_Click(object sender, EventArgs e) => await LoadNextPageAsync();
    }
}
